using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.SyncService.Filters;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces
{
    interface IConfigurationRepository
    {
        List<Log> GetLogs();
        List<Survey> GetAnonymousSurveys();
        List<string> GetWebClientConfiguration(string systemType);
		void SaveAppointments(List<Appointment> appointments, AppointmentFilter filter, string modifiedBy, SystemType systemType, Boolean status, Boolean isUntimed);
		void SaveTPPAppointments(List<Appointment> appointments, AppointmentFilter filter, string modifiedBy, SystemType systemType, Boolean status, Boolean isUntimed);
		void SaveSyncLog(SyncType type, long lastItemId);
        void SaveWebClientConfiguration(ApiUser user);
		void UpdateClientConfiguration(List<int> OrgsList);
		DateTime GetModifiedDate(int syncType);
        void SaveTranslation(List<Translation> translation, DateTime lastRowModifiedDate);
        void SaveScreenControl(List<ScreenControl> screenControl, DateTime lastRowModifiedDate);
	    void SaveProductKey(string productKey);
		List<Organisation> GetKioskOrganisation();
		void SyncMembers(List<Member> members, int organisationId, SystemType systemType, string modifiedBy);
	}
}
