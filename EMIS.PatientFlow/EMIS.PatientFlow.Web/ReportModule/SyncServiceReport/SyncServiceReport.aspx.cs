using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository;
using Microsoft.Reporting.WebForms;

namespace EMIS.PatientFlow.Web.ReportModule.SyncServiceReport
{
    public partial class SyncServiceReport : System.Web.UI.Page
    {
        private ReportRepository _repository;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewerSyncServiceReport.ShowReportBody = false;
                lbError.Visible = false;
            }
        }

        private DataTable CreateListDataTable(List<AuditTrial> entitylist)
        {
            DataTable dtlist = new DataTable("LogDataTable");
            try
            {
                dtlist.Columns.Add("RowNo", typeof(int));
                dtlist.Columns.Add("Date", typeof(string));
                dtlist.Columns.Add("Message", typeof(string));
                dtlist.Columns.Add("Exception", typeof(string));
                for (int i = 0; i < entitylist.Count; i++)
                {
                    var dr = dtlist.NewRow();
                    dr["RowNo"] = i + 1;
                    dr["Date"] = entitylist[i].Date.ToString();
                    dr["Message"] = entitylist[i].Message;
                    dr["Exception"] = entitylist[i].Exception;
                    dtlist.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, HttpContext.Current.User.Identity.Name);
            }

            return dtlist;
        }

        protected async void BtnViewReports_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDatetime;
                DateTime toDatetime;
                DateTime.TryParseExact(hidtxtFromDate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDatetime);
                DateTime.TryParseExact(hidtxtToDate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out toDatetime);
            
                if (hidOrganizationId.Value == string.Empty || hidtxtFromDate.Value == string.Empty
                    || hidtxtToDate.Value == string.Empty)
                {
                    lbError.Visible = true;
                    lbError.Text = "All fields are mandatory *";
                    ReportViewerSyncServiceReport.ShowReportBody = false;
                }
                else if (fromDatetime > toDatetime)
                {
                    lbError.Visible = true;
                    lbError.Text = "<i>To Date</i> must be greater than <i>From Date</i>.";
                }
                else
                {
                    lbError.Visible = false;
                    _repository = new ReportRepository();
                    int organisationId = Convert.ToInt32(hidOrganizationId.Value);
                    List<AuditTrial> auditTrial = await _repository.GetSyncServiceLogs(organisationId, hidtxtFromDate.Value, hidtxtToDate.Value);
                    DataTable dt = CreateListDataTable(auditTrial);
                    if (dt.Rows.Count > 0)
                    {
                        ReportViewerSyncServiceReport.ShowReportBody = true;
                        ReportViewerSyncServiceReport.Reset();
                        ReportViewerSyncServiceReport.LocalReport.ReportPath = "ReportModule/LogReport/LogReport.rdlc";
                        ReportViewerSyncServiceReport.LocalReport.DataSources.Clear();
                        ReportParameter repParam = new ReportParameter("HeaderParameter", "Sync Service Report");
                        ReportViewerSyncServiceReport.LocalReport.DataSources.Add(new ReportDataSource("LogDataSet", dt));
                        ReportViewerSyncServiceReport.LocalReport.SetParameters(new ReportParameter[] { repParam });
                        ReportViewerSyncServiceReport.DataBind();
                    }
                    else 
                    {
                        lbError.Visible = true;
                        lbError.Text = "No data available as per given criteria";
                    }
                 
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, HttpContext.Current.User.Identity.Name);
                lbError.Visible = true;
                lbError.Text = ex.Message;
            }
        }
    }
}