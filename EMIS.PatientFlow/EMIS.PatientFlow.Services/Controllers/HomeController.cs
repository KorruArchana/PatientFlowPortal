using System;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Services.Controllers
{
    public class HomeController : Controller
    {

        public static string ConfigSiteUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HomePageUrl"];
            }
        }

        public static bool IsDirect
        {
            get
            {
                int pageSize;
                if (!Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["Redirect"], out pageSize))
                    pageSize = 1;

                return pageSize == 1;
            }
        }

        public ActionResult Index()
        {
            if (IsDirect)
            {
                if (string.IsNullOrEmpty(ConfigSiteUrl))
                    return Redirect("https://patientflow.egton.thirdparty.nhs.uk/");
                return Redirect(ConfigSiteUrl);
            }
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
