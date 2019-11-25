using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Enums;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Controllers
{
    [OutputCache(Duration = 0)]
    [Authorize(Roles = "Practice Admin, EMIS Super User,Egton Engineer")]
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class MessagesController : Controller
    {
        private readonly IAlertRepository _repository;
        private readonly IKioskRepository _kioskRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IPatientRepository _patientRepository;

        public MessagesController(
            IAlertRepository repository,
            IOrganisationRepository organisationRepository,
            IKioskRepository kioskRepository,
             IPatientRepository patientRepository)
        {
            _repository = repository;
            _organisationRepository = organisationRepository;
            _kioskRepository = kioskRepository;
            _patientRepository = patientRepository;
        }


        private static IEnumerable<SelectListItem> GetDisplayType()
        {
            List<SelectListItem> alertDisplayType = Enum.GetValues(typeof(AlertDisplayType)).Cast<AlertDisplayType>().Select(item =>
                            new SelectListItem { Text = item.ToString(), Value = ((int)item).ToString(CultureInfo.InvariantCulture) }).ToList();
            return alertDisplayType;
        }

        private static List<string> GetLinkedKiosk(AlertsViewModel alertsVm)
        {
            var list = alertsVm.LinkedKiosk;

            if (list != null)
            {
                list.Remove("All Kiosks");
            }

            return list;
        }


        public async Task<ActionResult> Index()
        {
            if (!User.IsInRole("Practice Admin"))
            {
                return RedirectToAction("Index", "EgtonMessages");
            }
            else
            {
                var alertsListVm = new AlertsListViewModel();
                try
                {
                    alertsListVm = await GetMessages();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                }

                return PartialView("Index", alertsListVm);
            }
        }

        [HttpGet]
        public async Task<AlertsListViewModel> GetMessages()
        {
            var model = new AlertsListViewModel();
            model.AlertsList = new List<AlertsViewModel>();
            try
            {
                List<Alert> alertList = await _repository.GetAlerts();
                if (alertList != null)
                {

                    foreach (var alert in alertList)
                    {
                        var list = new AlertsViewModel()
                        {
                            Id = alert.Id,
                            AlertText = alert.AlertText,
                            OrganisationId = alert.OrganisationId,
                            OrganisationName = alert.OrganisationName,
                            KioskName = alert.KioskName,
                            AlertsDisplayType = ((AlertDisplayType)int.Parse(alert.AlertsDisplayType)).ToString(),
                            MessageType = alert.Target == "All patients" ? "Group" : "SomePatients",
                            Target = alert.Target
                        };
                        model.AlertsList.Add(list);
                    }
                }

                if (!User.IsInRole("Egton Engineer"))
                {
                    List<Patient> patientMessages = await _patientRepository.GetPatientMessageList();
                    var alertviewmodel = new AlertsViewModel();

                    if (patientMessages != null)
                    {
                        foreach (var patientmsg in patientMessages)
                        {
                            var list = new AlertsViewModel()
                            {
                                Id = patientmsg.PatientMessageId,
                                AlertText = patientmsg.Message,
                                OrganisationId = patientmsg.OrganisationId,
                                OrganisationName = patientmsg.OrganisationName,
                                KioskName = "All Kiosks",
                                AlertsDisplayType = "Patient Important",
                                MessageType = "Specific Patient",
                                Target = Convert.ToString(patientmsg.Surname.ToUpper() + (", ") + patientmsg.Firstname)
                            };

                            model.AlertsList.Add(list);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                ModelState.AddModelError("CustomError", ex.Message);
            }

            return model;
        }

        [HttpGet]
        public async Task<ActionResult> AddAlert(int MemberId = 0, string MemberName = null)
        {
            var alertsVm = new AlertsViewModel();
            alertsVm.MemberId = MemberId;
            alertsVm.MemberName = MemberName;
            try
            {
                alertsVm.OrganisationList = new List<SelectListItem>();
                var operation = new List<SelectListItem>
                {
                    new SelectListItem()
                    {
                        Text = "None",
                        Value = "None"
                    },
                    new SelectListItem()
                    {
                        Text = "Less than",
                        Value = "LessThan"
                    },
                    new SelectListItem()
                    {
                        Text = "Over",
                        Value = "GreaterThan"
                    },
                    new SelectListItem()
                    {
                        Text = "Between",
                        Value = "Between"
                    }
                };
                alertsVm.AlertsDisplayTypes = GetDisplayType().ToList();

                ViewBag.Operation = new SelectList(operation, "Value", "Text");
                alertsVm.KioskList = new List<SelectListItem>();

                var kiosklst = await _kioskRepository.GetKioskDetailListForOrganisation(alertsVm.OrganisationId);

                var kiosklist = new List<SelectListItem>();
                kiosklist.Add(new SelectListItem() { Text = "All Kiosks", Value = "" });
                kiosklist.AddRange(kiosklst.
                               Select(kiosk => new SelectListItem
                               {
                                   Text = kiosk.KioskName,
                                   Value = kiosk.KioskGuid
                               }));
                alertsVm.KioskList = kiosklist;
                List<Organisation> organisationsList = await _organisationRepository.GetOrganisationsForDropDown();
                var OrgsystemType = System.Configuration.ConfigurationManager.AppSettings["SystemType"];
                var organisationList = new List<SelectListItem>();
                if (organisationsList != null)
                {
                    organisationList.AddRange(organisationsList.Select(org => new SelectListItem
                    {
                        Text = org.OrganisationName,
                        Value = org.Id.ToString()
                    }).ToList());
                }
                alertsVm.OrganisationList = organisationList;
                alertsVm.SelectedDepartments = string.Empty;
                alertsVm.SelectedMembers = string.Empty;
                alertsVm.MessageType = "Group";
                alertsVm.PatientVM = new PatientViewModel();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return View("AddEditAlert", alertsVm);
            }

            return PartialView("AddEditAlert", alertsVm);
        }

        public async Task<ActionResult> EditAlert(int messageId, string MessageType)
        {
            AlertsViewModel alertsVm = new AlertsViewModel();

            try
            {
                List<Organisation> organisationsList = await _organisationRepository.GetOrganisationsForDropDown();
                var organisationList = new List<SelectListItem>();
                if (organisationsList != null)
                {
                    organisationList = organisationsList.Select(org => new SelectListItem
                    {
                        Text = org.OrganisationName,
                        Value = org.Id.ToString(),
                        Selected = org.Id == alertsVm.OrganisationId
                    }).ToList();
                }
                alertsVm.OrganisationList = organisationList;

                if (MessageType != "Specific Patient")
                {
                    Alert alerts = await _repository.AlertDetails(messageId);

                    alertsVm.Id = alerts.Id;
                    alertsVm.AlertText = alerts.AlertText;
                    alertsVm.Gender = alerts.Gender;
                    alertsVm.Age1 = alerts.Age1;
                    alertsVm.Age2 = alerts.Age2;
                    alertsVm.Operation = alerts.Operation;
                    alertsVm.AlertsDisplayType = alerts.AlertsDisplayType;
                    alertsVm.OrganisationIds = alerts.OrganisationIds;
                    alertsVm.LinkedKiosk = alerts.LinkedKiosk ?? new List<string>() { "All Kiosks" };
                    alertsVm.OrganisationId = alertsVm.OrganisationIds.FirstOrDefault();
                    alertsVm.SelectedDepartments = string.Empty;
                    alertsVm.SelectedMembers = string.Empty;

                    if (alerts.SelectedDepartments != null && alerts.SelectedDepartments.Count() > 0)
                    {
                        alertsVm.SelectedDepartments = string.Join(",", alerts.SelectedDepartments);
                    }
                    if (alerts.SelectedMembers != null && alerts.SelectedMembers.Any())
                    {
                        alertsVm.SelectedMembers = string.Join(",", alerts.SelectedMembers);
                    }

                    List<SelectListItem> operation = Enum.GetValues(typeof(Operation)).Cast<Operation>().Select(item =>
                    new SelectListItem() { Text = item.ToString(), Value = item.ToString() }).ToList();

                    IEnumerable<SelectListItem> alertDisplayType = GetDisplayType();

                    alertsVm.KioskList = new List<SelectListItem>();

                    var kiosklst = await _kioskRepository.GetKioskDetailListForOrganisation(alertsVm.OrganisationId);

                    var kiosklist = new List<SelectListItem>();
                    kiosklist.Add(new SelectListItem() { Text = "All Kiosks", Value = "" });
                    kiosklist.AddRange(kiosklst.Select(kiosk => new SelectListItem { Text = kiosk.KioskName, Value = kiosk.KioskGuid, Selected = alertsVm.LinkedKiosk.Contains(kiosk.KioskGuid) }));
                    alertsVm.KioskList = kiosklist;


                    ViewBag.Operations = new SelectList(operation, "Value", "Text");
                    ViewBag.DisplayType = new SelectList(alertDisplayType, "Value", "Text");

                    alertsVm.AlertsDisplayTypes = alertDisplayType.ToList();
                    alertsVm.PatientVM = new PatientViewModel();

                    if (alertsVm.Gender == "None" && alertsVm.Operation == "None" &&
                        alertsVm.SelectedDepartments == "" && alertsVm.SelectedMembers == "")
                    {
                        alertsVm.MessageType = "Group";
                    }
                    else
                    {
                        alertsVm.MessageType = "SomePatients";
                    }
                }
                else
                {
                    Patient patient = await _patientRepository.GetPatientDetails(messageId);
                    alertsVm.PatientVM = new PatientViewModel();
                    alertsVm.AlertText = patient.Message;
                    alertsVm.PatientVM = new PatientViewModel
                    {
                        PatientMessageId = patient.PatientMessageId,
                        Message = patient.Message,
                        OrganisationId = patient.OrganisationId,
                        OrganisationName = patient.OrganisationName,
                        PatientId = patient.PatientId,
                        Firstname = patient.Firstname,
                        Surname = patient.Surname,
                        Dob = patient.Dob
                    };
                    alertsVm.MessageType = "Specific Patient";
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return View("AddEditAlert", alertsVm);
            }

            return PartialView("AddEditAlert", alertsVm);
        }

        public async Task<ActionResult> DeleteAlert(int id, int orgId, string MessageType)
        {
            try
            {
                int result;
                result = MessageType != "Specific Patient" ? await _repository.DeleteAlert(id) : await _patientRepository.DeletePatient(id, orgId);
                Session["DeleteMessage"] = (result != -1) ? "Message deleted" : "Message is not deleted";
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                Session["DeleteErrorMessage"] = "Message is not deleted";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> SaveAlert(AlertsViewModel alertsVm)
        {
            try
            {
                if (alertsVm.MessageType != "SpecificPatient")
                {
                    var alerts = new Alert()
                    {
                        AlertText = alertsVm.AlertText,
                        AlertType = alertsVm.AlertType,
                        LinkedTo = alertsVm.LinkedTo,
                        Gender = alertsVm.Gender == null ? "None" : alertsVm.Gender,
                        Age1 = alertsVm.Age1,
                        Age2 = alertsVm.Age2,
                        Operation = alertsVm.Operation == null ? "None" : alertsVm.Operation,
                        AlertsDisplayType = alertsVm.AlertsDisplayType,
                        OrganisationId = alertsVm.OrganisationId,
                        LinkedKiosk = GetLinkedKiosk(alertsVm),
                        OrganisationIds = new List<int>() { alertsVm.OrganisationId },
                        Id = alertsVm.Id
                    };

                    if (alertsVm.SelectedDepartments != null && alertsVm.SelectedDepartments.Any())
                    {
                        alerts.SelectedDepartments = alertsVm.SelectedDepartments.Split(',').Distinct().ToList();
                    }

                    if (alertsVm.SelectedMembers != null && alertsVm.SelectedMembers.Length > 0)
                    {
                        alerts.SelectedMembers = alertsVm.SelectedMembers.Split(',').Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                    }

                    if (alertsVm.Id > 0)
                    {
                        alertsVm.Id = await _repository.UpdateAlert(alerts);
                        TempData["SuccessMessage"] = (alertsVm.Id != -1) ? "Message edited" : "Message is not updated";
                    }
                    else
                    {
                        alertsVm.Id = await _repository.AddAlert(alerts);
                        TempData["SuccessMessage"] = (alertsVm.Id != -1) ? "Message added" : "Message is not saved";
                    }
                }
                else
                {
                    var patient = new Patient()
                    {
                        Dob = alertsVm.PatientVM.Dob.EncryptAES256(),
                        Firstname = alertsVm.PatientVM.Firstname.EncryptAES256(),
                        Surname = alertsVm.PatientVM.Surname.EncryptAES256(),
                        Id = alertsVm.PatientVM.Id,
                        Message = alertsVm.AlertText,
                        OrganisationId = alertsVm.PatientVM.OrganisationId,
                        PatientId = alertsVm.PatientVM.PatientId,
                        PatientMessageId = alertsVm.PatientVM.PatientMessageId,
                    };

                    if (!User.IsInRole("Egton Engineer"))
                    {
                        patient.PatientMessageId = await _patientRepository.SavePatientMessage(patient);
                    }

                    TempData["SuccessMessage"] = (patient.PatientMessageId != -1) ? (alertsVm.PatientVM.PatientMessageId > 0) ? "Message edited" : "Message added" : "Message is not saved";
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                TempData["ErrorMessage"] = "Message is not saved";
                return PartialView("Error", new HandleErrorInfo(ex, "Alerts", "SaveAlert"));
            }
            return null;
        }

        [HttpGet]
        public async Task<ActionResult> SearchPatient(int organisationId, string filter)
        {
            var patientVmList = new List<PatientViewModel>();

            if (User.IsInRole("Egton Engineer"))
            {
                PatientViewModel patientVm = new PatientViewModel
                {
                    Dob = "01/01/1999",
                    Firstname = "Dummy",
                    Surname = "Patient1",
                    OrganisationId = organisationId,
                    PatientId = -1,
                    Id = -1
                };
                patientVmList.Add(patientVm);
                patientVm = new PatientViewModel
                {
                    Dob = "01/01/1990",
                    Firstname = "Dummy",
                    Surname = "Patient2",
                    OrganisationId = organisationId,
                    PatientId = -2,
                    Id = -2
                };
                patientVmList.Add(patientVm);
                patientVm = new PatientViewModel
                {
                    Dob = "01/01/1980",
                    Firstname = "Dummy",
                    Surname = "Patient3",
                    OrganisationId = organisationId,
                    PatientId = -3,
                    Id = -3
                };
                patientVmList.Add(patientVm);
            }
            else
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(filter.Trim()))
                        patientVmList = await new ApiHelper().GetMatchedPatients(organisationId, filter);
                    else
                        patientVmList = new List<PatientViewModel>();
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                }
            }

            return Json(patientVmList, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> IsPcsLocalConfiguration(int organisationId)
        {
            try
            {
                var data1 = await _organisationRepository.GetOrganisationsForDropDown();
                var result = data1.Where(o => o.Id == organisationId).Any(a => a.SystemTypeId == 6);
                if (!result)
                {
                    result = Config.IsEmisWebPortal ? data1.Where(o => o.Id == organisationId).Any(a => a.SystemTypeId != 1) : data1.Where(o => o.Id == organisationId).Any(a => a.SystemTypeId == 1);
                }
                return Json(new { success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}