using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IHomeRepository
    {
        Task<List<Language>> GetLanguageList();
        Task<List<Module>> GetModulesList();
	    Task<List<DemographicDetails>> GetDemographicDetails();
        Task<List<PatientMatch>> GetPatientMatchList();
        Task<List<PatientMatch>> GetAppointmentMatchList();
        Task<KioskMasterDetails> GetKioskMasterDetails();
	}
}
