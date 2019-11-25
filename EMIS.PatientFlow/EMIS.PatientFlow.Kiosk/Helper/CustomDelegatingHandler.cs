﻿using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EMIS.PatientFlow.Common.Extensions;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public class CustomDelegatingHandler : DelegatingHandler
	{
		internal string AppId = Utilities.GetAppSettingValue("RegistrationKey").ToLower();
		private readonly string APIKey;
		private const string schema = "Kiosk";

		public CustomDelegatingHandler()
		{
			APIKey = GenerateAPIKey();
			InnerHandler = new HttpClientHandler();
		}

		private string GenerateAPIKey()
		{
			return GeneratePassword(AppId.ToUpper());
		}

		private string GeneratePassword(string productId)   // case matter for product id
		{
			var deviceUniqueId = DateTime.UtcNow.ToString("ddMMMyyyy") + DateTime.UtcNow.AddDays(-1).DayOfWeek;
			string commonName = "patientflowkioskunqiuevalue".EncryptAES256();   // different value for Sync

			var passwordText = String.Format("{0}{1}{2}", commonName, productId, deviceUniqueId);
			var secretKey = String.Format("{0}{1}", commonName, productId);
			var reversed = new string(secretKey.ToCharArray().Reverse().ToArray());
			return HmacSha256Digest(passwordText, reversed);
		}

		private string HmacSha256Digest(string passwordText, string secret)
		{
			var encoding = new ASCIIEncoding();
			byte[] keyBytes = encoding.GetBytes(secret);
			byte[] messageBytes = encoding.GetBytes(passwordText);
			var cryptographer = new HMACSHA256(keyBytes);
			byte[] bytes = cryptographer.ComputeHash(messageBytes);
			return BitConverter.ToString(bytes).Replace("-", "").ToLower();
		}

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{

			HttpResponseMessage response = null;
			string requestContentBase64String = string.Empty;

			string requestUri = System.Web.HttpUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower());

			string requestHttpMethod = request.Method.Method;
			DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan timeSpan = DateTime.UtcNow - epochStart;
			string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

			string nonce = Guid.NewGuid().ToString("N");

			//Checking if the request contains body, usually will be null wiht HTTP GET and DELETE
			if (request.Content != null)
			{
				byte[] content = await request.Content.ReadAsByteArrayAsync();
				MD5 md5 = MD5.Create();
				//Hashing the request body, any change in request body will result in different hash, we'll incure message integrity
				byte[] requestContentHash = md5.ComputeHash(content);
				requestContentBase64String = Convert.ToBase64String(requestContentHash);
			}

			//Creating the raw signature string
			string signatureRawData = String.Format("{0}{1}{2}{3}{4}{5}", AppId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

			var secretKeyByteArray = Convert.FromBase64String(APIKey);

			byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

			using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
			{
				byte[] signatureBytes = hmac.ComputeHash(signature);
				string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);
				//Setting the values in the Authorization header using custom scheme (amx)
				request.Headers.Authorization = new AuthenticationHeaderValue(schema, string.Format("{0}:{1}:{2}:{3}", AppId, requestSignatureBase64String, nonce, requestTimeStamp));
			}

			response = await base.SendAsync(request, cancellationToken);

			return response;
		}
	}
}
