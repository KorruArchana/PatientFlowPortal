using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository
{
	public class QuestionnaireRepository : BaseRepository, IQuestionnaireRepository
	{
		public List<Questionnaire> GetQuestionnairesByType(bool isAnonymous)
		{
			return DbAccess.GetQuestionnairesByType(isAnonymous);
		}

		public List<Questions> GetQuestionsByQuestionnaire(int questionnaireId, int questionOrder)
		{
			return DbAccess.GetQuestionsByQuestionnaire(questionnaireId, questionOrder);
		}

		public Questions GetAnswerOptionsByQuestion(Questions question)
		{
			return DbAccess.GetAnswerOptionsByQuestion(question);
		}

		public List<Questions> GetQuestionById(int questionId)
		{
			return DbAccess.GetQuestionById(questionId);
		}

		public List<EMIS.PatientFlow.Kiosk.Model.NonAnonymousSurveyFrequency> GetQuestionnairesByPatient()
		{
			return DbAccess.GetQuestionnairesByPatient();
		}

		public void SaveQuestionniareFrequency(Questionnaire questionniare)
		{
			DbAccess.SaveQuestionniareFrequency(questionniare);
		}

		public void SaveAnonymousSurvey(List<EMIS.PatientFlow.Kiosk.Model.AnonymousSurvey> anonymousAnswerList)
		{
			DbAccess.SaveAnonymousSurvey(anonymousAnswerList);
		}

        public void SetPublish(bool status,int questionnaireId)
        {
            DbAccess.SetPublish(status,questionnaireId);
        }
    }
}
