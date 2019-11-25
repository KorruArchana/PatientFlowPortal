using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Models;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;

namespace EMIS.PatientFlow.Web.Controllers
{
    [OutputCache(Duration = 0)]
    [Authorize(Roles = "Practice Admin, EMIS Super User, Egton Engineer")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
		private readonly IMemberRepository _memberRepository;
		private readonly IOrganisationRepository _organisationRepository;
		public DepartmentController(
			IDepartmentRepository repository,
			IOrganisationRepository organisationRepository,
			IMemberRepository  memberRepository
			)
        {
            _repository = repository;
			_organisationRepository = organisationRepository;
			_memberRepository = memberRepository;
		}

		public async Task<ActionResult> Index()
		{
			if (!User.IsInRole("Practice Admin"))
			{
				return RedirectToAction("Index", "EgtonDepartment");
			}
			else
			{
				DepartmentListViewModel departmentListViewModel = new DepartmentListViewModel();
				try
				{
					var departmentList = await _repository.GetDepartments();
					List<DepartmentViewModel> departments = new List<DepartmentViewModel>();
					foreach (var item in departmentList)
					{
						DepartmentViewModel department = new DepartmentViewModel()
						{
							Id = item.Id,
							DepartmentName = item.DepartmentName,
							OrganisationId = item.OrganisationId,
							OrganisationName = item.OrganisationName,
							LinkCount = item.LinkCount,
							LinkedMessageCount = item.LinkedMessageCount
						};
						departments.Add(department);
					}
					departmentListViewModel.Departments = departments;
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				}
				return View("Index", departmentListViewModel);
			}
		}

		public async Task<ActionResult> AddDepartment()
		{
			DepartmentViewModel department = new DepartmentViewModel();
			try
			{
				List<Organisation> organisationList = await _organisationRepository.GetOrganisationsForDropDown();
				var organisations = organisationList.Select(org => new SelectListItem
				{
					Text = org.OrganisationName,
					Value = org.Id.ToString()
				});
				department.OrganisationsList = organisations.ToList();
				
			}
			catch(Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return View("AddEditDepartment", new DepartmentViewModel());
			}
			return PartialView("AddEditDepartment", department);
		}

		[HttpGet]
		public async Task<JsonResult> IsValidDepartmentName(string DepartmentName, int Id, int OrganisationId)
		{
			bool isNameValid = false;
			string errorMessage = string.Empty;
			try
			{
				if (DepartmentName != null)
				{
					if (OrganisationId > 0)
					{
						isNameValid = await _repository.ValidateDepartmentName(DepartmentName, Id, OrganisationId);
						if (!isNameValid)
						{
							errorMessage = "Department name already exists in the organisation!";
						}
					}
					else
					{
						errorMessage = "Selected organisation is not valid!";
					}
				}
				else
				{
					errorMessage = "Department name cannot be empty!";
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
			}
			TempData["DepartmentNameValidation"] = errorMessage;
			return Json(new { Result = isNameValid, Message = errorMessage }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public async Task<ActionResult> SaveDepartment(string DepartmentName,int Id,int OrganisationId)
		{
			try
			{
				var department = new Department()
				{
					DepartmentName = DepartmentName,
					OrganisationId = OrganisationId,
					Id = Id
				};

				if (Id > 0)
				{
					await _repository.UpdateDepartment(department);
					TempData["SuccessMessage"] = "Department edited";
				}
				else
				{
					Id = await _repository.AddDepartment(department);
					TempData["SuccessMessage"] = "Department added";
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				TempData["ErrorMessage"] = "Department is not saved";
			}
			return Json(Url.Action("Index", "Department"));
		}

		public async Task<ActionResult> DeleteDepartment(int departmentId)
		{
			try
			{
				await _repository.DeleteDepartment(departmentId);
				Session["DeleteMessage"] = "Department deleted";
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				Session["DeleteErrorMessage"] = "Department is not deleted";
			}
			return RedirectToAction("Index", "Department");
		}

		public async Task<ActionResult> EditDepartment(int departmentId)
		{
			var model = new DepartmentViewModel();
			try
			{
				Department department = await _repository.GetDepartmentDetails(departmentId);
				model.Id = department.Id;
				model.DepartmentName = department.DepartmentName;
				model.OrganisationId=department.OrganisationId;
				model.LinkCount=department.LinkCount;
				model.LinkedMessageCount=department.LinkedMessageCount;
				List<Organisation> organisationList = await _organisationRepository.GetOrganisationsForDropDown();
				var orgs = organisationList.Select(org => new SelectListItem
				{
					Text = org.OrganisationName,
					Value = org.Id.ToString(),
					Selected = org.Id == model.OrganisationId
				});
				model.OrganisationsList = orgs.ToList();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return View("AddEditDepartment", new DepartmentViewModel());
			}
			return PartialView("AddEditDepartment", model);
		}

		[HttpGet]
		public async Task<ActionResult> GetDepartmentMemberList(int OrganisationId)
		{
			List<DepartmentModel> listOfDeptModel = new List<DepartmentModel>();
			try
			{
				var membdata = await _memberRepository.GetMembersByOrganisationId(OrganisationId);

				var departments = await _repository.GetDepartmentList(OrganisationId);
				if (departments!=null)
				{
					foreach (var department in departments)
					{
						var dept = new DepartmentModel()
						{
							DepartmentName = department.DepartmentName,
							Id = department.Id
						};
						listOfDeptModel.Add(dept);
					}
				}

				foreach (var dept in listOfDeptModel)
				{
					var list = membdata.Where(a => a.DepartmentId == dept.Id).ToList();
					dept.MemberList = list;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return null;
			}

			return Json(listOfDeptModel, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public async Task<JsonResult> GetDepartmentsForOrganisation(int organisationId)
		{
			var model = new List<Department>();
			try
			{
				model = await _repository.GetDepartmentList(organisationId);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return null;
			}

			return Json(model, JsonRequestBehavior.AllowGet);
		}
	}
  }

