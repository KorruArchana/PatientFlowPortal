using System;
using System.Web.Http;
using System.Web.Mvc;
using EMIS.PatientFlow.Services.Areas.HelpPage.Models;

namespace EMIS.PatientFlow.Services.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
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

        public static string ConfigSiteUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HomePageUrl"];
            }
        }

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            if (!IsDirect)
            {
                ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
                return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
            }

            if (string.IsNullOrEmpty(ConfigSiteUrl))
                return Redirect("https://patientflow.egton.thirdparty.nhs.uk/");
            return Redirect(ConfigSiteUrl);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View("Error");
        }
    }
}