using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.AspNet.SignalR.Client;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.Kiosk.HubClients
{
	public class KioskHubClient : BaseHubClient
	{
		private readonly IPushServiceRepository _repository;
		private readonly IPushServiceAlertRepository _alertRepository;
        private readonly IQuestionnaireRepository _questionnaireRepository;
        private readonly string _registrationKey = Utilities.GetAppSettingValue("RegistrationKey");
		private static readonly KioskHubClient _single = new KioskHubClient();
		public static KioskHubClient Instance
		{
			get { return _single; }
		}

		public KioskHubClient()
		{
			_repository = DiResolver.CurrentInstance.Reslove<IPushServiceRepository>();
			_alertRepository = DiResolver.CurrentInstance.Reslove<IPushServiceAlertRepository>();
            _questionnaireRepository = DiResolver.CurrentInstance.Reslove<IQuestionnaireRepository>();

            Init();
			if (HubProxy == null)
			{
				HubProxy = HubConnection.CreateHubProxy("KioskHub");

				HubProxy.On<int>(
					"UpdateStatus",
					status =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									UpdateStatus(status))));

                HubProxy.On<int, string>(
					"UpdateStatusWithMessage",
					(status, message) =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									UpdateStatusWithMessage(status, message))));

				HubProxy.On<Model.Kiosk, AdditionalInformation>(
					"UpdateKioskConfiguration",
					(kiosk, additionalInformation) =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									UpdateKioskConfiguration(kiosk, additionalInformation))));
			
				HubProxy.On<List<Alerts>>(
					"AddAlert",
					alert =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									_alertRepository.AddAlert(alert))));

				HubProxy.On<PatientMessage>(
					"AddPatientMessage",
					patient =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									_repository.AddPatientMessage(patient))));

				HubProxy.On<int, int>(
					"DeletePatientMessage",
					(patientMessageId,
						organisationId) =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									_repository.DeletePatientMessage(
										patientMessageId))));

				HubProxy.On<Alerts>(
					"UpdateAlert",
					alert =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									_alertRepository.UpdateAlert(alert))));

				HubProxy.On<int>(
					"DeleteAlert",
					alertId =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									_alertRepository.DeleteAlert(alertId))));

				HubProxy.On<Organisation>(
					"UpdateOrganisation",
					organisation =>
						Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									UpdateOrganisation(organisation))));

				HubProxy.On(
					"DisconnectKiosk",
					() =>
						Application.Current.Dispatcher.Invoke(
							new Action(DisconnectKiosk)));

				HubProxy.On(
					"RestartKiosk",
					() =>
						Application.Current.Dispatcher.Invoke(
							new Action(RestartKiosk)));
			
				HubProxy.On<bool>("SyncCompleted", _syncStatus =>
				Application.Current.Dispatcher.Invoke(
							new Action(
								() =>
									SyncCompleted(_syncStatus))));

				HubProxy.On<List<LinkedMember>>(
					"SaveLinkedMembers",
					member => { _repository.SaveLinkedMembers(member); });

				HubProxy.On<bool, int, int>(
					"SetDivert",
					(status, sessionHolderId, organisationId) => { _repository.SetDivert(status, sessionHolderId, organisationId); });

				HubProxy.On<bool,int, string, int>(
				"SetTppDivert",
				(status, sessionHolderId, loginId, organisationId) => { SetTppDivert(status, sessionHolderId, loginId, organisationId); });

				HubProxy.On(
					 "DeleteKiosk",
					 () =>
						 Application.Current.Dispatcher.Invoke(
							 new Action(DeleteKiosk)));

				HubProxy.On<String>(
					"LogTransport", (transport) => { LogTransport(transport); });


                HubProxy.On<AdditionalInformation>(
               "SaveAdditionalInformation",
               (additionalInformation) =>
               Application.Current.Dispatcher.Invoke(new Action(
                   () =>
                   SaveAdditionalInfoMethod(additionalInformation)
                   )));

                HubProxy.On<bool,int>(
                    "setPublish",
                    (status,questionnaireId) =>
                        Application.Current.Dispatcher.Invoke(
                            new Action(
                                () =>
                                    _questionnaireRepository.SetPublish(status,questionnaireId))));
            }
        }

		private void SetTppDivert(bool status,int sessionHolderId, string loginId, int organisationId)
		{
			_repository.SetTPPDivert(status, sessionHolderId, loginId, organisationId);

			Logger.Instance.WriteLog(LogType.Info, "Divert enabled for username: " + loginId, null, _registrationKey);
		}

		private void LogTransport(string transport)
		{
			Logger.Instance.WriteLog(LogType.Info, "INFO: SignalR is connected using transport " + transport, null, _registrationKey);
		}

		private void UpdateKioskConfiguration(Model.Kiosk kiosk, AdditionalInformation additionalInformation)
		{
			try
			{
				if (kiosk.KioskGuid == Utilities.GetAppSettingValue("RegistrationKey"))
				{
					Logger.Instance.WriteLog(LogType.Info, "INFO: GetKioskData(UpdateKioskConfiguration) is requested", null, _registrationKey);

				    UpdateKioskConfig(kiosk);

				    if (kiosk.KioskLogoByte != null && kiosk.KioskLogoByte.Length > 0)
				    {
				        _repository.SaveKioskLogo(kiosk.KioskLogoByte);
				    }
                    else
                    {
                        _repository.DeleteKioskLogo();
                    }

				    Logger.Instance.WriteLog(LogType.Info, "INFO: GetAdditionalInformation(UpdateKioskConfiguration) is requested", null, _registrationKey);
                    if (additionalInformation != null)
                        SaveAdditionalInfoMethod(additionalInformation);
                    else
                        SaveAdditionalInformation();
                    //SaveAdditionalInfoMethod(additionalInformation.MemberList, additionalInformation.PatientList, additionalInformation.AlertList, additionalInformation.DivertList);
                }
				else
				{
					Logger.Instance.WriteLog(LogType.Info, "INFO: KioskGuid does not match with requested kiosk", null, kiosk.KioskGuid);
				}

			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}

		}

		private void UpdateKioskConfig(Model.Kiosk kiosk)
		{
			_repository.UpdateKioskConfig(kiosk);

			Logger.Instance.WriteLog(LogType.Info, "INFO: Update Kiosk Config done", null, _registrationKey);

			
			Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
			{
				if (GlobalVariables.IsHomePage)
					Messenger.Default.Send(AppPages.HomePage);
			}));

		}
        private void SaveAdditionalInfoMethod(AdditionalInformation additionalInformation)
        {
            Logger.Instance.WriteLog(LogType.Info, "INFO: Alert, Members patient and divert is saved", null, _registrationKey);
            _repository.SaveAdditionalInformation(additionalInformation.MemberList,
                additionalInformation.PatientList,
                additionalInformation.AlertList,
                additionalInformation.DivertList
                );
        }
		private void SyncCompleted(bool isSuccess)
		{
			if (!isSuccess)
			{
				Task.Factory.StartNew(() =>
				{
					KioskHubClient.Instance.CloseHub();
				});

				GlobalVariables.IsSyncErrorFree = false;
			}
			else
			{
				GlobalVariables.IsSyncErrorFree = true;
			}

			GlobalVariables.IsDataAvailable = true;

			GlobalVariables.ConnectionStatus = isSuccess;
		}

		private void UpdateStatus(int status)
		{
			try
			{
				if (status == Convert.ToInt32(ConnectionStatus.Restart))
				{
					Logger.Instance.WriteLog(LogType.Info, "INFO: Kiosk Restart", null, _registrationKey);
					Process.Start("shutdown", "/r /f /t 0");
				}
				else
				{
					_repository.UpdateKioskStatus(status);
					if (status == Constants.KioskOffline)
					{
						GlobalVariables.KioskStatus = Constants.StatusOutOfService;
						Messenger.Default.Send(AppPages.OutOfService);
					}
					else if (GlobalVariables.IsDbConnected)
					{
						GlobalVariables.KioskStatus = Constants.StatusOnline;
						Messenger.Default.Send(AppPages.HomePage);
					}
					else
					{
						Messenger.Default.Send(AppPages.ExceptionDivert);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}
		}

		private void UpdateStatusWithMessage(int status, string message)
		{
			if(status == 0 && !String.IsNullOrEmpty(message))
			{
				_repository.UpdateMessage(message);
			}
			UpdateStatus(status);
		}

		private void UpdateOrganisation(Organisation organisation)
		{
			try
			{
				_repository.UpdateOrganisation(organisation);
				HubProxy.Invoke("JoinSingleGroup", HubConnection.ConnectionId, organisation.OrganisationName);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}
		}

        private void DisconnectKiosk()
		{
			try
			{
				App.StopConnection.Reset();
				Logger.Instance.WriteLog(LogType.Info, "INFO: Key Reused", null, _registrationKey);
				KioskHubClient.Instance.CloseHub();
				GlobalVariables.IsUsedKey = true;
				Messenger.Default.Send(AppPages.OutOfService);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}
		}

		private void RestartKiosk()
		{
			try
			{
				App.StopConnection.Reset();
				KioskHubClient.Instance.CloseHub();
				if (KioskHubClient.Instance.StartHub())
				{
					Instance.SaveKioskDetails();					
				}
				Logger.Instance.WriteLog(LogType.Info, "INFO: Restarting Kiosk Connection", null, _registrationKey);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, "Error in Restarting Kiosk connection " + ex.Message, ex, _registrationKey);
			}
		}

		private void DeleteKiosk()
		{
			try
			{
				App.StopConnection.Reset();
				GlobalVariables.IsUsedKey = true;
				GlobalVariables.IsKioskDeleted = true;
				Messenger.Default.Send(AppPages.ExceptionDivert);
				_repository.DeleteKiosk();

				Logger.Instance.WriteLog(LogType.Info, "INFO: Delete Kiosk Called ", null, _registrationKey);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}
		}

		public void SaveKioskDetails()
		{
			try
			{
				IPHostEntry hostByName = Dns.GetHostEntry(Dns.GetHostName());
				string hostName = hostByName.HostName;
				string ipAddress = string.Empty;
				if (hostByName.AddressList != null && hostByName.AddressList.Any(a => a.AddressFamily == AddressFamily.InterNetwork))
				{
					ipAddress = hostByName.AddressList.First(a => a.AddressFamily == AddressFamily.InterNetwork).ToString();
				}

				if (StartHub())
				{
					HubProxy.Invoke("SaveKioskDetails", _registrationKey, new { PcName = hostName, IpAddress = ipAddress, Version = Assembly.GetExecutingAssembly().GetName().Version.ToString() });
					Logger.Instance.WriteLog(LogType.Info, "INFO: SaveKioskDetails", null, _registrationKey);
				}

			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}
		}

		public void IsConnected()
		{
			try
			{
				if (StartHub())
					HubProxy.Invoke("SendKioskConnectionDetails", _registrationKey, HubConnection.ConnectionId);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}

		}
        public void SaveAdditionalInformation()
        {
            try
            {
                if (StartHub())
                    HubProxy.Invoke("SaveAdditionalInformation", _registrationKey, HubConnection.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
            }
        }
    }
}
