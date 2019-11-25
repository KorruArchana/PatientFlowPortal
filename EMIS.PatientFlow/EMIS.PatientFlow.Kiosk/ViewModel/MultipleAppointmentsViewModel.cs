using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class MultipleAppointmentsViewModel : ViewModelBase
	{
		private AppointmentCollection _appointmentCollection;
		private AppointmentDetail _appointmentDetails = new AppointmentDetail();
		private IConfigurationRepository _configRepository;
		private KioskArrival _arrival;
		private string _hiText;
		private string _patientNameText;
		private string _selectApointmentsToCheckInText;
		private string _checkInText;
		private string _notYouText;
        private string _withText;
		private int _arrivableAppointmentsCount;

		private int? _arriveSelectionCount;
		private RelayCommand<object> _checkInCommand;
		private RelayCommand<long> _selectedAppointmentCommand;
		private RelayCommand<long> _selectedAppointmentByCheckBoxCommand;
		private RelayCommand<object> _notYouCommand;

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
		public string SelectApointmentsToCheckInText
		{
			get
			{
				return _selectApointmentsToCheckInText;
			}
			set
			{
				_selectApointmentsToCheckInText = value;
				RaisePropertyChanged("SelectApointmentsToCheckInText");
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


		public int? ArriveSelectionCount
		{
			get
			{
				return _arriveSelectionCount;
			}
			set
			{
				_arriveSelectionCount = value;
				RaisePropertyChanged("ArriveSelectionCount");
			}
		}

		public RelayCommand<object> CheckInCommand
		{
			get
			{
				return _checkInCommand
					?? (_checkInCommand = new RelayCommand<object>(
										  p =>
										  {
											  if(_arrivableAppointmentsCount < 1)
											  {
												  Messenger.Default.Send(AppPages.HomePage);
											  }
											  else
											  {
												  ConfirmArrival();
											  }
										  },
										  delegate { return CheckIfSelected(); }
										  ));
			}
		}

		private bool CheckIfSelected()
		{
			return (ArriveSelectionCount > 0 || _arrivableAppointmentsCount < 1) ? true : false;
		}

		public RelayCommand<long> SelectedAppointmentCommand
		{
			get
			{
				return _selectedAppointmentCommand
					?? (_selectedAppointmentCommand = new RelayCommand<long>(
										  SelectedAppointment));
			}
		}

		public RelayCommand<long> SelectedAppointmentByCheckBoxCommand
		{
			get
			{
				return _selectedAppointmentByCheckBoxCommand
					?? (_selectedAppointmentByCheckBoxCommand = new RelayCommand<long>(
										  SelectedAppointmentByCheckBox));
			}
		}

		public RelayCommand<object> NotYouCommand
		{
			get
			{
				return _notYouCommand
					?? (_notYouCommand = new RelayCommand<object>(
						p =>
						{	//PNF
							GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
							Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
						}));
						
			}
		}

		public MultipleAppointmentsViewModel()
		{
			InitializeControls();
			SetControlText();
		}

		private void InitializeControls()
		{
			AppointmentCollection = GlobalVariables.AppointmentCollection;
			AppointmentCollection.ForEach(x => x.AppointmentTimeStyle = (x.Time == "Appointment" ? "Normal" : "SemiBold"));
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			_arrival = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
			_arrivableAppointmentsCount = AppointmentCollection.Count(c => c.IsEnabled);
		}

		internal void SetControlText()
		{
			SetInitialText();
			if (_arrivableAppointmentsCount > 0)
			{
				SetArriveButtonText();
			}
			else
			{
				CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CloseText];
			}
			SetErrorMessageText();
		}

		private void SetErrorMessageText()
		{
			foreach (AppointmentDetail appointment in AppointmentCollection.Where(x => !x.IsEnabled))
			{
				appointment.IsErrorMessageVisible = true;

				switch (appointment.ArrivalType)
				{
					case AppointmentArrivalStatus.WrongLocation:
						appointment.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForWrongLocation] + " " + 
												   GlobalVariables.SelectedLanguageIdText[LanguageText.LateArrivalSpeakToReceptionText];
						break;

					case AppointmentArrivalStatus.DoctorDivert:
						appointment.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForDoctorDivert] + " " + 
												   GlobalVariables.SelectedLanguageIdText[LanguageText.SpeakToReceptionInstead];
						break;

					case AppointmentArrivalStatus.EarlyArrival:
						appointment.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForEarlyArrival] + " " + 
												   GlobalVariables.SelectedLanguageIdText[LanguageText.CanArriveUptoXMinForEarlyArrival];
						appointment.ErrorMessage = Regex.Replace(appointment.ErrorMessage, @"##", _arrival.EarlyArrival.ToString(CultureInfo.InvariantCulture));
						break;

					case AppointmentArrivalStatus.LateArrival:
						appointment.ErrorMessage = GlobalVariables.SelectedLanguageIdText[LanguageText.ErrorMessageForLateArrival] + " " +
												   GlobalVariables.SelectedLanguageIdText[LanguageText.LateArrivalSpeakToReceptionText];
						break;

				}
			}
		}

		private void SetInitialText()
		{
			HiText = GlobalVariables.SelectedLanguageIdText[LanguageText.HiText];
			PatientNameText = GlobalVariables.ArrivedPatientName;
			SelectApointmentsToCheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectTheAppointmentsToCheckInText];
			NotYouText = GlobalVariables.SelectedLanguageIdText[LanguageText.NotYouText];
            WithText = GlobalVariables.SelectedLanguageIdText[LanguageText.WithText];

		}

		private void SelectedAppointment(long appointmentId)
		{
			if (AppointmentCollection != null && AppointmentCollection.Count > 1)
			{
				_appointmentDetails = AppointmentCollection.FirstOrDefault(m => m.AppointmentId == appointmentId);
				if (_appointmentDetails != null)
					_appointmentDetails.IsChecked = !_appointmentDetails.IsChecked;

				ArriveSelectionCount = AppointmentCollection.Count(c => c.IsChecked);
				SetArriveButtonText();
			}

		}

		private void SetArriveButtonText()
		{
			switch (ArriveSelectionCount)
			{
				case null:
				case 0:
					CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInText];
					ArriveSelectionCount = null;
					break;
				case 1:
					CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForOneAppointmentText];
					break;
                case 2:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.TwoText]);
                    break;
                case 3:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.ThreeText]);
                    break;
                case 4:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.FourText]);
                    break;
                case 5:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.FiveText]);
                    break;
                case 6:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.SixText]);
                    break;
                case 7:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.SevenText]);
                    break;
                case 8:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.EightText]);
                    break;
                case 9:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.NineText]);
                    break;
                case 10:
                    CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
                    CheckInText = Regex.Replace(CheckInText, @"##", GlobalVariables.SelectedLanguageIdText[LanguageText.TenText]);
                    break;
                default:
					CheckInText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInForNAppointmentsText];
					CheckInText = Regex.Replace(CheckInText, @"##", ArriveSelectionCount.ToString());
					break;
			}
		}

		private void SelectedAppointmentByCheckBox(long appointmentId)
		{
			ArriveSelectionCount = AppointmentCollection.Count(c => c.IsChecked);
			SetArriveButtonText();
		}

		private void ConfirmArrival()
		{
			//Check for Arrivable appointment....If arrivable Do below one If not show error msg page

			GlobalVariables.AppointmentCollection = AppointmentCollection;
			int count = AppointmentCollection.Count(a => a.IsChecked);
			GlobalVariables.CommandParameter = 1;
			Messenger.Default.Send(AppPages.ArrivalConfirmationAndRouting);
		}
		
	}
}