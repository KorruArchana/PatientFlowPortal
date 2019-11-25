using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using Newtonsoft.Json.Linq;

namespace EMIS.PatientFlow.Web.Controllers
{
    [OutputCache(Duration = 0)]
    [Authorize(Roles = "Practice Admin, EMIS Super User")]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _repository;
		private readonly IOrganisationRepository _organisationRepository;

		public PatientController(
			IPatientRepository repository,
			IOrganisationRepository organisationRepository
			)
        {
            _repository = repository;
			_organisationRepository = organisationRepository;
        }

        public async Task<ActionResult> Index(int organisationId)
        {
            var patientListVm = new PatientListViewModel();
			Organisation organisation = await _organisationRepository.GetOrganisationDetails(organisationId);
			patientListVm.OrganisationId = organisationId;
		    patientListVm.SystemTypeId = organisation.SystemTypeId;
			return PartialView("_PatientList", patientListVm);
        }


        [HttpGet]
        public async Task<ActionResult> SearchPatient(int organisationId, string filter)
        {
            var patientVmList = new List<PatientViewModel>();
            try
            {
                patientVmList = await new ApiHelper().GetMatchedPatients(organisationId, filter);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
           }

            return PartialView("_SearchPatient", patientVmList);
        }

        [HttpGet]
        public ActionResult AddPatient(int organisationId)
        {
            var patientVm = new PatientViewModel();
            patientVm.OrganisationId = organisationId;
            return PartialView("_AddEditPatient", patientVm);
        }

        public async Task<ActionResult> EditPatient(int nodeId)
        {
            var patientVm = new PatientViewModel();
            try
            {
                Patient patient = await _repository.GetPatientDetails(nodeId);

                patientVm.Id = patient.Id;
                patientVm.PatientId = patient.PatientId;
                patientVm.Surname = patient.Surname;
                patientVm.OrganisationId = patient.OrganisationId;
                patientVm.Firstname = patient.Firstname;
                patientVm.Message = patient.Message;
                patientVm.Dob = patient.Dob;
                patientVm.PatientMessageId = patient.PatientMessageId;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }

            return PartialView("_AddEditPatient", patientVm);
        }

        [HttpPost]
        public async Task<ActionResult> Save(PatientViewModel patientVm, string submit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patient = new Patient()
                    {
                        PatientMessageId = patientVm.PatientMessageId,
                        Firstname = patientVm.Firstname.EncryptAES256(),
                        Surname = patientVm.Surname.EncryptAES256(),
                        PatientId = patientVm.PatientId,
                        OrganisationId = patientVm.OrganisationId,
                        Dob = patientVm.Dob.EncryptAES256(),
                        Message = patientVm.Message
                    };
                    await _repository.SavePatientMessage(patient);
                    
                    return RedirectToAction(
                        "Index",
                        "Patient",
                        new { organisationId = patientVm.OrganisationId });
                }
                else
                {
                    return PartialView("_AddEditPatient", patientVm);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeletePatient(int patientMessageId, int organisationId)
        {
            try
            {
                await _repository.DeletePatient(patientMessageId, organisationId);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
            }

            return RedirectToAction(
                "Index",
                "Patient",
                new
                    {
                        organisationId = organisationId
            });
        }
	}
}