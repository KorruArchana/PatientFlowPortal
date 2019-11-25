using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.Common.Enums
{
    public enum ActionType
    {
        Arrived,

        Booked,

        SurveyDone,

        NonAnonymousSurveyDone,

        Sleep,

        Wakeup,

        [Display(Name = "Unsuccesfull Arrival Request")]
        UnsuccessfullArrival,

        [Display(Name = "Multiple Matches found")]
        MultipleMatches,

		[Display(Name = "Valid Demographic Details")]
		ValidDemographicDetails,

		[Display(Name = "InValid Demographic Details")]
		InValidDemographicDetails,

		BarcodeArrival,

		[Display(Name = "Multiple Matches found – Successfully validated postcode")]
		MultipleMatchesValidated,

		[Display(Name = "Multiple Matches found – Failed to validate postcode")]
		MultipleMatchesInValid,

		[Display(Name = "Doctor Divert Enabled")]
		DoctorDivert,

		[Display(Name = "Patient Arrived in Wrong Location")]
		WrongLocation
    }
}
