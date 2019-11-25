using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace EMIS.PatientFlow.SyncService.Data.DataAccess
{
    public partial class DbAccess
    {       
        public void SaveQuestionnaire(Questionnaire questionnaire)
        {
            try
            {
                DbManager.Open();
                
                var dtQuestionIds = new DataTable();

                dtQuestionIds = CreateListOrderDataTable(questionnaire.SelectedQuestions);

                SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveQuestionnaire]");

                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaire.Id));
                spCommand.Parameters.Add(DbManager.CreateParameter("@Title", questionnaire.Title, 150));
                spCommand.Parameters.Add(DbManager.CreateParameter("@Frequency", questionnaire.Frequency));
                spCommand.Parameters.Add(DbManager.CreateParameter("@CreateConsultation", questionnaire.CreateConsultation));
                spCommand.Parameters.Add(DbManager.CreateParameter("@IsAnonymous", questionnaire.IsAnonymous));
                spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", questionnaire.OrganisationId));
                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionIdList", dtQuestionIds, "PatientFlow.ListWithOrder"));

                spCommand.ExecuteNonQuery();
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }
        }

        public void SaveQuestion(Question question)
        {
            try
            {
                DbManager.Open();

                SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveQuestion]");

                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionId", question.QuestionId));
                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", question.QuestionnaireId));
                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionText", question.QuestionText, 1000));
                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionType", question.QuestionType));
                spCommand.Parameters.Add(DbManager.CreateParameter("@OptionCharLimit", question.OptionCharLimit));
                spCommand.Parameters.Add(DbManager.CreateParameter("@Gender", question.Gender, 200));
                spCommand.Parameters.Add(DbManager.CreateParameter("@Age1", question.Age1));
                spCommand.Parameters.Add(DbManager.CreateParameter("@Age2", question.Age2));
                spCommand.Parameters.Add(DbManager.CreateParameter("@Operation", question.Operation, 200));
                int updateResult = Convert.ToInt32(spCommand.ExecuteScalar());
                if (question.QuestionOptions != null)
                {
                    if (question.QuestionOptions.Count > 0 && updateResult > 0)
                    {
                       InsertQuestionAnswerOptions(question.QuestionOptions, question.QuestionId);
                    }
                }
            }
            finally
            {
                //Close connection
                DbManager.Close();
            }
        }

        public void SaveQuestionInitialData(List<Question> question)
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

        public void DeleteQuestion(int questionId)
        {
            try
            {
                DbManager.Open();

                SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteQuestion]");

                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionId", questionId));
                spCommand.ExecuteScalar();
            }
            finally
            {
                DbManager.Close();
            }
        }

        public void DeleteQuestionnaire(int questionnaireId)
        {
            try
            {
                DbManager.Open();

                SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteQuestionnaire]");
                spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaireId));
                spCommand.ExecuteScalar();
            }
            finally
            {
                DbManager.Close();
            }
        }

        private void InsertQuestionAnswerOptions(
            List<QuestionOptionList> options,
            int questionId)
        {
            DataTable dtOptions = GetOptionsDataTable(options, questionId);

            SqlCommand command = DbManager.GetSprocCommand("[PatientFlow].[AddQuestionAnswerOptions]");

            command.CommandType = CommandType.StoredProcedure;
            command.UpdatedRowSource = UpdateRowSource.None;

            command.Parameters.Add("@QuestionId", SqlDbType.Int, 4, dtOptions.Columns[0].ColumnName);
            command.Parameters.Add("@OptionValue", SqlDbType.VarChar, 100, dtOptions.Columns[1].ColumnName);
            command.Parameters.Add("@OptionCode", SqlDbType.VarChar, 100, dtOptions.Columns[2].ColumnName);
            command.Parameters.Add("@NestedQuestionId", SqlDbType.Int, 100, dtOptions.Columns[3].ColumnName);
            command.Parameters.Add("@OptionId", SqlDbType.Int, 100, dtOptions.Columns[4].ColumnName);

            var adpt = new SqlDataAdapter();
            adpt.InsertCommand = command;
            adpt.UpdateBatchSize = options.Count();
            adpt.Update(dtOptions);
        }

        private DataTable GetOptionsDataTable(List<QuestionOptionList> options, int questionId)
        {
            var dtOptions = new DataTable();
            dtOptions.Columns.Add("QuestionId", typeof(int));
            dtOptions.Columns.Add("OptionValue", typeof(string));
            dtOptions.Columns.Add("OptionCode", typeof(string));
            dtOptions.Columns.Add("NestedQuestionId", typeof(int));
            dtOptions.Columns.Add("OptionId", typeof(int));

            foreach (QuestionOptionList itm in options)
            {
                DataRow drOptions = dtOptions.NewRow();
                drOptions["QuestionId"] = questionId;
                drOptions["OptionValue"] = itm.QuestionOption.Trim();
                drOptions["OptionCode"] = itm.QuestionOptionCode == null ? null : itm.QuestionOptionCode.Trim();
                drOptions["NestedQuestionId"] = itm.NestedQuestionId;
                drOptions["OptionId"] = itm.OptionId;
                dtOptions.Rows.Add(drOptions);
            }

            return dtOptions;
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

        public DataTable CreateListOrderDataTable(List<int> entitylist)
        {
            var dtlist = new DataTable("ListWithOrder");
            int i = 1;
            dtlist.Columns.Add("Id", typeof(int));
            dtlist.Columns.Add("ListOrder", typeof(int));
            DataRow dr;
            if (entitylist != null && entitylist.Count > 0)
            {
                foreach (var item in entitylist)
                {
                    dr = dtlist.NewRow();

                    dr["Id"] = item;
                    dr["ListOrder"] = i;
                    dtlist.Rows.Add(dr);
                    i++;
                }
            }

            return dtlist;
        }
    }
}
