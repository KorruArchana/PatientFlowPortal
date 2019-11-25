using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public IEnumerable<Report> GetReports(int pageNo, int pageSize, out int recordCount)
        {
			try
			{
				return DbAccess.GetReports(pageNo, pageSize, out recordCount);
			}
			catch (Exception ex)
			{
				recordCount = -1;
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Report>();
			}
        }

        public IEnumerable<AuditTrial> GetLogs(Guid kioskGuid, string fromDate, string toDate)
        {
			try
			{
				return DbAccess1.GetLogs(kioskGuid, fromDate, toDate);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<AuditTrial>();
			}
        } 
        public IEnumerable<AuditTrial> GetSyncServiceLogs(int organisationId, string fromDate, string toDate)
        {
			try
			{
				return DbAccess.GetSyncServiceLogs(organisationId, fromDate, toDate);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<AuditTrial>();
			}
        }
        public IEnumerable<QuestionnaireReport> GetQuestionnaireReport(int kioskId, string fromDate, string toDate)
        {
			try
			{
				return DbAccess.GetQuestionnaireReport(kioskId, fromDate, toDate);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<QuestionnaireReport>();
			} 
        }
    }
}
