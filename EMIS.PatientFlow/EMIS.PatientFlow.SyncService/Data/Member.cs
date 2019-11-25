using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.SyncService.Data
{
    public class Member
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
		public int WaitingTime { get; set; }
		public string PracticeName { get; set; }
		public string Code { get; set; }
		public string MemberIdentifiers { get; set; }
	}
}
