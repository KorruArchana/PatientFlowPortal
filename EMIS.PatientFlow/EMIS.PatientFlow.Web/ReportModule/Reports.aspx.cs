using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EMIS.PatientFlow.Web.Repository;
using EMIS.PatientFlow.Entities;
using Microsoft.Reporting.WebForms;
using System.Threading.Tasks;
using System.Web.Mvc;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Common.Enums;


namespace EMIS.PatientFlow.Web.ReportModule
{
    public partial class Reports : System.Web.UI.Page
    {

        private ReportRepository repository;
        protected  void  Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewerLogReport.ShowReportBody = false; 
            
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
                DataRow dr;
                for (int i = 0;i < entitylist.Count;i++)
                {
                    dr = dtlist.NewRow();
                    dr["RowNo"] = i + 1;
                    dr["Date"] = entitylist[i].Date.ToString("dd/M/yyyy"); ;
                    dr["Message"] = entitylist[i].Message;
                    dr["Exception"] = entitylist[i].Exception;
                    dtlist.Rows.Add(dr);
                }
            
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, "Reports");
                this.ModelState.AddModelError("CustomError", ex.Message);
            }
            return dtlist;


        }


        protected async void BtnViewReports_Click(object sender, EventArgs e)
        {
            try
            {
                ReportViewerLogReport.ShowReportBody = true;
                repository = new ReportRepository();
                ReportViewerLogReport.Reset();
                int kioskId = Convert.ToInt32(hidKioskId.Value);
                string fromDate = Convert.ToString(hidtxtFromDate.Value);
                string toDate = Convert.ToString(hidtxtToDate.Value);
                List<AuditTrial> auditTrial = await repository.GetLogs(84, fromDate, toDate);
                DataTable dt = CreateListDataTable(auditTrial);
                ReportViewerLogReport.LocalReport.ReportPath = "ReportModule/LogReport.rdlc";
                ReportViewerLogReport.LocalReport.DataSources.Clear();
                ReportViewerLogReport.LocalReport.DataSources.Add(new ReportDataSource("LogDataSet", dt));
                ReportViewerLogReport.DataBind();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, "Reports");
               
            }

        }


    }
}