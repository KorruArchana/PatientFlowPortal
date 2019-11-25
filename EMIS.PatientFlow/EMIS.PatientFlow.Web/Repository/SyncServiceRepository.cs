using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class SyncServiceRepository : BaseRepository, ISyncServiceRepository
    {
        public async Task<bool> SaveSyncService(SyncService service)
        {
            await PostAsJsonAsync<bool>("api/SyncService/SaveSyncService", service);

            return true;
        }

        public async Task<bool> SaveSyncServiceOrganisations(SyncService service)
        {
            await PostAsJsonAsync<bool>("api/SyncService/SaveSyncServiceOrganisations", service);

            return true;
        }

        public async Task<bool> DeleteSyncService(int serviceId)
        {
            await DeleteAsync<bool>("api/SyncService/DeleteSyncService?serviceId=" + serviceId);

            return true;
        }

        public async Task<dynamic> GetSyncServices(string organisation, int pageNo, int pageSize)
        {
            return await GetAsync<dynamic>(
                string.Format(
                    "api/SyncService/GetSyncServices?organisation={0}&pageNo={1}&pageSize={2}",
                    organisation,
                    pageNo,
                pageSize));
        }

        public async Task<SyncService> GetSyncServiceById(int serviceId)
        {
            return await GetAsync<SyncService>("api/SyncService/GetSyncService?serviceId=" + serviceId.ToString());
        }

        public async Task<bool> UpdateSyncServiceStatus(int serviceId, bool status)
        {
            await PostAsJsonAsync<bool>("api/SyncService/UpdateSyncServiceStatus", new { serviceId, status });

            return true;
        }
    }
}