using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
	public interface IOrganisationRepository
	{
		IEnumerable<Organisation> GetOrganisationList(List<int> organisationIds);
		IEnumerable<Organisation> GetOrganisationListForKiosk(int kioskId);
		IEnumerable<Organisation> GetOrganisationListForAlert(int alertId);
		IEnumerable<Organisation> GetOrganisations();
		IEnumerable<Organisation> GetOrganisations(string userName);
		IEnumerable<Organisation> GetOrganisationsForDropDown();
		IEnumerable<Organisation> GetOrganisationsForDropDown(string userName);
		Organisation GetOrganisationDetails(int organisationId);
		Organisation GetOrganisationDetail(int organisationId);
		IEnumerable<Department> GetDepartmentListFororganisation(int organisationId);
		List<KeyValuePair<int, string>> GetOrganisationAccessRights(string userName);
		int AddOrganisation(Organisation organisation);
		int DeleteOrganisation(int organisationId);
		int UpdateOrganisation(Organisation organisation);
		WebUser GetPatientFlowUser(int organisationId);
		void ValidateOrganisationName(string organisationName, int organisationId, out bool status);
		void ValidateOrganisationKey(string organisationKey, int organisationId, out bool status);
		void ValidateOrganisationSiteNumber(string organisationSiteNumber, int organisationId, out bool status);
		void SaveAccessMapping(string userName, List<int> accesses);
		List<Entity> GetSystemTypeList();
		List<string> GetIPAddresses();
		List<AuthUser> GetAuthUsers();
		List<AuthUser> GetAuthUsersForPracticeAdmin();
        bool DeleteOrganisationAuthUsers(string userName);
    }
}
