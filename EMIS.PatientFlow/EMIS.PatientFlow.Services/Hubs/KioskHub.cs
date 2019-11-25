using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace EMIS.PatientFlow.Services.Hubs
{
	[HubName("KioskHub")]
	public class KioskHub : Hub
	{
		private readonly ILoggerRepository _logRepository;
		private readonly IKioskRepository _kioskRepository;
		private readonly IAlertsRepository _alertRepository;
		private readonly IHubContext _hubContext = GlobalHost.ConnectionManager.GetHubContext<KioskHub>();
		public KioskHub(
			ILoggerRepository logRepository,
			IKioskRepository kioskRepository,
			IAlertsRepository alertRepository)
		{
			_kioskRepository = kioskRepository;
			_logRepository = logRepository;
			_alertRepository = alertRepository;
		}

		public void SendKioskConnectionDetails(string kioskAddress, string connectionId)
		{
			try
			{
				var kioskId = _kioskRepository.UpdateKioskConnection(kioskAddress, connectionId);

				var kiosk = _kioskRepository.GetKioskDetailsForSync(kioskId);
			    if (string.IsNullOrEmpty(kiosk.KioskGuid) || string.IsNullOrEmpty(kiosk.ConnectionGuid)) return;
			    UpdateKioskConfiguration(kiosk);

			    var newOrgs = kiosk.OrganisationList.Select(item => item.OrganisationName).ToList();
			    JoinGroup(connectionId, newOrgs);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "KioskHub Method : SendKioskConnectionDetails", ex, kioskAddress);
			}
		}

		// needs to check backward compatibility
		public void UpdateKioskConfiguration(Kiosk kiosk)
		{
            AdditionalInformation additionalInformation = _kioskRepository.GetAdditionalInformation(kiosk.KioskGuid);

			if(additionalInformation == null)
			{
				additionalInformation = new AdditionalInformation();
			}
			foreach (Alert alert in additionalInformation.AlertList)
            {
                alert.SessionHolderIdList = _alertRepository.GetSessionHolderIdForAlert(alert.Id);
            }

            if (kiosk.OrganisationList != null)
            {
                foreach (var org in kiosk.OrganisationList)
                {
                    if (org.SystemType == SystemType.EmisPcsLan.GetDisplayName())
                    {
                        org.SystemType = SystemType.EmisPcs.GetDisplayName();
                    }
                }
            }

            if (!string.IsNullOrEmpty(kiosk.ConnectionGuid))
				_hubContext.Clients.Client(kiosk.ConnectionGuid).updateKioskConfiguration(kiosk, additionalInformation);
		}

		public void UpdateQuestionnaire(List<int> questionnaireId, string connectionGuid)
		{
			if (!string.IsNullOrEmpty(connectionGuid))
			{
				_hubContext.Clients.Client(connectionGuid).updateQuestionnaire(questionnaireId);
			}
		}

		public void AddPatient(string groupName, Patient patient)
		{
			if (groupName != null)
			{
				_hubContext.Clients.Group(groupName).addPatientMessage(new
				{
					PatientId = patient.PatientId,
					OrganisationId = patient.OrganisationId,
					Firstname = String.Empty,
					Surname = String.Empty,
					Dob = String.Empty,
					Message = patient.Message,
					PatientMessageId = patient.PatientMessageId
				});
			}
		}

		public void DeletePatient(string groupName, int patientMessageId, int organisationId)
		{
			if (groupName != null)
			{
				_hubContext.Clients.Group(groupName).deletePatientMessage(patientMessageId, organisationId);
			}
		}

		public void UpdateAlert(List<string> groupName, Alert alert)
		{
			if (groupName != null)
			{
				_hubContext.Clients.Groups(groupName).updateAlert(alert);
			}
		}

		public void DeleteAlert(List<string> groupName, int alertId)
		{
			if (groupName != null)
			{
				_hubContext.Clients.Groups(groupName).deleteAlert(alertId);
			}
		}

		public void UpdateAlertToIndividualKiosk(string connectionId, List<Alert> alertsList)
		{
			if (!String.IsNullOrEmpty(connectionId))
			{
				_hubContext.Clients.Client(connectionId).addAlert(alertsList);
			}
		}

		public void UpdateOrganisation(Organisation organisation, string groupName, string organisationKey)
		{
			_hubContext.Clients.Group(groupName).updateOrganisation(new
			{
				Id = organisation.Id,
				DatabaseName = organisationKey,
				OrganisationName = organisation.OrganisationName,
				SystemType = ConvertToSystemTypeEnum(organisation.SystemTypeId)
			});
		}

		private static string ConvertToSystemTypeEnum(int systemTypeId)
		{
			if (systemTypeId == Convert.ToInt32(SystemType.EmisWeb))
				return SystemType.EmisWeb.GetDisplayName();
			if (systemTypeId == Convert.ToInt32(SystemType.EmisPcs))
				return SystemType.EmisPcs.GetDisplayName();
			return SystemType.None.GetDisplayName();
		}

		public void JoinGroup(string connectionGuid, List<string> groups)
		{
			if (!String.IsNullOrEmpty(connectionGuid))
			{
				foreach (string groupName in groups)
				{
					_hubContext.Groups.Add(connectionGuid, groupName);
				}
			}
		}

		public void JoinSingleGroup(string connectionGuid, string groupName)
		{
			if (!String.IsNullOrEmpty(groupName))
			{
				_hubContext.Groups.Add(connectionGuid, groupName);
			}
		}

		public void RemoveGroup(string connectionGuid, List<string> groups)
		{
			if (!String.IsNullOrEmpty(connectionGuid))
			{
				foreach (string groupName in groups)
				{
					_hubContext.Groups.Remove(connectionGuid, groupName);
				}
			}
		}

		public override Task OnConnected()
		{
			string transport = Context.QueryString.First(p => p.Key == "transport").Value;
			return SaveConnection(Context.QueryString["registrationKey"], Context.ConnectionId, transport);
		}

		private Task SaveConnection(string registrationkey, string connectionId, string transport)
		{
			bool isSuccess;
			string registrationKey = registrationkey;
			string oldConnectionId = String.Empty;
			int kioskId = 0;
			try
			{
				if (!String.IsNullOrEmpty(registrationKey))
				{
					oldConnectionId = _kioskRepository.GetConnectionIdFromKey(registrationKey);
					kioskId = _kioskRepository.GetKioskIdFromKey(registrationKey);

				}
				if (kioskId > 0)
				{
					SendKioskConnectionDetails(registrationKey, connectionId);
					try
					{
						_hubContext.Clients.Client(connectionId).LogTransport(transport);
					}
					catch (Exception ex)
					{
						_logRepository.WriteLog(Entities.Enums.LogType.Debug, "KioskHub Method : Log Transport", ex, registrationKey);
					}
				}
				else
				{
					Clients.Caller.DeleteKiosk();
					_hubContext.Clients.Client(connectionId).SyncCompleted(true);
					return base.OnConnected();
				}
				isSuccess = true;
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Debug, "KioskHub Method : OnConnected", ex, registrationKey);
				isSuccess = false;
			}

			_hubContext.Clients.Client(Context.ConnectionId).SyncCompleted(isSuccess);
			if (!String.IsNullOrEmpty(oldConnectionId))
			{
				_hubContext.Clients.Client(oldConnectionId).disconnectKiosk();
			}
			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			string registrationKey = Context.QueryString["registrationKey"];
			return DisconnectKiosk(stopCalled, registrationKey);
		}

		private Task DisconnectKiosk(bool stopCalled, string registrationKey)
		{
			if (!String.IsNullOrEmpty(registrationKey))
			{
				try
				{
					_kioskRepository.DisconnectKiosk(registrationKey);
					_logRepository.WriteLog(Entities.Enums.LogType.Debug, "KioskHub Method : OnDisconnected", null, registrationKey);
					return base.OnDisconnected(stopCalled);
				}
				catch (Exception ex)
				{
					_logRepository.WriteLog(Entities.Enums.LogType.Debug, "KioskHub Method : OnDisconnected(stopCalled)", ex,
						registrationKey);
				}
			}
			return base.OnDisconnected(stopCalled);
		}

		public override Task OnReconnected()
		{
			_logRepository.WriteLog(Entities.Enums.LogType.Debug, "KioskHub Method : OnReconnected", null, Context.ConnectionId);
			return base.OnReconnected();
		}

		public void UpdateKioskStatus(string connectionGuid, int status)
		{
			if (!String.IsNullOrEmpty(connectionGuid))
			{
				_hubContext.Clients.Client(connectionGuid).updateStatus(status);
			}
		}

		public void UpdateKioskStatus(string connectionGuid, int status, string message)
		{
			if (!String.IsNullOrEmpty(connectionGuid))
			{
				_hubContext.Clients.Client(connectionGuid).updateStatusWithMessage(status, message);
			}
		}

		// needs to check backward compatibility
		public void SaveAdditionalInformation(string registrationKey,string ConnectionGuid)
		{
			AdditionalInformation additionalInformation = _kioskRepository.GetAdditionalInformation(registrationKey);
			foreach (var alert in additionalInformation.AlertList)
			{
				alert.OrganisationIds = _alertRepository.GetOrganisationIdListForAlert(alert.Id).Distinct().ToList();
				alert.SessionHolderIdList = _alertRepository.GetSessionHolderIdForAlert(alert.Id);
			}
           _hubContext.Clients.Client(ConnectionGuid).SaveAdditionalInformation(additionalInformation);
           
		}

		public void SaveLinkedMembers(string registrationKey, string connectionGuid)
		{
			List<Member> memberList = _kioskRepository.GetLinkedMembersForKiosk(registrationKey);
			if (!String.IsNullOrEmpty(connectionGuid))
			{
				_hubContext.Clients.Client(connectionGuid).SaveLinkedMembers(memberList);
			}
		}

		public void SaveKioskDetails(string registrationKey, string pcName, string ipAddress)
		{
			try
			{
				_kioskRepository.SaveKioskDetails(registrationKey, pcName, ipAddress);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "KioskHub Method : SaveKioskDetails", ex, registrationKey);
			}
		}

		public void SaveKioskDetails(string registrationKey, KioskSystemInformation systemInformation)
		{
			try
			{
				_kioskRepository.SaveKioskDetails(registrationKey, systemInformation);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "KioskHub Method : SaveKioskDetails", ex, registrationKey);
			}
		}

		public void CheckKioskConnectionStatus(int kioskId)
		{
			try
			{
				int status = _kioskRepository.GetKioskConnectionStatus(kioskId);

				_hubContext.Clients.Client(Context.ConnectionId).ChangeKioskStatus(status);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "KioskHub Method : CheckKioskConnectionStatus", ex, Convert.ToString(kioskId));
			}
		}

		public void SetDivert(bool status, int sessionHolderId, int organisationId, string organisationName)
		{
			if (!String.IsNullOrEmpty(organisationName))
			{
				_hubContext.Clients.Group(organisationName).setDivert(status, sessionHolderId, organisationId);
			}
		}

		internal void SetTPPDivert(bool status, int sessionHolderId, string loginId, int organisationId, string organisationName)
		{
			if (!String.IsNullOrEmpty(organisationName))
			{
				_hubContext.Clients.Group(organisationName).setTppDivert(status, sessionHolderId, loginId, organisationId);

			}
		}
		public void SetPublish(bool status, int questionnaireId, string organisationName)
		{
			if (!String.IsNullOrEmpty(organisationName))
			{
				_hubContext.Clients.Group(organisationName).setPublish(status, questionnaireId);
			}
		}

		public void DisconnectKiosk(int kioskId)
		{
			try
			{
				string connectionId = _kioskRepository.GetConnectionId(kioskId);
				if (!String.IsNullOrEmpty(connectionId))
				{
					_hubContext.Clients.Client(connectionId).disconnectKiosk();
				}
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "KioskHub Method : DisconnectKiosk", ex, Convert.ToString(kioskId));
			}
		}

		public void CheckKioskConnection(string registrationKey)
		{
			try
			{
				if (!String.IsNullOrEmpty(registrationKey))
				{
					string oldConnectionId = _kioskRepository.GetConnectionIdFromKey(registrationKey);

					if (string.IsNullOrEmpty(oldConnectionId) || Context.ConnectionId != oldConnectionId)
					{
						_hubContext.Clients.Client(Context.ConnectionId).restartKiosk();
					}
				}
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "", ex, registrationKey);
			}
		}
        public void UpdateMember(int OrganisationId)
        {
            var kiosksList = _kioskRepository.GetKioskListForOrganisation(OrganisationId);
            foreach (var item in kiosksList)
            {
                if (item.ConnectionGuid != string.Empty)
                {
                    SaveAdditionalInformation(item.KioskGuid, item.ConnectionGuid);
                }
            }
        }
	}
}