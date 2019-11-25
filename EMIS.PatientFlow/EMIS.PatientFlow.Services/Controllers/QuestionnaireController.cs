using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Services.Helper;
using EMIS.PatientFlow.Services.Hubs;

namespace EMIS.PatientFlow.Services.Controllers
{
	[Authorize(Roles = "Practice Admin, EMIS Super User, Egton Engineer")]
	public class QuestionnaireController : ApiController
    {
        private readonly SyncHub _syncHub;
        private readonly ILoggerRepository _logger;
        private readonly IQuestionnaireRepository _repository;
		private readonly IOrganisationRepository _orgRepository;
		private readonly KioskHub _kioskHub;

		public QuestionnaireController(
			IQuestionnaireRepository questionnaireRepository,
			ILoggerRepository loggerRepository,
			SyncHub syncHub,
			IOrganisationRepository organisationRepository,
			KioskHub kioskHub)
		{
			_logger = loggerRepository;
			_syncHub = syncHub;
			_repository = questionnaireRepository;
			_orgRepository = organisationRepository;
			_kioskHub = kioskHub;
		}

		[AcceptVerbs("GET", "POST")]
        public int AddQuestionnaire([FromBody]string value)
        {
            int result;
            try
            {
               var questionnaire = JSONHelper.Deserialize<Questionnaire>(value);

                result = _repository.AddQuestionnaireDetails(questionnaire);
                questionnaire.Id = result;
                _syncHub.SaveQuestionnaire(questionnaire);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return result;
        }

        public List<Questionnaire> GetQuestionnaireForKiosk(int kioskId, int organisationId)
        {
            try
            {
                List<Questionnaire> questionnaires = _repository.GetQuestionnaireListForKiosk(kioskId, organisationId);
                return questionnaires;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

		public List<Questionnaire> GetQuestionnaires()
		{
			try
			{
				if (!HttpContext.Current.User.IsInRole("Practice Admin"))
				{
					return _repository.GetQuestionnaires();
				}
				else
				{
					return _repository.GetQuestionnairesByUser();
				}
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

		public List<Questionnaire> GetSurveyQuestionnaireForKiosk(int kioskId, int organisationId)
        {
            List<Questionnaire> questionnaireList;
            try
            {
                questionnaireList = _repository.GetSurveyQuestionnaireForKiosk(kioskId, organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return questionnaireList;
        }

        public List<Questionnaire> GetSurveyQuestionnaire(int organisationId)
        {
            List<Questionnaire> questionnaireList;
            try
            {
                questionnaireList = _repository.GetSurveyQuestionnaire(organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return questionnaireList;
        }

        public Questionnaire GetQuestionnaireDetails(int questionnaireId)
        {
            Common.Validations.ArgumentValidator.IsNegativeOrZero(questionnaireId, "questionnaireId");
            try
            {
                Questionnaire questionnaire = _repository.GetQuestionnaireDetails(questionnaireId);
                return questionnaire;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

		public int UpdateQuestionnaireDetails([FromBody]string value)
		{
			int result;
			try
			{
				var questionnaire = JSONHelper.Deserialize<Questionnaire>(value);
				result = _repository.UpdateQuestionnaireDetails(questionnaire);
				_syncHub.SaveQuestionnaire(questionnaire);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return result;
		}

		[AcceptVerbs("GET")]
		public int DeleteQuestionnaireDetails(int questionnaireId, int organisationId)
		{
			Common.Validations.ArgumentValidator.IsNegativeOrZero(questionnaireId, "questionnaireId");
			Common.Validations.ArgumentValidator.IsNegativeOrZero(organisationId, "organisationId");
			int result;
			try
			{
				result = _repository.DeleteQuestionnaireDetails(questionnaireId);
				_syncHub.DeleteQuestionnaire(questionnaireId, organisationId);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return result;
		}

        public List<Questionnaire> GetQuestionnairesForOrganisation(int organisationId)
        {
            List<Questionnaire> questionnaireList;

            try
            {
                questionnaireList = _repository.GetQuestionnairesForOrganisation(organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return questionnaireList;
        }

		[AcceptVerbs("GET")]
		public bool SetPublish(bool status, int questionnaireId, int organisationId)
		{
			try
			{
				Questionnaire questionnaire = _repository.SetPublish(status, questionnaireId);
				string organisationName = _orgRepository.GetOrganisationDetails(organisationId).OrganisationName;
				_kioskHub.SetPublish(status, questionnaireId, organisationName);
				return true;
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

	}
}
