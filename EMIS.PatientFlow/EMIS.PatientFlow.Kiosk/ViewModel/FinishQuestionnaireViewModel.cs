using System;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class FinishQuestionnaireViewModel : ViewModelBase
	{
		private string _thankYouText;
		private string _answersSavedText;
		private string _feedBackText;
        private string _closeText;

		private RelayCommand<string> _loadedCommand;
        private RelayCommand<string> _closeCommand;

		public string ThankYouText
		{
			get { return _thankYouText; }
			set
			{
				_thankYouText = value;
				RaisePropertyChanged("ThankYouText");
			}
		}

		public string AnswersSavedText
		{
			get { return _answersSavedText; }
			set
			{
				_answersSavedText = value;
				RaisePropertyChanged("AnswersSavedText");
			}
		}

		public string FeedBackText
		{
			get { return _feedBackText; }
			set
			{
				_feedBackText = value;
				RaisePropertyChanged("FeedBackText");
			}
		}
		
        public string CloseText
        {
            get { return _closeText; }
            set
            {
				_closeText = value;
                RaisePropertyChanged("CloseText");
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
       
        public RelayCommand<string> CloseCommand
        {
            get
            {
                return _closeCommand
                    ?? (_closeCommand = new RelayCommand<string>(
                        p =>
                        {
                            Messenger.Default.Send(AppPages.HomePage);
                        }));
            }
        }

		public FinishQuestionnaireViewModel()
		{
			InitializeControls();
			SetControlText();
		}

		private void InitializeControls()
		{
			var languageRepository = DiResolver.CurrentInstance.Reslove<ILanguageRepository>();
			GlobalVariables.IsKeyboardInitialised = false;
			GlobalVariables.ArrivedPatientDetails = null;
		}

		internal void SetControlText()
		{
			ThankYouText = GlobalVariables.SelectedLanguageIdText[LanguageText.ThankYouText];
			AnswersSavedText = GlobalVariables.SelectedLanguageIdText[LanguageText.AnswersSavedText];
			FeedBackText = GlobalVariables.SelectedLanguageIdText[LanguageText.FeedBackText];
			CloseText = GlobalVariables.SelectedLanguageIdText[LanguageText.CloseText];
		}
	}
}
