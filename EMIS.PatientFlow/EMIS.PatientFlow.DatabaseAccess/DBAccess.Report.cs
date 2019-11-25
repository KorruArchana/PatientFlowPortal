using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public IEnumerable<Report> GetReports(int pageNo, int pageSize, out int recordCount)
		{
			var reportList = new List<Report>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetReports]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@PageNo", pageNo));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PageSize", pageSize));
					var outputParameter = DbManager.CreateOutputParameter("@TotalCount", SqlDbType.BigInt);
					spCommand.Parameters.Add(outputParameter);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var report = new Report
							{
								Id = Convert.ToInt32(dr["ReportId"]),
								ReportName = dr["ReportName"].ToString()
							};
							reportList.Add(report);
						}
					}

					recordCount = Convert.ToInt32(outputParameter.Value);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return reportList;
		}

		public IEnumerable<AuditTrial> GetLogs(Guid kioskGuid, string fromDate, string toDate)
		{
			var logList = new List<AuditTrial>();

			using (var connection = DbManager2.GetNewConnection())
			{
				try
				{
					DateTime fromDatetime;
					DateTime toDatetime;
					DateTime.TryParseExact(fromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
						System.Globalization.DateTimeStyles.None, out fromDatetime);
					DateTime.TryParseExact(toDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
						System.Globalization.DateTimeStyles.None, out toDatetime);
					DbManager2.Open(connection);
					SqlCommand spCommand = DbManager2.GetSprocCommand("[PatientFlow].[GetAuditTrail]",connection);
					spCommand.CommandTimeout = 120;
					spCommand.Parameters.Add(DbManager2.CreateParameter("@KioskGuid", kioskGuid));
					spCommand.Parameters.Add(DbManager2.CreateParameter("@StartDate", fromDatetime.Date));
					spCommand.Parameters.Add(DbManager2.CreateParameter("@EndDate", toDatetime.Date));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var log = new AuditTrial
							{
								Date = Convert.ToDateTime(dr["UsageDate"]),
								Message = Convert.ToString(dr["UsageDetails"])
							};
							logList.Add(log);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager2.Close(connection);
				}
			}
			return logList;
		}

		public IEnumerable<AuditTrial> GetSyncServiceLogs(int organisationId, string fromDate, string toDate)
		{
			var logList = new List<AuditTrial>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					DateTime fromDatetime;
					DateTime toDatetime;
					DateTime.TryParseExact(fromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
						System.Globalization.DateTimeStyles.None, out fromDatetime);
					DateTime.TryParseExact(toDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
						System.Globalization.DateTimeStyles.None, out toDatetime);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAuditTrailForSyncService]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@StartDate", fromDatetime));
					spCommand.Parameters.Add(DbManager.CreateParameter("@EndDate", toDatetime));
					spCommand.CommandTimeout = 120;
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var log = new AuditTrial
							{
								Id = Convert.ToInt32(dr["LogId"]),
								Date = Convert.ToDateTime(dr["Date"]),
								Level = Convert.ToString(dr["Level"]),
								Message = Convert.ToString(dr["Message"]),
								Exception = Convert.ToString(dr["Exception"])
							};
							logList.Add(log);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return logList;
		}

		public IEnumerable<QuestionnaireReport> GetQuestionnaireReport(int kioskId, string fromDate, string toDate)
		{
			var qReportList = new List<QuestionnaireReport>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DateTime fromDatetime;
					DateTime toDatetime;
					DateTime.TryParseExact(fromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
						System.Globalization.DateTimeStyles.None, out fromDatetime);
					DateTime.TryParseExact(toDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
						System.Globalization.DateTimeStyles.None, out toDatetime);

					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[QuestionnaireReport]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@StartDate", fromDatetime));
					spCommand.Parameters.Add(DbManager.CreateParameter("@EndDate", toDatetime));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var qReport = new QuestionnaireReport
							{
								QuestionnaireTitle = Convert.ToString(dr["QuestionnaireTitle"]),
								QuestionText = Convert.ToString(dr["QuestionText"]),
								AnswerText = Convert.ToString(dr["AnswerText"]),
								Modified = Convert.ToDateTime(dr["Modified"])
							};
							qReportList.Add(qReport);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}

				return qReportList;
			}
		}
	}
}
