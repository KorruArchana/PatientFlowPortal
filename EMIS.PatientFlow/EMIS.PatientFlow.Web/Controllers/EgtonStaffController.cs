using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
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
	public class EgtonStaffController : Controller
    {
		private readonly IMemberRepository _repository;

		public EgtonStaffController(IMemberRepository repository)
		{
			_repository = repository;
		}

		public async Task<ActionResult> Index()
		{
			var model = new DepartmentViewModel();
			try
			{
				var data = await _repository.GetMembers();
				model.MemberList = data.ToList();
				model.DepartmentNameList = model.MemberList.Select(m => m.DepartmentName).Distinct().ToList();
				model.OrganisationNameList = model.MemberList.Select(m => m.OrganisationName).Distinct().ToList();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
			}
			return PartialView("Index", model);
		}

		public async Task<ActionResult> GetMembersData()
		{
			var model = new DepartmentViewModel();
			try
			{
				var data = await _repository.GetMembers();
				List<Member> memberData = data.ToList();

				model.DepartmentNameList = memberData.Select(m => m.DepartmentName).Distinct().ToList();
				model.OrganisationNameList = memberData.Select(m => m.OrganisationName).Distinct().ToList();

				int sortColumn = -1;
				string sortDirection = "asc";
				var result = new List<Member>();
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
							result = sortDirection == "desc" ? memberData.OrderByDescending(m => m.FullName).ToList()
													 : memberData.OrderBy(m => m.FullName).ToList();

							break;
						case 3:
							result = sortDirection == "desc" ? memberData.OrderByDescending(m => m.SessionHolderId).ToList()
													 : memberData.OrderBy(m => m.SessionHolderId).ToList();

							break;
						case 4:
							result = sortDirection == "desc" ? memberData.OrderByDescending(m => m.OrganisationName).ToList()
													 : memberData.OrderBy(m => m.OrganisationName).ToList();

							break;
						case 5:
							result = sortDirection == "desc" ? memberData.OrderByDescending(m => m.DepartmentName).ToList()
													 : memberData.OrderBy(m => m.DepartmentName).ToList();

							break;
						default:
							result = memberData.OrderBy(m => m.FullName).ToList();
							break;
					}
				}

				string nameFilter = Request.QueryString["columns[1][search][value]"] ?? Request.QueryString["columns[1][search][value]"].ToString();
				string sessionHolderIdFilter = Request.QueryString["columns[3][search][value]"] ?? Request.QueryString["columns[3][search][value]"].ToString();
				string organisationNameFilter = Request.QueryString["columns[4][search][value]"] ?? Request.QueryString["columns[4][search][value]"].ToString();
				organisationNameFilter = Regex.Replace(organisationNameFilter, "[;.^#$]+", "", RegexOptions.Compiled);
				organisationNameFilter = Regex.Replace(organisationNameFilter, "[|]+", ",", RegexOptions.Compiled);
				string departmentNameFilter = Request.QueryString["columns[5][search][value]"] ?? Request.QueryString["columns[5][search][value]"].ToString();

				if (!string.IsNullOrWhiteSpace(nameFilter))
				{
					result = result.Where(x => x.Firstname.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0
								|| x.Surname.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0
								|| x.Title.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0
								).ToList();
				}
				if (!string.IsNullOrWhiteSpace(sessionHolderIdFilter))
				{
					result = result.Where(x => x.SessionHolderId.ToString().IndexOf(sessionHolderIdFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(organisationNameFilter))
				{
					string[] organisationNameFilters = organisationNameFilter.Split(',');
					// Has to ignore case here
					result = result.Where(x => Array.IndexOf(organisationNameFilters, x.OrganisationName) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(departmentNameFilter))
				{
					result = result.Where(x => x.DepartmentName.IndexOf(departmentNameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}

				model.draw = int.Parse(Request.QueryString["draw"]);
				int start = int.Parse(Request.QueryString["start"]);
				int length = int.Parse(Request.QueryString["length"]);

				model.data = result.Skip(start).Take(length).ToArray();
				model.recordsTotal = memberData.Count();
				model.recordsFiltered = result.Count();
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