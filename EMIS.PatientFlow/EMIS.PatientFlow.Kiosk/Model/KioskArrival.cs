using System.Runtime.Serialization;

namespace EMIS.PatientFlow.Kiosk.Model
{
    [DataContract]
    public class KioskArrival
    {
        [DataMember(Name = "EarlyArrival")]
        public int EarlyArrival { get; set; }

        [DataMember(Name = "LateArrival")]
        public int LateArrival { get; set; }

        [DataMember(Name = "ShowTime")]
        public bool ShowTime { get; set; }

        [DataMember(Name = "DisplayLanguageFlag")]
        public bool DisplayLanguageFlag { get; set; }

        //[DataMember(Name = "DelayInMinutes")]
        //public int DelayInMinutes { get; set; }

        [DataMember(Name = "AutoConfirmArrival")]
        public bool AutoConfirmArrival { get; set; }

		[DataMember(Name = "AutoConfirmMultipleArrival")]
		public bool AutoConfirmMultipleArrival { get; set; }

        [DataMember(Name = "ForceSurvey")]
        public bool ForceSurvey { get; set; }

        [DataMember(Name = "SkipSurveyQuestion")]
        public bool SkipSurveyQuestion { get; set; }

		[DataMember(Name = "QOFKioskUser")]
		public bool QOFKioskUser { get; set; }

		[DataMember(Name = "ShowDoctorDelay")]
		public bool ShowDoctorDelay { get; set; }

		[DataMember(Name = "AllowUntimed")]
		public bool AllowUntimed { get; set; }
		
	}
}
