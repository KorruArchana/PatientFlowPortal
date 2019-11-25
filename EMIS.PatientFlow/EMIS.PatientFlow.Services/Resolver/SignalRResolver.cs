using System;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;

namespace EMIS.PatientFlow.Services.Resolver
{
    public class SignalRResolver : IHubActivator
    {
        private readonly IUnityContainer _container;

        public SignalRResolver(IUnityContainer container)
        {
            _container = container;
        }
 
        public IHub Create(HubDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException("descriptor");
            }
 
            if (descriptor.HubType == null)
            {
                return null;
            }
 
            object hub = _container.Resolve(descriptor.HubType) ?? Activator.CreateInstance(descriptor.HubType);
            return hub as IHub;
        }
    }
}