using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class OrganisationRepository : BaseRepository, IOrganisationRepository
    {
		public async Task<List<Organisation>> GetOrganisations()
		{
			return await GetAsync<List<Organisation>>(
				   string.Format("api/Organisation/GetOrganisations"));
		}

		public async Task<List<Organisation>> GetOrganisationsForDropDown()
		{
			return await GetAsync<List<Organisation>>("api/Organisation/GetOrganisationsForDropDown");
		}

		public async Task<Organisation> GetOrganisationDetails(int organisationId)
		{
			return await GetAsync<Organisation>("api/Organisation/GetOrganisationDetails?OrganisationId=" + organisationId);
		}

		public async Task<int> AddOrganisation(Organisation organisation)
		{
			var jsonorganisation = new JavaScriptSerializer().Serialize(organisation);
			return await PostAsJsonAsync<int>("api/Organisation/AddOrganisation", jsonorganisation);
		}

		public async Task<int> AddWebUser(WebUser webUser)
		{
			var jsonWebUser = new JavaScriptSerializer().Serialize(webUser);
			return await PostAsJsonAsync<int>("api/Organisation/AddWebUser", jsonWebUser);
		}

		public async Task<int> UpdateOrganisation(Organisation organisation, string groupName)
		{
			var jsonorganisation = new JavaScriptSerializer().Serialize(organisation);
			return await PostAsJsonAsync<int>("api/Organisation/UpdateOrganisation", jsonorganisation);
		}

		public async Task<int> DeleteOrganisation(int organisationId)
		{
			return await GetAsync<int>("api/Organisation/DeleteOrganisation?organisationId=" + organisationId);
		}

        public async Task<WebUser> GetPatientFlowUser(int organisationId)
        {
            return await GetAsync<WebUser>("api/Organisation/GetPatientFlowUser?organisationId=" + organisationId);
        }

		public async Task<bool> ValidateOrganisationName(string organisationName, int organisationId)
		{
			return
				await
					GetAsync<bool>(
						"api/Organisation/ValidateOrganisationName?organisationName=" + organisationName
						+ "&organisationId=" + organisationId);
		}

        public async Task<bool> ValidateOrganisationKey(string organisationKey, int organisationId)
        {
            return
                await
                    GetAsync<bool>(
                        "api/Organisation/ValidateOrganisationKey?organisationKey=" + organisationKey
                        + "&organisationId=" + organisationId);
        }

		public async Task<bool> ValidateOrganisationSiteNumber(string organisationSiteNumber, int organisationId)
		{
			return
				await
					GetAsync<bool>(
						"api/Organisation/ValidateOrganisationSiteNumber?organisationSiteNumber=" + organisationSiteNumber
						+ "&organisationId=" + organisationId);
		}

		public async Task<List<Entity>> GetSystemTypeList()
        {
            return await GetAsync<List<Entity>>("api/Organisation/GetSystemTypeList");
        }


		public async Task<List<Organisation>> GetOrganisationList(List<int> organisationIds)
		{
			return await PostAsJsonAsync<List<Organisation>>("api/Organisation/GetOrganisationListByIds", organisationIds);
		}

		public async Task<List<Organisation>> GetOrganisationListForKiosk(int kioskId)
		{
			return await GetAsync<List<Organisation>>("api/Organisation/GetOrganisationListForKiosk?kioskId=" + kioskId);
		}

		public async Task<List<Organisation>> GetOrganisationListForAlert(int alertId)
		{
			return await GetAsync<List<Organisation>>("api/Organisation/GetOrganisationListForAlert?alertId=" + alertId);
		}

		public async Task<List<string>> GetIPAddresses()
        {
            return await GetAsync<List<string>>("api/Organisation/GetIPAddresses");
        }
	}
}