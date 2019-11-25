using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
	public class QuestionnaireViewModel
	{
		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }
		[RegularExpression(@"^[0-9]*$", ErrorMessage = "Frequency must be number.")]
		[Range(typeof(int), "0", "365")]
		[Required(ErrorMessage = "Frequency is required")]
		public int? Frequency { get; set; }
		public bool CreateConsultation { get; set; }
		public int QuestionnaireId { get; set; }
		public bool Isanonymous { get; set; }
		public List<Questions> Questions { get; set; }
		public List<SelectListItem> QuestionsList { get; set; }
		public List<int> LinkedKiosk { get; set; }
		public List<SelectListItem> KioskList { get; set; }
		public int LinkCount { get; set; }

		[Required(ErrorMessage = "Link to Organisation is required")]
		public int OrganisationId { get; set; }
		public string OrganisationName { get; set; }
		public List<SelectListItem> OrganisationList { get; set; }
		public int FrequencyType { get; set; }

		public string QuestionsListData { get; set; }
		public bool IsActive { get; set; }
		public string QuestionOptionsList { get; set; }

		public string StringifiedDeletedQuestions { get; set; }
		public List<int> DeletedQuestions { get; set; }

		public bool IsSnomedCode { get; set; }
	}

	public class QuestionnaireListingViewModel
	{
		public List<QuestionnaireViewModel> QuestionnaireList { get; set; }

		public int draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
		public QuestionnaireViewModel[] data { get; set; }
		public List<string> OrganisationNameList { get; set; }

	}
}