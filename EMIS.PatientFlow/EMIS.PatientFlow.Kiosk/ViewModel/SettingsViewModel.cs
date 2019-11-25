using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class SettingsViewModel : ViewModelBase
	{
		private string _welcomeText;
		private string _enterYourPinText;
		private bool _isCorrectPin;
		private bool? _isAdminLoginVisible;
		private bool? _isAdminDetailsVisible;
		private string _passwordDigit1;
		private string _passwordDigit2;
		private string _passwordDigit3;
		private string _passwordDigit4;
		private int _index;
		private RelayCommand<string> _numberClickCommand;

		private RelayCommand<object> _shutDownCommand;
		private RelayCommand<object> _restartCommand;
		private RelayCommand _exitCommand;

		private bool? _isSoftwareVersionVisible;
		private bool? _isIPAddressVisible;
		private bool? _isServerUriVisible;
		private bool? _isKioskKeyVisible;
		private bool? _isSyncServiceKeyVisible;
		private bool? _isKioskSerialNumberVisible;
        private bool _isDeleteEnabled;

        public String WelcomeText
		{
			get
			{
				return _welcomeText;
			}
			set
			{
				_welcomeText = value;
				RaisePropertyChanged("WelcomeText");
			}
		}

		public String EnterYourPinText
		{
			get
			{
				return _enterYourPinText;
			}
			set
			{
				_enterYourPinText = value;
				RaisePropertyChanged("EnterYourPinText");
			}
		}

		public bool IsCorrectPin
		{
			get
			{
				return _isCorrectPin;
			}
			set
			{
				_isCorrectPin = value;
				RaisePropertyChanged("IsCorrectPin");
			}
		}

		public bool? IsAdminLoginVisible
		{
			get
			{
				return _isAdminLoginVisible;
			}
			set
			{
				_isAdminLoginVisible = value;
				RaisePropertyChanged("IsAdminLoginVisible");
			}
		}

		public bool? IsAdminDetailsVisible
		{
			get
			{
				return _isAdminDetailsVisible;
			}
			set
			{
				_isAdminDetailsVisible = value;
				RaisePropertyChanged("IsAdminDetailsVisible");
			}
		}

		public String PasswordDigit1
		{
			get
			{
				return _passwordDigit1;
			}
			set
			{
				_passwordDigit1 = value;
				RaisePropertyChanged("PasswordDigit1");
			}
		}

		public String PasswordDigit2
		{
			get
			{
				return _passwordDigit2;
			}
			set
			{
				_passwordDigit2 = value;
				RaisePropertyChanged("PasswordDigit2");
			}
		}

		public String PasswordDigit3
		{
			get
			{
				return _passwordDigit3;
			}
			set
			{
				_passwordDigit3 = value;
				RaisePropertyChanged("PasswordDigit3");
			}
		}

		public String PasswordDigit4
		{
			get
			{
				return _passwordDigit4;
			}
			set
			{
				_passwordDigit4 = value;
				RaisePropertyChanged("PasswordDigit4");
			}
		}

		public string SoftwareVersionText { get; set; }

		public string IPAddressText { get; set; }

		public string ServerUriText { get; set; }

		public string KioskRegistrationKeyText { get; set; }

		public string SyncServiceRegistrationKeyText { get; set; }

		public string KioskSerialNumberText { get; set; }

		public bool? IsSoftwareVersionVisible
		{
			get
			{
				return _isSoftwareVersionVisible;
			}
			set
			{
				_isSoftwareVersionVisible = value;
				RaisePropertyChanged("IsSoftwareVersionVisible");
			}
		}

		public bool? IsIPAddressVisible
		{
			get
			{
				return _isIPAddressVisible;
			}
			set
			{
				_isIPAddressVisible = value;
				RaisePropertyChanged("IsIPAddressVisible");
			}
		}

		public bool? IsServerUriVisible
		{
			get
			{
				return _isServerUriVisible;
			}
			set
			{
				_isServerUriVisible = value;
				RaisePropertyChanged("IsServerUriVisible");
			}
		}

		public bool? IsKioskKeyVisible
		{
			get
			{
				return _isKioskKeyVisible;
			}
			set
			{
				_isKioskKeyVisible = value;
				RaisePropertyChanged("IsKioskKeyVisible");
			}
		}

		public bool? IsSyncServiceKeyVisible
		{
			get
			{
				return _isSyncServiceKeyVisible;
			}
			set
			{
				_isSyncServiceKeyVisible = value;
				RaisePropertyChanged("IsSyncServiceKeyVisible");
			}
		}

		public bool? IsKioskSerialNumberVisible
		{
			get
			{
				return _isKioskSerialNumberVisible;
			}
			set
			{
				_isKioskSerialNumberVisible = value;
				RaisePropertyChanged("IsKioskSerialNumberVisible");
			}
		}

        public bool IsDeleteEnabled
        {
            get { return _isDeleteEnabled; }
            set
            {
                _isDeleteEnabled = value;
                RaisePropertyChanged("IsDeleteEnabled");
            }
        }

        public SettingsViewModel()
		{
			InitializeControls();
		}

		public RelayCommand ExitCommand
		{
			get
			{
				return _exitCommand
					?? (_exitCommand = new RelayCommand(ExitApp));
			}
		}

		private void ExitApp()
		{
			try
			{
				Logger.Instance.WriteLog(LogType.Info, "Kiosk Exit by Admin", null, KioskRegistrationKeyText);
				ResetSystemProcess();
                Application.Current.Shutdown();
                //foreach (Process p in Process.GetProcesses().Where(p => p.ProcessName == "EMIS.PatientFlow.Kiosk"))
                //{
                //    p.Kill();
                //}
            }
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskRegistrationKeyText);
			}
		}

		private void ResetSystemProcess()
		{
			try
			{
				
				foreach (Process p in Process.GetProcesses().Where(p => p.ProcessName == "cmd"))
				{
					p.Kill();
				}
				var runningProcessByName = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "explorer");
				if (runningProcessByName == null)
				{
					Process.Start(System.IO.Path.Combine(Environment.GetEnvironmentVariable("windir"), "explorer.exe"));
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskRegistrationKeyText);
			}
		}

		public RelayCommand<object> ShutDownCommand
		{
			get
			{
				return _shutDownCommand
					?? (_shutDownCommand = new RelayCommand<object>(
										  p =>
										  {
											  Logger.Instance.WriteLog(LogType.Info, "Kiosk Shutdown by Admin", null, KioskRegistrationKeyText);
											  Process.Start("shutdown", "-s -f -t 0");
										  }));
			}
		}

		public RelayCommand<object> RestartCommand
		{
			get
			{
				return _restartCommand
					?? (_restartCommand = new RelayCommand<object>(
										  p =>
										  {
											  Logger.Instance.WriteLog(LogType.Info, "Kiosk Restart by Admin", null, KioskRegistrationKeyText);
											  Process.Start("shutdown", "-r -f -t 0");
										  }));
			}
		}

		public RelayCommand<string> NumberClickCommand
		{
			get
			{
				return _numberClickCommand
					??(_numberClickCommand = new RelayCommand<string>(
						ValidateAdminPassword));
			}
		}

		private void InitializeControls()
		{
			IsCorrectPin = true;
			IsAdminLoginVisible = true;
			IsAdminDetailsVisible = null;
            WelcomeText = "Admin login";
            EnterYourPinText = "Enter your pin to log in";
            //WelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SettingsAdminLoginWelcomeText];
			//EnterYourPinText = GlobalVariables.SelectedLanguageIdText[LanguageText.EnterYourPinText];
			
			IsSoftwareVersionVisible = true;
			IsIPAddressVisible = true;
			IsServerUriVisible = true;
			IsKioskKeyVisible = true;
			IsSyncServiceKeyVisible = true;
			IsKioskSerialNumberVisible = true;
            IsDeleteEnabled = false;

            var configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();

			SoftwareVersionText = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			if (SoftwareVersionText == null || string.IsNullOrEmpty(SoftwareVersionText))
			{
				IsSoftwareVersionVisible = null;
			}

			IPAddressText = GetIpAddress();
			if (IPAddressText == null || string.IsNullOrEmpty(IPAddressText))
			{
				IsIPAddressVisible = null;
			}

			ServerUriText = Utilities.GetAppSettingValue("ServerURI");
			if (ServerUriText == null || string.IsNullOrEmpty(ServerUriText))
			{
				IsServerUriVisible = null;
			}
			
			KioskRegistrationKeyText = Utilities.GetAppSettingValue("RegistrationKey").ToLower();
			if (KioskRegistrationKeyText == null || string.IsNullOrEmpty(KioskRegistrationKeyText))
			{
				IsKioskKeyVisible = null;
			}

			KioskSerialNumberText = GetKioskSerialNumber();
			if (KioskSerialNumberText == null || string.IsNullOrEmpty(KioskSerialNumberText))
			{
				IsKioskSerialNumberVisible = null;
			}

			SyncServiceRegistrationKeyText = GetSyncKey(configRepository);
			if (SyncServiceRegistrationKeyText == null || string.IsNullOrEmpty(SyncServiceRegistrationKeyText))
			{
				IsSyncServiceKeyVisible = null;
			}
		}

		internal void SetControlText()
		{

		}

		private void ValidateAdminPassword(string selectedPasswordDigit)
		{
            IsDeleteEnabled = true;
			if (!selectedPasswordDigit.Equals("X"))
			{
				switch (_index)
				{
					case 0:
						PasswordDigit1 = selectedPasswordDigit;
						_index++;
						break;
					case 1:
						PasswordDigit2 = selectedPasswordDigit;
						_index++;
						break;
					case 2:
						PasswordDigit3 = selectedPasswordDigit;
						_index++;
						break;
					case 3:
						PasswordDigit4 = selectedPasswordDigit;
						_index++;
						ForwardNavigation();
						break;
				}
			}
			else if (selectedPasswordDigit.Equals("X"))
			{
				switch (_index)
				{
					case 1:
						PasswordDigit1 = string.Empty;
                        IsDeleteEnabled = false;
						break;
					case 2:
						PasswordDigit2 = string.Empty;
						break;
					case 3:
						PasswordDigit3 = string.Empty;
						break;
					case 4:
						PasswordDigit4 = string.Empty;
						break;
				}

				if (_index != 0)
				{
					_index--;
				}
			}
		}

		private void ForwardNavigation()
		{
			string Password = PasswordDigit1 + PasswordDigit2 + PasswordDigit3 + PasswordDigit4;
			string password = Constants.AdminPassword;
			if (GlobalVariables.KioskSettings != null && !string.IsNullOrEmpty(GlobalVariables.KioskSettings.AdminPassword))
			{
				password = GlobalVariables.KioskSettings.AdminPassword;
			}

			if (Password == password)
			{
				IsCorrectPin = true;
                WelcomeText = "Admin area";
                IsAdminLoginVisible = null;
				IsAdminDetailsVisible = true;
			}
			else
			{
				IsCorrectPin = false;
                WelcomeText = "Incorrect pin";
            }
        }

		private string GetSyncKey(IConfigurationRepository configRepository)
		{
			try
			{
				var kioskdetails =
						configRepository.GetKioskConfiguration<KioskRegistrationGuid>(KioskConfigType.KioskDetails.ToString());
				if (kioskdetails != null && kioskdetails.SyncServiceGuid != null)
				{
					return kioskdetails.SyncServiceGuid.ToLower();
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskRegistrationKeyText);
			}

			return "";
		}

		private string GetKioskSerialNumber()
		{
			try
			{
				using (var mos = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS"))
				using (var moc = mos.Get())
				{
					foreach (var mo in moc.Cast<ManagementObject>())
						return (string)mo["SerialNumber"];
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskRegistrationKeyText);
			}

			return string.Empty;
		}

		private string GetIpAddress()
		{
			try
			{
				IPHostEntry hostByName = Dns.GetHostEntry(Dns.GetHostName());
				string ipAddress = string.Empty;
				if (hostByName.AddressList != null && hostByName.AddressList.Any(a => a.AddressFamily == AddressFamily.InterNetwork))
				{
					ipAddress = hostByName.AddressList.First(a => a.AddressFamily == AddressFamily.InterNetwork).ToString();
				}
				return ipAddress;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskRegistrationKeyText);
			}

			return string.Empty;
		}
	}
}