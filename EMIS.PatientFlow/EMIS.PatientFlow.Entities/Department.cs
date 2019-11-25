using System.Collections.Generic;

namespace EMIS.PatientFlow.Entities
{
    public class Department : Entity
    {
        public string DepartmentName { get; set; }
        public int OrganisationId { get; set; }
        public int LinkCount { get; set; }
        public int LinkedMessageCount { get; set; }
		public List<Member> MemberList { get; set; }
		public string OrganisationName { get; set; }
	}
}
