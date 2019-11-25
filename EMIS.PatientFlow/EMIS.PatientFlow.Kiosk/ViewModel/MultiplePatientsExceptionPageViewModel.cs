using System;
using System.Linq;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using EMIS.PatientFlow.Kiosk.Model;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class MultiplePatientsExceptionPageViewModel : ViewModelBase
	{
        private string _checkInInfoText;
        private string _cantFindText;
        private string _close;
        private string _tryAgain;
		private string _columnSpan;
		private bool? _isPatientEnteredDetailsVisible;
		private Dictionary<String, String> _patientMatchList;
		private List<CustomiseUserDisplayText> DaysList;
		private List<CustomiseUserDisplayText> MonthList;

		private RelayCommand<string> _closeCommand;
        private RelayCommand<int> _tryAgainCommand;

        public string CheckInInfoText
        {
            get
            {
                return _checkInInfoText;
            }

            set
            {
                _checkInInfoText = value;
                RaisePropertyChanged("CheckInInfoText");
            }
        }

        public string CantFindText
        {
            get
            {
                return _cantFindText;
            }

            set
            {
                _cantFindText = value;
                RaisePropertyChanged("CantFindText");
            }
        }

        public string Close
        {
            get
            {
                return _close;
            }

            set
            {
                _close = value;
                RaisePropertyChanged("Close");
            }
        }

        public string TryAgain
        {
            get
            {
                return _tryAgain;
            }

            set
            {
                _tryAgain = value;
                RaisePropertyChanged("TryAgain");
            }
        }

		public string ColumnSpan
		{
			get
			{
				return _columnSpan;
			}

			set
			{
				_columnSpan = value;
				RaisePropertyChanged("ColumnSpan");
			}
		}

		public bool? IsPatientEnteredDetailsVisible
		{
			get
			{
				return _isPatientEnteredDetailsVisible;
			}

			set
			{
				_isPatientEnteredDetailsVisible = value;
				RaisePropertyChanged("IsPatientEnteredDetailsVisible");
			}
		}

		public Dictionary<String, String> PatientMatchList
        {
            get
            {
                return _patientMatchList;
            }

            set
            {
                _patientMatchList = value;
                RaisePropertyChanged("PatientMatchList");
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

        public RelayCommand<int> TryAgainCommand
        {
            get
            {
                return _tryAgainCommand
                    ?? (_tryAgainCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              Utilities.ClearPatientMatchingAppValues();
                                              if (GlobalVariables.IsBarCodeArrival)
                                                  Messenger.Default.Send(AppPages.ArrivalByBarcode);
                                              else if(GlobalVariables.IsArrive)
                                                  Utilities.SetPatientMatchFirstPage(KioskConfigType.PatientMatchArrival.ToString());
                                              else
                                                  Utilities.SetPatientMatchFirstPage(KioskConfigType.PatientMatchBooking.ToString());
                                          }));
            }
        }

		public MultiplePatientsExceptionPageViewModel()
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			DaysList = new List<CustomiseUserDisplayText>();
			MonthList = new List<CustomiseUserDisplayText>();
            if(GlobalVariables.IsArrive)
			    CantFindText = GlobalVariables.SelectedLanguageIdText[LanguageText.CantFindText];
			else
                CantFindText = GlobalVariables.SelectedLanguageIdText[LanguageText.CantFindTextAppointment];

            CheckInInfoText = GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInInfoText] +" "+ GlobalVariables.SelectedLanguageIdText[LanguageText.CheckInInfoText2];
			Close = GlobalVariables.SelectedLanguageIdText[LanguageText.CloseText];
			TryAgain = GlobalVariables.SelectedLanguageIdText[LanguageText.TryAgain];
			GetDetails();
		}

		public void GetDetails()
        {
            if (GlobalVariables.IsBarCodeArrival)
            {
				IsPatientEnteredDetailsVisible = null;
                ColumnSpan = "2";
            }
            else
            {
                var configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
                var message = configRepository.GetKioskConfiguration<Message>(KioskConfigType.Message.ToString());

				DaysList = ViewModelHelper.GetDaysinText();
				MonthList = ViewModelHelper.GetMonthText();

				IsPatientEnteredDetailsVisible = true;
                if (GlobalVariables.IsArrive)
                    GetDisplayText(message.PatientMatchTitle);
                else
                    GetDisplayText(message.AppointmentMatchTitle);
            }
        }

		private void GetDisplayText(string patientMatchTitle)
		{
			PatientMatchList = new Dictionary<String, String>();
			if (string.IsNullOrEmpty(patientMatchTitle)) return;
			List<string> patientMatchTitles = patientMatchTitle.Split(',').ToList();

			if (patientMatchTitles != null && patientMatchTitles.Count > 0)
			{
				foreach (string title in patientMatchTitles)
				{
					string matchTitle = string.Empty;
					string matchValue = string.Empty;
					switch (title.Trim().ToUpper())
					{
						case "DAY OF BIRTH":
							matchTitle = GlobalVariables.SelectedLanguageIdText[LanguageText.DayOfBirth];
							matchValue = DaysList.FirstOrDefault(m => m.Value == GlobalVariables.Day).DisplayText +
										 DaysList.FirstOrDefault(m => m.Value == GlobalVariables.Day).OrdinalText;
							break;
						case "MONTH OF BIRTH":
							matchTitle = GlobalVariables.SelectedLanguageIdText[LanguageText.MonthOfBirth];
							matchValue = MonthList.FirstOrDefault(m => m.Value == GlobalVariables.PatientMatchSelectedMonth).DisplayText;
							break;
						case "YEAR OF BIRTH":
							matchTitle = GlobalVariables.SelectedLanguageIdText[LanguageText.YearOfBirth];
							if(GlobalVariables. Year != null)
							{
								matchValue = GlobalVariables.Year;
							}
							else
							{
								matchValue = GlobalVariables.SelectedLanguageIdText[LanguageText.YearNotPresentText];
                            }
							break;
						case "FULL DATE OF BIRTH":
							matchTitle = GlobalVariables.SelectedLanguageIdText[LanguageText.FullDateOfBirth];
							List<string> _dob = new List<string>();
							_dob = GlobalVariables.PatientMatchDobFilter.ToString().Split('/').ToList();
							matchValue = _dob[0] +
										 DaysList.FirstOrDefault(m => m.Value == _dob[0]).OrdinalText + " " +
										 MonthList.FirstOrDefault(m => m.Value == _dob[1]).DisplayText + " " +
										 _dob[2];

							break;
						case "GENDER":
							matchTitle = GlobalVariables.SelectedLanguageIdText[LanguageText.Gender];
							matchValue = GenderDisplayText(GlobalVariables.PatientMatchGender);
							break;
						case "FIRST LETTER OF SURNAME":
							matchTitle = GlobalVariables.SelectedLanguageIdText[LanguageText.FirstLetterOfSurname];
							matchValue = GlobalVariables.PatientMatchSurname;
							break;
					}
					PatientMatchList.Add(matchTitle + ":", matchValue);
				}
			}
		}

		public string GenderDisplayText(string selectedGender)
        {
            string value = GlobalVariables.SelectedLanguageIdText[LanguageText.NotDisclosed]; ;
            switch (selectedGender)
            {
                case "M":
                    value = GlobalVariables.SelectedLanguageIdText[LanguageText.Male];
                    break;
                case "F":
                    value = GlobalVariables.SelectedLanguageIdText[LanguageText.Female];
                    break;
                case "U":
                    value = GlobalVariables.SelectedLanguageIdText[LanguageText.Other];
                    break;
            }
            return value;
        }

	}
}