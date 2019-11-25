using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.Entities
{
    public class Kiosk : Entity
    {
        [Required(ErrorMessage = "Enter the Kiosk Name")]
        public string KioskName { get; set; }
        public string PcName { get; set; }

        [RegularExpression(@"^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$",
            ErrorMessage = "Enter a valid IP Address")]
        public string IpAddress { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "Enter the Kiosk Title")]
        public string Title { get; set; }
        public int LinkTypeId { get; set; }
        public string ConnectionGuid { get; set; }

		[Required(ErrorMessage = "Select at least one Patient Match Id ")]
		public int PatientMatchId { get; set; }
        public string PatientMatchTitle { get; set; }
        public List<Language> LanguageList { get; set; }
        public List<Module> Module { get; set; }
        [Required(ErrorMessage = "Select at least one Module ")]
        public List<int> SelectedModules { get; set; }
        public List<AppointmentSlotType> SlotTypes { get; set; }
        public List<PatientMatch> PatientMatch { get; set; }

		[Required(ErrorMessage = "Select at least one Appointment Match Id ")]
		public int AppointmentMatchId { get; set; }
        public string AppointmentMatchTitle { get; set; }
        public List<PatientMatch> AppointmentMatch { get; set; }
        public string Organisations { get; set; }
        public string KioskStatus { get; set; }
        public List<string> Languages { get; set; }
        public string LanguageIdList { get; set; }
        public string KioskGuid { get; set; }
		public List<Questionnaire> QuestionnaireList { get; set; }
		public List<int> SelectedQuestionnaires { get; set; }
		public string OrganisationName { get; set; }
        public byte[] KioskLogoByte { get; set; }
		public List<SiteMap> KioskSiteMapList { get; set; }
		public bool AutoConfirmArrival { get; set; }
		public bool ShowDoctorDelay { get; set; }
        public bool ForceSurvey { get; set; }
        public bool SkipSurveyQuestion { get; set; }
		public bool FileasKioskUser { get; set; }
		public bool? AppointmentReason { get; set; }
		public List<Member> SessionHolderList { get; set; }
		public string SelectedMemberList { get; set; }
		public List<int> SelectedSessionHolderIdList { get; set; }
		
		[Required(ErrorMessage = "Enter Early Arrival"),Range(typeof(int),"1","150")]
        public int? EarlyArrival { get; set; }

		[Required(ErrorMessage = "Enter Late Arrival"), Range(typeof(int), "1", "150")]
        public int? LateArrival { get; set; }

        [Required(ErrorMessage = "Select at least one organisation")]
        public List<int> SelectedOrganisationList { get; set; }
		public List<string> SelectedOrganisationValue { get; set; }
		public List<string> SelectedOrganisationText { get; set; }

		[Required(ErrorMessage = "Select at least one language")]
        public List<int> SelectedLanguageList { get; set; }
        public List<Organisation> OrganisationList { get; set; }
        public int Usage { get; set; }
		public int ErrorCount { get; set; }

        [Required(ErrorMessage = "Enter Screen TimeOut")]
        public int ScreenTimeOut { get; set; }
        public string KioskStartTime { get; set; }
        public string KioskEndTime { get; set; }
        public string GeneralMessage { get; set; }
        public int GeneralMessageSpeed { get; set; }

		[Required(ErrorMessage = "Enter Admin Password")]
		public string AdminPassword { get; set; }
        public List<BrachModel> BranchList { get; set; }
		public List<int> SelectedDemographicDetails { get; set; }
		public List<DemographicDetails> SelectedDemographicDetailsList { get; set; }
		public bool ShowDemographicDetails { get; set; }
		[Required(ErrorMessage = "Demographic Details Duration is required")]
		public int DemographicDetailsDuration { get; set; }
		public string KioskVersion { get; set; }
		public DateTime? LastStatusModified { get; set; }
		public bool AutoConfirmMultipleArrival { get; set; }
		public bool ScrambleDemographicDetails { get; set; }
		public string SyncGuid { get; set; }
		public bool AllowUntimed { get; set; }

		//Udpate delete call and can delete this variable
		[Required(ErrorMessage = "Link to Organisation is required")]
		public int OrganisationId { get; set; }
		

		public List<int> SelectedSlotTypes { get; set; } // Has to delete it once kiosk edit is done
		//public string QuestionnaireIdList { get; set; }
		//public List<SiteMenu> SelectedDepartmentMemberTree { get; set; }
		//public string ModuleIdList { get; set; }
		//public byte[] KioskSiteMapByte { get; set; }
		//public byte[] KioskSiteMapByte1 { get; set; }
		//public byte[] KioskSiteMapByte2 { get; set; }
		//public string KioskSiteMapName { get; set; }
		//public string KioskSiteMapName1 { get; set; }
		//public string KioskSiteMapName2 { get; set; }
		//public bool ShowTimeDate { get; set; }
		//public bool DisplayLanguageFlag { get; set; }
		//public List<Member> SelectedSessionHolderList { get; set; }
		//public int DelayInMinutes { get; set; }
		//public bool ShowSysInfo { get; set; }
		//public bool ShowNewsletter { get; set; }
		//public List<string> SelectedDepartments { get; set; }
		//public List<string> SelectedMembers { get; set; }
	}

	public class BrachModel
    {
        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public int BranchId { get; set; }
		public bool IsMainLocation { get; set; }
        public List<Site> SiteList { get; set; }
        public string BranchName { get; set; }
       
    }

	 public class SiteMap
	 {
		 public string SiteMapName { get; set; }
		 public byte[] SiteMapImage { get; set; } //not using check and update it
		 public string SiteMapData { get; set; }
	 }

	public class KioskUsageLog
	{
		public string KioskGuid { get; set; }
		public int UsageLog { get; set; }
	}

	public class KioskDetails
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string KioskName { get; set; }
		public int Status { get; set; }
		public string ConnectionGuid { get; set; }
		public string KioskGuid { get; set; }
		public int OrganisationId { get; set; }
		public string OrganisationName { get; set; }
		public string OrganisationKey { get; set; }
		public int SystemTypeId { get; set; }
		public string SystemType { get; set; }
		public string PcName { get; set; }
		public string IpAddress { get; set; }
		public string KioskStatus { get; set; }
		public string KioskVersion { get; set; }
	}
}