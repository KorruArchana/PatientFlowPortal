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
using System.Web.UI.WebControls;

namespace EMIS.PatientFlow.Web.Controllers
{
    [OutputCache(Duration = 0)]
    [Authorize(Roles = "Practice Admin, EMIS Super User,Egton Engineer")]
    public class KioskController : Controller
    {
        private readonly IKioskRepository _repository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IHomeRepository _homeRepository;
        public KioskController(IKioskRepository repository, IOrganisationRepository organisationRepository, IHomeRepository homeRepository)
        {
            _repository = repository;
            _organisationRepository = organisationRepository;
            _homeRepository = homeRepository;
        }

        public async Task<ActionResult> Index()
        {
            if (!User.IsInRole("Practice Admin"))
            {
                return RedirectToAction("Index", "EgtonKiosk");
            }
            else
            {
                var model = new KioskListingViewModel();
                model.KioskList = new List<Kiosk>();
                try
                {
                    List<Kiosk> kioskList = await _repository.GetKiosksWithUsageLog();

                    if (kioskList != null)
                    {
                        model.KioskList = kioskList.Select(kiosk => new Kiosk
                        {
                            Id = kiosk.Id,
                            KioskName = kiosk.KioskName,
                            Title = kiosk.Title,
                            KioskGuid = kiosk.KioskGuid,
                            PcName = kiosk.PcName,
                            IpAddress = kiosk.IpAddress,
                            KioskVersion = kiosk.KioskVersion,
                            Status = kiosk.Status,
                            Usage = kiosk.Usage,
                            OrganisationName = kiosk.OrganisationName,
                            ConnectionGuid = kiosk.ConnectionGuid,
                            KioskStatus = kiosk.KioskStatus
                        }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                    return null;
                }

                return PartialView("Index", model);
            }
        }

        public async Task<ActionResult> AddKiosk()
        {
            var kioskAdd = new KioskEditViewModel();
            try
            {
                kioskAdd.OrganisationList = await _organisationRepository.GetOrganisationsForDropDown();
                var organisationList = new List<SelectListItem>();
                if (kioskAdd.OrganisationList != null)
                {
                    organisationList = kioskAdd.OrganisationList.Select(organisation => new SelectListItem
                    {
                        Text = organisation.OrganisationName,
                        Value = "*" + organisation.Id.ToString()
                    }).ToList();
                }
                kioskAdd.OrganisationSelectList = organisationList;

                KioskMasterDetails kioskMasterDetails = await _homeRepository.GetKioskMasterDetails();
                kioskAdd.LanguageList = kioskMasterDetails.Languages;
                kioskAdd.AvailablePatientMatches = kioskMasterDetails.PatientMatches;
                kioskAdd.AvailableAppointmentMatches = kioskMasterDetails.AppointmentMatches;
                kioskAdd.AvailableModules = kioskMasterDetails.Modules;
                List<DemographicDetails> demographicsList = kioskMasterDetails.DemographicDetails;

                var languageList = new List<SelectListItem>();
                if (kioskAdd.LanguageList != null)
                {
                    languageList = kioskAdd.LanguageList.Select(language => new SelectListItem
                    {
                        Text = language.LanguageName,
                        Value = language.Id.ToString()
                    }).ToList();
                }
                kioskAdd.LanguageSelectList = languageList.OrderByDescending(a => a.Text.ToUpper() == "ENGLISH").ThenBy(b => b.Text).ToList();


                kioskAdd.AvailableModules.Where(m => m.Id == 1).Select(m => m.IsSelected = true).ToList();


                var demograhicList = new List<SelectListItem>();
                if (demographicsList != null)
                {
                    demograhicList = demographicsList.Select(demographic => new SelectListItem
                    {
                        Text = demographic.ModuleNameToDisplay,
                        Value = demographic.DemographicDetailsTypeId.ToString()
                    }).ToList();
                }
                kioskAdd.DemographicList = demograhicList;

                kioskAdd.KioskInstance = new Kiosk
                {
                    DemographicDetailsDuration = 180,
                    EarlyArrival = 60,
                    LateArrival = 10,
                    ScreenTimeOut = 30,
                    FileasKioskUser = true,
                    ShowDoctorDelay = true,
                    AllowUntimed = false,
                    AutoConfirmArrival = true,
                    AutoConfirmMultipleArrival = true,
                    SelectedLanguageList = new List<int> { Convert.ToInt32(kioskAdd.LanguageSelectList.Select(item => item.Value).First()) }
                };

                if (kioskAdd.AvailablePatientMatches.Count > 0)
                    kioskAdd.KioskInstance.PatientMatchId = kioskAdd.AvailablePatientMatches.Select(item => item.Id).First();
                if (kioskAdd.AvailableAppointmentMatches.Count > 0)
                    kioskAdd.KioskInstance.AppointmentMatchId = kioskAdd.AvailableAppointmentMatches.Select(item => item.Id).First();

                //Has to check and update it yet...
                var list = new List<SelectListItem> { new SelectListItem() { Text = "Not configured", Value = "1" } };
                ViewData["Status"] = list;

            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }
            return View("AddEditKiosk", kioskAdd);
        }

        //Has to update it better
        public async Task<JsonResult> GetAdditionalDetails(int organisationId)
        {
            Organisation organisation = await _organisationRepository.GetOrganisationDetails(organisationId);
            var branchViewModel = GetLocation(organisation);
            var slots = await GetAppointmentSlotType(organisationId);
            return Json(new { branchList = branchViewModel, slotList = slots}, JsonRequestBehavior.AllowGet);
        }

		public async Task<JsonResult> GetAdditionalMemberDetails(int organisationId)
		{
			try
			{
				Organisation organisation = await _organisationRepository.GetOrganisationDetails(organisationId);
				var members = await GetMember(organisation);
				return Json(new { memberList = members }, JsonRequestBehavior.AllowGet);
			}
			catch(Exception ex)
			{
				return Json(new { memberList = new List<Member>() }, JsonRequestBehavior.AllowGet);
			}
		}

		private BrachViewModel GetLocation(Organisation organisation)
        {
            List<Site> siteList = organisation.SiteList;
            BrachViewModel branchViewModel = new BrachViewModel
            {
                BranchId = 0
            };
            if (siteList != null && siteList.Count > 0)
            {
                branchViewModel.SitesList = siteList.Select(site => new SelectListItem
                {
                    Text = site.SiteName,
                    Value = site.Id.ToString()
                }).ToList();
                branchViewModel.OrganisationId = organisation.Id;
            }
            else
            {
                branchViewModel.SitesList = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "No Branch Site Available",
                        Value = "0"
                    }
                };
                branchViewModel.BranchId = 0;
            }

            if (branchViewModel.OrganisationId == organisation.Id)
            {
                branchViewModel.OrganisationName = organisation.OrganisationName;
            }

            return branchViewModel;
        }

