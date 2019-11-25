using System.Runtime.Serialization;

namespace EMIS.PatientFlow.Kiosk.Model
{
    [DataContract]
    public class Organisation
    {
        [DataMember(Name = "Id")]
        public string OrganisationId { get; set; }

        [DataMember(Name = "OrganisationName")]
        public string OrganisationName { get; set; }

        [DataMember(Name = "DatabaseName")]
        public string DatabaseName { get; set; }

        [DataMember(Name = "SystemType")]
        public string SystemType { get; set; }

        [DataMember(Name = "SiteId")]
        public long? SiteId { get; set; }

		[DataMember(Name = "MainLocation")]
		public bool MainLocation { get; set; }

		[DataMember(Name = "SiteName")]
		public string SiteName { get; set; }
	}
}
