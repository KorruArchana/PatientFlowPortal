using System.Collections.Generic;
using System.Web.Mvc;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class ReportViewModel
    {
        public Report Report { get; set; }
        public int Id { get; set; }
        public string ReportName { get; set; }
    }

    public class ReportListViewModel
    {
        public List<ReportViewModel> ReportList { get; set; }
        public int TotalCount { get; set; }
    }

    public class Customer
    {
        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

	public class PatientFlowReportViewModel
	{
		public string OrganisationName { get; set; }
		public List<Kiosk> KioskList { get; set; }
		public string KioskName { get; set; }
		public int OrganisationId { get; set; }
		public int KioskId { get; set; }
		public List<SelectListItem> OrganisationsList { get; set; }
	}
}