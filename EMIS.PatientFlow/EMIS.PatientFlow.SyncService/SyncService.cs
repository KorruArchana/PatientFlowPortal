using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces;
using EMIS.PatientFlow.SyncService.Helper;
using EMIS.PatientFlow.SyncService.Hubs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.SyncService
{
    public partial class SyncService : ServiceBase
    {
        private readonly IConfigurationRepository _repository;
        private readonly ManualResetEvent _stop = new ManualResetEvent(false);
        private readonly string _schedules = string.Empty;
        private bool _isConfigSet = true;
        private bool _isZeroAppointmentPresent;
        private const int LogSyncTiming = 5;
        private readonly string _registrationKey;
        private DateTime? _nextCheck;
		private DateTime? _nextMemberCheck;
		private bool _isWeekendFlag;

        public SyncService()
        {
            InitializeComponent();

            ServiceName = "EMIS PatientFlow SyncService";
            _schedules = Utility.GetAppSettingValue("Schedules");
            _registrationKey = Utility.GetAppSettingValue("ProductKey");

            _repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
        }

        protected override void OnStart(string[] args)
        {
            _stop.Reset();
            StartHub();
            ThreadPool.RegisterWaitForSingleObject(_stop, SyncServerConnection, null, 15000, false);
            SyncSurveyFromServer();
            UpdateStatusToEventLogs();
            UpdateVersion();
            SyncMembers();
		}

		private void SyncMembers()
		{
			if (_isConfigSet)
			{
				//send creential and orgId
				//AppointmentHelper.SyncMembers();
			}
		}

		private void UpdateStatusToEventLogs()
        {
            string message = "EMIS PatientFlow SyncService version :" + Assembly.GetExecutingAssembly().GetName().Version;
            EventLog.WriteEntry(message, EventLogEntryType.Information);
            Logger.Instance.WriteLog(LogType.Info, message + " - Started at " + DateTime.Now.ToString("dd-MMM hh:mm:ss tt"), null, _registrationKey);
        }

        private void UpdateVersion()
        {
            try
            {

                string registry = @"SOFTWARE\CENTRASTAGE\";
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registry, true);
                if (regKey == null)
                {
                    regKey = Registry.LocalMachine.CreateSubKey(registry);
                }
                if (regKey != null)
                {
                    regKey.SetValue("Custom3", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Unable to update version the value into Registry", EventLogEntryType.FailureAudit);
                Logger.Instance.WriteLog(LogType.Error, "Unable to update version the value into Registry", ex, _registrationKey);
            }
        }

        private static void StartHub()
        {
            SyncHubClient.Instance.StartHub();
            //OrganisationHubClient.Instance.StartHub();
        }

        private void SyncSurveyFromServer()
        {
            Task.Factory.StartNew(() =>
            {
                PeriodicProcess();
                RunScheduler(GetNextSchedule(DateTime.UtcNow.AddMinutes(1)));
            });

            Task.Factory.StartNew(() => RunLogScheduler(DateTime.UtcNow.AddSeconds(30)));
        }

        protected override void OnStop()
        {
            _stop.Set();

            SyncHubClient.Instance.CloseHub();
            //OrganisationHubClient.Instance.CloseHub();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        private void SyncServerConnection(object state, bool timedOut)
        {
            if (timedOut)
            {
                SyncHubClient.Instance.StartHub();
                //OrganisationHubClient.Instance.StartHub();
            }
        }

        private Task Delay(int milliseconds)
        {
            var tcs = new TaskCompletionSource<object>();

            try
            {
                new Timer(p => tcs.SetResult(null)).Change(milliseconds, -1);
            }
            catch
            {
                return Delay(GetTimeSpan(DateTime.UtcNow).Milliseconds);
            }

            return tcs.Task;
        }

        private DateTime GetNextSchedule(DateTime date)
        {
            DateTime nextSchedule = DateTime.MinValue, tempDate = DateTime.UtcNow;

            var formats = new Dictionary<string, string>
            {
                {"M", @"every ([0-9]+)\ minutes$"},
                {"H", @"every ([0-9]+)\ hours$"},
                {"D", @"every ([0-9]+)\ days$"},
                {"F", string.Empty}
            };

            foreach (string key in formats.Keys)
            {
                if (key == "F")
                {
                    do
                    {
                        nextSchedule = _schedules
                            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => new DateTime(
                                tempDate.Year,
                                tempDate.Month,
                                tempDate.Day,
                                int.Parse(x.Split(new[] { ':' })[0]),
                                int.Parse(x.Split(new[] { ':' })[1]),
                                0)).OrderBy(z => z).FirstOrDefault(y => y > date);

                        tempDate = tempDate.AddDays(1).Date;
                    } while (nextSchedule == DateTime.MinValue);
                }
                else
                {
                    var match = Regex.Match(_schedules, formats[key], RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        var val = Convert.ToInt32(match.Groups[1].Value);
                        switch (key)
                        {
                            case "M":
                                nextSchedule = date.AddMinutes(val);
                                break;
                            case "H":
                                nextSchedule = date.AddHours(val);
                                break;
                            case "D":
                                nextSchedule = date.AddDays(val);
                                break;
                        }
                    }
                }

                if (nextSchedule != DateTime.MinValue) break;
            }

            return nextSchedule;
        }

        private void RunScheduler(DateTime date)
        {
            var ts = GetTimeSpan(date);

            Delay((int)ts.TotalMilliseconds).ContinueWith(x =>
            {
                PeriodicProcess();

                DateTime dateTime = !_isConfigSet
                    ? DateTime.UtcNow.AddMinutes(1)
                    : _isZeroAppointmentPresent ? DateTime.UtcNow.AddMinutes(60) : GetNextSchedule(date);
                RunScheduler(dateTime);
            });
        }

        private void RunLogScheduler(DateTime date)
        {
            var ts = GetTimeSpan(date);

            Delay((int)ts.TotalMilliseconds).ContinueWith(x =>
            {
                CheckSyncSettings();
                RunLogScheduler(date.AddMinutes(_isWeekendFlag ? 60 : LogSyncTiming));
            });
        }

        private TimeSpan GetTimeSpan(DateTime date)
        {
            TimeSpan ts;
            if (date > DateTime.UtcNow)
                ts = date - DateTime.UtcNow;
            else
            {
                date = GetNextSchedule(date);
                ts = date - DateTime.UtcNow;
            }
            return ts;
        }

        private void CheckSyncSettings()
        {
            try
            {
                //var modifiedDate = _repository.GetModifiedDate(Convert.ToInt32(SyncType.Questionnaire));
                if (!_isConfigSet)
                {
                    SyncHubClient.Instance.GetUpdatedConfiguration();
                }
                if (_nextCheck == null || _nextCheck < DateTime.UtcNow)
                {
                    SyncHubClient.Instance.SaveAllQuestionnaires();
                    _nextCheck = DateTime.UtcNow.AddMinutes(60);
                }
                else
                {
                    SyncHubClient.Instance.SaveQuestionnairesByTimeStamp();
                }

                SyncHubClient.Instance.IsConnected();
                SyncHubClient.Instance.SyncLog();
                SyncHubClient.Instance.SyncAnonymousSurvey();
                _isWeekendFlag = (DateTime.UtcNow.TimeOfDay.Hours < 07 || DateTime.UtcNow.TimeOfDay.Hours > 19);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
                EventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
        }

        private void SaveSyncProductKey(string productKey)
        {
            try
            {
                _repository.SaveProductKey(productKey);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
                EventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
        }

        private void PeriodicProcess()
        {
            bool isError = true;
            _isConfigSet = false;
            try
            {
                var webconfigvalues = _repository.GetWebClientConfiguration(Utility.GetAppSettingValue("SystemType"));

                var orgList = _repository.GetKioskOrganisation();
                if (webconfigvalues == null || webconfigvalues.Count == 0)
                {
                    Logger.Instance.WriteLog(LogType.Info, "Client Credentials not available. Requested the Portal", null, _registrationKey);
                }
                else
                {
                    _isConfigSet = true;
                    foreach (string item in webconfigvalues)
                    {
                        try
                        {
                            var credential = item.ConvertFromJsonString<API.Credential>();
                            if (orgList.Count == 0 ||
                                (orgList.Count > 0 && orgList.Exists(a => a.DatabaseName == credential.OrganisationKey)))
                            {
                                credential.Password = credential.Password.DecryptAES256();
                                credential.UserName = credential.UserName.DecryptAES256();
                                credential.SupplierId = credential.SupplierId.DecryptAES256();
                                credential.WebServiceUrl = credential.WebServiceUrl;
                                _isZeroAppointmentPresent = AppointmentHelper.SyncBookedAppointments(credential);

								if (credential.SystemType == SystemType.TPPSystmOne)
								{
									if (_nextMemberCheck == null || _nextMemberCheck < DateTime.UtcNow)
									{
										AppointmentHelper.SyncMembers(credential, credential.OrganisationId);
										_nextMemberCheck = DateTime.UtcNow.AddHours(6);
									}
								}

								Logger.Instance.WriteLog(LogType.Info, "Info: Appointment Syncing is done for Org: " + credential.OrganisationKey, null, _registrationKey);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
                            EventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                            isError = false;
                            _isConfigSet = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
                EventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                isError = false;
            }
            finally
            {
                Logger.Instance.WriteLog(LogType.Info, "Completed - SyncBookedAppointments" + (isError ? string.Empty : " with Errors"), null, _registrationKey);
            }
        }
    }
}
