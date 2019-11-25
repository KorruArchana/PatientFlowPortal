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
	public class SelectDobYearViewModel : ViewModelBase
	{
		private int _index;
		private string _year1Text;
		private string _year2Text;
		private string _year3Text;
		private string _year4Text;
		private string _dateOfBirthTitleText;
		private string _yearLabelText;
		private string _nextBtnText;
		private RelayCommand<string> _clickCommand;
		private RelayCommand<string> _nextCommand;
		private bool _enableScreenTap;
		private bool? _isProgressBarVisible;
		private RelayCommand<string> _loadedCommand;

		public SelectDobYearViewModel()
		{
			InitializeControls();
		}

		public string Year1Text
		{
			get { return _year1Text; }
			set
			{
				_year1Text = value;
				RaisePropertyChanged("Year1text");
			}
		}

		public string Year2Text
		{
			get { return _year2Text; }
			set
			{
				_year2Text = value;
				RaisePropertyChanged("Year2text");
			}
		}

		public string Year3Text
		{
			get { return _year3Text; }
			set
			{
				_year3Text = value;
				RaisePropertyChanged("Year3text");
			}
		}

		public string Year4Text
		{
			get { return _year4Text; }
			set
			{
				_year4Text = value;
				RaisePropertyChanged("Year4text");
			}
		}

		public string DateOfBirthTitleText
		{
			get { return _dateOfBirthTitleText; }
			set
			{
				_dateOfBirthTitleText = value;
				RaisePropertyChanged("DateOfBirthTitleText");
			}
		}

		public string YearLabelText
		{
			get { return _yearLabelText; }
			set
			{
				_yearLabelText = value;
				RaisePropertyChanged("YearLabelText");
			}
		}

		public string NextBtnText
		{
			get { return _nextBtnText; }
			set
			{
				_nextBtnText = value;
				RaisePropertyChanged("NextBtnText");
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
										  DateofBirthValidation));
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

		public bool? IsProgressBarVisible
		{
			get { return _isProgressBarVisible; }
			set
			{
				_isProgressBarVisible = value;
				RaisePropertyChanged("IsProgressBarVisible");
			}
		}

		private void InitializeControls()
		{
			IsProgressBarVisible = null;
			EnableScreenTap = true;
			Year1Text = string.Empty;
			Year2Text = string.Empty;
			Year3Text = string.Empty;
			Year4Text = string.Empty;
			DateOfBirthTitleText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectYearText];
			NextBtnText = GlobalVariables.SelectedLanguageIdText[LanguageText.NextBtnText];
		}

		private void ForwardNavigation()
		{
			string dateval = GlobalVariables.Day + "/" + GlobalVariables.PatientMatchSelectedMonth + "/" + Year1Text;
			dateval += Year2Text;
			dateval += Year3Text;
			dateval += Year4Text;


			try
			{
				if (Year1Text != string.Empty && Year2Text != string.Empty &&
					Year3Text != string.Empty && Year4Text != string.Empty && Convert.ToDateTime(dateval) <= DateTime.Today &&
					(DateTime.Today.Year - Convert.ToDateTime(dateval).Year) <= 150)
				{
					IsProgressBarVisible = true;
					EnableScreenTap = false;
					Task.Factory.StartNew(() =>
					{
						DateTime datetime = DateTime.ParseExact(dateval, "dd/MM/yyyy", CultureInfo.InvariantCulture);
						GlobalVariables.PatientMatchDob = datetime.Date;
						GlobalVariables.PatientMatchDobFilter = dateval;
					}).ContinueWith(
						t =>
						{
							IsProgressBarVisible = null;
							EnableScreenTap = true;
							Dispatcher.CurrentDispatcher.BeginInvoke(new Action(Utilities.PatientMatchForwardNavigation));
						},
						TaskScheduler.FromCurrentSynchronizationContext());
				}
				else
				{
					GlobalVariables.ErrorCode = ErrorCodes.MultiplePatientsFound;
					Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}

		}

		private void DateofBirthValidation(string selectedValue)
		{
			if (!selectedValue.Equals("X"))
			{
				switch (_index)
				{
					case 0:
						if (selectedValue == "1" || selectedValue == "2")
						{
							Year1Text = selectedValue;
							_index++;
						}

						break;
					case 1:
						Year2Text = selectedValue;
						_index++;
						break;
					case 2:
						Year3Text = selectedValue;
						_index++;
						break;
					case 3:
						Year4Text = selectedValue;
						_index++;
						break;
					case 4:
						Year4Text = selectedValue;
						break;
				}
			}
			else if (selectedValue.Equals("X"))
			{
				switch (_index)
				{
					case 1:
						Year1Text = string.Empty;
						break;
					case 2:
						Year2Text = string.Empty;
						break;
					case 3:
						Year3Text = string.Empty;
						break;
					case 4:
						Year4Text = string.Empty;
						break;
				}

				if (_index != 0)
				{ _index--; }
			}
		}
	}

}
