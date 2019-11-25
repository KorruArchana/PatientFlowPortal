using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EMIS.PatientFlow.Web.Controllers
{
	[OutputCache(Duration = 0)]
	[AuthorizeUser(Roles = "EMIS Super User")]
	public class SyncServiceController : Controller
	{
		private readonly ISyncServiceRepository _repository;
		private readonly IOrganisationRepository _organisationRepository;
		private readonly IKioskRepository _kiokRepository;
		public SyncServiceController(ISyncServiceRepository repository, IOrganisationRepository organisationRepository, IKioskRepository kiokRepository)
		{
			_repository = repository;
			_organisationRepository = organisationRepository;
			_kiokRepository = kiokRepository;
		}

		[HttpGet]
		public ActionResult Index()
		{
			return PartialView();
		}

		[HttpPost]
		public async Task<ActionResult> Index(SyncServiceListViewModel model)
		{
			model.Services = new List<SyncService>();
			try
			{
				model.Services = await _repository.GetSyncServices(model.OrganisationName, 1, Config.PageSize);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

				ModelState.AddModelError("CustomError", ex.Message);
			}

			return PartialView(model);
		}

		public ActionResult Register()
		{
			var model = new SyncServiceViewModel();
			try
			{
				model.Service = new SyncService();
				model.Service.ProductKey = Guid.NewGuid();
				model.Service.IsActive = true;
				model.OrganisationList = new List<SelectListItem>();
				model.KioskList = new List<SelectListItem>();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

				ModelState.AddModelError("CustomError", ex.Message);
			}

			return PartialView(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(SyncServiceViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					model.Service.OrganisationIds = model.OrganisationIds;
					model.Service.KioskId = model.KioskIds.First();

					var result = await _repository.SaveSyncServiceOrganisations(model.Service);

					if (result)
						return RedirectToAction("Index", "SyncService", new { nodeId = (int)NodeType.SyncServices });
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

				ModelState.AddModelError("CustomError", ex.Message);
			}

			var organisationList = new List<SelectListItem>();

			if (model.OrganisationIds != null)
			{
				var organisations = await _organisationRepository.GetOrganisationList(model.OrganisationIds);

				if (organisations != null)
				{
					organisationList = (from org in organisations
										select new SelectListItem
										{
											Text = org.OrganisationName,
											Value = org.Id.ToString()
										}).ToList();
				}
			}

			model.OrganisationList = organisationList;

			return PartialView(model);
		}

		public async Task<ActionResult> UpdateService(int serviceId)
		{
			var model = new SyncServiceViewModel();

			try
			{
				model.Service = await _repository.GetSyncServiceById(serviceId);

				model.OrganisationIds = model.Service.OrganisationIds;

				model.OrganisationList = new List<SelectListItem>();

				var organisations = await _organisationRepository.GetOrganisationList(model.OrganisationIds);
				organisations = organisations.OrderBy(d => model.OrganisationIds.IndexOf(d.Id)).ToList();

				var organisationList = (from org in organisations
										select new SelectListItem
										{
											Text = org.OrganisationName,
											Value = org.Id.ToString()
										}).ToList();

				if (organisationList.Count > 0)
				{
					model.OrganisationList = organisationList;

					model.KioskList = new List<SelectListItem>();

					var kiosklst = await _kiokRepository.GetKioskDetailListForOrganisation(model.OrganisationIds.Last());

					if (model.Service.KioskId > 0)
						model.KioskIds = new List<int>() { model.Service.KioskId };
					var klist = (from org in kiosklst
								 select new SelectListItem
								 {
									 Text = org.KioskName,
									 Value = org.Id.ToString(CultureInfo.InvariantCulture),
									 Selected = org.Id == model.Service.KioskId
								 }).ToList();


					model.KioskList = klist;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				ModelState.AddModelError("CustomError", ex.Message);
			}

			return PartialView(model);
		}

		public async Task<ActionResult> DeleteSyncService(int serviceId)
		{
			try
			{
				await _repository.DeleteSyncService(serviceId);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				ModelState.AddModelError("CustomError", ex.Message);
			}

			return RedirectToAction("Index", "SyncService", new { nodeId = (int)NodeType.SyncServices });
		}

		public async Task<JsonResult> UpdateSyncServiceStatus(int serviceId, bool isActive)
		{
			try
			{
				await _repository.UpdateSyncServiceStatus(serviceId, isActive);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				ModelState.AddModelError("CustomError", ex.Message);
			}

			return Json(true, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public async Task<JsonResult> GetSyncService(int pageNumber, int pageSize, string organisationName)
		{
			var model = new SyncServiceListViewModel();

			try
			{
				JToken data = await _repository.GetSyncServices(organisationName, pageNumber, pageSize);

				model.Services = data.Value<JArray>("Services").ToObject<List<SyncService>>();

				model.TotalCount = data.Value<int>("TotalCount");
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

				ModelState.AddModelError("CustomError", ex.Message);
			}

			return Json(model, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public async Task<JsonResult> GetOrganisations(string searchTerm)
		{
			var results = new List<dynamic>();

			List<Organisation> OrganisationList = await _organisationRepository.GetOrganisations();

			var organisations=OrganisationList.Select(org=>new SelectListItem
			{
				Text = org.OrganisationName,
				Value = org.Id.ToString()
			});

			return Json(organisations, JsonRequestBehavior.AllowGet);
		}
	}
}