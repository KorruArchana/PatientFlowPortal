namespace EMIS.PatientFlow.Entities
{
	public class Questions : Entity
	{
		public int QuestionnaireId { get; set; }
		public int QuestionId { get; set; }
		public string QuestionText { get; set; }
		public int QuestionType { get; set; }
		public System.Collections.Generic.List<QuestionOptionList> QuestionOptions { get; set; }
		public int AgeCriteria { get; set; }
		public int OptionCharLimit { get; set; }
		public string AnswerControlType { get; set; }
		public string Gender { get; set; }
		public int Age1 { get; set; }
		public int Age2 { get; set; }
		public string Operation { get; set; }
		public int LinkCount { get; set; }
		public int OrganisationId { get; set; }
		public int Order { get; set; }

		public string QuestionOptionsList { get; set; }

		public string QuestionDisplayType
		{
			get
			{
                switch (QuestionType)
                {
                    case 1:
                        return "Text Box";
                    case 2:
                        return "Numeric Text Box";
                    case 3:
                        return "Check Box";
                    case 4:
                        return "Radio Button";
                }
                return "Text Box";
            }
		}
	}

	public class QuestionOptionList
	{
		public int OptionId { get; set; }
		public int QuestionId { get; set; }
		public string QuestionOption { get; set; }
		public string QuestionOptionCode { get; set; }
		public long QuestionSnomedOptionCode { get; set; }
		public int NestedQuestionId { get; set; }
	}
}
