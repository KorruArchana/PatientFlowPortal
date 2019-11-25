using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Controllers
{
	[OutputCache(Duration = 0)]
	[Authorize(Roles = "EMIS Super User, Egton Engineer")]
	public class EgtonDepartmentController : Controller
	{
		private readonly IDepartmentRepository _repository;
		public EgtonDepartmentController(IDepartmentRepository repository)
		{
			_repository = repository;
		}

		public async Task<ActionResult> Index()
		{
			DepartmentListViewModel departmentListViewModel = new DepartmentListViewModel();
			departmentListViewModel = await GetDepartments();
			departmentListViewModel.OrganisationNameList = departmentListViewModel.Departments.Select(m => m.OrganisationName).Distinct().ToList();
			return View("Index", departmentListViewModel);
		}

		public async Task<ActionResult> GetDepartmentsData()
		{
			var model = new DepartmentListViewModel();
			try
			{
				model = await GetDepartments();
				List<DepartmentViewModel> DepartmentData = model.Departments;

				model.OrganisationNameList = DepartmentData.Select(m => m.OrganisationName).Distinct().ToList();

				int sortColumn = -1;
				string sortDirection = "asc";
				var result = new List<DepartmentViewModel>();
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
							result = sortDirection == "desc" ? DepartmentData.OrderByDescending(m => m.DepartmentName).ToList()
													 : DepartmentData.OrderBy(m => m.DepartmentName).ToList();

							break;
						case 2:
							result = sortDirection == "desc" ? DepartmentData.OrderByDescending(m => m.OrganisationName).ToList()
													 : DepartmentData.OrderBy(m => m.OrganisationName).ToList();

							break;
						default:
							result = DepartmentData.OrderBy(m => m.DepartmentName).ToList();
							break;
					}
				}

				string departmentNameFilter = Request.QueryString["columns[1][search][value]"] ?? Request.QueryString["columns[1][search][value]"].ToString();
				string organisationNameFilter = Request.QueryString["columns[2][search][value]"] ?? Request.QueryString["columns[2][search][value]"].ToString();
				organisationNameFilter = Regex.Replace(organisationNameFilter, "[;.^#$]+", "", RegexOptions.Compiled);
				organisationNameFilter = Regex.Replace(organisationNameFilter, "[|]+", ",", RegexOptions.Compiled);

				if (!string.IsNullOrWhiteSpace(departmentNameFilter))
				{
					result = result.Where(x => x.DepartmentName.IndexOf(departmentNameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(organisationNameFilter))
				{
					string[] organisationNameFilters = organisationNameFilter.Split(',');
					// Has to ignore case here
					result = result.Where(x => Array.IndexOf(organisationNameFilters, x.OrganisationName) >= 0).ToList();
				}

				model.draw = int.Parse(Request.QueryString["draw"]);
				int start = int.Parse(Request.QueryString["start"]);
				int length = int.Parse(Request.QueryString["length"]);

				model.data = result.Skip(start).Take(length).ToArray();
				model.recordsTotal = DepartmentData.Count();
				model.recordsFiltered = result.Count();
				return Json(model, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return (Json(model, JsonRequestBehavior.AllowGet));
			}
		}

		private async Task<DepartmentListViewModel> GetDepartments()
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
			return departmentListViewModel;
		}
	}
}