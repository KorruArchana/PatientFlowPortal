using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface IMemberRepository
    {
        int AddMember(Member member);
        void AddSyncedMember(List<Member> member);
        int DeleteMember(int memberId);
        Member GetMemberDetails(int memberId);
        int UpdateMember(Member member);
        bool ValidateSessionHolderId(int sessionHolderId, out bool result);
        List<int> GetSessionHolderIdOrganisation(int organisationId);
        Member SetDivert(bool status, int sessionHolderId,int organisationId);
		IEnumerable<Member> GetMembers();
		IEnumerable<Member> GetMembersByUser();
		IEnumerable<Member> GetMembersByOrganisationId(int OrganisationId);
		bool EnableMessageForMember(int alertId, int memberId, out bool status);
	}
}
