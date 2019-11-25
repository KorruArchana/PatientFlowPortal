using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class SelectMonthViewModel : ViewModelBase
    {
        private string _selectMonthText;
		private string _monthWelcomeText;
		private bool _enableScreenTap;
		private bool? _isProgressBarVisible;
		private List<CustomiseUserDisplayText> _monthList;
        private RelayCommand<string> _monthSelectionCommand;
        private RelayCommand<string> _loadedCommand;

		public string SelectMonthText
		{
			get { return _selectMonthText; }
			set
			{
				_selectMonthText = value;
				RaisePropertyChanged("SelectMonthText");
			}
		}

		public string MonthWelcomeText
		{
			get
			{
				return _monthWelcomeText;
			}
			set
			{
				_monthWelcomeText = value;
				RaisePropertyChanged("MonthWelcomeText");
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

		public List<CustomiseUserDisplayText> MonthList
		{
			get { return _monthList; }
			set
			{
				_monthList = value;
				RaisePropertyChanged("MonthList");
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

		public RelayCommand<string> MonthSelectionCommand
		{
			get
			{
				return _monthSelectionCommand
					?? (_monthSelectionCommand = new RelayCommand<string>(
										  ForwardNavigation));
			}
		}

		public SelectMonthViewModel()
        {
			InitializeControls();
			SetControlText();
        }

		public void SetControlText()
	    {
			MonthList =	ViewModelHelper.GetMonthText();
	    }

        /// <summary>
        /// Method to initialize controls
        /// </summary>
        private void InitializeControls()
        {
            EnableScreenTap = true;
			IsProgressBarVisible = null;
            SelectMonthText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectMonthText];
            MonthWelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectionWelcomeText];
        }

		private void ForwardNavigation(string selectedMonth)
		{
			try
			{
				GlobalVariables.PatientMatchSelectedMonth = selectedMonth;
				IsProgressBarVisible = true;
				EnableScreenTap = false;
				Task.Factory.StartNew(Utilities.MatchPatient).ContinueWith(
					t =>
					{
						IsProgressBarVisible = null;
						EnableScreenTap = true;
						Dispatcher.CurrentDispatcher.BeginInvoke(
							new Action(Utilities.PatientMatchForwardNavigation));
					},
					TaskScheduler.FromCurrentSynchronizationContext());
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}
    }
}