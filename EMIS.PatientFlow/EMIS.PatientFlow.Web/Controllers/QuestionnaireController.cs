using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using Newtonsoft.Json;

namespace EMIS.PatientFlow.Web.Controllers
{
	[OutputCache(Duration = 0)]
    [Authorize(Roles = "Practice Admin, EMIS Super User, Egton Engineer")]
    public class QuestionnaireController : Controller
    {
        private readonly IQuestionnaireRepository _repository;
        private readonly IOrganisationRepository _orgrepository;
		private readonly IKioskRepository _kioskRepository;

		public QuestionnaireController(IQuestionnaireRepository repository, IOrganisationRepository orgrepository, IKioskRepository kioskRepository)
		{
			_repository = repository;
			_orgrepository = orgrepository;
			_kioskRepository = kioskRepository;
		}

		public async Task<ActionResult> Index()
		{
			if (!User.IsInRole("Practice Admin"))
			{
				return RedirectToAction("Index", "EgtonQuestionnaire");
			}
			else
			{
				var model = new QuestionnaireListingViewModel();
				try
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
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
					return null;
				}
				return PartialView("Index", model);
			}
		}

		public async Task<ActionResult> AddQuestionnaire()
		{
			QuestionnaireViewModel questionnaireVm = new QuestionnaireViewModel();
			questionnaireVm.Questions = new List<Questions>();
			questionnaireVm.LinkedKiosk = new List<int>();

			var organisationsList = await _orgrepository.GetOrganisationsForDropDown();
            var organisationList = new List<SelectListItem>();
            if (organisationsList != null)
            {
                organisationList = organisationsList.Select(o => new SelectListItem
                {
                    Text = o.OrganisationName,
                    Value = o.Id.ToString()
                }).ToList();
            }
            questionnaireVm.OrganisationList = organisationList;
			return PartialView("AddEditQuestionnaire", questionnaireVm);
		}

		public async Task<ActionResult> EditQuestionnaire(int questionnaireId)
		{
			Questionnaire questionnaire = await _repository.QuestionnaireDetails(questionnaireId);
			var questionnaireVm = new QuestionnaireViewModel()
			{
				Frequency = questionnaire.Frequency,
				Title = questionnaire.Title,
				CreateConsultation = questionnaire.CreateConsultation,
				QuestionnaireId = questionnaire.Id,
				Isanonymous = questionnaire.IsAnonymous,
				IsActive = questionnaire.IsActive,
				OrganisationId = questionnaire.OrganisationId,
				LinkedKiosk = questionnaire.LinkedKiosk
			};
			questionnaireVm.IsSnomedCode = IsSnomedCode(questionnaire.QuestionOptions);
			questionnaireVm.KioskList = new List<SelectListItem>();
			var organisationsList = await _orgrepository.GetOrganisationsForDropDown();
            var organisationList = new List<SelectListItem>();
            if (organisationsList != null)
            {
                organisationList = organisationsList.Select(o => new SelectListItem
                {
                    Text = o.OrganisationName,
                    Value = o.Id.ToString(),
                    Selected = o.Id == questionnaireVm.OrganisationId
                }).ToList();
            }
            questionnaireVm.OrganisationList = organisationList;
            var kiosklst = await _kioskRepository.GetKioskDetailListForOrganisation(questionnaireVm.OrganisationId);

			var klist = new List<SelectListItem>();

			klist.AddRange(kiosklst.Select(
				kiosk => new SelectListItem
				{
					Text = kiosk.KioskName,
					Value = kiosk.Id.ToString(),
					Selected = questionnaire.LinkedKiosk == null ? false : questionnaire.LinkedKiosk.Contains(kiosk.Id)
				}));

			questionnaireVm.KioskList = klist;
			questionnaireVm.Questions = questionnaire.Questions.OrderBy(x => x.Order).ToList();
			if (questionnaire.QuestionOptions != null)
			{
				foreach (var question in questionnaireVm.Questions)
				{
					question.QuestionOptions = questionnaire.QuestionOptions.Where(x => x.QuestionId == question.Id).ToList();
					question.QuestionOptionsList = JsonConvert.SerializeObject(question.QuestionOptions);
				}
			}
			return PartialView("AddEditQuestionnaire", questionnaireVm);
		}

