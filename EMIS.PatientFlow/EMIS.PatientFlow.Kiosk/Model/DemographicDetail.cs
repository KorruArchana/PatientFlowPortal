using System.ComponentModel;

namespace EMIS.PatientFlow.Kiosk.Model
{
	public class DemographicDetail
	{
		private bool _isEnabled;
		public long SiteId { get; set; }
		public string Name { get; set; }
		public string DetailText { get; set; }


		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				_isEnabled = value;
				OnPropertyChanged("IsEnabled");
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