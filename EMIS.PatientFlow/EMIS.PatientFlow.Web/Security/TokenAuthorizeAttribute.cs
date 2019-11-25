using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EMIS.PatientFlow.Web.Security
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        public string AccessType { get; set; }
        public string Permission { get; set; }

        protected virtual TokenPrincipal CurrentUser
        {
            get { return ((TokenGenericPrincipal)HttpContext.Current.User).Instance; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (!CurrentUser.IsInRole("Practice Admin") &&
                    !CurrentUser.IsInRole("EMIS Super User") &&
					!CurrentUser.IsInRole("Egton Engineer") &&
				CurrentUser.IsInRole("Standard User") && !string.IsNullOrEmpty(AccessType))
                {
                    bool flag = true;

                    if (string.IsNullOrEmpty(Permission)) Permission = "View";

                    foreach (string item in Regex.Split(Permission, ",").Where(x => !string.IsNullOrEmpty(x)))
                    {
                        if (!CurrentUser.IsInRole(AccessType + "_" + item))
                        {
                            flag = false;
                        }
                    }

                    if (!flag)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
                    }
                }
                else
                    base.OnAuthorization(filterContext);
           }
        }
    }
}