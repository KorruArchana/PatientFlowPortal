using System.ComponentModel.DataAnnotations;
using EMIS.PatientFlow.Common.Enums;

namespace EMIS.PatientFlow.SyncService.Data
{
    public class Patient
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string CallingName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        [Required]
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WorkTelephone { get; set; }
        public string HomeTelephone { get; set; }
        public string PostCode { get; set; }
		public string Address1Field { get; set; }
		public string Address2Field { get; set; }
		public string Address3Field { get; set; }
		public string Address4Field { get; set; }
		public string PatientIdentifiers { get; set; }
		public SystemType PatientSystemType { get; set; }
    }
}
