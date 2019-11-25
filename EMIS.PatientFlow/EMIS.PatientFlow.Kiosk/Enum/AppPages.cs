using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.Kiosk.Enum
{
	public enum AppPages
	{
		[Display(Name = "P_MODULE")]
		SelectModule = 1,

		[Display(Name = "P_DAY")]
		SelectDay = 2,

		[Display(Name = "P_YEAR")]
		SelectYear = 3,

		[Display(Name = "P_OUT_SERV")]
		OutOfService = 4,

		[Display(Name = "P_ARR_CNF")]
		Appointments = 5,

		[Display(Name = "P_ARR_FIN")]
		Finish = 6,

		[Display(Name = "P_SUR_QUES")]
		SurveyQuestions = 7,

		[Display(Name = "MakeAppointment")]
		BookMe = 8,

		[Display(Name = "P_BOK_CNF")]
		ConfirmBooking = 9,

		[Display(Name = "P_BOK_FIN")]
		FinishBooking = 10,

		[Display(Name = "P_BOK_DAY")]
		BookingTimeSelection = 11,

		[Display(Name = "P_SUR_FIN")]
		FinishQuestionnaires = 12,
				
		[Display(Name = "P_GENDER")]
		SelectGender = 13,

		[Display(Name = "P_SURNAME")]
		SelectSurname = 14,

		[Display(Name = "P_MONTH")]
		SelectMonth = 15,

		[Display(Name = "Arrival")]
		ArriveMe = 16,

		[Display(Name = "P_EXCP_DVT")]
		ExceptionDivert = 17,

		[Display(Name = "Survey")]
		Surveys = 18,

		[Display(Name = "P_ORG")]
		Organisation = 19,

		[Display(Name = "P_SLOTTYPE")]
		SlotType = 20,

		[Display(Name = "P_DOB_YEAR")]
		SelectDobYear = 21,

		[Display(Name = "P_SETTINGS")]
		Settings = 22,

		[Display(Name = "Demo_MESSAGES")]
		DemographicMessages = 23,

		[Display(Name = "DemoGP_MESSAGES")]
		ShowGpDemographicMessages=24,

		[Display(Name = "ArrivalbyBarcode")]
		ArrivalByBarcode = 25,

		[Display(Name ="SelectPostCode")]
		SelectPostCode = 26,

		[Display(Name = "SiteMap")]
		SiteMap = 27,

		[Display(Name = "FirstAvailableAppointment")]
		FirstAvailableAppointment = 28,

		[Display(Name = "MultiplePatientsExceptionPage")]
		MultiplePatientsExceptionPage = 29,

		[Display(Name = "FullDateOfBirthViewModel")]
		FullDateOfBirthViewModel = 30,

		[Display(Name ="HomePage")]
		HomePage = 31,

		SelectDayMonthYear = 32,

		ArrivalRouting = 33,

		ArrivalConfirmationAndRouting = 34,

		SingleAppointment = 35,

		MultipleAppointments = 36,

		FinishRouting = 37,

		ArrivedAppointmentError = 38
	}
}
