using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class DemographicGpMessageViewModel : ViewModelBase
	{
		private string _userText;
		private string _upToDateInfoText;
		private string _continueCheckinButtonText;
		private RelayCommand<AppPages> _nextCommand;
		
		public string UserText
		{
			get
			{
				return _userText;
			}
			set
			{
				_userText = value;
				RaisePropertyChanged("UserText");
			}
		}

		public string UpToDateInfoText
		{
			get
			{
				return _upToDateInfoText;
			}
			set
			{
				_upToDateInfoText = value;
				RaisePropertyChanged("UpToDateInfoText");
			}
		}

		public string ContinueCheckinButtonText
		{
			get
			{
				return _continueCheckinButtonText;
			}
			set
			{
				_continueCheckinButtonText = value;
				RaisePropertyChanged("ContinueCheckinButtonText");
			}
		}
		
		public RelayCommand<AppPages> NextCommand
		{
			get
			{
				return _nextCommand
					   ?? (_nextCommand = new RelayCommand<AppPages>(
						   p =>
						   {
							   Messenger.Default.Send(AppPages.FinishRouting);
						   }));
			}
		}

		public DemographicGpMessageViewModel()
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			SetControlText();
		}

		internal void SetControlText()
		{
			UserText = GlobalVariables.SelectedLanguageIdText[LanguageText.ContactReceptionwithCorrectDetails];
			UpToDateInfoText = GlobalVariables.SelectedLanguageIdText[LanguageText.UpToDateInfoText];
			ContinueCheckinButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.ContinueCheckinButtonText];
		}
	}
}