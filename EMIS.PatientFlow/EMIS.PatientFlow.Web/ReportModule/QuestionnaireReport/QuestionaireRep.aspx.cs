using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository;
using Microsoft.Reporting.WebForms;

namespace EMIS.PatientFlow.Web.ReportModule.QuestionnaireReport
{
    public partial class QuestionaireRep : System.Web.UI.Page
    {
        private ReportRepository _repository;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewerQuestionnaireReport.ShowReportBody = false;
                lbError.Visible = false;
            }
        }

        private DataTable CreateListDataTable(List<EMIS.PatientFlow.Entities.QuestionnaireReport> entitylist)
        {
            DataTable dtlist = new DataTable("QuestionnaireDatatable");
            try
            {
                dtlist.Columns.Add("RowNo", typeof(int));
                dtlist.Columns.Add("QuestionnaireTitle", typeof(string));
                dtlist.Columns.Add("QuestionText", typeof(string));
                dtlist.Columns.Add("AnswerText", typeof(string));
                dtlist.Columns.Add("Modified", typeof(string));
                DataRow dr;
                for (int i = 0; i < entitylist.Count; i++)
                {
                    dr = dtlist.NewRow();
                    dr["RowNo"] = i + 1;
                    dr["Modified"] = entitylist[i].Modified.ToString();
                    dr["QuestionnaireTitle"] = entitylist[i].QuestionnaireTitle.Trim();
                    dr["QuestionText"] = entitylist[i].QuestionText.Trim();
                    dr["AnswerText"] = entitylist[i].AnswerText.Trim();
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
            
                if (hidKioskId.Value == string.Empty || hidtxtFromDate.Value == string.Empty || hidtxtToDate.Value == string.Empty)
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
                    _repository = new ReportRepository();
                    int kioskId = Convert.ToInt32(hidKioskId.Value);
                    List<EMIS.PatientFlow.Entities.QuestionnaireReport> questionnairereport = await _repository.GetQuestionnaireReport(kioskId, hidtxtFromDate.Value, hidtxtToDate.Value);
                    DataTable dt = CreateListDataTable(questionnairereport);
                    if (dt.Rows.Count > 0)
                    {
                        ReportViewerQuestionnaireReport.ShowReportBody = true;
                        ReportViewerQuestionnaireReport.Reset();
                        ReportViewerQuestionnaireReport.LocalReport.ReportPath = "ReportModule/QuestionnaireReport/QuestionnaireReport.rdlc";
                        ReportViewerQuestionnaireReport.LocalReport.DataSources.Clear();
                        ReportViewerQuestionnaireReport.LocalReport.DataSources.Add(new ReportDataSource("QuestionnaireDataSet", dt));
                        ReportViewerQuestionnaireReport.DataBind();
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
            }
        }
    }
}