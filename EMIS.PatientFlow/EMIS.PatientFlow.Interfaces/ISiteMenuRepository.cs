using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface ISiteMenuRepository
    {
        List<SiteMenu> GetSiteMenuByParentId(int parentId);
        int GetMenuBypermission(int parentId);       
        List<SiteMenu> GetSiteMenuList(int parentMenuId);     
        List<SiteMenu> GetDepartmentMemberTreeList(Kiosk kiosk);
        List<SiteMenu> GetKioskDepartmentMemberTreeListDetails(Kiosk kiosk);
        List<SiteMenu> GetDepartmentMemberTreeListByOrganisationId(string orgIds,out List<Site> site);  
        List<SiteMenu> GetAlertDepartmentMemberTreeList(int alertId);
        List<SiteMenu> GetAlertDepartmentMemberTreeListDetails(int alertId);
    }
}
