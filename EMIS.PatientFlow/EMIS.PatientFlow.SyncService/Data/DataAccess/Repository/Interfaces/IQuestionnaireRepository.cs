using System;
using System.Collections.Generic;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces
{
    interface IQuestionnaireRepository
    {
        void SaveQuestionnaire(Questionnaire questionnaire);
        void SaveQuestionnaireInitialData(List<Questionnaire> questionnaire, DateTime lastRowModifiedDate);
        void SaveQuestion(Question question);
        void SaveQuestionInitialData(List<Question> question);
        void SaveQuestionOptionInitialData(List<QuestionOptionList> questionOptions);
        void DeleteQuestion(int questionId);
        void DeleteQuestionnaire(int questionnaireId);
    }
}
