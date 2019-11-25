namespace EMIS.PatientFlow.Web.ViewModel
{
    public class WebUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SupplierId { get; set; }
        public string IpAddress { get; set; }
        public int OrganisationId { get; set; }
        public string DatabaseName { get; set; }
		public string WebServiceUrl { get; set; }
    }
}