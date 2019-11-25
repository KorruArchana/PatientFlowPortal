using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class QuestionnaireRepository : BaseRepository, IQuestionnaireRepository
    {
		//Has to check and made be need to update.. Can remve that pagesize and update procedure.
		public List<Questionnaire> GetQuestionnaireList(string productKey, DateTime syncedRowModifiedDate, out DateTime lastRowModifiedDate)
        {
			try
			{
				return DbAccess.GetQuestionnaireList(productKey, syncedRowModifiedDate, out lastRowModifiedDate);
			}
			catch (Exception ex)
			{
				lastRowModifiedDate = new DateTime();
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Questionnaire>();
			}
        }

		public List<Questionnaire> GetQuestionnaires()
		{
			try
			{
				return DbAccess.GetQuestionnaires();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Questionnaire>();
			}
        }

		public List<Questionnaire> GetQuestionnairesByUser()
		{
			return DbAccess.GetQuestionnairesByUser(CurrentUser);
		}

        public Questionnaire GetQuestionnaireDetails(int questionnaireId)
        {
			try
			{
				Questionnaire questionnaire = DbAccess.GetQuestionnaireDetails(questionnaireId);
				return questionnaire;
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new Questionnaire();
			}
        }
		
		public List<Questionnaire> GetSurveyQuestionnaire(int organisationId)
		{
			try
			{
				return DbAccess.GetSurveyQuestionnaire(organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Questionnaire>();
			}
		}

		public List<Questionnaire> GetSurveyQuestionnaireForKiosk(int kioskId, int organisationId)
		{
			try
			{
				return DbAccess.GetSurveyQuestionnaireForKiosk(kioskId, organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Questionnaire>();
			}
		}

		public List<Questionnaire> GetQuestionnaireListForKiosk(int kioskId, int organisationId)
		{
			try
			{
				List<Questionnaire> questionnaires = DbAccess.GetQuestionnaireListForKiosk(kioskId, organisationId);
				foreach (Questionnaire questionnaire in questionnaires)
					questionnaire.Questions = GetQuestionListForQuestionnaire(questionnaire.Id);

				return questionnaires;
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Questionnaire>();
			}
		}

		public List<Questions> GetQuestionListForQuestionnaire(int questionnaireId)
		{
			try
			{
				return DbAccess.GetQuestionListForQuestionnaire(questionnaireId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Questions>();
			}
		}

		public List<Questionnaire> GetQuestionnairesForOrganisation(int organisationId)
		{
			try
			{
				return DbAccess.GetQuestionnairesForOrganisation(organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Questionnaire>();
			}
		}

		public int AddQuestionnaireDetails(Questionnaire questionnaire)
		{
			int questionnaireId = AddQuestionnaire(questionnaire);
			foreach (var question in questionnaire.Questions)
			{
				AddQuestionDetails(question, questionnaireId);
			}

			return questionnaireId;
		}

		private int AddQuestionnaire(Questionnaire questionnaire)
		{
			try
			{
				return DbAccess.AddQuestionnaireDetails(questionnaire, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		private int AddQuestionDetails(Questions question, int questionnaireId)
		{
			return DbAccess.AddQuestionDetails(question, questionnaireId);
		}

		public int UpdateQuestionnaireDetails(Questionnaire questionnaire)
		{
			int questionnaireId = UpdateQuestionnaire(questionnaire);

			foreach (var question in questionnaire.Questions)
			{
				if (question.QuestionId > 0)
				{
					UpdateQuestionDetails(question);
				}
				else
				{
					AddQuestionDetails(question, questionnaireId);
				}
			}
			foreach (int deletedquestionId in questionnaire.deletedQuestions)
			{
				DeleteQuestionDetails(deletedquestionId);
			}
			return questionnaireId;
		}

		private int UpdateQuestionnaire(Questionnaire questionnaire)
		{
			try
			{
				return DbAccess.UpdateQuestionnaireDetails(questionnaire, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		private int UpdateQuestionDetails(Questions question)
		{
			try
			{
				return DbAccess.UpdateQuestionDetails(question);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		private int DeleteQuestionDetails(int question)
		{
			try
			{
				return DbAccess.DeleteQuestionDetails(question);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public int DeleteQuestionnaireDetails(int questionnaireid)
		{
			try
			{
				return DbAccess.DeleteQuestionnaireDetails(questionnaireid);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public Questionnaire SetPublish(bool status, int questionnaireId)
		{
			return DbAccess.SetPublish(status, questionnaireId, CurrentUser);
		}
	}
}
