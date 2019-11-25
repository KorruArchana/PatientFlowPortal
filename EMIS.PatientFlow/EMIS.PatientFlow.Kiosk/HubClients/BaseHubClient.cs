using System;
using System.Collections.Generic;
using System.Threading;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Helper;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNet.SignalR.Client.Transports;

namespace EMIS.PatientFlow.Kiosk.HubClients
{
    public abstract class BaseHubClient : IDisposable
    {
        public IHubProxy HubProxy;
        public HubConnection HubConnection;
        private readonly string _serverUri = Utilities.GetAppSettingValue("ServerURI");
        private readonly string _registrationKey = Utilities.GetAppSettingValue("RegistrationKey");

        public ConnectionState State
        {
            get { return HubConnection.State; }
        }

        protected void Init()
        {
            var querystringData = new Dictionary<string, string>();
            querystringData.Add("registrationKey", _registrationKey);

            if (HubConnection == null)
                HubConnection = new HubConnection(_serverUri, querystringData);
        }

        public void CloseHub()
        {
            GlobalVariables.IsUsedKey = true;
            HubConnection.Stop();
            HubConnection.Dispose();
            GlobalVariables.IsUsedKey = false;
        }

        public bool StartHub()
        {
            bool isSuccess = true;
            try
            {
                var connectionTrial = 3;
                var connectionState = true;
                while (connectionTrial > 0 && connectionState)
                {
                    try
                    {
                        if (!(HubConnection.State == ConnectionState.Connected
                           || HubConnection.State == ConnectionState.Reconnecting
                            || HubConnection.State == ConnectionState.Connecting))
                        {
							try
							{
								var httpClient = new DefaultHttpClient();
								List<IClientTransport> transports = new List<IClientTransport>()
								{
									new ServerSentEventsTransport(httpClient),
									new LongPollingTransport(httpClient)
								};
								HubConnection.Start(new AutoTransport(httpClient, transports)).Wait();
							}
							catch(Exception ex)
							{
								HubConnection.Start().Wait();
							}

							connectionState = false;
                        }
                        else
                            connectionState = false;
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteLog(LogType.Debug, ex.Message, HubConnection.LastError, _registrationKey);
                        Thread.Sleep(500);
                        connectionTrial--;
                    }

                    if (connectionTrial == 0 && connectionState)
                        isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Debug, ex.Message, ex, "EMIS PatientFlow Kiosk hub client");
            }

            return isSuccess;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CloseHub();
            }
        }
    }
}
