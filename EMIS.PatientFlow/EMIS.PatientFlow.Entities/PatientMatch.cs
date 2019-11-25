using System.Collections.Generic;

namespace EMIS.PatientFlow.Entities
{
    public class PatientMatch : Entity
    {
        public string ScreenTitle { get; set; }
        public string ScreenCode { get; set; }
        public int Order { get; set; }
    }

	public class KioskMasterDetails
	{
		public List<Module> Modules { get; set; }
		public List<Language> Languages { get; set; }
		public List<PatientMatch> PatientMatches { get; set; }
		public List<PatientMatch> AppointmentMatches { get; set; }
		public List<DemographicDetails> DemographicDetails { get; set; }
	}
}
