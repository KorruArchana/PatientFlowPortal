using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using Newtonsoft.Json;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess
{
    public partial class DbAccess
    {
        private const string SpKioskInitialConfiguration = "[PatientFlow].[SetKioskInitialConfiguration]";
        private const string SpKioskConfiguration = "[PatientFlow].[SetKioskConfiguration]";
        public void UpdateKioskStatus(int status)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.Status), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", "{" + "\"Status\":" + "\"" + status + "\"" + "}", 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: UpdateKioskStatus", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        private DataTable CreateKioskConfigurationDataTable
            (
                DataTable dtkioskConfiguration,
                Model.Kiosk kiosk,
                KioskSettings kioskSettings,
                KioskArrival kioskArrival,
                Message message,
                BookingAppointment bookingAppointment,
                UserDemographicSetting userDemographicDetails
            )
        {
            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.Language),
                KioskId,
                "{" + "\"Languages\":" + JsonConvert.SerializeObject(kiosk.LanguageList) + "}");
            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.Modules),
                KioskId,
                kiosk.Module.ConvertToJsonString());

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.PatientMatchArrival),
                KioskId,
                kiosk.PatientMatch.ConvertToJsonString());

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.PatientMatchBooking),
                KioskId,
                kiosk.AppointmentMatch.ConvertToJsonString());
            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.Status),
                KioskId,
                "{" + "\"Status\":" + "\"" + kiosk.Status + "\"" + "}");

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.KioskArrival),
                KioskId,
                kioskArrival.ConvertToJsonString());

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.Organisation),
                KioskId,
                kiosk.OrganisationList.ConvertToJsonString());

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.KioskSettings),
                KioskId,
                kioskSettings.ConvertToJsonString());

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.Message),
                KioskId,
                message.ConvertToJsonString());
            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.Newsletter),
                KioskId,
                kiosk.ShowNewsletter.ConvertToJsonString());
            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.SlotTypes),
                KioskId,
                GetSlotTypes(kiosk).ConvertToJsonString());

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.BookingAppointment),
                KioskId,
                bookingAppointment.ConvertToJsonString());

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.UserDemographicDetails),
                KioskId,
                userDemographicDetails.ConvertToJsonString());

            KioskRegistrationGuid kioskRegistration = new KioskRegistrationGuid
            {
                KioskGuid = kiosk.KioskGuid,
                SyncServiceGuid = kiosk.SyncGuid
            };

            dtkioskConfiguration.Rows.Add(
                Convert.ToString(KioskConfigType.KioskDetails),
                KioskId,
                kioskRegistration.ConvertToJsonString());

            dtkioskConfiguration.Rows.Add(
               Convert.ToString(KioskConfigType.SyncDetails),
               kiosk.SyncGuid,
                kioskRegistration.ConvertToJsonString()
               );

            return dtkioskConfiguration;
        }

        private static List<AppointmentSlotType> GetSlotTypes(Model.Kiosk kiosk)
        {
            if (kiosk.OrganisationList.Any(a => a.SystemType == SystemType.EmisPcs.GetDisplayName()))
            {
                foreach (var slot in kiosk.SlotTypes)
                {
                    switch (slot.SlotTypeId)
                    {
                        case "6001":
                            slot.SlotTypeId = "D1";
                            break;
                        case "6002":
                            slot.SlotTypeId = "D2";
                            break;
                        case "6003":
                            slot.SlotTypeId = "D3";
                            break;
                        case "6004":
                            slot.SlotTypeId = "D4";
                            break;
                        case "6005":
                            slot.SlotTypeId = "D5";
                            break;
                        case "6006":
                            slot.SlotTypeId = "D6";
                            break;
                        default:
                            break;
                    }
                }
            }
            return kiosk.SlotTypes;
        }

        public void UpdateKioskConfig(Model.Kiosk kiosk, KioskSettings kioskSettings, KioskArrival kioskArrival, Message message, BookingAppointment bookingAppointment, UserDemographicSetting userDemographicDetails)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);
                    DataTable dtkioskConfiguration = new DataTable();
                    dtkioskConfiguration.Columns.Add("ConfigType", typeof(string));
                    dtkioskConfiguration.Columns.Add("KioskId", typeof(string));
                    dtkioskConfiguration.Columns.Add("Value", typeof(string));

                    dtkioskConfiguration = CreateKioskConfigurationDataTable(dtkioskConfiguration, kiosk, kioskSettings, kioskArrival, message, bookingAppointment, userDemographicDetails);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskInitialConfiguration, connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskConfiguration", dtkioskConfiguration, "PatientFlow.KioskConfiguration"));

                    spCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void SaveKioskLogo(byte[] kioskLogo)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    const string spSaveKioskLogo = "[PatientFlow].[SaveKioskLogo]";

                    DbManager.Open(connection);
                    SqlCommand spCommand = DbManager.GetSprocCommand(spSaveKioskLogo, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Logo", kioskLogo));
                    spCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void SaveKioskSiteMap(List<SiteMap> siteMapList)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    const string spSaveKioskLogo = "[PatientFlow].[SaveKioskSiteMap]";

                    DbManager.Open(connection);
                    SqlCommand spCommand = DbManager.GetSprocCommand(spSaveKioskLogo, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@SiteMapList", CreateKioskSiteMapList(siteMapList), "PatientFlow.KioskSiteMapList"));
                    spCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        private DataTable CreateKioskSiteMapList(List<SiteMap> kioskSiteMapList)
        {
            var dtlist = new DataTable("KioskSiteMapList");
            int i = 1;
            dtlist.Columns.Add("KioskSiteMapListId", typeof(int));
            dtlist.Columns.Add("SiteDescription", typeof(string));
            dtlist.Columns.Add("SiteMap", typeof(byte[]));


            foreach (SiteMap item in kioskSiteMapList)
            {
                DataRow dr = dtlist.NewRow();
                dr["KioskSiteMapListId"] = i;
                dr["SiteDescription"] = item.SiteMapName ?? "default";
                dr["SiteMap"] = item.SiteMapImage;
                dtlist.Rows.Add(dr);
                i++;
            }
            return dtlist;
        }

        public void SaveAdditionalInformation(List<LinkedMember> linkedMember, List<PatientMessage> patientMessages, List<Alerts> alertsList, List<Divert> divertList)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    List<Alerts> listalerts = alertsList.Where(alerts => alerts.LinkedKiosk.Where(kiosk => !string.IsNullOrEmpty(kiosk)).Any(kiosk => kiosk.Equals(KioskId, StringComparison.InvariantCultureIgnoreCase))).ToList();
                    listalerts.AddRange(alertsList.Where(alerts => alerts.LinkedKiosk.Any(string.IsNullOrEmpty)));

                    alertsList = listalerts;

                    DbManager.Open(connection);
                    DataTable dtkioskConfiguration = new DataTable();
                    dtkioskConfiguration.Columns.Add("ConfigType", typeof(string));
                    dtkioskConfiguration.Columns.Add("KioskId", typeof(string));
                    dtkioskConfiguration.Columns.Add("Value", typeof(string));

                    dtkioskConfiguration.Rows.Add(
                        Convert.ToString(KioskConfigType.LinkedMember),
                        KioskId,
                        JsonConvert.SerializeObject(linkedMember));
                    dtkioskConfiguration.Rows.Add(
                        Convert.ToString(KioskConfigType.PatientMessage),
                        KioskId,
                        JsonConvert.SerializeObject(patientMessages));
                    string alertstring = GetAlertString(alertsList);
                    dtkioskConfiguration.Rows.Add(
                        Convert.ToString(KioskConfigType.Alerts),
                        KioskId,
                        alertstring);
                    dtkioskConfiguration.Rows.Add(
                        Convert.ToString(KioskConfigType.Divert),
                        KioskId,
                        JsonConvert.SerializeObject(divertList));

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskInitialConfiguration, connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskConfiguration", dtkioskConfiguration, "PatientFlow.KioskConfiguration"));

                    spCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        private static string GetAlertString(List<Alerts> alertsList1)
        {
            List<Alerts> alertsList = alertsList1.ConvertToJsonString().ConvertFromJsonString<List<Alerts>>();

            string result = JsonConvert.SerializeObject(alertsList);
            while (result.Length > 4000)
            {
                int index = alertsList.Count;
                alertsList.RemoveAt(index - 1);
                result = JsonConvert.SerializeObject(alertsList);
            }
            return result;
        }

        public void SaveLinkedMembers(List<LinkedMember> linkedMember)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);
                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.LinkedMember), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", linkedMember.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void SavePatientMessage(PatientMessage patientMessage)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    List<PatientMessage> patientMessageList = GetKioskConfiguration<List<PatientMessage>>(KioskConfigType.PatientMessage.ToString());
                    if (patientMessageList != null)
                    {
                        PatientMessage selectedItem = patientMessageList.FirstOrDefault(item => item.PatientMessageId == patientMessage.PatientMessageId && item.OrganisationId == patientMessage.OrganisationId);

                        if (selectedItem == null)
                        {
                            patientMessageList.Add(patientMessage);
                        }
                        else
                        {
                            patientMessageList.Remove(selectedItem);
                            patientMessageList.Add(patientMessage);
                        }
                    }
                    else
                    {
                        patientMessageList = new List<PatientMessage>();
                        patientMessageList.Add(patientMessage);
                    }

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.PatientMessage), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", patientMessageList.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: SavePatientMessage", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void DeletePatientMessage(int patientMessageId)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    List<PatientMessage> patientMessageList = GetKioskConfiguration<List<PatientMessage>>(KioskConfigType.PatientMessage.ToString());

                    PatientMessage patientMessage = patientMessageList.FirstOrDefault(item => item.PatientMessageId == patientMessageId);

                    if (patientMessage != null)
                    {
                        patientMessageList.Remove(patientMessage);
                    }

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.PatientMessage), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", patientMessageList.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: DeletePatientMessage", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void UpdateQuestionnaire(List<int> questionnaireId)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    if (questionnaireId != null)
                    {
                        DataTable dtQuestionnaires = CreateListOrderDataTable(questionnaireId);
                        DbManager.Open(connection);
                        const string spKioskUpdateQuestionnaire = "[PatientFlow].[UpdateKioskQuestionnaire]";
                        SqlCommand spCommand = DbManager.GetSprocCommand(spKioskUpdateQuestionnaire, connection);

                        spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireIdList", dtQuestionnaires, "PatientFlow.ListWithOrder"));
                        spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                        spCommand.ExecuteReader();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: UpdateQuestionnaire", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void SetPublish(bool status,int questionnaireId)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                if (questionnaireId != 0)
                {
                    try
                    {
                        DbManager.Open(connection);
                        const string spSetPublish = "[PatientFlow].[SetPublish]";
                        SqlCommand spCommand = DbManager.GetSprocCommand(spSetPublish, connection);
                        spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaireId));
                        spCommand.Parameters.Add(DbManager.CreateParameter("@status", status));
                        spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                        spCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: SetPublish", ex, KioskId);
                    }
                    finally
                    {
                        DbManager.Close(connection);
                    }
                }
            }
        }

        private DataTable CreateListOrderDataTable(List<int> entitylist)
        {
            try
            {
                DataTable dtlist = new DataTable("ListWithOrder");
                int i = 1;
                dtlist.Columns.Add("Id", typeof(int));
                dtlist.Columns.Add("ListOrder", typeof(int));
                if (entitylist != null && entitylist.Count > 0)
                {
                    foreach (var item in entitylist)
                    {
                        DataRow dr = dtlist.NewRow();
                        dr["Id"] = item;
                        dr["ListOrder"] = i;
                        dtlist.Rows.Add(dr);
                        i++;
                    }
                }

                return dtlist;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
                return null;
            }
        }

        public void AddAlert(List<Alerts> alert)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);

                    List<Alerts> listalerts = alert.Where(alerts => alerts.LinkedKiosk.Where(kiosk => !string.IsNullOrEmpty(kiosk)).Any(kiosk => kiosk.Equals(KioskId, StringComparison.InvariantCultureIgnoreCase))).ToList();
                    listalerts.AddRange(alert.Where(alerts => alerts.LinkedKiosk.Any(string.IsNullOrEmpty)));

                    alert = listalerts;


                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.Alerts), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", alert.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: AddAlert", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void UpdateOrganisation(Organisation organisation)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    List<Organisation> orgList = GetKioskConfiguration<List<Organisation>>(KioskConfigType.Organisation.ToString());
                    Organisation org = orgList.First(item => item.OrganisationId == organisation.OrganisationId);
                    orgList.Remove(org);
                    orgList.Add(organisation);

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.Organisation), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", orgList.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: UpdateOrganisation", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void UpdateAlert(Alerts alert)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    bool isLinkedKioskPresent = alert.LinkedKiosk == null || (alert.LinkedKiosk != null && alert.LinkedKiosk.Count == 0);

                    bool alertForCurrentKiosk = isLinkedKioskPresent || alert.LinkedKiosk.Contains(KioskId, StringComparer.InvariantCultureIgnoreCase);

                    List<Alerts> alertList = GetKioskConfiguration<List<Alerts>>(KioskConfigType.Alerts.ToString());
                    var selectedItem = alertList != null ? alertList.FirstOrDefault(item => item.Id == alert.Id) : null;

                    if (selectedItem == null && !alertForCurrentKiosk) return;

                    if (selectedItem != null && !alertForCurrentKiosk)
                        alertList.Remove(selectedItem);

                    if (alertForCurrentKiosk && selectedItem != null)
                    {
                        alertList.Remove(selectedItem);
                        alertList.Add(alert);
                    }
                    else if (alertForCurrentKiosk && selectedItem == null)
                    {
                        if (alertList != null)
                            alertList.Add(alert);
                    }


                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.Alerts), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", alertList.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: AddAlert", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void DeleteAlert(int alertId)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    List<Alerts> alertList = GetKioskConfiguration<List<Alerts>>(KioskConfigType.Alerts.ToString());
                    Alerts alert = alertList.FirstOrDefault(item => item.Id == alertId);

                    if (alert != null)
                    {
                        alertList.Remove(alert);
                    }

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.Alerts), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", alertList.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: DeleteAlert", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void SetDivert(bool status, int sessionHolderId, int organisationId)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    List<Divert> divertList = GetKioskConfiguration<List<Divert>>(KioskConfigType.Divert.ToString());


                    if (status)
                    {
                        var divert = new Divert();
                        divert.SessionHolderId = sessionHolderId;
                        divert.OrganisationId = organisationId;
						divert.LoginId = "";
                        divertList.Add(divert);
                    }
                    else
                    {
                        Divert divert = divertList.FirstOrDefault(item => item.OrganisationId == organisationId && item.SessionHolderId == sessionHolderId);

                        divertList.Remove(divert);
                    }

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.Divert), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", divertList.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: SetDivert", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

		public void SetTppDivert(bool status, int sessionHolderId, string LoginId, int organisationId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					List<Divert> divertList = GetKioskConfiguration<List<Divert>>(KioskConfigType.Divert.ToString());


					if (status)
					{
						var divert = new Divert();
						divert.SessionHolderId = sessionHolderId;
						divert.OrganisationId = organisationId;
						divert.LoginId = LoginId;
						divertList.Add(divert);
					}
					else
					{
						Divert divert = divertList.FirstOrDefault(item => item.OrganisationId == organisationId && item.LoginId == LoginId);

						divertList.Remove(divert);
					}

					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.Divert), 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Value", divertList.ConvertToJsonString(), 4000));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
					spCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: SetDivert", ex, KioskId);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public void DeleteKiosk()
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteKiosk]", connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: DeleteKiosk", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public void DeleteKioskLogo()
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteKioskLogo]", connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: DeleteKioskLogo", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }


        public void UpdateMessage(string message)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    Message kioskmessage = GetKioskConfiguration<Message>(KioskConfigType.Message.ToString());
                    kioskmessage.GeneralMessage = message;

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand(SpKioskConfiguration, connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", Convert.ToString(KioskConfigType.Message), 100));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Value", kioskmessage.ConvertToJsonString(), 4000));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Username", KioskId, 50));
                    spCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, "PushConfiguration: UpdateMessage", ex, KioskId);
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }

        }
    }
}
