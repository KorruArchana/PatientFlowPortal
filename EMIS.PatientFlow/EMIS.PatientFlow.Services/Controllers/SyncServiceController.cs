using System;
using System.Web.Http;
using EMIS.PatientFlow.Common.Validations;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;


namespace EMIS.PatientFlow.Services.Controllers
{
    [Authorize(Roles = "EMIS Super User")]
    public class SyncServiceController : ApiController
    {
        private readonly ISyncServiceRepository _repository;
        private readonly ILoggerRepository _logger;
        public SyncServiceController(ISyncServiceRepository repository, ILoggerRepository loggerRepository)
        {
            _repository = repository;
            _logger = loggerRepository;
        }

        [HttpGet]
        public dynamic GetSyncServices(string organisation, int pageNo, int pageSize)
        {
            try
            {
                ArgumentValidator.IsNegativeOrZero(pageNo, "pageNo");
                ArgumentValidator.IsNegativeOrZero(pageSize, "pageSize");

                int count;
                return new
                {
                    Services = _repository.GetSyncServices(organisation, pageNo, pageSize, out count),
                    TotalCount = count
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [HttpGet]
        public SyncService GetSyncService(int serviceId)
        {
            try
            {
                ArgumentValidator.IsNegativeOrZero(serviceId, "serviceId");

                return _repository.GetSyncServiceById(serviceId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public void UpdateSyncServiceStatus([FromBody]dynamic model)
        {
            try
            {
                ArgumentValidator.IsNull(model, "model");

                _repository.UpdateSyncServiceStatus((int)model.serviceId, (bool)model.status);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult SaveSyncService(SyncService model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.SaveSyncService(model);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult SaveSyncServiceOrganisations(SyncService model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.SaveSyncService(model.ProductKey, model.OrganisationIds, model.IsActive,model.KioskId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteSyncService(int serviceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.DeleteSyncService(serviceId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return Ok();
        }
    }
}
