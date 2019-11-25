using System;

namespace EMIS.PatientFlow.Entities.Filters
{
    public class AppointmentFilter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string OrganisationId { get; set; }
        public int StatusId { get; set; }
    }
}
