using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System.Globalization;
using EMIS.PatientFlow.Common.Extensions;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class FirstAvailableAppointmentViewModel : ViewModelBase
	{
		private string _whenDoUWantAppointmentText;
		private string _chooseSpecificDateText;
		private string _inOneWeekText;
		private string _inTwoWeeksText;
		private string _inOneMonthText;
		private string _inOneWeekDate;
		private string _inTwoWeeksDate;
		private string _inOneMonthDate;

		private string _nextAvailableAppointmentText;
		private string _bookThisAppointmentText;
		private string _bookingDate;
		private string _bookingTime;
		private string _slotName;
		private string _doctorName;
		private string _branchsiteName;

		private bool? _isAvailableAppointmentDetailsVisible;
		private bool? _isProgressBarVisible;
		private bool? _isFirstAvailableAppointmentVisible;

		private IConfigurationRepository _configRepository;
		private RelayCommand<AppPages> _bookThisAppointmentCommand;
		private RelayCommand<int> _moreAppointmentOptionsCommand;
		private RelayCommand<AppPages> _clearScreenCommand;
		private RelayCommand<string> _loadedCommand;


		public string WhenDoUWantAppointmentText
		{
			get
			{
				return _whenDoUWantAppointmentText;
			}

			set
			{
				_whenDoUWantAppointmentText = value;
				RaisePropertyChanged("WhenDoUWantAppointmentText");
			}
		}

		public string ChooseSpecificDateText
		{
			get
			{
				return _chooseSpecificDateText;
			}

			set
			{
				_chooseSpecificDateText = value;
				RaisePropertyChanged("ChooseSpecificDateText");
			}
		}

		public string InOneWeekText
		{
			get
			{
				return _inOneWeekText;
			}

			set
			{
				_inOneWeekText = value;
				RaisePropertyChanged("InOneWeekText");
			}
		}

		public string InTwoWeeksText
		{
			get
			{
				return _inTwoWeeksText;
			}

			set
			{
				_inTwoWeeksText = value;
				RaisePropertyChanged("InTwoWeeksText");
			}
		}

		public string InOneMonthText
		{
			get
			{
				return _inOneMonthText;
			}

			set
			{
				_inOneMonthText = value;
				RaisePropertyChanged("InOneMonthText");
			}
		}

		public string InOneWeekDate
		{
			get
			{
				return _inOneWeekDate;
			}

			set
			{
				_inOneWeekDate = value;
				RaisePropertyChanged("InOneWeekDate");
			}
		}

		public string InTwoWeeksDate
		{
			get
			{
				return _inTwoWeeksDate;
			}

			set
			{
				_inTwoWeeksDate = value;
				RaisePropertyChanged("InTwoWeeksDate");
			}
		}

		public string InOneMonthDate
		{
			get
			{
				return _inOneMonthDate;
			}

			set
			{
				_inOneMonthDate = value;
				RaisePropertyChanged("InOneMonthDate");
			}
		}

		public string NextAvailableAppointmentText
		{
			get
			{
				return _nextAvailableAppointmentText;
			}

			set
			{
				_nextAvailableAppointmentText = value;
				RaisePropertyChanged("NextAvailableAppointmentText");
			}
		}

		public string BookThisAppointmentText
		{
			get
			{
				return _bookThisAppointmentText;
			}

			set
			{
				_bookThisAppointmentText = value;
				RaisePropertyChanged("BookThisAppointmentText");
			}
		}

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
				return _branchsiteName;
			}

			set
			{
				_branchsiteName = value;
				RaisePropertyChanged("BranchSiteName");
			}
		}

		public bool? IsAvailableAppointmentDetailsVisible
		{
			get
			{
				return _isAvailableAppointmentDetailsVisible;
			}

			set
			{
				_isAvailableAppointmentDetailsVisible = value;
				RaisePropertyChanged("IsAvailableAppointmentDetailsVisible");
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

		public bool? IsFirstAvailableAppointmentVisible
		{
			get
			{
				return _isFirstAvailableAppointmentVisible;
			}

			set
			{
				_isFirstAvailableAppointmentVisible = value;
				RaisePropertyChanged("IsFirstAvailableAppointmentVisible");
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

		public RelayCommand<AppPages> BookThisAppointmentCommand
		{
			get
			{
				return _bookThisAppointmentCommand
					   ?? (_bookThisAppointmentCommand = new RelayCommand<AppPages>(
										  p =>
										  {
											  Messenger.Default.Send(AppPages.ConfirmBooking);
										  }));
			}
		}

		public RelayCommand<int> MoreAppointmentOptionsCommand
		{
			get
			{
				return _moreAppointmentOptionsCommand
					   ?? (_moreAppointmentOptionsCommand = new RelayCommand<int>(
										  p =>
										  {
											  GetValue(p);
										  }));
			}
		}

		internal void GetValue(int commandParameter)
		{
			if (commandParameter == 4)
			{
				Messenger.Default.Send(AppPages.BookingTimeSelection);
				return;
			}
			switch (commandParameter)
			{
				case 1:
					GlobalVariables.Appointment.SessionDate = InOneWeekDate;
					break;
				case 2:
					GlobalVariables.Appointment.SessionDate = InTwoWeeksDate;
					break;
				case 3:
					GlobalVariables.Appointment.SessionDate = InOneMonthDate;
					break;
			}

			Messenger.Default.Send(AppPages.BookMe);
		}

		public RelayCommand<AppPages> ClearScreenCommand
		{
			get
			{
				return _clearScreenCommand
					   ?? (_clearScreenCommand = new RelayCommand<AppPages>(
										  p =>
										  {
											  Messenger.Default.Send(AppPages.HomePage);
										  }));
			}
		}

		public FirstAvailableAppointmentViewModel()
		{
			InitializeControls();
			SetControlText();
		}

		private void InitializeControls()
		{
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			GlobalVariables.BookAppointmentSettings = _configRepository.GetKioskConfiguration<BookingAppointment>
															(KioskConfigType.BookingAppointment.ToString());

			IsAvailableAppointmentDetailsVisible = null;
			IsFirstAvailableAppointmentVisible = null;
			IsProgressBarVisible = true;

			AppointmentSlots slot = null;
			ApiHelper api = new ApiHelper();
			Task.Factory.StartNew(() =>
			{
				slot = api.GetFirstAppointmentSlots(DateTime.Today);
			}).
			ContinueWith(
				t =>
				{
					Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => IsProgressBarVisible = null));
					if (slot == null)
					{
						IsFirstAvailableAppointmentVisible = null;
						IsAvailableAppointmentDetailsVisible = true;
						
						string message = " Appointment Not Found" + " - " + GlobalVariables.Appointment.PatientId + " Slot Type : " + ((GlobalVariables.SelectedSlotType != null) ? GlobalVariables.SelectedSlotType.Description : "All Slots");
						Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
					}
					else
					{
						IsAvailableAppointmentDetailsVisible = true;
						IsFirstAvailableAppointmentVisible = true;

						GlobalVariables.Appointment.SlotId = slot.SlotId;
						BookingTime = slot.StartTime;
						BookingTime = string.Concat(slot.StartTime, " - ", slot.EndTime);
						GlobalVariables.Appointment.SessionTime = BookingTime;
						GlobalVariables.Appointment.SlotStartTime = slot.StartTime;
						BookingDate = GlobalVariables.Appointment.SessionDate;
						GetBookingDisplayDate();

						var doctorDetails = GlobalVariables.DoctorDetailsBooking.FirstOrDefault(doctorDetailsBooking => doctorDetailsBooking.DoctorId == GlobalVariables.Appointment.DoctorId);
						if (doctorDetails != null)
						{
							DoctorName = doctorDetails.DoctorNameToDisplay;
							BranchSiteName = GlobalVariables.Appointment.SiteName;
						}
						GlobalVariables.Appointment.SlotName = slot.SlotTypeDescription;
						SlotName = Constants.OpenBracket + slot.SlotTypeDescription + Constants.CloseBracket;
					}
					
					InOneWeekDate = String.Format("{0:ddd dd MMMM yyyy}", DateTime.Today.AddDays(7));
					InTwoWeeksDate = String.Format("{0:ddd dd MMMM yyyy}", DateTime.Today.AddDays(14));
					InOneMonthDate = String.Format("{0:ddd dd MMMM yyyy}", DateTime.Today.AddMonths(1));
				},
				TaskScheduler.FromCurrentSynchronizationContext());

		}

		internal void SetControlText()
		{
			WhenDoUWantAppointmentText = GlobalVariables.SelectedLanguageIdText[LanguageText.WhenDoUWantAppointmentText];
			ChooseSpecificDateText = GlobalVariables.SelectedLanguageIdText[LanguageText.ChooseSpecificDate];
			InOneWeekText = GlobalVariables.SelectedLanguageIdText[LanguageText.InOneWeekText];
			InTwoWeeksText = GlobalVariables.SelectedLanguageIdText[LanguageText.InTwoWeeksText];
			InOneMonthText = GlobalVariables.SelectedLanguageIdText[LanguageText.InOneMonthText];

			NextAvailableAppointmentText = GlobalVariables.SelectedLanguageIdText[LanguageText.NextAvailableAppointmentText];
			BookThisAppointmentText = GlobalVariables.SelectedLanguageIdText[LanguageText.BookThisAppointmentText];
		}

		private void GetBookingDisplayDate()
		{
			DateTime dateFormat = DateTime.ParseExact(BookingDate, "ddd dd MMMM yyyy", CultureInfo.InvariantCulture);
			int day = dateFormat.Day;
			string ordinal = ViewModelHelper.AddOrdinal(day);
			BookingDate = String.Format("{0:dddd dd}{1} {0:MMMM yyyy}", dateFormat, ordinal);
		}
	}
}