namespace EMIS.PatientFlow.API.Data
{
    public class PatientNewsletterResponse
    {
        public bool Success { get; set; }
        public int ErrorNumber { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}
