using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class MemberRepository : BaseRepository, IMemberRepository
    {
        public int AddMember(Member member)
        {
            try
            {
                return DbAccess.AddMember(member, CurrentUser);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public void AddSyncedMember(List<Member> member)
        {
            try
            {
                DbAccess.AddSyncedMember(member, CurrentUser);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
            }
        }

        public int DeleteMember(int memberId)
        {
            try
            {
                return DbAccess.DeleteMember(memberId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public Member GetMemberDetails(int memberId)
        {
            try
            {
                return DbAccess.GetMemberDetails(memberId);
            }

            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new Member();
            }
        }

        public int UpdateMember(Member member)
        {
            try
            {
                return DbAccess.UpdateMember(member, CurrentUser);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return -1;
            }
        }

        public bool ValidateSessionHolderId(int sessionHolderId, out bool result)
        {
            try
            {
                return DbAccess.ValidateSessionHolderId(sessionHolderId, out result);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                result = false;
                return result;
            }
        }

        public List<int> GetSessionHolderIdOrganisation(int organisationId)
        {
            try
            {
                return DbAccess.GetSessionHolderIdOrganisation(organisationId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new List<int>();
            }
        }

        public Member SetDivert(bool status, int sessionHolderId,int organisationId)
        {
            try
            {
                return DbAccess.SetDivert(status, sessionHolderId, organisationId);
            }
            catch (Exception ex)
            {
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return new Member();
            }
        }
		public IEnumerable<Member> GetMembers()
		{
			return DbAccess.GetMembers();
		}

		public IEnumerable<Member> GetMembersByUser()
		{
			return DbAccess.GetMembersByUser(CurrentUser);
		}
		public IEnumerable<Member> GetMembersByOrganisationId(int OrganisationId)
		{
			return DbAccess.GetMemberByOrganisationId(OrganisationId);
		}

		public bool EnableMessageForMember(int alertId, int memberId, out bool result)
		{
			try
			{
				return DbAccess.EnableMessageForMember(alertId, memberId, out result);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				result = false;
				return result;
			}
		}
	}
}
