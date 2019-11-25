namespace EMIS.PatientFlow.Kiosk.Model
{
    public class PatientMessage
    {
        public int PatientMessageId { get; set; }
        public int PatientId { get; set; }
        public int OrganisationId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Dob { get; set; }
        public string Message { get; set; }
    }
}
