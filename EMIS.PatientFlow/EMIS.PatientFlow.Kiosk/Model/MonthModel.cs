namespace EMIS.PatientFlow.Kiosk.Model
{
	public class CustomiseUserDisplayText
	{
		public string Value { get; set; }
		public string DisplayText { get; set; }
		public string OrdinalText { get; set; }
	}


	internal class DateOfBirthDetailsMessage
	{
		private readonly DateOfBirthType _dateOfBirthType;
		private readonly int _dateOfBirthValue;
		private readonly string _displayText;

		public DateOfBirthDetailsMessage(DateOfBirthType dateOfBirthType, int dateOfBirthValue, string displayText)
		{
			_dateOfBirthType = dateOfBirthType;
			_dateOfBirthValue = dateOfBirthValue;
			_displayText = displayText;
		}

		public DateOfBirthType DateOfBirthType { get { return _dateOfBirthType; } }
		public int DateOfBirthValue { get { return _dateOfBirthValue; } }

		public string DisplayText { get { return _displayText; } }
	}

	internal enum DateOfBirthType
	{
		None = 0,
		Day = 1,
		Month = 2,
		YearOfBirth = 3,
		DataOfBirthYear = 4

	}
}
