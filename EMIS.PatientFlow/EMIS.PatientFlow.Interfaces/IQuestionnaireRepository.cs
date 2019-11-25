using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface IQuestionnaireRepository
    {
        List<Questionnaire> GetQuestionnaireList(string productKey, DateTime syncedRowModifiedDate, out DateTime lastRowModifiedDate);
		List<Questionnaire> GetQuestionnaires();
		List<Questionnaire> GetQuestionnairesByUser();
		Questionnaire GetQuestionnaireDetails(int questionnaireId);
        List<Questionnaire> GetSurveyQuestionnaire(int organisationId);
        List<Questionnaire> GetSurveyQuestionnaireForKiosk(int kioskId, int organisationId);
        List<Questionnaire> GetQuestionnaireListForKiosk(int kioskId, int organisationId);
		List<Questions> GetQuestionListForQuestionnaire(int questionnaireId);
		List<Questionnaire> GetQuestionnairesForOrganisation(int organisationId);
		int AddQuestionnaireDetails(Questionnaire questionnaire);
		int UpdateQuestionnaireDetails(Questionnaire questionnaire);
		int DeleteQuestionnaireDetails(int questionnaireid);
		Questionnaire SetPublish(bool status, int questionnaireId);

	}
}
