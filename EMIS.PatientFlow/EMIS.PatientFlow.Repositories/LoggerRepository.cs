using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Entities.Enums;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class LoggerRepository : BaseRepository, ILoggerRepository
    {
        public void WriteLog(LogType level, string message)
        {
            Logger.Instance.WriteLog(level, message, null, null);
        }

        public void WriteLog(LogType level, Exception exception)
        {
            Logger.Instance.WriteLog(level, null, exception, null);
        }

        public void WriteLog(LogType level, string message, Exception exception)
        {
            Logger.Instance.WriteLog(level, message, exception, null);
        }
       
        public void WriteLog(LogType level, string message, string user)
        {
            Logger.Instance.WriteLog(level, message, null, user);
        }

        public void WriteLog(LogType level, Exception exception, string user)
        {
            Logger.Instance.WriteLog(level, null, exception, user);
        }

        public void WriteLog(LogType level, string message, Exception exception, string user)
        {
           Logger.Instance.WriteLog(level, message, exception, user);
        }

        public void WriteLog(List<Log> logs)
        {
            DbAccess1.LogSave(logs);
        }
    }
}
