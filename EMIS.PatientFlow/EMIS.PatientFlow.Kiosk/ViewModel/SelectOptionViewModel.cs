using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class SelectOptionViewModel : ViewModelBase
    {
        private readonly IConfigurationRepository _repository;
        private string _welcomeText;
        private string _hospitalNameText;
        private BitmapImage _logoImageSource;
        private GridLength _gridImageRowDefinition;

        private Options _arrivalOrBookAppointmentModel;
        private Options _bookAppointmentModel;
        private Options _barCodeArrivalOrArrivalModel;
        private Options _siteMapModel;
        private Options _surveyModel;
        private Options _surveyOrSiteMapModel;

        private bool? _isArrivalVisible;
        private bool? _isSurveySiteMapVisible;
        private bool? _isSurveyOrSiteMapVisible;
        private bool? _isTitleVisible;
        private bool? _isLogoVisible;

        private RelayCommand<int> _selectOptionCommand;
        private RelayCommand<string> _loadedCommand;

        public string WelcomeText
        {
            get
            {
                return _welcomeText;
            }
            set
            {
                _welcomeText = value;
                RaisePropertyChanged("WelcomeText");
            }
        }

        public string HospitalNameText
        {
            get { return _hospitalNameText; }
            set
            {
                _hospitalNameText = value;
                RaisePropertyChanged("HospitalNameText");
            }
        }

        public BitmapImage LogoImageSource
        {
            get
            {
                return _logoImageSource;
            }
            set
            {
                _logoImageSource = value;
                RaisePropertyChanged("LogoImageSource");
            }
        }

        public GridLength GridImageRowDefinition
        {
            get { return _gridImageRowDefinition; }
            set
            {
                _gridImageRowDefinition = value;
                RaisePropertyChanged("GridImageRowDefinition");
            }
        }

        public Options ArrivalOrBookAppointmentModel
        {
            get
            {
                return _arrivalOrBookAppointmentModel;
            }
            set
            {
                _arrivalOrBookAppointmentModel = value;
                RaisePropertyChanged("ArrivalOrBookAppointmentModel");
            }
        }

        public Options BookAppointmentModel
        {
            get
            {
                return _bookAppointmentModel;
            }
            set
            {
                _bookAppointmentModel = value;
                RaisePropertyChanged("BookAppointmentModel");
            }
        }

        public Options BarCodeArrivalOrArrivalModel
        {
            get
            {
                return _barCodeArrivalOrArrivalModel;
            }
            set
            {
                _barCodeArrivalOrArrivalModel = value;
                RaisePropertyChanged("BarCodeArrivalOrArrivalModel");
            }
        }

        public Options SiteMapModel
        {
            get
            {
                return _siteMapModel;
            }
            set
            {
                _siteMapModel = value;
                RaisePropertyChanged("SiteMapModel");
            }
        }

        public Options SurveyModel
        {
            get
            {
                return _surveyModel;
            }
            set
            {
                _surveyModel = value;
                RaisePropertyChanged("SurveyModel");
            }
        }

        public Options SurveyOrSiteMapModel
        {
            get
            {
                return _surveyOrSiteMapModel;
            }
            set
            {
                _surveyOrSiteMapModel = value;
                RaisePropertyChanged("SurveyOrSiteMapModel");
            }
        }

        public bool? IsArrivalVisible
        {
            get
            {
                return _isArrivalVisible;
            }
            set
            {
                _isArrivalVisible = value;
                RaisePropertyChanged("IsArrivalVisible");
            }
        }

        public bool? IsSurveySiteMapVisible
        {
            get
            {
                return _isSurveySiteMapVisible;
            }
            set
            {
                _isSurveySiteMapVisible = value;
                RaisePropertyChanged("IsSurveySiteMapVisible");
            }
        }

        public bool? IsSurveyOrSiteMapVisible
        {
            get
            {
                return _isSurveyOrSiteMapVisible;
            }
            set
            {
                _isSurveyOrSiteMapVisible = value;
                RaisePropertyChanged("IsSurveyOrSiteMapVisible");
            }
        }

        public bool? IsTitleVisible
        {
            get
            {
                return _isTitleVisible;
            }
            set
            {
                _isTitleVisible = value;
                RaisePropertyChanged("IsTitleVisible");
            }
        }

        public bool? IsLogoVisible
        {
            get
            {
                return _isLogoVisible;
            }
            set
            {
                _isLogoVisible = value;
                RaisePropertyChanged("IsLogoVisible");
            }
        }

        public RelayCommand<int> SelectOptionCommand
        {
            get
            {
                return _selectOptionCommand
                    ?? (_selectOptionCommand = new RelayCommand<int>(
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

        public SelectOptionViewModel()
        {
            _repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
            InitializeControls();
        }

        private void InitializeControls()
        {
            IsArrivalVisible = true;
            IsSurveyOrSiteMapVisible = true;
            IsSurveySiteMapVisible = null;

            ArrivalOrBookAppointmentModel = new Options();
            BookAppointmentModel = new Options();
            BookAppointmentModel.IsButtonVisible = true;
            BarCodeArrivalOrArrivalModel = new Options();
            SiteMapModel = new Options();
            SurveyModel = new Options();
            SurveyOrSiteMapModel = new Options();

            BitmapImage image = _repository.GetLogoImage();
            if (GlobalVariables.IsKioskLogoPresent)
            {
                GridImageRowDefinition = new GridLength(1.5, GridUnitType.Star);
                IsLogoVisible = true;
                IsTitleVisible = null;
                LogoImageSource = image;
            }
            else
            {
                GridImageRowDefinition = new GridLength(0.8, GridUnitType.Star);
                IsLogoVisible = null;
                IsTitleVisible = true;
                LogoImageSource = null;
            }

            HospitalNameText = (GlobalVariables.KioskSettings != null) ? GlobalVariables.KioskSettings.Title : string.Empty;
            WelcomeText = GlobalVariables.SelectedLanguageIdText != null ? GlobalVariables.SelectedLanguageIdText[LanguageText.WelcomeToText] : "Welcome to";

            Utilities.ClearPatientMatchingAppValues();
            GlobalVariables.IsAppLaunched = true;

            GetModules();
        }

        private void GetModules()
        {
            var configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
            var moduleOptions = configRepository.GetKioskConfiguration<List<Options>>(KioskConfigType.Modules.ToString());
            if (moduleOptions != null && moduleOptions.Count > 0)
            {
                CheckForSurveyQuestions(moduleOptions);
                CheckForSiteMap(moduleOptions);

                string systemType = GlobalVariables.SelectedOrganisation.SystemType;

                foreach (var option in moduleOptions)
                {
                    GetText(option);
                }

                if (moduleOptions.Exists(m => m.Id == 4))
                {
                    if (moduleOptions.Exists(m => m.Id == 1))
                    {

                        ArrivalOrBookAppointmentModel = moduleOptions.FirstOrDefault(m => m.Id == 1);
                        BookAppointmentModel = moduleOptions.FirstOrDefault(m => m.Id == 4);
                        BookAppointmentModel.IsButtonVisible = true;
                    }
                    else
                    {
                        ArrivalOrBookAppointmentModel = moduleOptions.FirstOrDefault(m => m.Id == 4);
                        BookAppointmentModel.IsButtonVisible = null;
                    }
                }
                else if (moduleOptions.Exists(m => m.Id == 1))
                {
                    ArrivalOrBookAppointmentModel = moduleOptions.FirstOrDefault(m => m.Id == 1);
                    BookAppointmentModel.IsButtonVisible = null;
                }

                if (!moduleOptions.Exists(m => m.Id == 2) && moduleOptions.Exists(m => m.Id == 3))
                {
                    SurveyOrSiteMapModel = moduleOptions.FirstOrDefault(m => m.Id == 3);
                }
                else if (moduleOptions.Exists(m => m.Id == 2) && !moduleOptions.Exists(m => m.Id == 3))
                {
                    SurveyOrSiteMapModel = moduleOptions.FirstOrDefault(m => m.Id == 2);
                }
                else if (moduleOptions.Exists(m => m.Id == 2) && moduleOptions.Exists(m => m.Id == 3))
                {
                    IsSurveyOrSiteMapVisible = null;
                    IsSurveySiteMapVisible = true;

                    SurveyModel = moduleOptions.FirstOrDefault(m => m.Id == 2);
                    SiteMapModel = moduleOptions.FirstOrDefault(m => m.Id == 3);
                }
                else if (!moduleOptions.Exists(m => m.Id == 2) && !moduleOptions.Exists(m => m.Id == 3))
                {
                    IsSurveyOrSiteMapVisible = null;
                    IsSurveySiteMapVisible = null;
                }
            }
        }

        private static void CheckForSurveyQuestions(List<Options> moduleOptions)
        {
            if (!moduleOptions.Exists(a => a.Id == 2)) return;
            var questionRepository = DiResolver.CurrentInstance.Reslove<IQuestionnaireRepository>();
            var question = questionRepository.GetQuestionnairesByType(true);
            if (question == null || question.Count <= 0)
                moduleOptions.Remove(moduleOptions.Find(a => a.Id == 2));
        }

        private void CheckForSiteMap(List<Options> moduleOptions)
        {
            if (!moduleOptions.Exists(a => a.Id == 3)) return;
            var repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
            List<SiteMapImage> KioskSiteMaps = repository.GetSiteMapImage();

            if (KioskSiteMaps == null || KioskSiteMaps.Count == 0)
                moduleOptions.Remove(moduleOptions.Find(a => a.Id == 3));
            else
                GlobalVariables.KioskSiteMapsList = KioskSiteMaps;
        }

        private void GetText(Options moduleOption)
        {
            string systemType = GlobalVariables.SelectedOrganisation.SystemType;

            switch (moduleOption.Id)
            {
                case 1:
                    moduleOption.DisplayOrder = 2;
                    if (systemType != null && (systemType == SystemType.None.ToString() || systemType.ToUpper() == SystemType.Topas.GetDisplayName()))
                    {
                        moduleOption.ModuleNameToDisplay = GlobalVariables.SelectedLanguageIdText[LanguageText.ArrivalTopasText];
                    }
                    else
                    {
                        moduleOption.ModuleNameToDisplay = GlobalVariables.SelectedLanguageIdText[LanguageText.ArrivalText];
                    }
                    break;
                case 2:
                    moduleOption.ModuleNameToDisplay = GlobalVariables.SelectedLanguageIdText[LanguageText.SurveyText];
                    break;
                case 3:
                    moduleOption.ModuleNameToDisplay = GlobalVariables.SelectedLanguageIdText[LanguageText.SiteMapText];
                    break;
                case 4:
                    moduleOption.ModuleNameToDisplay = GlobalVariables.SelectedLanguageIdText[LanguageText.BookAppointmentText];
                    break;
                case 5:
                    moduleOption.ModuleNameToDisplay = GlobalVariables.SelectedLanguageIdText[LanguageText.ArrivalBarCodeTopasText];
                    break;
                default:
                    moduleOption.ModuleNameToDisplay = moduleOption.ModuleName;
                    break;
            }
        }

        private void ForwardNavigation(int selectedModuleId)
        {
            GlobalVariables.IsArrive = false;
            try
            {
                switch (selectedModuleId)
                {
                    case 1:
                        GlobalVariables.IsArrive = true;
                        GlobalVariables.IsBarCodeArrival = false;
                        GlobalVariables.IsAnonymous = false;
                        GlobalVariables.IsBarCodeArrivalDone = false;
                        GlobalVariables.SelectedSlotId = 0;
                        Utilities.SetPatientMatchFirstPage(KioskConfigType.PatientMatchArrival.ToString());
                        break;

                    case 2:
                        GlobalVariables.IsAnonymous = true;
                        Messenger.Default.Send(AppPages.Surveys);
                        break;

                    case 3:
                        Messenger.Default.Send(AppPages.SiteMap);
                        break;

                    case 4:
                        GlobalVariables.IsArrive = false;
                        GlobalVariables.IsBarCodeArrival = false;
                        GlobalVariables.IsBarCodeArrivalDone = false;
                        GlobalVariables.SelectedSlotId = 0;
                        Utilities.SetPatientMatchFirstPage(KioskConfigType.PatientMatchBooking.ToString());
                        break;

                    case 5:
                        GlobalVariables.IsArrive = true;
                        GlobalVariables.IsBarCodeArrival = true;
                        GlobalVariables.IsAnonymous = false;
                        GlobalVariables.IsBarCodeArrivalDone = false;
                        GlobalVariables.SelectedSlotId = 0;
                        Messenger.Default.Send(AppPages.ArrivalByBarcode);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
                Messenger.Default.Send(AppPages.ExceptionDivert);
            }
        }
    }
}