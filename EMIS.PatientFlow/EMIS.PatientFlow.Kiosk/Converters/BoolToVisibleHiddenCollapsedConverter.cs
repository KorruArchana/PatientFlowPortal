using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace EMIS.PatientFlow.Kiosk.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibleHiddenCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? booleanValue = (bool?)value;

            return booleanValue != null ? (booleanValue == true ? Visibility.Visible : Visibility.Hidden) : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Convert back is not supported.");
        }
    }
}
