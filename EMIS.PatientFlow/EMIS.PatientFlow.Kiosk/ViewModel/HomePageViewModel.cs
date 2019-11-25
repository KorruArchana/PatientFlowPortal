using System.Collections.Generic;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class HomePageViewModel : ViewModelBase
	{
		private RelayCommand<string> _loadedCommand;

		private IConfigurationRepository _configRepository;

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

											  if (GlobalVariables.Organisations != null)
											  {
												  if (GlobalVariables.Organisations.Count == 1)
												  {
													  GlobalVariables.SelectedOrganisation = GlobalVariables.Organisations[0];
													  Messenger.Default.Send(AppPages.SelectModule);
												  }
												  else if (GlobalVariables.Organisations.Count > 1)
												  {
													  Messenger.Default.Send(AppPages.Organisation);
												  }
											  }
											  else
											  {
												  Messenger.Default.Send(AppPages.ExceptionDivert);
											  }
										  }));
			}
		}

		public HomePageViewModel()
		{
			InitialiseControls();
		}

		private void InitialiseControls()
		{
			if (GlobalVariables.IsDataAvailable)
			{
				_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
				GlobalVariables.KioskSettings = _configRepository.GetKioskConfiguration<KioskSettings>(KioskConfigType.KioskSettings.ToString());
				GlobalVariables.ArrivedPatientDetails = null;
                GlobalVariables.TimeOutValue =
					(GlobalVariables.KioskSettings != null && GlobalVariables.KioskSettings.ScreenTimeOut >= 5)
					? GlobalVariables.KioskSettings.ScreenTimeOut
					: 30;

				GlobalVariables.Organisations = _configRepository.GetKioskConfiguration<List<Organisation>>(KioskConfigType.Organisation.ToString());
			}
		}
	}
}