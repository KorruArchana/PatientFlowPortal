using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class KioskListingViewModel
    {
        public List<Kiosk> KioskList { get; set; }
        public int OrganisationId { get; set; }
        public string DisplayString { get; set; }
        public int TotalCount { get; set; }
		public int SystemTypeId { get; set; }

		public int draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
		public Kiosk[] data { get; set; }
	}
}