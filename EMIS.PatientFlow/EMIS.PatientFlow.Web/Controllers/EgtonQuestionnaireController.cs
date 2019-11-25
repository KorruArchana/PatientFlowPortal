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
	public class EgtonQuestionnaireController : Controller
	{

		private readonly IQuestionnaireRepository _repository;
		public EgtonQuestionnaireController(IQuestionnaireRepository repository)
		{
			_repository = repository;
		}

		public async Task<ActionResult> Index()
		{
			var model = new QuestionnaireListingViewModel();
			try
			{
				await GetQuestionnaires(model);
				model.OrganisationNameList = model.QuestionnaireList.Select(m => m.OrganisationName).Distinct().ToList();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return null;
			}
			return PartialView("Index", model);
		}

		public async Task<ActionResult> GetQuestionnairesData()
		{
			var model = new QuestionnaireListingViewModel();
			try
			{
				await GetQuestionnaires(model);
				List<QuestionnaireViewModel> questionnaireData = model.QuestionnaireList;

				model.OrganisationNameList = questionnaireData.Select(m => m.OrganisationName).Distinct().ToList();

				int sortColumn = -1;
				string sortDirection = "asc";
				var result = new List<QuestionnaireViewModel>();
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
							result = sortDirection == "desc" ? questionnaireData.OrderByDescending(m => m.Title).ToList()
													 : questionnaireData.OrderBy(m => m.Title).ToList();

							break;
						case 2:
							result = sortDirection == "desc" ? questionnaireData.OrderByDescending(m => m.Frequency).ToList()
													 : questionnaireData.OrderBy(m => m.Frequency).ToList();

							break;
						case 3:
							result = sortDirection == "desc" ? questionnaireData.OrderByDescending(m => m.OrganisationName).ToList()
													 : questionnaireData.OrderBy(m => m.OrganisationName).ToList();

							break;
						default:
							result = questionnaireData.OrderBy(m => m.Title).ToList();
							break;
					}
				}

				string titleFilter = Request.QueryString["columns[1][search][value]"] ?? Request.QueryString["columns[1][search][value]"].ToString();
				string frequencyFilter = Request.QueryString["columns[2][search][value]"] ?? Request.QueryString["columns[2][search][value]"].ToString();
				string organisationNameFilter = Request.QueryString["columns[3][search][value]"] ?? Request.QueryString["columns[3][search][value]"].ToString();
				organisationNameFilter = Regex.Replace(organisationNameFilter, "[;.^#$]+", "", RegexOptions.Compiled);
				organisationNameFilter = Regex.Replace(organisationNameFilter, "[|]+", ",", RegexOptions.Compiled);

				if (!string.IsNullOrWhiteSpace(titleFilter))
				{
					result = result.Where(x => x.Title.IndexOf(titleFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(frequencyFilter))
				{
					result = result.Where(x => x.Frequency.ToString().IndexOf(frequencyFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
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
				model.recordsTotal = questionnaireData.Count();
				model.recordsFiltered = result.Count();
				return Json(model, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return (Json(model, JsonRequestBehavior.AllowGet));
			}
		}

		private async Task GetQuestionnaires(QuestionnaireListingViewModel model)
		{
			List<Questionnaire> questionnairelist = await _repository.GetQuestionnaires();
			model.QuestionnaireList = questionnairelist.Select(
				quest => new QuestionnaireViewModel
				{
					Title = quest.Title,
					Frequency = quest.Frequency,
					QuestionnaireId = quest.Id,
					Isanonymous = quest.IsAnonymous,
					LinkCount = quest.LinkCount,
					OrganisationId = quest.OrganisationId,
					OrganisationName = quest.OrganisationName,
					IsActive = quest.IsActive
				}).ToList();
		}
	}
}