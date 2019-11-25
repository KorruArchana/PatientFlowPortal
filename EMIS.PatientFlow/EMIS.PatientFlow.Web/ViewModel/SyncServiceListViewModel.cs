using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class SyncServiceListViewModel
    {
        public string OrganisationName { get; set; }
        public List<SyncService> Services { get; set; }
        public int TotalCount { get; set; }
    }
}