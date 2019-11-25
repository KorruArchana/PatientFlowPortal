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
    [Authorize(Roles = "Practice Admin, EMIS Super User, Egton Engineer")]
    public class StaffController : Controller
    {
        private readonly IMemberRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IAlertRepository _alertRepository;

        public StaffController(IMemberRepository repository, IDepartmentRepository departmentRepository, IOrganisationRepository organisationRepository, IAlertRepository alertRepository)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
            _organisationRepository = organisationRepository;
            _alertRepository = alertRepository;
        }

        public async Task<ActionResult> Index()
        {
            if (!User.IsInRole("Practice Admin"))
            {
                return RedirectToAction("Index", "EgtonStaff");
            }
            else
            {
                var model = new DepartmentViewModel();
                try
                {
                    var data = await _repository.GetMembers();
                    model.MemberList = data.ToList();
                    var data1 = await _organisationRepository.GetOrganisationsForDropDown();
                    model.IsPcsLocalLinked = data1.Count == 1 && data1.Any(a => a.SystemTypeId == 6);
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                }
                return PartialView("Index", model);
            }

        }

        [HttpGet]
        public async Task<ActionResult> AddMember()
        {
            var memberVm = new MemberViewModel();
            try
            {
                List<Organisation> organisationsList = await _organisationRepository.GetOrganisationsForDropDown();

                if (organisationsList.Count > 1)
                {

					organisationsList = Config.IsEmisWebPortal ?
									   organisationsList.Where(a => a.SystemTypeId == 1 || a.SystemTypeId == 7).ToList()
									   :
										organisationsList.Where(a => a.SystemTypeId == 2 || a.SystemTypeId == 7).ToList();

				}

	            List<SelectListItem> organisationList = organisationsList.Select(org => new SelectListItem
	            {
		            Text = org.OrganisationName,
		            Value = org.Id.ToString()
	            }).ToList();

	            memberVm.OrganisationList = organisationList;
                return View("AddEditMember", memberVm);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return View("AddEditMember", new MemberViewModel());
            }
        }

        public async Task<ActionResult> GetMemberDetails(int memberId)
        {
            var memberVm = new MemberViewModel();
            try
            {
                Member member = await _repository.GetMemberDetails(memberId);
                memberVm.FullName = member.Title + (String.IsNullOrWhiteSpace(member.Title) ? "" : ". ") + member.Firstname + " " + member.Surname;
                memberVm.OrganisationName = member.OrganisationName;
                memberVm.DepartmentId = member.DepartmentId;
                memberVm.Id = member.Id;
                memberVm.OrganisationId = member.OrganisationId;
                return View("AddEditMember", memberVm);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return View("AddEditMember", new MemberViewModel());
            }

        }

        public async Task<ActionResult> DeleteMember(int memberId, string memberName, int OrganisationId)
        {
            try
            {
                await _repository.DeleteMember(memberId, OrganisationId);
                Session["DeleteMessage"] = "Staff member removed";
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                Session["DeleteErrorMessage"] = "Staff member cannot be removed";
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UpdateMemberDepartment(int memberId, int departmentId, string FullName)
        {
            int result = -1;
            Member updateMember = new Member();
            try
            {
                updateMember.Id = memberId;
                updateMember.DepartmentId = departmentId;
                result = await _repository.UpdateMember(updateMember);
                TempData["SuccessMessage"] = "Staff member edited";
                return Json(new { success = true, Message = "Success" });
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                TempData["ErrorMessage"] = "Staff member is not updated";
                return Json(new { success = false, Message = result });
            }
        }

        public async Task<JsonResult> SaveSyncedMember(string jsonMemberList, int organisationId, int departmentId)
        {
            int result = -1;
            try
            {
                var memberList = jsonMemberList.ConvertFromJsonString<List<Member>>();
                foreach (var member in memberList)
                {
                    member.OrganisationId = organisationId;
                    member.DepartmentId = departmentId;
                }

                result = await _repository.AddSyncedMember(memberList);
                TempData["SuccessMessage"] = "Staff member added";
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                TempData["ErrorMessage"] = "Staff member not added";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SetDivert(bool status, int sessionHolderId, int organisationId)
        {

            try
            {
                await _repository.SetDivert(status, sessionHolderId, organisationId);

            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            return Json(!status, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> LinkMember(int organisationId)
        {
            var newMemberList = new List<Member>();
            try
            {
                List<Member> memberList = await new ApiHelper().GetMember(organisationId);

                List<int> sessionHolderIdList = await _repository.GetSessionHolderIdOrganisation(organisationId);
                newMemberList = memberList.Where(member => !sessionHolderIdList.Contains(member.SessionHolderId)).ToList();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
            }

            return Json(newMemberList, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAlerts(int MemberId, string MemberName)
        {
            ViewBag.MemberId = MemberId;
            ViewBag.MemberName = MemberName;
            List<Alert> alerts = new List<Alert>();
            try
            {
                alerts = (List<Alert>)await _alertRepository.GetAlertsByMember(MemberId);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
            }
            return View("MemberMessage", alerts);
        }

        public async Task<ActionResult> EnableMessages(int alertId, int memberId)
        {
            try
            {
                var result = await _repository.EnableMessageForMember(alertId, memberId);
                var message = result == true ? "Message enabled for staff member" : "Message enabling failed for staff member.";
                return Json(new { Success = true, Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                var message = "Message enabling failed for staff member.";
                return Json(new { Success = false, Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DestroySession()
        {
            Session["DeleteMessage"] = null;
            Session["DeleteErrorMessage"] = null;
            Session["RefreshMessage"] = null;
            Session["RefreshErrorMessage"] = null;
            return null;
        }
    }
}