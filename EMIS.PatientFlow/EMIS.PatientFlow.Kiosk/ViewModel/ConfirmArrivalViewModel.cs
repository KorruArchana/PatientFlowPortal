using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class ConfirmArrivalViewModel : ViewModelBase
	{
		private IConfigurationRepository _configRepository;
		private IAppointmentRepository _appointmentRepository;
		private List<Appointment> _appointments;
		private int _distinctPatientCount;
		private bool _isSynched;
		private bool _exceptionFlag;
		private string _patientNameText;
		private string _thanksText;
		private string _cancelText;
		private string _noDetailsText;
		private string _unableToArriveText;
		private string _unableToArriveDetailsText;
		private string _goToReceptionText;
		private string _clearScreenContent;
		private string _errorCodeText;
		private string _errorCode;
		private bool _enableScreenTap;
		private bool? _isGridNoContentVisible;
		private bool? _isGridNoDetailsVisible;
		private bool? _isProgressBarVisible;
		private bool? _isUnableToArriveDetailsVisible;
		private bool? _isUnableToArriveVisible;
		private bool? _isArrivalButtonVisible;
		private bool? _isClearScreenVisible;
		private AppointmentCollection _appointmentCollection;
		private RelayCommand<string> _clearScreenCommand;
		private RelayCommand<int> _confirmCommand;
		private RelayCommand<string> _loadedCommand;
		private RelayCommand<long> _selectedAppointmentCommand;
		private RelayCommand<long> _selectedAppointmentByCheckBoxCommand;

		private AppointmentDetail _appointmentDetails = new AppointmentDetail();

		private string _appointmentListText;
		private string _arrivalCheckboxText;
		private string _arriveButtonText;
		private int? _arriveSelectionCount;
		private String _arriveAppointment;
		private AppointmentArrivalStatus _arrivalstatus;
		private List<Divert> _lstDiverts;
		private KioskArrival _arrival;
		private int _orgDbId;

		public ConfirmArrivalViewModel()
		{
			InitializeControls();
			GlobalVariables.ArrivedPatientDetails = null;
			GlobalVariables.ListOfArrivedAppointmentDetails = new List<Appointment>();
			FillAppointmentList();
			SetControlText();
		}

		public string ArriveButtonText
		{
			get
			{
				return _arriveButtonText;
			}
			set
			{
				_arriveButtonText = value;
				RaisePropertyChanged("ArriveButtonText");
			}
		}

		public string ArriveAppointment
		{
			get
			{
				return _arriveAppointment;
			}
			set
			{
				_arriveAppointment = value;
				RaisePropertyChanged("ArriveAppointment");
			}
		}

		public int? ArriveSelectionCount
		{
			get
			{
				return _arriveSelectionCount;
			}
			set
			{
				_arriveSelectionCount = value;
				RaisePropertyChanged("ArriveSelectionCount");
			}
		}

		public string PatientNameText
		{
			get
			{
				return _patientNameText;
			}

			set
			{
				_patientNameText = value;
				RaisePropertyChanged("PatientNameText");
			}
		}

		public string ThanksText
		{
			get
			{
				return _thanksText;
			}

			set
			{
				_thanksText = value;
				RaisePropertyChanged("ThanksText");
			}
		}

		public string CancelText
		{
			get
			{
				return _cancelText;
			}

			set
			{
				_cancelText = value;
				RaisePropertyChanged("CancelText");
			}
		}

		public string NoDetailsText
		{
			get
			{
				return _noDetailsText;
			}

			set
			{
				_noDetailsText = value;
				RaisePropertyChanged("NoDetailsText");
			}
		}

		public string UnableToArriveText
		{
			get
			{
				return _unableToArriveText;
			}

			set
			{
				_unableToArriveText = value;
				RaisePropertyChanged("UnableToArriveText");
			}
		}

		public string UnableToArriveDetailsText
		{
			get
			{
				return _unableToArriveDetailsText;
			}

			set
			{
				_unableToArriveDetailsText = value;
				RaisePropertyChanged("UnableToArriveDetailsText");
			}
		}

		public string GoToReceptionText
		{
			get
			{
				return _goToReceptionText;
			}

			set
			{
				_goToReceptionText = value;
				RaisePropertyChanged("GoToReceptionText");
			}
		}

		public string ClearScreenContent
		{
			get
			{
				return _clearScreenContent;
			}

			set
			{
				_clearScreenContent = value;
				RaisePropertyChanged("ClearScreenContent");
			}
		}

		public string ErrorCodeText
		{
			get
			{
				return _errorCodeText;
			}

			set
			{
				_errorCodeText = value;
				RaisePropertyChanged("ErrorCodeText");
			}
		}

		public string ErrorCode
		{
			get
			{
				return _errorCode;
			}

			set
			{
				_errorCode = value;
				RaisePropertyChanged("ErrorCode");
			}
		}

		public bool EnableScreenTap
		{
			get
			{
				return _enableScreenTap;
			}

			set
			{
				_enableScreenTap = value;
				RaisePropertyChanged("EnableScreenTap");
			}
		}

		public bool? IsGridNoContentVisible
		{
			get
			{
				return _isGridNoContentVisible;
			}

			set
			{
				_isGridNoContentVisible = value;
				RaisePropertyChanged("IsGridNoContentVisible");
			}
		}

		public bool? IsGridNoDetailsVisible
		{
			get
			{
				return _isGridNoDetailsVisible;
			}

			set
			{
				_isGridNoDetailsVisible = value;
				RaisePropertyChanged("IsGridNoDetailsVisible");
			}
		}

		public bool? IsProgressBarVisible
		{
			get
			{
				return _isProgressBarVisible;
			}

			set
			{
				_isProgressBarVisible = value;
				RaisePropertyChanged("IsProgressBarVisible");
			}
		}

		public bool? IsUnableToArriveDetailsVisible
		{
			get
			{
				return _isUnableToArriveDetailsVisible;
			}
			set
			{
				_isUnableToArriveDetailsVisible = value;
				RaisePropertyChanged("IsUnableToArriveDetailsVisible");
			}
		}

		public bool? IsUnableToArriveVisible
		{
			get
			{
				return _isUnableToArriveVisible;
			}
			set
			{
				_isUnableToArriveVisible = value;
				RaisePropertyChanged("IsUnableToArriveVisible");
			}
		}

		public bool? IsArrivalButtonVisible
		{
			get
			{
				return _isArrivalButtonVisible;
			}
			set
			{
				_isArrivalButtonVisible = value;
				RaisePropertyChanged("IsArrivalButtonVisible");
			}
		}

		public bool? IsClearScreenVisible
		{
			get
			{
				return _isClearScreenVisible;
			}
			set
			{
				_isClearScreenVisible = value;
				RaisePropertyChanged("IsClearScreenVisible");
			}
		}

		public AppointmentCollection AppointmentCollection
		{
			get
			{
				return _appointmentCollection;
			}

			set
			{
				_appointmentCollection = value;
				RaisePropertyChanged("AppointmentCollection");
			}
		}

		private static bool IsTopas
		{
			get
			{
				return GlobalVariables.SelectedOrganisation.SystemType.Equals("TOPAS") ||
					   GlobalVariables.SelectedOrganisation.SystemType.Equals("None");
			}
		}
		public RelayCommand<string> ClearScreenCommand
		{
			get
			{
				return _clearScreenCommand
					?? (_clearScreenCommand = new RelayCommand<string>(
						p => Messenger.Default.Send(AppPages.HomePage)));
			}
		}

		public RelayCommand<int> ConfirmCommand
		{
			get
			{
				return _confirmCommand
					?? (_confirmCommand = new RelayCommand<int>(
										  ConfirmArrival));
			}
		}

		public RelayCommand<string> LoadedCommand
		{
			get
			{
				return _loadedCommand
					?? (_loadedCommand = new RelayCommand<string>(
										  p =>
										  {
											  if (_exceptionFlag || !GlobalVariables.IsDbConnected)
											  {
												  Messenger.Default.Send(AppPages.ExceptionDivert);
											  }
											  else if (_distinctPatientCount > 1)
											  {
												  Messenger.Default.Send(GlobalVariables.IsMuliplePatientCheckDone
													  ? ExceptionDivert()
													  : AppPages.SelectPostCode);
											  }
											  else if (GlobalVariables.KioskArrival != null && AppointmentCollection.Count > 1 && GlobalVariables.KioskArrival.AutoConfirmMultipleArrival)
											  {
												  AppointmentCollection.OrderByDescending(x => x.AppointmentDate);
												  int arrivableAppointmentsCount = AppointmentCollection.Count(c => c.IsEnabled);

												  if (arrivableAppointmentsCount == AppointmentCollection.Count)
												  {
													  IsGridNoContentVisible = false;
													  ConfirmArrival(2);
												  }
												  else if (arrivableAppointmentsCount == 0)
												  {
													  IsArrivalButtonVisible = null;
													  IsClearScreenVisible = true;
												  }
											  }
											  else if (GlobalVariables.KioskArrival != null && AppointmentCollection.Count == 1 && GlobalVariables.KioskArrival.AutoConfirmArrival)
											  {
												  IsGridNoContentVisible = false;
												  ConfirmArrival(3);
											  }
											  else
											  {
												  int arrivableAppointmentsCount = AppointmentCollection.Count(c => c.IsEnabled);

												  if (arrivableAppointmentsCount == 0)
												  {
													  IsArrivalButtonVisible = null;
													  IsClearScreenVisible = true;
												  }
											  }
										  }));
			}
		}

		private AppPages ExceptionDivert()
		{
			Logger.Instance.WriteLog(LogType.Info, ActionType.MultipleMatchesInValid.GetDisplayName(), null, KioskId);
			GlobalVariables.ErrorCode = ErrorCodes.MultiplePatientsFound;
			return AppPages.MultiplePatientsExceptionPage;
		}

		public RelayCommand<long> SelectedAppointmentCommand
		{
			get
			{
				return _selectedAppointmentCommand
					?? (_selectedAppointmentCommand = new RelayCommand<long>(
										  SelectedAppointment));
			}
		}

		public RelayCommand<long> SelectedAppointmentByCheckBoxCommand
		{
			get
			{
				return _selectedAppointmentByCheckBoxCommand
					?? (_selectedAppointmentByCheckBoxCommand = new RelayCommand<long>(
										  SelectedAppointmentByCheckBox));
			}
		}

		private void SelectedAppointment(long appointmentId)
		{
			if (AppointmentCollection != null && AppointmentCollection.Count > 1)
			{
				_appointmentDetails = AppointmentCollection.FirstOrDefault(m => m.AppointmentId == appointmentId);
				if (_appointmentDetails != null)
					_appointmentDetails.IsChecked = !_appointmentDetails.IsChecked;

				ArriveSelectionCount = AppointmentCollection.Count(c => c.IsChecked);
				SetArriveButtonText();
			}

		}

		private void SelectedAppointmentByCheckBox(long appointmentId)
		{
			ArriveSelectionCount = AppointmentCollection.Count(c => c.IsChecked);
			SetArriveButtonText();
		}

		public string AppointmentListText
		{
			get
			{
				return _appointmentListText;
			}
			set
			{
				_appointmentListText = value;
				RaisePropertyChanged("AppointmentListText");
			}
		}

		public string ArrivalCheckboxText
		{
			get
			{
				return _arrivalCheckboxText;
			}
			set
			{
				_arrivalCheckboxText = value;
				RaisePropertyChanged("ArrivalCheckboxText");
			}
		}

		private void InitializeControls()
		{
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			_appointmentRepository = DiResolver.CurrentInstance.Reslove<IAppointmentRepository>();
			_lstDiverts = _configRepository.GetKioskConfiguration<List<Divert>>(KioskConfigType.Divert.ToString());
			_arrival = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());

			IsProgressBarVisible = null;
			IsUnableToArriveVisible = true;
			IsUnableToArriveDetailsVisible = true;
			IsArrivalButtonVisible = true;
			IsClearScreenVisible = null;
			EnableScreenTap = true;
		}

		public void SetControlText()
		{
			ThanksText = GlobalVariables.SelectedLanguageIdText[LanguageText.ThanksText];
			CancelText = GlobalVariables.SelectedLanguageIdText[LanguageText.Cancel];

			if (AppointmentCollection.Count > 0)
			{
				AppointmentListText = AppointmentCollection.Count == 1 ? GlobalVariables.SelectedLanguageIdText[LanguageText.SingleAppointmentListText] :
					GlobalVariables.SelectedLanguageIdText[LanguageText.MultipleAppointmentsListText];
				ArrivalCheckboxText = AppointmentCollection.Count == 1 ? GlobalVariables.SelectedLanguageIdText[LanguageText.ArrivalCheckboxTextForSingleArrival] :
					GlobalVariables.SelectedLanguageIdText[LanguageText.ArrivalCheckboxTextForMultipleArrival];

				if (AppointmentCollection.Count == 1)
				{
					ArriveButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.SingleArrivalButtonText];
				}
				else
				{
					SetArriveButtonText();
				}
			}

			SetErrorControlText();
		}

		private void SetErrorControlText()
		{

			switch (_arrivalstatus)
			{
				case AppointmentArrivalStatus.EarlyArrival:
					IntializeErrorControls(AppointmentArrivalStatus.EarlyArrival);
					break;

				case AppointmentArrivalStatus.LateArrival:
					IntializeErrorControls(AppointmentArrivalStatus.LateArrival);
					break;

				case AppointmentArrivalStatus.DoctorDivert:
					IntializeErrorControls(AppointmentArrivalStatus.DoctorDivert);
					break;

				case AppointmentArrivalStatus.WrongLocation:
					IntializeErrorControls(AppointmentArrivalStatus.WrongLocation);
					break;

				case AppointmentArrivalStatus.MultiplePatientsFound:
					IsUnableToArriveDetailsVisible = null;
					IntializeErrorControls(AppointmentArrivalStatus.MultiplePatientsFound);
					break;

				default:
					IsUnableToArriveDetailsVisible = null;
					IntializeErrorControls(AppointmentArrivalStatus.PatientNotFound);
					break;
			}
		}

		private void ConfirmArrival(int commandParameter)
		{
			try
			{
				if (commandParameter == 1 || commandParameter == 2 || commandParameter == 3)
				{
					if (commandParameter == 2)
					{
						foreach (AppointmentDetail app in AppointmentCollection.Where(app => app.IsEnabled))
						{
							app.IsChecked = true;
						}
					}
					if (commandParameter == 3)
					{
						AppointmentCollection.Where(x => x.IsEnabled).FirstOrDefault(x => x.IsChecked = true);
					}

					IsProgressBarVisible = true;
					EnableScreenTap = false;

					ConfirmArrival();
				}
				else
				{
					Messenger.Default.Send(AppPages.HomePage);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void ConfirmArrival()
		{
			try
			{
				if (AppointmentCollection.Count(appointmentdetail => appointmentdetail.IsChecked) > 0)
				{

					var appdetailList = AppointmentCollection.Where(a => a.IsChecked).ToList();
					foreach (AppointmentDetail appointment in appdetailList)
					{
						long appid = appointment.AppointmentId;
						GlobalVariables.ListOfArrivedAppointmentDetails.Add(_appointments.First(a => a.Id == appid));
					}
				}
				if (AppointmentCollection.Count == 1 && CheckForWrongLocation(AppointmentCollection.First()))
				{
					IsProgressBarVisible = null;
					IsUnableToArriveDetailsVisible = null;

					IntializeErrorControls(AppointmentArrivalStatus.WrongLocation);
					_arrivalstatus = AppointmentArrivalStatus.WrongLocation;
				}
				else if (AppointmentCollection.Count == 1 && CheckForDivert(AppointmentCollection.First()))
				{
					IsProgressBarVisible = null;
					IsUnableToArriveDetailsVisible = null;

					IntializeErrorControls(AppointmentArrivalStatus.DoctorDivert);
					_arrivalstatus = AppointmentArrivalStatus.DoctorDivert;
				}
				else
				{
					ExecuteTask();
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(
					LogType.Error,
					ex.Message,
					ex,
					KioskId);
			}
		}

		private bool CheckForDivert(AppointmentDetail appointment)
		{
			try
			{
				var isDivertPresent = false;
				var doctorId = appointment.DoctorId;

				if (appointment.ArrivalType == AppointmentArrivalStatus.WrongLocation)
					return true;

				if (_lstDiverts != null)
				{
					List<int> sessionHolderIdList = _lstDiverts.Where(x => x.OrganisationId.ToString(CultureInfo.InvariantCulture) == GlobalVariables.SelectedOrganisation.OrganisationId).Select(x => x.SessionHolderId).ToList();
					isDivertPresent = sessionHolderIdList.Contains(doctorId);
				}

				if (isDivertPresent)
				{
					appointment.ArrivalType = AppointmentArrivalStatus.DoctorDivert;
					string message = ActionType.DoctorDivert.GetDisplayName();
					message = message + " - " + appointment.Name + " - " + appointment.Time;
					Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
				}

				return isDivertPresent;
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return true;
			}

		}

		private bool CheckForWrongLocation(AppointmentDetail appointment)
		{
			try
			{
				if (appointment.ArrivalType == AppointmentArrivalStatus.WrongLocation)
					return true;

				bool isInWrongLocation = GlobalVariables.SelectedOrganisation.SiteId != null && appointment.SiteId != GlobalVariables.SelectedOrganisation.SiteId && appointment.SiteId != _orgDbId;

				if (isInWrongLocation)
				{
					appointment.ArrivalType = AppointmentArrivalStatus.WrongLocation;
					string message = ActionType.WrongLocation.GetDisplayName();
					message = message + " - " + appointment.Name + " - " + appointment.Time + " - " + appointment.SiteId;
					Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
				}

				return isInWrongLocation;
			}

			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return true;
			}
		}

		private void ExecuteTask()
		{
			bool isSuccess = false;
			GlobalVariables.Day = "0";
			GlobalVariables.PatientMatchSelectedMonth = "0";
			GlobalVariables.IfForcedSurveyDone = false;
			IsUnableToArriveVisible = true;
			IsUnableToArriveDetailsVisible = true;

			Task.Factory.StartNew(() =>
			{
				if (AppointmentCollection != null)
				{
					AppointmentCollection.ToList().ForEach(
						appointmentDetail => appointmentDetail.ArrivalType = CompareArrivalTime(
							appointmentDetail.AppointmentDate).Last().Value);

					int selectedAppointmentsCount = AppointmentCollection.Count(appointmentDetail => appointmentDetail.IsChecked);
					//GlobalVariables.AppointmentsConfirmed = selectedAppointmentsCount == 1;

					if (selectedAppointmentsCount == GetAppointmentsCountByArrivalType(AppointmentArrivalStatus.OnTime))
						isSuccess = ConfirmAppointment();
					else if (selectedAppointmentsCount == 1 && GetAppointmentsCountByArrivalType(AppointmentArrivalStatus.EarlyArrival) == 1)
					{
						IntializeErrorControls(AppointmentArrivalStatus.EarlyArrival);
						_arrivalstatus = AppointmentArrivalStatus.EarlyArrival;
					}
					else if (selectedAppointmentsCount == 1 && GetAppointmentsCountByArrivalType(AppointmentArrivalStatus.LateArrival) == 1)
					{
						IntializeErrorControls(AppointmentArrivalStatus.LateArrival);
						_arrivalstatus = AppointmentArrivalStatus.LateArrival;
					}
					else
					{
						IsUnableToArriveDetailsVisible = null;

						IntializeErrorControls(AppointmentArrivalStatus.MultiplePatientsFound);
						_arrivalstatus = AppointmentArrivalStatus.MultiplePatientsFound;
					}
				}
			}).ContinueWith(
					t =>
					{
						Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => IsProgressBarVisible = null));
						EnableScreenTap = true;
						if (IsGridNoDetailsVisible == null)
						{
							if (AppointmentCollection.Count(appointmentDetail => appointmentDetail.IsChecked) > 0)
							{
								AppPages appPages;
								if (isSuccess && ShowDemoDetails())
									appPages = AppPages.DemographicMessages;
								else if (isSuccess)
									appPages = AppPages.Finish;
								else
									appPages = AppPages.ExceptionDivert;
								Messenger.Default.Send(appPages);
							}
						}
					},
					TaskScheduler.FromCurrentSynchronizationContext());
		}

		private static bool ShowDemoDetails()
		{
			var demographicRepository = DiResolver.CurrentInstance.Reslove<IDemographicRepository>();
			UserDemographicSetting userDemographicSetting = GlobalVariables.UserDemographicDetailsSettings;
			bool isdatapresent = userDemographicSetting.UserDemographicLists != null && userDemographicSetting.UserDemographicLists.Count > 0;
			bool showDemographicDetailsForPatient = demographicRepository.ShowDemographicDetailsForPatient(userDemographicSetting.Duration);

			return showDemographicDetailsForPatient && userDemographicSetting.ShowDetails && isdatapresent;
		}

		private int GetAppointmentsCountByArrivalType(AppointmentArrivalStatus arrivalType)
		{
			return AppointmentCollection.Count(appointmentDetail => appointmentDetail.ArrivalType == arrivalType &&
				appointmentDetail.IsChecked);
		}

		private void IntializeErrorControls(AppointmentArrivalStatus arrivalType)
		{
			try
			{
				GlobalVariables.UserDemographicDetailsSettings = _configRepository.GetKioskConfiguration<UserDemographicSetting>(KioskConfigType.UserDemographicDetails.ToString());

				AppointmentDetail selectedAppointment = AppointmentCollection.FirstOrDefault(x => x.IsChecked);
				ErrorCodeText = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorCodeText];

				switch (arrivalType)
				{
					case AppointmentArrivalStatus.LateArrival:

						IsGridNoDetailsVisible = true;
						IsGridNoContentVisible = null;

						NoDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.SorryLateArrivalText];
						ErrorCode = Constants.OpenBracket + GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorCodeForLateArrival] + Constants.CloseBracket;
						UnableToArriveText = GlobalVariables.SelectedLanguageIdText[LanguageText.UnfortunatelyLateArrivalText];
						UnableToArriveDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.LateArrivalDetailsText];

						if (selectedAppointment != null)
						{
							string appointmentTime = selectedAppointment.Time;
							int index = -1;

							UnableToArriveDetailsText = Regex.Replace(UnableToArriveDetailsText, @"##",
								delegate
								{
									index++;
									return index == 0 ? appointmentTime : _arrival.LateArrival.ToString(CultureInfo.InvariantCulture);
								});
						}

						GoToReceptionText = GetReceptionText(selectedAppointment.ReceptionName);
						ClearScreenContent = GlobalVariables.SelectedLanguageIdText[LanguageText.ClearScreenText];
						break;

					case AppointmentArrivalStatus.EarlyArrival:

						IsGridNoDetailsVisible = true;
						IsGridNoContentVisible = null;

						NoDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.SorryEarlyArrivalText];
						ErrorCode = Constants.OpenBracket + GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorCodeForEarlyArrival] + Constants.CloseBracket;
						UnableToArriveText = GlobalVariables.SelectedLanguageIdText[LanguageText.UnfortunatelyEarlyArrivalText];
						UnableToArriveDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.EarlyArrivalDetailsText];

						if (selectedAppointment != null)
						{
							string appointmentTime = selectedAppointment.Time;
							int index = -1;

							UnableToArriveDetailsText = Regex.Replace(UnableToArriveDetailsText, @"##",
								delegate
								{
									index++;
									return index == 0 ? appointmentTime : _arrival.EarlyArrival.ToString(CultureInfo.InvariantCulture);
								});
						}

						GoToReceptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.EarlyArrivalTryAgainOrSpeakToReceptionText];
						ClearScreenContent = GlobalVariables.SelectedLanguageIdText[LanguageText.ClearScreenText];
						break;

					case AppointmentArrivalStatus.DoctorDivert:

						IsGridNoDetailsVisible = true;
						IsGridNoContentVisible = null;

						NoDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.SorrySomethingWrongText];
						ErrorCode = Constants.OpenBracket + GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorCodeForDoctorDivert] + Constants.CloseBracket;
						UnableToArriveText = GlobalVariables.SelectedLanguageIdText[LanguageText.UnableToArriveText];
						UnableToArriveDetailsText = string.Empty;
						GoToReceptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.DoubleCheckOrCheckWithReceptionText];
						ClearScreenContent = GlobalVariables.SelectedLanguageIdText[LanguageText.ClearScreenText];
						break;

					case AppointmentArrivalStatus.WrongLocation:

						IsGridNoDetailsVisible = true;
						IsGridNoContentVisible = null;

						NoDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.SorrySomethingWrongText];
						ErrorCode = Constants.OpenBracket + GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorCodeForWrongLocation] + Constants.CloseBracket;
						UnableToArriveText = GlobalVariables.SelectedLanguageIdText[LanguageText.UnableToArriveText];
						UnableToArriveDetailsText = string.Empty;
						GoToReceptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.WrongLocationCheckWithReceptionText];
						ClearScreenContent = GlobalVariables.SelectedLanguageIdText[LanguageText.ClearScreenText];

						break;

					case AppointmentArrivalStatus.PatientNotFound:

						NoDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.SorrySomethingWrongText];
						ErrorCode = Constants.OpenBracket + GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorCodeForPatientNotFound] + Constants.CloseBracket;
						UnableToArriveText = GlobalVariables.SelectedLanguageIdText[LanguageText.UnableToFindDetailsText];
						UnableToArriveDetailsText = string.Empty;
						GoToReceptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.DoubleCheckOrCheckWithReceptionText];
						ClearScreenContent = GlobalVariables.SelectedLanguageIdText[LanguageText.ClearScreenText];
						break;

					case AppointmentArrivalStatus.MultiplePatientsFound:

						NoDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.SorrySomethingWrongText];
						ErrorCode = Constants.OpenBracket + GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorCodeForMultiplePatientsFound] + Constants.CloseBracket;
						UnableToArriveText = GlobalVariables.SelectedLanguageIdText[LanguageText.UnableToArriveText];
						UnableToArriveDetailsText = string.Empty;
						GoToReceptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.DoubleCheckOrCheckWithReceptionText];
						ClearScreenContent = GlobalVariables.SelectedLanguageIdText[LanguageText.ClearScreenText];
						break;

					default:

						NoDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.UnableProcessRequest];
						UnableToArriveText = string.Empty;
						UnableToArriveDetailsText = string.Empty;
						GoToReceptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.ReportToReception];
						ClearScreenContent = GlobalVariables.SelectedLanguageIdText[LanguageText.Ok];
						break;

				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void FillAppointmentList()
		{
			try
			{
				AppointmentCollection = new AppointmentCollection();
				MatchAppointmentList();
				if (_appointments == null || _appointments.Count == 0)
				{
					ResyncAppointmentDetails();
				}

				if (AppointmentCollection != null && AppointmentCollection.Count > 0)
				{
					if (GlobalVariables.SelectedOrganisation.MainLocation)
					{
						var apiHelper = new ApiHelper();
						_orgDbId = apiHelper.GetOrganisationId();
					}

					if (AppointmentCollection.Count == 1)
					{
						AppointmentCollection.OrderByDescending(x => x.AppointmentDate);
						AppointmentCollection.ForEach(x => x.IsCheckBoxVisible = false);

						SetEnablePropertyForAppointments();
						AppointmentCollection.ForEach(a => a.IsChecked = true);

						ArriveSelectionCount = null;
						ArriveAppointment = null;
					}
					else
					{
						AppointmentCollection.OrderBy(x => x.AppointmentDate);
						AppointmentCollection.ForEach(x => x.IsCheckBoxVisible = true);

						SetEnablePropertyForAppointments();

						foreach (var appointment in AppointmentCollection.Where(appointment => !appointment.IsEnabled))
						{
							appointment.IsCheckBoxVisible = false;
						}

						ArriveSelectionCount = 0;
					}
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		private void SetEnablePropertyForAppointments()
		{
			AppointmentCollection.ForEach(x => x.IsEnabled = !CheckForWrongLocation(x) && !CheckForDivert(x) && (x.ArrivalType == AppointmentArrivalStatus.OnTime));

			foreach (AppointmentDetail appointment in AppointmentCollection.Where(x => !x.IsEnabled))
			{
				appointment.DelayText = string.Empty;
			}

			foreach (AppointmentDetail appointment in AppointmentCollection.Where(x => !x.IsEnabled))
			{
				appointment.IsErrorMessageVisible = true;

				switch (appointment.ArrivalType)
				{
					case AppointmentArrivalStatus.WrongLocation:
						appointment.ErrorCode = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForWrongLocation] + " " + GlobalVariables.SelectedLanguageIdText[LanguageText.LateArrivalSpeakToReceptionText];
						break;

					case AppointmentArrivalStatus.DoctorDivert:
						appointment.ErrorCode = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForDoctorDivert] + " " + GlobalVariables.SelectedLanguageIdText[LanguageText.LateArrivalSpeakToReceptionText];
						break;

					case AppointmentArrivalStatus.EarlyArrival:
						appointment.ErrorCode = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForEarlyArrival] + " " + GlobalVariables.SelectedLanguageIdText[LanguageText.LateArrivalSpeakToReceptionText];
						appointment.ErrorCode = Regex.Replace(appointment.ErrorCode, @"##", _arrival.EarlyArrival.ToString(CultureInfo.InvariantCulture));
						break;

					case AppointmentArrivalStatus.LateArrival:
						appointment.ErrorCode = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForLateArrival] + " " + GetReceptionText(appointment.ReceptionName);
						break;

				}
			}
		}

		private string GetReceptionText(string ReceptionName)
		{
			if (IsTopas && !string.IsNullOrEmpty(ReceptionName))
				return GlobalVariables.SelectedLanguageIdText[LanguageText.ReceptionTextForEarlyOrLateArrival] + " " + ReceptionName + ".";

			return GlobalVariables.SelectedLanguageIdText[LanguageText.ReceptionTextError];
		}

		private void SetArriveButtonText()
		{
			ArriveButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.MultipleArrivalButtonText];

			switch (ArriveSelectionCount)
			{
				case 0:
					ArriveButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.PleaseSelectAppointmentToProceedText];
					ArriveSelectionCount = null;
					ArriveAppointment = string.Empty;
					break;
				case 1:
					ArriveAppointment = GlobalVariables.SelectedLanguageIdText[LanguageText.Appointment];
					break;
				default:
					ArriveAppointment = GlobalVariables.SelectedLanguageIdText[LanguageText.Appointments];
					break;
			}
		}

		private void MatchAppointmentList()
		{
			try
			{
				_appointments = _appointmentRepository.GetMatchingAppointments(_appointments, _isSynched);

				if (_appointments != null && _appointments.Count > 0)
				{
					_appointments = _appointments.OrderBy(a => a.AppointmentTime).ToList();
					MatchMemberData();

					_distinctPatientCount = _appointments.GroupBy(appointment => appointment.BookedPatient.Id).ToList().Count;
					if (_distinctPatientCount == 1)
					{
						AddAppointmentsToList(_appointments);
					}
					else if (_distinctPatientCount > 1)
					{
						Logger.Instance.WriteLog(
									LogType.Info,
									ActionType.MultipleMatches.GetDisplayName(),
									null,
									KioskId);
					}
					else if (_isSynched && _distinctPatientCount == 0)
					{
						FillAppointmentListNone();
					}
				}
				else if (_isSynched)
				{
					FillAppointmentListNone();
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void MatchMemberData()
		{
			try
			{
				var memberList = _configRepository.GetKioskConfiguration<List<LinkedMember>>(KioskConfigType.LinkedMember.ToString());

				if (memberList != null)
				{
					memberList = memberList.Where(linkedMember => linkedMember.OrganisationId.ToString(CultureInfo.InvariantCulture).Equals(GlobalVariables.SelectedOrganisation.OrganisationId)).ToList();
					if (memberList.Count > 0)
					{
						List<Appointment> list = (_appointments.Join(memberList, idAppointment => idAppointment.SessionHolder.Id, idMember => idMember.SessionHolderId, (idAppointment, idMember) => new Appointment
						{
							Id = idAppointment.Id,
							AppointmentTime = idAppointment.AppointmentTime,
							SiteId = idAppointment.SiteId,
							SessionHolder = idAppointment.SessionHolder,
							BookedPatient = idAppointment.BookedPatient
						})).ToList();
						_appointments = list;
					}
					else
						_appointments = _appointments.ToList();
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void AddAppointmentsToList(List<Appointment> appointments)
		{
			GlobalVariables.ArrivedPatientDetails = appointments.First();

			foreach (Appointment appointment in appointments)
			{
				PatientNameText = (string.IsNullOrEmpty(appointment.BookedPatient.Title) ? string.Empty : appointment.BookedPatient.Title + " ") +
					appointment.BookedPatient.FirstName + " " +
					appointment.BookedPatient.FamilyName;
				IsGridNoDetailsVisible = null;
				IsGridNoContentVisible = true;
				IDictionary<bool, AppointmentArrivalStatus> kioskArrival = CompareArrivalTime(appointment.AppointmentTime);

				AppointmentCollection.Add(new AppointmentDetail
				{
					DoctorId = appointment.SessionHolder.Id,
					SiteId = appointment.SiteId,
					AppointmentId = appointment.Id,
					Name = appointment.SessionHolder.DisplayName,
					Time = appointment.AppointmentTime.ToString("h:mm tt"),
					AppointmentDate = appointment.AppointmentTime,
					IsEnabled = kioskArrival.Count <= 1,
					ArrivalType = kioskArrival.Last().Value,
					DelayText = GetDelayText(appointment),
					ReceptionName = appointment.Reception
				});
			}
		}

		private static string GetDelayText(Appointment appointment)
		{
			return GlobalVariables.KioskArrival != null && (appointment.SessionHolder.WaitingTime > 0 && GlobalVariables.KioskArrival.ShowDoctorDelay)
				? Constants.OpenBracket + appointment.SessionHolder.WaitingTime + Constants.ArrivalDelayTimeText + Constants.CloseBracket
				: string.Empty;
		}

		private void ResyncAppointmentDetails()
		{
			_isSynched = true;

			try
			{
				var apiHelper = new ApiHelper();
				_appointments = apiHelper.GetBookedPatients();
				if (_appointments != null && _appointments.Count > 0)
				{
					_appointments = _appointments.OrderBy(a => a.AppointmentTime).ToList();
					foreach (Appointment item in _appointments)
					{
						item.BookedPatient.Gender = item.BookedPatient.Gender.DecryptAES256();
						item.BookedPatient.Title = item.BookedPatient.Title.DecryptAES256();
						item.BookedPatient.CallingName = item.BookedPatient.CallingName.DecryptAES256();
						item.BookedPatient.FirstName = item.BookedPatient.FirstName.DecryptAES256();
						item.BookedPatient.FamilyName = item.BookedPatient.FamilyName.DecryptAES256();
						item.BookedPatient.Email = item.BookedPatient.Email.DecryptAES256();
						item.BookedPatient.Mobile = item.BookedPatient.Mobile.DecryptAES256();
						item.BookedPatient.HomeTelephone = item.BookedPatient.HomeTelephone.DecryptAES256();
						item.BookedPatient.WorkTelephone = item.BookedPatient.WorkTelephone.DecryptAES256();
						item.BookedPatient.PostCode = item.BookedPatient.PostCode.DecryptAES256();
						item.BookedPatient.Dob = item.BookedPatient.Dob.DecryptAES256();
					}

					if (_appointments.Any(a => a.BookedPatient.Id < 1))
					{
						_isSynched = false;
					}

					MatchAppointmentList();

					if (_appointments == null || _appointments.Count == 0)
					{
						FillAppointmentListNone();
					}
				}
				else
				{
					FillAppointmentListNone();
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		private void FillAppointmentListNone()
		{
			IsGridNoDetailsVisible = true;
			IsGridNoContentVisible = null;
		}

		private bool ConfirmAppointment()
		{
			var isSuccess = false;
			var doctorList = new List<int>();

			try
			{
				if (AppointmentCollection.Count > 0)
				{
					var apiHelper = new ApiHelper();
					foreach (AppointmentDetail appointment in AppointmentCollection.Where(appointment => appointment.IsChecked && appointment.ArrivalType == AppointmentArrivalStatus.OnTime))
					{
						isSuccess = apiHelper.SetAppointmentStatus((Int32)appointment.AppointmentId);
						doctorList.Add(appointment.DoctorId);
						appointment.ConfirmationFailed = !isSuccess;
						if (isSuccess)
						{
							if (GlobalVariables.IsMuliplePatientCheckDone)
								Logger.Instance.WriteLog(LogType.Info, ActionType.MultipleMatchesValidated.GetDisplayName(), null, KioskId);
							string message = GlobalVariables.IsBarCodeArrivalDone ? ActionType.BarcodeArrival.ToString() : ActionType.Arrived.ToString();
							message = message + " - " + GlobalVariables.ArrivedPatientDetails.BookedPatient.Id + " - " + appointment.Name + " - " + appointment.Time;
							Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
							_appointmentRepository.UpdateAppointmentStatus((Int32)appointment.AppointmentId);
						}
						else
							Logger.Instance.WriteLog(
								LogType.Info,
								GlobalVariables.IsMuliplePatientCheckDone ? ActionType.MultipleMatchesInValid.GetDisplayName() : ActionType.UnsuccessfullArrival.GetDisplayName(),
								null,
								KioskId);
					}

					isSuccess = AppointmentCollection.Count(appointmentDetail => appointmentDetail.ConfirmationFailed) <= 0;
				}

				GlobalVariables.ArrivedPatientDoctorList = doctorList;
				return isSuccess;
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
				return false;
			}
		}

		private IDictionary<bool, AppointmentArrivalStatus> CompareArrivalTime(DateTime appointmentTime)
		{
			var result = new Dictionary<bool, AppointmentArrivalStatus>();

			try
			{
				result.Add(true, AppointmentArrivalStatus.OnTime);
				var kioskArrival = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
				GlobalVariables.KioskArrival = kioskArrival;
				int earlyArrival = (kioskArrival != null) ? kioskArrival.EarlyArrival : 0;
				int lateArrival = (kioskArrival != null) ? kioskArrival.LateArrival : 0;
				if (appointmentTime < DateTime.Now)
				{
					if (lateArrival > 0 && ((DateTime.Now - appointmentTime).TotalMinutes > lateArrival))
						result.Add(false, AppointmentArrivalStatus.LateArrival);
				}
				else if (appointmentTime > DateTime.Now)
				{
					if (earlyArrival > 0 && ((appointmentTime - DateTime.Now).TotalMinutes > earlyArrival))
						result.Add(false, AppointmentArrivalStatus.EarlyArrival);
				}

				return result;
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return null;
			}
		}
	}
}