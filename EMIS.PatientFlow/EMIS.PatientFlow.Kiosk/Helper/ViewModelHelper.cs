using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public static class ViewModelHelper
	{
		public static List<CustomiseUserDisplayText> GetDaysinText()
		{
			List<CustomiseUserDisplayText> DaysModel = new List<CustomiseUserDisplayText>();

			for (int i = 1; i <= 31; i++)
			{
				DaysModel.Add(new CustomiseUserDisplayText { Value = i.ToString("00"), DisplayText = i.ToString(CultureInfo.InvariantCulture), OrdinalText = AddOrdinal(i) });
			}

			return DaysModel;
		}

		public static string AddOrdinal(int num)
		{
			if (num <= 0) return num.ToString(CultureInfo.InvariantCulture);

			switch (num % 100)
			{
				case 11:
				case 12:
				case 13:
					return "th";
			}

			switch (num % 10)
			{
				case 1:
					return "st";
				case 2:
					return "nd";
				case 3:
					return "rd";
				default:
					return "th";
			}

		}

		public static List<CustomiseUserDisplayText> GetMonthText()
		{
			return new List<CustomiseUserDisplayText>()
			{
				new CustomiseUserDisplayText()
				{
					Value = "01",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Jan]
				},
				new CustomiseUserDisplayText()
				{
					Value = "02",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Feb]
				},
				new CustomiseUserDisplayText()
				{
					Value = "03",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Mar]
				},
				new CustomiseUserDisplayText()
				{
					Value = "04",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Apr]
				},
				new CustomiseUserDisplayText()
				{
					Value = "05",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.May]
				},
				new CustomiseUserDisplayText()
				{
					Value = "06",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Jun]
				},
				new CustomiseUserDisplayText()
				{
					Value = "07",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.July]
				},
				new CustomiseUserDisplayText()
				{
					Value = "08",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Aug]
				},
				new CustomiseUserDisplayText()
				{
					Value = "09",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Sep]
				},
				new CustomiseUserDisplayText()
				{
					Value = "10",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Oct]
				},
				new CustomiseUserDisplayText()
				{
					Value = "11",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Nov]
				},
				new CustomiseUserDisplayText()
				{
					Value = "12",
					DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Dec]
				},
			};
		}
	}
}
