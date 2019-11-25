using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class SiteMenuRepository : BaseRepository, ISiteMenuRepository
    {
        public List<SiteMenu> GetSiteMenuList(int parentMenuId)
        {
			try
			{
				return DbAccess.GetSiteMenuList(parentMenuId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<SiteMenu>();
			} 
        }
     
        public List<SiteMenu> GetDepartmentMemberTreeList(Kiosk kiosk)
        {
			try
			{
				return DbAccess.GetKioskDepartmentMemberTreeList(kiosk);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<SiteMenu>();
			} 
        }

        public List<SiteMenu> GetKioskDepartmentMemberTreeListDetails(Kiosk kiosk)
        {
			try
			{
				return DbAccess.GetKioskDepartmentMemberTreeListDetails(kiosk);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<SiteMenu>();
			} 
        }

        public List<SiteMenu> GetDepartmentMemberTreeListByOrganisationId(string orgIds,out List<Site> site)
        {
			try
			{
				return DbAccess.GetDepartmentMemberTreeListByOrganisationId(orgIds, out site);
			}
			catch (Exception ex)
			{
				site = new List<Site>();
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<SiteMenu>();
			} 
        }

        public List<SiteMenu> GetAlertDepartmentMemberTreeList(int alertId)
        {
			try
			{
				return DbAccess.GetAlertDepartmentMemberTreeList(alertId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<SiteMenu>();
			} 
        }

        public List<SiteMenu> GetAlertDepartmentMemberTreeListDetails(int alertId)
        {
			try
			{
				return DbAccess.GetAlertDepartmentMemberTreeListDetails(alertId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<SiteMenu>();
			} 
        }

        public List<SiteMenu> GetSiteMenuByParentId(int parentId)
        {
			try
			{
				return DbAccess.GetSiteMenuByParentId(parentId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<SiteMenu>();
			} 
        }

        public int GetMenuBypermission(int parentId)
        {
			try
			{
				return DbAccess.GetMenuBypermission(parentId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			} 
        }

         
    }
}
