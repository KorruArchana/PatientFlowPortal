using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;
using System.Linq;
using System.Text;

namespace EMIS.PatientFlow.Repositories
{
    public class KioskRepository : BaseRepository, IKioskRepository
    {
		private static readonly ReadOnlyCollection<Kiosk> _kioskList = new List<Kiosk>().AsReadOnly();

		public List<KioskDetails> GetKioskList()
        {
            try
            {
                return DbAccess.GetKioskList();
            }

            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<KioskDetails>();
            }
        }

        public List<Kiosk> GetKioskListForOrganisation(int organisationId)
        {
            try
            {
                return DbAccess.GetKioskListForOrganisation(organisationId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<Kiosk>();

            }
        }

        public IEnumerable<Kiosk> GetKioskDetailListForOrganisation(int organisationId)
        {
            try
            {
                return DbAccess.GetKioskDetailListForOrganisation(organisationId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Kiosk>();
			}
        }

        public IEnumerable<Kiosk> GetKioskForOrganisation(int organisationId, string kioskTitle)
        {
            try
            {
                return DbAccess.GetKioskDetailListForOrganisation(organisationId, kioskTitle);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Kiosk>();
			}
        }

        public IEnumerable<Kiosk> GetKiosks()
        {
            try
            {
                return DbAccess.GetKiosks();
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<Kiosk>();
            }
        }

		public IEnumerable<Kiosk> GetKiosksByUser()
		{
			try
			{
				return DbAccess.GetKiosksByUser(CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Kiosk>();
			}
		}

		public Kiosk GetKioskDetails(int kioskId)
        {
            try
            {
                Kiosk kiosk = DbAccess.GetKioskDetails(kioskId);				

				kiosk.SelectedModules = kiosk.Module.Select(m => m.ModuleId).ToList();
				kiosk.SelectedOrganisationList = new List<int>();
				kiosk.SelectedLanguageList = new List<int>();
							
				if (kiosk.OrganisationList.Any())
				{                 					
					kiosk.SelectedOrganisationList.AddRange(kiosk.OrganisationList.Select(a => a.Id));
					kiosk.Organisations = string.Join(",", kiosk.OrganisationList.Select(a => a.OrganisationName));
				}

				if (kiosk.LanguageList.Any())
				{					
					kiosk.SelectedLanguageList.AddRange(kiosk.LanguageList.Select(a => a.Id));
					kiosk.LanguageIdList = string.Join(",", kiosk.LanguageList.Select(a => a.Id));
				}
				
				kiosk.SlotTypes = DbAccess.GetKioskSlotTypes(kioskId);
				return kiosk;
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new Kiosk();
            }
        }

		public Kiosk GetKioskDetailsForSync(int kioskId)
		{
			try
			{

				//StringBuilder orgNames = new StringBuilder();
				//if (kiosk.OrganisationList != null)
				//{
				//	kiosk.SelectedOrganisationList = new List<int>();
				//	foreach (var item in kiosk.OrganisationList)
				//	{
				//		orgNames.Append(item.OrganisationName);
				//		orgNames.Append(',');
				//		kiosk.SelectedOrganisationList.Add(item.Id);
				//	}
				//}

				//StringBuilder languageIds = new StringBuilder();
				//if (kiosk.LanguageList != null)
				//{
				//	kiosk.SelectedLanguageList = new List<int>();
				//	foreach (var item in kiosk.LanguageList)
				//	{
				//		languageIds.Append(item.Id);
				//		languageIds.Append(",");
				//		kiosk.SelectedLanguageList.Add(item.Id);
				//	}
				//	}

				//	kiosk.SelectedModules = kiosk.Module.Select(m => m.ModuleId).ToList();
				//	kiosk.LanguageIdList = languageIds.Length != 0 ? languageIds.ToString().TrimEnd(',') : null;
				//	kiosk.Organisations = orgNames.Length != 0 ? orgNames.ToString().TrimEnd(',') : null;

				

				Kiosk kiosk = DbAccess.GetKioskDetails(kioskId);
                if (kiosk != null)
                {
                    kiosk.SlotTypes = DbAccess.GetKioskSlotTypes(kioskId);
                    if (kiosk.QuestionnaireList != null)
                    {
                        var ids = kiosk.QuestionnaireList.Select(item => item.Id).ToList();
                        kiosk.SelectedQuestionnaires = ids;
                        List<Questionnaire> QuestionnaireList = new List<Questionnaire>();
                        foreach (int id in ids)
                        {
                            QuestionnaireList.Add(DbAccess.GetQuestionnaireDetails(id));
                        }

                        if (kiosk.QuestionnaireList.Count == QuestionnaireList.Count)
                        {
                            kiosk.QuestionnaireList = QuestionnaireList;
                        }
                    }
                    return kiosk;
                }
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");	
			}
            return new Kiosk();
        }

		public int EditKiosk(Kiosk kiosk)
        {
            try
            {
                return DbAccess.KioskEdit(kiosk, CurrentUser);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public int GetKioskStatus(int kioskId)
        {
            try
            {
                return DbAccess.GetKioskStatus(kioskId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public int GetKioskConnectionStatus(int kioskId)
        {
            try
            {
                return DbAccess.GetKioskConnectionStatus(kioskId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public int UpdateKioskConnection(string kioskAddress, string connectionId)
        {
            try
            {
                return DbAccess.UpdateKioskConnection(kioskAddress, connectionId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public void DisconnectKiosk(string connectionId)
        {
            try
            {
                DbAccess.DisconnectKiosk(connectionId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
            }
        }

        public void UpdateKioskStatus(int kioskId, int status)
        {
            try
            {
                DbAccess.UpdateKioskStatus(kioskId, status);
            }

            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
            }
        }

        public int AddKioskDetails(Kiosk kiosk)
        {
            try
            {
                return DbAccess.AddKioskDetails(kiosk, CurrentUser);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public List<Module> GetModulesList()
        {
            try
            {
                return DbAccess.GetModulesList();
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<Module>();
            }
        }

        public List<DemographicDetails> GetDemographicDetails()
        {
            try
            {
                return DbAccess.GetDemographicDetailsList();
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<DemographicDetails>();
            }
        }

		public KioskMasterDetails GetKioskMasterDetails()
		{
			try
			{
				return DbAccess.GetKioskMasterDetails();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new KioskMasterDetails();
			}
		}

		public KioskMasterDetails GetKioskMasterDetailsByUser()
		{
			try
			{
				return DbAccess.GetKioskMasterDetailsByUser();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new KioskMasterDetails();
			}
		}

		public List<Module> GetModulesListForUser()
        {
            try
            {
                return DbAccess.GetModulesListForUser();
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<Module>();
            }
        }

        public List<PatientMatch> GetPatientMatchList()
        {
            try
            {
                return DbAccess.GetPatientMatchList();
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<PatientMatch>();
            }
        }

        public List<PatientMatch> GetAppointmentMatchList()
        {
            try
            {
                return DbAccess.GetAppointmentMatchList();
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<PatientMatch>();
            }
        }

        public List<AppointmentSlotType> GetKioskSlotTypes(int kioskId)
        {
            try
            {
                return DbAccess.GetKioskSlotTypes(kioskId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<AppointmentSlotType>();
            }

        }
        public string GetConnectionId(int kioskId)
        {
            try
            {
                return DbAccess.GetConnectionId(kioskId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return String.Empty;
            }
        }

        public string GetConnectionIdFromKey(string key)
        {
            try
            {
                return DbAccess.GetConnectionIdFromKey(key);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return String.Empty;
            }
        }

        public int GetKioskIdFromKey(string key)
        {
            try
            {
                return DbAccess.GetKioskIdFromKey(key);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public AdditionalInformation GetAdditionalInformation(string registrationKey)
        {
            try
            {
                return DbAccess.GetAdditionalInformation(registrationKey);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new AdditionalInformation();
            }
        }

        public List<Member> GetLinkedMembersForKiosk(string registrationKey)
        {
            try
            {
                return DbAccess.GetLinkedMembersForKiosk(registrationKey);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<Member>();
            }
        }

        public List<Patient> GetPatientsForKiosk(string registrationKey)
        {
            try
            {
                return DbAccess.GetPatientsForKiosk(registrationKey);
            }

            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<Patient>();
            }
        }

        public void SaveAppointmentSlotType(List<AppointmentSlotType> appointmentSlotType)
        {
            try
            {
                DbAccess.SaveAppointmentSlotType(appointmentSlotType);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
            }
        }

        public List<AppointmentSlotType> GetAppointmentSlotTypes(int organisationId)
        {
            try
            {
                return DbAccess.GetAppointmentSlotTypes(organisationId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<AppointmentSlotType>();
            }
        }

        public bool ValidateKioskName(string kioskName, int kioskId, List<int> organisationList, out bool status)
        {
            try
            {
                return DbAccess.ValidateKioskName(kioskName, kioskId, organisationList, out status);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                status = false;
                return false;
            }
        }

        public bool DeleteKiosk(int kioskId)
        {
            try
            {
                DbAccess.DeleteKiosk(kioskId);
				return true;
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return false;
            }
        }

        public void SaveKioskDetails(string registrationKey, string pcName, string ipAddress)
        {
            try
            {
                DbAccess.SaveKioskDetails(registrationKey, pcName, ipAddress);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
            }
        }

        public void SaveKioskDetails(string registrationKey, KioskSystemInformation systemInformation)
        {
            try
            {
                DbAccess.SaveKioskDetails(registrationKey, systemInformation);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
            }
        }

        public List<WebUser> GetPatientFlowUserByKioskKey(Guid Kioskkey)
        {
            try
            {
                return DbAccess.GetPatientFlowUserByKioskKey(Kioskkey);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<WebUser>();
            }
        }

		public List<KioskUsageLog> GetKioskUsageLog(List<Guid> KioskGuids)
		{
			try
			{
				return DbAccess1.GetKioskUsageLog(KioskGuids);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<KioskUsageLog>();
			}
		}

        public KioskSyncKeys GetKioskSyncServiceKeys(int kioskId)
        {
			try
			{
				return DbAccess.GetKioskSyncKeys(kioskId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new KioskSyncKeys();
			}
        }
    }
}
