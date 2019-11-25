using System.ComponentModel;
using System.Runtime.Serialization;

namespace EMIS.PatientFlow.Kiosk.Model
{
  [DataContract]
  public class QuestionnaireAnswerOption : INotifyPropertyChanged
  {
		private bool _isChecked;

		[DataMember(Name = "Id")]
        public int AnswerOptionId { get; set; }
        [DataMember(Name = "AnswerOptionText")]
        public string AnswerOptionText { get; set; }
        public string OptionCode { get; set; }
        public long SnomedCode { get; set; }
        public int NestedQuestion { get; set; }
        public bool IsChecked
		{
			get
			{
				return _isChecked;
			}
			set
			{
				_isChecked = value;
				OnPropertyChanged("IsChecked");
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
