using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class AlertRepository : BaseRepository, IAlertRepository
    {
		public async Task<List<Alert>> GetAlerts()
		{
			return
					await
						GetAsync<List<Alert>>(
							string.Format(
								"api/Alerts/GetAlerts"));
		}

		public async Task<Alert> AlertDetails(int alertId)
        {
            return await GetAsync<Alert>("api/Alerts/GetAlertDetails?alertId=" + alertId);
        }

        public async Task<int> UpdateAlert(Alert alert)
        {
            var jsonAlert = alert.ConvertToJsonString();
            return await PostAsJsonAsync<int>("api/Alerts/UpdateAlert", jsonAlert);
        }

        public async Task<int> DeleteAlert(int alertId)
        {
            return await GetAsync<int>("api/Alerts/DeleteAlert?alertId=" + alertId);
        }

        public async Task<int> AddAlert(Alert alert)
        {
            var jsonAlert = alert.ConvertToJsonString();
            return await PostAsJsonAsync<int>("api/Alerts/AddAlert", jsonAlert);
        }

		public async Task<IEnumerable<Alert>> GetAlertsByMember(int MemberId)
		{
			return await GetAsync<List<Alert>>("api/Alerts/GetAlertsByMember?MemberId=" + MemberId);
		}
  }
}