using System;
using System.Globalization;
using System.Linq;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{   
    public class FinishBookingViewModel : ViewModelBase
    {
		private string _bookedYourAppointmentText;
		private string _doneText;
		private string _bookingDate;
		private string _bookingTime;
		private string _slotName;
		private string _doctorName;
		private string _branchSiteName;
		private RelayCommand<string> _loadedCommand;
		private RelayCommand<object> _doneCommand;

		public string BookedYourAppointmentText
		{
			get
			{
				return _bookedYourAppointmentText;
			}

			set
			{
				_bookedYourAppointmentText = value;
				RaisePropertyChanged("BookedYourAppointmentText");
			}
		}

		public string DoneText
		{
			get
			{
				return _doneText;
			}

			set
			{
				_doneText = value;
				RaisePropertyChanged("DoneText");
			}
		}

		public string BookingDate
		{
			get
			{
				return _bookingDate;
			}

			set
			{
				_bookingDate = value;
				RaisePropertyChanged("BookingDate");
			}
		}

		public string BookingTime
		{
			get
			{
				return _bookingTime;
			}

			set
			{
				_bookingTime = value;
				RaisePropertyChanged("BookingTime");
			}
		}

		public string SlotName
		{
			get
			{
				return _slotName;
			}

			set
			{
				_slotName = value;
				RaisePropertyChanged("SlotName");
			}
		}

		public string DoctorName
		{
			get
			{
				return _doctorName;
			}

			set
			{
				_doctorName = value;
				RaisePropertyChanged("DoctorName");
			}
		}

		public string BranchSiteName
		{
			get
			{
				return _branchSiteName;
			}

			set
			{
				_branchSiteName = value;
				RaisePropertyChanged("BranchSiteName");
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

		public RelayCommand<object> DoneCommand
		{
			get
			{
				return _doneCommand
					?? (_doneCommand = new RelayCommand<object>(
										  p =>
										  {
											  Messenger.Default.Send(AppPages.HomePage);
										  }));
			}
		}

		public FinishBookingViewModel()
		{
			InitializeControls();
			SetControlText();
		}

		internal void SetControlText()
		{
			BookedYourAppointmentText = GlobalVariables.SelectedLanguageIdText[LanguageText.BookedYourAppointmentText];
			DoneText = GlobalVariables.SelectedLanguageIdText[LanguageText.DoneText];
		}

		private void InitializeControls()
        {
            try
            {
				BookingDate = GlobalVariables.Appointment.SessionDate;
				GetBookingDisplayDate();

				BookingTime = GlobalVariables.Appointment.SessionTime;
				SlotName = Constants.OpenBracket + GlobalVariables.Appointment.SlotName + Constants.CloseBracket;

				var doctorDetails = GlobalVariables.DoctorDetailsBooking.FirstOrDefault(doctorDetailsBooking => doctorDetailsBooking.DoctorId == GlobalVariables.Appointment.DoctorId);
				if (doctorDetails != null)
				{
					DoctorName = doctorDetails.DoctorNameToDisplay;
					BranchSiteName = GlobalVariables.Appointment.SiteName;
				}
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
            }
        }

		private void GetBookingDisplayDate()
		{
			DateTime dateFormat = DateTime.ParseExact(BookingDate, "ddd dd MMMM yyyy", CultureInfo.InvariantCulture);
			int day = dateFormat.Day;
			string ordinal = ViewModelHelper.AddOrdinal(day);
			BookingDate = String.Format("{0:dddd dd}{1} {0:MMMM yyyy}", dateFormat, ordinal);
		}
	}
}