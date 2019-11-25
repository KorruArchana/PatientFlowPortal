using System.Collections.Generic;
using System.Windows.Documents;


namespace EMIS.PatientFlow.Kiosk.Model
{
	public class BookingAppointment
	{
		public bool? AppointmentReason { get; set; }
		public string SelectedMemberList { get; set; }
	}

	public class UserDemographicSetting
	{
		public bool ShowDetails { get; set; }
		public bool ScrambleDemographicDetails { get; set; }
		public int Duration { get; set; }
		public List<DemographicDetails> UserDemographicLists { get; set; }
	}

	public class DemographicDetails
	{
		public int DemographicDetailsTypeId { get; set; }
		public string DemographicDetailsType { get; set; }
	}
}