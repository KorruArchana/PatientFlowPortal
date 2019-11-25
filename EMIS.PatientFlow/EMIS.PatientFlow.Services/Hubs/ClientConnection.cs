using System;
using System.Web.Http;
using EMIS.PatientFlow.Interfaces;

namespace EMIS.PatientFlow.Services.Hubs
{
    public sealed class ClientConnection
    {
        private static readonly ClientConnection _single = new ClientConnection();
        private readonly ISyncServiceRepository _syncRepository;
        private readonly ILoggerRepository _logRepository;
        public static ClientConnection Instance 
        {
            get { return _single; } 
        }

        private ClientConnection()
        {
            _syncRepository = (ISyncServiceRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISyncServiceRepository));
            _logRepository = (ILoggerRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILoggerRepository));
        }

        public void ResetClientConnections()
        {
            try
            {
                _syncRepository.RemoveClientConnections('A');
            }
            catch (Exception ex)
            {
                _logRepository.WriteLog(Entities.Enums.LogType.Error, "ResetClientConnections", ex);
            }
        }
    }
}