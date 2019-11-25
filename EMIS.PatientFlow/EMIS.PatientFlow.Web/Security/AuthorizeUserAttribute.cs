using System.Web.Mvc;
using System.Web.Routing;
using Enumerable = System.Linq.Enumerable;

namespace EMIS.PatientFlow.Web.Security
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                bool authorize = Enumerable.Any(
                    Roles.Split(','),
                    role => filterContext.HttpContext.User.IsInRole(role));

                if (!authorize && !string.IsNullOrEmpty(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Error",
                                action = "AccessDenied"
                            }));
                }
            }
        }
    }
}