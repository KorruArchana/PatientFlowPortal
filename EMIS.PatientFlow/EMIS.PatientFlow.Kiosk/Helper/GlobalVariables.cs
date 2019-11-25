using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public sealed class GlobalVariables
	{
		public static string Language { get; set; }
		public static string Day { get; set; }
		public static string Year { get; set; }
		public static int YearofBirth { get; set; }
		public static string PatientMatchGender { get; set; }
		public static string PatientMatchSurname { get; set; }
		public static string PatientMatchSelectedMonth { get; set; }
		public static string PatientMatchPinCode { get; set; }
		public static int ArrivedPatientAge { get; set; }
		public static List<PatientMatch> PatientMatchOrderList { get; set; }
		public static DateTime? PatientMatchDob { get; set; }
		public static string PatientMatchDobFilter { get; set; }
		public static int SelectedPatientMatchOrder { get; set; }
		public static int SelectedSlotId { get; set; }
		public static int ArrivalSelectcount { get; set; }

		public static int SelectedSurveyOption { get; set; }
		public static string SelectedSurveyTitle { get; set; }
		public static ErrorCodes ErrorCode { get; set; }
		public static List<Questions> NonAnonymousQuestionList { get; set; }
		public static List<Questionnaire> NonAnonymousQuestionnaireList { get; set; }
		public static bool IsDataAvailable { get; set; }
		public static bool IsSyncErrorFree { get; set; }
		public static bool IsBarCodeArrival { get; set; }
		public static bool IsBarCodeArrivalDone { get; set; }
		public static bool IsMuliplePatientCheckDone { get; set; }
		public static bool IsDbConnected { get; set; }
		public static bool WasDbConnectionError { get; set; }
		public static bool IsKioskDataError { get; set; }
		public static bool InvalidCredentials { get; set; }

		public static List<SiteMapImage> KioskSiteMapsList { get; set; }

		public static bool IsKioskLogoPresent { get; set; }
		public static bool IsArrive { get; set; }
		public static bool IsAnonymous { get; set; }
		public static bool ViewArrivalInfo { get; set; }

		public static string KioskStatus { get; set; }

		public static bool ConnectionStatus { get; set; }

		public static AppointmentCollection AppointmentCollection { get; set; }
		public static List<Model.Appointment> Appointments { get; set; }
		public static int CommandParameter { get; set; }

		public static List<LanguageModel> LanguageList { get; set; }
		public static ObservableCollection<LanguageModel> FrequentlyUsedLanguageList { get; set; }
		public static Model.Appointment ArrivedPatientDetails { get; set; }
		public static string ArrivedPatientName { get; set; }
		public static List<Model.Appointment> ListOfArrivedAppointmentDetails { get; set; }
		public static List<int> ArrivedPatientDoctorList { get; set; }

		public static List<DoctorDetailsBooking> DoctorDetailsBooking { get; set; }
		public static API.Data.Appointment Appointment { get; set; }

		public static PatientMatches PatientMatches { get; set; }
		public static PatientMatchesPatient PatientMatchesPatient { get; set; }
		public static bool IsHomePage { get; set; }
		public static bool IsInFinishArrival { get; set; }
		public static bool IsInFinishBooking { get; set; }
		public static Organisation SelectedOrganisation { get; set; }
		public static List<Organisation> Organisations { get; set; }
		public static KioskSettings KioskSettings { get; set; }
		public static BookingAppointment BookAppointmentSettings { get; set; }
		public static UserDemographicSetting UserDemographicDetailsSettings { get; set; }

		public static double TimeOutValue { get; set; }
		public static string TimeOutMessage { get; set; }

		public static bool IsUsedKey { get; set; }
		public static bool IsKioskDeleted { get; set; }

		public static bool IsKeyboardInitialised { get; set; }
		public static bool IsAppLaunched { get; set; }
		public static KioskArrival KioskArrival { get; set; }
		public static AppointmentSlotType SelectedSlotType { get; set; }
		public static List<AppointmentSlotType> SlotTypeList { get; set; }
		public static string RegistrationKey { get; set; }

		public static volatile bool NetworkUnavailable;
		public static string PcsPatientRecordErrMsg { get; set; }
		public static int AnswerOptionLimit { get; set; }
		public static bool IfForcedSurveyDone { get; set; }
		public static bool IsForcedSurvey { get; set; }

		private static int _selectedLanguageId;

		public static int SelectedLanguageId
		{
			get { return _selectedLanguageId; }
			set
			{
				_selectedLanguageId = value;
				SelectedLanguageIdText = CurrentLanguageText.LoadLanguage(_selectedLanguageId);
			}
		}

		public static List<LanguageModel> GlobalLanguageList { get; set; }
		public static Dictionary<LanguageText, string> SelectedLanguageIdText { get; set; }
		public static List<string> Letters { get; set; }

		static GlobalVariables()
		{
			IsSyncErrorFree = true;
			IsBarCodeArrival = false;
			IsBarCodeArrivalDone = false;
			IsMuliplePatientCheckDone = false;
			IsDbConnected = true;
			WasDbConnectionError = false;
			IsKioskDataError = false;
			InvalidCredentials = false;
			IsKioskLogoPresent = true;
			IsArrive = true;
			IsAnonymous = false;
			ViewArrivalInfo = false;
			KioskStatus = Constants.StatusOnline;
			ConnectionStatus = false;
			FrequentlyUsedLanguageList = new ObservableCollection<LanguageModel>();
			ListOfArrivedAppointmentDetails = new List<Model.Appointment>();
			DoctorDetailsBooking = new List<DoctorDetailsBooking>();
			Appointment = new API.Data.Appointment();
			PatientMatches = new PatientMatches();
			TimeOutMessage = "Timing Out In...";
			IsUsedKey = false;
			IsKioskDeleted = false;
			IsKeyboardInitialised = false;
			IsAppLaunched = false;
			RegistrationKey = Utilities.GetAppSettingValue("RegistrationKey");
			PcsPatientRecordErrMsg = "Patient record filing not supported for PCS system";
			IfForcedSurveyDone = false;
			IsForcedSurvey = false;
			GlobalLanguageList = CurrentLanguageText.GetLanguageDetails();
			Letters = new List<string>()
				{
				"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
				};
		}
	}
}
