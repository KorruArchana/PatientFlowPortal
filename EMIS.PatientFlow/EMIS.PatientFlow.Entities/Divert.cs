
using System.Collections.Generic;
namespace EMIS.PatientFlow.Entities
{
    public class Divert : Entity
    {
        public int OrganisationId { get; set; }
        public int SessionHolderId { get; set; }
		public string LoginId { get; set; }
    }
}
