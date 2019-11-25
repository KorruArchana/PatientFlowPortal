using System;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace EMIS.PatientFlow.Common.Logging
{
	public static class Config
	{
		private static Level _logLevel = null;
		const string AppConfigKey = "LoggingEnabled";

		public static Level LogLevel
		{
			get
			{
				if (_logLevel == null)
					SetLogLevel();
				return _logLevel;
			}
		}


		private static void SetLogLevel()
		{
			switch (GetAppSettingValue(AppConfigKey))
			{
				case "1":
					_logLevel = Level.All;
					break;
				case "2":
					_logLevel = Level.Fatal;
					break;
				default:
					_logLevel = Level.Off;
					break;
			}
		}

		public static void ConfigureFileAppender()
		{
			RootLogger.AddAppender(CreateRollingFileAppender(LogLevel));
			RootLogger.Repository.Configured = true;
		}

		public static void ConfigureAdoNetAppender(string connection)
		{
			RootLogger.AddAppender(CreateAdoNetAppender(LogLevel, connection));
			RootLogger.Repository.Configured = true;
		}

		private static Logger RootLogger
		{
			get
			{
				return ((Hierarchy)LogManager.GetRepository()).Root;
			}
		}

		private static RollingFileAppender CreateRollingFileAppender(Level level)
		{
			string usingFileName = string.Format(
				"Logs\\{0}-{1}-{2}_{3}.log",
				DateTime.Today.Year,
				DateTime.Today.Month,
				DateTime.Today.Day,
				level.Name);
			var layout = new PatternLayout("%date %property{user} %-5level - %message%newline");
			var rollingFileAppender = new RollingFileAppender
			{
				Layout = layout,
				AppendToFile = true,
				RollingStyle = RollingFileAppender.RollingMode.Date,
				File = usingFileName,
				ImmediateFlush = true,
				Threshold = level
			};
			rollingFileAppender.ActivateOptions();

			return rollingFileAppender;
		}

		private static AdoNetAppender CreateAdoNetAppender(Level level, string connection)
		{
			var appender = new AdoNetAppender()
			{
				Name = "AdoNetAppender",
				BufferSize = 1,
				ReconnectOnError = true,
				ConnectionType = "System.Data.SqlClient.SqlConnection",
				ConnectionString = connection,
				CommandType = System.Data.CommandType.Text,
				CommandText = @"INSERT INTO [PatientFlow].[Log]([Date],[Thread],[Level],[Logger],[User],[Message],[Exception]) 
                                VALUES (@date,@thread,@level,@logger,@user,@message,@exception)",
				Threshold = level
			};

			appender.AddParameter(AddDateTimeParameter("date"));
			appender.AddParameter(AddStringParameter("thread", 20, "%thread"));
			appender.AddParameter(AddStringParameter("level", 10, "%level"));
			appender.AddParameter(AddStringParameter("logger", 200, "%logger"));
			appender.AddParameter(AddStringParameter("user", 50, "%property{user}"));
			appender.AddParameter(AddStringParameter("message", 1000, "%message"));
			appender.AddParameter(AddErrorParameter("exception", 4000));

			appender.ActivateOptions();

			return appender;
		}

		public static AdoNetAppenderParameter AddStringParameter(string paramName, int size, string conversionPattern)
		{
			var param = new AdoNetAppenderParameter();
			param.ParameterName = paramName;
			param.DbType = System.Data.DbType.String;
			param.Size = size;
			param.Layout = new Layout2RawLayoutAdapter(new PatternLayout(conversionPattern));
			return param;
		}

		public static AdoNetAppenderParameter AddDateTimeParameter(string paramName)
		{
			var param = new AdoNetAppenderParameter();
			param.ParameterName = paramName;
			param.DbType = System.Data.DbType.DateTime;
			param.Layout = new RawTimeStampLayout();
			return param;
		}

		public static AdoNetAppenderParameter AddErrorParameter(string paramName, int size)
		{
			var param = new AdoNetAppenderParameter();
			param.ParameterName = paramName;
			param.DbType = System.Data.DbType.String;
			param.Size = size;
			param.Layout = new Layout2RawLayoutAdapter(new ExceptionLayout());
			return param;
		}

		private static string GetAppSettingValue(string key)
		{
			return System.Configuration.ConfigurationManager.AppSettings[key];
		}
	}
}
