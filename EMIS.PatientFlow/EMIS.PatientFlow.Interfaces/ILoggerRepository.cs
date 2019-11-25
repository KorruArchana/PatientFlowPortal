using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Entities.Enums;

namespace EMIS.PatientFlow.Interfaces
{
    public interface ILoggerRepository
    {
        void WriteLog(LogType level, string message);
        void WriteLog(LogType level, string message, string user);
        void WriteLog(LogType level, Exception exception);
        void WriteLog(LogType level, Exception exception, string user);
        void WriteLog(LogType level, string message, Exception exception);
        void WriteLog(LogType level, string message, Exception exception, string user);
        void WriteLog(List<Log> logs);
    }
}
