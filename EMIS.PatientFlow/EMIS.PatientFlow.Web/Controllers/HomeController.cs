using System.Configuration;
using System.Web.Mvc;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Controllers
{
    [OutputCache(Duration = 0)]
    [Authorize]
    public class HomeController : Controller
    {
		public HomeController()
		{
		}

		[OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
		public ActionResult Index()
		{
            ViewBag.HeaderMessage = Config.GetAppSettingHomePageHeaderValue;
            ViewBag.BodyMessage = Config.GetAppSettingHomePageBodyValue;

            return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		public ActionResult CookiePage()
		{
			ViewBag.Message = "Your application description page.";
			return View();
		}
	}
}