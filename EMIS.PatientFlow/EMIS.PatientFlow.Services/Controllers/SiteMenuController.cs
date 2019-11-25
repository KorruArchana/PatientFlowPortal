using System;
using System.Collections.Generic;
using System.Web.Http;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;

namespace EMIS.PatientFlow.Services.Controllers
{
    public class SiteMenuController : ApiController
    {
        private readonly ISiteMenuRepository _repository;
        private readonly ILoggerRepository _logger;
        
        public SiteMenuController(ISiteMenuRepository sitemenuRepository, ILoggerRepository loggerRepository)
        {
             _logger = loggerRepository;
             _repository = sitemenuRepository;
        }

         [HttpGet]
         [ActionName("GetSiteMenu")]
         public List<SiteMenu> GetSiteMenu(int parentId)
         {
             return _repository.GetSiteMenuByParentId(parentId);
         }

         [HttpGet]
         [ActionName("GetMenuBypermission")]
         public int GetMenuBypermission(int parentId)
         {
             return _repository.GetMenuBypermission(parentId);
         }

         [ActionName("GetSiteMenuList")]
         [AcceptVerbs("GET", "POST")]
         public List<SiteMenu> GetSiteMenuList(int parentMenuId)
         {
             List<SiteMenu> siteMenuList;
             try
             {
               siteMenuList = _repository.GetSiteMenuList(parentMenuId);
             }
             catch (Exception ex)
             {
                 _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                 return null;
             }

             return siteMenuList;
         }

         public List<SiteMenu> GetSiteMenuByParentId(int parentId)
         {
             List<SiteMenu> siteMenuList;
             try
             {
                 siteMenuList = _repository.GetSiteMenuByParentId(parentId);
             }
             catch (Exception ex)
             {
                 _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                 return null;
             }

             return siteMenuList;
         }

        [HttpPost]
         public List<SiteMenu> GetDepartmentMemberTreeList([FromBody]string value)
         {
             List<SiteMenu> siteMenuList;
            try
             {
                 Kiosk kiosk = value.ConvertFromJsonString<Kiosk>();
                 siteMenuList = _repository.GetDepartmentMemberTreeList(kiosk);
             }
             catch (Exception ex)
             {
                 _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                 return null;
             }

             return siteMenuList;
         }

        [HttpPost]
        public List<SiteMenu> GetKioskDepartmentMemberTreeListDetails([FromBody]string value)
         {
             List<SiteMenu> siteMenuList;
            try
             {
                 Kiosk kiosk = value.ConvertFromJsonString<Kiosk>();
                 siteMenuList = _repository.GetKioskDepartmentMemberTreeListDetails(kiosk);
             }
             catch (Exception ex)
             {
                 _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                 return null;
             }

             return siteMenuList;
         }

        [HttpGet]
        public dynamic GetDepartmentMemberTreeListByOrganisationId(string orgIds)
        {
            List<SiteMenu> siteMenuList;
            List<Site> site;
            try
            {
                
                siteMenuList = _repository.GetDepartmentMemberTreeListByOrganisationId(orgIds,out site);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                return null;
            }

            return new
            {
                siteMenuList = siteMenuList,
                site = site
            };
                
              
        }

         public List<SiteMenu> GetAlertDepartmentMemberTreeList(int alertId)
         {
             List<SiteMenu> siteMenuList;
             try
             {
                 siteMenuList = _repository.GetAlertDepartmentMemberTreeList(alertId);
             }
             catch (Exception ex)
             {
                 _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                 return null;
             }

             return siteMenuList;
         }

         public List<SiteMenu> GetAlertDepartmentMemberTreeListDetails(int alertId)
         {
             List<SiteMenu> siteMenuList;
             try
             {
                 siteMenuList = _repository.GetAlertDepartmentMemberTreeListDetails(alertId);
             }
             catch (Exception ex)
             {
                 _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                 return null;
             }

             return siteMenuList;
         }

    }
}
