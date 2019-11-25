using System;
using System.Globalization;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class AppointmentSlots
    {
		private string _startDateTime;
        public int SlotId { get; set; }

        public string StartTime { get; set; }

        public int SlotLength { get; set; }

		public string EndTime
		{
			get
			{
				DateTime dateTime = DateTime.ParseExact(StartTime, "HH:mm", CultureInfo.InvariantCulture);
				return dateTime.AddMinutes(SlotLength).ToString("HH:mm");
			}
		}
		public string StartDateTime {
			get {
				return this._startDateTime;
			}
			set
			{
				if (value!=null)
				{
					this._startDateTime = value;
					StartTime = DateTime.Parse(value).ToString("HH:mm");
				}
			}
		}
		public bool SlotStatus { get; set; }

		public string SlotTypeId { get; set; }

		public string SlotTypeDescription { get; set; }
		
		public int SessionId { get; set; }

		public string SiteName { get; set; }

		public string DoctorName { get; set; }

		public string ClinicType { get; set; }
		public string UserName { get; set; }
    }
}
