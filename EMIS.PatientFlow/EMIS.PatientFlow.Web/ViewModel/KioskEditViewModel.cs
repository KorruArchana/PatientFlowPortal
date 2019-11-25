using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
	public class KioskEditViewModel
	{
		public Kiosk KioskInstance { get; set; }
		public string PatientMatchTitle { get; set; }
		public IEnumerable<Language> LanguageList { get; set; }
		public List<int> SelectedModules { get; set; }
		public List<Module> Module { get; set; }
		public List<Module> AvailableModules { get; set; }
		public List<PatientMatch> PatientMatch { get; set; }

		public List<PatientMatch> AvailablePatientMatches { get; set; }
		public List<PatientMatch> AvailableAppointmentMatches { get; set; }
		public IEnumerable<Organisation> OrganisationList { get; set; }
		public int OrganisationId { get; set; }
		public int OrganisationSystemType { get; set; }
		public int LanguageId { get; set; }
		public List<string> Languages { get; set; }
		public List<SelectListItem> UnsortedQuestionnaireList { get; set; }
		public List<string> SelectedUnsortedList { get; set; }
		public List<SelectListItem> SortedQuestionnaireList { get; set; }
		public List<int> SelectedSortedList { get; set; }

		public List<string> SelectedUnsortedDDList { get; set; }
		public List<SelectListItem> DemographicList { get; set; }
		public List<SelectListItem> SortedDemographicList { get; set; }
		public List<int> SelectedSortedDDList { get; set; }

		public List<Member> SessionHolderList { get; set; }
		public List<SelectListItem> MemberList { get; set; } // Just delete it.. you can just use the sessionholderlist
		public string SelectedMemberList { get; set; }


		#region Questionnaire
		public List<SelectListItem> SurveyQuestionnaireList { get; set; }
		public List<SelectListItem> SelectedSurveyQuestionnaireList { get; set; }

		public List<int> SelectedSurveyList { get; set; }
		#endregion

		public List<int> SelectedLanguageList { get; set; }
		public List<SelectListItem> OrganisationSelectList { get; set; }
		public List<SelectListItem> LanguageSelectList { get; set; }
		public List<SelectListItem> SessionHolderListItemsList { get; set; }
		public bool IsImageChecked { get; set; }
		public bool IsSiteMapImageChecked { get; set; }
		public bool IsSiteMapImageChecked1 { get; set; }
		public bool IsSiteMapImageChecked2 { get; set; }
		public List<SelectListItem> DepartmentsselList { get; set; }
		public List<string> ValidatioErrorList { get; set; }
		public int DelayInMinutes { get; set; }
		public string GeneralMessage { get; set; }
		public int GeneralMessageSpeed { get; set; }
		public bool ShowSysInfo { get; set; }
		public bool ShowNewsletter { get; set; }

		[Required(ErrorMessage = " Admin password is required")]
		[RegularExpression("^[0-9]{4}$", ErrorMessage = "Password should consist of 4 digits.")]
		public string AdminPassword { get; set; }
		public List<AppointmentSlotType> AvailableSlotTypes { get; set; }
		public List<AppointmentSlotType> SlotTypes { get; set; }
		public List<int> SelectedSlotTypes { get; set; }
		public int NavigationId { get; set; }
		public string SelectedDepartments { get; set; }
		public string SelectedMembers { get; set; }
		public List<BrachViewModel> BranchList { get; set; }
		public List<int> SelectedSessionHolderIdList { get; set; }

		public string BranchJson { get; set; }
		public string SlotJson { get; set; }
		public string MemberJson { get; set; }
		public string SiteMapJson { get; set; }
        public string KioskLogoByte { get; set; }


	}

	public class BrachViewModel
	{
		public int OrganisationId { get; set; }
		public string OrganisationName { get; set; }
		public int BranchId { get; set; }
		public bool IsMainLocation { get; set; }
		public List<SelectListItem> SitesList { get; set; }

	}
}