using System.Collections.Generic;

namespace EMIS.PatientFlow.SyncService.Data
{
	public class Questionnaire
	{
		public int Id { get; set; }
		public int ParentMenuId { get; set; }
		public int LinkCount { get; set; }
		public int NodeTypeId { get; set; }
		public string Title { get; set; }
		public int Frequency { get; set; }
		public bool CreateConsultation { get; set; }
		public List<Question> Questions { get; set; }
		public List<int> LinkedKiosk { get; set; }
		public bool IsAnonymous { get; set; }
		public int QuestionCount { get; set; }
		public List<int> SelectedQuestions { get; set; }
		public string SelectedQuestionids { get; set; }
		public List<QuestionOptionList> QuestionOptions { get; set; }
		public int OrganisationId { get; set; }
	}
}
