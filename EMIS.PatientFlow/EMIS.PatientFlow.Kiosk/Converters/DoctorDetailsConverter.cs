using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Common.Extensions;

namespace EMIS.PatientFlow.Kiosk.Converters
{
	public class DoctorDetailsConverter : IMultiValueConverter
	{
		public object Convert(object[] doctorDetails, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (GlobalVariables.SelectedOrganisation.SystemType == SystemType.TPPSystmOne.GetDisplayName())
			{
				return doctorDetails[1];
			}
			else
				return doctorDetails[0];
		}
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
