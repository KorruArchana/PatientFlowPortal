using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EMIS.PatientFlow.Kiosk.Helper.ResolutionSizeHelper
{

	public class SizeHelper
	{
		public int TextBlockXXLargeFont { get; set; }
		public int TextBlockXLargeFont { get; set; }
		public int TextBlockLargeFont { get; set; }
		public int TextBlockLargFont { get; set; }
		public int TextBlockMediumFont { get; set; }
		public int TextBlockXMediumFont { get; set; }
		public int TextBlockSmallFont { get; set; }
		public int TextBlockXSmallFont { get; set; }

		public int ButtonLargeFont { get; set; }
		public int ButtonMediumFont { get; set; }
		public int ButtonsMinimumHeight { get; set; }

		public int LanguagePopUpWidth { get; set; }
		public int LanguagePopUpHeight { get; set; }
		public int RadioButtonCheckBoxMaximumHeight { get; set; }

		public int DayOfBirthWidth { get; set; }
		public int MonthOfBirthWidth { get; set; }
		public int YearOfBirthWidth { get; set; }

		public int DoctorSelectionButtonWidth { get; set; }
		public int SurveyQuestionnaireButtonHeight { get; set; }
		public int DOBButtonHeight { get; set; }
		public int DOBButtonWidth { get; set; }
		public int ButtonsMinimumWidth { get; set; }

		public Thickness MarginThickness { get; set; }
		public Thickness ButtonMargin { get; set; }

		public GridLength PostcodeGridLength { get; set; }
		public int DayButtonWidth { get; set; }
		public int DayButtonHeight{get;set;}
		public int MonthButtonMinWidth { get; set; }
		public int MonthButtonMaxWidth { get; set; }
		public int LanguageButtonWidth { get; set; }

		public double GridColumn1 { get; set; }
		public double GridColumn2 { get; set; }
		public int FontSize { get; set; }
		public int ArrowFontSize { get; set; }
		public int ArrowWidth { get; set; }
	}

	public class SSeriesKioskSize : SizeHelper
	{
		public SSeriesKioskSize()
		{
			TextBlockXXLargeFont = 48;
			TextBlockXLargeFont = 44;
			TextBlockLargeFont = 36;
            TextBlockLargFont = 30;
			TextBlockMediumFont = 24;
			TextBlockXMediumFont = 20;
			TextBlockSmallFont = 19;
			TextBlockXSmallFont = 16;

			ButtonLargeFont = 28;
			ButtonMediumFont = 24;
			ButtonsMinimumHeight = 90;

			LanguagePopUpWidth = 950;
			LanguagePopUpHeight = 665;
			RadioButtonCheckBoxMaximumHeight = 550;

            DayOfBirthWidth = 80;
            MonthOfBirthWidth = 95;
            YearOfBirthWidth = 150;

            DoctorSelectionButtonWidth = 320;
			SurveyQuestionnaireButtonHeight = 60;
			DOBButtonHeight = 65;
			DOBButtonWidth = 95;
			ButtonsMinimumWidth = 300;

			MarginThickness = new Thickness(125, 10, 10, 10);
			ButtonMargin = new Thickness(10);

			PostcodeGridLength = new GridLength(0.25, GridUnitType.Star);
			DayButtonWidth = 75;
			DayButtonHeight = 60;
			MonthButtonMinWidth = 200;
			MonthButtonMaxWidth = 270;
			LanguageButtonWidth = 160;

			GridColumn1 = 70;
			GridColumn2 = 110;
			FontSize = 20;
			ArrowFontSize = 40;
			ArrowWidth = 20;
		}
	}

	public class IBMKioskSize : SizeHelper
	{
		public IBMKioskSize()
		{
			TextBlockXXLargeFont = 44;
			TextBlockXLargeFont = 36;
			TextBlockLargeFont = 26;
            TextBlockLargFont = 22;
			TextBlockMediumFont = 19;
			TextBlockXMediumFont = 18;
			TextBlockSmallFont = 16;
			TextBlockXSmallFont = 13;

			ButtonLargeFont = 22;
			ButtonMediumFont = 18;
			ButtonsMinimumHeight = 60;

			LanguagePopUpWidth = 820;
			LanguagePopUpHeight = 580;
			RadioButtonCheckBoxMaximumHeight = 400;

            DayOfBirthWidth = 70;
            MonthOfBirthWidth = 80;
            YearOfBirthWidth = 120;

            DoctorSelectionButtonWidth = 255;
			SurveyQuestionnaireButtonHeight = 60;

			DOBButtonHeight = 65;
			DOBButtonWidth = 95;
			ButtonsMinimumWidth = 300;

			MarginThickness = new Thickness(125, 10, 10, 10);
			ButtonMargin = new Thickness(10);

			PostcodeGridLength = new GridLength(0.25, GridUnitType.Star);
			DayButtonWidth = 75;
			DayButtonHeight =60 ;
			MonthButtonMinWidth = 200;
			MonthButtonMaxWidth = 270;
			LanguageButtonWidth = 160;

			GridColumn1 = 70;
			GridColumn2 = 110;
			FontSize = 20;
			ArrowFontSize = 40;
			ArrowWidth = 20;
		}
	}

	public class ElephantKioskSize : SizeHelper
	{
		public ElephantKioskSize()
		{
			TextBlockXXLargeFont = 50;
			TextBlockXLargeFont = 46;
			TextBlockLargeFont = 40;
			TextBlockLargFont = 26;
			TextBlockMediumFont = 23;
			TextBlockXMediumFont = 20;
			TextBlockSmallFont = 20;
			TextBlockXSmallFont = 16;

			ButtonLargeFont = 30;
			ButtonMediumFont = 25;
			ButtonsMinimumHeight = 70;

			LanguagePopUpWidth = 1020;
			LanguagePopUpHeight = 1080;
			RadioButtonCheckBoxMaximumHeight = 470;

			DayOfBirthWidth = 90;
			MonthOfBirthWidth = 110;
			YearOfBirthWidth = 160;

			DoctorSelectionButtonWidth = 450;
			SurveyQuestionnaireButtonHeight = 80;

			DOBButtonHeight = 75;
			DOBButtonWidth = 120;
			ButtonsMinimumWidth = 340;

			MarginThickness = new Thickness(150, 10, 10, 10);
			ButtonMargin = new Thickness(15);

			PostcodeGridLength = new GridLength(0.5, GridUnitType.Star);
			DayButtonWidth = 85;
			DayButtonHeight = 70;
			MonthButtonMinWidth = 240;
			MonthButtonMaxWidth = 300;
			LanguageButtonWidth = 170;

			GridColumn1 = 130;
			GridColumn2 = 180;
			FontSize = 30;
			ArrowFontSize = 50;
			ArrowWidth = 30;
		}
	}
}
