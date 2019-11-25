using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
	public class QuestionViewModel
	{
		public int QuestionnaireId { get; set; }
		public int QuestionId { get; set; }

		[Required(ErrorMessage = "Question is required")]
		public string QuestionText { get; set; }
		public int QuestionType { get; set; }
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
		public List<QuestionOptionList> QuestionOptions { get; set; }
		public string Gender { get; set; }

		public string Operation { get; set; }
		[Display(Name = "Age")]
		public int Age1 { get; set; }
		[Display(Name = "Age")]
		public int Age2 { get; set; }

		[Range(typeof(int), "1", "200")]
		public int OptionCharLimit { get; set; }
		public string QuestionAnonymous { get; set; }
		public List<SelectListItem> NestedQuestions { get; set; }
		public int OrganisationId { get; set; }
		public string QuestionOptionsList { get; set; }
		public int Order { get; set; }
	}
}