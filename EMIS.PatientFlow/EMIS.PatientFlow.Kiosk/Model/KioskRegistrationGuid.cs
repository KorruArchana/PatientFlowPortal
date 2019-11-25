using System.Runtime.Serialization;

namespace EMIS.PatientFlow.Kiosk.Model
{
	[DataContract]
	public class KioskRegistrationGuid
	{
		[DataMember(Name = "KioskGuid")]
		public string KioskGuid { get; set; }

		[DataMember(Name = "SyncServiceGuid")]
		public string SyncServiceGuid { get; set; }
	}
}