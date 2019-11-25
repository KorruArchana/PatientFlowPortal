using System.Collections.Generic;
using EMIS.PatientFlow.SyncService.Filters;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces
{
    interface IOrganisationRepository
    {
        List<Appointment> GetAppointments(AppointmentFilter filter, int pageNo, int pageSize, out long recordCount);
    }
}
