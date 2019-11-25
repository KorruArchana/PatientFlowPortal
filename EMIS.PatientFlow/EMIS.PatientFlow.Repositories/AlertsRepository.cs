using System;
using System.Collections.Generic;
using System.Text;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
	public class AlertsRepository : BaseRepository, IAlertsRepository
	{
		public IEnumerable<Alert> GetAlerts()
		{
			try
			{
				return DbAccess.GetAlerts();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Alert>();
			}
		}

		public IEnumerable<Alert> GetAlertsByUser()
		{
			try
			{
				return DbAccess.GetAlertsByUser(CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				
				return new List<Alert>();
			}
		}

		public List<Alert> GetAlertsListForOrganisation(List<int> organisationIds)
		{
			try
			{
				return DbAccess.GetAlertsListForOrganisation(organisationIds);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Alert>();
			}
		}

		public int AddAlert(Alert alert)
		{
			try
			{
				return DbAccess.AddAlerts(alert, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return 0;
			}
		}

		public List<int> GetSessionHolderIdForAlert(int alertId)
		{
			try
			{
				return DbAccess.GetSessionHolderIdForAlert(alertId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<int>();
			}
		}

		public List<int> GetOrganisationIdListForAlert(int alertId)
		{
			try
			{
				return DbAccess.GetOrganisationIdListForAlert(alertId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<int>();
			}
		}

		public int UpdateAlert(Alert alert)
		{
			try
			{
				return DbAccess.UpdateAlerts(alert, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public Alert GetAlertDetails(int alertId)
		{
			try
			{
				Alert alert = DbAccess.GetAlertDetails(alertId);
				alert.OrganisationIds = new List<int>();
				StringBuilder OrganisationName = new StringBuilder();
				foreach (var item in alert.Organisation)
				{
					OrganisationName.Append(item.OrganisationName).Append(",");
					alert.OrganisationIds.Add(item.Id);
				}
				alert.OrganisationList =  OrganisationName.ToString().TrimEnd(',') ;
				return alert;
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new Alert();
			}
		}
		public int DeleteAlert(int alertId)
		{
			try
			{
				return DbAccess.DeleteAlert(alertId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public List<string> GetActiveGroupsForAlert(int alertId)
		{
			try
			{
				return DbAccess.GetActiveGroupsForAlert(alertId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<string>();
			}
		}

		public IEnumerable<Alert> GetAlertsByMember(int MemberId)
		{
			try
			{
				return DbAccess.GetAlertsByMember(MemberId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Alert>();
			}
		}
		public IEnumerable<Alert> GetAllAlertsByMemberOrganisation(int MemberId)
		{
			try
			{
				return DbAccess.GetAllAlertsByMemberOrganisation(MemberId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Alert>();
			}
		}
	}
}
