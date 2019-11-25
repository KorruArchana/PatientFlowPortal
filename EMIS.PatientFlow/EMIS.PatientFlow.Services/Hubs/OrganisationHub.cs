using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities.Filters;
using EMIS.PatientFlow.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace EMIS.PatientFlow.Services.Hubs
{
    [HubName("OrganisationHub")]
    public class OrganisationHub : Hub
    {
        private readonly IHubContext _hubContext;
        private readonly ILoggerRepository _logRepository;
        private readonly ISyncServiceRepository _syncRepository;
        public OrganisationHub(ILoggerRepository logRepository, ISyncServiceRepository syncRepository)
        {
            _logRepository = logRepository;
            _syncRepository = syncRepository;
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<OrganisationHub>();
        }

        public void GetAppointments(int organisationId, string fromDate, string toDate, int pageNumber, int pageSize)
        {
            if (ValidateToken(Context.QueryString["token"]))
            {
                List<string> connections = _syncRepository.GetSyncServiceConnections(organisationId, 'O');

                DateTime dtFromDate;
                DateTime dtToDate;
                var filter = new AppointmentFilter();

                DateTime.TryParseExact(fromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFromDate);
                DateTime.TryParseExact(toDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtToDate);
                filter.FromDate = dtFromDate;
                filter.ToDate = dtToDate;
                filter.StatusId = -1;
                filter.OrganisationId = organisationId.ToString();

                if (connections != null && connections.Count > 0)
                    Clients.Clients(connections).GetAppointments(filter, pageNumber, pageSize, Context.ConnectionId);
                else
                    Clients.Caller.Appointments(new
                                    {
                                        TotalCount = 0,
                                        Appointments = null as object
                                    });
            }
        }

        public void PushAppointments(dynamic items, string requestor)
        {
            _hubContext.Clients.Client(requestor).Appointments(items);
        }

        public override Task OnConnected()
        {
            var productKey = string.IsNullOrEmpty(Context.QueryString["id"]) ? Guid.Empty : new Guid(Context.QueryString["id"]);

            if (productKey != Guid.Empty)
            {
                try
                {
                    if (!_syncRepository.IsExistSyncService(productKey))
                    {
                        Clients.Caller.DisconnectFromServer();
                    }
                    else
                    {
                        var result = _syncRepository.SyncServiceConnected(productKey, new Guid(Context.ConnectionId), 'O');
                        if (result == -1)
                            Clients.Caller.DisconnectFromServer();
                    }
                }
                catch (Exception ex)
                {
                    _logRepository.WriteLog(Entities.Enums.LogType.Debug, "OrganisationHub Method : OnConnected", ex, productKey.ToString());
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var productKey = string.IsNullOrEmpty(Context.QueryString["id"]) ? Guid.Empty : new Guid(Context.QueryString["id"]);
            try
            {
                _syncRepository.SyncServiceDisconnected(productKey, new Guid(Context.ConnectionId), 'O');
            }
            catch (Exception ex)
            {
                _logRepository.WriteLog(Entities.Enums.LogType.Debug, "OrganisationHub Method : OnConnected", ex, productKey.ToString());
            }

            return base.OnDisconnected(stopCalled);
        }

        private bool ValidateToken(string token)
        {
	        var ticket = Startup.OAuthOptions.AccessTokenFormat.Unprotect(token);

	        return ticket != null && ticket.Identity != null && ticket.Identity.IsAuthenticated;
        }
    }
}