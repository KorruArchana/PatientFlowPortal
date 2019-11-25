using System.Collections.Generic;
using EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces;
using EMIS.PatientFlow.SyncService.Filters;
using EMIS.PatientFlow.SyncService.Helper;
using Microsoft.AspNet.SignalR.Client;

namespace EMIS.PatientFlow.SyncService.Hubs
{
    public sealed class OrganisationHubClient : BaseHubClient
    {
        private const string _hubName = "OrganisationHub";        
        private static readonly OrganisationHubClient _single = new OrganisationHubClient();       
        public static OrganisationHubClient Instance
        {
            get { return _single; }
        }

        private OrganisationHubClient()
        {
            Init();

            if (HubProxy == null)
            {
                HubProxy = HubConnection.CreateHubProxy(_hubName);

                HubProxy.On<AppointmentFilter, int, int, string>(
                    "GetAppointments",
                    (filter, pageNo, pageSize, requestor) =>
                    {
                        var repository = DiResolver.CurrentInstance.Reslove<IOrganisationRepository>();
                        long count;
                        List<Data.Appointment> appointments = repository.GetAppointments(filter, pageNo, pageSize, out count);

                        if (!(HubConnection.State == ConnectionState.Connected
                       || HubConnection.State == ConnectionState.Reconnecting
                        || HubConnection.State == ConnectionState.Connecting))
                            HubConnection.Start().Wait();

                        HubProxy.Invoke(
                            "PushAppointments",
                            new
                                {
                                    TotalCount = count,
                                    Appointments = appointments
                                },
                            requestor);
                    });
           }
        }
    }
}
