using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Error/
        public ActionResult Index(HandleErrorInfo model)
        {
            return PartialView(model);
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            ViewData["error_path"] = aspxerrorpath;
            
            return PartialView();
        }
       
        public ActionResult AccessDenied()
        {
            return PartialView();
        }
	}
}