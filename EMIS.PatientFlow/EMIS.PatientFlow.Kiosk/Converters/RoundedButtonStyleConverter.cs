using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace EMIS.PatientFlow.Kiosk.Converters
{
	public class RoundedButtonStyleConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string data = values[0] as string;
			Style EmptyRoundButtonStyle = values[1] as Style;
			Style FilledRoundButtonStyle = values[2] as Style;

			return string.IsNullOrEmpty(data) ? EmptyRoundButtonStyle : FilledRoundButtonStyle;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
