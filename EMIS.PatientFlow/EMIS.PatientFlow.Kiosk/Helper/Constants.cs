namespace EMIS.PatientFlow.Kiosk.Helper
{
    public static class Constants
    {
        public const string StatusOnline = "Online";
        public const string StatusClosed = "Closed";
        public const string StatusOutOfService = "Out Of Service";
        public const string StatusNetworkUnavailable = "Network Unavailable";

		public const string KioskOutOfService = "This KIOSK is currently out of service";
		public const string KioskClosed = "This KIOSK is closed. Please report to reception";
		public const string KioskClosedNoConnection = "Sorry, this kiosk is closed";
		public const string KioskUsed = "This key is already used.";
		public const string KioskNetworkUnavailable = "Network unavailable. Please report to reception";
		public const string EgtonStatusMessage = "Please check the Egton status page for further updates";

		public const string ErrorTitleText = "Sorry! Unable to process your request.";
		public const string ErrorText = "Please report to reception";
		public const string ErrorClearScreenText = "Clear Screen";
        public const string ErrorCloseScreenText = "Close";
		public const string KioskOfflineMessage = "We are currently experiencing some issues with out of service, we expect this to be resolved shortly. Please do not switch off your kiosk during this time.";

		public const string GenderMale = "M";
        public const string GenderFemale = "F";
        public const string GenderNone = "None";
        public const string GenderOther = "U";

        public const int KioskOnline = 1;
        public const int KioskOffline = 0;

        public const string SkippedAnswer = "Skipped";
        public const string NotAnswered = "Not Answered";

        public const string AdminPassword = "1234";

        public const int DbConnectionErrorCode = -2146232060;

        public const string Delayed = "Delayed";
        public const string OnTime = "On Time";

        public const string DemographicsTitle = "Demographics Review";
        public const string DemographicsInstruction = "Please check the following details and press Yes or No";
        public const string DemographicsNextText = "Next";

		public const string BackgroundArrivalPrev = "#ffffff";
        public const string ForegroundArrival = "#943c20";
        public const string ArrivalDelayTimeText = " min delay";
        public const string ArrivalNoDelayText = "No delay";
		public const string OpenBracket = "(";
		public const string CloseBracket = ")";
		public const string GreenBackGround = "#4A7D3A";
		public const string BlueBackGround = "#BBDEFB";
		public const string DarkBlueBorderBrush = "#1976d2";
        public const string ExampleDOBText = "(e.g. 01/01/1979)";

	}
}
