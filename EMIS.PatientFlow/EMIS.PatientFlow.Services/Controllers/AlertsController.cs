using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Services.Helper;
using EMIS.PatientFlow.Services.Hubs;
namespace EMIS.PatientFlow.Services.Controllers
{
    [Authorize(Roles = "Practice Admin, EMIS Super User,Egton Engineer")]
    public class AlertsController : ApiController
    {
        private readonly ILoggerRepository _logger;
        private readonly IAlertsRepository _repository;
        private readonly KioskHub _kioskHub;
        public AlertsController(
            IAlertsRepository alertRepository,
            ILoggerRepository loggerRepository,
            KioskHub kioskHub)
        {
            _repository = alertRepository;
            _logger = loggerRepository;
            _kioskHub = kioskHub;
        }

		public IEnumerable<Alert> GetAlerts()
		{
			try
			{
				if (!HttpContext.Current.User.IsInRole("Practice Admin"))
				{
					return _repository.GetAlerts();
				}
				else
				{
					return _repository.GetAlertsByUser();
				}
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

        public int AddAlert([FromBody]string value)
        {
            try
            {
				Alert alert = value.ConvertFromJsonString<Alert>();
                int result = _repository.AddAlert(alert);
                alert.Id = result;
                List<string> newOrgs = _repository.GetActiveGroupsForAlert(alert.Id);
				alert.SessionHolderIdList = _repository.GetSessionHolderIdForAlert(alert.Id);
                _kioskHub.UpdateAlert(newOrgs, alert);
                return result;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public int UpdateAlert([FromBody]string value)
        {
            try
            {
	            Alert alert = value.ConvertFromJsonString<Alert>();
                int result = _repository.UpdateAlert(alert);
                List<string> newOrgs = _repository.GetActiveGroupsForAlert(alert.Id);
                alert.SessionHolderIdList = _repository.GetSessionHolderIdForAlert(alert.Id);
                _kioskHub.UpdateAlert(newOrgs, alert);
                return result;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public Alert GetAlertDetails(int alertId)
        {
            try
            {
                return _repository.GetAlertDetails(alertId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [AcceptVerbs("GET")]
        public int DeleteAlert(int alertId)
        {
            try
            {
                List<string> newOrgs = _repository.GetActiveGroupsForAlert(alertId);
                int result = _repository.DeleteAlert(alertId);
                _kioskHub.DeleteAlert(newOrgs, alertId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

		public IEnumerable<Alert> GetAlertsByMember(int MemberId)
		{
			return _repository.GetAlertsByMember(MemberId);
		}

		public IEnumerable<Alert> GetAllAlertsByMemberOrg(int MemberId)
		{
			return _repository.GetAllAlertsByMemberOrganisation(MemberId);
		}
	}
}
