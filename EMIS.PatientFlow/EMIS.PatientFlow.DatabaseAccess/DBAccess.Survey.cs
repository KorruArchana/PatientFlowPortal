namespace EMIS.PatientFlow.DatabaseAccess
{
    public partial class DbAccess
    {       
        public void SurveySave(System.Collections.Generic.List<Entities.Survey> surveys)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    //Open connection.
                    DbManager.Open(connection);

                    var dsSurvey = new System.Data.DataSet();

                    System.Data.SqlClient.SqlCommand spCommand = DbManager.GetSqlCommand("select * from [PatientFlow].[Survey]", connection);
					spCommand.CommandTimeout = 120;
                    var adptLogs = new System.Data.SqlClient.SqlDataAdapter(spCommand);

                    adptLogs.FillSchema(dsSurvey, System.Data.SchemaType.Source);

                    var dtSurvey = dsSurvey.Tables[0];
					var cmdBuilder = new System.Data.SqlClient.SqlCommandBuilder(adptLogs);

					foreach (var item in surveys)
                    {
                        var dr = dtSurvey.NewRow();

                        dr["RefAnswerId"] = item.AnswerId;
                        dr["AnswerText"] = item.AnswerText;
                        dr["RefKioskId"] = item.KioskId;
                        dr["RefOptionId"] = item.OptionId;
                        dr["RefQuestionId"] = item.QuestionId;
                        dr["RefQuestionnaireId"] = item.QuestionnaireId;
                        dr["QuestionnaireTitle"] = item.QuestionnaireTitle;
                        dr["QuestionText"] = item.QuestionText;
                        dr["ModifiedBy"] = item.ModifiedBy;
                        dr["Modified"] = item.Modified;
                        dtSurvey.Rows.Add(dr);
                    }

                    adptLogs.Update(dtSurvey);
                }
                finally
                {
                    //Close connection
                    DbManager.Close(connection);
                }
            }
        }
    }
}
