using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IKioskRepository
    {
        Task<int> SaveAppointmentSlotType(int organisationId, List<AppointmentSlotType> appointmentSlotTypes);
        Task<List<AppointmentSlotType>> GetAppointmentSlotTypes(int organisationId);
        Task<Kiosk> GetKioskDetails(int kioskId);
		Task<List<Kiosk>> GetKiosksWithUsageLog();
		Task<string> UpdateKioskStatus(int kioskId, string connectionId, int status = 1);
        Task<int> AddKiosk(Kiosk kiosk);
        Task<int> EditKiosk(Kiosk kiosk);
        Task<bool> DeleteKiosk(int kioskId);
		Task<List<Kiosk>> GetKioskDetailListForOrganisation(int organisationId);
        Task<KioskSyncKeys> GetKioskSyncKeys(int kioskId);

		Task<List<Kiosk>> GetKioskList();
		Task<List<Kiosk>> GetKioskListForOrganisation(int organisationId);
		Task<List<Kiosk>> GetKiosks();
		Task<bool> ValidateKioskName(string kioskName, int kioskId, List<int> organisationList);
	}
}