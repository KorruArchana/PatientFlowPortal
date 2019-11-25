using System.Linq;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class ArrivedAppointmentErrorViewModel : ViewModelBase
	{
		private string _hiText;
		private string _patientNameText;
		private string _notYouText;
		private string _close;
		private AppointmentDetail _appointmentDetail;
		private RelayCommand<object> _notYouCommand;
		private RelayCommand<object> _closeCommand;

		public string HiText
		{
			get
			{
				return _hiText;
			}
			set
			{
				_hiText = value;
				RaisePropertyChanged("HiText");
			}
		}

		public string PatientNameText
		{
			get
			{
				return _patientNameText;
			}
			set
			{
				_patientNameText = value;
				RaisePropertyChanged("PatientNameText");
			}
		}

		public string NotYouText
		{
			get
			{
				return _notYouText;
			}
			set
			{
				_notYouText = value;
				RaisePropertyChanged("NotYouText");
			}
		}

		public string Close
		{
			get
			{
				return _close;
			}
			set
			{
				_close = value;
				RaisePropertyChanged("Close");
			}
		}

		public AppointmentDetail AppointmentDetail
		{
			get
			{
				return _appointmentDetail;
			}
			set
			{
				_appointmentDetail = value;
				RaisePropertyChanged("AppointmentDetail");
			}
		}

		public RelayCommand<object> NotYouCommand
		{
			get
			{
				return _notYouCommand
					?? (_notYouCommand = new RelayCommand<object>(
						p =>
						{   //PNF
							GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
							Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
						}));

			}
		}

		public RelayCommand<object> CloseCommand
		{
			get
			{
				return _closeCommand
					?? (_closeCommand = new RelayCommand<object>(
										  p =>
										  {
											  Messenger.Default.Send(AppPages.HomePage);
										  }));
			}
		}

		public ArrivedAppointmentErrorViewModel()
		{
			SetControlText();
		}

		private void SetControlText()
		{
			HiText = GlobalVariables.SelectedLanguageIdText[LanguageText.HiText];
			PatientNameText = GlobalVariables.ArrivedPatientName;
			NotYouText = GlobalVariables.SelectedLanguageIdText[LanguageText.NotYouText];
			Close = GlobalVariables.SelectedLanguageIdText[LanguageText.CloseText];
			AppointmentDetail = GlobalVariables.AppointmentCollection.FirstOrDefault();
			AppointmentDetail.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ArrivedAppointmentErrorMessage];
		}

	}
}
