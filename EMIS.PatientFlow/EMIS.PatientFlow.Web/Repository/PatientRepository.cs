using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
		public async Task<List<Patient>> GetPatientMessageList()
		{
			return
			  await
				  GetAsync<List<Patient>>(
					  string.Format(
						  "api/Patient/GetPatientMessageList"));
		}

		public async Task<int> SavePatientMessage(Patient patient)
        {
            var jsonPatient = patient.ConvertToJsonString();
            return await PostAsJsonAsync<int>("api/Patient/SavePatientMessage", jsonPatient);
        }

        public async Task<Patient> GetPatientDetails(int id)
        {
            return await GetAsync<Patient>("api/Patient/GetPatientDetails?patientId=" + id);
        }

        public async Task<int> DeletePatient(int patientMessageId, int organisationId)
        {
            return await GetAsync<int>(
                string.Format(
                    "api/Patient/DeletePatient?patientMessageId={0}&organisationId={1}",
                    patientMessageId,
                    organisationId));
        }
	}
}