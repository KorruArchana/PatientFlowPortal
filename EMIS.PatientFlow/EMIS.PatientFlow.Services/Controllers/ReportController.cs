using System;
using System.Collections.Generic;
using System.Web.Http;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;

namespace EMIS.PatientFlow.Services.Controllers
{
    [Authorize(Roles = "Practice Admin, EMIS Super User")]
    public class ReportController : ApiController
    {
        private readonly ILoggerRepository _logger;
        private readonly IReportRepository _repository;
        public ReportController(
            IReportRepository reportRepository,
            ILoggerRepository loggerRepository)
        {
            _repository = reportRepository;
            _logger = loggerRepository;
        }

        public dynamic GetReports(int pageNo, int pageSize)
        {
            Common.Validations.ArgumentValidator.IsNegativeOrZero(pageNo, "pageNo");
            Common.Validations.ArgumentValidator.IsNegativeOrZero(pageSize, "pageSize");
            try
            {
                int count;
                return new
                {
                    Report = _repository.GetReports(pageNo, pageSize, out count),
                    TotalCount = count
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IEnumerable<AuditTrial> GetLogs(Guid kioskGuid, string fromDate, string toDate)
        {
            Common.Validations.ArgumentValidator.IsNull(kioskGuid, "kioskGuid");
			Common.Validations.ArgumentValidator.IsNullOrEmpty(fromDate, "fromDate");
			Common.Validations.ArgumentValidator.IsNullOrEmpty(toDate, "toDate");
			try
            {
				return _repository.GetLogs(kioskGuid, fromDate, toDate);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IEnumerable<AuditTrial> GetSyncServiceLogs(int organisationId, string fromDate, string toDate)
        {
            Common.Validations.ArgumentValidator.IsNegativeOrZero(organisationId, "organisationId");
            Common.Validations.ArgumentValidator.IsNullOrEmpty(fromDate, "fromDate");
            Common.Validations.ArgumentValidator.IsNullOrEmpty(toDate, "toDate");
            try
            {
                return _repository.GetSyncServiceLogs(organisationId, fromDate, toDate);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IEnumerable<QuestionnaireReport> GetQuestionnaireReport(int kioskId, string fromDate, string toDate)
        {
            Common.Validations.ArgumentValidator.IsNegativeOrZero(kioskId, "kioskId");
            Common.Validations.ArgumentValidator.IsNullOrEmpty(fromDate, "fromDate");
            Common.Validations.ArgumentValidator.IsNullOrEmpty(toDate, "toDate");
            try
            {
                return _repository.GetQuestionnaireReport(kioskId, fromDate, toDate);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }
    }
}