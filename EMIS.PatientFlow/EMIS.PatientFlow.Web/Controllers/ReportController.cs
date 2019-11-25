using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Controllers
{
    [OutputCache(Duration = 0)]
    [Authorize(Roles = "Practice Admin, EMIS Super User")]
    public class ReportController : Controller
    {
        private readonly IReportRepository _repository;
        private readonly IOrganisationRepository _organisationRepository;
        public ReportController(IReportRepository repository, IOrganisationRepository organisationRepository)
        {
            _repository = repository;
            _organisationRepository = organisationRepository;
        }
        public ActionResult Index()
        {
            return PartialView("Index");
        }
        public ActionResult ReportList(int nodeTypeId)
        {
            var container = new ContainerViewModel();
            try
            {
                var reportListtVm = new ReportListViewModel();
                container.NodeTypeId = nodeTypeId;
                container.ViewModelObject = reportListtVm;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }

            return PartialView("_ContainerPartial", container);
        }

        [HttpGet]
        public async Task<JsonResult> GetReports(int pageNumber, int pageSize)
        {
            var model = new ReportListViewModel();
            try
            {
                JToken data = await _repository.GetReports(pageNumber, pageSize);
                List<Report> reportList = data.Value<JArray>("Report").ToObject<List<Report>>();
                if (reportList != null)
                    model.ReportList = (from report in reportList
                                        select new ReportViewModel()
                                        {
                                            Id = report.Id,
                                            ReportName = report.ReportName,
                                        }).ToList();
                model.TotalCount = data.Value<int>("TotalCount");
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> UsageReportSelection(string reportName)
        {
            PatientFlowReportViewModel patientFlowReportViewModel = new PatientFlowReportViewModel();
            try
            {
                ViewBag.ReportName = reportName;
                List<Organisation> OrganisationList = await _organisationRepository.GetOrganisationsForDropDown();
                var organisations = OrganisationList.Select(org => new SelectListItem
                {
                    Text = org.OrganisationName,
                    Value = org.Id.ToString()
                });
                patientFlowReportViewModel.OrganisationsList = organisations.ToList();
                return View("UsageReportListView", patientFlowReportViewModel);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }
        }
        public async Task<ActionResult> QuestionnaireReportSelection(string reportName)
        {
            PatientFlowReportViewModel patientFlowReportViewModel = new PatientFlowReportViewModel();
            try
            {
                ViewBag.ReportName = reportName;
                List<Organisation> OrganisationList = await _organisationRepository.GetOrganisationsForDropDown();
                var organisations = OrganisationList.Select(org => new SelectListItem
                {
                    Text = org.OrganisationName,
                    Value = org.Id.ToString()
                });
                patientFlowReportViewModel.OrganisationsList = organisations.ToList();
                return View("QuestionnaireReportListView", patientFlowReportViewModel);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return null;
            }
        }

        public async Task<ActionResult> GetUsageLogReport(Guid kioskGuid, string fromDate, string toDate)
        {
            List<AuditTrial> auditTrials = new List<AuditTrial>();
            try
            {
                auditTrials = await _repository.GetLogs(kioskGuid, fromDate, toDate);
                auditTrials = auditTrials.Select(c => { c.DateToDisplay = Convertdate(c.Date); return c; }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(new { success = false, exceptionMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, result = auditTrials }, JsonRequestBehavior.AllowGet);
        }

        private static string Convertdate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }

        [HttpGet]
        public async Task<ActionResult> GetAnonymousReport(int kioskId, string fromDate, string toDate)
        {
            List<QuestionnaireReport> questionnaireReports = new List<QuestionnaireReport>();
            try
            {
                questionnaireReports = await _repository.GetQuestionnaireReport(kioskId, fromDate, toDate);
                questionnaireReports = questionnaireReports.Select(record => { record.ModifiedDateToDisplay = Convertdate(record.Modified); return record; }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(new { success = false, exceptionMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, result = questionnaireReports }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetUsageLogReport1(Guid kioskGuid, string fromDate, string toDate)
        {
            var resultData = new WebApiResult<List<AuditTrial>>();
            resultData.Result = new List<AuditTrial>();
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

            try
            {
                for (int i = 1; i <= pageSize; i++)
                {
                    resultData.Result.Add(new AuditTrial() { DateToDisplay = Convert.ToString(DateTime.Now), Message = "Arrived Patient no" + i });
                }

                return Json(new { success = true, result = resultData.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return Json(new { success = false, exceptionMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}