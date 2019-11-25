using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IQuestionnaireRepository
    {
        Task<List<Questionnaire>> GetQuestionnaires();
		Task<int> AddQuestionnaire(Questionnaire questionnaire);
		Task<Questionnaire> QuestionnaireDetails(int questionnaireId);
		Task<int> UpdateQuestionnaire(Questionnaire questionnaire);
		Task<int> DeleteQuestionniare(int questionnaireId, int organisationId);
		Task<bool> SetPublish(bool status, int QuestionnaireId, int organisationId);

		//Being used in Kiosk Controller. Has to check and Need to update/delete them
		Task<List<Questionnaire>> GetQuestionnairesForOrganisation(int organisationId); 
		Task<List<Questionnaire>> GetSurveyQuestionnaire(int organisationId);
		Task<List<Questionnaire>> GetSurveyQuestionnaireForKiosk(int kioskId, int organisationId);
		Task<List<Questionnaire>> GetQuestionnaireListForKiosk(int kioskId, int organisationId);
	}
}