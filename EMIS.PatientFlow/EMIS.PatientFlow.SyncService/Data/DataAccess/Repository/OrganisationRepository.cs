using System.Collections.Generic;
using EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces;
using EMIS.PatientFlow.SyncService.Filters;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess.Repository
{
    public class OrganisationRepository : BaseRepository, IOrganisationRepository
    {
        public List<Appointment> GetAppointments(AppointmentFilter filter, int pageNo, int pageSize, out long recordCount)
        {
            return DbAccess.GetAppointments(filter, pageNo, pageSize, out recordCount);
        }
    }
}
