using EMIS.PatientFlow.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.API
{
    public class Credential
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "IP address cannot be empty")]
        public string IpAddress { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Organisation ID cannot be empty")]
        public int OrganisationId { get; set; }
        public string OrganisationKey { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Supplier ID cannot be empty")]
        public string SupplierId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User name cannot be empty")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
		public SystemType SystemType { get; set; }

        public string SiteNumber { get; set; }
		public string WebServiceUrl { get; set; }
    }
}
