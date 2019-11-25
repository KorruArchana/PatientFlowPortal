using EMIS.PatientFlow.Common.Database;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Helper;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository
{
   public class BaseRepository
   {
       public DbManager DbManager { get; set; }
       public DbAccess DbAccess { get; set; }
       public string ConnectionString { get; set; }

       public BaseRepository()
       {
           ConnectionString = Utilities.GetAppSettingValue("DBConnection");
		   try
		   {
			   if (!ConnectionString.EndsWith(";"))
				   ConnectionString += ";";

			   ConnectionString += "MultipleActiveResultSets=True;";
			   DbManager = new DbManager(ConnectionString);
		   }
		   catch (System.Exception ex)
		   {
			  Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, "DB manager");
			  DbManager = new DbManager(Utilities.GetAppSettingValue("DBConnection"));
		   }

           DbAccess = new DbAccess(DbManager);
       }
    }
}