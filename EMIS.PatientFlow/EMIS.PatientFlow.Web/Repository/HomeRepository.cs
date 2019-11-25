using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class HomeRepository : BaseRepository, IHomeRepository
    {
        public async Task<List<Language>> GetLanguageList()
        {
            return await GetAsync<List<Language>>("api/Language/GetLanguageList");
        }

        public async Task<List<Module>> GetModulesList()
        {
            return await GetAsync<List<Module>>("api/Kiosk/GetModulesList");
        }

		public async Task<List<DemographicDetails>> GetDemographicDetails()
        {
			return await GetAsync<List<DemographicDetails>>("api/Kiosk/GetDemographicDetails");
        }

        public async Task<List<PatientMatch>> GetPatientMatchList()
        {
            return await GetAsync<List<PatientMatch>>("api/Kiosk/GetPatientMatchList");
        }

        public async Task<List<PatientMatch>> GetAppointmentMatchList()
        {
            return await GetAsync<List<PatientMatch>>("api/Kiosk/GetAppointmentMatchList");
        }

		public async Task<KioskMasterDetails> GetKioskMasterDetails()
		{
			return await GetAsync<KioskMasterDetails>("api/Kiosk/GetKioskMasterDetails");
		}
	}
}