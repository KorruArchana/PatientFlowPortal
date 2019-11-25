using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
    public partial class DbAccess
    {       
        public void LogSave(List<Log> logs)
        {
            using (var connection = DbManager2.GetNewConnection())
            {
                try
                {
                    var dtlogs = new DataTable("LogEntry");

					dtlogs.Columns.Add("LogId", typeof(int));
                    dtlogs.Columns.Add("LogDate", typeof(DateTime));                    
                    dtlogs.Columns.Add("LogUser", typeof(string));
                    dtlogs.Columns.Add("LogMessage", typeof(string));
                    

					int rowcount = 1;
                    foreach (var item in logs)
                    {
                        DataRow dr = dtlogs.NewRow();
						dr["LogId"] = rowcount++;
						dr["LogDate"] = item.Date;
                        dr["LogUser"] = item.User;
                        dr["LogMessage"] = item.Message;
						dtlogs.Rows.Add(dr);
                    }

                    //Open connection.
                    DbManager2.Open(connection);
                    SqlCommand spCommand = DbManager2.GetSprocCommand("[PatientFlow].[SaveLog]", connection);
					spCommand.CommandTimeout = 120;
                    spCommand.Parameters.Add(DbManager2.CreateParameter("@Data", dtlogs, "PatientFlow.LogEntry"));

                    spCommand.ExecuteNonQuery();
                }
                finally
                {
                    //Close connection
                    DbManager2.Close(connection);
                }
            }
        }
    }
}
