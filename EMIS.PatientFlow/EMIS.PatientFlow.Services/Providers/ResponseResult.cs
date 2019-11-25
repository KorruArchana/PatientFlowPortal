using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace EMIS.PatientFlow.Services.Providers
{

	public class ResponseResult : IHttpActionResult
	{
		private readonly string authenticationScheme;
		private readonly IHttpActionResult next;

		public ResponseResult(IHttpActionResult next)
		{
			this.next = next;
			authenticationScheme = "Kiosk";
		}

		public ResponseResult(IHttpActionResult next, string _authenticationScheme)
		{
			this.next = next;
			authenticationScheme = _authenticationScheme;
		}

		public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var response = await next.ExecuteAsync(cancellationToken);

			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(authenticationScheme));
			}

			return response;
		}
	}

	internal static class AuthenticationHelper
	{
		internal static string[] GetAutherizationHeaderValues(string rawAuthzHeader)
		{
			var credArray = rawAuthzHeader.Split(':');

			if (credArray.Length == 4)
			{
				return credArray;
			}
			else
			{
				return null;
			}
		}

		internal static string GeneratePassword(string productId, string commonName)
		{
			var deviceUniqueId = DateTime.UtcNow.ToString("ddMMMyyyy") + DateTime.UtcNow.AddDays(-1).DayOfWeek;
			var passwordText = String.Format("{0}{1}{2}", commonName, productId, deviceUniqueId);
			var secretKey = String.Format("{0}{1}", commonName, productId);
			var reversed = new string(secretKey.ToCharArray().Reverse().ToArray());
			return HmacSha256Digest(passwordText, reversed);
		}

		private static string HmacSha256Digest(string passwordText, string secret)
		{
			var encoding = new ASCIIEncoding();
			byte[] keyBytes = encoding.GetBytes(secret);
			byte[] messageBytes = encoding.GetBytes(passwordText);
			var cryptographer = new HMACSHA256(keyBytes);
			byte[] bytes = cryptographer.ComputeHash(messageBytes);
			return BitConverter.ToString(bytes).Replace("-", "").ToLower();
		}


	}
}