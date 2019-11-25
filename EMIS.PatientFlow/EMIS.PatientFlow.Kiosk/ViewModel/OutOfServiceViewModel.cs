using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class OutOfServiceViewModel : ViewModelBase
    {
        private string _welcomeText;
        private string _hospitalNameText;
        private string _outOfServiceText;

		private string _egtonStatusMessageText;
		private string _kioskOfflineMessage;
		private static Message kioskGeneralMessage;

		public string WelcomeText
        {
            get { return _welcomeText; }
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
		
		public string EgtonStatusMessageText
		{
			get { return _egtonStatusMessageText; }
			set
			{
				_egtonStatusMessageText = value;
				RaisePropertyChanged("EgtonStatusMessageText");
			}
		}

		public string OutOfServiceText
        {
            get { return _outOfServiceText; }
            set
            {
                _outOfServiceText = value;
                RaisePropertyChanged("OutOfServiceText");
            }
        }

		public string KioskOfflineMessage
		{
			get
			{
				return _kioskOfflineMessage;
			}
			set
			{
				_kioskOfflineMessage = value;
				RaisePropertyChanged("KioskOfflineMessage");
			}
		}

		public OutOfServiceViewModel()
        {
            InitializeControls();
        }


        private void InitializeControls()
        {
            var configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
            if (GlobalVariables.IsUsedKey)
            {
                OutOfServiceText = Constants.KioskClosed;
            }
            else if (GlobalVariables.NetworkUnavailable)
            {
                OutOfServiceText = Constants.KioskNetworkUnavailable;
            }
            else
            {
	            GlobalVariables.KioskSettings = configRepository.GetKioskConfiguration<KioskSettings>(KioskConfigType.KioskSettings.ToString());
				kioskGeneralMessage = configRepository.GetKioskConfiguration<Message>(KioskConfigType.Message.ToString());
				HospitalNameText = (GlobalVariables.KioskSettings != null) ? GlobalVariables.KioskSettings.Title : string.Empty;
				if(string.IsNullOrEmpty(HospitalNameText))
				{
					WelcomeText = "Welcome";
				}
				else
				{
					WelcomeText = "Welcome to";
				}

				switch (GlobalVariables.KioskStatus)
                {
                    case Constants.StatusOnline:
                        break;
                    case Constants.StatusClosed:
                        OutOfServiceText = Constants.KioskClosed;
                        break;
                    case Constants.StatusOutOfService:
                        OutOfServiceText = Constants.KioskOutOfService;
						if(kioskGeneralMessage == null)
						{
							KioskOfflineMessage = Constants.KioskOfflineMessage;
						}
						else
						{
							KioskOfflineMessage = string.IsNullOrWhiteSpace(kioskGeneralMessage.GeneralMessage)
								? Constants.KioskOfflineMessage
								: kioskGeneralMessage.GeneralMessage;
						}
						EgtonStatusMessageText = Constants.EgtonStatusMessage;
                        break;
                }
            }
        }
    }
}