using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;
using EMIS.PatientFlow.Kiosk.Enum;

namespace EMIS.PatientFlow.Kiosk.Model
{
	[DataContract]
	public class Options : INotifyPropertyChanged
	{
		private int _id;

		[DataMember(Name = "Id")]
		public int Id
		{
			get { return _id; }
			set
			{
				_id = value;
				_moduleType = (Modules)value;
			}
		}

		[DataMember(Name = "ModuleName")]
		public string ModuleName { get; set; }

		[DataMember(Name = "ModuleNameToDisplay")]
		public string ModuleNameToDisplay { get; set; }

		[DataMember(Name = "TranslationRefId")]
		public Int64? TranslationRefId { get; set; }

		public int DisplayOrder { get; set; }

		private string _buttonImagepath;

		public string ButtonImagepath
		{
			get { return _buttonImagepath; }
			set
			{
				_buttonImagepath = value;
				OnPropertyChanged("ButtonImagepath");
			}
		}

		public bool? IsButtonVisible { get; set; }

		public string HelpText { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private Modules _moduleType;
		public Modules ModuleType { get { return _moduleType; } }

		void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
