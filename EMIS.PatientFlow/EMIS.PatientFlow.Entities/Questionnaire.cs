using System.Collections.Generic;

namespace EMIS.PatientFlow.Entities
{
    public class Questionnaire : Entity
    {
        public int LinkCount { get; set; }
        public string Title { get; set; }
        public int Frequency { get; set; }
        public bool CreateConsultation { get; set; }       
        public List<Questions> Questions { get; set; }
        public bool IsAnonymous { get; set; }
        public int QuestionCount { get; set; }
        public List<QuestionOptionList> QuestionOptions { get; set; }
        public int OrganisationId { get; set; }
		public string OrganisationName { get; set; }
		public bool IsActive { get; set; }
		public List<int> LinkedKiosk { get; set; }
		public string KioskName { get; set; }
		public List<int> deletedQuestions { get; set; }
	}
}
