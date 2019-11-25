using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.Services.Hubs
{
	[HubName("SyncServiceHub")]
	public class SyncHub : Hub
	{
		private readonly ILoggerRepository _logRepository;
		private readonly ISyncServiceRepository _syncRepository;
		private readonly IOrganisationRepository _orgRepository;
		private readonly IHubContext _hubContext;

		public SyncHub(
			ILoggerRepository logRepository,
			ISurveyRepository surveyRepository,
			ISyncServiceRepository syncRepository,
			IOrganisationRepository orgRepository,
			IQuestionnaireRepository questionnaireRepository,
			ILanguageRepository languageRepository)
		{
			_logRepository = logRepository;
			_syncRepository = syncRepository;
			_orgRepository = orgRepository;
			_hubContext = GlobalHost.ConnectionManager.GetHubContext<SyncHub>();
		}

		public override Task OnConnected()
		{
			var productKey = string.IsNullOrEmpty(Context.QueryString["id"]) ? Guid.Empty : new Guid(Context.QueryString["id"]);

			return SaveConnection(productKey);
		}

		private Task SaveConnection(Guid productKey)
		{
			try
			{
				if (!_syncRepository.IsExistSyncService(productKey))
				{
					Clients.Caller.DisconnectFromServer();

					return base.OnConnected();
				}

				UpdateConnectionDetails(productKey, Context.ConnectionId);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : OnConnected", ex, productKey.ToString());
			}

			return base.OnConnected();
		}

		public void UpdateConnectionDetails(Guid productKey, string connectionId)
		{
			try
			{
				var result = _syncRepository.SyncServiceConnected(productKey, new Guid(connectionId), 'S');

				if (result == -1)
				{
					Clients.Caller.DisconnectFromServer();
				}
				else
				{
					SaveClientConfiguration(productKey);
				}
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : OnConnected-UpdateConnectionDetails", ex, productKey.ToString());
			}
		}

		public void SaveClientConfiguration(Guid productKey)
		{
			string connectionId = Context.ConnectionId;

			UpdateClientConfiguration(productKey, connectionId);
		}

		public void UpdateClientConfiguration(Guid productKey, string connectionId)
		{
			try
			{
				var organisations = _syncRepository.GetSyncServiceOrganisations(productKey);

				if (organisations == null || organisations.Count == 0)
				{
					_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : OnConnected", new Exception("Dev Error Log : SaveClientConfiguration"), productKey.ToString());
				}
				else
				{
					foreach (var group in organisations)
					{
						_hubContext.Groups.Add(connectionId, group.Value);
					}

					this._hubContext.Clients.Client(connectionId).GetUpdatedConfiguration();
					this._hubContext.Clients.Client(connectionId).SaveAllQuestionnaires();
				}
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : SaveClientConfiguration", ex, productKey.ToString());
			}
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			var productKey = string.IsNullOrEmpty(Context.QueryString["id"]) ? Guid.Empty : new Guid(Context.QueryString["id"]);

			return DisconnectSync(stopCalled, productKey);
		}

		private Task DisconnectSync(bool stopCalled, Guid productKey)
		{
			try
			{
				_syncRepository.SyncServiceDisconnected(productKey, new Guid(Context.ConnectionId), 'S');
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : OnConnected", ex, productKey.ToString());
			}

			return base.OnDisconnected(stopCalled);
		}

		public void TransferGroup(int organisationId, string groupName)
		{
			foreach (var conn in _syncRepository.GetSyncServiceConnections(organisationId, 'S').Where(conn => !String.IsNullOrEmpty(conn)))
			{
				_hubContext.Groups.Add(conn, groupName);
			}
		}

		public void SaveWebClientConfiguration(WebUser webUser, string groupName)
		{
			try
			{
				_hubContext.Clients.Group(groupName).GetUpdatedConfiguration();
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : SaveWebClientConfiguration", ex);
			}
		}

		public void SaveQuestionnaire(Questionnaire questionnaire)
		{
			try
			{
				string organisationName = _orgRepository.GetOrganisationDetail(questionnaire.OrganisationId).OrganisationName;
				_hubContext.Clients.Group(organisationName).saveQuestionnaire(questionnaire.Id);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : SaveQuestionnaire", ex);
			}
		}

		public void SaveQuestion(Questions question)
		{
			try
			{
				string organisationName = _orgRepository.GetOrganisationDetail(question.OrganisationId).OrganisationName;
				_hubContext.Clients.Group(organisationName).saveQuestionnaire(question.QuestionnaireId);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : SaveQuestion", ex);
			}
		}

		public void DeleteQuestion(int questionId, int organisationId)
		{
			try
			{
				string organisationName = _orgRepository.GetOrganisationDetail(organisationId).OrganisationName;

				_hubContext.Clients.Group(organisationName).deleteQuestion(questionId);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : DeleteQuestion", ex);
			}
		}

		public void DeleteQuestionnaire(int questionnaireId, int organisationId)
		{
			try
			{
				string organisationName = _orgRepository.GetOrganisationDetail(organisationId).OrganisationName;

				_hubContext.Clients.Group(organisationName).deleteQuestionnaire(questionnaireId);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : DeleteQuestionnaire", ex);
			}
		}

		public void CheckSyncConnection(string productKey)
		{
			string connectionId = Context.ConnectionId;
			string key = productKey;
			ValidateSyncConnection(key, connectionId);
		}

		private void ValidateSyncConnection(string key, string connectionId)
		{
			var result = _syncRepository.SyncServiceConnected(new Guid(key), new Guid(connectionId), 'S');

			if (result == -1)
			{
				Clients.Caller.RestartSyncServer();
			}
		}
	}
}