using System;
using System.Reflection;
using EMIS.PatientFlow.Common.Interfaces;
using log4net;

namespace EMIS.PatientFlow.Common.Logging
{
    public class DatabaseLogger : ILogger
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public DatabaseLogger(string connection)
        {
            Config.ConfigureAdoNetAppender(connection);
        }

        public void Warn(string message)
        {
            Log.Warn(message);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(Exception exception)
        {
            Log.Error(exception);
        }

        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void Warn(Exception exception)
        {
            Log.Warn(exception);
        }

        public void Info(Exception exception)
        {
            Log.Info(exception);
        }

        public void Debug(Exception exception)
        {
            Log.Debug(exception);
        }

        public void Warn(string message, Exception exception)
        {
            Log.Warn(message, exception);
        }

        public void Info(string message, Exception exception)
        {
            Log.Info(message, exception);
        }

        public void Debug(string message, Exception exception)
        {
            Log.Debug(message, exception);
        }

        public void Warn(string message, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Warn(message);
        }

        public void Info(string message, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Info(message);
        }

        public void Debug(string message, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Debug(message);
        }

        public void Error(string message, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Error(message);
        }

        public void Warn(Exception exception, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Warn(exception);
        }

        public void Info(Exception exception, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Info(exception);
        }

        public void Debug(Exception exception, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Debug(exception);
        }

        public void Error(Exception exception, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Error(exception);
        }

        public void Warn(string message, Exception exception, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Warn(message, exception);
        }

        public void Info(string message, Exception exception, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Info(message, exception);
        }

        public void Debug(string message, Exception exception, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Debug(message, exception);
        }

		public void Fatal(string message, Exception exception, string user)
		{
			log4net.GlobalContext.Properties["user"] = user;
			Log.Fatal(message, exception);
		}

		public void Error(string message, Exception exception, string user)
        {
            log4net.GlobalContext.Properties["user"] = user;
            Log.Error(message, exception);
        }
    }
}
