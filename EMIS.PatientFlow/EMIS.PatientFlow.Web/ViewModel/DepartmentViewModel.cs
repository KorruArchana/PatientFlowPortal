using EMIS.PatientFlow.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
		public int SystemTypeId { get; set; }
		[Required(ErrorMessage="Department name is required")]
		public string DepartmentName { get; set; }
        public int OrganisationId { get; set; }
        public List<Member> MemberList { get; set; }

        public List<string> OrganisationNameList { get; set; }
        public List<string> DepartmentNameList { get; set; }
        
        public bool IsPcsLocalLinked { get; set; }

		public List<Department> DepartmentList { get; set; }
        public int LinkCount { get; set; }
        public int LinkedMessageCount { get; set; }
		public string OrganisationName { get; set; }
		public List<SelectListItem> OrganisationsList { get; set; }

		public int draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
		public Member[] data { get; set; }
	}

	public class DepartmentListViewModel
	{
		public List<DepartmentViewModel> Departments { get; set; }

		public int draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
		public DepartmentViewModel[] data { get; set; }
		public List<string> OrganisationNameList { get; set; }
	}
}