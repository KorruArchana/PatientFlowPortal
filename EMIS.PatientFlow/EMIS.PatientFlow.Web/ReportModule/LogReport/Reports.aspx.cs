using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository;
using Microsoft.Reporting.WebForms;

namespace EMIS.PatientFlow.Web.ReportModule.LogReport
{
    public partial class Reports : System.Web.UI.Page
    {
        private ReportRepository _repository;

        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewerLogReport.ShowReportBody = false;
                lbError.Visible = false;
            }
        }

        private DataTable CreateListDataTable(List<AuditTrial> entitylist)
        {
            DataTable dtlist = new DataTable("LogDataTable");
            try
            {
                dtlist.Columns.Add("Date", typeof(string));
				dtlist.Columns.Add("Message", typeof(string));
                DataRow dr;
                for (int i = 0; i < entitylist.Count; i++)
                {
                    dr = dtlist.NewRow();
                    dr["Date"] = entitylist[i].Date.ToString(); 
					dr["Message"] = entitylist[i].Message;
                    dtlist.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, HttpContext.Current.User.Identity.Name);
                ModelState.AddModelError("CustomError", ex.Message);
            }

            return dtlist;
        }

        protected async void BtnViewReports_Click(object sender, EventArgs e)
        {
            DateTime fromDatetime;
            DateTime toDatetime;
            DateTime.TryParseExact(hidtxtFromDate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDatetime);
            DateTime.TryParseExact(hidtxtToDate.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out toDatetime);
            try
            {
                if (hidKioskGuid.Value == string.Empty || hidtxtFromDate.Value == string.Empty
                    || hidtxtToDate.Value == string.Empty)
                {
                    lbError.Text = "All fields are mandatory *";
                    lbError.Visible = true;
                }
                else if (fromDatetime > toDatetime)
                {
                    lbError.Visible = true;
                    lbError.Text = "<i>To Date</i> must be greater than <i>From Date</i>.";
                }
                else
                {    
                    lbError.Visible = false;
					Guid kioskGuid;
					Guid.TryParse(hidKioskGuid.Value, out kioskGuid);
                    _repository = new ReportRepository();
                    List<AuditTrial> auditTrial = await _repository.GetLogs(kioskGuid, hidtxtFromDate.Value , hidtxtToDate.Value);
                    DataTable dt = CreateListDataTable(auditTrial);
                    if (dt.Rows.Count > 0)
                    {
                        ReportViewerLogReport.ShowReportBody = true;
                        ReportParameter repParam = new ReportParameter("HeaderParameter", "Log Report");
                       
                        ReportViewerLogReport.Reset();
                        ReportViewerLogReport.LocalReport.ReportPath = "ReportModule/LogReport/LogReport.rdlc";
                        ReportViewerLogReport.LocalReport.DataSources.Clear();
                        ReportViewerLogReport.LocalReport.DataSources.Add(new ReportDataSource("LogDataSet", dt));
                        ReportViewerLogReport.LocalReport.SetParameters(new ReportParameter[] { repParam });
                        ReportViewerLogReport.DataBind();
                    }
                    else {
						ReportViewerLogReport.Reset();
						lbError.Visible = true;
                        lbError.Text = "No data available as per given criteria";
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, HttpContext.Current.User.Identity.Name);
                lbError.Visible = true;
                lbError.Text = ex.Message + " " + hidtxtFromDate.Value + " " + hidtxtToDate.Value;
            }
        }
    }
}