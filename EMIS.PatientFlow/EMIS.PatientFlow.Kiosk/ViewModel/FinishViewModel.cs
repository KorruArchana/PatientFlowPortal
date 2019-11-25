using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight.Views;
using EMIS.PatientFlow.Kiosk.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using EMIS.PatientFlow.Kiosk.View;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class FinishViewModel : ViewModelBase
	{
		private readonly IConfigurationRepository _configRepository;

		private string _thanksText;
		private string _patientNameText;
		private string _checkedAppointmentsText;
		private string _surveyMessageAskQuestionaireText;
		private string _wishToAnswerQuestionaireText;
		private string _wishToAnswerButtonText;
		private string _doNotWishToAnswerButtonText;

		private bool? _isgridVisible;
		private bool? _issurveyPromptVisible;
		private bool? _isDirectionalAlertsPresent;
		private bool? _isAlertsPresent;
		private bool _isAllAlertsPopUpVisible;
		private bool _isAllAppointmentsPopUpVisible;
        private List<AlertDisplay> _alertList;
		private List<AlertDisplay> _directionalAlertList;
		private RelayCommand<AppPages> _nextCommand;
		private RelayCommand<AppPages> _yesCommand;
		private RelayCommand<AppPages> _noCommand;
		private List<Questions> _questions;
		private RelayCommand<string> _loadedCommand;
		private RelayCommand _continueOrDoneCommand;
        private RelayCommand _showAllAlertsCommand;
        private RelayCommand _viewAllAppointmentsCommand;
		private RelayCommand _gotItCommand;
		private RelayCommand _closeCommand;
		private List<Appointment> _listOfArrivedAppointmentDetails;
		private List<Appointment> _displayListOfArrivedAppointmentDetails;

		private string _alertText;
		private string _allAlertsDisplayHeadingText;
		private string _allAppointmentsDisplayHeadingText;

		private string _continueOrDoneText;
		private string _showAllText;
		private string _gotItText;
		private string _closeText;
		private string _withText;
		private string _viewAllAppointmentsText;
		private string _moreArrivedAppointmentsText;
		private int _appointmentsColumnSpan;
		private int _arrivedAppointmentsCount;
		private int _arrivedForXMoreAppointmentsCount;


		public bool? IsSurveyPromptVisible
		{
			get { return _issurveyPromptVisible; }
			set
			{
				_issurveyPromptVisible = value;
				RaisePropertyChanged("IsSurveyPromptVisible");
			}
		}

		public bool? IsDirectionalAlertsPresent
		{
			get { return _isDirectionalAlertsPresent; }
			set
			{
				_isDirectionalAlertsPresent = value;
				RaisePropertyChanged("IsDirectionalAlertsPresent");
			}
		}

		public bool? IsGridVisible
		{
			get { return _isgridVisible; }
			set
			{
				_isgridVisible = value;
				RaisePropertyChanged("IsGridVisible");
			}
		}

		public string ThanksText
		{
			get
			{
				return _thanksText;
			}

			set
			{
				_thanksText = value;
				RaisePropertyChanged("ThanksText");
			}
		}

		public string PatientNameText
		{
			get
			{
				return _patientNameText;
			}

			set
			{
				_patientNameText = value;
				RaisePropertyChanged("PatientNameText");
			}
		}

		public string CheckedAppointmentsText
		{
			get
			{
				return _checkedAppointmentsText;
			}

			set
			{
				_checkedAppointmentsText = value;
				RaisePropertyChanged("CheckedAppointmentsText");
			}
		}

		public string SurveyMessageAskQuestionaireText
		{
			get
			{
				return _surveyMessageAskQuestionaireText;
			}

			set
			{
				_surveyMessageAskQuestionaireText = value;
				RaisePropertyChanged("SurveyMessageAskQuestionaireText");
			}
		}

		public string WishToAnswerQuestionaireText
		{
			get
			{
				return _wishToAnswerQuestionaireText;
			}

			set
			{
				_wishToAnswerQuestionaireText = value;
				RaisePropertyChanged("WishToAnswerQuestionaireText");
			}
		}

		public string WishToAnswerButtonText
		{
			get
			{
				return _wishToAnswerButtonText;
			}

			set
			{
				_wishToAnswerButtonText = value;
				RaisePropertyChanged("WishToAnswerButtonText");
			}
		}

		public string DoNotWishToAnswerButtonText
		{
			get
			{
				return _doNotWishToAnswerButtonText;
			}

			set
			{
				_doNotWishToAnswerButtonText = value;
				RaisePropertyChanged("DoNotWishToAnswerButtonText");
			}
		}

		public string AlertText
		{
			get
			{
				return _alertText;
			}

			set
			{
				_alertText = value;
				RaisePropertyChanged("AlertText");
			}
		}

		public string AllAlertsDisplayHeadingText
		{
			get
			{
				return _allAlertsDisplayHeadingText;
			}

			set
			{
				_allAlertsDisplayHeadingText = value;
				RaisePropertyChanged("AllAlertsDisplayHeadingText");
			}
		}

		public string AllAppointmentsDisplayHeadingText
		{
			get
			{
				return _allAppointmentsDisplayHeadingText;
			}

			set
			{
				_allAppointmentsDisplayHeadingText = value;
				RaisePropertyChanged("AllAppointmentsDisplayHeadingText");
			}
		}

		public bool? IsAlertsPresent
		{
			get
			{
				return _isAlertsPresent;
			}

			set
			{
				_isAlertsPresent = value;
				RaisePropertyChanged("IsAlertsPresent");
			}
		}

        public bool IsAllAlertsPopUpVisible
        {
            get
            {
                return _isAllAlertsPopUpVisible;
            }

            set
            {
                _isAllAlertsPopUpVisible = value;
                RaisePropertyChanged("IsAllAlertsPopUpVisible");
            }
        }

        public bool IsAllAppointmentsPopUpVisible
        {
            get
            {
                return _isAllAppointmentsPopUpVisible;
            }

            set
            {
                _isAllAppointmentsPopUpVisible = value;
                RaisePropertyChanged("IsAllAppointmentsPopUpVisible");
            }
        }

        public string ContinueOrDoneText
		{
			get
			{
				return _continueOrDoneText;
			}

			set
			{
				_continueOrDoneText = value;
				RaisePropertyChanged("ContinueOrDoneText");
			}
		}

		public string ShowAllText
		{
			get
			{
				return _showAllText;
			}

			set
			{
				_showAllText = value;
				RaisePropertyChanged("ShowAllText");
			}
		}

		public string GotItText
		{
			get
			{
				return _gotItText;
			}

			set
			{
				_gotItText = value;
				RaisePropertyChanged("GotItText");
			}
		}

		public string CloseText
		{
			get
			{
				return _closeText;
			}

			set
			{
				_closeText = value;
				RaisePropertyChanged("CloseText");
			}
		}

		public string WithText
		{
			get
			{
				return _withText;
			}

			set
			{
				_withText = value;
				RaisePropertyChanged("WithText");
			}
		}

		public string ViewAllAppointmentsText
		{
			get
			{
				return _viewAllAppointmentsText;
			}

			set
			{
				_viewAllAppointmentsText = value;
				RaisePropertyChanged("ViewAllAppointmentsText");
			}
		}

		public string MoreArrivedAppointmentsText
		{
			get
			{
				return _moreArrivedAppointmentsText;
			}

			set
			{
				_moreArrivedAppointmentsText = value;
				RaisePropertyChanged("MoreArrivedAppointmentsText");
			}
		}

		public int AppointmentsColumnSpan
		{
			get
			{
				return _appointmentsColumnSpan;
			}

			set
			{
				_appointmentsColumnSpan = value;
				RaisePropertyChanged("AppointmentsColumnSpan");
			}
		}

		public int ArrivedAppointmentsCount
		{
			get
			{
				return _arrivedAppointmentsCount;
			}

			set
			{
				_arrivedAppointmentsCount = value;
				RaisePropertyChanged("ArrivedAppointmentsCount");
			}
		}

		public int ArrivedForXMoreAppointmentsCount
		{
			get
			{
				return _arrivedForXMoreAppointmentsCount;
			}

			set
			{
				_arrivedForXMoreAppointmentsCount = value;
				RaisePropertyChanged("ArriveForXMoreAppointmentsCount");
			}
		}

		public List<Appointment> ListOfArrivedAppointmentDetails
		{
			get
			{
				return _listOfArrivedAppointmentDetails;
			}

			set
			{
				_listOfArrivedAppointmentDetails = value;
				RaisePropertyChanged("ListOfArrivedPatientDetails");
			}
		}

		public List<Appointment> DisplayListOfArrivedAppointmentDetails
		{
			get
			{
				return _displayListOfArrivedAppointmentDetails;
			}

			set
			{
				_displayListOfArrivedAppointmentDetails = value;
				RaisePropertyChanged("DisplayListOfArrivedAppointmentDetails");
			}
		}

		public List<AlertDisplay> AlertList
		{
			get { return _alertList; }
			set
			{
				_alertList = value;
				RaisePropertyChanged("AlertList");
			}
		}

		public List<AlertDisplay> DirectionalAlertList
		{
			get { return _directionalAlertList; }
			set
			{
				_directionalAlertList = value;
				RaisePropertyChanged("DirectionalAlertList");
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

		public RelayCommand ContinueOrDoneCommand
		{
			get
			{
				return _continueOrDoneCommand
					?? (_continueOrDoneCommand = new RelayCommand(ForwardNavigation));
			}
		}

        public RelayCommand ShowAllAlertsCommand
        {
            get
            {
                return _showAllAlertsCommand
                    ?? (_showAllAlertsCommand = new RelayCommand(
                        ShowAllAlerts));
            }
        }

        public RelayCommand ViewAllAppointmentsCommand
        {
            get
            {
                return _viewAllAppointmentsCommand
                    ?? (_viewAllAppointmentsCommand = new RelayCommand(
                        ViewAllAppointments));
            }
        }

        public RelayCommand GotItCommand
        {
            get
            {
                return _gotItCommand
                    ?? (_gotItCommand = new RelayCommand(
                        CloseShowAllAlertsDialog));
            }
        }

		public RelayCommand CloseCommand
		{
			get
			{
				return _closeCommand
					?? (_closeCommand = new RelayCommand(
						CloseShowAllAppointmentsDialog));
			}
		}

		public RelayCommand<AppPages> NextCommand
		{
			get
			{
				return _nextCommand
					?? (_nextCommand = new RelayCommand<AppPages>(
										  p => ForwardNavigation()));
			}
		}

		public RelayCommand<AppPages> YesCommand
		{
			get
			{
				return _yesCommand
					?? (_yesCommand = new RelayCommand<AppPages>(
										  p => Messenger.Default.Send(AppPages.SurveyQuestions)));
			}
		}

		public RelayCommand<AppPages> NoCommand
		{
			get
			{
				return _noCommand
					?? (_noCommand = new RelayCommand<AppPages>(
										  p => Messenger.Default.Send(AppPages.HomePage)));
			}
		}

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

		public FinishViewModel()
		{
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			if (GlobalVariables.KioskArrival == null)
			{
				var kioskArrival = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
				GlobalVariables.KioskArrival = kioskArrival;
			}
			InitializeControls();
			GetQuestionsList();
			InitialiseFinishPageContent();
			SetControlText();
		}

		private void InitializeControls()
		{
			ListOfArrivedAppointmentDetails = new List<Appointment>();
			PatientNameText = GlobalVariables.ArrivedPatientName;

			IsSurveyPromptVisible = null;
			IsGridVisible = true;
            IsAllAlertsPopUpVisible = false;
            IsAllAppointmentsPopUpVisible = false;
		}

		private void InitialiseFinishPageContent()
		{
			GetArrivedAppointments();
			GetAlerts();
		}

		private void GetQuestionsList()
		{
			Questions = GlobalVariables.NonAnonymousQuestionList;
		}

		private bool IsQuestionsPresent
		{
			get
			{
				return Questions != null && Questions.Count > 0;
			}
		}

		private void GetArrivedAppointments()
		{
			ListOfArrivedAppointmentDetails = GlobalVariables.ListOfArrivedAppointmentDetails;
			
			ArrivedAppointmentsCount = ListOfArrivedAppointmentDetails.Count;

			if (ListOfArrivedAppointmentDetails.Count > 6)
			{
				DisplayListOfArrivedAppointmentDetails = ListOfArrivedAppointmentDetails.Take(5).ToList();
				DisplayListOfArrivedAppointmentDetails.ForEach(m => m.IsViewAllAppointments = null);
				DisplayListOfArrivedAppointmentDetails.ForEach(m => m.IsNormalAppointment = true);
				DisplayListOfArrivedAppointmentDetails.Add(new Appointment()
				{
					IsViewAllAppointments = true,
					IsNormalAppointment = null,
					SessionHolder = new Member() { WaitingTime = 0 },
				}
				);

				ArrivedForXMoreAppointmentsCount = ArrivedAppointmentsCount - 5;
			}
			else
			{
				DisplayListOfArrivedAppointmentDetails = ListOfArrivedAppointmentDetails;
				DisplayListOfArrivedAppointmentDetails.ForEach(m => m.IsViewAllAppointments = null);
				DisplayListOfArrivedAppointmentDetails.ForEach(m => m.IsNormalAppointment = true);
			}

			MoreArrivedAppointmentsText = GlobalVariables.SelectedLanguageIdText[LanguageText.XMoreArrivedAppointmentsText];
			MoreArrivedAppointmentsText = Regex.Replace(MoreArrivedAppointmentsText, @"##", ArrivedForXMoreAppointmentsCount.ToString());

		}

		internal void SetControlText()
		{
			try
			{
				ThanksText = GlobalVariables.SelectedLanguageIdText[LanguageText.HiText];
                if (ArrivedAppointmentsCount < 2 )
                    CheckedAppointmentsText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckedAppointmentText];
                else
                    CheckedAppointmentsText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckedAppointmentsText];
				WithText = GlobalVariables.SelectedLanguageIdText[LanguageText.WithText];
				ViewAllAppointmentsText = GlobalVariables.SelectedLanguageIdText[LanguageText.ViewAllAppointmentsText];
				AlertText = GlobalVariables.SelectedLanguageIdText[LanguageText.WeWouldLikeToLetUKnowText];
				AllAlertsDisplayHeadingText = GlobalVariables.SelectedLanguageIdText[LanguageText.SurgeryWouldLikeToLetUKnowText];
				AllAppointmentsDisplayHeadingText = GlobalVariables.SelectedLanguageIdText[LanguageText.AppointmentsYouHaveCheckedInText];

				WishToAnswerQuestionaireText = GlobalVariables.SelectedLanguageIdText[LanguageText.WouldYouLikeToTakeSurveyText];
				WishToAnswerButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.WishToAnswerButtonText];
				DoNotWishToAnswerButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.DoNotWishToAnswerButtonText];

				SetButtonText();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void SetButtonText()
		{
			ShowAllText = GlobalVariables.SelectedLanguageIdText[LanguageText.ShowAllText];
			GotItText = GlobalVariables.SelectedLanguageIdText[LanguageText.GotItText];
			CloseText = GlobalVariables.SelectedLanguageIdText[LanguageText.CloseText];

			if (!GlobalVariables.KioskArrival.ForceSurvey && IsQuestionsPresent)
			{
				ContinueOrDoneText = GlobalVariables.SelectedLanguageIdText[LanguageText.ContinueText];
			}
			else
			{
				ContinueOrDoneText = GlobalVariables.SelectedLanguageIdText[LanguageText.DoneText];
			}
		}

        private void ShowAllAlerts()
        {
            IsAllAlertsPopUpVisible = true;
            IndexWindow.DarkWindow.Show();
        }

        private void ViewAllAppointments()
        {
            IndexWindow.DarkWindow.Show();
            IsAllAppointmentsPopUpVisible = true;
        }

        private void CloseShowAllAlertsDialog()
        {
            IsAllAlertsPopUpVisible = false;
            if(IndexWindow.DarkWindow != null)
            {
                IndexWindow.DarkWindow.Hide();
            }
        }

		private void CloseShowAllAppointmentsDialog()
		{
            IsAllAppointmentsPopUpVisible = false;
            if (IndexWindow.DarkWindow != null)
            {
                IndexWindow.DarkWindow.Hide();
            }
        }

        private void ForwardNavigation()
		{
			try
			{
				if (IsQuestionsPresent && !GlobalVariables.KioskArrival.ForceSurvey)
				{
					GlobalVariables.NonAnonymousQuestionList = Questions;
					IsSurveyPromptVisible = (!GlobalVariables.KioskArrival.ForceSurvey)? true : (bool?)null;
					IsGridVisible = null;
				}
				else
				{
					GlobalVariables.IfForcedSurveyDone = true; // To set that there are no survey question
					Messenger.Default.Send(AppPages.HomePage);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void GetAlerts()
		{
			try
			{
				List<Alerts> lstAlerts = _configRepository.GetKioskConfiguration<List<Alerts>>(KioskConfigType.Alerts.ToString());
				List<AlertDisplay> lstValidAlerts = new List<AlertDisplay>();

				//Alert by organisation
				if (lstAlerts != null && lstAlerts.Count > 0)
				{
					var lstAlertsByOrganisation = lstAlerts.Where(alert => (alert.OrganisationIds != null && alert.OrganisationIds.Count > 0 &&
										   alert.OrganisationIds.Contains(Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId))) && (alert.SessionHolderIdList == null || alert.SessionHolderIdList.Count == 0)).ToList();

					if (lstAlertsByOrganisation.Count > 0)
					{
						lstValidAlerts.AddRange(lstAlertsByOrganisation.Select(alerts => new AlertDisplay
						{
							OrganisationIds = alerts.OrganisationIds,
							AlertText = alerts.AlertText,
							Gender = alerts.Gender,
							Age1 = alerts.Age1,
							Age2 = alerts.Age2,
							Operation = alerts.Operation,
							AlertsDisplayType = alerts.AlertsDisplayType,
							SessionHolderIdList = alerts.SessionHolderIdList,
							LinkedKiosk = alerts.LinkedKiosk
						}));
					}
				}

				//Alert by member
				if (lstAlerts != null && lstAlerts.Count > 0)
				{
					var lstAlertsByMmeber = new List<Alerts>();

					if(GlobalVariables.ArrivedPatientDoctorList != null)
					{
						foreach (var alert in
						GlobalVariables.ArrivedPatientDoctorList
						.SelectMany(id => lstAlerts.Where(
							alert => (alert.SessionHolderIdList != null && alert.SessionHolderIdList.Count > 0 && alert.SessionHolderIdList.Contains(id)))
						.Where(alert => !lstAlertsByMmeber.Contains(alert))))
						{
							lstAlertsByMmeber.Add(alert);
						}
					}

					if (lstAlertsByMmeber.Count > 0)
					{
						lstValidAlerts.AddRange(lstAlertsByMmeber.Select(alerts => new AlertDisplay
						{
							OrganisationIds = alerts.OrganisationIds,
							AlertText = alerts.AlertText,
							Gender = alerts.Gender,
							Age1 = alerts.Age1,
							Age2 = alerts.Age2,
							Operation = alerts.Operation,
							AlertsDisplayType = alerts.AlertsDisplayType,
							SessionHolderIdList = alerts.SessionHolderIdList,
							LinkedKiosk = alerts.LinkedKiosk
						}));
					}
				}

				GetAlertsByPatient(lstValidAlerts);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
				AlertList = null;
			}
		}

		/// <summary>
		/// Method to get alerts by patient criteria
		/// </summary>
		/// <param name="lstAlerts">List of alerts</param>
		public void GetAlertsByPatient(List<AlertDisplay> lstAlerts)
		{
			try
			{
				//Calculate patient age
				if (GlobalVariables.ArrivedPatientDetails != null && GlobalVariables.ArrivedPatientDetails.BookedPatient != null)
				{
					int patientAge = GlobalVariables.ArrivedPatientAge;

					//Alerts by patient criterias
					List<AlertDisplay> lstAlertsByAge = new List<AlertDisplay>();

					if (lstAlerts != null && lstAlerts.Count > 0)
					{
						List<AlertDisplay> lstAlertsByGender =
							lstAlerts.Where(alert => (alert.Gender == GlobalVariables.ArrivedPatientDetails.BookedPatient.Gender || alert.Gender == "None")).ToList();

						foreach (var alert in lstAlertsByGender)
						{
							switch (alert.Operation)
							{
								case AgeOperations.LessThan:
									if (patientAge <= alert.Age1)
										lstAlertsByAge.Add(alert);
									break;
								case AgeOperations.GreaterThan:
									if (patientAge >= alert.Age1)
										lstAlertsByAge.Add(alert);
									break;
								case AgeOperations.Between:
									if ((patientAge >= alert.Age1 && patientAge <= alert.Age2) ||
										(patientAge >= alert.Age2 && patientAge <= alert.Age1))
										lstAlertsByAge.Add(alert);
									break;
								case AgeOperations.None:
									lstAlertsByAge.Add(alert);
									break;
							}
						}
					}

					if (DirectionalAlertList == null)
					{
						DirectionalAlertList = new List<AlertDisplay>();
					}
					DirectionalAlertList.AddRange(lstAlertsByAge.Where(a => a.AlertsDisplayType == DisplayType.Directional).ToList());

					if (DirectionalAlertList != null && DirectionalAlertList.Count != 0)
					{
						IsDirectionalAlertsPresent = true;

						DirectionalAlertList = (from arriveddetails in DirectionalAlertList
												select arriveddetails).Take(1).ToList();
					}
					else
					{
						IsDirectionalAlertsPresent = null;
					}
					lstAlertsByAge = lstAlertsByAge.Where(a => a.AlertsDisplayType != DisplayType.Directional).ToList();

					//Per-patient messaging
					List<AlertDisplay> alertList = GetPerPatientMessage(lstAlertsByAge);
					AlertList = alertList.Count != 0 ? alertList : null;

					if (AlertList != null && AlertList.Count != 0)
					{
						AlertList = AlertList.OrderByDescending(d => d.IsImportantAlert).ToList();

						IsAlertsPresent = true;
						AppointmentsColumnSpan = 0;
					}
					else
					{
						IsAlertsPresent = null;
						AppointmentsColumnSpan = 3;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
				AlertList = null;
			}
		}

		/// <summary>
		/// Method to get alerts by patient details
		/// </summary>
		/// <param name="lstAlertsByAge">list of alerts</param>
		/// <returns>final list of alerts</returns>
		public List<AlertDisplay> GetPerPatientMessage(List<AlertDisplay> lstAlertsByAge)
		{
			try
			{
				var patientMessageList = _configRepository.GetKioskConfiguration<List<PatientMessage>>(KioskConfigType.PatientMessage.ToString());
				if (patientMessageList != null && GlobalVariables.ArrivedPatientDetails.BookedPatient != null)
				{
					patientMessageList = patientMessageList.Where(patient => patient.OrganisationId.ToString().Equals(GlobalVariables.SelectedOrganisation.OrganisationId) &&
																			 patient.PatientId.Equals(GlobalVariables.ArrivedPatientDetails.BookedPatient.Id)).ToList();

					if (patientMessageList.Count > 0)
					{
						lstAlertsByAge.AddRange(patientMessageList.Select(alertMessage => new AlertDisplay
						{
							AlertText = alertMessage.Message
						}));
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
			}

			return lstAlertsByAge;
		}

	}
}