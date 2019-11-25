namespace EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces
{
	interface IDemographicRepository
	{
		void SaveDemorgraphicFrequency(bool isValid);
		bool ShowDemographicDetailsForPatient(int frequency);
	}
}