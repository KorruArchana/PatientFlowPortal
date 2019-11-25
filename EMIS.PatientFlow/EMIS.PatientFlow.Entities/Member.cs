using System;
using System.ComponentModel.DataAnnotations;
namespace EMIS.PatientFlow.Entities
{
    public class Member : Entity
    {
        [Required]
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }
        public string Title { get; set; }
        public string LoginId { get; set; }
        public DateTime WorkStartTime { get; set; }
        public DateTime WorkEndTime { get; set; }
        public string DepartmentName { get; set; }
        public string OrganisationName { get; set; }
        public string GpCode { get; set; }
        public string SecurityGroup { get; set; }
        public string StaffCategory { get; set; }
        public int SessionHolderId { get; set; }
        public int OrganisationId { get; set; }
        public bool IsDivertSet { get; set; }
		public bool IsSelected { get; set; }
		public string FullName { get; set; }
    }
}
