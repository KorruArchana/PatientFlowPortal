using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class SiteMenuRepository : BaseRepository, ISiteMenuRepository
    {
        public async Task<List<SiteMenu>> LoadSiteMenu(int parentmenuid = 0)
        {
            return await GetAsync<List<SiteMenu>>("api/SiteMenu/GetSiteMenuList?ParentMenuId=" + parentmenuid);
        }

        public async Task<int> GetMenuBypermission(int parentId)
        {
            return await GetAsync<int>("api/SiteMenu/GetMenuBypermission?parentId=" + parentId);
        }
        

        public async Task<List<SiteMenu>> GetDepartmentMemberTreeList(Kiosk kiosk)
        {
            var jsonKiosk = new JavaScriptSerializer().Serialize(kiosk);
            return await PostAsJsonAsync<List<SiteMenu>>("api/SiteMenu/GetDepartmentMemberTreeList", jsonKiosk);
        }

        public async Task<List<SiteMenu>> GetKioskDepartmentMemberTreeListDetails(Kiosk kiosk)
        {
            var jsonKiosk = new JavaScriptSerializer().Serialize(kiosk);
            return await PostAsJsonAsync<List<SiteMenu>>("api/SiteMenu/GetKioskDepartmentMemberTreeListDetails", jsonKiosk);
        }
        
        public async Task<List<SiteMenu>> GetAlertDepartmentMemberTreeList(int alertId)
        {
           return await GetAsync<List<SiteMenu>>("api/SiteMenu/GetAlertDepartmentMemberTreeList?alertId=" + alertId);
        }

        public async Task<List<SiteMenu>> GetAlertDepartmentMemberTreeListDetails(int alertId)
        {
            return await GetAsync<List<SiteMenu>>("api/SiteMenu/GetAlertDepartmentMemberTreeListDetails?alertId=" + alertId);
        }

        public async Task<dynamic> GetDepartmentMemberTreeListByOrganisationId(string orgIds)
        {
            return await GetAsync<dynamic>("api/SiteMenu/GetDepartmentMemberTreeListByOrganisationId?orgIds=" + orgIds);
        }
        
        public async Task<List<SiteMenu>> GetSiteMenuByParentId(int parentId)
        {
            return await GetAsync<List<SiteMenu>>("api/SiteMenu/GetSiteMenu?parentId=" + parentId.ToString());
        }
    }
}