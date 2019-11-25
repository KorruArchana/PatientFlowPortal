using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;
using EMIS.PatientFlow.Common.Database;
using EMIS.PatientFlow.DatabaseAccess;

namespace EMIS.PatientFlow.Repositories.Base
{
    public class BaseRepository
    {
        public DbManager DbManager { get; set; }
        public DbAccess DbAccess { get; set; }
		public DbAccess DbAccess1 { get; set; }
        public string ConnectionString { get; set; }
        public string LogConnectionString { get; set; }
		public string CurrentUser
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public BaseRepository()
        {
            ConnectionString = WebConfigurationManager.ConnectionStrings["PatientFlow"].ConnectionString;
            LogConnectionString = WebConfigurationManager.ConnectionStrings["Monitoring"].ConnectionString;

            DbManager = new DbManager(ConnectionString);

            DbAccess = new DbAccess(DbManager);
			DbAccess1 = new DbAccess(LogConnectionString);
        }

		public static void GenerateSqlException(Exception ex)
		{
			var exception = ex as SqlException;
			if (exception != null)
			{
				FileLogger.Instance.WriteLog(Entities.Enums.LogType.Fatal,
					"Error Code :" + exception.ErrorCode + " - " + ex.Message, exception.InnerException, "TestSqlUser");

				String errorMessage = string.Empty;
				StringBuilder errMsg = new StringBuilder();

				if (exception.Errors != null && exception.Errors.Count > 0)
				{
					SqlErrorCollection errors = exception.Errors;

					for (int i = 0; i < errors.Count; i++)
					{
						errMsg.Append("Message : ");
						errMsg.Append(errors[i].Message);
						errMsg.Append(" Exception Number : ");
						errMsg.Append(errors[i].Message);
						errMsg.Append(" Source : ");
						errMsg.Append(errors[i].Message);
						errMsg.Append(" Server : ");
						errMsg.Append(errors[i].Message);
						errMsg.Append(" Procedure : ");
						errMsg.Append(errors[i].Message);
						errMsg.Append(" LineNumber : ");
						errMsg.Append(errors[i].Message);
					}

					errorMessage = errMsg.ToString();

					FileLogger.Instance.WriteLog(Entities.Enums.LogType.Fatal,
				"Errors :" + errorMessage, null, "TestSqlUser2");

				}
			}
		}
	}
}