		private bool IsSnomedCode(List<QuestionOptionList> questionOptions)
		{
			var isSnomedCount = questionOptions.Where(x => x.QuestionSnomedOptionCode > 0).Count();
			return isSnomedCount>0 ? true:false;
		}

		[HttpPost]
		public async Task<ActionResult> SaveQuestionnaire(QuestionnaireViewModel questionnaireVm)
		{
			try
			{
				questionnaireVm.Questions = questionnaireVm.QuestionsListData.ConvertFromJsonString<List<Questions>>();
				if (questionnaireVm.Questions != null)
				{
					questionnaireVm.Questions.ForEach(x => x.QuestionOptions = (x.QuestionOptionsList.ConvertFromJsonString<List<QuestionOptionList>>()));
				}
				else
				{
					questionnaireVm.Questions = new List<Questions>();
				}

				var saveQuestionnaire = new Questionnaire()
				{
					Id = questionnaireVm.QuestionnaireId,
					Frequency = questionnaireVm.Frequency ?? 0,
					Title = questionnaireVm.Title,
					CreateConsultation = !questionnaireVm.Isanonymous,
					IsAnonymous = questionnaireVm.Isanonymous,
					IsActive = questionnaireVm.IsActive,
					OrganisationId = questionnaireVm.OrganisationId,
					Questions = questionnaireVm.Questions,
					LinkedKiosk = questionnaireVm.LinkedKiosk
				};
				if (questionnaireVm.QuestionnaireId > 0)
				{
					string stringifiedDeletedQuestions = questionnaireVm.StringifiedDeletedQuestions;
					
					List<string> deletedQuestion = Regex.Split(stringifiedDeletedQuestions, @"[^0-9\.]+").Where(x => !string.IsNullOrEmpty(x)).ToList();
					List<int> intlist = deletedQuestion.ConvertAll(int.Parse);
					saveQuestionnaire.deletedQuestions = intlist;
				}

				if (saveQuestionnaire.Id > 0)
				{
					await _repository.UpdateQuestionnaire(saveQuestionnaire);
					TempData["successMessage"] = (saveQuestionnaire.Id != -1) ? "Questionnaire edited" : "Questionnaire is not updated";
				}
				else
				{
					int result = await _repository.AddQuestionnaire(saveQuestionnaire);
					TempData["successMessage"] = result > 0 ? "Questionnaire added" : "Questionnaire is not saved";
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				TempData["ErrorMessage"] = "Questionnaire is not saved successfully";
			}
			return null;
		}

		[HttpGet]
		public async Task<ActionResult> DeleteQuestionnaire(int questionnaireId, int organisationId)
		{
			try
			{
				await _repository.DeleteQuestionniare(
					questionnaireId,
					organisationId);
				Session["DeleteMessage"] = "Questionnaire deleted";
				return RedirectToAction("Index", "Questionnaire");
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				Session["DeleteErrorMessage"] = "Questionnaire is not deleted";
				return null;
			}
		}

		public async Task<ActionResult> SetPublish(bool status, int QuestionnaireId, int organisationId)
		{
			try
			{
				bool publish=await _repository.SetPublish(status, QuestionnaireId, organisationId);
                return Json(new { Success = publish}, JsonRequestBehavior.AllowGet);
            }
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(new { Success = false}, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> IsEmisWebOrganisation(int organisationId)
        {
            try
            {
                var data1 = await _orgrepository.GetOrganisationsForDropDown();
                return Json(new { success = data1.Where(o => o.Id == organisationId).Any(a => a.SystemTypeId == 1) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}