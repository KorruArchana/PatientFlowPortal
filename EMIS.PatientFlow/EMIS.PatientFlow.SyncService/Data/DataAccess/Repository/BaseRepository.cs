using EMIS.PatientFlow.Common.Database;
using EMIS.PatientFlow.SyncService.Helper;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess.Repository
{
   public class BaseRepository
    {
        public DbManager DbManager { get; set; }
        public DbAccess DbAccess { get; set; }
        public string ConnectionString { get; set; }
        
        public BaseRepository()
        {
            ConnectionString = Utility.GetAppSettingValue("DBConnection");
            DbManager = new DbManager(ConnectionString);
            DbAccess = new DbAccess(DbManager);
        }
    }
}
