using System.Collections.Generic;
using System.ComponentModel;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class DoctorDetailsBooking : INotifyPropertyChanged
    {
        private int? _doctorId;
        private long? _siteId;
		private Dictionary<int,string> _siteName;
        private string _doctorName;
        private string _doctorNameToDisplay;
        private string _buttonImagepath;
		private List<int> _sessionIDs;
		private bool _isSelected;

        public int? DoctorId
        {
            get { return _doctorId; }
            set
            {
                _doctorId = value;
                OnPropertyChanged("DoctorID");
            }
        }

        public long? SiteId
        {
            get { return _siteId; }
            set
            {
                _siteId = value;
                OnPropertyChanged("SiteId");
            }
        }

        public string DoctorName 
        {
            get { return _doctorName; }
            set
            {
                _doctorName = value;
                OnPropertyChanged("DoctorName");
            }
        }

		public Dictionary<int, string> SiteName
		{
			get { return _siteName; }
			set
			{
				_siteName = value;
				OnPropertyChanged("SiteName");
			}
		}

        public string DoctorNameToDisplay
        {
            get { return _doctorNameToDisplay; }
            set
            {
                _doctorNameToDisplay = value;
                OnPropertyChanged("DoctorNameToDisplay");
            }
        }

        public string ButtonImagepath
        {
            get { return _buttonImagepath; }
            set
            {
                _buttonImagepath = value;
                OnPropertyChanged("ButtonImagepath");
            }
        }
		
		public List<int> SessionIDs
        {
            get { return _sessionIDs; }
            set
            {
                _sessionIDs = value;
                OnPropertyChanged("SessionIDs");
            }
        }

		public bool IsSelected
		{
			get
			{
				return _isSelected;
			}
			set
			{
				_isSelected = value;
				OnPropertyChanged("IsSelected");
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
    }
}
