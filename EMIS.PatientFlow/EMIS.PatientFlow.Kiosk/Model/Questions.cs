using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using EMIS.PatientFlow.Kiosk.Enum;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class Questions : Entity, INotifyPropertyChanged
    {
        private bool? _isQuestionVisible;
        public string QuestionText { get; set; }
        public string QuestionCode { get; set; }
        public string Gender { get; set; }
        public int Age1 { get; set; }
        public int Age2 { get; set; }
        public AgeOperations Operation { get; set; }
        public string AnswerControlType { get; set; }
        public int OptionCharLimit { get; set; }
        public int QuestionOrder { get; set; }
             
        public bool? IsQuestionVisible
		{
            get { return _isQuestionVisible; }
            set
            {
				_isQuestionVisible = value;
                OnPropertyChanged("IsQuestionVisible");
            }
        }

        public List<string> QuestionOptions { get; set; }
        public List<QuestionnaireAnswerOption> AnswerOptions { get; set; }
        public string SelectedAnswer { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


		public int QuestionId { get; set; }
		public int QuestionnaireId { get; set; }
		public int QuestionType { get; set; }
		public int Order { get; set; }

	}
}
