using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using EMIS.PatientFlow.Web.Security;
using Newtonsoft.Json;

namespace EMIS.PatientFlow.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (authTicket != null)
                {
                    TokenPrincipalSerializeModel data = JsonConvert.DeserializeObject<TokenPrincipalSerializeModel>(authTicket.UserData);
                
                    TokenPrincipal user = new TokenPrincipal(authTicket.Name);
                    user.Name = data.Name;
                    user.Token = data.Token;
                    user.Roles = data.Roles;

                    var genericPrinciple = new TokenGenericPrincipal(user);

                    HttpContext.Current.User = genericPrinciple;
                }
            }
        }
    }
}
