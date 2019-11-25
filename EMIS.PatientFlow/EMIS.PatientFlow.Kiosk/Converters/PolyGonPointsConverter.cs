using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace EMIS.PatientFlow.Kiosk.Converters
{
	class PolyGonPointsConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			double height = (double)values[0];
			double width = (double)values[1];
			PointCollection pointCollection = new PointCollection();
			pointCollection.Add(new System.Windows.Point(3, height / 2));
			pointCollection.Add(new System.Windows.Point(width / 5, height));
			pointCollection.Add(new System.Windows.Point(width, height));
			pointCollection.Add(new System.Windows.Point(width,0));
			pointCollection.Add(new System.Windows.Point(width/5,0));
			return pointCollection;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
