using System;
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
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// </para>
    /// </summary>
    public class SelectGenderViewModel : ViewModelBase
    {
		private string _genderMaleText;
        private string _genderFemaleText;
        private string _genderOtherText;
        private string _skipBtnText;
        private string _genderMaleParameter;
        private string _genderFemaleParameter;
        private string _genderOtherParameter;
        private bool? _isProgressBarVisible;
        private bool _enableScreenTap;
        private string _selectGenderText;
        private string _genderWelcomeText;
        private RelayCommand<string> _selectGenderCommand;
        private RelayCommand<string> _loadedCommand;

        public SelectGenderViewModel()
        {
			IsProgressBarVisible = null;
            EnableScreenTap = true;
            GenderMaleParameter = Constants.GenderMale;
            GenderFemaleParameter = Constants.GenderFemale;
            GenderOtherParameter = Constants.GenderOther;

            GenderWelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectionWelcomeText];
            SelectGenderText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectGenderText];
            GenderMaleText = GlobalVariables.SelectedLanguageIdText[LanguageText.Male];
			GenderFemaleText = GlobalVariables.SelectedLanguageIdText[LanguageText.Female];
            GenderOtherText = GlobalVariables.SelectedLanguageIdText[LanguageText.Other];
            
			SkipBtnText = GlobalVariables.SelectedLanguageIdText[LanguageText.IdRatherNotSayText];
        }

        public string GenderMaleText
        {
            get { return _genderMaleText; }
            set
            {
                _genderMaleText = value;
                RaisePropertyChanged("GenderMaleText");
            }
        }

        public string GenderFemaleText
        {
            get { return _genderFemaleText; }
            set
            {
                _genderFemaleText = value;
                RaisePropertyChanged("GenderFemaleText");
            }
        }

        public string GenderOtherText
        {
            get
            {
                return _genderOtherText;
            }
            set
            {
                _genderOtherText = value;
                RaisePropertyChanged("GenderOtherText");
            }
        }

        public string SkipBtnText
        {
            get { return _skipBtnText; }
            set
            {
                _skipBtnText = value;
                RaisePropertyChanged("SkipBtnText");
            }
        }

        public string GenderMaleParameter
        {
            get { return _genderMaleParameter; }
            set
            {
                _genderMaleParameter = value;
                RaisePropertyChanged("GenderMaleParameter");
            }
        }

        public string GenderFemaleParameter
        {
            get { return _genderFemaleParameter; }
            set
            {
                _genderFemaleParameter = value;
                RaisePropertyChanged("GenderFemaleParameter");
            }
        }

        public string GenderOtherParameter
        {
            get
            {
                return _genderOtherParameter;
            }
            set
            {
                _genderOtherParameter = value;
                RaisePropertyChanged("GenderOtherParameter");
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

        public bool EnableScreenTap
        {
            get { return _enableScreenTap; }
            set
            {
                _enableScreenTap = value;
                RaisePropertyChanged("EnableScreenTap");
            }
        }

        public string SelectGenderText
        {
            get
            {
                return _selectGenderText;
            }
            set
            {
                _selectGenderText = value;
                RaisePropertyChanged("SelectGenderText");
            }
        }

        public string GenderWelcomeText
        {
            get
            {
                return _genderWelcomeText;
            }
            set
            {
                _genderWelcomeText = value;
                RaisePropertyChanged("GenderWelcomeText");
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

        public RelayCommand<string> SelectGenderCommand
        {
            get
            {
                return _selectGenderCommand
                    ?? (_selectGenderCommand = new RelayCommand<string>(
                                          ForwardNavigation));
            }
        }

        private void ForwardNavigation(string selectedValue)
        {
			IsProgressBarVisible = true;
            EnableScreenTap = false;
            Task.Factory.StartNew(() =>
            {
                GlobalVariables.PatientMatchGender = selectedValue;
                Utilities.MatchPatient();
            }).ContinueWith(
                t =>
                {
                    EnableScreenTap = true;
                    Dispatcher.CurrentDispatcher.BeginInvoke(new Action(Utilities.PatientMatchForwardNavigation));
                    IsProgressBarVisible = null;
                },
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}