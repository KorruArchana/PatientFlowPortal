﻿using System;

namespace EMIS.PatientFlow.SyncService.Data
{
    public class Log
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string UsageDate { get; set; }
    }
}
