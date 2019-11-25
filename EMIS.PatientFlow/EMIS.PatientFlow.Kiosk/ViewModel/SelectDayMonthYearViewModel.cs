using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class SelectDayMonthYearViewModel : ViewModelBase
    {
        private int index = 0;
        private string _selectDOBYearWelcomeText;
        private string _dateOfBirthTitleText;
        private string _exampleDOBText;
        private string _nextBtnText;
        private string _day1text;
        private string _day2text;
        private string _month1text;
        private string _month2text;
        private string _year1text;
        private string _year2text;
        private string _year3text;
        private string _year4text;
		private string _dateTextHighlightBorderColor;
		private string _monthTextHighlightBorderColor;
		private string _yearTextHighlightBorderColor;
        private string _dayForeGround;
        private string _monthForeGround;
        private string _yearForeGround;
        private bool _enableScreenTap;
        private bool? _progressBarVisibility;
        private bool _isNextEnabled;
        private bool? _isErrorTextVisible;
        private bool _isDeleteEnabled;
        private string _invalidDate;

        private RelayCommand<string> _loadedCommand;
        private RelayCommand<string> _clickCommand;
        private RelayCommand<string> _nextCommand;

        private const string DarkGreen = "#087A6F";
		private const string Gray = "Gray";
		private const string Transparent = "Transparent";

        public string SelectDOBYearWelcomeText
        {
            get
            {
                return _selectDOBYearWelcomeText;
            }
            set
            {
                _selectDOBYearWelcomeText = value;
                RaisePropertyChanged("SelectDOBYearWelcomeText");
            }
        }

        public string DateOfBirthTitleText
        {
            get
            {
                return _dateOfBirthTitleText;
            }
            set
            {
                _dateOfBirthTitleText = value;
                RaisePropertyChanged("DateOfBirthTitleText");
            }
        }

        public string ExampleDOBText
        {
            get
            {
                return _exampleDOBText;
            }
            set
            {
                _exampleDOBText = value;
                RaisePropertyChanged("ExampleDOBText");
            }
        }

        public string NextBtnText
        {
            get
            {
                return _nextBtnText;
            }
            set
            {
                _nextBtnText = value; RaisePropertyChanged("NextBtnText");
            }
        }

        public string Day1text
        {
            get { return _day1text; }
            set { _day1text = value; RaisePropertyChanged("Day1text"); }
        }

        public string Day2text
        {
            get { return _day2text; }
            set { _day2text = value; RaisePropertyChanged("Day2text"); }
        }

        public string Month1text
        {
            get { return _month1text; }
            set { _month1text = value; RaisePropertyChanged("Month1text"); }
        }

        public string Month2text
        {
            get { return _month2text; }
            set { _month2text = value; RaisePropertyChanged("Month2text"); }
        }

        public string Year1text
        {
            get { return _year1text; }
            set { _year1text = value; RaisePropertyChanged("Year1text"); }
        }

        public string Year2text
        {
            get { return _year2text; }
            set { _year2text = value; RaisePropertyChanged("Year2text"); }
        }

        public string Year3text
        {
            get { return _year3text; }
            set { _year3text = value; RaisePropertyChanged("Year3text"); }
        }

        public string Year4text
        {
            get { return _year4text; }
            set { _year4text = value; RaisePropertyChanged("Year4text"); }
        }

		public string DateTextHighlightBorderColor
		{
			get
			{
				return _dateTextHighlightBorderColor;
			}
			set
			{
				_dateTextHighlightBorderColor = value;
				RaisePropertyChanged("DateTextHighlightBorderColor");
			}
		}

		public string MonthTextHighlightBorderColor
		{
			get
			{
				return _monthTextHighlightBorderColor;
			}
			set
			{
				_monthTextHighlightBorderColor = value;
				RaisePropertyChanged("MonthTextHighlightBorderColor");
			}
		}

		public string YearTextHighlightBorderColor
		{
			get
			{
				return _yearTextHighlightBorderColor;
			}
			set
			{
				_yearTextHighlightBorderColor = value;
				RaisePropertyChanged("YearTextHighlightBorderColor");
			}
		}

        public string DayForeGround
        {
            get
            {
                return _dayForeGround;
            }
            set
            {
                _dayForeGround = value;
                RaisePropertyChanged("DayForeGround");
            }
        }

        public string MonthForeGround
        {
            get
            {
                return _monthForeGround;
            }
            set
            {
                _monthForeGround = value;
                RaisePropertyChanged("MonthForeGround");
            }
        }

        public string YearForeGround
        {
            get
            {
                return _yearForeGround;
            }
            set
            {
                _yearForeGround = value;
                RaisePropertyChanged("YearForeGround");
            }
        }

        public bool EnableScreenTap
        {
            get { return _enableScreenTap; }
            set { _enableScreenTap = value; RaisePropertyChanged("EnableScreenTap"); }
        }

        public bool? ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { _progressBarVisibility = value; RaisePropertyChanged("ProgressBarVisibility"); }
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set
            {
                _isNextEnabled = value;
                RaisePropertyChanged("IsNextEnabled");
            }
        }

        public bool? IsErrorTextVisible
        {
            get { return _isErrorTextVisible; }
            set
            {
                _isErrorTextVisible = value;
                RaisePropertyChanged("IsErrorTextVisible");
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

        public string InvalidDate
        {
            get { return _invalidDate; }
            set
            {
                _invalidDate = value;
                RaisePropertyChanged("InvalidDate");
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

        public RelayCommand<string> ClickCommand
        {
            get
            {
                return _clickCommand
                    ?? (_clickCommand = new RelayCommand<string>(
                                          p =>
                                          {
                                              DateofBirthValidation(p);
                                          }));
            }
        }

        public RelayCommand<string> NextCommand
        {

            get
            {
                return _nextCommand
                    ?? (_nextCommand = new RelayCommand<string>(
                        p =>
                        {
                            ForwardNavigation();
                        }));
                ;
            }
        }

        public SelectDayMonthYearViewModel()
        {
            InitializeControls();
			SetControlText();
		}

		private void InitializeControls()
        {
            ProgressBarVisibility = null;
            EnableScreenTap = true;
            ILanguageRepository languageRepository = DiResolver.CurrentInstance.Reslove<ILanguageRepository>();
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            if (GlobalVariables.PatientMatchDobFilter == null)
            {
				DateTextHighlightBorderColor = DarkGreen;
				MonthTextHighlightBorderColor = YearTextHighlightBorderColor = Transparent;

                DayForeGround = MonthForeGround = YearForeGround = Gray;

                Day1text = Day2text = "D";
                Month1text = Month2text = "M";
                Year1text = Year2text = Year3text = Year4text = "Y";
                IsNextEnabled = false;
                IsDeleteEnabled = false;
            }
            else
            {
                var dob = GlobalVariables.PatientMatchDobFilter.ToCharArray();
                Day1text = dob[0].ToString();
                Day2text = dob[1].ToString();
                Month1text = dob[3].ToString();
                Month2text = dob[4].ToString();
                Year1text = dob[6].ToString();
                Year2text = dob[7].ToString();
                Year3text = dob[8].ToString();
                Year4text = dob[9].ToString();

                index = 8;

                DayForeGround = MonthForeGround = YearForeGround = DarkGreen;
                IsNextEnabled = true;
                IsDeleteEnabled = true;
            }

        }

        internal void SetControlText()
        {
            SelectDOBYearWelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectionWelcomeText];
            DateOfBirthTitleText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectDOBYearText];
            NextBtnText = GlobalVariables.SelectedLanguageIdText[LanguageText.NextBtnText];
            ExampleDOBText = Helper.Constants.ExampleDOBText;
            InvalidDate = GlobalVariables.SelectedLanguageIdText[LanguageText.EnterValidDateOfBirthText];
        }

        private void ForwardNavigation()
        {
            try
            {
                ProgressBarVisibility = true;
                EnableScreenTap = false;
                Task tskOpen = Task.Factory.StartNew(() =>
                {
                    Utilities.MatchPatient();
                }).ContinueWith(t =>
                {
                    ProgressBarVisibility = null;
                    EnableScreenTap = true;
                    Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => Utilities.PatientMatchForwardNavigation()));

                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message.ToString(), ex, this.KioskId);
                Messenger.Default.Send(AppPages.ExceptionDivert);
            }
        }

        private void DateofBirthValidation(string selectedValue)
        {
            IsNextEnabled = false;
            IsDeleteEnabled = true;
            if (!selectedValue.Equals("X"))
            {
                switch (index)
                {
                    case 0:
						if (selectedValue == "0" || selectedValue == "1" || selectedValue == "2" || selectedValue == "3")
                        {
                            Day1text = selectedValue;
                            Day2text = string.Empty;
                            DayForeGround = DarkGreen;
                            index++;
                        }
						else
						{
							IsDeleteEnabled = false;
						}
						break;
                    case 1:
                        switch (Day1text)
                        {
                            case "0":
                                if (selectedValue != "0")
                                {
                                    Day2text = selectedValue;
                                    index++;
                                }
                                break;
                            case "3":
                                if (selectedValue == "0" || selectedValue == "1")
                                {
                                    Day2text = selectedValue;
                                    index++;
                                }
                                break;

                            default:
                                Day2text = selectedValue;
                                index++;
                                break;
                        }
                        break;

                    case 2:
                        if (selectedValue == "0" || selectedValue == "1")
                        {
                            Month1text = selectedValue;
                            Month2text = string.Empty;
                            MonthForeGround = DarkGreen;
                            index++;
                        }
                        break;
                    case 3:
                        switch (Month1text)
                        {
                            case "0":
                                if (selectedValue != "0")
                                {
                                    Month2text = selectedValue;
                                    index++;
                                }
                                break;
                            case "1":
                                if (selectedValue == "0" || selectedValue == "1" || selectedValue == "2")
                                {
                                    Month2text = selectedValue;
                                    index++;
                                }
                                break;
                        }
                        break;
                    case 4:
                        if (selectedValue == "1" || selectedValue == "2")
                        {
                            Year1text = selectedValue;

                            Year2text = Year3text = Year4text = string.Empty;
                            YearForeGround = DarkGreen;

                            index++;
                        }
                        break;
                    case 5:
                        Year2text = selectedValue;
                        index++;
                        break;
                    case 6:
                        Year3text = selectedValue;
                        index++;
                        break;
                    case 7:
                        Year4text = selectedValue;
                        index++;
                        break;
                    case 8:
                        Year4text = selectedValue;
                        break;

                }
            }
            else if (selectedValue.Equals("X"))
            {

                switch (index)
                {
                    case 1:
                        Day1text = string.Empty;
                        Day1text = Day2text = "D";
                        DayForeGround = Gray;
                        IsDeleteEnabled = false;
                        break;

                    case 2:
                        Day2text = string.Empty;
                        break;

                    case 3:
                        Month1text = string.Empty;
                        Month1text = Month2text = "M";
                        MonthForeGround = Gray;
                        break;

                    case 4:
                        Month2text = string.Empty;
                        break;

                    case 5:
                        Year1text = string.Empty;
                        Year1text = Year2text = Year3text = Year4text = "Y";
                        YearForeGround = Gray;
                        break;

                    case 6:
                        Year2text = string.Empty;
                        break;

                    case 7:
                        Year3text = string.Empty;
                        break;

                    case 8:
                        Year4text = string.Empty;
                        IsErrorTextVisible = null;
                        break;

                }
                if (index != 0)
                { index--; }
            }

            switch (index)
            {
                case 0:
                case 1:
					DateTextHighlightBorderColor = DarkGreen;
					MonthTextHighlightBorderColor = YearTextHighlightBorderColor = Transparent;

                    break;
                case 2:
                case 3:
					MonthTextHighlightBorderColor = DarkGreen;
					DateTextHighlightBorderColor = YearTextHighlightBorderColor = Transparent;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
					YearTextHighlightBorderColor = DarkGreen;
					DateTextHighlightBorderColor = MonthTextHighlightBorderColor = Transparent;
                    break;
                case 8:
					DateTextHighlightBorderColor = MonthTextHighlightBorderColor = YearTextHighlightBorderColor = Transparent;

                    if (ValidDob())
                    {
                        IsNextEnabled = true;
                    }
                    break;

            }
        }

        private bool ValidDob()
        {
            if (Day1text != string.Empty && Day2text != string.Empty && Month1text != string.Empty &&
                                  Month2text != string.Empty && Year1text != string.Empty && Year2text != string.Empty &&
                                  Year3text != string.Empty && Year4text != string.Empty)
            {
                string dateval = Day1text;
                dateval += Day2text + "/" + Month1text;
                dateval += Month2text + "/" + Year1text;
                dateval += Year2text;
                dateval += Year3text;
                dateval += Year4text;
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
            IsErrorTextVisible = true;
            return false;
        }
    }
}