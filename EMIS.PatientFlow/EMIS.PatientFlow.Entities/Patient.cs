using System;

namespace EMIS.PatientFlow.Entities
{
    public class Patient : Entity
    {
        public int PatientId { get; set; }
        public int OrganisationId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Dob { get; set; }
        public string Message { get; set; }
        public int PatientMessageId { get; set; }
		public string OrganisationName { get; set; }
	}
}
