using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class SelectSurnameViewModel : ViewModelBase
    {
		private string _surnameWelcomeText;
		private string _selectSurnameText;
		private bool _enableScreenTap;
		private bool? _isProgressBarVisible;
		private List<string> _letterList;
        private RelayCommand<string> _selectSurnameCommand;
		private RelayCommand<string> _loadedCommand;

        public string SurnameWelcomeText
        {
            get
            {
                return _surnameWelcomeText;
            }
            set
            {
                _surnameWelcomeText = value;
                RaisePropertyChanged("SurnameWelcomeText");
            }
        }

        public string SelectSurnameText
        {
            get
            {
                return _selectSurnameText;
            }
            set
            {
                _selectSurnameText = value;
                RaisePropertyChanged("SelectSurnameText");
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

		public List<string> LetterList
        {
            get { return _letterList; }
            set
            {
                _letterList = value;
                RaisePropertyChanged("LetterList");
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

        public RelayCommand<string> SelectSurnameCommand
        {
            get
            {
                return _selectSurnameCommand
                    ?? (_selectSurnameCommand = new RelayCommand<string>(
                                          ForwardNavigation));
            }
        }

		public SelectSurnameViewModel()
		{
			InitializeControls();
		}

		private void InitializeControls()
        {
            EnableScreenTap = true;
			IsProgressBarVisible = null;

            SurnameWelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectionWelcomeText];
            SelectSurnameText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectSurnameText];
			LetterList = GlobalVariables.Letters;
        }

        private void ForwardNavigation(string selectedSurname)
        {
            GlobalVariables.PatientMatchSurname = selectedSurname;
            try
            {
				IsProgressBarVisible = true;
                EnableScreenTap = false;
                Task.Factory.StartNew(Utilities.MatchPatient).ContinueWith(
                    t =>
                    {
						IsProgressBarVisible = null;
						EnableScreenTap = true;
	                    if (GlobalVariables.InvalidCredentials)
	                    {
							Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(AppPages.ExceptionDivert)));
	                    }
	                    else
	                    {
							Dispatcher.CurrentDispatcher.BeginInvoke(new Action(Utilities.PatientMatchForwardNavigation));    
	                    }
                    },
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
                Messenger.Default.Send(AppPages.ExceptionDivert);
            }
        }
    }
}