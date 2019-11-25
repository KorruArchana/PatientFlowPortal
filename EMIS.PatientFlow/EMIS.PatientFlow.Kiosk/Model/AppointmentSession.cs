using System.Collections.Generic;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class AppointmentSession
    {
        public int SessionId { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SlotLength { get; set; }
        public AppointmentSlotType SlotType { get; set; }
        public Member Member { get; set; }
        public long? SiteId { get; set; }
		public string SiteName { get; set; }
		public int AvaiableSlots { get; set; }
		public string ClinicType { get; set; }
		public List<AppointmentSlots> AppointmentSlotsList { get; set; }
	}
}
