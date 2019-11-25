using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Controllers
{
	[Authorize(Roles = "EMIS Super User,Egton Engineer")]
	public class EgtonKioskController : Controller
	{
		private readonly IKioskRepository _repository;
		public EgtonKioskController(IKioskRepository repository)
		{
			_repository = repository;
		}

		public async Task<ActionResult> Index()
		{
			var model = new KioskListingViewModel();
			model.KioskList = new List<Kiosk>();
			try
			{
				await GetKiosks(model);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return null;
			}

			return PartialView("Index", model);
		}

		private async Task GetKiosks(KioskListingViewModel model)
		{
			List<Kiosk> kioskList = await _repository.GetKiosksWithUsageLog();

			if (kioskList != null)
			{
				model.KioskList = kioskList.Select(kiosk => new Kiosk
				{
					Id = kiosk.Id,
					KioskName = kiosk.KioskName,
					Title = kiosk.Title,
					KioskGuid = kiosk.KioskGuid,
					PcName = kiosk.PcName,
					IpAddress = kiosk.IpAddress,
					KioskVersion = kiosk.KioskVersion,
					Status = kiosk.Status,
					Usage = kiosk.Usage,
					OrganisationName = kiosk.OrganisationName,
					ConnectionGuid = kiosk.ConnectionGuid,
					KioskStatus = kiosk.KioskStatus
				}).ToList();
			}
		}

		public async Task<ActionResult> GetKiosksData()
		{
			var model = new KioskListingViewModel();
			try
			{
				await GetKiosks(model);
				List<Kiosk> kioskData = model.KioskList;

				int sortColumn = -1;
				string sortDirection = "asc";
				var result = new List<Kiosk>();
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
							result = sortDirection == "desc" ? kioskData.OrderByDescending(m => m.KioskName).ToList()
													 : kioskData.OrderBy(m => m.KioskName).ToList();

							break;
						case 2:
							result = sortDirection == "desc" ? kioskData.OrderByDescending(m => m.Title).ToList()
													 : kioskData.OrderBy(m => m.Title).ToList();

							break;
						case 3:
							result = sortDirection == "desc" ? kioskData.OrderByDescending(m => m.IpAddress).ToList()
													 : kioskData.OrderBy(m => m.IpAddress).ToList();

							break;
						case 4:
							result = sortDirection == "desc" ? kioskData.OrderByDescending(m => m.PcName).ToList()
													 : kioskData.OrderBy(m => m.PcName).ToList();

							break;
						case 5:
							result = sortDirection == "desc" ? kioskData.OrderByDescending(m => m.KioskVersion).ToList()
													 : kioskData.OrderBy(m => m.KioskVersion).ToList();

							break;
						case 6:
							result = sortDirection == "desc" ? kioskData.OrderByDescending(m => m.Usage).ToList()
													 : kioskData.OrderBy(m => m.Usage).ToList();

							break;
						case 7:
							result = sortDirection == "desc" ? kioskData.OrderByDescending(m => m.KioskStatus).ToList()
													 : kioskData.OrderBy(m => m.KioskStatus).ToList();

							break;
						case 8:
							result = sortDirection == "desc" ? kioskData.OrderByDescending(m => m.OrganisationName).ToList()
													 : kioskData.OrderBy(m => m.OrganisationName).ToList();

							break;
						default:
							result = kioskData.OrderBy(m => m.KioskName).ToList();
							break;
					}
				}

				string kioskNameFilter = Request.QueryString["columns[1][search][value]"] ?? Request.QueryString["columns[1][search][value]"].ToString();
				string kioskTitleFilter = Request.QueryString["columns[2][search][value]"] ?? Request.QueryString["columns[2][search][value]"].ToString();
				string ipAddressFilter = Request.QueryString["columns[3][search][value]"] ?? Request.QueryString["columns[3][search][value]"].ToString();
				string hostNameFilter = Request.QueryString["columns[4][search][value]"] ?? Request.QueryString["columns[4][search][value]"].ToString();
				string kioskVersionFilter = Request.QueryString["columns[5][search][value]"] ?? Request.QueryString["columns[5][search][value]"].ToString();
				string kioskUsageFilter = Request.QueryString["columns[6][search][value]"] ?? Request.QueryString["columns[6][search][value]"].ToString();
				string connectionStatusFilter = Request.QueryString["columns[7][search][value]"] ?? Request.QueryString["columns[7][search][value]"].ToString();
				string organisationNameFilter = Request.QueryString["columns[8][search][value]"] ?? Request.QueryString["columns[8][search][value]"].ToString();

				if (!string.IsNullOrWhiteSpace(kioskNameFilter))
				{
					result = result.Where(x => x.KioskName.IndexOf(kioskNameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(kioskTitleFilter))
				{
					result = result.Where(x => x.Title.ToString().IndexOf(kioskTitleFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(ipAddressFilter))
				{
					result = result.Where(x => x.IpAddress.IndexOf(ipAddressFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(hostNameFilter))
				{
					result = result.Where(x => x.PcName.IndexOf(hostNameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(kioskVersionFilter))
				{
					result = result.Where(x => x.KioskVersion.IndexOf(kioskVersionFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(kioskUsageFilter))
				{
					result = result.Where(x => x.Usage == Convert.ToInt32(kioskUsageFilter)).ToList();
				}
				if (!string.IsNullOrWhiteSpace(connectionStatusFilter))
				{
					result = result.Where(x => x.KioskStatus.IndexOf(connectionStatusFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}
				if (!string.IsNullOrWhiteSpace(organisationNameFilter))
				{
					result = result.Where(x => x.OrganisationName.IndexOf(organisationNameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				}

				model.draw = int.Parse(Request.QueryString["draw"]);
				int start = int.Parse(Request.QueryString["start"]);
				int length = int.Parse(Request.QueryString["length"]);

				model.data = result.Skip(start).Take(length).ToArray();
				model.recordsTotal = kioskData.Count();
				model.recordsFiltered = result.Count();
				model.KioskList = new List<Kiosk>();
				return Json(model, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				return (Json(model, JsonRequestBehavior.AllowGet));
			}
		}

	}
}