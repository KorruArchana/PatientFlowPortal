using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IAlertRepository
    {
		Task<List<Alert>> GetAlerts();
        Task<Alert> AlertDetails(int alertId);
        Task<int> UpdateAlert(Alert alert);
        Task<int> DeleteAlert(int alertId);
        Task<int> AddAlert(Alert alert);
		Task<IEnumerable<Alert>> GetAlertsByMember(int MemberId);
    }
}
