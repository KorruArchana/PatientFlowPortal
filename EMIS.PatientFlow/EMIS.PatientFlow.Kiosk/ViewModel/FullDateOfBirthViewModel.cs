using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Threading;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class FullDateOfBirthViewModel : ViewModelBase
	{
		private RelayCommand<object> _backCommand;
		private RelayCommand<AppPages> _restartCommand;
		private bool? _isContinueVisible;
		private bool? _isErrorTextVisible;
		private string _errorText;
		private string _continueText;
		private string _findingYourAppointmentDetailsText;
		private string _enterDateOfBirthText;
		private string _dayText;
		private string _monthText;
		private string _yearText;
		private string _touchToEnterDayText;
		private string _touchToEnterMonthText;
		private string _touchToEnterYearText;
		private string _touchHereToEnterText;

		private RelayCommand _enterDayOfBirthCommand;
		private RelayCommand _enterMonthOfBirthCommand;
		private RelayCommand _enterYearOfBirthCommand;
		private RelayCommand _enterDateOfBirthYearCommand;
		private RelayCommand<bool> _continueCommand;

		private Boolean _canSelectDayOfBirth;
		private Boolean _canSelectMonthOfBirth;
		private Boolean _canSelectYearOfBirth;
		private Boolean _canSelectDateOfBirthYear;
		private Int32 _dayTextBlockValue;
		private Int32 _monthTextBlockValue;
		private Int32 _yearTextBlockValue;
		private Boolean? _patientMatchHasDayOfBirth;
		private Boolean? _patientMatchHasMonthOfBirth;
		private Boolean? _patientMatchHasYearOfBirthInput;
		private Boolean? _patientMatchHasYearOfBirthSelection;
		private Boolean? _patientMatchContainsDateOfBirthYearSelection;

		private SelectYearViewModel _yearViewModel;
		private SelectDobYearViewModel _selectDobYear;
		private SelectMonthViewModel _selectMonth;

		public FullDateOfBirthViewModel(SelectMonthViewModel selectMonth, SelectYearViewModel selectYear, SelectDobYearViewModel selectDobYear)
		{
			InitialiseGlobalVariables();


			_patientMatchHasDayOfBirth = null;
			_patientMatchHasMonthOfBirth = null;
			_patientMatchHasYearOfBirthInput = null;
			_patientMatchHasYearOfBirthSelection = null;
			_patientMatchContainsDateOfBirthYearSelection = null;

			var str = new List<string>
			{
					AppPages.SelectDobYear.GetDisplayName(),
					AppPages.SelectDay.GetDisplayName(),
					AppPages.SelectMonth.GetDisplayName(),
					AppPages.SelectYear.GetDisplayName()
				};

			SetSelectedValue(selectMonth, selectYear, selectDobYear, str);

			IsContinueVisible = IsValid();

			Messenger.Default.Register<DateOfBirthDetailsMessage>(
				this,
				DateOfBirthDetailsSelected);
		}

		private void SetSelectedValue(SelectMonthViewModel selectMonth, SelectYearViewModel selectYear,
			SelectDobYearViewModel selectDobYear, List<string> str)
		{
			foreach (var screenCode in GlobalVariables.PatientMatchOrderList.Select(match => match.ScreenCode).Where(str.Contains))
			{
				int output;
				if (AppPages.SelectDobYear.GetDisplayName() == screenCode)
				{
					_patientMatchContainsDateOfBirthYearSelection = true;
					_enterDateOfBirthYearCommand = new RelayCommand(AllowDateOfBirthYearSelection);
					_selectDobYear = selectDobYear;

					if (GlobalVariables.YearofBirth > 1900)
					{
						DateofBirthYearText = GlobalVariables.YearofBirth;
					}
				}
				else if (AppPages.SelectDay.GetDisplayName() == screenCode)
				{
					_patientMatchHasDayOfBirth = true;
					_enterDayOfBirthCommand = new RelayCommand(AllowDayOfBirthSelection);

					if (int.TryParse(GlobalVariables.Day, out output))
					{
						if (output > 0)
						{
							DayTextBlockValue = output;
							DayDisplayText = output + AddOrdinal(output);
						}
					}
				}

				else if (AppPages.SelectMonth.GetDisplayName() == screenCode)
				{
					_patientMatchHasMonthOfBirth = true;
					_selectMonth = selectMonth;
					_enterMonthOfBirthCommand = new RelayCommand(AllowMonthOfBirthSelection);

					if (int.TryParse(GlobalVariables.PatientMatchSelectedMonth, out output))
					{
						if (output > 0)
						{
							MonthTextBlockValue = output;
							MonthDisplayText = _monthList.First(a => a.Value == GlobalVariables.PatientMatchSelectedMonth).DisplayText;
						}
					}
				}

				else if (AppPages.SelectYear.GetDisplayName() == screenCode)
				{
					_patientMatchHasYearOfBirthInput = true;
					_patientMatchHasYearOfBirthSelection = true;
					_enterYearOfBirthCommand = new RelayCommand(AllowYearOfBirthSelection);
					_yearViewModel = selectYear;
				}
			}
		}

		public Boolean? PatientMatchContainsDayOfBirth
		{
			get
			{
				return _patientMatchHasDayOfBirth;
			}
		}

		public Boolean? PatientMatchContainsMonthOfBirth
		{
			get
			{
				return _patientMatchHasMonthOfBirth;
			}
		}

		public Boolean? PatientMatchContainsYearOfBirthInput
		{
			get
			{
				return _patientMatchHasYearOfBirthInput;
			}
		}

		public Boolean? PatientMatchContainsYearOfBirthSelection
		{
			get
			{
				return _patientMatchHasYearOfBirthSelection;
			}
		}

		public Boolean? PatientMatchContainsDateOfBirthYearSelection
		{
			get
			{
				return _patientMatchContainsDateOfBirthYearSelection;
			}
		}

		public Boolean CanSelectDayOfBirth
		{
			get { return _canSelectDayOfBirth; }
			private set
			{
				_canSelectDayOfBirth = value;
				RaisePropertyChanged("CanSelectDayOfBirth");

			}
		}

		public Boolean CanSelectMonthOfBirth
		{
			get { return _canSelectMonthOfBirth; }
			private set
			{
				_canSelectMonthOfBirth = value;
				RaisePropertyChanged("CanSelectMonthOfBirth");
			}
		}

		public Boolean CanSelectYearOfBirth
		{
			get { return _canSelectYearOfBirth; }
			private set
			{
				_canSelectYearOfBirth = value;
				RaisePropertyChanged("CanSelectYearOfBirth");
			}
		}

		public RelayCommand EnterDayOfBirthCommand
		{
			get { return _enterDayOfBirthCommand; }
		}

		public RelayCommand EnterMonthOfBirthCommand
		{
			get { return _enterMonthOfBirthCommand; }
		}

		public RelayCommand EnterYearOfBirthCommand
		{
			get { return _enterYearOfBirthCommand; }
		}

		public RelayCommand EnterDateOfBirthYearCommand
		{
			get { return _enterDateOfBirthYearCommand; }
		}

		public Int32 DayTextBlockValue
		{
			get
			{
				return _dayTextBlockValue;
			}
			private set
			{
				_dayTextBlockValue = value;
				RaisePropertyChanged("DayTextBlockValue");
				RaisePropertyChanged("DayOfBirthHasValue");
			}
		}

		private string _dayDisplayText;
		public string DayDisplayText
		{
			get
			{
				return _dayDisplayText;
			}
			private set
			{
				_dayDisplayText = value;
				RaisePropertyChanged("DayDisplayText");
			}
		}

		public Boolean DayOfBirthHasValue
		{
			get
			{
				return DayTextBlockValue > 0;
			}
		}

		private string _monthDisplayText;
		public string MonthDisplayText
		{
			get
			{
				return _monthDisplayText;
			}
			private set
			{
				_monthDisplayText = value;
				RaisePropertyChanged("MonthDisplayText");
			}
		}

		public Int32 MonthTextBlockValue
		{
			get
			{
				return _monthTextBlockValue;
			}
			private set
			{
				_monthTextBlockValue = value;
				RaisePropertyChanged("MonthTextBlockValue");
				RaisePropertyChanged("MonthOfBirthHasValue");
			}
		}

		public Boolean MonthOfBirthHasValue
		{
			get
			{
				return MonthTextBlockValue > 0;
			}
		}

		public Int32 YearTextBlockValue
		{
			get
			{
				return _yearTextBlockValue;
			}
			private set
			{
				_yearTextBlockValue = value;
				RaisePropertyChanged("YearTextBlockValue");
				RaisePropertyChanged("YearOfBirthHasValue");
			}
		}

		private Int32 _dateofBirthYearText;

		public Int32 DateofBirthYearText
		{
			get
			{
				return _dateofBirthYearText;
			}
			private set
			{
				_dateofBirthYearText = value;
				RaisePropertyChanged("DateofBirthYearText");
				RaisePropertyChanged("DateofBirthYearTextHasValue");
			}
		}

		public Boolean DateofBirthYearTextHasValue
		{
			get { return DateofBirthYearText > 0; }

		}

		public Boolean YearOfBirthHasValue
		{
			get
			{
				return YearTextBlockValue > 0;
			}
		}

		public bool IsValid()
		{
			Boolean isValid = true;

			if (_patientMatchHasDayOfBirth.HasValue && _patientMatchHasDayOfBirth.Value)
				isValid = DayOfBirthHasValue;

			if (_patientMatchHasMonthOfBirth.HasValue && _patientMatchHasMonthOfBirth.Value)
				isValid &= MonthOfBirthHasValue;

			if (_patientMatchHasYearOfBirthInput.HasValue && _patientMatchHasYearOfBirthInput.Value)
				isValid &= YearOfBirthHasValue;

			if (_patientMatchContainsDateOfBirthYearSelection.HasValue && _patientMatchContainsDateOfBirthYearSelection.Value)
				isValid &= DateofBirthYearTextHasValue;


			isValid = isValid && ValidDob();

			return isValid;
		}

		private bool ValidDob()
		{
			if (!_patientMatchHasDayOfBirth.HasValue || !_patientMatchHasDayOfBirth.Value ||
				!_patientMatchHasMonthOfBirth.HasValue || !_patientMatchHasMonthOfBirth.Value ||
				!_patientMatchContainsDateOfBirthYearSelection.HasValue || !_patientMatchContainsDateOfBirthYearSelection.Value)
				return true;

			string dateval = GlobalVariables.Day + "/" + GlobalVariables.PatientMatchSelectedMonth + "/" +
							 GlobalVariables.YearofBirth;
			DateTime datetime;
			IsErrorTextVisible = null;
			if (DateTime.TryParseExact(dateval, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
							System.Globalization.DateTimeStyles.None, out datetime))
			{
				GlobalVariables.PatientMatchDob = datetime.Date;
				GlobalVariables.PatientMatchDobFilter = dateval;
				return true;
			}
			IsErrorTextVisible = true;
			return false;
		}

		public RelayCommand<object> BackCommand
		{
			get
			{
				return _backCommand
					?? (_backCommand = new RelayCommand<object>(
										  p =>
										  {
											  UnRegisterDobMessage();
											  Utilities.PatientMatchBackwardNavigation();
										  }));
			}
		}

		public RelayCommand<AppPages> RestartCommand
		{
			get
			{
				return _restartCommand
					?? (_restartCommand = new RelayCommand<AppPages>(
										  p =>
										  {
											  UnRegisterDobMessage();
											  Messenger.Default.Send(AppPages.HomePage);
										  }));
			}
		}

		public bool? IsContinueVisible
		{
			get
			{
				return _isContinueVisible;
			}
			set
			{
				_isContinueVisible = value;
				RaisePropertyChanged("IsContinueVisible");
			}
		}

		public bool? IsErrorTextVisible
		{
			get
			{
				return _isErrorTextVisible;
			}
			set
			{
				_isErrorTextVisible = value;
				RaisePropertyChanged("IsErrorTextVisible");
			}
		}

		public String ErrorText
		{
			get
			{
				return _errorText;
			}
			set
			{
				_errorText = value;
				RaisePropertyChanged("ErrorText");
			}
		}

		public String ContinueText
		{
			get
			{
				return _continueText;
			}
			set
			{
				_continueText = value;
				RaisePropertyChanged("ContinueText");
			}
		}


		public String FindingYourAppointmentDetailsText
		{
			get
			{
				return _findingYourAppointmentDetailsText;
			}
			set
			{
				_findingYourAppointmentDetailsText = value;
				RaisePropertyChanged("FindingYourAppointmentDetailsText");
			}
		}

		public String EnterDateOfBirthText
		{
			get
			{
				return _enterDateOfBirthText;
			}
			set
			{
				_enterDateOfBirthText = value;
				RaisePropertyChanged("EnterDateOfBirthText");
			}
		}

		public String DayText
		{
			get
			{
				return _dayText;
			}
			set
			{
				_dayText = value;
				RaisePropertyChanged("DayText");
			}
		}

		public String MonthText
		{
			get
			{
				return _monthText;
			}
			set
			{
				_monthText = value;
				RaisePropertyChanged("MonthText");
			}
		}

		public String YearText
		{
			get
			{
				return _yearText;
			}
			set
			{
				_yearText = value;
				RaisePropertyChanged("YearText");
			}
		}

		public String TouchToEnterDayText
		{
			get
			{
				return _touchToEnterDayText;
			}
			set
			{
				_touchToEnterDayText = value;
				RaisePropertyChanged("TouchToEnterDayText");
			}
		}

		public String TouchToEnterMonthText
		{
			get
			{
				return _touchToEnterMonthText;
			}
			set
			{
				_touchToEnterMonthText = value;
				RaisePropertyChanged("TouchToEnterMonthText");
			}
		}

		public String TouchHereToEnterText
		{
			get
			{
				return _touchHereToEnterText;
			}
			set
			{
				_touchHereToEnterText = value;
				RaisePropertyChanged("TouchHereToEnterText");
			}
		}

		public String TouchToEnterYearText
		{
			get
			{
				return _touchToEnterYearText;
			}
			set
			{
				_touchToEnterYearText = value;
				RaisePropertyChanged("TouchToEnterYearText");
			}
		}


		private void InitialiseGlobalVariables()
		{
			GlobalVariables.IsMuliplePatientCheckDone = false;
			GlobalVariables.PatientMatchPinCode = null;
			IsContinueVisible = null;
			IsErrorTextVisible = null;

			if (GlobalVariables.IsArrive)
			{
				FindingYourAppointmentDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.FindingYourAppointmentDetailsArrivalText];
				EnterDateOfBirthText = GlobalVariables.SelectedLanguageIdText[LanguageText.EnterDateOfBirthArrivalText];
			}
			else
			{
				FindingYourAppointmentDetailsText = GlobalVariables.SelectedLanguageIdText[LanguageText.FindingYourAppointmentDetailsBookingText];
				EnterDateOfBirthText = GlobalVariables.SelectedLanguageIdText[LanguageText.EnterDateOfBirthBookingText];
			}

			DayText = GlobalVariables.SelectedLanguageIdText[LanguageText.DayText];
			MonthText = GlobalVariables.SelectedLanguageIdText[LanguageText.MonthText];
			YearText = GlobalVariables.SelectedLanguageIdText[LanguageText.YearText];
			TouchToEnterDayText = GlobalVariables.SelectedLanguageIdText[LanguageText.TouchToEnterDayText];
			TouchToEnterMonthText = GlobalVariables.SelectedLanguageIdText[LanguageText.TouchToEnterMonthText];
			TouchToEnterYearText = GlobalVariables.SelectedLanguageIdText[LanguageText.TouchToEnterYearText];
			TouchHereToEnterText = GlobalVariables.SelectedLanguageIdText[LanguageText.TouchHereToEnterText];
			ContinueText = GlobalVariables.SelectedLanguageIdText[LanguageText.ContinueText];
			ErrorText = GlobalVariables.SelectedLanguageIdText[LanguageText.EnterValidDateOfBirthText];
		}

		private void DateOfBirthDetailsSelected(DateOfBirthDetailsMessage message)
		{
			switch (message.DateOfBirthType)
			{
				case DateOfBirthType.Day:
					CanSelectDayOfBirth = false;
					DayTextBlockValue = message.DateOfBirthValue;
					DayDisplayText = message.DisplayText;
					break;
				case DateOfBirthType.Month:
					CanSelectMonthOfBirth = false;
					MonthTextBlockValue = message.DateOfBirthValue;
					MonthDisplayText = message.DisplayText;
					break;
				case DateOfBirthType.YearOfBirth:
					CanSelectYearOfBirth = false;
					YearTextBlockValue = message.DateOfBirthValue;
					break;
				case DateOfBirthType.DataOfBirthYear:
					CanSelectDateOfBirthYear = false;
					DateofBirthYearText = message.DateOfBirthValue;
					break;
				default:
					throw new InvalidEnumArgumentException(@"Invalid value for DateOfBirthType");
			}

			IsContinueVisible = IsValid();
		}

		public RelayCommand<bool> ContinueCommand
		{
			get
			{
				return _continueCommand
					  ?? (_continueCommand = new RelayCommand<bool>(
						  p =>
						  {
							  ForwardNavigation();
						  }));
			}
		}

		private void ForwardNavigation()
		{

			UnRegisterDobMessage();
			Messenger.Default.Send(
				Task.Factory.StartNew(Utilities.MatchPatient).ContinueWith(
					t =>
					{
						Dispatcher.CurrentDispatcher.BeginInvoke(new Action(Utilities.PatientMatchForwardNavigation));
					},
					TaskScheduler.FromCurrentSynchronizationContext())
				);

		}


		private void UnRegisterDobMessage()
		{
			Messenger.Default.Unregister<DateOfBirthDetailsMessage>(this, DateOfBirthDetailsSelected);
		}

		private void AllowDayOfBirthSelection()
		{
			CanSelectDayOfBirth = true;

			CanSelectMonthOfBirth = false;
			CanSelectYearOfBirth = false;
			CanSelectDateOfBirthYear = false;
		}

		private void AllowMonthOfBirthSelection()
		{
			_selectMonth.SetControlText();
			CanSelectMonthOfBirth = true;

			CanSelectDayOfBirth = false;
			CanSelectYearOfBirth = false;
			CanSelectDateOfBirthYear = false;
		}

		private void AllowYearOfBirthSelection()
		{
			GlobalVariables.Year = "0";
			YearTextBlockValue = 0;
			_yearViewModel.GetYearList();
			CanSelectYearOfBirth = true;

			CanSelectDayOfBirth = false;
			CanSelectMonthOfBirth = false;
			CanSelectDateOfBirthYear = false;
		}

		private void AllowDateOfBirthYearSelection()
		{
			CanSelectDateOfBirthYear = true;

			CanSelectMonthOfBirth = false;
			CanSelectYearOfBirth = false;
			CanSelectDayOfBirth = false;
		}

		public bool CanSelectDateOfBirthYear
		{
			get { return _canSelectDateOfBirthYear; }
			private set
			{
				_canSelectDateOfBirthYear = value;
				RaisePropertyChanged("CanSelectDateOfBirthYear");
			}
		}

		public static string AddOrdinal(int num)
		{
			if (num <= 0) return num.ToString(System.Globalization.CultureInfo.InvariantCulture);

			switch (num % 100)
			{
				case 11:
				case 12:
				case 13:
					return "th";
			}

			switch (num % 10)
			{
				case 1:
					return "st";
				case 2:
					return "nd";
				case 3:
					return "rd";
				default:
					return "th";
			}

		}

		private List<CustomiseUserDisplayText> _monthList = new List<CustomiseUserDisplayText>()
		    {
			    new CustomiseUserDisplayText
			    {
				    Value = "01",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Jan]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "02",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Feb]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "03",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Mar]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "04",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Apr]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "05",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.May]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "06",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Jun]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "07",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.July]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "08",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Aug]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "09",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Sep]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "10",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Oct]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "11",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Nov]
			    },
			    new CustomiseUserDisplayText
			    {
				    Value = "12",
				    DisplayText = GlobalVariables.SelectedLanguageIdText[LanguageText.Dec]
			    },
		    };
	}
}