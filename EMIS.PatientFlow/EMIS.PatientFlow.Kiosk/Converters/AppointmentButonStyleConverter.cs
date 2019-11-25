using EMIS.PatientFlow.Kiosk.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace EMIS.PatientFlow.Kiosk.Converters
{
	public class AppointmentButtonStyleConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string dataValue = values[0] != null ? values[0].ToString() : string.Empty;
			Style DefaultButtonStyle = values[1] as Style;
			Style ErrorAppointmentButtonStyle = values[2] as Style;

			return dataValue.Equals("True") ? DefaultButtonStyle : ErrorAppointmentButtonStyle;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class CheckInButtonStyleConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string dataValue = values[0].ToString();
			Style DefaultButtonStyle = values[1] as Style;
			Style CloseAppointmentButtonStyle = values[2] as Style;

            return dataValue.Equals(GlobalVariables.SelectedLanguageIdText[Enum.LanguageText.CloseText]) ? CloseAppointmentButtonStyle : DefaultButtonStyle;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
