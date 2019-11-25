using System.Web.Mvc;
using EMIS.PatientFlow.Web.Repository;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace EMIS.PatientFlow.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAuthenticationRepository, AuthenticationRepository>();
            container.RegisterType<IOrganisationRepository, OrganisationRepository>();
			//container.RegisterType<ISiteMenuRepository, SiteMenuRepository>();
			container.RegisterType<IAlertRepository, AlertRepository>();
            container.RegisterType<IQuestionnaireRepository, QuestionnaireRepository>();
            container.RegisterType<IMemberRepository, MemberRepository>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<IHomeRepository, HomeRepository>();
            container.RegisterType<IKioskRepository, KioskRepository>();
            //container.RegisterType<ISyncServiceRepository, SyncServiceRepository>();
            container.RegisterType<IPatientRepository, PatientRepository>();
            container.RegisterType<IReportRepository, ReportRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}