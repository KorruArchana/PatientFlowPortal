using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IPatientRepository
    {
		//Task<List<Patient>> GetPatients();
		Task<List<Patient>> GetPatientMessageList();
		Task<int> SavePatientMessage(Patient patient);
        Task<Patient> GetPatientDetails(int id);
        Task<int> DeletePatient(int patientMessageId, int organisationId);
	}
}