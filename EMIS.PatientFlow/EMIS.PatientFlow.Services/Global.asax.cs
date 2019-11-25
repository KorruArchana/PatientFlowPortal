using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EMIS.PatientFlow.Services.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;

namespace EMIS.PatientFlow.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(120);

            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(20);

            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(5);

            InitializeUser();			

			Hubs.ClientConnection.Instance.ResetClientConnections();
        }

        private static void InitializeUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!roleManager.RoleExists("Practice Admin"))
                roleManager.Create(new IdentityRole("Practice Admin"));

            if (!roleManager.RoleExists("EMIS Super User"))
                roleManager.Create(new IdentityRole("EMIS Super User"));

			if (!roleManager.RoleExists("Egton Engineer"))
				roleManager.Create(new IdentityRole("Egton Engineer"));

			var user = userManager.FindByName("Administrator");

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "Administrator",
                    Email = "Administrator@rename.com"
                };

                var result = userManager.Create(
                    user,
                    "A1d2m3i4n#123");
                if (result.Succeeded)
                {
                    userManager.AddToRole(
                        user.Id,
                        "EMIS Super User");
                }
            }
        }
    }
}
