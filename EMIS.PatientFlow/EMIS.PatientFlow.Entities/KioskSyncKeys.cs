
namespace EMIS.PatientFlow.Entities
{
	public class KioskSyncKeys
	{
		public string OrganisationName { get; set; }
		public string KioskName { get; set; }
		public string KioskGuid { get; set; }
		public string SyncGuid { get; set; }
		public string SyncConnectionGuid { get; set; }
		public string SyncServiceStatus { get { return string.IsNullOrEmpty(SyncConnectionGuid) ? "Not Connected" : "Connected"; } }

	}
}
