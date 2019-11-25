using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IMemberRepository
    {
        Task<int> UpdateMember(Member member);
        Task<int> DeleteMember(int nodeId, int OrganisationId);
        Task<Member> GetMemberDetails(int nodeId);
        Task<List<Member>> GetMemberList();
        Task<bool> ValidateSessionHolderId(int sessionHolderId);
        Task<int> AddSyncedMember(List<Member> member);
        Task<List<int>> GetSessionHolderIdOrganisation(int organisationId);
        Task<bool> SetDivert(bool status, int sessionHolderId, int organisationId);
		Task<IEnumerable<Member>> GetMembers();
		Task<IEnumerable<Member>> GetMembersByOrganisationId(int OrganisationId);
		Task<bool> EnableMessageForMember(int alertId, int memberId);
	}
}
