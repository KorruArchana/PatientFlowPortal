using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public List<Questionnaire> GetQuestionnaireList(string productKey, DateTime syncedRowModifiedDate, out DateTime lastRowModifiedDate)
		{
			var questionnaireList = new List<Questionnaire>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetQuestionnaireList]", connection);
					var outputParameter = DbManager.CreateOutputParameter("@LastRowDate", SqlDbType.DateTime);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", productKey, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SyncedRowDate", syncedRowModifiedDate));
					spCommand.Parameters.Add(outputParameter);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var questionnaire = new Questionnaire
							{
								Id = Convert.ToInt32(dr["QuestionnaireId"]),
								Title = Convert.ToString(dr["Title"]),
								Frequency = TryParseInt(dr, "Frequency"),
								CreateConsultation = TryParseBoolean(dr, "CreateConsultation"),
								IsAnonymous = TryParseBoolean(dr, "IsAnonymous"),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								IsActive = TryParseBoolean(dr, "IsActive")
							};

							questionnaireList.Add(questionnaire);
						}
					}

					lastRowModifiedDate = outputParameter.Value != DBNull.Value ? Convert.ToDateTime(outputParameter.Value) : syncedRowModifiedDate;
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return questionnaireList;
		}

		public List<Questionnaire> GetQuestionnaires()
		{
			var questionnaireList = new List<Questionnaire>();
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetQuestionnaires]");
				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						var questionnaire = new Questionnaire
						{
							Id = Convert.ToInt32(dr["QuestionnaireId"]),
							Title = Convert.ToString(dr["Title"]),
							Frequency = TryParseInt(dr, "Frequency"),
							CreateConsultation = TryParseBoolean(dr, "CreateConsultation"),
							IsAnonymous = TryParseBoolean(dr, "IsAnonymous"),
							OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
							LinkCount = TryParseInt(dr, "LinkCount"),
							IsActive = TryParseBoolean(dr, "IsActive"),
							OrganisationName = Convert.ToString(dr["OrganisationName"])
						};
						questionnaireList.Add(questionnaire);
					}
				}
			}
			finally
			{
				DbManager.Close();
			}

			return questionnaireList;
		}

		public List<Questionnaire> GetSurveyQuestionnaire(int organisationId)
		{
			var questionnaireList = new List<Questionnaire>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSurveyQuestionnaire]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var questionnaire = new Questionnaire
							{
								Id = Convert.ToInt32(dr["QuestionnaireId"]),
								Title = Convert.ToString(dr["Title"]),
								Frequency = TryParseInt(dr, "Frequency"),
								CreateConsultation = TryParseBoolean(dr, "CreateConsultation"),
								IsAnonymous = TryParseBoolean(dr, "IsAnonymous")
							};
							questionnaireList.Add(questionnaire);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return questionnaireList;
		}

		public List<Questionnaire> GetSurveyQuestionnaireForKiosk(int kioskId, int organisationId)
		{
			string procedureName = "[PatientFlow].[GetSurveyQuestionnaireForKiosk]";
			return GetQuestionnairesForKiosk(procedureName, kioskId, organisationId);
		}

		public List<Questionnaire> GetQuestionnaireListForKiosk(int kioskId, int organisationId)
		{
			string procedureName = "[PatientFlow].[GetQuestionnaireListForKiosk]";
			return GetQuestionnairesForKiosk(procedureName, kioskId, organisationId);
		}

		private List<Questionnaire> GetQuestionnairesForKiosk(string procedureName, int kioskId, int organisationId)
		{
			var questionnaireList = new List<Questionnaire>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand(procedureName, connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var questionnaire = new Questionnaire
							{
								Id = Convert.ToInt32(dr["QuestionnaireId"]),
								Title = Convert.ToString(dr["Title"]),
								Frequency = TryParseInt(dr, "Frequency"),
								IsAnonymous = TryParseBoolean(dr, "IsAnonymous")
							};
							questionnaireList.Add(questionnaire);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return questionnaireList;
		}

		public Questionnaire GetQuestionnaireDetails(int questionnaireId)
		{
			var questionnaire = new Questionnaire();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					var ds = new DataSet();
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetQuestionnaireDetails]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaireId));
					using (var da = new SqlDataAdapter())
					{
						da.SelectCommand = spCommand;
						da.Fill(ds);

						if (ds.Tables.Count > 0)
						{
							DataRow dataRow = ds.Tables[0].Rows[0];
							questionnaire.Id = Convert.ToInt32(dataRow["QuestionnaireId"]);
							questionnaire.Title = Convert.ToString(dataRow["Title"]);
							questionnaire.Frequency = GetIntValue(dataRow, "Frequency");
							questionnaire.CreateConsultation = GetBooleanValue(dataRow, "CreateConsultation");
							questionnaire.IsAnonymous = GetBooleanValue(dataRow, "IsAnonymous");
							questionnaire.IsActive = GetBooleanValue(dataRow, "IsActive");
							questionnaire.OrganisationId = Convert.ToInt32(dataRow["OrganisationId"]);

							questionnaire.Questions = new List<Questions>();
							if (ds.Tables[1].Rows.Count > 0)
							{
								foreach (DataRow dr in ds.Tables[1].Rows)
								{
									questionnaire.Questions.Add(ToQuestionObject(dr));
								}
							}

							questionnaire.QuestionOptions = new List<QuestionOptionList>();
							if (ds.Tables[2].Rows.Count > 0)
							{
								foreach (DataRow dr in ds.Tables[2].Rows)
								{
									questionnaire.QuestionOptions.Add(ToQuestionOptionObject(dr));
								}
							}

							if (ds.Tables[3].Rows.Count > 0)
							{
								questionnaire.LinkedKiosk = new List<int>();
								List<string> sb = new List<string>();
								foreach (DataRow dr in ds.Tables[3].Rows)
								{
									if (dr["KioskId"] != DBNull.Value)
									{
										questionnaire.LinkedKiosk.Add(GetIntValue(dr, "KioskId"));
										sb.Add(dr["KioskName"].ToString());
									}
								}

								questionnaire.KioskName = sb.Count > 0 ? string.Join(",", sb.ToArray()) : "Select Kiosks";

							}

						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return questionnaire;
		}

		private Questions ToQuestionObject(DataRow dr)
		{
			return new Questions
			{
				Id = Convert.ToInt32(dr["QuestionId"]),
				QuestionId = Convert.ToInt32(dr["QuestionId"]),
				QuestionnaireId = Convert.ToInt32(dr["QuestionnaireId"]),
				QuestionText = Convert.ToString(dr["QuestionText"]),
				QuestionType = GetIntValue(dr,"QuestionType"),
				OptionCharLimit = GetIntValue(dr, "OptionCharLimit"),
				Age1 = GetIntValue(dr, "Age1"),
				Age2 = GetIntValue(dr, "Age2"),
				Order = GetIntValue(dr, "QuestionOrder"),
				Gender = Convert.ToString(dr["Gender"]),
				Operation = Convert.ToString(dr["Operation"])
			};
		}

		private QuestionOptionList ToQuestionOptionObject(DataRow dr)
		{
			return new QuestionOptionList
			{
				QuestionId = Convert.ToInt32(dr["QuestionId"]),
				OptionId = Convert.ToInt32(dr["OptionId"]),
				QuestionOption = Convert.ToString(dr["OptionValue"]),
				QuestionOptionCode = GetStringValue(dr, "OptionCode"),
				QuestionSnomedOptionCode = GetLongValue(dr, "SnomedCode"),
				NestedQuestionId = GetIntValue(dr, "NestedQuestionId")
			};
		}

		public List<Questions> GetQuestionListForQuestionnaire(int questionnaireId)
		{
			var questionList = new List<Questions>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetQuestionListForQuestionnaire]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaireId));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var question = new Questions
							{
								Id = Convert.ToInt32(dr["QuestionId"]),
								QuestionnaireId = Convert.ToInt32(dr["QuestionnaireId"]),
								QuestionText = Convert.ToString(dr["QuestionText"]),
								QuestionType = TryParseInt(dr, "QuestionType"),
								Age1 = Convert.ToInt32(dr["Age1"]),
								Age2 = Convert.ToInt32(dr["Age2"]),
								Operation = Convert.ToString(dr["Operation"]),
								Gender = Convert.ToString(dr["Gender"])
							};
							questionList.Add(question);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return questionList;
		}
	
		public List<Questionnaire> GetQuestionnairesForOrganisation(int organisationId)
		{
			var questionnaireList = new List<Questionnaire>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetQuestionnairesForOrganisation]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var questionnaire = new Questionnaire
							{
								Id = Convert.ToInt32(dr["QuestionnaireId"]),
								Title = Convert.ToString(dr["Title"]),
								Frequency = TryParseInt(dr, "Frequency"),
								CreateConsultation = TryParseBoolean(dr, "CreateConsultation"),
								IsAnonymous = TryParseBoolean(dr, "IsAnonymous"),
								IsActive = TryParseBoolean(dr, "IsActive")
							};
							questionnaireList.Add(questionnaire);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return questionnaireList;
		}

		public int AddQuestionnaireDetails(Questionnaire questionnaire, string user)
		{
			try
			{
				var dtKiosk = new DataTable();
				dtKiosk = CreateListDataTable(questionnaire.LinkedKiosk);

				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[AddQuestionnaire]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@Title", questionnaire.Title, 200));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Frequency", questionnaire.Frequency));
				spCommand.Parameters.Add(DbManager.CreateParameter("@IsActive", questionnaire.IsActive));
				spCommand.Parameters.Add(DbManager.CreateParameter("@CreateConsultation", questionnaire.CreateConsultation));
				spCommand.Parameters.Add(DbManager.CreateParameter("@IsAnonymous", questionnaire.IsAnonymous));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", questionnaire.OrganisationId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@KioskList", dtKiosk, "PatientFlow.List"));

				return Convert.ToInt32(spCommand.ExecuteScalar());
			}
			finally
			{
				DbManager.Close();
			}
		}

		public int UpdateQuestionnaireDetails(Questionnaire questionnaire, string user)
		{
			try
			{
				var dtKiosk = CreateListDataTable(questionnaire.LinkedKiosk);

				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateQuestionnaireDetails]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaire.Id));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Title", questionnaire.Title, 200));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Frequency", questionnaire.Frequency));
				spCommand.Parameters.Add(DbManager.CreateParameter("@CreateConsultation", questionnaire.CreateConsultation));
				spCommand.Parameters.Add(DbManager.CreateParameter("@IsAnonymous", questionnaire.IsAnonymous));
				spCommand.Parameters.Add(DbManager.CreateParameter("@IsActive", questionnaire.IsActive));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", questionnaire.OrganisationId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@KioskList", dtKiosk, "PatientFlow.List"));

				return Convert.ToInt32(spCommand.ExecuteScalar());
			}
			finally
			{
				DbManager.Close();
			}
		}

		public int DeleteQuestionnaireDetails(int questionnaire)
		{
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteQuestionnaire]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaire));
				return Convert.ToInt32(spCommand.ExecuteScalar());
			}
			finally
			{
				DbManager.Close();
			}
		}

		public int AddQuestionDetails(Questions question, int questionnaireId)
		{
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[AddQuestion]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaireId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionText", question.QuestionText, 1000));
				spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionType", question.QuestionType));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OptionCharLimit", question.OptionCharLimit));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Gender", question.Gender, 200));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Age1", question.Age1));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Age2", question.Age2));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Operation", question.Operation, 200));
				spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionOrder", question.Order));
				int questionId = Convert.ToInt32(spCommand.ExecuteScalar());
				if (question.QuestionOptions != null)
				{
					if (question.QuestionOptions.Count > 0 && questionId > 0)
						InsertQuestionAnswerOptions(question.QuestionOptions, questionId);
				}

				return questionId;
			}
			finally
			{
				DbManager.Close();
			}
		}

		public int UpdateQuestionDetails(Questions question)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateQuestionDetails]",connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionId", question.QuestionId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", question.QuestionnaireId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionText", question.QuestionText, 1000));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AgeCriteria", question.AgeCriteria));
					spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionType", question.QuestionType));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OptionCharLimit", question.OptionCharLimit));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Gender", question.Gender, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Age1", question.Age1));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Age2", question.Age2));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Operation", question.Operation, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionOrder", question.Order));
					int updateResult = Convert.ToInt32(spCommand.ExecuteScalar());
					if (question.QuestionType ==3 || question.QuestionType == 4)
					{
						if (question.QuestionOptions != null)
						{
							if (question.QuestionOptions.Count > 0 && updateResult > 0)
								InsertQuestionAnswerOptions(question.QuestionOptions, question.QuestionId);
						}
					}

					return updateResult;
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int DeleteQuestionDetails(int question)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteQuestion]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionId", question));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		private void InsertQuestionAnswerOptions(List<QuestionOptionList> options, int questionId)
		{
			DataTable dtOptions = GetOptionsDataTable(options, questionId);

			SqlCommand command = DbManager.GetSprocCommand("[PatientFlow].[AddQuestionAnswerOptions]");
			command.CommandTimeout = 120;
			command.CommandType = CommandType.StoredProcedure;
			command.UpdatedRowSource = UpdateRowSource.None;

			command.Parameters.Add("@QuestionId", SqlDbType.Int, 4, dtOptions.Columns[0].ColumnName);
			command.Parameters.Add("@OptionValue", SqlDbType.VarChar, 100, dtOptions.Columns[1].ColumnName);
			command.Parameters.Add("@OptionCode", SqlDbType.VarChar, 100, dtOptions.Columns[2].ColumnName);
			command.Parameters.Add("@SnomedCode", SqlDbType.BigInt, 100, dtOptions.Columns[3].ColumnName);
			command.Parameters.Add("@NestedQuestionId", SqlDbType.Int, 100, dtOptions.Columns[4].ColumnName);

			SqlDataAdapter adptQtn = new SqlDataAdapter
			{
				InsertCommand = command,
				UpdateBatchSize = options.Count()
			};
			adptQtn.Update(dtOptions);
		}

		private DataTable GetOptionsDataTable(List<QuestionOptionList> options, int questionId)
		{
			var dtOptions = new DataTable();
			dtOptions.Columns.Add("QuestionId", typeof(int));
			dtOptions.Columns.Add("OptionValue", typeof(string));
			dtOptions.Columns.Add("OptionCode", typeof(string));
			dtOptions.Columns.Add("SnomedCode", typeof(long));
			dtOptions.Columns.Add("NestedQuestionId", typeof(int));

			foreach (QuestionOptionList t in options)
			{
				DataRow drOptions = dtOptions.NewRow();
				drOptions["QuestionId"] = questionId;
				drOptions["OptionValue"] = t.QuestionOption.Trim();
				drOptions["OptionCode"] = t.QuestionOptionCode == null ? null : t.QuestionOptionCode.Trim();
				drOptions["SnomedCode"] = t.QuestionSnomedOptionCode;
				drOptions["NestedQuestionId"] = t.NestedQuestionId;
				dtOptions.Rows.Add(drOptions);
			}

			return dtOptions;
		}

		public List<Questionnaire> GetQuestionnairesByUser(string user)
		{
			var questionnaireList = new List<Questionnaire>();
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetQuestionnairesByUser]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@User", user, 200));
				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						var questionnaire = new Questionnaire
						{
							Id = Convert.ToInt32(dr["QuestionnaireId"]),
							Title = Convert.ToString(dr["Title"]),
							Frequency = TryParseInt(dr, "Frequency"),
							CreateConsultation = TryParseBoolean(dr, "CreateConsultation"),
							IsAnonymous = TryParseBoolean(dr, "IsAnonymous"),
							OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
							IsActive = TryParseBoolean(dr, "IsActive"),
							OrganisationName = Convert.ToString(dr["OrganisationName"]),
                            LinkCount = TryParseInt(dr, "LinkCount")
                        };
						questionnaireList.Add(questionnaire);
					}
				}

			}
			finally
			{
				DbManager.Close();
			}

			return questionnaireList;
		}

		public Questionnaire SetPublish(bool status, int questionnaireId, string user)
		{
			var questionnaire = new Questionnaire();
			var ds = new DataSet();
			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SetPublish]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@Status", status));
				spCommand.Parameters.Add(DbManager.CreateParameter("@QuestionnaireId", questionnaireId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));

				using (var da = new SqlDataAdapter())
				{
					da.SelectCommand = spCommand;
					da.Fill(ds);
					if (ds.Tables[0].Rows != null)
					{
						DataRow dr = ds.Tables[0].Rows[0];
						questionnaire = new Questionnaire
						{
							Id = Convert.ToInt32(dr["QuestionnaireId"]),
							Title = Convert.ToString(dr["Title"]),
							Frequency = GetIntValue(dr, "Frequency"),
							CreateConsultation = GetBooleanValue(dr, "CreateConsultation"),
							IsAnonymous = GetBooleanValue(dr, "IsAnonymous"),
							IsActive = GetBooleanValue(dr, "IsActive")
						};
					}
				}
			}
			finally
			{
				DbManager.Close();
			}
			return questionnaire;
		}
	}
}
