using System;
using System.Collections.Generic;
using EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess.Repository
{
    public class QuestionnaireRepository : BaseRepository, IQuestionnaireRepository
    {
        public void SaveQuestionnaire(Questionnaire questionnaire)
        {
            DbAccess.SaveQuestionnaire(questionnaire);
        }

        public void SaveQuestion(Question question)
        {
            DbAccess.SaveQuestion(question);
        }

        public void DeleteQuestion(int questionId)
        {
            DbAccess.DeleteQuestion(questionId);
        }

        public void DeleteQuestionnaire(int questionnaireId)
        {
            DbAccess.DeleteQuestionnaire(questionnaireId);
        }

        public void SaveQuestionInitialData(List<Question> question)
        {
            DbAccess.SaveQuestionInitialData(question);
        }

        public void SaveQuestionOptionInitialData(List<QuestionOptionList> questionOptions)
        {
            DbAccess.SaveQuestionOptionInitialData(questionOptions);
        }

        public void SaveQuestionnaireInitialData(List<Questionnaire> questionnaire, DateTime lastRowModifiedDate)
        {
            DbAccess.SaveQuestionnaireInitialData(questionnaire, lastRowModifiedDate);
        }
    }
}
