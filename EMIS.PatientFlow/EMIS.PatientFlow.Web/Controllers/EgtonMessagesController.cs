using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Enums;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Controllers
{
	[Authorize(Roles = "EMIS Super User, Egton Engineer")]
	[HandleError(ExceptionType = typeof(Exception), View = "Error")]
	public class EgtonMessagesController : Controller
    {

		private readonly IAlertRepository _repository;
		private readonly IPatientRepository _patientRepository;

		public EgtonMessagesController(IAlertRepository repository, IPatientRepository patientRepository)
		{
			_repository = repository;
			_patientRepository = patientRepository;
		}

		public async Task<ActionResult> Index()
        {
			var alertsListVm = new AlertsListViewModel();
			try
			{
				alertsListVm = await GetMessages();
				alertsListVm.OrganisationNameList = alertsListVm.AlertsList.Select(m => m.OrganisationName).Distinct().ToList();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
			}

			return PartialView("Index", alertsListVm);
        }

		public async Task<ActionResult> GetMessagesData()
		{
			var model = new AlertsListViewModel();
			try
			{
				model = await GetMessages();
				List<AlertsViewModel> messageData = model.AlertsList;

				model.OrganisationNameList = messageData.Select(m => m.OrganisationName).Distinct().ToList();

				int sortColumn = -1;
				string sortDirection = "asc";
				var result = new List<AlertsViewModel>();
				if (Request.QueryString["order[0][dir]"] != null)
				{
					sortDirection = Request.QueryString["order[0][dir]"];
				}
				if (Request.QueryString["order[0][column]"] != null)
				{
					sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
					switch (sortColumn)
					{
						case 1:
							result = sortDirection == "desc" ? messageData.OrderByDescending(m => m.AlertText).ToList()
													 : messageData.OrderBy(m => m.AlertText).ToList();

							break;
						case 2:
							result = sortDirection == "desc" ? messageData.OrderByDescending(m => m.Target).ToList()
													 : messageData.OrderBy(m => m.Target).ToList();

							break;
						case 3:
							result = sortDirection == "desc" ? messageData.OrderByDescending(m => m.OrganisationName).ToList()
													 : messageData.OrderBy(m => m.OrganisationName).ToList();

							break;
						case 4:
							result = sortDirection == "desc" ? messageData.OrderByDescending(m => m.KioskName).ToList()
													 : messageData.OrderBy(m => m.KioskName).ToList();

							break;
						default:
							result = messageData.OrderBy(m => m.AlertText).ToList();
							break;
					}
				}

				string alertTextFilter = Request.QueryString["columns[1][search][value]"] ?? Request.QueryString["columns[1][search][value]"].ToString();
				string targetFilter = Request.QueryString["columns[2][search][value]"] ?? Request.QueryString["columns[2][search][value]"].ToString();
				string organisationNameFilter = Request.QueryString["columns[3][search][value]"] ?? Request.QueryString["columns[3][search][value]"].ToString();
				organisationNameFilter = Regex.Replace(organisationNameFilter, "[;.^#$]+", "", RegexOptions.Compiled);
				organisationNameFilter = Regex.Replace(organisationNameFilter, "[|]+", ",", RegexOptions.Compiled);
				string kioskNameFilter = Request.QueryString["columns[4][search][value]"] ?? Request.QueryString["columns[4][search][value]"].ToString();

				if (!string.IsNullOrWhiteSpace(alertTextFilter))
				{
					result = result.Where(x => x.AlertText.IndexOf(alertTextFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(targetFilter))
				{
					result = result.Where(x => x.Target.ToString().IndexOf(targetFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(organisationNameFilter))
				{
					string[] organisationNameFilters = organisationNameFilter.Split(',');
					// Has to ignore case here
					result = result.Where(x => Array.IndexOf(organisationNameFilters, x.OrganisationName) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(kioskNameFilter))
				{
					result = result.Where(x => x.KioskName.IndexOf(kioskNameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}

				model.draw = int.Parse(Request.QueryString["draw"]);
				int start = int.Parse(Request.QueryString["start"]);
				int length = int.Parse(Request.QueryString["length"]);

				model.data = result.Skip(start).Take(length).ToArray();
				model.recordsTotal = messageData.Count();
				model.recordsFiltered = result.Count();
				return Json(model, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return (Json(model, JsonRequestBehavior.AllowGet));
			}
		}


		[HttpGet]
		public async Task<AlertsListViewModel> GetMessages()
		{
			var model = new AlertsListViewModel();
			model.AlertsList = new List<AlertsViewModel>();
			try
			{
				List<Alert> alertList = await _repository.GetAlerts();
				if (alertList != null)
				{

					foreach (var alert in alertList)
					{
						var list = new AlertsViewModel()
						{
							Id = alert.Id,
							AlertText = alert.AlertText,
							OrganisationId = alert.OrganisationId,
							OrganisationName = alert.OrganisationName,
							KioskName = alert.KioskName,
							AlertsDisplayType = ((AlertDisplayType)int.Parse(alert.AlertsDisplayType)).ToString(),
							MessageType = alert.Target == "All patients" ? "Group" : "SomePatients",
							Target = alert.Target
						};
						model.AlertsList.Add(list);
					}
				}

				if (!User.IsInRole("Egton Engineer"))
				{
					List<Patient> patientMessages = await _patientRepository.GetPatientMessageList();
					var alertviewmodel = new AlertsViewModel();

					if (patientMessages != null)
					{
						foreach (var patientmsg in patientMessages)
						{
							var list = new AlertsViewModel()
							{
								Id = patientmsg.PatientMessageId,
								AlertText = patientmsg.Message,
								OrganisationId = patientmsg.OrganisationId,
								OrganisationName = patientmsg.OrganisationName,
								KioskName = "All Kiosks",
								AlertsDisplayType = "Patient Important",
								MessageType = "Specific Patient",
								Target = Convert.ToString(patientmsg.Surname.ToUpper() + (", ") + patientmsg.Firstname)
							};

							model.AlertsList.Add(list);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				ModelState.AddModelError("CustomError", ex.Message);
			}

			return model;
		}
	}
}