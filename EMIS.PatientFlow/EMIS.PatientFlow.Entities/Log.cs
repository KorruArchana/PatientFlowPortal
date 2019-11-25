using System;

namespace EMIS.PatientFlow.Entities
{
    public class Log
    {
        public DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public long Id { get; set; }
        public string UsageDate { get; set; }
    }
}
