namespace EMIS.PatientFlow.Entities
{
    public class WebUser : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SupplierId { get; set; }
        public string IpAddress { get; set; }
        public string InternalIpAddress { get; set; }
		public string DatabaseName { get; set; }
        public int OrganisationId { get; set; }
        public string Type { get; set; }
		public string WebServiceUrl { get; set; }
    }
}
