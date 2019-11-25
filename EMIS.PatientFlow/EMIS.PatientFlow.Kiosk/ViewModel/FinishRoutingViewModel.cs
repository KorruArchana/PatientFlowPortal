using System;
using System.Collections.Generic;
using System.Linq;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class FinishRoutingViewModel : ViewModelBase
	{
		private readonly IConfigurationRepository _configRepository;
		private readonly IQuestionnaireRepository _questionaireRepository;
		private RelayCommand<string> _loadedCommand;
		private List<Questions> _questions;

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

		private bool IsQuestionsPresent
		{
			get
			{
				return Questions != null && Questions.Count > 0;
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
											  else if (GlobalVariables.KioskArrival.ForceSurvey && !GlobalVariables.IfForcedSurveyDone && IsQuestionsPresent)
											  {
												  GlobalVariables.AnswerOptionLimit = 200;
												  GlobalVariables.NonAnonymousQuestionList = Questions;
												  Messenger.Default.Send(AppPages.SurveyQuestions);
											  }
											  else
											  {
												  GlobalVariables.IfForcedSurveyDone = GlobalVariables.KioskArrival.ForceSurvey && !IsQuestionsPresent;
												  GlobalVariables.NonAnonymousQuestionList = Questions;
												  Messenger.Default.Send(AppPages.Finish);
											  }
										  }));
			}
		}

		public FinishRoutingViewModel()
		{
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			_questionaireRepository = DiResolver.CurrentInstance.Reslove<IQuestionnaireRepository>();
			if (GlobalVariables.KioskArrival == null)
			{
				var kioskArrival = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
				GlobalVariables.KioskArrival = kioskArrival;
			}

			if (GlobalVariables.ArrivedPatientDetails != null && GlobalVariables.ArrivedPatientDetails.BookedPatient != null)
			{
				DateTime pateintDob = Convert.ToDateTime(GlobalVariables.ArrivedPatientDetails.BookedPatient.Dob);
				DateTime today = DateTime.Today;
				int patientAge = today.Year - pateintDob.Year;
				if (pateintDob > DateTime.Now.AddYears(-patientAge))
					patientAge--;

				GlobalVariables.ArrivedPatientAge = patientAge;
			}

			GetQuestionsList();
		}

		private void GetQuestionsList()
		{
			if (CheckIfSurveySelected())
				GetNonAnonymousQuestions();
		}

		private bool CheckIfSurveySelected()
		{
			bool isSurveyAvailable = false;
			var moduleOptions = _configRepository.GetKioskConfiguration<List<Options>>(KioskConfigType.Modules.ToString());
			if (moduleOptions.Where(mod => mod.ModuleNameToDisplay == AppPages.Surveys.GetDisplayName()).ToList().Count > 0)
				isSurveyAvailable = true;
			return isSurveyAvailable;
		}

		private void GetNonAnonymousQuestions()
		{
			try
			{
				List<Questionnaire> questionnaire = _questionaireRepository.GetQuestionnairesByType(false).ToList();
				List<Questionnaire> copyQuestionnaire = questionnaire.ToList();

				List<NonAnonymousSurveyFrequency> patientQuestionnaire = _questionaireRepository.GetQuestionnairesByPatient();

				//Checking frequency
				if (patientQuestionnaire != null && patientQuestionnaire.Count > 0)
				{
					foreach (var questionnaireItem in
						from questionnaireItem in questionnaire
						from patientQn in patientQuestionnaire
						.Where(d => d.QuestionnaireId == questionnaireItem.Id)
						.Where(patientQn => questionnaireItem.Frequency != 0 && CheckDateDifference(patientQn, questionnaireItem))
						select questionnaireItem)
					{
						copyQuestionnaire.Remove(questionnaireItem);
					}
				}

				AddToQuestionList(copyQuestionnaire);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private static bool CheckDateDifference(NonAnonymousSurveyFrequency patientQn, Questionnaire questionnaireItem)
		{
			TimeSpan daysDiff = DateTime.Today - patientQn.AccessedOn;
			return daysDiff.Days < questionnaireItem.Frequency;
		}

		private void AddToQuestionList(List<Questionnaire> nonAnonymousQuestionnaire)
		{
			try
			{
				var questionList = new List<Questions>();
				List<Questionnaire> copyQuestionnaire = nonAnonymousQuestionnaire.ToList();

				foreach (var questionnaire in nonAnonymousQuestionnaire)
				{
					List<Questions> questions = _questionaireRepository.GetQuestionsByQuestionnaire(questionnaire.Id, 0);
					foreach (var question in questions)
					{
						if (!Utilities.CheckQuestionByPatient(question, GlobalVariables.ArrivedPatientAge)) continue;
						questionList.Add(question);
						_questionaireRepository.SaveQuestionniareFrequency(questionnaire);
						goto QuestionAdded;
					}
					copyQuestionnaire.Remove(questionnaire);
				}

				QuestionAdded:
				GlobalVariables.NonAnonymousQuestionnaireList = copyQuestionnaire.ToList();
				Questions = questionList.ToList();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
			}
		}
	}
}