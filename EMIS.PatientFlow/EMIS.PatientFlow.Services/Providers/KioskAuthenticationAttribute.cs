using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities.Enums;

namespace EMIS.PatientFlow.Services.Providers
{
	public class KioskAuthenticationAttribute : Attribute, IAuthenticationFilter
	{
		private readonly UInt64 requestMaxAgeInSeconds = 300;  //5 mins
		private readonly string authenticationScheme = "Kiosk";
		private static Repositories.KioskRepository repository = new Repositories.KioskRepository();

		public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			var req = context.Request;

			if (req.Headers.Authorization != null && authenticationScheme.Equals(req.Headers.Authorization.Scheme, StringComparison.OrdinalIgnoreCase))
			{
				var rawAuthzHeader = req.Headers.Authorization.Parameter;

				var autherizationHeaderArray = AuthenticationHelper.GetAutherizationHeaderValues(rawAuthzHeader);

				if (autherizationHeaderArray != null)
				{
					var APPId = autherizationHeaderArray[0];
					var incomingBase64Signature = autherizationHeaderArray[1];
					var nonce = autherizationHeaderArray[2];
					var requestTimeStamp = autherizationHeaderArray[3];

					var isValid = IsValidRequest(req, APPId, incomingBase64Signature, nonce, requestTimeStamp);

					try
					{
						if (isValid.Exception == null && isValid.Result)
						{
							var currentPrincipal = new GenericPrincipal(new GenericIdentity(APPId), null);
							context.Principal = currentPrincipal;
						}
						else
						{
							context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
						}
					}
					catch (Exception ex)
					{
						context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
					}
				}
				else
				{
					context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
				}
			}
			else
			{
				context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
			}

			return Task.FromResult(0);
		}

		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			context.Result = new ResponseResult(context.Result, authenticationScheme);
			return Task.FromResult(0);
		}

		public bool AllowMultiple
		{
			get { return false; }
		}

		private async Task<bool> IsValidRequest(HttpRequestMessage req, string APPId, string incomingBase64Signature, string nonce, string requestTimeStamp)
		{
			string requestContentBase64String = "";
			string requestUri = HttpUtility.UrlEncode(req.RequestUri.AbsoluteUri.ToLower());
			string requestHttpMethod = req.Method.Method;

			if (repository.GetKioskIdFromKey(APPId) < 1) //(!ActiveProductKeys.Contains(APPId, StringComparer.OrdinalIgnoreCase))
			{
				Repositories.Logger.Instance.WriteLog(Entities.Enums.LogType.Info, "INFO: Error in Validating product key : " + APPId, null, "KioskAPIUser");
				return false;
			}

			var sharedKey = GetSharedKey(APPId);

			if (isReplayRequest(nonce, requestTimeStamp))
			{
				Repositories.Logger.Instance.WriteLog(Entities.Enums.LogType.Info, "INFO: Error in Validating isReplayRequest : " + APPId, null, "KioskAPIUser");
				return false;
			}

			byte[] hash = await ComputeHash(req.Content);

			if (hash != null)
			{
				requestContentBase64String = Convert.ToBase64String(hash);
			}

			string data = String.Format("{0}{1}{2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

			var secretKeyBytes = Convert.FromBase64String(sharedKey);

			byte[] signature = Encoding.UTF8.GetBytes(data);

			using (HMACSHA256 hmac = new HMACSHA256(secretKeyBytes))
			{
				byte[] signatureBytes = hmac.ComputeHash(signature);

				if (!incomingBase64Signature.Equals(Convert.ToBase64String(signatureBytes), StringComparison.Ordinal))
				{
					Repositories.Logger.Instance.WriteLog(Entities.Enums.LogType.Info, "INFO: Error in Validating isReplayRequest : " + APPId + " - " + sharedKey, null, "KioskAPIUser");
					return false;
				}
			}
			return true;

		}

		private string GetSharedKey(string productKey)
		{
			string commonName = "patientflowkioskunqiuevalue".EncryptAES256();
			return AuthenticationHelper.GeneratePassword(productKey.ToUpper(), commonName);
		}



		private bool isReplayRequest(string nonce, string requestTimeStamp)
		{
			if (System.Runtime.Caching.MemoryCache.Default.Contains(nonce))
			{
				return true;
			}

			DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan currentTs = DateTime.UtcNow - epochStart;
			var requestTotalSeconds = TimeSpan.FromSeconds(Convert.ToUInt64(requestTimeStamp));
			var ts = currentTs.Subtract(requestTotalSeconds);
			Repositories.Logger.Instance.WriteLog(LogType.Info, "INFO: Error in Validating isReplayRequest : requestTimeStamp " + requestTotalSeconds + " -  " + currentTs.TotalSeconds + " - " + (ts.Seconds > 300), null, "KioskAPIUser");

			System.Runtime.Caching.MemoryCache.Default.Add(nonce, requestTimeStamp, DateTimeOffset.UtcNow.AddSeconds(requestMaxAgeInSeconds));

			return false;
		}

		private static async Task<byte[]> ComputeHash(HttpContent httpContent)
		{
			using (MD5 md5 = MD5.Create())
			{
				byte[] hash = null;
				var content = await httpContent.ReadAsByteArrayAsync();
				if (content.Length != 0)
				{
					hash = md5.ComputeHash(content);
				}
				return hash;
			}
		}
	}
}