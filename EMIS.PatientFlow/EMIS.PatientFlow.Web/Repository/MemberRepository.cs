using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class MemberRepository : BaseRepository, IMemberRepository
    {
        public async Task<int> UpdateMember(Member member)
        {
            var jsonMember = member.ConvertToJsonString();
            return await PostAsJsonAsync<int>("api/Member/UpdateMember", jsonMember);
        }

        public async Task<int> DeleteMember(int nodeId,int OrganisationId)
        {
            return await GetAsync<int>("api/Member/DeleteMember?memberId=" + nodeId+ "&OrganisationId="+OrganisationId);
        }

        public async Task<Member> GetMemberDetails(int nodeId)
        {
            return await GetAsync<Member>("api/Member/GetMemberDetails?memberId=" + nodeId);
        }

        public async Task<List<Member>> GetMemberList()
        {
            return await GetAsync<List<Member>>("api/Member/GetMemberList");
        }

        public async Task<bool> ValidateSessionHolderId(int sessionHolderId)
        {
            return await GetAsync<bool>("api/Member/ValidateSessionHolderId?sessionHolderId=" + sessionHolderId);
        }

        public async Task<int> AddSyncedMember(List<Member> member)
        {
            var jsonMember = member.ConvertToJsonString();
            return await PostAsJsonAsync<int>("api/Member/AddSyncedMember", jsonMember);
        }

        public async Task<List<int>> GetSessionHolderIdOrganisation(int organisationId)
        {
            return await GetAsync<List<int>>("api/Member/GetSessionHolderIdOrganisation?organisationId=" + organisationId);
        }

        public async Task<bool> SetDivert(bool status, int sessionHolderId, int organisationId)
        {
            return await GetAsync<bool>("api/Member/SetDivert?status=" + status + "&sessionHolderId=" + sessionHolderId + "&organisationId=" +organisationId);
        }
		public async Task<IEnumerable<Member>> GetMembers()
		{
			return await GetAsync<IEnumerable<Member>>(string.Format("api/Member/GetMembers"));
		}
		public async Task<IEnumerable<Member>> GetMembersByOrganisationId(int OrganisationId)
		{
			return await GetAsync<IEnumerable<Member>>(string.Format("api/Member/GetMemberByOrganisationId?OrganisationId="+OrganisationId));
		}

		public async Task<bool> EnableMessageForMember(int alertId, int memberId)
		{
			return await GetAsync<bool>("api/Member/EnableMessageForMember?alertId=" + alertId + "&memberId=" + memberId);
		}
	}
}