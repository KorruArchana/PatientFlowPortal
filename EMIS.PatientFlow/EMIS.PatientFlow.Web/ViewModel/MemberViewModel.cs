using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Login ID is required")]
        public string LoginId { get; set; }
        public string WorkStartTime { get; set; }
        public string WorkEndTime { get; set; }
        public string DepartmentName { get; set; }
        public string OrganisationName { get; set; }
        public string GpCode { get; set; }
        public string SecurityGroup { get; set; }
        public string StaffCategory { get; set; }
        [Required]
        public int SessionHolderId { get; set; }
        public int DelayInMinutes { get; set; }
		public List<SelectListItem> OrganisationList { get; set; }
		[Required(ErrorMessage = "Link to Organisation is required")]
		public int OrganisationId { get; set; }
		public int SelectedDepartment { get; set; }
		public string FullName { get; set; }

    }
}