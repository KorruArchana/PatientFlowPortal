using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.SyncService.Data;
using EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces;
using EMIS.PatientFlow.SyncService.Helper;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;

namespace EMIS.PatientFlow.SyncService.Hubs
{
	public sealed class SyncHubClient : BaseHubClient
	{
		private const string _hubName = "SyncServiceHub";
		private string SyncProductKey = Utility.GetAppSettingValue("ProductKey");
		private static readonly SyncHubClient _single = new SyncHubClient();

		public static SyncHubClient Instance
		{
			get { return _single; }
		}

		private SyncHubClient()
		{
			Init();

			if (HubProxy == null)
			{
				HubProxy = HubConnection.CreateHubProxy(_hubName);

				HubProxy.On<int>("SaveQuestionnaire", SaveQuestionnaire);
				HubProxy.On("SaveAllQuestionnaires", SaveAllQuestionnaires);
				HubProxy.On("DisconnectFromServer", DisconnectFromServer);
				HubProxy.On("RestartSyncServer", RestartSyncServer);
				HubProxy.On<int>("DeleteQuestion", DeleteQuestion);
				HubProxy.On<int>("DeleteQuestionnaire", DeleteQuestionnaire);
				HubProxy.On("GetUpdatedConfiguration", GetUpdatedConfiguration);				
			}
		}

		public async void GetUpdatedConfiguration()
		{
			
			try
			{
				Logger.Instance.WriteLog(LogType.Info, "INFO: GetPatientFlowUser(GetUpdatedConfiguration) is requested", null, SyncProductKey);
				string response = await ApiHelper.GetPatientFlowUser();
				var listOfUser = JsonConvert.DeserializeObject<List<ApiUser>>(response);

				var repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();

				if (listOfUser.Count < 1)
				{
					Logger.Instance.WriteLog(LogType.Info, "INFO: Error in WriteLogs ", null, SyncProductKey);
				}
				List<int> OrgsList = new List<int>();
				foreach (var user in listOfUser)
				{
					OrgsList.Add(user.OrganisationId);
					repository.SaveWebClientConfiguration(user);
				}

				repository.UpdateClientConfiguration(OrgsList);
				Logger.Instance.WriteLog(LogType.Info, "Saved Web Configuration", null, SyncProductKey);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}

		public async void SyncLog()
		{
			try
			{
				var repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
				var logs = repository.GetLogs();

				if (logs == null || logs.Count == 0)
					return;

				Logger.Instance.WriteLog(LogType.Info, "INFO: WriteLogs is requested", null, SyncProductKey);
				string response = await ApiHelper.WriteLogs(logs);
				if (!string.IsNullOrEmpty(response) && response.Length>0)
				{
					SyncCompleted(SyncType.Log, long.Parse(response));
					Logger.Instance.WriteLog(LogType.Info, "INFO: Logs are synced.", null, SyncProductKey);
				}
				else
				{ 
					Logger.Instance.WriteLog(LogType.Info, "INFO: Error in WriteLogs ", null, SyncProductKey);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}

		public async void SyncAnonymousSurvey()
		{
			try
			{
				var repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
				var surveys = repository.GetAnonymousSurveys();

				if (surveys == null || surveys.Count == 0)
					return;

				Logger.Instance.WriteLog(LogType.Info, "INFO: Writesurveys is requested", null, SyncProductKey);
				string response = await ApiHelper.Writesurveys(surveys);
				if (!string.IsNullOrEmpty(response) && response.Length > 0)
				{
					SyncCompleted(SyncType.AnonymousSurvey, long.Parse(response));
					Logger.Instance.WriteLog(LogType.Info, "INFO: Anonymous survey Logs are synced.", null, SyncProductKey);
				}
				else
				{
					Logger.Instance.WriteLog(LogType.Info, "INFO: Error in Writesurveys ", null, SyncProductKey);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error,ex.Message, ex, SyncProductKey);
			}
		}

		public void IsConnected()
		{
			try
			{
				if (StartHub())
					HubProxy.Invoke("UpdateConnectionDetails", SyncProductKey, HubConnection.ConnectionId);
				else
				{
					GetUpdatedConfiguration();
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}

		public void SyncCompleted(SyncType type, long lastItemId)
		{
			if (lastItemId == -1)
				return;

			UpdateSyncLog(type, lastItemId);
		}

		public void DisconnectFromServer()
		{
			CloseHub();
		}

		public void RestartSyncServer()
		{
			CloseHub();
			SyncHubClient.Instance.StartHub();
			//OrganisationHubClient.Instance.StartHub();
		}

		public async void SaveQuestionnaire(int questionnaireId)
		{
			try
			{
				Logger.Instance.WriteLog(LogType.Info, "INFO: GetQuestionnaire to save is requested", null, SyncProductKey);
				string response = await ApiHelper.GetQuestionnaire(questionnaireId);
				var questionnaire = JsonConvert.DeserializeObject<Questionnaire>(response);
				if (questionnaire == null)
				{
					Logger.Instance.WriteLog(LogType.Info, "INFO: Error in GetQuestionnaire to save", null, SyncProductKey);
					return;
				}
				SaveQuestionnaireDetails(questionnaire);
				Logger.Instance.WriteLog(LogType.Info, "Saved questionnaire by id successfully.", null, SyncProductKey);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}

		public async void SaveAllQuestionnaires()
		{
			try
			{
				Logger.Instance.WriteLog(LogType.Info, "INFO: GetAllQuestionnaires to save is requested", null, SyncProductKey);
				string response = await ApiHelper.GetAllQuestionnaires();
				var questionnaires = JsonConvert.DeserializeObject<List<Questionnaire>>(response);

				if (questionnaires == null)
				{
					Logger.Instance.WriteLog(LogType.Info, "INFO: Error in GetAllQuestionnaires to save", null, SyncProductKey);
					return;
				}

				foreach (var questionnaire in questionnaires)
				{
					SaveQuestionnaireDetails(questionnaire);
				}
				Logger.Instance.WriteLog(LogType.Info, "Saved all questionnaires successfully.", null, SyncProductKey);

			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}

		public async void SaveQuestionnairesByTimeStamp()
		{
			try
			{
				string response = await ApiHelper.GetAllQuestionnaires();
				var questionnaires = JsonConvert.DeserializeObject<List<Questionnaire>>(response);
				foreach (var questionnaire in questionnaires)
				{
					SaveQuestionnaireDetails(questionnaire);
				}
				Logger.Instance.WriteLog(LogType.Info, "Updated questionnaire by timestamp successfully.", null, SyncProductKey);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}

		private static void SaveQuestionnaireDetails(Questionnaire questionnaire)
		{
			var repository = DiResolver.CurrentInstance.Reslove<IQuestionnaireRepository>();
			repository.SaveQuestionnaire(questionnaire);
			repository.SaveQuestionInitialData(questionnaire.Questions);
			repository.SaveQuestionOptionInitialData(questionnaire.QuestionOptions);
		}

		public void DeleteQuestion(int questionId)
		{
			try
			{
				var repository = DiResolver.CurrentInstance.Reslove<IQuestionnaireRepository>();
				repository.DeleteQuestion(questionId);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}

		public void DeleteQuestionnaire(int questionnaireId)
		{
			try
			{
				var repository = DiResolver.CurrentInstance.Reslove<IQuestionnaireRepository>();
				repository.DeleteQuestionnaire(questionnaireId);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}

		public void UpdateSyncLog(SyncType type, long lastItemId)
		{
			try
			{
				var repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
				repository.SaveSyncLog(type, lastItemId);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncProductKey);
			}
		}
	}
}
