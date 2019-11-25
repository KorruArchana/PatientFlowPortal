using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public static class KioskDataHelper
	{
		private readonly static string apiBaseAddress = Utilities.GetAppSettingValue("ServerURI");

		internal static async Task<string> GetKioskData()
		{
			string response = await RunAsync(new Uri(apiBaseAddress + "api/KioskData/GetKioskData/"));
			return response;
		}

		internal static async Task<string> GetAdditionalInformation()
		{
			string response = await RunAsync(new Uri(apiBaseAddress + "api/KioskData/GetAdditionalInformation/"));
			return response;
		}

		internal static async Task<string> GetAlerts()
		{
			string response = await RunAsync(new Uri(apiBaseAddress + "api/KioskData/GetAlerts/"));
			return response;
		}

		internal static async Task<string> GetLinkedMembersForKiosk()
		{
			string response = await RunAsync(new Uri(apiBaseAddress + "api/KioskData/GetLinkedMembersForKiosk/"));
			return response;
		}

		internal static async Task<string> GetPatientFlowUser()
		{
			string response = await RunAsync(new Uri(apiBaseAddress + "api/KioskData/GetPatientFlowUser/"));
			return response;
		}

		internal static async Task SaveKioskDetails(Model.KioskSystemInformation systemInformation)
		{
			await PostAsJsonAsync(new Uri(apiBaseAddress + "api/KioskData/SaveKioskDetails/"), systemInformation);
		}

		static async Task<string> RunAsync(Uri path)
		{
			using (HttpClient client = new HttpClient(new CustomDelegatingHandler()))
			{
				HttpResponseMessage response = await client.GetAsync(path);

				if (response.IsSuccessStatusCode)
				{
					string responseString = await response.Content.ReadAsStringAsync();
					return responseString;
				}
				else
				{
					string message = string.Format("Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
					Logger.Instance.WriteLog(Common.Enums.LogType.Info, message, null, Utilities.GetAppSettingValue("RegistrationKey"));
				}

				return string.Empty;
			}
		}


		public static async Task<string> PostAsJsonAsync(Uri path, Model.KioskSystemInformation systemInformation)
		{
			using (var client = new HttpClient(new CustomDelegatingHandler()))
			{
				var jsonKiosk = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(systemInformation);
				var content = new StringContent(jsonKiosk, System.Text.Encoding.UTF8, "application/json");
				var response = await client.PostAsync(path.ToString(), content);
				if (!response.IsSuccessStatusCode)
				{
					string message = string.Format("systemInformation : Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
					Logger.Instance.WriteLog(Common.Enums.LogType.Info, message, null, Utilities.GetAppSettingValue("RegistrationKey"));
				}

				return await response.Content.ReadAsStringAsync();
			}
		}
	}
}