        private async Task<IEnumerable<Member>> GetMember(Organisation organisation)
        {
            try
            {
                IEnumerable<Member> sessHolderList = await new ApiHelper().GetMember(organisation.Id,organisation.OrganisationName);            
                return sessHolderList;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        public async Task<List<AppointmentSlotType>> GetAppointmentSlotType(int id)
        {
            try
            {
                List<AppointmentSlotType> slotsList = await (new ApiHelper()).GetAppointmentSlotType(id);
                //  slotsList.ForEach(slot => slot.OrganisationName = organisation.OrganisationName);
                return slotsList;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
                return null;
            }
        }

        public async Task<ActionResult> SaveKiosk(KioskEditViewModel kioskVm)
        {
            try
            {
                int kioskId;
				
				kioskVm.KioskInstance.SelectedOrganisationList = kioskVm.KioskInstance.SelectedOrganisationValue.Select(x => int.Parse(x.TrimStart('*'))).ToList();

				Kiosk kiosk = SetKioskValues(kioskVm);

                if (kioskVm.KioskInstance.Id > 0)
                {
                    kiosk.Id = kioskVm.KioskInstance.Id;
                    kioskId = await _repository.EditKiosk(kiosk);
                    TempData["successMessage"] = kioskId > 0 ? "Kiosk edited" : "Kiosk is not updated";
                }
                else
                {
                    kioskId = kiosk != null ? await _repository.AddKiosk(kiosk) : -1;
                    TempData["successMessage"] = kioskId > 0 ? "Kiosk added" : "Kiosk is not saved";
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
            }
            return null;
        }

        private Kiosk SetKioskValues(KioskEditViewModel kioskVm)
        {
            var kiosk = new Kiosk();
            if (kiosk.KioskSiteMapList == null)
                kiosk.KioskSiteMapList = new List<SiteMap>();
            try
            {
                kiosk.KioskLogoByte = kioskVm.KioskInstance.KioskLogoByte;
                kiosk.KioskName = kioskVm.KioskInstance.KioskName;
                kiosk.Title = kioskVm.KioskInstance.Title;
                kiosk.Status = kioskVm.KioskInstance.Status;
                //kiosk.Organisations = kioskVm.KioskInstance.Organisations;
                //kiosk.Languages = kioskVm.Languages;
                kiosk.AppointmentMatchId = kioskVm.KioskInstance.AppointmentMatchId;
                kiosk.PatientMatchId = kioskVm.KioskInstance.PatientMatchId;
                kiosk.EarlyArrival = kioskVm.KioskInstance.EarlyArrival;
                kiosk.LateArrival = kioskVm.KioskInstance.LateArrival;
                kiosk.DemographicDetailsDuration = kioskVm.KioskInstance.DemographicDetailsDuration;
                kiosk.SelectedModules = kioskVm.AvailableModules.Where(item => item.IsSelected == true).Select(x => x.Id).ToList();
                kiosk.SelectedLanguageList = kioskVm.KioskInstance.SelectedLanguageList;
                kiosk.SelectedOrganisationList = kioskVm.KioskInstance.SelectedOrganisationList;
                kiosk.FileasKioskUser = true;
                kiosk.ForceSurvey = kioskVm.KioskInstance.ForceSurvey;
                kiosk.ScreenTimeOut = kioskVm.KioskInstance.ScreenTimeOut;
                kiosk.AdminPassword = kioskVm.KioskInstance.AdminPassword;
                kiosk.SkipSurveyQuestion = kioskVm.KioskInstance.SkipSurveyQuestion;
                kiosk.ShowDoctorDelay = kioskVm.KioskInstance.ShowDoctorDelay;
                kiosk.AutoConfirmArrival = true;//kioskVm.KioskInstance.AutoConfirmArrival;
                kiosk.AutoConfirmMultipleArrival = true;// kioskVm.KioskInstance.AutoConfirmMultipleArrival;
                kiosk.ShowDemographicDetails = kioskVm.KioskInstance.ShowDemographicDetails;
                kiosk.ScrambleDemographicDetails = kioskVm.KioskInstance.ScrambleDemographicDetails;
                kiosk.AllowUntimed = kioskVm.KioskInstance.AllowUntimed;
                kiosk.Module = kioskVm.AvailableModules;
                kiosk.AppointmentReason = kioskVm.KioskInstance.AppointmentReason;
                if (kioskVm.KioskInstance.SelectedDemographicDetails != null)
                    kiosk.SelectedDemographicDetails = kioskVm.KioskInstance.SelectedDemographicDetails;

                var selectedSlotList = kioskVm.SlotJson.ConvertFromJsonString<List<AppointmentSlotType>>();
                var selectedMemberList = kioskVm.MemberJson.ConvertFromJsonString<List<Member>>();
                var selectedSiteMapList = kioskVm.SiteMapJson.ConvertFromJsonString<List<Entities.SiteMap>>();
                var kioskBranchList = kioskVm.BranchJson.ConvertFromJsonString<List<BrachViewModel>>();

                //Try to update those names to SelectedSlotTypes/SelectedSessionHolderList and all
                kiosk.SlotTypes = selectedSlotList.Select(slot => new AppointmentSlotType
                {
                    OrganisationId = slot.OrganisationId,
                    SlotTypeId = slot.SlotTypeId
                }).ToList();

                kiosk.SessionHolderList = selectedMemberList.Select(member => new Member
                {
                    OrganisationId = member.OrganisationId,
                    SessionHolderId = member.SessionHolderId
                }).ToList();

                if (selectedSiteMapList != null)
                    kiosk.KioskSiteMapList = selectedSiteMapList.Select(x => new SiteMap
                    {
                        SiteMapName = x.SiteMapName,
                        SiteMapImage = Convert.FromBase64String(x.SiteMapData)
                    }).ToList();

                kiosk.BranchList = kioskBranchList.Select(bmodel => new BrachModel
                {
                    BranchId = bmodel.BranchId,
                    OrganisationId = bmodel.OrganisationId,
                    IsMainLocation = bmodel.IsMainLocation
                }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }

            return kiosk;
        }

        public async Task<ActionResult> DeleteKiosk(int kioskId, string kioskName)
        {
            try
            {
                var result = await _repository.DeleteKiosk(kioskId);
                Session["DeleteMessage"] = (result == true) ? " Kiosk deleted" : "Kiosk has not been deleted";
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                Session["DeleteErrorMessage"] = "Kiosk has not been deleted";
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> EditKiosk(int kioskId)
        {
            KioskEditViewModel kioskEditModel = new KioskEditViewModel();
            try
            {
                Kiosk kiosk = await _repository.GetKioskDetails(kioskId);
                kioskEditModel = await PopulateKioskEditvalues(kiosk);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }

            return View("AddEditKiosk", kioskEditModel);
        }

        private async Task<KioskEditViewModel> PopulateKioskEditvalues(Kiosk kiosk)
        {
            KioskEditViewModel model = new KioskEditViewModel
            {
                KioskInstance = kiosk,
                OrganisationSelectList = new List<SelectListItem>(),
                LanguageSelectList = new List<SelectListItem>(),
                BranchList = new List<BrachViewModel>()
            };
            var demograhicList = new List<SelectListItem>();

            var kioskMasterDetails = await _homeRepository.GetKioskMasterDetails();
            var demographicsList = kioskMasterDetails.DemographicDetails;
            model.LanguageList = kioskMasterDetails.Languages;
            model.AvailableModules = kioskMasterDetails.Modules;
            model.Module = kiosk.Module;
            model.AvailablePatientMatches = kioskMasterDetails.PatientMatches;
            model.AvailableAppointmentMatches = kioskMasterDetails.AppointmentMatches;

            var list = new List<SelectListItem> { new SelectListItem() { Text = "Not configured", Value = "1" } };
            ViewData["status"] = new SelectList(list, "Value", "Text", kiosk.Status);
            model.KioskInstance.Status = kiosk.Status;
            //Demographics


            if (demographicsList != null)
            {
                demograhicList = demographicsList.Select(demographic => new SelectListItem
                {
                    Text = demographic.ModuleNameToDisplay,
                    Value = demographic.DemographicDetailsTypeId.ToString(),
                }).ToList();
            }
            model.DemographicList = demograhicList;
            model.OrganisationList = await _organisationRepository.GetOrganisationsForDropDown();

            model.OrganisationSelectList = (model.OrganisationList.Select(org => new SelectListItem
            {
                Text = org.OrganisationName,
                Value = "*" + org.Id.ToString(),
                Selected = kiosk.SelectedOrganisationList.Contains(org.Id)
            })).ToList();

			//model.OrganisationSelectList.Where().ForEach(x => x.Value = string.Concat('*', x.Value)).where

            model.KioskInstance.SelectedOrganisationList = kiosk.SelectedOrganisationList;
            model.KioskInstance.SelectedOrganisationValue = new List<string>();
			model.KioskInstance.SelectedOrganisationText = model.OrganisationList
				.Where(a => kiosk.SelectedOrganisationList.Contains(a.Id))
				.Select(a => a.OrganisationName).ToList();

			if (model.LanguageList != null)
            {
                model.LanguageSelectList = (model.LanguageList.Select(lang => new SelectListItem
                {
                    Text = lang.LanguageName,
                    Value = lang.Id.ToString(),
                })).ToList();
            }

            model.LanguageSelectList = model.LanguageSelectList.OrderByDescending(a => a.Text.ToUpper() == "ENGLISH").ThenBy(b => b.Text).ToList();
            model.KioskInstance.SelectedLanguageList = kiosk.SelectedLanguageList;

            var selectedModules = kiosk.Module.Select(m => m.Id).ToList();
            model.AvailableModules.Where(x => selectedModules.Contains(x.Id)).ToList().ForEach(m => m.IsSelected = true);

            foreach (var items in model.KioskInstance.BranchList)
            {
                var bmodel = new BrachViewModel
                {
                    OrganisationId = items.OrganisationId,
                    OrganisationName = items.OrganisationName,
                    IsMainLocation = items.IsMainLocation,
                    SitesList = new List<SelectListItem>()
                };
                if (items.SiteList != null && items.SiteList.Count > 0)
                {
                    bmodel.SitesList = (items.SiteList.Select(s => new SelectListItem
                    {
                        Text = s.SiteName,
                        Value = s.Id.ToString(),
                        Selected = (s.Id == items.BranchId)
                    })).ToList();

                    bmodel.BranchId = items.BranchId;
                }
                else
                {
                    bmodel.SitesList.Add(new SelectListItem { Text = " No Branch Sites Available", Value = "0" });
                }

                model.BranchList.Add(bmodel);
            }

            model.AvailableSlotTypes = new List<AppointmentSlotType>();
            foreach (var id in kiosk.SelectedOrganisationList)
            {
                var slots = await GetAppointmentSlotType(id);
                if (slots != null)
                {
                    model.AvailableSlotTypes.AddRange(slots);
                }
            }

            model.SlotTypes = kiosk.SlotTypes;

            model.AvailableSlotTypes.Where(slots => (kiosk.SlotTypes.Any(ss => ss.SlotTypeId == slots.SlotTypeId && ss.OrganisationId == slots.OrganisationId))).ToList().ForEach(selSlots => selSlots.IsSelected = true);


            model.SessionHolderList = new List<Member>();
            foreach (var id in kiosk.SelectedOrganisationList)
            {
                var firstOrDefault = model.OrganisationList.FirstOrDefault(o => o.Id == id);
	            if (firstOrDefault == null) continue;
				//if (kiosk.SessionHolderList.Any(a => a.OrganisationId == firstOrDefault.Id))
				//{
					var members = await (new ApiHelper()).GetMember(id, firstOrDefault.OrganisationName);
					if (members != null)
					{
						model.SessionHolderList.AddRange(members);
					}
				//}
            }           

            model.SessionHolderList.Where(mem => (kiosk.SessionHolderList.Any(mb => mb.SessionHolderId == mem.SessionHolderId
                                                                                    && mb.OrganisationId == mem.OrganisationId)))
                        .ToList().ForEach(selMembers => selMembers.IsSelected = true);

            //Has to update the name in view and delete this and it's property too...
            model.SelectedSessionHolderIdList = new List<int>();

			model.KioskLogoByte = kiosk.KioskLogoByte != null ? Convert.ToBase64String(kiosk.KioskLogoByte) : string.Empty;
            model.KioskInstance.KioskSiteMapList = kiosk.KioskSiteMapList.Select(x => new SiteMap()
            {
                SiteMapName = x.SiteMapName,
                SiteMapData = Convert.ToBase64String(x.SiteMapImage)
            }).ToList();

            return model;
        }

        [HttpGet]
        public async Task<JsonResult> GetKioskList(int organisationId)
        {
            var model = new List<Kiosk>();
            try
            {
                model = await _repository.GetKioskDetailListForOrganisation(organisationId);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetKioskSyncServiceKeys(int KioskId)
        {
            var model = new KioskSyncKeys();
            try
            {
                var kioskSyncKeys = await _repository.GetKioskSyncKeys(KioskId);
                model.SyncGuid = kioskSyncKeys.SyncGuid;
                model.KioskGuid = kioskSyncKeys.KioskGuid;
                model.KioskName = kioskSyncKeys.KioskName;
                model.OrganisationName = kioskSyncKeys.OrganisationName;
				model.SyncConnectionGuid = kioskSyncKeys.SyncConnectionGuid;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> UpdateKioskStatus(int kioskId, string connectionId)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionId))
                {
                    string value = await _repository.UpdateKioskStatus(kioskId, connectionId, 2);
                    if (value != null)
                        Session["DeleteMessage"] = "Kiosk restarted";
                    else
                        Session["DeleteMessage"] = "Failed to restart kiosk";
                }
                else
                {
                    Session["DeleteMessage"] = "Failed to restart kiosk";
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                Session["DeleteMessage"] = "Failed to restart kiosk";

                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}