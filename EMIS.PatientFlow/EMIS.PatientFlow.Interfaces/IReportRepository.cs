using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
   public interface IReportRepository
    {
       IEnumerable<Report> GetReports(int pageNo, int pageSize, out int recordCount);
       IEnumerable<AuditTrial> GetLogs(Guid kioskGuid, string fromDate, string toDate);
       IEnumerable<QuestionnaireReport> GetQuestionnaireReport(int kioskId, string fromDate, string toDate);
       IEnumerable<AuditTrial> GetSyncServiceLogs(int organisationId, string fromDate, string toDate);
    }
}
