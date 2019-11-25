using System;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Interfaces;
using EMIS.PatientFlow.Web.Resolver;

namespace EMIS.PatientFlow.Web.Helper
{
    public sealed class Logger
    {
        private static readonly Logger _single = new Logger();
        private readonly ILogger _log;
        public static Logger Instance 
        {
            get { return _single; } 
        }

        private Logger()
        {
            _log = DiResolver.CurrentInstance.Reslove<ILogger>();
        }

        public void WriteLog(LogType level, string message, Exception exception, string user)
        {           
            switch (level)
            {
                case LogType.Error:
                    _log.Error(message, exception, user);
                    break;
                case LogType.Info:
                    _log.Info(message, exception, user);
                    break;
                case LogType.Warn:
                    _log.Warn(message, exception, user);
                    break;
                case LogType.Debug:
                    _log.Debug(message, exception, user);
                    break;
            }
        }
    }
}
