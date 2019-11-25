using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface ISyncServiceRepository
    {
        Task<bool> DeleteSyncService(int serviceId);
        Task<bool> UpdateSyncServiceStatus(int serviceId, bool isActive);
        Task<bool> SaveSyncService(SyncService service);
        Task<bool> SaveSyncServiceOrganisations(SyncService service);
        Task<SyncService> GetSyncServiceById(int serviceId);
        Task<dynamic> GetSyncServices(string organisation, int pageNo, int pageSize);
    }
}
