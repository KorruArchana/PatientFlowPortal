using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class QuestionnaireRepository : BaseRepository, IQuestionnaireRepository
    {
		public async Task<List<Questionnaire>> GetQuestionnaires()
		{
			return await GetAsync<List<Questionnaire>>(
				string.Format("api/Questionnaire/GetQuestionnaires"));
		}

		public async Task<int> AddQuestionnaire(Questionnaire questionnaire)
		{
			var jsonquestionnaire = new JavaScriptSerializer().Serialize(questionnaire);
			return await PostAsJsonAsync<int>("api/Questionnaire/AddQuestionnaire", jsonquestionnaire);
		}

		public async Task<Questionnaire> QuestionnaireDetails(int questionnaireId)
		{
			return await GetAsync<Questionnaire>("api/Questionnaire/GetQuestionnaireDetails?questionnaireId=" + questionnaireId);
		}

		public async Task<int> UpdateQuestionnaire(Questionnaire questionnaire)
		{
			var jsonquestionnaire = new JavaScriptSerializer().Serialize(questionnaire);
			return await PostAsJsonAsync<int>("api/Questionnaire/UpdateQuestionnaireDetails", jsonquestionnaire);
		}

		public async Task<int> DeleteQuestionniare(int questionnaireId, int organisationId)
		{
			return await GetAsync<int>("api/Questionnaire/DeleteQuestionnaireDetails?questionnaireId=" + questionnaireId + "&organisationId=" + organisationId);
		}

		public async Task<bool> SetPublish(bool status, int questionnaireID, int organisationId)
		{
			return await GetAsync<bool>("api/Questionnaire/SetPublish?status=" + status + "&questionnaireID=" + questionnaireID + "&organisationId=" + organisationId);
		}

		public async Task<List<Questionnaire>> GetQuestionnairesForOrganisation(int organisationId)
		{
			return await GetAsync<List<Questionnaire>>("api/Questionnaire/GetQuestionnairesForOrganisation?organisationId=" + organisationId);
		}

		public async Task<List<Questionnaire>> GetSurveyQuestionnaire(int organisationId)
		{
			return await GetAsync<List<Questionnaire>>("api/Questionnaire/GetSurveyQuestionnaire?organisationId=" + organisationId);
		}

		public async Task<List<Questionnaire>> GetSurveyQuestionnaireForKiosk(int kioskId, int organisationId)
		{
			return await GetAsync<List<Questionnaire>>("api/Questionnaire/GetSurveyQuestionnaireForKiosk?kioskId=" + kioskId + "&organisationId=" + organisationId);
		}

		public async Task<List<Questionnaire>> GetQuestionnaireListForKiosk(int kioskId, int organisationId)
		{
			return await GetAsync<List<Questionnaire>>("api/Questionnaire/GetQuestionnaireForKiosk?KioskId=" + kioskId + "&organisationId=" + organisationId);
		}

	}
}