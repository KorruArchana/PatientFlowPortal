using System;

namespace EMIS.PatientFlow.Common.Interfaces
{
    public interface ILogger
    {
        void Warn(string message);
        void Info(string message);
        void Debug(string message);
        void Error(string message);
        void Warn(Exception exception);
        void Info(Exception exception);
        void Debug(Exception exception);
        void Error(Exception exception);
        void Warn(string message, Exception exception);
        void Info(string message, Exception exception);
        void Debug(string message, Exception exception);
        void Error(string message, Exception exception);

        void Warn(string message, string user);
        void Info(string message, string user);
        void Debug(string message, string user);
        void Error(string message, string user);
        void Warn(Exception exception, string user);
        void Info(Exception exception, string user);
        void Debug(Exception exception, string user);
        void Error(Exception exception, string user);
        void Warn(string message, Exception exception, string user);
        void Info(string message, Exception exception, string user);
        void Debug(string message, Exception exception, string user);
        void Error(string message, Exception exception, string user);
		void Fatal(string message, Exception exception, string user);
	}
}
