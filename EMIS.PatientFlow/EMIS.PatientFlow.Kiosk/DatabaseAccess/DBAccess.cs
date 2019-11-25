using System.Configuration;
using EMIS.PatientFlow.Common.Database;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess
{
    public partial class DbAccess
    {
		public string KioskId
		{
			get
			{
				return ConfigurationManager.AppSettings["RegistrationKey"];
			}
		}

        public DbManager DbManager { get; set; }

        public DbAccess(DbManager dbManager)
        {
            DbManager = dbManager;
        }
    }
}
