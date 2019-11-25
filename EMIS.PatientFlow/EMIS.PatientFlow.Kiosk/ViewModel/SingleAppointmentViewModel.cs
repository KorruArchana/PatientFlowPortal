using System;
using System.Linq;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class SingleAppointmentViewModel : ViewModelBase
	{
		private IConfigurationRepository _configRepository;
		private KioskArrival _arrival;
		private string _hiText;
		private string _patientNameText;
		private string _notYouText;
		private string _close;
		private string _withText;
		private string _checkInText;
		private string _appointmentText;
		private AppointmentCollection _appointmentCollection;
		private AppointmentDetail _appointmentDetail;

		private RelayCommand _confirmCommand;
		private RelayCommand<object> _closeCommand;
		private RelayCommand<object> _notYouCommand;

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

		public string WithText
		{
			get
			{
				return _withText;
			}

			set
			{
				_withText = value;
				RaisePropertyChanged("WithText");
			}
		}

		public string CheckInText
		{
			get
			{
				return _checkInText;
			}

			set
			{
				_checkInText = value;
				RaisePropertyChanged("CheckInText");
			}
		}
		public string AppointmentText
		{
			get
			{
				return _appointmentText;
			}

			set
			{
				_appointmentText = value;
				RaisePropertyChanged("AppointmentText");
			}
		}

		public AppointmentCollection AppointmentCollection
		{
			get
			{
				return _appointmentCollection;
			}
			set
			{
				_appointmentCollection = value;
				RaisePropertyChanged("AppointmentCollection");
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

		public RelayCommand ConfirmCommand
		{
			get
			{
				return _confirmCommand
					?? (_confirmCommand = new RelayCommand(
										  ConfirmArrival));
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

		private void ConfirmArrival()
		{
			GlobalVariables.CommandParameter = 1;
			Messenger.Default.Send(AppPages.ArrivalConfirmationAndRouting);
		}

		public SingleAppointmentViewModel()
		{
			InitializeControls();
			SetControlText();
		}

		internal void SetControlText()
		{
			HiText = GlobalVariables.SelectedLanguageIdText[LanguageText.HiText];
			PatientNameText = GlobalVariables.ArrivedPatientName;
			NotYouText = GlobalVariables.SelectedLanguageIdText[LanguageText.NotYouText];
			AppointmentCollection = GlobalVariables.AppointmentCollection;
			AppointmentCollection.ForEach(x => x.AppointmentTimeStyle = (x.Time == "Appointment" ? "Normal" : "Bold"));
			Close = GlobalVariables.SelectedLanguageIdText[LanguageText.CloseText];
			WithText = GlobalVariables.SelectedLanguageIdText[LanguageText.WithText];
			AppointmentText = GlobalVariables.SelectedLanguageIdText[LanguageText.AppointmentText];
			CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInText];

			if (AppointmentCollection.Count == 1)
			{
				AppointmentDetail = AppointmentCollection.FirstOrDefault();
			}
			if (!AppointmentDetail.IsEnabled)
			{
				SetErrorMessageText();
			}
		}

		private void InitializeControls()
		{
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			_arrival = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
		}

		private void SetErrorMessageText()
		{
			AppointmentDetail.IsErrorMessageVisible = true;

			switch (AppointmentDetail.ArrivalType)
			{
				case AppointmentArrivalStatus.WrongLocation:
					AppointmentDetail.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForWrongLocation] + " " +
													 GlobalVariables.SelectedLanguageIdText[LanguageText.LateArrivalSpeakToReceptionText];
					break;

				case AppointmentArrivalStatus.DoctorDivert:
					AppointmentDetail.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForDoctorDivert] + " " +
													 GlobalVariables.SelectedLanguageIdText[LanguageText.SpeakToReceptionInstead];
					break;

				case AppointmentArrivalStatus.EarlyArrival:
					AppointmentDetail.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForEarlyArrival] + " " +
													 GlobalVariables.SelectedLanguageIdText[LanguageText.CanArriveUptoXMinForEarlyArrival];
					AppointmentDetail.ErrorMessage = Regex.Replace(AppointmentDetail.ErrorMessage, @"##", _arrival.EarlyArrival.ToString(CultureInfo.InvariantCulture));
					break;

				case AppointmentArrivalStatus.LateArrival:
					AppointmentDetail.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForLateArrival] + " " +
													GlobalVariables.SelectedLanguageIdText[LanguageText.LateArrivalSpeakToReceptionText];
					break;

			}
		}
	}
}