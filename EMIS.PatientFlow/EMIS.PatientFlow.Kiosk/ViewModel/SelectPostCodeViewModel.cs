using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    public class SelectPostCodeViewModel : ViewModelBase
    {
        private IAppointmentRepository _appointmentRepository;
        private List<string> _postcodeList;
        private bool _enableScreenTap;
        private RelayCommand<string> _postCodeSelectionCommand;
        private string _noneButtonText;
        private bool? _isProgressBarVisible;
        private RelayCommand<string> _noneOfTheAboveCommand;
        private RelayCommand<string> _loadedCommand;
        private string _postcodeWelcomeText;
        private string _selectPostCodeText;

        public SelectPostCodeViewModel()
        {
            InitializeControls();
            GetPostcodeList();
        }

        public List<string> PostcodeList
        {
            get { return _postcodeList; }
            set
            {
                _postcodeList = value;
                RaisePropertyChanged("PostcodeList");
            }
        }

        public bool EnableScreenTap
        {
            get { return _enableScreenTap; }
            set
            {
                _enableScreenTap = value;
                RaisePropertyChanged("EnableScreenTap");
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
                               if (PostcodeList != null && (!GlobalVariables.IsDbConnected || PostcodeList.Count == 0))
                               {
                                   GlobalVariables.ErrorCode = ErrorCodes.MultiplePatientsFound;
                                   Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
                               }
                           }));
            }
        }

        public RelayCommand<string> PostCodeSelectionCommand
        {
            get
            {
                return _postCodeSelectionCommand
                       ?? (_postCodeSelectionCommand = new RelayCommand<string>(
                           ForwardNavigation));
            }
        }

        public string NoneButtonText
        {
            get { return _noneButtonText; }
            set
            {
                _noneButtonText = value;
                RaisePropertyChanged("NoneButtonText");
            }
        }

        public bool? IsProgressBarVisible
        {
            get { return _isProgressBarVisible; }
            set
            {
                _isProgressBarVisible = value;
                RaisePropertyChanged("IsProgressBarVisible");
            }
        }

        public RelayCommand<string> NoneOfTheAboveCommand
        {
            get
            {
                return _noneOfTheAboveCommand
                       ?? (_noneOfTheAboveCommand = new RelayCommand<string>(
                           p =>
                           {
                               GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
                               Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
                           }));
            }
        }

        public string PostcodeWelcomeText
        {
            get
            {
                return _postcodeWelcomeText;
            }
            set
            {
                _postcodeWelcomeText = value;
                RaisePropertyChanged("PostcodeWelcomeText");
            }
        }
        public string SelectPostCodeText
        {
            get
            {
                return _selectPostCodeText;
            }
            set
            {
                _selectPostCodeText = value;
                RaisePropertyChanged("SelectPostCodeText");
            }
        }
        private void InitializeControls()
        {
            EnableScreenTap = true;
            IsProgressBarVisible = null;

            _appointmentRepository = DiResolver.CurrentInstance.Reslove<IAppointmentRepository>();

            NoneButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.NoneoftheAbove];
            SelectPostCodeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectPostcodeText];
            PostcodeWelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectionWelcomeText];
        }

        /// <summary>
        /// Method to get list of year to be displayed
        /// </summary>
        private void GetPostcodeList()
        {
            try
            {
                List<Appointment> appointments = _appointmentRepository.GetMatchingAppointments(null, false);
				if (appointments != null && appointments.Any())
                {
                    PostcodeList =
                        appointments.Where(appointment => !string.IsNullOrEmpty(appointment.BookedPatient.PostCode)).Select(appointment => appointment.BookedPatient.PostCode.ToString(CultureInfo.InvariantCulture))
                            .Distinct()
                            .ToList();
                }
                else
                {
                    PostcodeList = new List<string>();
                }

                if (PostcodeList != null && PostcodeList.Count > 0)
                {
                    PostcodeList = GetRandomizedPostCode(PostcodeList, 6).ToList();
                    PostcodeList.Sort();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(EMIS.PatientFlow.Common.Enums.LogType.Error, ex.Message, ex, KioskId);
                Messenger.Default.Send(AppPages.ExceptionDivert);
            }
        }

        private static IEnumerable<string> GetRandomizedPostCode(List<string> postCodeList, int totalPostCodesNeeded)
        {
            var postCodesAvailableCount = postCodeList.Count;
            int numberOfRandomPostCodesNeeded = totalPostCodesNeeded - postCodesAvailableCount;

            int postCodeListIterator = 0;
            var randomPostCodeList = new List<String>();

            while (numberOfRandomPostCodesNeeded > 0)
            {
                String postCodeToRandomise = postCodeList[postCodeListIterator];
                String randomisedPostCode = RandomiseNumbersInPostCode(postCodeToRandomise);

                if (IsValidPostcode(randomisedPostCode) && !randomPostCodeList.Contains(randomisedPostCode))
                {
                    randomPostCodeList.Add(randomisedPostCode);
                    numberOfRandomPostCodesNeeded--;

                    if (++postCodeListIterator == postCodeList.Count)
                        postCodeListIterator = 0;
                }
            }

            var allPostCodesList = new List<String>();
            allPostCodesList.AddRange(postCodeList);
            allPostCodesList.AddRange(randomPostCodeList);
            return allPostCodesList;
        }
		
        private static bool IsValidPostcode(string postcode)
        {
			string Regex = @"^[A-Z]{1,2}[0-9][0-9A-Z]? ?[0-9][A-Z]{2}$";
			return System.Text.RegularExpressions.Regex.IsMatch(postcode, Regex);
		}

        private static String RandomiseNumbersInPostCode(string postCodeToRandomise)
		{
			postCodeToRandomise = GetTrimmedPostCode(postCodeToRandomise);
			var randomisedPostCode = new StringBuilder(postCodeToRandomise);

			var numbersAndIndexesInPostCode = GetNumbersAndIndexesInPostCode(postCodeToRandomise);

			foreach (var numberAndIndex in numbersAndIndexesInPostCode)
			{
				randomisedPostCode[numberAndIndex.Key] = char.Parse(GetRandomNumberFor(numberAndIndex.Value).ToString());
			}

			return randomisedPostCode.ToString();
		}

		private static string GetTrimmedPostCode(string postCodeToRandomise)
		{
			var result = postCodeToRandomise.Split(' ');
			string temp = string.Empty;
			foreach (string s in result)
			{
				if (!string.IsNullOrWhiteSpace(s))
				{
					temp += s + " ";
				}
			}
			postCodeToRandomise = temp.TrimEnd();
			return postCodeToRandomise;
		}

		private static int GetRandomNumberFor(int numberToRandomise)
        {
            var range = Enumerable.Range(0, 9).Where(number => number != numberToRandomise);
            var rand = new System.Random();
            int index = rand.Next(0, 8);
            return range.ElementAt(index);
        }

        private static Dictionary<int, int> GetNumbersAndIndexesInPostCode(string postCodeToRandomise)
        {
            int index = 0;

            return
                postCodeToRandomise
                    .Select(c => new { Index = index++, Value = c })
                    .Where(cObj => Char.IsDigit(cObj.Value))
                    .ToDictionary(chObj => chObj.Index, chObj => int.Parse(chObj.Value.ToString()));
        }

        /// <summary>
        /// Method to set navigate to next patient match criteria
        /// </summary>
        /// <param name="selectedYear">selected year</param>
        private void ForwardNavigation(string selectedPostCode)
        {
            try
            {
				GlobalVariables.PatientMatchPinCode = selectedPostCode;
				EnableScreenTap = false;
                IsProgressBarVisible = true;
                GlobalVariables.IsMuliplePatientCheckDone = true;
                Messenger.Default.Send(GlobalVariables.IsArrive ? AppPages.ArrivalRouting : AppPages.SlotType);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
                GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
                Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
            }
        }
    }
}