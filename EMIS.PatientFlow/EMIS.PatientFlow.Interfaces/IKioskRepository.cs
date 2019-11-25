using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface IKioskRepository
    {
        List<KioskDetails> GetKioskList();
        IEnumerable<Kiosk> GetKiosks();
        IEnumerable<Kiosk> GetKiosksByUser();
		List<Kiosk> GetKioskListForOrganisation(int organisationId);
		IEnumerable<Kiosk> GetKioskDetailListForOrganisation(int organisationId);
		IEnumerable<Kiosk> GetKioskForOrganisation(int organisationId,string kioskTitle);
        int AddKioskDetails(Kiosk kiosk);
        Kiosk GetKioskDetails(int kioksId);
		Kiosk GetKioskDetailsForSync(int kioskId);
		int EditKiosk(Kiosk kiosk);
        int GetKioskStatus(int kioskId);
        int GetKioskConnectionStatus(int kioskId);
        int UpdateKioskConnection(string kioskAddress, string connectionId);
        void UpdateKioskStatus(int kioskId, int status);
        void DisconnectKiosk(string connectionId);
        List<PatientMatch> GetPatientMatchList();
        List<PatientMatch> GetAppointmentMatchList();
        List<Module> GetModulesList();
	    List<DemographicDetails> GetDemographicDetails();
	    KioskMasterDetails GetKioskMasterDetails();
	    KioskMasterDetails GetKioskMasterDetailsByUser();
		List<Module> GetModulesListForUser();
        List<AppointmentSlotType> GetKioskSlotTypes(int kioskId);
        string GetConnectionId(int kioskId);
        string GetConnectionIdFromKey(string key);
        int GetKioskIdFromKey(string key);
        List<Member> GetLinkedMembersForKiosk(string registrationKey);
        AdditionalInformation GetAdditionalInformation(string registrationKey);
        List<Patient> GetPatientsForKiosk(string registrationKey);
        void SaveAppointmentSlotType(List<AppointmentSlotType> appointmentSlotType);
        List<AppointmentSlotType> GetAppointmentSlotTypes(int organisationId);
        bool ValidateKioskName(string kioskName, int kioskId, List<int> organisationList, out bool status);
        bool DeleteKiosk(int kioskId);
	    void SaveKioskDetails(string registrationKey, string pcName, string ipAddress);
	    void SaveKioskDetails(string registrationKey, KioskSystemInformation systemInformation);
		List<KioskUsageLog> GetKioskUsageLog(List<Guid> KioskGuids);
        KioskSyncKeys GetKioskSyncServiceKeys(int kioskId);
    }
}
