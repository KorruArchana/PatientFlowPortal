using System.Collections.Generic;
namespace EMIS.PatientFlow.Entities
{
    public class AdditionalInformation
    {
		public AdditionalInformation()
		{
			MemberList = new List<Member>();
			PatientList = new List<Patient>();
			AlertList = new List<Alert>();
			DivertList = new List<Divert>();
		}

        public List<Member> MemberList { get; set; }
        public List<Patient> PatientList { get; set; }
        public List<Alert> AlertList { get; set; }
        public List<Divert> DivertList { get; set; }
    }
}
