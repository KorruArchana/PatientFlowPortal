using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EMIS.PatientFlow.Web.ViewModel
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int OrganisationId { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        public string Firstname { get; set; }
		public string OrganisationName { get; set; }
		public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter message")]
        public string Message { get; set; }
        public string Dob { get; set; }
        public int PatientMessageId { get; set; }
    }

    public class PatientListViewModel
    {
        public List<PatientViewModel> Patients { get; set; }
        public int OrganisationId { get; set; }
        public int TotalCount { get; set; }
		public int SystemTypeId { get; set; }
	}
}