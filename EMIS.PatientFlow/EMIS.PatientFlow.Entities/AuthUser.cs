using System.Collections.Generic;

namespace EMIS.PatientFlow.Entities
{
    public class AuthUser : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
		public string UserId { get; set; }
		public int OrganisationId { get; set; }
		public string OrganisationName { get; set; }
	}
}
