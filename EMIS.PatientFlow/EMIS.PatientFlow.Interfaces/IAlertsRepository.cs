using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface IAlertsRepository
    {
		IEnumerable<Alert> GetAlerts();
		IEnumerable<Alert> GetAlertsByUser();
        int AddAlert(Alert alert);
        int UpdateAlert(Alert alert);
        Alert GetAlertDetails(int alertId);
        List<int> GetSessionHolderIdForAlert(int alertId);
        List<int> GetOrganisationIdListForAlert(int alertId);
        List<Alert> GetAlertsListForOrganisation(List<int> organisationIds);
        int DeleteAlert(int alertId);
        List<string> GetActiveGroupsForAlert(int alertId);
		IEnumerable<Alert> GetAlertsByMember(int MemberId);
		IEnumerable<Alert> GetAllAlertsByMemberOrganisation(int MemberId);

    }
}
