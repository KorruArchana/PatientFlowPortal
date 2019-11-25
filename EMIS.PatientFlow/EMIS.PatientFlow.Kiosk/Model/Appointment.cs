using System;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class Appointment
    {
        public Patient BookedPatient { get; set; }
        public Member SessionHolder { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string DisplayTime
        {
            get
            {
                string appTime = AppointmentTime.ToString("h:mm tt");
				return appTime != "12:00 AM" ? appTime : "Appointment";
            }
        }

		public string AppointmentTimeStyle
		{
			get
			{
				return DisplayTime == "Appointment" ? "Normal" : "SemiBold";
			}
		}

        public long Id { get; set; }
        public long SiteId { get; set; }
        public string Reception { get; set; }
		public string DelayText { get; set; }
		public bool? IsViewAllAppointments { get; set; }
		public bool? IsNormalAppointment { get; set; }
		public bool? IsDelayTextVisible
		{
			get
			{
				if(string.IsNullOrEmpty(DelayText))
				{
					return null;
				}
				else
				{
					return true;
				}
			}
		}

		public bool IsUnTimedAppointment { get; set; }
		public int Duration { get; set; }
		public string TPPAppointmentID { get; set; }
	}
}
