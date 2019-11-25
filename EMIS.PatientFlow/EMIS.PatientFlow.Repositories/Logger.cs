using System;
using System.Web.Configuration;
using EMIS.PatientFlow.Common.Interfaces;
using EMIS.PatientFlow.Entities.Enums;

namespace EMIS.PatientFlow.Repositories
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
            object[] parameters = new object[] { WebConfigurationManager.ConnectionStrings["Monitoring"].ConnectionString };

            _log = Common.DiResolver.CurrentInstance.Reslove<ILogger>(parameters);
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
				case LogType.Fatal:
					_log.Fatal(message, exception, user);
					break;

			}
        }
    }

	public sealed class FileLogger
	{
		private static readonly FileLogger _single = new FileLogger();
		private readonly ILogger _fileLog;
		public static FileLogger Instance
		{
			get { return _single; }
		}

		private FileLogger()
		{
			_fileLog = Common.DiFileLogResolver.CurrentInstance.Reslove<ILogger>();
		}

		public void WriteLog(LogType level, string message, Exception exception, string user)
		{
			switch (level)
			{
				case LogType.Error:
					_fileLog.Error(message, exception, user);
					break;
				case LogType.Info:
					_fileLog.Info(message, exception, user);
					break;
				case LogType.Warn:
					_fileLog.Warn(message, exception, user);
					break;
				case LogType.Debug:
					_fileLog.Debug(message, exception, user);
					break;
				case LogType.Fatal:
					_fileLog.Fatal(message, exception, user);
					break;

			}
		}
	}
}
