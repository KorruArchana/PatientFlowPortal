using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class SyncServiceViewModel
    {
        public SyncService Service { get; set; }
        public List<SelectListItem> OrganisationList { get; set; }
        [Required(ErrorMessage = "Organisation is required.")]
        public List<int> OrganisationIds { get; set; }
		[Required(ErrorMessage = "Kiosk is required.")]
		public List<int> KioskIds { get; set; }
		public List<SelectListItem> KioskList { get; set; }
    }
}