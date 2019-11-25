using System;
using System.Web.Http;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Services.Hubs;
using EMIS.PatientFlow.Common.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMIS.PatientFlow.Services.Controllers
{
    [Authorize(Roles = "Practice Admin, EMIS Super User")]
    public class PatientController : ApiController
    {
        private readonly ILoggerRepository _logger;
        private readonly IPatientRepository _repository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly KioskHub _kioskHub;

        public PatientController(
            IPatientRepository patientRepository, 
            ILoggerRepository loggerRepository,
            IOrganisationRepository organisationRepository,
            KioskHub kioskHub)
        {
            _repository = patientRepository;
            _logger = loggerRepository;
            _organisationRepository = organisationRepository;
            _kioskHub = kioskHub;
        }

		public List<Patient> GetPatientMessageList()
		{
			try
			{

				if (!HttpContext.Current.User.IsInRole("Practice Admin"))
				{
					return _repository.GetPatientMessageList().ToList();
				}
				else
				{
					return _repository.GetPatientMessageListForUser().ToList();
				}	

			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

		public int SavePatientMessage([FromBody]string value)
        {
            int result;
            try
            {
                Patient patient = value.ConvertFromJsonString<Patient>();
                result = _repository.SavePatientMessage(patient);

                patient.PatientMessageId = result;
                string organisationName =
                  _organisationRepository.GetOrganisationDetail(patient.OrganisationId).OrganisationName;
                _kioskHub.AddPatient(organisationName, patient);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return result;
        }

        public Patient GetPatientDetails(int patientId)
        {
            try
            {
                ArgumentValidator.IsNegativeOrZero(patientId, "patientId");
                return _repository.GetPatientDetails(patientId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            } 
        }

        [AcceptVerbs("GET")]
        public int DeletePatient(int patientMessageId, int organisationId)
        {
            int result = 0;
            try
            {
                ArgumentValidator.IsNegativeOrZero(patientMessageId, "patientMessageId");
                ArgumentValidator.IsNegativeOrZero(organisationId, "organisationId");
                result = _repository.DeletePatient(patientMessageId);

                string organisationName =
                  _organisationRepository.GetOrganisationDetail(organisationId).OrganisationName;
                _kioskHub.DeletePatient(
                    organisationName,
                    patientMessageId, 
                    organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
            }

            return result;
        }
	}
}
