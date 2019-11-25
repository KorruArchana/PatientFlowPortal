using System;

namespace EMIS.PatientFlow.SyncService.Filters
{
    public class AppointmentFilter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int OrganisationId { get; set; }
        public int StatusId { get; set; }
        public string SystemType { get; set; }
    }
}
