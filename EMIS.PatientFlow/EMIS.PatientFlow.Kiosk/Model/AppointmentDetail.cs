using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EMIS.PatientFlow.Kiosk.Enum;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class AppointmentDetail : INotifyPropertyChanged
    {
        private bool _isChecked;
        private bool _isEnabled;
        private bool _confirmationFailed;
        private bool? _isCheckBoxVisible;
		private bool? _isErrorMessageVisible;
		private string _errorMessage;
		private int _patientId;

		public int DoctorId { get; set; }

        public string Name { get; set; }

        public string Time { get; set; }

		public string AppointmentTimeStyle { get; set; }

		public AppointmentArrivalStatus ArrivalType { get; set; }

        public string DelayText { get; set; }

		public string ErrorMessage
		{
			get
			{
				return _errorMessage;
			}
			set
			{
				_errorMessage = value;
				OnPropertyChanged("ErrorMessage");
			}
		}

		public string ErrorCode { get; set; }

        public int DoctorDelay { get; set; }

        public long AppointmentId { get; set; }
        public string TPPAppointmentId { get; set; }
        public string PatientIdentifier { get; set; }

		public DateTime AppointmentDate { get; set; }

        public long SiteId { get; set; }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public bool ConfirmationFailed
        {
            get { return _confirmationFailed; }
            set
            {
                _confirmationFailed = value;
                OnPropertyChanged("ConfirmationFailed");
            }
        }

		public bool? IsCheckBoxVisible
        {
            get { return _isCheckBoxVisible; }
            set
            {
                _isCheckBoxVisible = value;
                OnPropertyChanged("IsCheckBoxVisible");
            }
        }

		public ImageSource ErrorAppointmentImagepath
		{
			get
			{
				return new BitmapImage(new Uri("/Assets/Icons/UIIcons/ErrorAppointmentImage.png",UriKind.Relative));
			}
		}

		public bool? IsErrorImageVisible
		{
			get
			{
				if(IsCheckBoxVisible == true)
				{
					return null;
				}
				else if(IsCheckBoxVisible == null)
				{
					return true;
				}
				else
				{
					return true;
				}
			}
		}

		public bool? IsErrorMessageVisible
		{
			get { return _isErrorMessageVisible; }
			set
			{
				_isErrorMessageVisible = value;
				OnPropertyChanged("IsErrorMessageVisible");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
		public string ReceptionName { get; set; }

		public string DoctorUserName { get; set; }

		public int PatientId
		{
			get { return _patientId; }
			set
			{
				_patientId = value;
				OnPropertyChanged("PatientId");
			}
		}
	}
}
