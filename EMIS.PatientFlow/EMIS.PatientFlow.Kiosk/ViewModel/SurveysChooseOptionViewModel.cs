using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
    public class SurveysChooseOptionViewModel : ViewModelBase
    {
        private IQuestionnaireRepository _questionaireRepository;
        private string _chooseOptionText;
        private List<Questionnaire> _questionnaire;
        private RelayCommand<int> _selectOptionCommand;
        private RelayCommand<string> _loadedCommand;

        public string ChooseOptionText
        {
            get { return _chooseOptionText; }
            set
            {
                _chooseOptionText = value;
                RaisePropertyChanged("ChooseOptionText");
            }
        }

        public List<Questionnaire> Questionnaire
        {
            get { return _questionnaire; }
            set
            {
                _questionnaire = value;
                RaisePropertyChanged("Questionnaire");
            }
        }

        public RelayCommand<int> SelectOptionCommand
        {
            get
            {
                return _selectOptionCommand
                    ?? (_selectOptionCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              GlobalVariables.SelectedSurveyOption = p;
                                              var selectedQuestionnaire =
                                                  Questionnaire.FirstOrDefault(questionnaire => questionnaire.Id == p);
                                              if (selectedQuestionnaire != null)
                                                  GlobalVariables.SelectedSurveyTitle =
                                                      selectedQuestionnaire.Title.ToString();
                                              Messenger.Default.Send(AppPages.SurveyQuestions);
                                          }));
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
        
        public SurveysChooseOptionViewModel()
        {
            InitializeControls();
            GetSurveyOptions();
            SetControlText();
        }

        private void InitializeControls()
		{
            _questionaireRepository = DiResolver.CurrentInstance.Reslove<IQuestionnaireRepository>();
		}

		private void GetSurveyOptions()
        {
            try
            {
                Questionnaire = _questionaireRepository.GetQuestionnairesByType(true).ToList();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, message: ex.Message, exception: ex, user: KioskId);
                Messenger.Default.Send(AppPages.ExceptionDivert);
            }
        }

        internal void SetControlText()
        {
            ChooseOptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectQuestionnaire];
        }
    }
}