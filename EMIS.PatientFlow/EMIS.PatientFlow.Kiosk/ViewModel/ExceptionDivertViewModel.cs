using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.HubClients;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class ExceptionDivertViewModel : ViewModelBase
	{
		private string _somethingWentWrongText;
		private string _unableToProcessText;
		private string _goToReceptionText;
        private string _closeText;
        private RelayCommand<string> _closeCommand;

		public ExceptionDivertViewModel()
		{
			InitializeControls();
		}

		public string SomethingWentWrongText
		{
			get { return _somethingWentWrongText; }
			set
			{
				_somethingWentWrongText = value;
				RaisePropertyChanged("SomethingWentWrongText");
			}
		}

		public string UnableToProcessText
		{
			get { return _unableToProcessText; }
			set
			{
				_unableToProcessText = value;
				RaisePropertyChanged("UnableToProcessText");
			}
		}

		public string GoToReceptionText
		{
			get { return _goToReceptionText; }
			set
			{
				_goToReceptionText = value;
				RaisePropertyChanged("GoToReceptionText");
			}
		}

        public string CloseText
        {
            get { return _closeText; }
            set
            {
                _closeText = value;
                RaisePropertyChanged("CloseText");
            }
        }

        public RelayCommand<string> CloseCommand
		{
			get
			{
                return _closeCommand
                    ?? (_closeCommand = new RelayCommand<string>(
						p =>
						{
							Messenger.Default.Send(AppPages.HomePage);
						}));
			}
		}

		private void InitializeControls()
		{
			try
			{
				Task.Factory.StartNew(() =>
				{
					if (GlobalVariables.IsKioskDeleted)
					{
						KioskHubClient.Instance.CloseHub();
					}
				});

				if (NetworkInterface.GetIsNetworkAvailable())
				{
					SomethingWentWrongText = GlobalVariables.SelectedLanguageIdText[LanguageText.SorrySomethingWrongText];

					if (!GlobalVariables.IsDbConnected)
					{
						UnableToProcessText = GlobalVariables.SelectedLanguageIdText[LanguageText.KioskClosedNoConnectionText];
					}
					else
					{
						UnableToProcessText = GlobalVariables.SelectedLanguageIdText[LanguageText.UnableProcessRequest];
					}

					GoToReceptionText = GlobalVariables.SelectedLanguageIdText[LanguageText.ReportToReception];
                    CloseText = GlobalVariables.SelectedLanguageIdText[LanguageText.CloseText];

				}
				else
				{
					InitialiseWithLocal();
				}
			}
			catch (Exception)
			{
				InitialiseWithLocal();
			}
		}

		private void InitialiseWithLocal()
		{
			SomethingWentWrongText = Constants.ErrorTitleText;
			UnableToProcessText = Constants.ErrorText;
            CloseText = Constants.ErrorCloseScreenText;
		}
	}
}