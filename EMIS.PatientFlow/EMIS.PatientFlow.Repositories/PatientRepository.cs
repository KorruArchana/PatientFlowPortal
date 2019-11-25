using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {

        public int SavePatientMessage(Patient patient)
        {
			try
			{
				return DbAccess.SavePatientMessage(patient, CurrentUser);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
        }

        public int DeletePatient(int patientMessageId)
        {
			try
			{
				return DbAccess.DeletePatient(patientMessageId);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
        }

        public Patient GetPatientDetails(int patientId)
        {
			try
			{
				return DbAccess.GetPatientDetails(patientId);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new Patient();
			}
          
        }

		public IEnumerable<Patient> GetPatientMessageList()
		{
			try
			{
				return DbAccess.GetPatientMessageList();
			}
			catch(Exception ex)
			{
			
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Patient>();
			}
			
		}
		public IEnumerable<Patient> GetPatientMessageListForUser()
		{
			try
			{
				return DbAccess.GetPatientMessageList(CurrentUser);
			}
			catch (Exception ex)
			{

				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Patient>();
			}
		}
	}
}
