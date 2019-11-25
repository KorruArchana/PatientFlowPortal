using EMIS.PatientFlow.Common.Enums;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string CallingName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WorkTelephone { get; set; }
        public string HomeTelephone { get; set; }
        public string PostCode { get; set; }
		public string PatientIdentifiers { get; set; }
		public SystemType PatientSystemType { get; set; }

        public string DisplayName
        {
            get
            {
                return string.IsNullOrEmpty(Title) ?
                   string.Format("{0} {1}", FirstName, FamilyName) :
                   string.Format("{0}. {1} {2}", Title, FirstName, FamilyName);
            }
        }
    }
}
