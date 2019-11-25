using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EMIS.PatientFlow.Kiosk.Enum
{
	public enum Modules
	{
		[Display(Name = "Arrival")]
		Arrival = 1,

		[Display(Name = "Survey")]
		Survey = 2,

		[Display(Name = "SiteMap")]
		SiteMap = 3,

		[Display(Name = "BookAppointment")]
		MakeAppointment = 4,

		[Display(Name = "BarcodeArrival")]
		ArrivalByBarCode = 5,
	}
}
