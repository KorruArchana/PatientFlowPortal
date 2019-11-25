using System.Collections.Generic;

namespace EMIS.PatientFlow.Entities
{
    public class OrganisationAccess
    {
        public List<AccessMapping> AccessTypes { get; set; }
        public List<int> Accesses { get; set; }
        public string UserName { get; set; }
        public OrganisationAccess()
        {
            AccessTypes = new List<AccessMapping>();
            Accesses = new List<int>();
        }
    }
}
