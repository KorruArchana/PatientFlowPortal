using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class AlertsViewModel
    {
        public int Id { get; set; }
        public int AlertType { get; set; }

        [Required(ErrorMessage = "Alert text is required")]
        public string AlertText { get; set; }

        public string LinkedTo { get; set; }
        public List<int> LinkedGroupId { get; set; }
        public string Gender { get; set; }

        [Display(Name = "Age")]
        public int Age1 { get; set; }
        [Display(Name = "Age")]
        public int Age2 { get; set; }
        public string Operation { get; set; }
		public string AlertsDisplayType { get; set; }
        public int OrganisationId { get; set; }

        public string OrganisationName { get; set; }
		public string KioskName { get; set; }
        public List<SelectListItem> OrganisationList { get; set; }

        [Required(ErrorMessage = "Link to Organisation is required")]
        public List<int> OrganisationIds { get; set; }
		public List<string> LinkedKiosk { get; set; }
		public List<SelectListItem> KioskList { get; set; }
        public string SelectedDepartments { get; set; }
        public string SelectedMembers { get; set; }
        public List<Entities.SiteMenu> SelectedDepartmentMemberTree { get; set; }
		public string MessageType { get; set; }
		public DateTime Modifed { get; set; }
		public string ModifiedBy { get; set; }
		public List<SelectListItem> AlertsDisplayTypes { get; set; }
		public PatientViewModel PatientVM { get; set; }
		public string[] SelectedMemberList { get; set; }
		public int MemberId { get; set; }
		public string MemberName { get; set; }
		public string Target { get; set; }
	}

    public class AlertsListViewModel
    {
        public List<AlertsViewModel> AlertsList { get; set; }
        public int TotalCount { get; set; }
		public List<SelectListItem> OrganisationList { get; set; }
		public List<int> OrganisationIds { get; set; }

		public int draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
		public AlertsViewModel[] data { get; set; }
		public List<string> OrganisationNameList { get; set; }

	}
}