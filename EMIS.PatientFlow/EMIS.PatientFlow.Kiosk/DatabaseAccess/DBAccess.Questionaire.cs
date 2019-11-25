using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess
{
    public partial class DbAccess
    {
        public List<Questionnaire> GetQuestionnairesByType(bool isAnonymous)
        {
            const string spQuestionnairebytypeList = "[PatientFlow].[GetQuestionnairesByType]";
            var questionnaires = new List<Questionnaire>();
            try
            {
                DbManager.Open();

                SqlCommand spCommand = DbManager.GetSprocCommand(spQuestionnairebytypeList);

                spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));
                spCommand.Parameters.Add(DbManager.CreateParameter("@IsAnonymous", isAnonymous));
                spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));

                using (SqlDataReader dr = spCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var questionnaire = new Questionnaire()
                        {
                            Id = dr["QuestionnaireId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["QuestionnaireId"]),
                            Title = dr["Title"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Title"]),
                            IsAnonymous = dr["IsAnonymous"] != DBNull.Value && Convert.ToBoolean(dr["IsAnonymous"]),
                            CreateConsultation = dr["CreateConsultation"] != DBNull.Value && Convert.ToBoolean(dr["CreateConsultation"]),
                            Frequency = dr["Frequency"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Frequency"])
                        };
                        questionnaires.Add(questionnaire);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }

            return questionnaires;
        }

        public List<Questions> GetQuestionsByQuestionnaire(int questionnaireId, int questionOrder)
        {
            const string spQuestionsbyquestionnaireList = "[PatientFlow].[GetQuestionsByQuestionnaire]";
            var questions = new List<Questions>();
            try
            {
                DbManager.Open();
                SqlCommand spCommand = DbManager.GetSprocCommand(spQuestionsbyquestionnaireList);

                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaireId));
                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionOrder", questionOrder));

                using (SqlDataReader dr = spCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var question = new Questions()
                        {
                            Id = dr["QuestionId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["QuestionId"]),
                            QuestionText = dr["QuestionText"] == DBNull.Value ? string.Empty : Convert.ToString(dr["QuestionText"]),
                            AnswerControlType = dr["QuestionType"] == DBNull.Value ? string.Empty : Convert.ToString(dr["QuestionType"]),
                            QuestionOrder = dr["QuestionOrder"] == DBNull.Value ? 0 : Convert.ToInt32(dr["QuestionOrder"]),
                            Gender = dr["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Gender"]),
                            Age1 = dr["Age1"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Age1"]),
                            Age2 = dr["Age2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Age2"]),
                            Operation = (dr["Operation"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Operation"])).ParseEnum<AgeOperations>(true),
                            OptionCharLimit = dr["OptionCharLimit"] == DBNull.Value ? 200 : Convert.ToInt32(dr["OptionCharLimit"])
                        };
                        questions.Add(question);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }

            return questions;
        }

        public List<Questions> GetQuestionById(int questionId)
        {
            const string spQuestionsbyidList = "[PatientFlow].[GetQuestionById]";
            var questions = new List<Questions>();
            try
            {
                DbManager.Open();

                SqlCommand spCommand = DbManager.GetSprocCommand(spQuestionsbyidList);

                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionId", questionId));

                using (SqlDataReader dr = spCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var question = new Questions()
                        {
                            Id = dr["QuestionId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["QuestionId"]),
                            QuestionText = dr["QuestionText"] == DBNull.Value ? string.Empty : Convert.ToString(dr["QuestionText"]),
                            AnswerControlType = dr["QuestionType"] == DBNull.Value ? string.Empty : Convert.ToString(dr["QuestionType"]),
                            QuestionOrder = dr["QuestionOrder"] == DBNull.Value ? 0 : Convert.ToInt32(dr["QuestionOrder"]),
                            Gender = dr["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Gender"]),
                            Age1 = dr["Age1"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Age1"]),
                            Age2 = dr["Age2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Age2"]),
                            Operation = Convert.ToString(dr["Operation"]).ParseEnum<AgeOperations>(true),
                            OptionCharLimit = dr["OptionCharLimit"] == DBNull.Value ? 200 : Convert.ToInt32(dr["OptionCharLimit"])
                        };
                        questions.Add(question);
                    }  
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }

            return questions;
        }

        public Questions GetAnswerOptionsByQuestion(Questions question)
        {
            const string spGetansweroptionsbyquestion = "[PatientFlow].[GetAnswerOptionsByQuestion]";
            var answerOptions = new List<QuestionnaireAnswerOption>();
            try
            {
                DbManager.Open();

                SqlCommand spCommand = DbManager.GetSprocCommand(spGetansweroptionsbyquestion);

                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionId", question.Id));

                using (SqlDataReader dr = spCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var answerOption = new QuestionnaireAnswerOption()
                        {
                            AnswerOptionId = dr["OptionId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OptionId"]),
                            AnswerOptionText =
                                dr["OptionValue"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OptionValue"]),
                            NestedQuestion = dr["NestedQuestionId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NestedQuestionId"]),
                            OptionCode = dr["OptionCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OptionCode"]),
                            SnomedCode = dr["SnomedCode"] == DBNull.Value ? 0 : Convert.ToInt64(dr["SnomedCode"])

                        };
                        answerOptions.Add(answerOption);
                    }
                }

                question.AnswerOptions = answerOptions;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }

            return question;
        }

        /// <summary>
        /// Method to get list of questionnaires by patient
        /// </summary>
        /// <returns>anonymous survey frequency</returns>
        public List<NonAnonymousSurveyFrequency> GetQuestionnairesByPatient()
        {
            const string spQuestionnairebypatientList = "[PatientFlow].[GetQuestionnairesByPatient]";
            var questionnaireFrequency = new List<NonAnonymousSurveyFrequency>();
            try
            {
                DbManager.Open();

                SqlCommand spCommand = DbManager.GetSprocCommand(spQuestionnairebypatientList);

                spCommand.Parameters.Add(DbManager.CreateParameter("@PatientId", GlobalVariables.ArrivedPatientDetails.BookedPatient.Id));
                spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));

                using (SqlDataReader dr = spCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var frequency = new NonAnonymousSurveyFrequency()
                        {
                            QuestionnaireId =
                                dr["QuestionnaireId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["QuestionnaireId"]),
                            PatientId = dr["PatientId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PatientId"]),
                            AccessedOn = Convert.ToDateTime(dr["AccessedOn"])
                        };
                        questionnaireFrequency.Add(frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }

            return questionnaireFrequency;
        }

        public void SaveQuestionniareFrequency(Questionnaire questionniare)
        {
            const string spQuestionnairefrequencySave = "[PatientFlow].[SaveQuestionniareFrequency]";
            try
            {
                DbManager.Open();
                SqlCommand spCommand = DbManager.GetSprocCommand(spQuestionnairefrequencySave);
                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionniare.Id));
                spCommand.Parameters.Add(DbManager.CreateParameter("@PatientId", GlobalVariables.ArrivedPatientDetails.BookedPatient.Id));
                spCommand.Parameters.Add(DbManager.CreateParameter("@AccessedOn", DateTime.Today));
                spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));

                spCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }
        }

        public void SaveAnonymousSurvey(List<AnonymousSurvey> surveyList)
        {
            const string spAnonymoussurveySave = "[PatientFlow].[SaveAnonymousSurvey]";
            try
            {
                var dtAnonymousSurveys = GetAnonymousSurveyDataTable(surveyList);
                DbManager.Open();

                SqlCommand spCommand = DbManager.GetSprocCommand(spAnonymoussurveySave);
                spCommand.Parameters.Add(DbManager.CreateParameter("@AnonymousSurvey", dtAnonymousSurveys, "PatientFlow.AnonymousSurvey"));

                spCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }
        }

        public DataTable GetAnonymousSurveyDataTable(List<AnonymousSurvey> surveyList)
        {
            var dtAnonymousSurveys = new DataTable();
	        dtAnonymousSurveys.Columns.Add("AnonymousSurveyId", typeof (int));
            dtAnonymousSurveys.Columns.Add("KioskId", typeof(string));
            dtAnonymousSurveys.Columns.Add("QuestionnaireId", typeof(int));
            dtAnonymousSurveys.Columns.Add("QuestionnaireTitle", typeof(string));
            dtAnonymousSurveys.Columns.Add("QuestionId", typeof(int));
            dtAnonymousSurveys.Columns.Add("QuestionText", typeof(string));
            dtAnonymousSurveys.Columns.Add("OptionId", typeof(string));
            dtAnonymousSurveys.Columns.Add("AnswerText", typeof(string));

	        int rowcount = 1;
            foreach (var item in surveyList)
            {
                dtAnonymousSurveys.Rows.Add(
					rowcount++,
                    item.KioskId,
                    item.QuestionnaireId,
                    item.QuestionnaireTitle,
                    item.QuestionId,
                    item.QuestionText,
                    item.OptionId,
                    item.AnswerText);
            }

            return dtAnonymousSurveys;
        }

		public void SaveQuestionInitialData(List<Questions> question)
		{
			try
			{
				DbManager.Open();

				var dtQuestion = new DataTable();
				dtQuestion.Columns.Add("QuestionId", typeof(int));
				dtQuestion.Columns.Add("QuestionnaireId", typeof(int));
				dtQuestion.Columns.Add("QuestionText", typeof(string));
				dtQuestion.Columns.Add("QuestionType", typeof(int));
				dtQuestion.Columns.Add("OptionCharLimit", typeof(int));
				dtQuestion.Columns.Add("Gender", typeof(string));
				dtQuestion.Columns.Add("QuestionOrder", typeof(int));
				dtQuestion.Columns.Add("Age1", typeof(int));
				dtQuestion.Columns.Add("Age2", typeof(int));
				dtQuestion.Columns.Add("Operation", typeof(string));

				foreach (var item in question)
				{
					dtQuestion.Rows.Add(
						item.QuestionId,
						item.QuestionnaireId,
						item.QuestionText,
						item.QuestionType,
						item.OptionCharLimit,
						item.Gender,
						item.Order,
						item.Age1,
						item.Age2,
						item.Operation);
				}

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveQuestionInitialData]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionList", dtQuestion, "PatientFlow.Question"));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}

		public void SaveQuestionOptionInitialData(List<QuestionOptionList> question)
		{
			try
			{
				DbManager.Open();

				var dtQuestionOption = new DataTable();
				dtQuestionOption.Columns.Add("QuestionId", typeof(int));
				dtQuestionOption.Columns.Add("OptionId", typeof(int));
				dtQuestionOption.Columns.Add("OptionValue", typeof(string));
				dtQuestionOption.Columns.Add("NestedQuestionId", typeof(int));
				dtQuestionOption.Columns.Add("OptionCode", typeof(string));
                dtQuestionOption.Columns.Add("SnomedCode", typeof(long));
                foreach (var item in question)
				{
					dtQuestionOption.Rows.Add(
						item.QuestionId,
						item.OptionId,
						item.QuestionOption,
						item.NestedQuestionId,
						item.QuestionOptionCode,
                        item.QuestionSnomedOptionCode);
				}

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveQuestionOptionInitialData]");
				spCommand.Parameters.Add(DbManager.CreateParameter(
					"@QuestionOptionList",
					dtQuestionOption,
					"PatientFlow.QuestionOption"));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}

		public void SaveQuestionnaireInitialData(List<Questionnaire> questionnaire, DateTime lastRowModifiedDate)
		{
			try
			{
				DbManager.Open();

				var dtQuestionnaire = new DataTable();
				dtQuestionnaire.Columns.Add("QuestionnaireId", typeof(int));
				dtQuestionnaire.Columns.Add("Title", typeof(string));
				dtQuestionnaire.Columns.Add("Frequency", typeof(int));
				dtQuestionnaire.Columns.Add("CreateConsultation", typeof(bool));
				dtQuestionnaire.Columns.Add("IsAnonymous", typeof(string));
				dtQuestionnaire.Columns.Add("OrganisationId", typeof(int));

				foreach (var item in questionnaire)
				{
					dtQuestionnaire.Rows.Add(
						item.Id,
						item.Title,
						item.Frequency,
						item.CreateConsultation,
						item.IsAnonymous,
						item.OrganisationId);
				}

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveQuestionnaireInitialData]");
				spCommand.Parameters.Add(DbManager.CreateParameter(
					"@QuestionnaireList",
					dtQuestionnaire,
					"PatientFlow.Questionnaire"));
				spCommand.Parameters.Add(DbManager.CreateParameter("@LastRowModifiedDate", lastRowModifiedDate));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}
	}
}
