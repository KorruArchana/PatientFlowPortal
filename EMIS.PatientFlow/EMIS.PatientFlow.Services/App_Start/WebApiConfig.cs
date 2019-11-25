using System;
using System.Web.Http;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories;
using EMIS.PatientFlow.Services.Hubs;
using EMIS.PatientFlow.Services.Resolver;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;

namespace EMIS.PatientFlow.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "ActionApi",
                routeTemplate: "api/{controller}/{action}");

            config.Routes.MapHttpRoute(
           name: "ActionParmApi",
           routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var container = new UnityContainer();
            container.RegisterType<ILoggerRepository, LoggerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IKioskRepository, KioskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISiteMenuRepository, SiteMenuRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrganisationRepository, OrganisationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILanguageRepository, LanguageRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IQuestionnaireRepository, QuestionnaireRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAlertsRepository, AlertsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDepartmentRepository, DepartmentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMemberRepository, MemberRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISurveyRepository, SurveyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISyncServiceRepository, SyncServiceRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPatientRepository, PatientRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IReportRepository, ReportRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<SyncHub, SyncHub>(new ContainerControlledLifetimeManager());
            container.RegisterType<KioskHub, KioskHub>(new ContainerControlledLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            Lazy<IUnityContainer> signalRContainer = new Lazy<IUnityContainer>(() => { return container; });
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new SignalRResolver(signalRContainer.Value));                    
        }
    }
}
