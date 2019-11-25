using System.Collections.Generic;

namespace EMIS.PatientFlow.Kiosk.Model
{
	public class Questionnaire : Entity
	{
		public string Title { get; set; }
		public int Frequency { get; set; }
		public bool IsAnonymous { get; set; }
		public List<Questions> Questions { get; set; }
		public bool CreateConsultation { get; set; }
		public int OrganisationId { get; set; }
		public List<QuestionOptionList> QuestionOptions { get; set; }
	}

	public class QuestionOptionList
	{
		public string QuestionOption { get; set; }
		public string QuestionOptionCode { get; set; }
		public int NestedQuestionId { get; set; }
		public int OptionId { get; set; }
		public int QuestionId { get; set; }
        public long QuestionSnomedOptionCode { get; set; }
    }
}
