using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface IPatientRepository
    {
		IEnumerable<Patient> GetPatientMessageList();
		IEnumerable<Patient> GetPatientMessageListForUser();
		int SavePatientMessage(Patient patient);
        int DeletePatient(int patientMessageId);
        Patient GetPatientDetails(int patientId);

	}
}
