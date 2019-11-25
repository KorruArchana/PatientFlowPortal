using System;
using System.Collections.Generic;

namespace EMIS.PatientFlow.Entities
{
    public class Alert : Entity
    {
		public Alert()
		{
			SelectedDepartments = new List<string>();
			SelectedMembers = new List<string>();
			Organisation = new List<Organisation>();
			LinkedKiosk = new List<string>();
		}
		public int AlertType { get; set; }
        public string AlertText { get; set; }
        public string LinkedTo { get; set; }
        public List<int> LinkedGroupId { get; set; }
        public string Gender { get; set; }
        public int Age1 { get; set; }
        public int Age2 { get; set; }
        public string Operation { get; set; }
		public string AlertsDisplayType { get; set; }
        public int OrganisationId { get; set; }
        public List<int> OrganisationIds { get; set; }
        public string OrganisationName { get; set; }
        public string OrganisationList { get; set; }
        public List<string> SelectedDepartments { get; set; }
        public List<string> SelectedMembers { get; set; }
        public List<int> SessionHolderIdList { get; set; }
        public List<Organisation> Organisation { get; set; }
		public DateTime Modifed { get; set; }
		public string ModifiedBy { get; set; }
		public List<string> LinkedKiosk { get; set; }
		public string KioskName { get; set; }
		public bool IsMemberAlert { get; set; }
		public bool IsDeparmentAlert { get; set; }
		public bool IsOrganisationAlert { get; set; }
		private bool isEnabled;
		public bool IsEnabled {
			get
			{
				return isEnabled;
			}
			set
			{
				isEnabled = IsMemberAlert || IsDeparmentAlert || IsOrganisationAlert;
			}
		}
		public string Target { get; set; }
		public string DepartmentName { get; set; }
		public string MemberName { get; set; }
	}
}
