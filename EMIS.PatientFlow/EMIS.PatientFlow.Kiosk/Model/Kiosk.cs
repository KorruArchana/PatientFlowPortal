using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class Kiosk : Entity
    {
        public string KioskName { get; set; }
        public int Status { get; set; }
        public List<LanguageSettingsModel> LanguageList { get; set; }
        public List<Options> Module { get; set; }
        public List<PatientMatch> PatientMatch { get; set; }
        public List<PatientMatch> AppointmentMatch { get; set; }
        public string KioskGuid { get; set; }
        public List<Organisation> OrganisationList { get; set; }
        public bool ShowNewsletter { get; set; }
        public List<AppointmentSlotType> SlotTypes { get; set; }
        public List<SiteMap> KioskSiteMapList { get; set; }
        public List<int> SelectedQuestionnaires { get; set; }
        public string Title { get; set; }
        public int ScreenTimeOut { get; set; }
        public string AdminPassword { get; set; }
        public bool? AppointmentReason { get; set; }
        public int EarlyArrival { get; set; }
        public int LateArrival { get; set; }
        //public int DelayInMinutes { get; set; }
        public bool AutoConfirmMultipleArrival { get; set; }
        public bool AutoConfirmArrival { get; set; }
        public bool ShowDoctorDelay { get; set; }
        public bool AllowUntimed { get; set; }
        public bool ForceSurvey { get; set; }
        public bool SkipSurveyQuestion { get; set; }
        public bool FileasKioskUser { get; set; }
        public string PatientMatchTitle { get; set; }
        public string AppointmentMatchTitle { get; set; }

        public string SelectedMemberList { get; set; }
        public List<DemographicDetails> SelectedDemographicDetailsList { get; set; }
        public bool ShowDemographicDetails { get; set; }
        public int DemographicDetailsDuration { get; set; }
        public bool ScrambleDemographicDetails { get; set; }

        public List<Questionnaire> QuestionnaireList { get; set; }
        public byte[] KioskLogoByte { get; set; }
        public string SyncGuid { get; set; }
    }


    public class SiteMap
    {
        public string SiteMapName { get; set; }
        public byte[] SiteMapImage { get; set; }
    }

    public class SiteMapImage : SiteMap
    {
        public BitmapImage Image { get; set; }
    }

    public class AdditionalInformation
    {
        public List<LinkedMember> MemberList { get; set; }
        public List<PatientMessage> PatientList { get; set; }
        public List<Alerts> AlertList { get; set; }
        public List<Divert> DivertList { get; set; }
    }

    public class KioskSystemInformation
    {
        public string PcName { get; set; }
        public string IpAddress { get; set; }
        public string Version { get; set; }

    }
}
