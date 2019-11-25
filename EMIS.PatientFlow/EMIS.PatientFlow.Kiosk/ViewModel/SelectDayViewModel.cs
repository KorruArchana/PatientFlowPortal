using System;
using System.Collections.Generic;
using System.Globalization;
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
	public class SelectDayViewModel : ViewModelBase
	{
		private string _selectDayText;
		private string _dayWelcomeText;
		private bool _enableScreenTap;
		private bool? _isProgressBarVisible;
		private List<CustomiseUserDisplayText> _daysModel;
		private RelayCommand<string> _daySelectionCommand;
		private RelayCommand<string> _loadedCommand;

		public string SelectDayText
		{
			get { return _selectDayText; }
			set
			{
				_selectDayText = value;
				RaisePropertyChanged("SelectDayText");
			}
		}

		public string DayWelcomeText
		{
			get { return _dayWelcomeText; }
			set
			{
				_dayWelcomeText = value;
				RaisePropertyChanged("DayWelcomeText");
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

		public List<CustomiseUserDisplayText> DaysModel
		{
			get { return _daysModel; }
			set
			{
				_daysModel = value;
				RaisePropertyChanged("DaysModel");
			}
		}

		public RelayCommand<string> DaySelectionCommand
		{
			get
			{
				return _daySelectionCommand
					?? (_daySelectionCommand = new RelayCommand<string>(
												ForwardNavigation));
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

		public SelectDayViewModel()
		{
			InitializeControls();
			SetControlText();
		}

		public void SetControlText()
		{
			DaysModel = ViewModelHelper.GetDaysinText();
		}

		/// <summary>
		/// Method to initialize controls of UI.
		/// </summary>
		private void InitializeControls()
		{
			IsProgressBarVisible = null;
			EnableScreenTap = true;
			SelectDayText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectDayText];
            DayWelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectionWelcomeText];
		}

		private void ForwardNavigation(string selectedDay)
		{
			try
			{
				GlobalVariables.Day = selectedDay.ToString(CultureInfo.InvariantCulture);
				IsProgressBarVisible = true;
				EnableScreenTap = false;
				Task.Factory.StartNew(Utilities.MatchPatient).ContinueWith(
					t =>
					{
						IsProgressBarVisible = null;
						EnableScreenTap = true;
						Dispatcher.CurrentDispatcher.BeginInvoke(new Action(Utilities.PatientMatchForwardNavigation));
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