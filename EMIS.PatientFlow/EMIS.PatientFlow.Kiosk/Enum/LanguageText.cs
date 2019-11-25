namespace EMIS.PatientFlow.Kiosk.Enum
{
	public enum LanguageText
	{
		//Common Text
		//Welcome = 1001,
		HiText = 1002,
		YesText = 1003,
		NoText = 1004,
		ThankYouText = 1005,
		TimingOutIn = 1006,

		//Organisation page
		OrganisationWelcomeText = 1201,
		SelectOrganisationText = 1202,
	
		//SelectOption Text
		ArrivalText = 1203,
		ArrivalTopasText = 1204,
		SurveyText = 1205,
		SiteMapText = 1206,
		BookAppointmentText = 1207,
		ArrivalBarCodeTopasText = 1208,
		ArrivalBarCodeTopasHelpText = 1209,
		WelcomeToText = 1210,
        
        //Surname Text
        SelectionWelcomeText = 1301,
        SelectSurnameText =1302,

        //Day Text
        SelectDayText = 1303,

        //Months Text
        Jan = 1304,
        Feb = 1305,
        Mar = 1306,
        Apr = 1307,
        May = 1308,
        Jun = 1309,
        July = 1310,
        Aug = 1311,
        Sep = 1312,
        Oct = 1313,
        Nov = 1314,
        Dec = 1315,
        SelectMonthText = 1316,

        //Year Text
        SelectYearText = 1317,
        NoneoftheAbove = 1318,

		//DOB Year Text
		SelectDOBYearText = 1319,
		NextBtnText = 1320,

		//Postcode Text
		SelectPostcodeText = 1321,

        //Gender
        SelectGenderText = 1322,
        Other = 1323,
        Male = 1324,
        Female = 1325,
        IdRatherNotSayText = 1326,

		//Settings Page
		SettingsAdminLoginWelcomeText = 1401,
		EnterYourPinText = 1402,
		IncorrectPinText = 1403,
		SettingsAdminAreaWelcomeText =1404,

		//Multiple Appointments Page
		CheckInText = 1501,
		CheckInForOneAppointmentText = 1502,
		CheckInForNAppointmentsText = 1503,
		SelectTheAppointmentsToCheckInText = 1504,
		NotYouText = 1505,
		CloseText = 1506,

		//Single Appointments Page
        AppointmentText = 1507,

        //MultipleAppointmentNumbers
        OneText = 1511,
        TwoText = 1512,
        ThreeText = 1513,
        FourText = 1514,
        FiveText = 1515,
        SixText = 1516,
        SevenText = 1517,
        EightText = 1518,
        NineText = 1519,
        TenText = 1520,

		//Finish Arrival Page
		CheckedAppointmentsText = 1601,
		DoneText = 1602,
		ContinueText = 1603,
		ShowAllText = 1604,
		ViewAllAppointmentsText = 1605,
		WeWouldLikeToLetUKnowText = 1606,
		SurgeryWouldLikeToLetUKnowText = 1607,
		GotItText = 1608,
		AppointmentsYouHaveCheckedInText = 1609,
		OnTimeText = 1610,
		RunningLateText = 1611,
		WithText = 1612,
		XMoreArrivedAppointmentsText = 1613,
        CheckedAppointmentText=1614,
        //Topas
        BarcodeSelectionText = 1701,

        //PatientMatching criteria Text
        DayOfBirth = 1801,
        MonthOfBirth = 1802,
        YearOfBirth = 1803,
        FullDateOfBirth = 1804,
        Gender = 1805,
        FirstLetterOfSurname = 1806,
        CheckInInfoText=1807,
        CantFindText=1808,
        TryAgain=1809,
        NotDisclosed=1810,
        YearNotPresentText = 1811,
        CantFindTextAppointment=1812,
        CheckInInfoText2=1813,
		//Demographics Page No
		ContinueCheckinButtonText = 1901,
		ContactReceptionwithCorrectDetails = 1902,
		UpToDateInfoText = 1903,

		//Demographics Page Yes
		NotYou = 1904,
		ThanksText = 1905,
		PleaseCheckDetails = 1906,
		UpToDateInfoMessage = 1907,
		NoInfoMessage = 1908,
		DetailsUptodateText = 1909,

		//Demographics Page text
		Name = 1910,
		PostCode = 1911,
		EmailAddress = 1912,
		MobileNumber = 1913,
		TelephoneNumber = 1914,
		GpName = 1915,
        EnterValidDateOfBirthText = 1916,
		PostCodeUnMasked = 1917,
		EmailAddressUnMasked = 1918,
		MobileNumberUnMasked = 1919,
		TelephoneNumberUnMasked = 1920,
       

		//Finish Questionaire Page Text
		AnswersSavedText = 1921,
		FeedBackText = 1922,

		//Survey Questions Text
		SkipText = 1923,
		SurgeryLikeToAskQuestionsText = 1924,

		//Would Like to Answer Survey Page
		WouldYouLikeToTakeSurveyText = 1925,
		WishToAnswerButtonText = 1926,
		DoNotWishToAnswerButtonText = 1927,

		//Questionnaire
		SelectQuestionnaire = 1928,

		//Booking Doctor Selection Page Text
		NoAppointmentsAvailable = 1930,

		//FirstAvailableAppointment Text
		WhenDoUWantAppointmentText = 1931,
		ChooseSpecificDate = 1932,
		InOneWeekText = 1933,
		InTwoWeeksText = 1934,
		InOneMonthText = 1935,
		NextAvailableAppointmentText = 1936,
		BookThisAppointmentText = 1937,

		//Calender Page
		SelectAppointmentDateText = 1940,
		AppointmentsText = 1941,
		NoAppointmentsText = 1942,
		TodayText = 1943,

        //SlotType Text
        SelectSlotText = 1950,
        CannotFindSpeakToReception = 1951,
        SpeakToReceptionText = 1952,

		//ConfirmBooking Page
		EnterReasonForAppointmentText = 1960,
		MandatoryText = 1961,
		OptionalText = 1962,
		BookMyAppointmentText = 1963,
		CannotBeMoreThanXCharText = 1964,
                
		//Finish Booking Page
		BookedYourAppointmentText = 1965,

        //Exception Page
        ReceptionTextError = 1970,
        ReportToReception = 1971,
        UnableProcessRequest = 1972,
        KioskClosedNoConnectionText = 1973,
        SorrySomethingWrongText = 1974,

		//Error Messages
		ErrorMessageForEarlyArrival = 1980,
		ErrorMessageForLateArrival = 1981,
		ErrorMessageForDoctorDivert = 1982,
		ErrorMessageForWrongLocation = 1983,
		LateArrivalSpeakToReceptionText =1984,
		CanArriveUptoXMinForEarlyArrival =1985,
		SpeakToReceptionInstead=1988,
		ArrivedAppointmentErrorMessage = 1989,

		//Topas Reception Text
		ReceptionTextForEarlyOrLateArrival = 1986,
		ReceptionText = 1987,
	}
}