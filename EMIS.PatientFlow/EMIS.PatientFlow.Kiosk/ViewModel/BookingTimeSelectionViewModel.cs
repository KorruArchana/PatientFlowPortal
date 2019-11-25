using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class BookingTimeSelectionViewModel : ViewModelBase
	{
		private string _selectAppointmentDateText;
		private string _calenderMonthText;
		private string _calenderYearText;
		private string _appointmentsText;
		private string _noAppointmentsText;
		private string _todayText;

		private List<SlotWeekCalender> _monthCalender;
		private List<CustomiseUserDisplayText> MonthList;
		private int SelectedMonth;
		private int SelectedYear;
		private bool _isPreviousButtonEnabled;
		private bool? _isProgressBarVisible;

		private RelayCommand<string> _loadedCommand;
		private RelayCommand _nextMonthCommand;
		private RelayCommand _previousMonthCommand;
		private RelayCommand<int> _dateSelectedCommand;

		public string SelectAppointmentDateText
		{
			get
			{
				return _selectAppointmentDateText;
			}
			set
			{
				_selectAppointmentDateText = value;
				RaisePropertyChanged("SelectAppointmentDateText");
			}
		}

		public string CalenderMonthText
		{
			get
			{
				return _calenderMonthText;
			}
			set
			{
				_calenderMonthText = value;
				RaisePropertyChanged("CalenderMonthText");
			}
		}

		public string CalenderYearText
		{
			get
			{
				return _calenderYearText;
			}
			set
			{
				_calenderYearText = value;
				RaisePropertyChanged("CalenderYearText");
			}
		}

		public string AppointmentsText
		{
			get
			{
				return _appointmentsText;
			}
			set
			{
				_appointmentsText = value;
				RaisePropertyChanged("AppointmentsText");
			}
		}

		public string NoAppointmentsText
		{
			get
			{
				return _noAppointmentsText;
			}
			set
			{
				_noAppointmentsText = value;
				RaisePropertyChanged("NoAppointmentsText");
			}
		}

		public string TodayText
		{
			get
			{
				return _todayText;
			}
			set
			{
				_todayText = value;
				RaisePropertyChanged("TodayText");
			}
		}

		public List<SlotWeekCalender> MonthCalender
		{
			get { return _monthCalender; }
			set
			{
				_monthCalender = value;
				RaisePropertyChanged("MonthCalender");
			}
		}

		public bool IsPreviousButtonEnabled
		{
			get
			{
				return _isPreviousButtonEnabled;
			}
			set
			{
				_isPreviousButtonEnabled = value;
				RaisePropertyChanged("IsPreviousButtonEnabled");
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

		public RelayCommand NextMonthCommand
		{
			get
			{
				return _nextMonthCommand
					?? (_nextMonthCommand = new RelayCommand(
											ForwardNavigation));
			}
		}

		public RelayCommand PreviousMonthCommand
		{
			get
			{
				return _previousMonthCommand
					?? (_previousMonthCommand = new RelayCommand(
											BackwardNavigation));
			}
		}

		public RelayCommand<int> DateSelectedCommand
		{
			get
			{
				return _dateSelectedCommand
					?? (_dateSelectedCommand = new RelayCommand<int>(
						p =>
						{
							DateTime date = new DateTime(SelectedYear, SelectedMonth, p);
							GlobalVariables.Appointment.SessionDate = String.Format("{0:ddd dd MMMM yyyy}", date);
							Messenger.Default.Send(AppPages.BookMe);
						}));

			}
		}

		public BookingTimeSelectionViewModel()
		{
			InitializeControls();
			GetMonthCalender();
			SetControlText();
		}

		private void InitializeControls()
		{
			MonthList = new List<CustomiseUserDisplayText>();
			
			IsPreviousButtonEnabled = false;

			MonthList = ViewModelHelper.GetMonthText();
			SelectedYear = DateTime.Today.Year;
			SelectedMonth = DateTime.Today.Month;
		}

		internal void SetControlText()
		{
			SelectAppointmentDateText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectAppointmentDateText];
			AppointmentsText = GlobalVariables.SelectedLanguageIdText[LanguageText.AppointmentsText];
			NoAppointmentsText = GlobalVariables.SelectedLanguageIdText[LanguageText.NoAppointmentsText];
			TodayText = GlobalVariables.SelectedLanguageIdText[LanguageText.TodayText];

			MonthList = ViewModelHelper.GetMonthText();
			CalenderMonthText = MonthList.FirstOrDefault(m => m.Value == (SelectedMonth.ToString("00"))).DisplayText;
		}

		private void GetMonthCalender()
		{
			CalenderMonthText = MonthList.FirstOrDefault(m => m.Value == (SelectedMonth.ToString("00"))).DisplayText;

			CalenderYearText = SelectedYear.ToString();

			if (SelectedYear == DateTime.Today.Year)
			{
				IsPreviousButtonEnabled = SelectedMonth > DateTime.Today.Month ? true : false;
			}
			//Always it should mean that SelectedYear > DateTime.Today.Year
			else
			{
				IsPreviousButtonEnabled = true;
			}

			IsProgressBarVisible = true;
			MonthCalender = AppointmentHelper.GetMonthAvailability(SelectedYear, SelectedMonth);
			IsProgressBarVisible = null;
		}

		private void ForwardNavigation()
		{
			if (SelectedMonth < 12)
			{
				SelectedMonth += 1;
			}
			else
			{
				SelectedYear += 1;
				SelectedMonth = 1;
			}

			GetMonthCalender();
		}

		private void BackwardNavigation()
		{
			if (SelectedMonth > 1)
			{
				SelectedMonth -= 1;
			}
			else
			{
				SelectedYear -= 1;
				SelectedMonth = 12;
			}
			GetMonthCalender();
		}

	}
}