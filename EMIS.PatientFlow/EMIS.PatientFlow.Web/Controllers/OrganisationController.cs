using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Controllers
{
    [OutputCache(Duration = 0)]
    public class OrganisationController : Controller
    {
        private readonly IOrganisationRepository _repository;
        private readonly IKioskRepository _kioskRepository;
        public OrganisationController(IOrganisationRepository repository, IKioskRepository kioskRepository)
        {
            _repository = repository;
            _kioskRepository = kioskRepository;
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        public async Task<ActionResult> Index()
        {
            var model = new OrganisationListViewModel();
            try
            {
                model.OrganisationList = await _repository.GetOrganisations();
                ViewBag.SystemTypesList = await _repository.GetSystemTypeList();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }
            return PartialView("Index", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetOrganisationsForDropDown()
        {
            var model = new OrganisationListViewModel();
            try
            {
                model.OrganisationList = await _repository.GetOrganisationsForDropDown();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        public async Task<ActionResult> AddOrganisation()
        {
            OrganisationViewModel model = new OrganisationViewModel();
            model.OrganisationKey = string.Empty;
            try
            {
                model.SupplierId = Config.GetAppSettingSupplierIdValue;
                var systemTypesList = await _repository.GetSystemTypeList();
                var SystemTypes = new List<SelectListItem>();
                if (systemTypesList != null)
                {
                    SystemTypes = systemTypesList.Select(system => new SelectListItem
                    {
                        Text = system.Name,
                        Value = system.Id.ToString()

                    }).ToList();
                }
                model.SystemTypesList = SystemTypes;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }
            return PartialView("AddOrganisation", model);
        }

        [HttpPost]
        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        public async Task<ActionResult> SaveOrganisation(OrganisationViewModel organisationvm)
        {
            try
            {
                string exception = string.Empty;
                string slotException = string.Empty;

                switch (organisationvm.SystemTypeId)
                {
                    case 1:
                        organisationvm.SystemType = SystemType.EmisWeb.GetDisplayName();
                        organisationvm.OrganisationKey = organisationvm.SiteNumber;
                        break;
                    case 2:
                        organisationvm.SystemType = SystemType.EmisPcs.GetDisplayName();
                        break;
                    case 6:
                        organisationvm.SystemType = SystemType.EmisPcsLan.GetDisplayName();
                        break;
					case 7:
						organisationvm.SystemType = SystemType.TPPSystmOne.GetDisplayName();
						organisationvm.OrganisationKey = organisationvm.SiteNumber;
						break;
                }

                bool IsValidCredentail = false;
                if (organisationvm.SystemTypeId != 6)
                {
                    var result = new ApiHelper().GetSiteDetails(organisationvm, out exception);
                    IsValidCredentail = true;
                    if (result.Result == null)
                    {
                        IsValidCredentail = false;
                    }
                    else
                    {
                        if (organisationvm.Id > 0)
                        {
                            organisationvm.SiteList = result.Result;
                            var slotResult = await new ApiHelper().SaveAppointmentSlot(organisationvm.Id);
                            if (!String.IsNullOrWhiteSpace(slotResult))
                            {
                                return Json(new { success = false, exceptionMessage = slotResult }, JsonRequestBehavior.AllowGet);
                            }
                        }
						else
						{
							organisationvm.SiteList = result.Result;
						}
                    }
                }
                else
                {
                    IsValidCredentail = true;
                    Organisation organisation = await _repository.GetOrganisationDetails(organisationvm.Id);
                    organisationvm.SiteList = organisation.SiteList;
                }
                if (IsValidCredentail)
                {
                    await SaveOrganisationDetails(organisationvm, string.Empty);
                }
                else
                {
                    return Json(new { success = false, exceptionMessage = exception }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(new { success = false, exceptionMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

        private async Task SaveOrganisationDetails(OrganisationViewModel organisationVm, string groupName)
        {

            var organisation = new Organisation()
            {
                OrganisationName = organisationVm.OrganisationName,
                SystemTypeId = organisationVm.SystemTypeId,
                Id = organisationVm.Id,
                SiteNumber = organisationVm.SiteNumber,
                DatabaseName = organisationVm.OrganisationKey,
                SiteList = organisationVm.SiteList,
                User = new WebUser()
                {
                    Username = organisationVm.Username.Encrypt(),
                    Password = organisationVm.Password.Encrypt(),
                    SupplierId = organisationVm.SupplierId.Encrypt(),
                    IpAddress = organisationVm.IpAddress,
                    OrganisationId = organisationVm.Id,
                    Type = organisationVm.SystemType,
                    WebServiceUrl = organisationVm.WebServiceUrl,
                    InternalIpAddress = string.IsNullOrWhiteSpace(organisationVm.InternalIpAddress) ? null : organisationVm.InternalIpAddress
                }
            };

            if (organisation.Id == 0)
            {
                await _repository.AddOrganisation(organisation);
                TempData["SuccessMessage"] = "Organisation added";
            }
            else
            {
                await _repository.UpdateOrganisation(organisation, groupName);
                TempData["SuccessMessage"] = "Organisation updated";
            }
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        public async Task<ActionResult> RefreshOrganisation(int OrganisationId)
        {
            string exception = string.Empty;
            try
            {
                Organisation organisation = await _repository.GetOrganisationDetails(OrganisationId);
                if (organisation.SystemTypeId != 6)
                {
                    var result = await (new ApiHelper().SaveAppointmentSlotAndSite(organisation));
                    if (result.Result != null && result.Result.Any())
                    {
                        if (organisation.Id > 0)
                        {
                            organisation.SiteList = result.Result;
                            await _repository.UpdateOrganisation(organisation, string.Empty);
                        }
                        Session["RefreshMessage"] = "Branches and slots up to date";
                    }
                    else
                    {
                        return Json(new { success = false, exceptionMessage = result.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(new { success = false, exceptionMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        public async Task<ActionResult> DeleteOrganisation(int organisationId)
        {
            int result = 0;

            try
            {
                result = await _repository.DeleteOrganisation(organisationId);
                Session["DeleteMessage"] = "Organisation deleted";
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                Session["DeleteErrorMessage"] = "Organisation is not deleted";
            }
            return RedirectToAction("Index", "Organisation");
        }

        private void FillOrganisationDetails(OrganisationViewModel organisationVm, Organisation organisation)
        {
            organisationVm.OrganisationName = organisation.OrganisationName;
            organisationVm.SystemTypeId = organisation.SystemTypeId;
            organisationVm.SystemType = organisation.SystemType;
            organisationVm.SiteNumber = organisation.SiteNumber ?? string.Empty;
            organisationVm.OrganisationKey = organisation.DatabaseName ?? string.Empty;
            organisationVm.Id = organisation.Id;
            organisationVm.Username = organisation.User.Username.Decrypt() ?? string.Empty;
            organisationVm.Password = organisation.User.Password.Decrypt() ?? string.Empty;
            organisationVm.SupplierId = organisation.User.SupplierId.Decrypt() ?? string.Empty;
            organisationVm.IpAddress = organisation.User.IpAddress ?? string.Empty;
            organisationVm.InternalIpAddress = organisation.User.InternalIpAddress ?? string.Empty;
            organisationVm.SiteList = organisation.SiteList != null ? organisation.SiteList : new List<Site>();
            organisationVm.ModifiedDate = organisation.ModifiedDate;
            organisationVm.LinkCount = organisation.LinkCount;
            organisationVm.WebServiceUrl = organisation.User.WebServiceUrl ?? string.Empty;
            organisationVm.SystemTypesList = organisation.SystemTypeList.Select(system => new SelectListItem
            {

                Text = system.Name,
                Value = system.Id.ToString()

            }).ToList();
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [HttpGet]
        public async Task<ActionResult> EditOrganisation(int OrganisationId)
        {
            var OrganisationVm = new OrganisationViewModel();
            try
            {
                Organisation organisation = await _repository.GetOrganisationDetails(OrganisationId);
                FillOrganisationDetails(OrganisationVm, organisation);
                OrganisationVm.SlotsList = await _kioskRepository.GetAppointmentSlotTypes(OrganisationId);
            }
            catch (Exception ex)
            {

                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }
            return PartialView("EditOrganisation", OrganisationVm);
        }

        [HttpGet]
        public async Task<JsonResult> IsValidOrganisationName(string OrganisationName, int OrganisationId)
        {
            bool isUniqueName = false;
            try
            {
                if (OrganisationName != null)
                {
                    isUniqueName = await _repository.ValidateOrganisationName(OrganisationName, OrganisationId);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
            }
            return Json(new { Result = isUniqueName }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> IsValidOrganisationKey(string OrganisationKey, int OrganisationId)
        {
            bool isUniqueOrganisationKey = true;
            try
            {
                isUniqueOrganisationKey = await _repository.ValidateOrganisationKey(OrganisationKey, OrganisationId);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
            }
            return Json(new { Result = isUniqueOrganisationKey }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> IsValidOrganisationSiteNumber(string OrganisationSiteNumber, int OrganisationId)
        {
            bool isUniqueOrganisationSiteNumber = true;
            try
            {
                isUniqueOrganisationSiteNumber = await _repository.ValidateOrganisationSiteNumber(OrganisationSiteNumber, OrganisationId);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
            }
            return Json(new { Result = isUniqueOrganisationSiteNumber }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetOrganisationData()
        {
            var model = new OrganisationListViewModel();
            try
            {
                model.OrganisationList = await _repository.GetOrganisations();

                List<Organisation> organisationData = model.OrganisationList;

                int sortColumn = -1;
                string sortDirection = "asc";
                var result = new List<Organisation>();
                if (Request.QueryString["order[0][dir]"] != null)
                {
                    sortDirection = Request.QueryString["order[0][dir]"];
                }
                if (Request.QueryString["order[0][column]"] != null)
                {
                    sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
                    switch (sortColumn)
                    {
                        case 1:
                            result = sortDirection == "desc" ? organisationData.OrderByDescending(m => m.OrganisationName).ToList()
                                                     : organisationData.OrderBy(m => m.OrganisationName).ToList();

                            break;
                        case 2:
                            result = sortDirection == "desc" ? organisationData.OrderByDescending(m => m.SystemType).ToList()
                                                     : organisationData.OrderBy(m => m.SystemType).ToList();

                            break;
                        case 3:
                            result = sortDirection == "desc" ? organisationData.OrderByDescending(m => m.SiteNumber).ToList()
                                                     : organisationData.OrderBy(m => m.SiteNumber).ToList();

                            break;
                        default:
                            result = organisationData.OrderBy(m => m.OrganisationName).ToList();
                            break;
                    }
                }

                string organisationNameFilter = Request.QueryString["columns[1][search][value]"] ?? Request.QueryString["columns[1][search][value]"].ToString();
                string systemTypeFilter = Request.QueryString["columns[2][search][value]"] ?? Request.QueryString["columns[2][search][value]"].ToString();
                string siteNumberFilter = Request.QueryString["columns[3][search][value]"] ?? Request.QueryString["columns[3][search][value]"].ToString();

                if (!string.IsNullOrWhiteSpace(systemTypeFilter))
                {
                    string[] systemTypeFilters = systemTypeFilter.Split(',');
                    result = result.Where(x => Array.IndexOf(systemTypeFilters, x.SystemTypeId.ToString()) >= 0).ToList();
                }

                if (!string.IsNullOrWhiteSpace(organisationNameFilter))
                {
                    result = result.Where(x => x.OrganisationName.ToString().IndexOf(organisationNameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                if (!string.IsNullOrWhiteSpace(siteNumberFilter))
                {
                    result = result.Where(x => x.SiteNumber.IndexOf(siteNumberFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                model.draw = int.Parse(Request.QueryString["draw"]);
                int start = int.Parse(Request.QueryString["start"]);
                int length = int.Parse(Request.QueryString["length"]);

                model.data = result.Skip(start).Take(length).ToArray();
                model.recordsTotal = organisationData.Count();
                model.recordsFiltered = result.Count();
                model.OrganisationList = new List<Organisation>();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return (Json(model, JsonRequestBehavior.AllowGet));
            }
        }
    }
}