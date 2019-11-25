using EMIS.PatientFlow.Common.Validations;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Services.Helper;
using EMIS.PatientFlow.Services.Hubs;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace EMIS.PatientFlow.Services.Controllers
{
    [Authorize(Roles = "Practice Admin, EMIS Super User,Egton Engineer")]
    public class OrganisationController : ApiController
    {
        private readonly IOrganisationRepository _repository;
        private readonly ILoggerRepository _logger;
        private readonly SyncHub _syncHub;
        private readonly KioskHub _kioskHub;
        public OrganisationController(
            IOrganisationRepository organisationRepository,
            SyncHub syncHub,
            KioskHub kioskHub,
            ILoggerRepository logger)
        {
            _syncHub = syncHub;
            _repository = organisationRepository;
            _kioskHub = kioskHub;
            _logger = logger;
        }

        public IEnumerable<Organisation> GetOrganisations()
        {
            try
            {
                if (!System.Web.HttpContext.Current.User.IsInRole("Practice Admin"))
                {
                    return _repository.GetOrganisations();
                }
                else
                {
                    return _repository.GetOrganisations(HttpContext.Current.User.Identity.Name);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IEnumerable<Organisation> GetOrganisationsForDropDown()
        {
            try
            {
                if (!System.Web.HttpContext.Current.User.IsInRole("Practice Admin"))
                {
                    return _repository.GetOrganisationsForDropDown();
                }
                else
                {
                    return _repository.GetOrganisationsForDropDown(System.Web.HttpContext.Current.User.Identity.Name);
                }

            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IHttpActionResult GetOrganisationDetails(int organisationId)
        {
            try
            {
                return Ok(_repository.GetOrganisationDetails(organisationId));
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IHttpActionResult GetDepartmentsFororganisation(int organisationId)
        {
            try
            {
                ArgumentValidator.IsNegativeOrZero(organisationId, "organisationId");
                return Ok(_repository.GetDepartmentListFororganisation(organisationId));
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public int AddOrganisation([FromBody]string value)
        {
            int result;
            try
            {
                var organisation = JSONHelper.Deserialize<Organisation>(value);

                result = _repository.AddOrganisation(organisation);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return result;
        }

        public int UpdateOrganisation([FromBody]string value)
        {
            int result;
            try
            {
                var organisation = JSONHelper.Deserialize<Organisation>(value);

                string organisationName = _repository.GetOrganisationDetail(organisation.Id).OrganisationName;
                result = _repository.UpdateOrganisation(organisation);
                organisation.User.DatabaseName = organisation.DatabaseName;

                _kioskHub.UpdateOrganisation(organisation, organisationName, organisation.DatabaseName);
                _syncHub.SaveWebClientConfiguration(organisation.User, organisationName);
                if (organisationName != organisation.OrganisationName)
                {
                    _syncHub.TransferGroup(organisation.Id, organisation.OrganisationName);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return result;
        }

        public WebUser GetPatientFlowUser(int organisationId)
        {
            try
            {
                ArgumentValidator.IsNegativeOrZero(organisationId, "organisationId");
                return _repository.GetPatientFlowUser(organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [AcceptVerbs("GET")]
        public int DeleteOrganisation(int organisationId)
        {
            int result;
            try
            {
                ArgumentValidator.IsNegativeOrZero(organisationId, "organisationId");
                result = _repository.DeleteOrganisation(organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return result;
        }

        [AcceptVerbs("GET")]
        public bool ValidateOrganisationName(string organisationName, int organisationId)
        {
            try
            {
                ArgumentValidator.IsNullOrEmpty(organisationName, "organisationName");
                bool status;
                _repository.ValidateOrganisationName(organisationName, organisationId, out status);
                return status;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [AcceptVerbs("GET")]
        public bool ValidateOrganisationKey(string organisationKey, int organisationId)
        {
            try
            {
                ArgumentValidator.IsNullOrEmpty(organisationKey, "organisationKey");
                bool status;
                _repository.ValidateOrganisationKey(organisationKey, organisationId, out status);
                return status;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [AcceptVerbs("GET")]
        public bool ValidateOrganisationSiteNumber(string organisationSiteNumber, int organisationId)
        {
            try
            {
                ArgumentValidator.IsNullOrEmpty(organisationSiteNumber, "organisationSiteNumber");
                bool status;
                _repository.ValidateOrganisationSiteNumber(organisationSiteNumber, organisationId, out status);
                return status;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetOrganisationAccessRights")]
        public List<KeyValuePair<int, string>> GetOrganisationAccessRights(string userName)
        {
            try
            {
                return _repository.GetOrganisationAccessRights(userName);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public void SaveAccessMapping(OrganisationAccess accesses)
        {
            try
            {
                _repository.SaveAccessMapping(accesses.UserName, accesses.Accesses);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }
        
        public bool DeleteOrganisationAuthUsers(string userName)
        {
            try
            {
                return _repository.DeleteOrganisationAuthUsers(userName);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }


        public List<Entity> GetSystemTypeList()
        {
            try
            {
                return _repository.GetSystemTypeList();
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetIPAddresses()
        {
            try
            {
                return Ok(_repository.GetIPAddresses());
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult GetOrganisationListByIds(List<int> organisationIds)
        {
            try
            {
                return Ok(_repository.GetOrganisationList(organisationIds));
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }
        public IHttpActionResult GetOrganisationListForKiosk(int kioskId)
        {
            try
            {
                ArgumentValidator.IsNegativeOrZero(kioskId, "kioskId");
                return Ok(_repository.GetOrganisationListForKiosk(kioskId));
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }
        public IHttpActionResult GetOrganisationListForAlert(int alertId)
        {
            try
            {
                return Ok(_repository.GetOrganisationListForAlert(alertId));
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }
    }
}
