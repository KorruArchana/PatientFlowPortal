using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface ISiteMenuRepository
    {
        Task<List<SiteMenu>> LoadSiteMenu(int parentmenuid = 0);
        Task<int> GetMenuBypermission(int parentId);
        Task<List<SiteMenu>> GetSiteMenuByParentId(int parentId);
        Task<List<SiteMenu>> GetDepartmentMemberTreeList(Kiosk kiosk);
        Task<List<SiteMenu>> GetKioskDepartmentMemberTreeListDetails(Kiosk kiosk);
        Task<List<SiteMenu>> GetAlertDepartmentMemberTreeListDetails(int alertId);
        Task<List<SiteMenu>> GetAlertDepartmentMemberTreeList(int alertId);
        Task<dynamic> GetDepartmentMemberTreeListByOrganisationId(string orgIds);
    }
}