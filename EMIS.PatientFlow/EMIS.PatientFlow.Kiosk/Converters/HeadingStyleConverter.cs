using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace EMIS.PatientFlow.Kiosk.Converters
{
	public class HeadingStyleConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string dataValue = values[0].ToString();
			Style HeadingButtonStyle = values[1] as Style;
			Style IncorrectHeadingButtonStyle = values[2] as Style;

			return dataValue.Equals("True") ? HeadingButtonStyle : IncorrectHeadingButtonStyle;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
