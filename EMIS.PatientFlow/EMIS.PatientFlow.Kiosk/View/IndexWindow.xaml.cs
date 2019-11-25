using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Controls;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.HubClients;
using EMIS.PatientFlow.Kiosk.KeyBoard;
using EMIS.PatientFlow.Kiosk.Model;
using EMIS.PatientFlow.Kiosk.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace EMIS.PatientFlow.Kiosk.View
{
	public partial class IndexWindow : Window
	{
		private readonly IConfigurationRepository _repository;

		private readonly string _registrationKey = Utilities.GetAppSettingValue("RegistrationKey");
		private readonly ManualResetEvent _stopConnection = new ManualResetEvent(false);
		private AppPages _currentPage;
		private DispatcherTimer _dateTimeTimer;
		private DispatcherTimer _timeoutTimer;
		private TimeSpan _timeoutValue;
		private DateTime _startTime;
		private int _timeOutCountdown;
		private static bool _isOutOfService;

		private List<LanguageModel> _languageList;
		private ObservableCollection<LanguageModel> _frequentlyUsedLanguageList;
		private LanguageModel _language;
		private RelayCommand<int> _selectedLanguageCommand;

		public static Window DarkWindow { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
		public void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public List<LanguageModel> LanguageList
		{
			get
			{
				return _languageList;
			}
			set
			{
				_languageList = value;
				RaisePropertyChanged("LanguageList");
			}
		}

		public ObservableCollection<LanguageModel> FrequentlyUsedLanguageList
		{
			get
			{
				return _frequentlyUsedLanguageList;
			}

			set
			{
				_frequentlyUsedLanguageList = value;
				RaisePropertyChanged("FrequentlyUsedLanguageList");
			}
		}

		public LanguageModel SelectedLanguage
		{
			get { return GlobalVariables.LanguageList.FirstOrDefault(language => language.LanguageId == GlobalVariables.SelectedLanguageId); }
			set
			{
				GlobalVariables.SelectedLanguageId = value.LanguageId;
				ChangePageLanguageText();

				RaisePropertyChanged("SelectedLanguage");
			}

		}

		private void ChangePageLanguageText()
		{
			switch (_currentPage)
			{
				case AppPages.SurveyQuestions:
					{
						var viewModel = ServiceLocator.Current.GetInstance<SurveyQuestionsViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.Organisation:
					{
						var viewModel = ServiceLocator.Current.GetInstance<SelectOrganisationViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.MultipleAppointments:
					{
						var viewModel = ServiceLocator.Current.GetInstance<MultipleAppointmentsViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.Finish:
					{
						var viewModel = ServiceLocator.Current.GetInstance<FinishViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.Settings:
					{
						var viewModel = ServiceLocator.Current.GetInstance<SettingsViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.FirstAvailableAppointment:
					{
						var viewModel = ServiceLocator.Current.GetInstance<FirstAvailableAppointmentViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.BookingTimeSelection:
					{
						var viewModel = ServiceLocator.Current.GetInstance<BookingTimeSelectionViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.ConfirmBooking:
					{
						var viewModel = ServiceLocator.Current.GetInstance<ConfirmBookingViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.FinishBooking:
					{
						var viewModel = ServiceLocator.Current.GetInstance<FinishBookingViewModel>();
						viewModel.SetControlText();
					}
					break;
				case AppPages.Surveys:
					{
						var viewModel = ServiceLocator.Current.GetInstance<SurveysChooseOptionViewModel>();
						viewModel.SetControlText();
					}
					break;

				case AppPages.FinishQuestionnaires:
					{
						var viewModel = ServiceLocator.Current.GetInstance<FinishQuestionnaireViewModel>();
						viewModel.SetControlText();
					}
					break;
				default:
					Navigate(_currentPage);
					break;
			}
		}

		public RelayCommand<int> SelectedLanguageCommand
		{
			get
			{
				return _selectedLanguageCommand
					?? (_selectedLanguageCommand = new RelayCommand<int>(
										  p =>
										  {
											  _language = new LanguageModel { LanguageId = p };
											  SelectedLanguage = _language;
											  LanguagePopup.IsOpen = false;
										  }));

			}
		}

		static IndexWindow()
		{
			DarkWindow = new Window()
			{
				Background = Brushes.Black,
				Opacity = 0.8,
				AllowsTransparency = true,
				ShowInTaskbar = false,
				WindowStyle = WindowStyle.None,
				WindowState = WindowState.Maximized,
				Topmost = false
			};
		}

		public IndexWindow()
		{
			InitializeComponent();
			_repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			LblTimeOut.Visibility = Visibility.Collapsed;
			RegisterPages();
			SetTime();
			BindData();
			CheckNetworkConnection();
			Logger.Instance.WriteLog(LogType.Info, "INFO: EMIS PatientFlow Kiosk Version : " + Assembly.GetExecutingAssembly().GetName().Version + " Started at " + DateTime.Now.ToString("dd-MMM hh:mm:ss tt"), null, _registrationKey);
		}

		private void RegisterPages()
		{
			Messenger.Default.Register<AppPages>(this, Navigate);
		}

		private void CheckValidConfigConnection(bool issuccess = true)
		{
			Task.Factory.StartNew(() =>
			{
				RunNextScheduler(issuccess ? DateTime.UtcNow.AddMinutes(1) : DateTime.UtcNow.AddSeconds(10));
			}).ContinueWith(ex =>
			{
				if (ex.Exception != null)
					Logger.Instance.WriteLog(LogType.Error, ex.Exception.Message, ex.Exception, _registrationKey);
			});

		}

		private void CheckNetworkConnection()
		{
			_stopConnection.Reset();
			ThreadPool.RegisterWaitForSingleObject(
				_stopConnection,
				CheckForNetworkError,
				null,
				4000,
				false);
		}

		private DateTime? _nextCheck;

		private void RunNextScheduler(DateTime date)
		{
			var ts = GetTimeSpan(date);

			Delay((int)ts.TotalMilliseconds).ContinueWith(x =>
			{
				SyncConnectionDetails();
				RunNextScheduler(date.AddMinutes(1));
			});
		}

		private Task Delay(int milliseconds)
		{
			var tcs = new TaskCompletionSource<object>();

			try
			{
				new System.Threading.Timer(p => tcs.SetResult(null)).Change(milliseconds, -1);
			}
			catch
			{
				return Delay(GetTimeSpan(DateTime.UtcNow).Milliseconds);
			}

			return tcs.Task;
		}

		private TimeSpan GetTimeSpan(DateTime date)
		{
			TimeSpan ts;
			try
			{
				if (date > DateTime.UtcNow)
					ts = date - DateTime.UtcNow;
				else
				{
					ts = DateTime.UtcNow.AddMinutes(1) - DateTime.UtcNow;
				}
			}
			catch
			{
				ts = DateTime.UtcNow.AddMinutes(1) - DateTime.UtcNow;
			}
			return ts;
		}

		private void SyncConnectionDetails()
		{
			try
			{
				if (!GlobalVariables.NetworkUnavailable)
				{
					var kioskRegGuid = _repository.GetKioskConfiguration<KioskRegistrationGuid>(KioskConfigType.KioskDetails.ToString());

					if (kioskRegGuid == null ||
						!String.Equals(_registrationKey, kioskRegGuid.KioskGuid, StringComparison.CurrentCultureIgnoreCase))
					{
						App.StopConnection.Reset();
						KioskHubClient.Instance.CloseHub();
						KioskHubClient.Instance.StartHub();
						KioskHubClient.Instance.SaveKioskDetails();
						Logger.Instance.WriteLog(LogType.Error, "InValid Key Restarting Kiosk Connection", null, _registrationKey);
					}
					else if (!GlobalVariables.IsUsedKey)
						KioskHubClient.Instance.StartHub();

					if (_nextCheck == null || _nextCheck < DateTime.UtcNow)
					{
						if (!GlobalVariables.IsUsedKey)
						{
							KioskHubClient.Instance.IsConnected();
						}
						_nextCheck = DateTime.UtcNow.AddMinutes(5);
						Logger.Instance.WriteLog(LogType.Info, "INFO: Updated Connection Details in Portal", null, _registrationKey);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}
		}

		private void CheckForNetworkError(object state, bool timedOut)
		{
			try
			{
				GlobalVariables.NetworkUnavailable = !CheckNetworkConnectivity();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _registrationKey);
			}
		}

		private static int _count = 0;

		public void BindData()
		{
			var isSuccess = false;
			ProgressBar.Visibility = Visibility.Visible;
			Task.Factory.StartNew(
				() =>
				{
					isSuccess = KioskHubClient.Instance.StartHub();
					if (isSuccess)
					{
						while (!GlobalVariables.ConnectionStatus && GlobalVariables.IsSyncErrorFree)
						{
							if (_count > 30)
								break;
							Thread.Sleep(1000);
							_count++;
						}
					}
				}).ContinueWith(
					t =>
					{
						App.StopConnection.Reset();

						if (isSuccess)
						{
							Dispatcher.CurrentDispatcher.BeginInvoke(new Action(TaskAfterSync));
						}
						else
						{
							Dispatcher.CurrentDispatcher.BeginInvoke(new Action(TaskOnSyncConnectionError));
						}
					},
					TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void TaskOnSyncConnectionError()
		{
			Logger.Instance.WriteLog(LogType.Info, "INFO: Connection Error in Index Window", null, Utilities.GetAppSettingValue("RegistrationKey"));
			TaskAfterConnection(false);
		}

		private void TaskAfterConnection(bool issucess = true)
		{
			CheckValidConfigConnection(issucess);
			CheckStatus();
			ProgressBar.Visibility = Visibility.Collapsed;
			SetTimerData();
		}

		public void TaskAfterSync()
		{
			try
			{
				KioskHubClient.Instance.SaveKioskDetails();
				TaskAfterConnection();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, Utilities.GetAppSettingValue("RegistrationKey"));
			}
		}

		private void Navigate(AppPages p)
		{
			ViewModelLocator.Cleanup(p);
			GlobalVariables.IsInFinishArrival = false;
			GlobalVariables.IsInFinishBooking = false;
			GlobalVariables.IsHomePage = false;
			_currentPage = p;

			LanguagePopup.IsOpen = false;
			HomeButton.Visibility = Visibility.Visible;
			BackButton.Visibility = Visibility.Visible;
			RemoveOverlay();

			switch (p)
			{
				case AppPages.OutOfService:
					StopTimeOutTimer();
					HomeButton.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new OutOfServiceUc();
					break;

				case AppPages.Organisation:
					_currentPage = AppPages.Organisation;
					LblTimeOut.Visibility = Visibility.Collapsed;
					GlobalVariables.IsHomePage = true;
					GlobalVariables.ArrivedPatientDetails = null;
					SetTimeoutTimer();
					HomeButton.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new SelectOrganisation();
					break;

				case AppPages.SelectModule:
					GlobalVariables.IsHomePage = true;
					SetTimeoutTimer();
					SetTimerData();
					ContentControl.Content = new SelectOptionUc();

					if (GlobalVariables.Organisations.Count < 2)
					{
						HomeButton.Visibility = Visibility.Collapsed;
					}
					BackButton.Visibility = Visibility.Collapsed;

					break;

				case AppPages.SelectDay:
					ContentControl.Content = new SelectDayUc();
					break;

				case AppPages.SelectYear:
					ContentControl.Content = new SelectYearUc();
					break;

				case AppPages.Finish:
					GlobalVariables.IsInFinishArrival = true;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new FinishUc();
					break;

				case AppPages.Surveys:
					ContentControl.Content = new SurveysChooseOptionUc();
					break;

				case AppPages.SurveyQuestions:
					if (GlobalVariables.KioskArrival != null && GlobalVariables.KioskArrival.ForceSurvey && !GlobalVariables.IsAnonymous)
					{
						HomeButton.Visibility = Visibility.Collapsed;
						BackButton.Visibility = Visibility.Collapsed;
					}
					ContentControl.Content = new SurveyQuestions();
					break;

				case AppPages.BookMe:
					ContentControl.Content = new DoctorSelectionUc();
					break;

				case AppPages.ConfirmBooking:
					GlobalVariables.IsInFinishBooking = true;
					ContentControl.Content = new ConfirmBooking();
					break;

				case AppPages.FinishBooking:
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new FinishBookingUc();
					break;

				case AppPages.BookingTimeSelection:
					ContentControl.Content = new BookingTimeSelectionUc();
					break;

				case AppPages.FinishQuestionnaires:
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new FinishQuestionnairesUc();
					break;

				case AppPages.SelectGender:
					ContentControl.Content = new SelectGenderUc();
					break;

				case AppPages.SelectSurname:
					ContentControl.Content = new SelectSurnameUc();
					break;

				case AppPages.SelectMonth:
					ContentControl.Content = new SelectMonthUc();
					break;

				case AppPages.SelectPostCode:
					ContentControl.Content = new SelectPostCodeUC();
					break;

				case AppPages.ExceptionDivert:
					ProgressBar.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new ExceptionDivert();
					break;

				case AppPages.SlotType:
					ContentControl.Content = new SelectSlotTypeUc();
					break;

				case AppPages.Settings:
					GlobalVariables.IsHomePage = false;
					HomeButton.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Visible;
					ContentControl.Content = new SettingsUc();
					break;

				case AppPages.DemographicMessages:
					ContentControl.Content = new DemographicDetailsUc();
					break;

				case AppPages.ShowGpDemographicMessages:
					ContentControl.Content = new DemographicGotGPMessageUc();
					break;

				case AppPages.ArrivalByBarcode:
					ContentControl.Content = new ArriveByBarcodeUC();
					break;

				case AppPages.SiteMap:
					ContentControl.Content = new SiteMapUC();
					break;

				case AppPages.FirstAvailableAppointment:
					ContentControl.Content = new FirstAvailableAppointmentUC();
					break;

				case AppPages.MultiplePatientsExceptionPage:
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new MultiplePatientsExceptionPage();
					break;

				case AppPages.HomePage:
					SetComboboxLanguages();
					HomeButton.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new HomePageUC();
					break;

				case AppPages.SelectDayMonthYear:
					ContentControl.Content = new SelectionDayMonthYearUc();
					break;

				case AppPages.ArrivalRouting:
					HomeButton.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new ArrivalRouting();
					break;

				case AppPages.ArrivalConfirmationAndRouting:
					HomeButton.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new ArrivalConfirmationAndRouting();
					break;

				case AppPages.SingleAppointment:
					ContentControl.Content = new SingleAppointmentUC();
					break;

				case AppPages.MultipleAppointments:
					ContentControl.Content = new MultipleAppointmentsUC();
					break;

				case AppPages.FinishRouting:
					HomeButton.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new FinishRoutingModelUc();
					break;

				case AppPages.ArrivedAppointmentError:
					ContentControl.Content = new ArrivedAppointmentErrorUC();
					break;

				default:
					HomeButton.Visibility = Visibility.Collapsed;
					BackButton.Visibility = Visibility.Collapsed;
					ContentControl.Content = new SelectOrganisation();
					break;
			}
		}

		private void SetComboboxLanguages()
		{
			var languageRepository = DiResolver.CurrentInstance.Reslove<ILanguageRepository>();
			LanguageList = languageRepository.GetLanguageList();
			GlobalVariables.LanguageList = LanguageList;
			if (LanguageList != null && LanguageList.Count > 0)
				GlobalVariables.SelectedLanguageId = LanguageList[0].LanguageId;

			if (GlobalVariables.LanguageList != null && GlobalVariables.LanguageList.Count > 1)
			{
				LanguageButton.Visibility = Visibility.Visible;
				GlobalVariables.LanguageList.ToList().ForEach(
				language => language.LanguageName = language.LanguageName.Substring(0, 3).ToUpper());
				LanguageList = GlobalVariables.LanguageList;
				GetFrequentlyUsedLanguageList();
				LanguageListView.ItemsSource = LanguageList;
			}
			else
			{
				LanguageButton.Visibility = Visibility.Hidden;
			}
		}

		private void CheckStatus()
		{
			string kioskId = Utilities.GetAppSettingValue("RegistrationKey");

			var kioskRegGuid = _repository.GetKioskConfiguration<KioskRegistrationGuid>(KioskConfigType.KioskDetails.ToString());

			if (kioskRegGuid != null && !String.Equals(kioskId, kioskRegGuid.KioskGuid, StringComparison.CurrentCultureIgnoreCase))
			{
				NavigateTo(AppPages.ExceptionDivert);
				GlobalVariables.IsKioskDataError = true;
			}
			else
			{
				try
				{
					var Kioskstatus = _repository.GetKioskConfiguration<KioskStatus>(KioskConfigType.Status.ToString());
					if (Kioskstatus == null)
					{
						GlobalVariables.IsHomePage = true;
						Logger.Instance.WriteLog(LogType.Info, "INFO: Status not yet updated", null, kioskId);
					}
					else
					{
						int status = Kioskstatus.Status;
						GlobalVariables.IsDataAvailable = true;
						GlobalVariables.IsKioskDataError = false;
						if (GlobalVariables.IsUsedKey)
						{
							NavigateTo(AppPages.OutOfService);
						}
						else
						{
							switch (status)
							{
								case Constants.KioskOnline:
									GlobalVariables.KioskStatus = Constants.StatusOnline;
									break;
								case Constants.KioskOffline:
									GlobalVariables.KioskStatus = Constants.StatusOutOfService;
									break;
							}
							StopTimeOutTimer();
							switch (GlobalVariables.KioskStatus)
							{
								case Constants.StatusOnline:
									NavigateTo(AppPages.HomePage);
									break;
								case Constants.StatusClosed:
									NavigateTo(AppPages.OutOfService);
									break;
								case Constants.StatusOutOfService:
									NavigateTo(AppPages.OutOfService);
									break;
								default:
									NavigateTo(AppPages.HomePage);
									break;
							}
						}
					}
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, kioskId);
					NavigateTo(AppPages.ExceptionDivert);
				}
			}
		}

		private void NavigateTo(AppPages page)
		{
			LanguagePopup.IsOpen = false;
			_currentPage = page;
			ViewModelLocator.Cleanup(page);

			BackButton.Visibility = Visibility.Collapsed;
			HomeButton.Visibility = Visibility.Collapsed;
			RemoveOverlay();

			SetComboboxLanguages();

			switch (page)
			{
				case AppPages.OutOfService:
					StopTimeOutTimer();
					ProgressBar.Visibility = Visibility.Collapsed;
					ContentControl.Content = new OutOfServiceUc();
					break;

				case AppPages.HomePage:
					GlobalVariables.IsKioskDataError = false;
					ContentControl.Content = new HomePageUC();
					break;

				case AppPages.ExceptionDivert:
					HomeButton.Visibility = Visibility.Visible;
					ProgressBar.Visibility = Visibility.Collapsed;
					ContentControl.Content = new ExceptionDivert();
					break;

				case AppPages.Settings:
					GlobalVariables.IsHomePage = false;
					BackButton.Visibility = Visibility.Visible;
					ProgressBar.Visibility = Visibility.Collapsed;
					SetTimerData();
					ContentControl.Content = new SettingsUc();
					break;
			}
		}

		private void SetTime()
		{
			_dateTimeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
			_dateTimeTimer.Tick -= DateTimeTimer_Tick;
			_dateTimeTimer.Tick += DateTimeTimer_Tick;
			_dateTimeTimer.Start();
		}

		private void DateTimeTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				TimeSpan timeOfDay = DateTime.Now.TimeOfDay;
				if (timeOfDay.Hours == 00 && timeOfDay.Minutes == 00 && timeOfDay.Seconds == 02)
				{
					DateTime date = DateTime.Today;
					TbkDate.Text = string.Format("{0:ddd dd MMMM yyyy}", date);
				}

				DateTime dateTime = DateTime.Now;
				TbkTime.Text = string.Format("{0:HH':'mm}", dateTime);

				if (GlobalVariables.IsDbConnected)
				{
					if (GlobalVariables.WasDbConnectionError && _currentPage == AppPages.ExceptionDivert)
					{
						GlobalVariables.WasDbConnectionError = false;
						CheckStatus();
					}
					if (GlobalVariables.NetworkUnavailable && _currentPage != AppPages.OutOfService && _currentPage != AppPages.Settings)
					{
						_isOutOfService = true;
						NavigateTo(AppPages.OutOfService);
					}
					if (GlobalVariables.KioskStatus.Equals(Constants.StatusOutOfService) && _currentPage != AppPages.OutOfService && _currentPage != AppPages.Settings)
					{
						_isOutOfService = false;
						NavigateTo(AppPages.OutOfService);
					}
					if (GlobalVariables.NetworkUnavailable && !_isOutOfService)
					{
						_isOutOfService = true;
						NavigateTo(AppPages.OutOfService);
					}
					else if (!GlobalVariables.NetworkUnavailable && _isOutOfService && (_timeoutTimer == null || !_timeoutTimer.IsEnabled) && GlobalVariables.KioskStatus.Equals(Constants.StatusOutOfService))
					{
						_isOutOfService = false;
						NavigateTo(AppPages.OutOfService);
					}
					else if (!GlobalVariables.NetworkUnavailable && _isOutOfService && (_timeoutTimer == null || !_timeoutTimer.IsEnabled))
					{
						_isOutOfService = false;
						NavigateTo(AppPages.HomePage);
					}
				}

				else
				{
					GlobalVariables.WasDbConnectionError = true;
					if (_currentPage != AppPages.Settings)
					{
						NavigateTo(AppPages.ExceptionDivert);
					}
				}
			}
			catch (Exception)
			{
				NavigateTo(AppPages.ExceptionDivert);
			}
		}

		[DllImport("wininet.dll")]
		private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

		private bool CheckNetworkConnectivity()
		{
			try
			{
				int desc;
				return InternetGetConnectedState(out desc, 0);
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void SetTimerData()
		{
			DateTime date = DateTime.Today;
			TbkDate.Text = string.Format("{0:ddd dd MMMM yyyy}", date);

			GlobalVariables.KioskSettings = _repository.GetKioskConfiguration<KioskSettings>(KioskConfigType.KioskSettings.ToString());
		}

		private void StopTimeOutTimer()
		{
			if (_timeoutTimer != null)
				_timeoutTimer.Stop();
		}

		private void BtnAdminLogin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (_currentPage != AppPages.Settings)
			{
				LanguagePopup.IsOpen = false;
				NavigateTo(AppPages.Settings);
			}
		}

		public void SetTimeoutTimer()
		{
			_timeoutValue = TimeSpan.FromSeconds(GlobalVariables.TimeOutValue);
			if (_timeoutTimer != null)
				_timeoutTimer.Stop();
			_timeoutTimer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher.CurrentDispatcher);
			_timeoutTimer.Interval = TimeSpan.FromMilliseconds(1000);
			_timeoutTimer.Tick += TimeoutTimer_Tick;
			Start();
		}

		public void Start()
		{
			_startTime = new DateTime();
			_timeoutTimer.Start();
		}

		private void TimeoutTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				if (LblTimeOut.Visibility != Visibility.Visible)
					_timeOutCountdown = Convert.ToInt32(GlobalVariables.TimeOutValue) - 6;

				LblTimeOut.Visibility = Visibility.Collapsed;
				LASTINPUTINFO lii = new LASTINPUTINFO { cbSize = Marshal.SizeOf(typeof(LASTINPUTINFO)) };

				if (UnsafeNativeMethods.GetLastInputInfo(out lii))
				{
					TimeSpan idleFor = TimeSpan.FromMilliseconds(unchecked((uint)Environment.TickCount - lii.dwTime));

					TimeSpan aliveFor = TimeSpan.FromMilliseconds(unchecked((uint)Environment.TickCount - _startTime.Millisecond));

					if (idleFor.Seconds >= Convert.ToInt32(GlobalVariables.TimeOutValue) - 3)
					{
						LblTimeOut.Content = GlobalVariables.TimeOutMessage;
						LblTimeOut.Visibility = GetTimeOutVisibility();
						LblTimeOut.Content = (LblTimeOut.Content != null ? LblTimeOut.Content.ToString() : string.Empty) + (idleFor.Seconds - _timeOutCountdown);
						_timeOutCountdown += 2;
					}

					if (aliveFor >= idleFor && idleFor >= _timeoutValue)
					{
						LblTimeOut.Visibility = Visibility.Collapsed;
						_timeoutTimer.Stop();
						if (TimeoutEvent != null)
							TimeoutEvent.Invoke(this, EventArgs.Empty);

						if (NumericKeyboard.InstanceObject != null)
						{
							NumericKeyboard.InstanceObject.Close();
						}

						if (TouchScreenKeyboard.InstanceObject != null)
						{
							TouchScreenKeyboard.InstanceObject.Close();
						}

						try
						{
							var viewModel = ServiceLocator.Current.GetInstance<FinishViewModel>();
							if (viewModel != null)
							{
								if (viewModel.IsAllAlertsPopUpVisible)
								{
									viewModel.IsAllAlertsPopUpVisible = false;
									DarkWindow.Hide();
								}

								if (viewModel.IsAllAppointmentsPopUpVisible)
								{
									viewModel.IsAllAppointmentsPopUpVisible = false;
									DarkWindow.Hide();
								}
							}
						}
						catch (Exception ex)
						{
							Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, _registrationKey);
							NavigateTo(AppPages.ExceptionDivert);
						}

						if (DarkWindow != null)
						{
							DarkWindow.Hide();
						}

						if (_currentPage == AppPages.SurveyQuestions && !GlobalVariables.IsAnonymous && GlobalVariables.KioskArrival.ForceSurvey)
							Messenger.Default.Send(AppPages.Finish);
						else
							Messenger.Default.Send(AppPages.HomePage);

						GlobalVariables.ViewArrivalInfo = false;
					}
				}
			}

			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, _registrationKey);
				NavigateTo(AppPages.ExceptionDivert);
			}
		}


		private Visibility GetTimeOutVisibility()
		{
			return ((_currentPage == AppPages.Organisation) || (GlobalVariables.Organisations.Count == 1 && _currentPage == AppPages.SelectModule))
				? Visibility.Collapsed
				: Visibility.Visible;
		}

		public event EventHandler TimeoutEvent;

		private void BtnLanguage_Click(object sender, RoutedEventArgs e)
		{
			LanguagePopup.IsOpen = true;
			DarkWindow.Show();
		}

		private void LanguageinLanguageListClick(object sender, RoutedEventArgs e)
		{
			var selectedLanguageId = ((System.Windows.Controls.Button)sender).Tag;

			_language = new LanguageModel();
			_language.LanguageId = Convert.ToInt32(selectedLanguageId);
			SelectedLanguage = _language;
			GetFrequentlyUsedLanguageList();
			LanguagePopup.IsOpen = false;
			RemoveOverlay();
		}

		private void RemoveOverlay()
		{
			DarkWindow.Hide();
		}

		private void GetFrequentlyUsedLanguageList()
		{
			GlobalVariables.FrequentlyUsedLanguageList.Insert(0, SelectedLanguage);
			FrequentlyUsedLanguageList = new ObservableCollection<LanguageModel>(GlobalVariables.FrequentlyUsedLanguageList.Distinct().Take(4));
			LanguageButton.DataContext = SelectedLanguage;
		}

		private void OnBackbuttonClick(object sender, RoutedEventArgs e)
		{
			switch (_currentPage)
			{
				case AppPages.SelectDay:
				case AppPages.SelectMonth:
				case AppPages.SelectYear:
				case AppPages.SelectDayMonthYear:
				case AppPages.SelectSurname:
				case AppPages.SelectGender:
				case AppPages.SelectPostCode:

					Utilities.PatientMatchBackwardNavigation();
					break;

				case AppPages.DemographicMessages:
				case AppPages.ShowGpDemographicMessages:
				case AppPages.Settings:
					Navigate(AppPages.HomePage);
					break;

				case AppPages.Surveys:
				case AppPages.SiteMap:
					Navigate(AppPages.SelectModule);
					break;

				case AppPages.SurveyQuestions:
					Navigate(GlobalVariables.IsAnonymous ? AppPages.Surveys : AppPages.SelectModule);
					break;

				case AppPages.SingleAppointment:
				case AppPages.MultipleAppointments:
				case AppPages.SlotType:
					GlobalVariables.IsMuliplePatientCheckDone = false;
					GlobalVariables.PatientMatchPinCode = string.Empty;
					Utilities.PatientMatchBackwardNavigation();
					break;

				case AppPages.BookMe:
					if (GlobalVariables.SlotTypeList == null || GlobalVariables.SlotTypeList.Count <= 1)
					{
						Navigate(AppPages.BookingTimeSelection);
					}
					else
					{
						Navigate(AppPages.SlotType);
					}
					break;

				case AppPages.FirstAvailableAppointment:
					if (GlobalVariables.SlotTypeList.Count <= 1 || GlobalVariables.SlotTypeList == null)
					{
						GlobalVariables.IsMuliplePatientCheckDone = false;
						GlobalVariables.PatientMatchPinCode = string.Empty;
						Utilities.PatientMatchBackwardNavigation();
					}
					else
					{
						Navigate(AppPages.SlotType);
					}
					break;

				case AppPages.BookingTimeSelection:
				case AppPages.ConfirmBooking:
					Navigate(AppPages.FirstAvailableAppointment);
					break;

				case AppPages.ArrivalByBarcode:
					UnRegisterBarcodeMessage();
					Navigate(AppPages.SelectModule);
					break;

				default:
					Utilities.PatientMatchBackwardNavigation();
					break;
			}
		}

		private void UnRegisterBarcodeMessage()
		{
			BarcodeArrivalViewModel barcodeArrivalViewModel = ServiceLocator.Current.GetInstance<BarcodeArrivalViewModel>();
			Messenger.Default.Unregister<BarCodeMessage>(barcodeArrivalViewModel, BarcodeDetected);
		}

		private void BarcodeDetected(BarCodeMessage obj)
		{
			int slotid;

			if (obj.Status == 1 && int.TryParse(obj.BarCode, out slotid))
			{
				GlobalVariables.IsBarCodeArrivalDone = true;
				GlobalVariables.SelectedSlotId = slotid;
				UnRegisterBarcodeMessage();
				Messenger.Default.Send(AppPages.ArrivalRouting);
			}
			else
			{
				UnRegisterBarcodeMessage();
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		private void OnHomebuttonClick(object sender, RoutedEventArgs e)
		{
			if (_currentPage == AppPages.ArrivalByBarcode)
			{
				UnRegisterBarcodeMessage();
			}
			Navigate(AppPages.HomePage);
		}
	}
}
