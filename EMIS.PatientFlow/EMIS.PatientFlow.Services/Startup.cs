﻿using System.Configuration;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(EMIS.PatientFlow.Services.Startup))]

namespace EMIS.PatientFlow.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
			string signalRConnectionString = ConfigurationManager.ConnectionStrings["SignalR"].ConnectionString;
            GlobalHost.DependencyResolver.UseSqlServer(
                new SqlScaleoutConfiguration(signalRConnectionString)
                {
                    TableCount = 3
                });
            
            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                };
                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });
            
            ConfigureAuth(app);
        }
    }
}
