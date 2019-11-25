using System;

namespace EMIS.PatientFlow.Entities
{
    public class AuditTrial : Entity
    {
        public DateTime Date { get; set; }
        public int Thread { get; set; }
        public string Level { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string DateToDisplay { get; set; }
    }
}
