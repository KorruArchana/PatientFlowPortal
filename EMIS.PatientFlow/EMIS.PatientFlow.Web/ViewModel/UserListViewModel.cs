using System.Collections.Generic;
using EMIS.PatientFlow.Web.Models;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class UserListViewModel
    {
        public string UserName { get; set; }
        public List<ExtendedAuthUser> Users { get; set; }
        public int TotalCount { get; set; }
		public int draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
		public ExtendedAuthUser[] data { get; set; }
		public List<string> OrganisationNameList { get; set; }

	}
}