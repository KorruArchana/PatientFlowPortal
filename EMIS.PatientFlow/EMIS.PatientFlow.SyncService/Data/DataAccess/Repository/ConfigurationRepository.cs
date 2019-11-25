using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces;
using EMIS.PatientFlow.SyncService.Filters;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess.Repository
{
    public class ConfigurationRepository : BaseRepository, IConfigurationRepository
    {
        public List<Log> GetLogs()
        {
            return DbAccess.GetLogs();
        }

        public List<Survey> GetAnonymousSurveys()
        {
            return DbAccess.GetAnonymousSurveys();
        }

        public List<string> GetWebClientConfiguration(string systemType)
        {
            return DbAccess.GetWebClientConfiguration(systemType);
        }

		public void SaveAppointments(List<Appointment> appointments, AppointmentFilter filter, string modifiedBy, SystemType systemType, Boolean status, Boolean isUntimed)
        {
			DbAccess.SaveAppointments(appointments, filter, modifiedBy, systemType, status, isUntimed);
        }

		public void SaveTPPAppointments(List<Appointment> appointments, AppointmentFilter filter, string modifiedBy, SystemType systemType, Boolean status, Boolean isUntimed)
		{
			DbAccess.SaveTPPAppointments(appointments, filter, modifiedBy, systemType, status, isUntimed);
		}

		public void SaveSyncLog(SyncType type, long lastItemId)
        {
            DbAccess.SaveSyncLog(type, lastItemId);
        }

        public void SaveWebClientConfiguration(ApiUser user)
        {
            DbAccess.SaveWebClientConfiguration(user);
        }

		public void UpdateClientConfiguration(List<int> OrgIdList)
		{
			DbAccess.UpdateClientConfiguration(OrgIdList);
		}

		public DateTime GetModifiedDate(int syncType)
        {
            return DbAccess.GetModifiedDate(syncType);
        }

        public void SaveTranslation(List<Translation> translation, DateTime lastRowModifiedDate)
        {
            DbAccess.SaveTranslation(translation, lastRowModifiedDate);
        }

        public void SaveScreenControl(List<ScreenControl> screenControl, DateTime lastRowModifiedDate)
        {
            DbAccess.SaveScreenControl(screenControl, lastRowModifiedDate);
        }

	    public void SaveProductKey(string productKey)
	    {
		    DbAccess.SaveProductKey(productKey);
	    }

		public List<Organisation> GetKioskOrganisation()
	    {
			return DbAccess.GetKioskOrganisation();
	    }

		public void SyncMembers(List<Member> members, int organisationId, SystemType systemType, string modifiedBy)
		{
			DbAccess.SyncMembers(members, organisationId, systemType, modifiedBy);
		}
	}
}
