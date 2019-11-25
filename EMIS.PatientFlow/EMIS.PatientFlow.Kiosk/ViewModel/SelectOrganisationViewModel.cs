using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class SelectOrganisationViewModel : ViewModelBase
    {
		private readonly IConfigurationRepository _repository;
        private List<Organisation> _organisationList;
        private RelayCommand<string> _setOrganisationCommand;
        private RelayCommand<string> _loadedCommand;
        private string _organisationWelcomeText;
        private string _selectOrganisationText;
		private BitmapImage _logoImageSource;
        private GridLength _gridImageRowDefinition;
        private bool? _isTitleVisible;
        private bool? _isLogoVisible;

        public string OrganisationWelcomeText
        {
            get
            {
                return _organisationWelcomeText;
            }
            set
            {
                _organisationWelcomeText = value;
                RaisePropertyChanged("OrganisationWelcomeText");
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

        public string SelectOrganisationText
        {
            get
            {
                return _selectOrganisationText;
            }
            set
            {
                _selectOrganisationText = value;
                RaisePropertyChanged("SelectOrganisationText");
            }
        }

        public List<Organisation> OrganisationList
        {
            get { return _organisationList; }
            set
            {
                _organisationList = value;
                RaisePropertyChanged("OrganisationList");
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

        public RelayCommand<string> SetOrganisationCommand
        {
            get
            {
                return _setOrganisationCommand
                    ?? (_setOrganisationCommand = new RelayCommand<string>(
                                          p =>
                                          {
                                              GlobalVariables.SelectedOrganisation = OrganisationList.FirstOrDefault(orgList => orgList.OrganisationId == p);
                                              Messenger.Default.Send(AppPages.SelectModule);
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
        

        public SelectOrganisationViewModel()
        {
			_repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
            InitializeControls();
            SetControlText();
        }
       
        private void InitializeControls()
        {
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
        }

        internal void SetControlText()
        {
            OrganisationWelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.OrganisationWelcomeText];
            SelectOrganisationText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectOrganisationText];
            OrganisationList = GlobalVariables.Organisations.OrderBy(s=> s.OrganisationName).ToList();   
        }
       
    }
}
