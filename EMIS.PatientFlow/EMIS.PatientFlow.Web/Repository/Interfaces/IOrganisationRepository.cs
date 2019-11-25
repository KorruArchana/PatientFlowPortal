using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IOrganisationRepository
    {
		Task<List<Organisation>> GetOrganisations();
		Task<List<Organisation>> GetOrganisationsForDropDown();
		Task<Organisation> GetOrganisationDetails(int organisationId);
        Task<int> AddOrganisation(Organisation organisation);
        Task<int> AddWebUser(WebUser webUser);
        Task<int> UpdateOrganisation(Organisation organisation, string groupName);
        Task<int> DeleteOrganisation(int organisationId);
        Task<WebUser> GetPatientFlowUser(int organisationId);
        Task<bool> ValidateOrganisationName(string organisationName, int organisationId);
        Task<bool> ValidateOrganisationKey(string organisationKey, int organisationId);
        Task<bool> ValidateOrganisationSiteNumber(string organisationSiteNumber, int organisationId);
		Task<List<Entity>> GetSystemTypeList();

		Task<List<Organisation>> GetOrganisationList(List<int> organisationIds);
		Task<List<Organisation>> GetOrganisationListForKiosk(int kioskId);
		Task<List<Organisation>> GetOrganisationListForAlert(int alertId);
		Task<List<string>> GetIPAddresses();
    }
}
