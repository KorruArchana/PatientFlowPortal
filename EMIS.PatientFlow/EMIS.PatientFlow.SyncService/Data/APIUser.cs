namespace EMIS.PatientFlow.SyncService.Data
{
    public class ApiUser
    {
        public string IpAddress { get; set; }
        public string DatabaseName { get; set; }
        public string SupplierId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public int OrganisationId { get; set; }
		public string WebServiceUrl { get; set; }
    }
}
