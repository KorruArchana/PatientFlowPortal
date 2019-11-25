using System;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Helper;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository
{
	public class DemographicRepository : BaseRepository, IDemographicRepository
	{
		public void SaveDemorgraphicFrequency(bool isValid)
		{
			DbAccess.SaveDemorgraphicFrequency(isValid);
		}

		public bool ShowDemographicDetailsForPatient(int frequency)
		{
			if (GlobalVariables.ArrivedPatientDetails.BookedPatient.Id <= 0)
				return false;

			DateTime? result = DbAccess.ShowDemographicDetailsForPatient();
			if (!result.HasValue)
				return true;
			return frequency < (DateTime.Today.Subtract(result.Value)).Days;
		}
	}
}