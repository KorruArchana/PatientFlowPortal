using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces
{
    interface IQuestionnaireRepository
    {
        List<Questionnaire> GetQuestionnairesByType(bool isAnonymous);
        List<Questions> GetQuestionsByQuestionnaire(int questionnaireId, int questionOrder);
        Questions GetAnswerOptionsByQuestion(Questions question);
        List<Questions> GetQuestionById(int questionId);
        List<EMIS.PatientFlow.Kiosk.Model.NonAnonymousSurveyFrequency> GetQuestionnairesByPatient();
        void SaveQuestionniareFrequency(Questionnaire questionniare);
        void SaveAnonymousSurvey(List<EMIS.PatientFlow.Kiosk.Model.AnonymousSurvey> anonymousAnswerList);
        void SetPublish(bool status,int questionnaireId);
    }
}
