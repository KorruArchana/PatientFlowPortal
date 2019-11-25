using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public async Task<dynamic> GetReports(int pageNo, int pageSize)
        {
            return await GetAsync<dynamic>(string.Format("api/Report/GetReports?pageNo={0}&pageSize={1}", pageNo, pageSize));
        }

        public async Task<List<AuditTrial>> GetLogs(
            Guid kioskGuid,
            string fromDate,
            string toDate)
        {
            List<AuditTrial> auditTrial = await GetAsync<List<AuditTrial>>(string.Format("api/Report/GetLogs?kioskGuid={0}&fromDate={1}&toDate={2}", kioskGuid, fromDate, toDate));
            return auditTrial;
        }

        public async Task<List<AuditTrial>> GetSyncServiceLogs(
           int organisationId,
           string fromDate,
           string toDate)
        {
            List<AuditTrial> auditTrial = await GetAsync<List<AuditTrial>>(string.Format("api/Report/GetSyncServiceLogs?organisationId={0}&fromDate={1}&toDate={2}", organisationId, fromDate, toDate));
            return auditTrial;
        }

        public async Task<List<QuestionnaireReport>> GetQuestionnaireReport(
            int kioskId,
            string fromDate,
            string toDate)
        {
            List<QuestionnaireReport> questionnaireReport = await GetAsync<List<QuestionnaireReport>>(string.Format("api/Report/GetQuestionnaireReport?kioskId={0}&fromDate={1}&toDate={2}", kioskId, fromDate, toDate));
            return questionnaireReport;
        }
    }
}