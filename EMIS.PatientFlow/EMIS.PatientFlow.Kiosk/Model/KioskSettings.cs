using System.Runtime.Serialization;

namespace EMIS.PatientFlow.Kiosk.Model
{
	[DataContract]
	public class KioskSettings
	{
		[DataMember(Name = "Title")]
		public string Title { get; set; }
		[DataMember(Name = "ScreenTimeOut")]
		public int ScreenTimeOut { get; set; }
		[DataMember(Name = "Sleep")]
		public string Sleep { get; set; }
		[DataMember(Name = "Wakeup")]
		public string Wakeup { get; set; }
		[DataMember(Name = "DisplayLanguageField")]
		public bool DisplayLanguageField { get; set; }
		[DataMember(Name = "ShowDateTime")]
		public bool ShowDateTime { get; set; }
		[DataMember(Name = "ShowSysInfo")]
		public bool ShowSysInfo { get; set; }
		[DataMember(Name = "AdminPassword")]
		public string AdminPassword { get; set; }
		[DataMember(Name = "AppointmentReason")]
		public bool? AppointmentReason { get; set; }
	}
}
