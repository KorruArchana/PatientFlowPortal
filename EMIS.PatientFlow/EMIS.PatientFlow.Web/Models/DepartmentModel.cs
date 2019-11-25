using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Models
{
	public class DepartmentModel
	{
		public int Id { get; set; }
		public string DepartmentName { get; set; }

		public List<Member> MemberList { get; set; }
	}
}