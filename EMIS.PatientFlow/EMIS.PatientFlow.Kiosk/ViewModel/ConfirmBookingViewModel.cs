using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
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
	public class ConfirmBookingViewModel : ViewModelBase
	{
		private bool _bookingStatus;
		private string _bookingDate;
		private string _bookingTime;
		private string _slotName;
		private string _doctorName;
		private string _branchSiteName;
		private string _enterReasonForAppointmentText;
		private string _optionalOrMandatoryText;
		private string _selectedAnswer;
		private string _bookMyAppointmentText;
		private int _enteredCharText;
		private int _maximumCharText;

		private bool? _isReasonTextVisible;
		private bool _enableScreenTap;
		private bool? _isProgressBarVisible;

		private RelayCommand<AppPages> _bookMyAppointmentCommand;
		private RelayCommand<string> _loadedCommand;

		private const string GrayColorForTextBox = "#696969";
		private const string RedColorForTextBox = "#E5685E";
		private const string RedBorderForTextBox = "#E16F6C";
		private const string GrayBorderForTextBox = "#CCCCCC";

		public string BookingDate
		{
			get
			{
				return _bookingDate;
			}

			set
			{
				_bookingDate = value;
				RaisePropertyChanged("BookingDate");
			}
		}

		public string BookingTime
		{
			get
			{
				return _bookingTime;
			}

			set
			{
				_bookingTime = value;
				RaisePropertyChanged("BookingTime");
			}
		}

		public string SlotName
		{
			get
			{
				return _slotName;
			}

			set
			{
				_slotName = value;
				RaisePropertyChanged("SlotName");
			}
		}

		public string DoctorName
		{
			get
			{
				return _doctorName;
			}

			set
			{
				_doctorName = value;
				RaisePropertyChanged("DoctorName");
			}
		}

		public string BranchSiteName
		{
			get
			{
				return _branchSiteName;
			}

			set
			{
				_branchSiteName = value;
				RaisePropertyChanged("BranchSiteName");
			}
		}

		public string EnterReasonForAppointmentText
		{
			get
			{
				return _enterReasonForAppointmentText;
			}

			set
			{
				_enterReasonForAppointmentText = value;
				RaisePropertyChanged("EnterReasonForAppointmentText");
			}
		}

		public string OptionalOrMandatoryText
		{
			get
			{
				return _optionalOrMandatoryText;
			}

			set
			{
				_optionalOrMandatoryText = value;
				RaisePropertyChanged("OptionalOrMandatoryText");
			}
		}

		public string SelectedAnswer
		{
			get
			{
				return _selectedAnswer;
			}

			set
			{
				_selectedAnswer = value;
				RaisePropertyChanged("SelectedAnswer");
				RaisePropertyChanged("EnteredCharText");
				RaisePropertyChanged("TextBoxForegroundColor");
				RaisePropertyChanged("TexBoxBorderBrush");
				RaisePropertyChanged("CannotBeMoreThanXCharText");
			}
		}

		public string BookMyAppointmentText
		{
			get
			{
				return _bookMyAppointmentText;
			}

			set
			{
				_bookMyAppointmentText = value;
				RaisePropertyChanged("BookMyAppointmentText");
			}
		}

		public int EnteredCharText
		{
			get
			{
				return (SelectedAnswer != null ? SelectedAnswer.Length : 0);
			}

			set
			{
				_enteredCharText = value;
			}
		}

		public int MaximumCharText
		{
			get
			{
				return _maximumCharText;
			}

			set
			{
				_maximumCharText = value;
				RaisePropertyChanged("MaximumCharText");
			}
		}

		public string TextBoxForegroundColor
		{
			get
			{
				return (EnteredCharText <= MaximumCharText ? GrayColorForTextBox : RedColorForTextBox);
			}
		}

		public string TexBoxBorderBrush
		{
			get
			{
				return (EnteredCharText <= MaximumCharText ? GrayBorderForTextBox : RedBorderForTextBox);
			}
		}

		public string CannotBeMoreThanXCharText
		{
			get
			{
				return (EnteredCharText <= MaximumCharText ? string.Empty : GlobalVariables.SelectedLanguageIdText[LanguageText.CannotBeMoreThanXCharText]);
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
											  if (!GlobalVariables.IsDbConnected)
											  {
												  Messenger.Default.Send(AppPages.ExceptionDivert);
											  }
										  }));
			}
		}

		public bool? IsReasonTextVisible
		{
			get
			{
				return _isReasonTextVisible;
			}
			set
			{
				_isReasonTextVisible = value;
				RaisePropertyChanged("IsReasonTextVisible");
			}
		}

		public bool? IsProgressBarVisible
		{
			get { return _isProgressBarVisible; }
			set
			{
				_isProgressBarVisible = value;
				RaisePropertyChanged("IsProgressBarVisible");
			}
		}

		public RelayCommand<AppPages> BookMyAppointmentCommand
		{
			get
			{
				return _bookMyAppointmentCommand
					   ?? (_bookMyAppointmentCommand = new RelayCommand<AppPages>(
										  p =>
										  {
											  ConfirmBooking();
										  },
										  delegate { return IsNextVisible(); }));
			}
		}

		public bool EnableScreenTap
		{
			get { return _enableScreenTap; }
			set
			{
				_enableScreenTap = value;
				RaisePropertyChanged("EnableScreenTap");
			}
		}

		private bool IsNextVisible()
		{
			return !GlobalVariables.KioskSettings.AppointmentReason.HasValue || 
				(!GlobalVariables.KioskSettings.AppointmentReason.Value || !String.IsNullOrEmpty(SelectedAnswer)) && 
				(EnteredCharText <= MaximumCharText);
		}

		public ConfirmBookingViewModel()
		{
			InitializeControls();
			SetControlText();
		}

		internal void SetControlText()
		{
			EnterReasonForAppointmentText = GlobalVariables.SelectedLanguageIdText[LanguageText.EnterReasonForAppointmentText];

			if (GlobalVariables.KioskSettings != null && GlobalVariables.KioskSettings.AppointmentReason != null)
			{
				OptionalOrMandatoryText = GlobalVariables.KioskSettings.AppointmentReason == true ?
					Constants.OpenBracket + GlobalVariables.SelectedLanguageIdText[LanguageText.MandatoryText] + Constants.CloseBracket :
					Constants.OpenBracket + GlobalVariables.SelectedLanguageIdText[LanguageText.OptionalText] + Constants.CloseBracket;
			}
			else if (GlobalVariables.KioskSettings != null && GlobalVariables.KioskSettings.AppointmentReason == null)
			{
				IsReasonTextVisible = null;
			}

			BookMyAppointmentText = GlobalVariables.SelectedLanguageIdText[LanguageText.BookMyAppointmentText];
			RaisePropertyChanged("CannotBeMoreThanXCharText");
		}

		private void InitializeControls()
		{
			IsReasonTextVisible = true;
			IsProgressBarVisible = null;
			EnableScreenTap = true;
			MaximumCharText = 35;

			var configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			var kioskSettings = configRepository.GetKioskConfiguration<KioskSettings>(KioskConfigType.KioskSettings.ToString());
			GlobalVariables.KioskSettings = kioskSettings;

			BookingDate = GlobalVariables.Appointment.SessionDate;
			GetBookingDisplayDate();

			BookingTime = GlobalVariables.Appointment.SessionTime;
			SlotName = Constants.OpenBracket + GlobalVariables.Appointment.SlotName + Constants.CloseBracket;

			var doctorDetails = GlobalVariables.DoctorDetailsBooking.FirstOrDefault(doctorDetailsBooking => doctorDetailsBooking.DoctorId == GlobalVariables.Appointment.DoctorId);
			if (doctorDetails != null)
			{
				DoctorName = doctorDetails.DoctorNameToDisplay;
				BranchSiteName = GlobalVariables.Appointment.SiteName;
			}

			GlobalVariables.IsKeyboardInitialised = false;
			GlobalVariables.AnswerOptionLimit = GlobalVariables.AnswerOptionLimit < 38 ? 37 : GlobalVariables.AnswerOptionLimit;
		}

		private void GetBookingDisplayDate()
		{
			DateTime dateFormat = DateTime.ParseExact(BookingDate, "ddd dd MMMM yyyy", CultureInfo.InvariantCulture);
			int day = dateFormat.Day;
			string ordinal = ViewModelHelper.AddOrdinal(day);
			BookingDate = String.Format("{0:dddd dd}{1} {0:MMMM yyyy}", dateFormat, ordinal);
		}

		public void NavigateTo()
		{
			Messenger.Default.Send(_bookingStatus ? AppPages.FinishBooking : AppPages.ExceptionDivert);
		}

		private void ConfirmBooking()
		 {
			IsProgressBarVisible = true;
			EnableScreenTap = false;
			GlobalVariables.Day = "0";
			GlobalVariables.PatientMatchSelectedMonth = "0";
			Task.Factory.StartNew(() =>
			{
				var apiHelper = new ApiHelper();

				if (!string.IsNullOrEmpty(SelectedAnswer))
				{
					if (SelectedAnswer.Length > 36)
						SelectedAnswer = SelectedAnswer.Substring(0, 35);
					GlobalVariables.Appointment.Reason = SelectedAnswer;
				}
				else
				{
					GlobalVariables.Appointment.Reason = string.Empty;
				}
				GlobalVariables.IsKeyboardInitialised = false;
				_bookingStatus = apiHelper.BookAppointment(GlobalVariables.Appointment);
				if (_bookingStatus)
				{
					if (GlobalVariables.IsMuliplePatientCheckDone)
						Logger.Instance.WriteLog(
							LogType.Info,
							ActionType.MultipleMatchesValidated.GetDisplayName(),
							null,
							KioskId);

					string message = ActionType.Booked + " - " + GlobalVariables.Appointment.PatientId + " - " + DoctorName + " at " + GlobalVariables.Appointment.SessionDate +" - "+ GlobalVariables.Appointment.SessionTime + " in " + GlobalVariables.Appointment.SiteName;

					Logger.Instance.WriteLog(
						LogType.Info,
						message,
						null,
						KioskId);
				}
			}).ContinueWith(
				t =>
				{
					IsProgressBarVisible = null;
					EnableScreenTap = true;
					Dispatcher.CurrentDispatcher.BeginInvoke(new Action(NavigateTo));
				},
				TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}