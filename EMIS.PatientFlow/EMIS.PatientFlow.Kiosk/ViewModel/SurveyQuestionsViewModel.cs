using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class SurveyQuestionsViewModel : ViewModelBase
	{
		private int _questionnaireIndex;
		private IQuestionnaireRepository _questionaireRepository;
		private bool? _isProgressBarVisible;
		private bool? _isSkipVisible;
		private bool? _isContinueVisible;
		private RelayCommand<int> _checkBoxCommand;
		private RelayCommand<List<object>> _radioButtonCommand;
		private RelayCommand<object> _continueCommand;
		private RelayCommand<object> _skipCommand;
		private List<Questions> _questions;
		private Questions _selectedQuestion;
		private List<AnonymousSurvey> _lstAnonymousAnswers;
		private string _continueButtonText;
		private string _skipButtonText;
		private string _surgeryLikeToAsktext;
		private Questionnaire _currentQuestionnaire;
		private List<Questions> _questionList;
		private RelayCommand<string> _loadedCommand;
        private string _horizontalAlignmentValue;
        private string _verticalAlignmentValue;

		public bool? IsProgressBarVisible
		{
			get { return _isProgressBarVisible; }
			set
			{
				_isProgressBarVisible = value;
				RaisePropertyChanged("IsProgressBarVisible");
			}
		}

		public bool? IsSkipVisible
		{
			get { return _isSkipVisible; }
			set
			{
				_isSkipVisible = value;
				RaisePropertyChanged("IsSkipVisible");
			}
		}

		public bool? IsContinueVisible
		{
			get { return _isContinueVisible; }
			set
			{
				_isContinueVisible = value;
				RaisePropertyChanged("IsContinueVisible");
			}
		}

		public RelayCommand<int> CheckBoxCommand
		{
			get
			{
				return _checkBoxCommand
					?? (_checkBoxCommand = new RelayCommand<int>(
										  p =>
										  {
											  QuestionnaireAnswerOption Option = SelectedQuestion.AnswerOptions.FirstOrDefault(x => x.AnswerOptionId == p);
											  Option.IsChecked = !Option.IsChecked;
										  }));

			}
		}

		public RelayCommand<List<object>> RadioButtonCommand
		{
			get
			{
				return _radioButtonCommand
					?? (_radioButtonCommand = new RelayCommand<List<object>>(
										  p =>
										  {
											  if (p.Count > 1)
											  {
												  int answerid = (int)p[1];
												  SelectedQuestion.AnswerOptions.FirstOrDefault(a => a.AnswerOptionId == answerid).IsChecked = true;
											  }
											  Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => NextQuestion(false, p[0])));
										  }));
			}
		}

		public RelayCommand<object> ContinueCommand
		{
			get
			{
				return _continueCommand
					?? (_continueCommand = new RelayCommand<object>(
										  p =>
										  {
											  Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => NextQuestion(false, p)));
										  },
										  delegate { return CheckIfAnswered(); }
						));
			}
		}

		public RelayCommand<object> SkipCommand
		{
			get
			{
				return _skipCommand
					?? (_skipCommand = new RelayCommand<object>(
										  p =>
										  {
											  Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => NextQuestion(true, p)));
										  }));
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

		public RelayCommand<SelectionChangedEventArgs> ListSelectionChanged { get; set; }

		public RelayCommand<RoutedEventArgs> ListLoaded { get; set; }

		public List<Questions> Questions
		{
			get
			{
				return _questions;
			}

			set
			{
				_questions = value;
				RaisePropertyChanged("Questions");
			}
		}

		public List<Questions> QuestionList
		{
			get
			{
				return _questionList;
			}

			set
			{
				_questionList = value;
				RaisePropertyChanged("QuestionList");
			}
		}

		public Questions SelectedQuestion
		{
			get
			{
				return _selectedQuestion;
			}

			set
			{
				_selectedQuestion = value;
				RaisePropertyChanged("SelectedQuestion");
			}
		}

		public List<AnonymousSurvey> ListAnonymousAnswers
		{
			get
			{
				return _lstAnonymousAnswers;
			}

			set
			{
				_lstAnonymousAnswers = value;
				RaisePropertyChanged("ListAnonymousAnswers");
			}
		}

		public string SkipButtonText
		{
			get { return _skipButtonText; }
			set
			{
				_skipButtonText = value;
				RaisePropertyChanged("SkipButtonText");
			}
		}

		public string ContinueButtonText
		{
			get { return _continueButtonText; }
			set
			{
				_continueButtonText = value;
				RaisePropertyChanged("ContinueButtonText");
			}
		}

		public string SurgeryLikeToAsktext
		{
			get { return _surgeryLikeToAsktext; }
			set
			{
				_surgeryLikeToAsktext = value;
				RaisePropertyChanged("SurgeryLikeToAsktext");
			}
		}

		public Questionnaire CurrentQuestionnaire
		{
			get { return _currentQuestionnaire; }
			set
			{
				_currentQuestionnaire = value;
                RaisePropertyChanged("CurrentQuestionnaire");
			}
		}

        public string HorizontalAlignmentValue
        {
            get { return _horizontalAlignmentValue; }
            set
            {
                _horizontalAlignmentValue = value;
                RaisePropertyChanged("HorizontalAlignmentValue");
            }
        }

        public string VerticalAlignmentValue
        {
            get { return _verticalAlignmentValue; }
            set
            {
                _verticalAlignmentValue = value;
                RaisePropertyChanged("VerticalAlignmentValue");
            }
        }

		public SurveyQuestionsViewModel()
		{
			_questionaireRepository = DiResolver.CurrentInstance.Reslove<IQuestionnaireRepository>();
			DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			GlobalVariables.IsKeyboardInitialised = false;

			IsProgressBarVisible = null;
			IsContinueVisible = true;
                
			var configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
            GlobalVariables.KioskArrival = configRepository.GetKioskConfiguration<KioskArrival>(
												   KioskConfigType.KioskArrival.ToString());

            if (GlobalVariables.KioskArrival != null)
            {
                if (GlobalVariables.KioskArrival.SkipSurveyQuestion)
                {
                    IsSkipVisible = null;
                }
                else
                {
                    IsSkipVisible = true;
                }
            }
			QuestionList = new List<Questions>();
			SetQuestionnaireDetails();
			ListSelectionChanged = new RelayCommand<SelectionChangedEventArgs>(ListViewSelectionChanged);
			ListLoaded = new RelayCommand<RoutedEventArgs>(ListViewLoaded);
			SetControlText();
		}

		internal void SetControlText()
		{
			SurgeryLikeToAsktext = GlobalVariables.SelectedLanguageIdText[LanguageText.SurgeryLikeToAskQuestionsText];
			ContinueButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.ContinueText];
			SkipButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.SkipText];
		}

		/// <summary>
		/// Method to set questionnaire details
		/// </summary>
		public void SetQuestionnaireDetails()
		{
			if (GlobalVariables.ArrivedPatientDetails != null)
			{
				Questions = GlobalVariables.NonAnonymousQuestionList;
				CurrentQuestionnaire = GlobalVariables.NonAnonymousQuestionnaireList[_questionnaireIndex];
				_questionnaireIndex = 0;
				if (Questions != null && Questions.Count > 0)
				{
					SetQuestionParam();
					bool isTextBox = Questions[0].AnswerControlType != AnswerControlType.Textbox.ToString();
					bool isNumericTextBox = Questions[0].AnswerControlType == AnswerControlType.NumericTextBox.ToString();
					if (!isTextBox && !isNumericTextBox)
					{
						GlobalVariables.IsKeyboardInitialised = true;
					}
				}
			}
			else
				GetSurveyQuestion();
		}

		/// <summary>
		/// Method to get next question or finish survey
		/// </summary>
		/// <param name="skippedQuestion">true if skipped question</param>
		/// <param name="listView">the question</param>
		public void NextQuestion(bool skippedQuestion, object listView)
		{
			try
			{
				if (GlobalVariables.ArrivedPatientDetails == null)
					SaveAnswers(skippedQuestion);
				else
				{
					if (CurrentQuestionnaire.CreateConsultation)
					{
						SetQuestionAnswerText(skippedQuestion);
						QuestionList.Add(SelectedQuestion);
					}
				}

				if (GetSurveyQuestion())
				{
					ListView questionnaireList = listView as ListView;
					SetListItemVisibility(questionnaireList);
				}
				else
				{
					if (GlobalVariables.ArrivedPatientDetails == null)
					{
						_questionaireRepository.SaveAnonymousSurvey(ListAnonymousAnswers);
						Logger.Instance.WriteLog(LogType.Info, ActionType.SurveyDone.ToString(), null, KioskId);
						Messenger.Default.Send(AppPages.FinishQuestionnaires);
					}
					else
					{
						IsProgressBarVisible = true;
						bool result = false;
                        Task.Factory.StartNew(() =>
						{
                            IsSkipVisible = null;
                            IsContinueVisible = null;
                            IsProgressBarVisible = true;
							result = FilePatientRecord();
						}).ContinueWith(
							t =>
							{
                                IsProgressBarVisible = null;
								if (result)
								{
                                    string message = ActionType.NonAnonymousSurveyDone.ToString() + " - " + GlobalVariables.ArrivedPatientDetails.BookedPatient.Id;
                                    Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
                                    GlobalVariables.IfForcedSurveyDone = true;
                                    Messenger.Default.Send(GlobalVariables.KioskArrival != null && GlobalVariables.KioskArrival.ForceSurvey
										? AppPages.Finish
										: AppPages.FinishQuestionnaires);
                                    GlobalVariables.IsKeyboardInitialised = false;
								}
								else
								{
                                    Messenger.Default.Send(AppPages.ExceptionDivert);
								}
							}, TaskScheduler.FromCurrentSynchronizationContext());
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		/// <summary>
		/// Method to check if the anonymous survey option are selected.
		/// </summary>
		/// <returns>true if an option is selected</returns>
		public bool CheckIfAnswered()
		{
			try
			{
				if (SelectedQuestion != null)
					switch (SelectedQuestion.AnswerControlType)
					{
						case "Textbox":
						case "NumericTextBox":
							return !String.IsNullOrEmpty(SelectedQuestion.SelectedAnswer);
						case "CheckBox":
						case "RadioButton":
							return SelectedQuestion.AnswerOptions.Where(answerOptions => answerOptions.IsChecked).ToList().Count > 0;
						default:
							return true;
					}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return false;
			}

			return false;
		}

		/// <summary>
		/// Method to call API and save non anonymous survey as consultation
		/// </summary>
		/// <returns>true if file record is success</returns>
		public bool FilePatientRecord()
		{
			bool isSuccess = false;
			try
			{
				if (QuestionList != null && QuestionList.Any())
				{
					var apiHelper = new ApiHelper();
					string medicalRecordXml = apiHelper.SetPatientMedicalRecord(QuestionList);

					if (!string.IsNullOrEmpty(medicalRecordXml))
					{
						isSuccess = apiHelper.SavePatientSurvey(medicalRecordXml, string.Empty);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}

			return isSuccess;
		}

		/// <summary>
		/// Method that saves the anonymous survey answer to a list
		/// </summary>
		/// <param name="skippedQuestion">true if question is skipped</param>
		public void SaveAnswers(bool skippedQuestion)
		{
			try
			{
				AnonymousSurvey newAnswer = new AnonymousSurvey();
				newAnswer = SetAnswer(newAnswer, skippedQuestion);
				if (ListAnonymousAnswers == null)
					ListAnonymousAnswers = new List<AnonymousSurvey>();
				ListAnonymousAnswers.Add(newAnswer);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		/// <summary>
		/// Method to save question answer text for non anonymous survey
		/// </summary>
		/// <param name="skippedQuestion"></param>
		/// <returns></returns>
		public string SetQuestionAnswerText(bool skippedQuestion)
		{
			string answerText;
			if (!skippedQuestion)
			{
				answerText = string.IsNullOrEmpty(SelectedQuestion.SelectedAnswer) ? Constants.NotAnswered : SelectedQuestion.SelectedAnswer;
				if (SelectedQuestion.AnswerOptions.Any(answerOption => answerOption.IsChecked))
				{
					StringBuilder lstAnswerText = new StringBuilder();
					foreach (var option in SelectedQuestion.AnswerOptions.Where(answerOption => answerOption.IsChecked))
					{
						lstAnswerText.Append(option.AnswerOptionText).Append(",");
					}
					answerText = lstAnswerText.ToString().TrimEnd(',');
				}
			}
			else
			{
				answerText = Constants.SkippedAnswer;
			}
			SelectedQuestion.SelectedAnswer = answerText;
			return answerText;
		}

		public AnonymousSurvey SetAnswer(AnonymousSurvey answer, bool skippedQuestion)
		{
			try
			{
				answer.QuestionnaireId = GlobalVariables.SelectedSurveyOption;
				answer.QuestionnaireTitle = GlobalVariables.SelectedSurveyTitle;
				answer.KioskId = ConfigurationManager.AppSettings["RegistrationKey"];
				answer.QuestionId = SelectedQuestion.Id;
				answer.QuestionText = SelectedQuestion.QuestionText;
				answer.OptionId = null;

				if (!skippedQuestion)
				{
					answer.AnswerText = SelectedQuestion.SelectedAnswer;
					if (SelectedQuestion.AnswerOptions.Count(answerOptions => answerOptions.IsChecked) > 0)
					{
						StringBuilder lstOptionId = new StringBuilder();
						StringBuilder lstAnswerText = new StringBuilder();
						foreach (var option in SelectedQuestion.AnswerOptions.Where(answerOptions => answerOptions.IsChecked))
						{
							lstOptionId.Append(option.AnswerOptionId).Append(",");
							lstAnswerText.Append(option.AnswerOptionText).Append(",");
						}
						answer.OptionId = lstOptionId.ToString().TrimEnd(',');
						answer.AnswerText = lstAnswerText.ToString().TrimEnd(',');
					}
				}
				else
				{
					answer.AnswerText = Constants.SkippedAnswer;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}

			return answer;
		}

		/// <summary>
		/// Method to get next survey question
		/// </summary>
		/// <returns>Bool value</returns>
		public bool GetSurveyQuestion()
		{
			try
			{
				//Anonymous questions
				if (GlobalVariables.ArrivedPatientDetails == null)
				{
					GetQuestionByQuestionnaireIdNOrder(GlobalVariables.SelectedSurveyOption);
					if (Questions != null && Questions.Count > 0)
					{
						SetQuestionParam();
						return true;
					}
					else
						return false;
				}
				else
				{
					//Non-anonymous questions
					return GetNonAnonymousQuestion();
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
				return false;
			}
		}

		/// <summary>
		/// Method to get non-anonymous question
		/// </summary>
		/// <returns>true if there is question</returns>
		public bool GetNonAnonymousQuestion()
		{
			bool validQuestion = false;
			try
			{
				CurrentQuestionnaire = GlobalVariables.NonAnonymousQuestionnaireList[_questionnaireIndex];
				GetQuestionByQuestionnaireIdNOrder(CurrentQuestionnaire.Id);
				if (GetNextQuestion(CurrentQuestionnaire.Id))
				{
					SetQuestionParam();
					validQuestion = true;
				}
				else
				{
					_questionnaireIndex++;
					if (_questionnaireIndex < GlobalVariables.NonAnonymousQuestionnaireList.Count)
					{
						SelectedQuestion = null;
						validQuestion = GetNonAnonymousQuestion();
						if (validQuestion)
							_questionaireRepository.SaveQuestionniareFrequency(GlobalVariables.NonAnonymousQuestionnaireList[_questionnaireIndex]);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}

			return validQuestion;
		}

		/// <summary>
		/// Method to get next question for non-anonymous questionnaire
		/// </summary>
		/// <param name="questionnaireId">questionnaire id</param>
		/// <returns>returns next question</returns>
		public bool GetNextQuestion(int questionnaireId)
		{
			bool validQuestion = false;
			try
			{
				if (Questions != null && Questions.Count > 0)
				{
					//Checking Gender and age
					if (Utilities.CheckQuestionByPatient(Questions[0], GlobalVariables.ArrivedPatientAge))
						validQuestion = true;
					else
					{
						SelectedQuestion = Questions[0];
						GetQuestionByQuestionnaireIdNOrder(questionnaireId);
						validQuestion = GetNextQuestion(questionnaireId);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}

			return validQuestion;
		}

		/// <summary>
		/// Method to get a question from DB by questionnaire and order no
		/// </summary>
		/// <param name="questionnaireId"> questionnaire Id </param>
		public void GetQuestionByQuestionnaireIdNOrder(int questionnaireId)
		{
			try
			{
				if (SelectedQuestion == null || SelectedQuestion.AnswerOptions == null ||
					SelectedQuestion.AnswerOptions.Count == 0 ||
					SelectedQuestion.AnswerOptions.Where(answerOptions => answerOptions.IsChecked).ToList().Count == 0 ||
					(SelectedQuestion.AnswerOptions.Where(answerOptions => answerOptions.IsChecked).ToList().Count > 0 &&
					 // ReSharper disable once PossibleNullReferenceException
					 (SelectedQuestion.AnswerOptions.FirstOrDefault(answerOptions => answerOptions.IsChecked).NestedQuestion == 0 ||
					  SelectedQuestion.AnswerOptions.FirstOrDefault(answerOptions => answerOptions.IsChecked).NestedQuestion == SelectedQuestion.Id)))
				{
					Questions = _questionaireRepository.GetQuestionsByQuestionnaire(questionnaireId, SelectedQuestion == null ? 1 : SelectedQuestion.QuestionOrder + 1).ToList();
				}
				else
				{
					var questionnaireAnswerOption = SelectedQuestion.AnswerOptions.FirstOrDefault(answerOptions => answerOptions.IsChecked);
					if (questionnaireAnswerOption != null)
						Questions = _questionaireRepository.GetQuestionById(questionnaireAnswerOption.NestedQuestion).ToList();
				}
			}
			catch (Exception ex)
			{
				Questions = null;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		/// <summary>
		/// Method to get the question list for anonymous surveys
		/// </summary>
		public void SetQuestionParam()
		{
			try
			{
				IsContinueVisible = true;
                HorizontalAlignmentValue = "Left";
                VerticalAlignmentValue = "Top";

				switch (Questions[0].AnswerControlType)
				{
					case "1":
						Questions[0].AnswerControlType = AnswerControlType.Textbox.ToString();
						break;
					case "2":
						Questions[0].AnswerControlType = AnswerControlType.NumericTextBox.ToString();
						break;
					case "3":
						Questions[0].AnswerControlType = AnswerControlType.CheckBox.ToString();
						break;
					case "4":
						Questions[0].AnswerControlType = AnswerControlType.RadioButton.ToString();
						IsContinueVisible = null;
                        HorizontalAlignmentValue = "Center";
                        VerticalAlignmentValue = "Center";

						break;
				}

				Questions[0] = _questionaireRepository.GetAnswerOptionsByQuestion(Questions[0]);
				GlobalVariables.AnswerOptionLimit = Questions[0].OptionCharLimit == 0 ? 10 : Questions[0].OptionCharLimit;
				bool isTextBox = Questions[0].AnswerControlType == AnswerControlType.Textbox.ToString();
				bool isNumericTextBox = Questions[0].AnswerControlType == AnswerControlType.NumericTextBox.ToString();
				if (!isTextBox && !isNumericTextBox)
				{
					GlobalVariables.IsKeyboardInitialised = true;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		/// <summary>
		/// Method to set visibility of list view item 
		/// </summary>
		/// <param name="questionList">question list</param>
		private void SetListItemVisibility(ListView questionList)
		{
			List<Questions> questions = (List<Questions>)questionList.ItemsSource;
			try
			{
                HorizontalAlignmentValue = "Left";
                VerticalAlignmentValue = "Top";
				
                if (SelectedQuestion == null || SelectedQuestion.Id != questions[0].Id)
				{
					SelectedQuestion = questions[0];
					questions[0].IsQuestionVisible = true;
				}
				else
				{
					questions[0].IsQuestionVisible = null;
				}

				if (SelectedQuestion != null && SelectedQuestion.AnswerControlType == AnswerControlType.RadioButton.ToString())
				{
					IsContinueVisible = null;
                    HorizontalAlignmentValue = "Center";
                    VerticalAlignmentValue = "Center";
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		private void ListViewSelectionChanged(SelectionChangedEventArgs e)
		{
		}	

		private void ListViewLoaded(RoutedEventArgs e)
		{
			try
			{
				ListView questionList = e.Source as ListView;
				if (questionList != null)
				{
					questionList.SelectedIndex = 0;
					SetListItemVisibility(questionList);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}
	}
}
