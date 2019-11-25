using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.Entities
{
    public class Site : Entity
    {
        public long SiteDbId { get; set; }
        public string SiteName { get; set; }
        public int OrganisationId { get; set; }
     
    }
}
