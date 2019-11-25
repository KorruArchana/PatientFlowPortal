using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
	public class OrganisationRepository : BaseRepository, IOrganisationRepository
	{
		public IEnumerable<Organisation> GetOrganisationList(List<int> organisationIds)
		{
			try
			{
				return DbAccess.GetOrganisationList(organisationIds);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Organisation>();
			}
		}

		public IEnumerable<Organisation> GetOrganisationListForKiosk(int kioskId)
		{
			try
			{
				return DbAccess.GetOrganisationListForKiosk(kioskId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Organisation>();
			}
		}

		public IEnumerable<Organisation> GetOrganisationListForAlert(int alertId)
		{
			try
			{
				return DbAccess.GetOrganisationListForAlert(alertId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Organisation>();
			}
		}
		public IEnumerable<Organisation> GetOrganisations()
		{
			try
			{
				return DbAccess.GetOrganisations();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Organisation>();
			}

		}

		public IEnumerable<Organisation> GetOrganisations(string userName)
		{
			try
			{
				return DbAccess.GetOrganisationsByUser(userName);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Organisation>();
			}
		}

		public IEnumerable<Organisation> GetOrganisationsForDropDown()
		{
			try
			{
				return DbAccess.GetOrganisationsForDropDown();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Organisation>();
			}

		}

		public IEnumerable<Organisation> GetOrganisationsForDropDown(string userName)
		{
			try
			{
				return DbAccess.GetOrganisationsByUserForDropDown(userName);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Organisation>();
			}
		}

		public Organisation GetOrganisationDetails(int organisationId)
		{
			try
			{
				return DbAccess.GetOrganisationDetails(organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new Organisation();
			}
		}

		public Organisation GetOrganisationDetail(int organisationId)
		{
			try
			{
				return DbAccess.GetOrganisationDetail(organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new Organisation();
			}
		}

		public IEnumerable<Department> GetDepartmentListFororganisation(int organisationId)
		{
			try
			{
				return DbAccess.GetDepartmentListForOrganisation(organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Department>();
			}
		}

		public int AddOrganisation(Organisation organisation)
		{
			try
			{
				return DbAccess.AddOrganisation(organisation, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public int DeleteOrganisation(int organisationId)
		{
			try
			{
				return DbAccess.DeleteOrganisation(organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public int UpdateOrganisation(Organisation organisation)
		{
			try
			{
				return DbAccess.UpdateOrganisation(organisation, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public WebUser GetPatientFlowUser(int organisationId)
		{
			try
			{
				return DbAccess.GetPatientFlowUser(organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new WebUser();
			}
		}

		public void ValidateOrganisationName(string organisationName, int organisationId, out bool status)
		{
			try
			{
				DbAccess.ValidateOrganisationName(organisationName, organisationId, out status);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				status = false;
			}
		}

		public void ValidateOrganisationKey(string organisationKey, int organisationId, out bool status)
		{
			try
			{
				DbAccess.ValidateOrganisationKey(organisationKey, organisationId, out status);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				status = false;
			}
		}

		public void ValidateOrganisationSiteNumber(string organisationSiteNumber, int organisationId, out bool status)
		{
			try
			{
				DbAccess.ValidateOrganisationSiteNumber(organisationSiteNumber, organisationId, out status);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				status = false;
			}
		}


		public List<KeyValuePair<int, string>> GetOrganisationAccessRights(string userName)
		{
			try
			{
				return DbAccess.GetOrganisationAccessRights(userName);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<KeyValuePair<int, string>>();
			}
		}

		public void SaveAccessMapping(string userName, List<int> accesses)
		{
			try
			{
				DbAccess.SaveAccessMapping(userName, accesses, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
			}
		}

        public bool DeleteOrganisationAuthUsers(string userName)
        {
            try
            {
              return DbAccess.DeleteOrganisationAuthUsers(userName);
            }
            catch (Exception ex)
            {                
                GenerateSqlException(ex);
                Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
                return false;
            }
        }

        public List<Entity> GetSystemTypeList()
		{
			try
			{
				return DbAccess.GetSystemTypeList();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Entity>();
			}
		}

		public List<string> GetIPAddresses()
		{
			try
			{
				return DbAccess.GetIPAddresses();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<string>();
			}

		}

		public List<AuthUser> GetAuthUsersForPracticeAdmin()
		{
			try
			{
				return DbAccess.GetAuthUsers(CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<AuthUser>();
			}

		}

		public List<AuthUser> GetAuthUsers()
		{
			try
			{
				return DbAccess.GetAuthUsers(string.Empty);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<AuthUser>();
			}

		}
	}
}
