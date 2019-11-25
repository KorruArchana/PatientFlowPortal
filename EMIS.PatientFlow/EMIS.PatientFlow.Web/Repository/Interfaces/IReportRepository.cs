using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IReportRepository
    {
        Task<dynamic> GetReports(int pageNumber, int pageSize);
        Task<List<AuditTrial>> GetLogs(Guid kioskGuid, string fromDate, string toDate);
        Task<List<AuditTrial>> GetSyncServiceLogs(int organisationId, string fromDate, string toDate);
        Task<List<QuestionnaireReport>> GetQuestionnaireReport(int kioskId, string fromDate, string toDate);
    }
}
