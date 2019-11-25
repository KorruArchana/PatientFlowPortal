using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.SyncService.Helper
{
	public class ApiHelper
	{
		private readonly static string apiBaseAddress = Utility.GetAppSettingValue("ServerURI");

		internal static async Task<string> GetPatientFlowUser()
		{
			string response = await RunAsync(new Uri(apiBaseAddress + "api/SyncData/GetPatientFlowUser/"));
			return response;
		}

		internal static async Task<string> GetAllQuestionnaires()
		{
			string response = await RunAsync(new Uri(apiBaseAddress + "api/SyncData/GetAllQuestionnaires/"));
			return response;
		}

		internal static async Task<string> GetQuestionnaire(int questionnaireId)
		{
			string response = await RunAsync(new Uri(string.Format("{0}api/SyncData/GetQuestionnaire/?questionnaireId={1}", apiBaseAddress, questionnaireId)));
			return response;
		}

		internal static async Task<string> GetRecentlyUpdatedQuestionnaire()
		{
			string response = await RunAsync(new Uri(apiBaseAddress + "api/SyncData/GetRecentlyUpdatedQuestionnaire/"));
			return response;
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
					Logger.Instance.WriteLog(Common.Enums.LogType.Info, string.Format("INFO: {2} - Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase, path.ToString()),
						   null, Utility.GetAppSettingValue("ProductKey"));
				}

				return string.Empty;
			}
		}

		//internal static async Task<string> WriteLogs1(List<Data.Log> logs)
		//{
		//	string response = await PostAsJsonAsync(new Uri(apiBaseAddress + "api/SyncData/SaveLogs/"), logs);
		//	return response;
		//}

		//internal static async Task<string> Writesurveys1(List<Data.Survey> surveys)
		//{
		//	string response = await SurveyPostAsJsonAsync(new Uri(apiBaseAddress + "api/SyncData/SaveAnonymousSurvey/"), surveys);
		//	return response;
		//}

        internal static async Task<string> WriteLogs(List<Data.Log> logs)
        {
            string response = await PostAsJsonAsync(new Uri(apiBaseAddress + "api/SyncData/UpdateUsageRecords/"), logs);
            return response;
        }

        internal static async Task<string> Writesurveys(List<Data.Survey> surveys)
        {
            string response = await SurveyPostAsJsonAsync(new Uri(apiBaseAddress + "api/SyncData/SaveAnonymousSurveyResult/"), surveys);
            return response;
        }

        public static async Task<string> PostAsJsonAsync(Uri path, List<Data.Log> data)
		{
			using (var client = new HttpClient(new CustomDelegatingHandler()))
			{
				var jsonKiosk = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data);
				var content = new StringContent(jsonKiosk, System.Text.Encoding.UTF8, "application/json");
				var response = await client.PostAsync(path.ToString(), content);

				if (!response.IsSuccessStatusCode)
				{
					Logger.Instance.WriteLog(Common.Enums.LogType.Info, string.Format("INFO: Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase),
						   null, Utility.GetAppSettingValue("ProductKey"));
				}

				return await response.Content.ReadAsStringAsync();
			}
		}

		public static async Task<string> SurveyPostAsJsonAsync(Uri path, List<Data.Survey> data)
		{
			using (var client = new HttpClient(new CustomDelegatingHandler()))
			{
				var jsonKiosk = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data);
				var content = new StringContent(jsonKiosk, System.Text.Encoding.UTF8, "application/json");
				var response = await client.PostAsync(path.ToString(), content);

				if (!response.IsSuccessStatusCode)
				{
					Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Info, "INFO: In post async method failure.", null, Utility.GetAppSettingValue("ProductKey"));
				}

				return await response.Content.ReadAsStringAsync();
			}
		}

	}
}
