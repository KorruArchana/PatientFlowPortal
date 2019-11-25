using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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
	public class SelectYearViewModel : ViewModelBase
	{
		private IAppointmentRepository _appointmentRepository;
		private string _selectYearText;
		private List<string> _yearList;
		private bool _enableScreenTap;
		private RelayCommand<string> _yearSelectionCommand;
		private string _noneButtonText;
		private bool? _isProgressBarVisible;
		private RelayCommand<string> _yearNotFoundCommand;
		private RelayCommand<string> _loadedCommand;
        private string _yearWelcomeText;
		public SelectYearViewModel()
		{
			InitializeControls();
            GetYearList();
		}

		public string SelectYearText
		{
			get { return _selectYearText; }
			set
			{
				_selectYearText = value;
				RaisePropertyChanged("SelectYearText");
			}
		}

		public List<string> YearList
		{
			get { return _yearList; }
			set
			{
				_yearList = value;
				RaisePropertyChanged("YearList");
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
											  if (!GlobalVariables.IsDbConnected)
											  {
												  Messenger.Default.Send(AppPages.ExceptionDivert);
											  }
										  }));
			}
		}

		public RelayCommand<string> YearSelectionCommand
		{
			get
			{
				return _yearSelectionCommand
					?? (_yearSelectionCommand = new RelayCommand<string>(
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

		public RelayCommand<string> YearNotFoundCommand
		{
			get
			{
				return _yearNotFoundCommand
					?? (_yearNotFoundCommand = new RelayCommand<string>(
										  p =>
										  {
											  GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
											  Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
										  }));
			}
		}
        public string YearWelcomeText
        {
            get
            {
                return _yearWelcomeText;
            }
            set
            {

                _yearWelcomeText = value;
                RaisePropertyChanged("YearWelcomeText");
            }
        }

		private void InitializeControls()
		{
			EnableScreenTap = true;
			IsProgressBarVisible = null;

			_appointmentRepository = DiResolver.CurrentInstance.Reslove<IAppointmentRepository>();
			YearList = new List<string>();
			NoneButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.NoneoftheAbove];
			SelectYearText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectYearText];
            YearWelcomeText = GlobalVariables.SelectedLanguageIdText[LanguageText.SelectionWelcomeText];
		}

		/// <summary>
		/// Method to get list of year to be displayed
		/// </summary>
		public void GetYearList()
		{
			try
			{
				var random = new Random();
				YearList = new List<string>();
				var appointmentYearCount = 0;

				List<Appointment> appointments = _appointmentRepository.GetMatchingAppointments(null, false);
				if (appointments != null && appointments.Count > 0)
				{
					YearList = 
						appointments.Select(appointment => Convert.ToDateTime(appointment.BookedPatient.Dob).Year.ToString(CultureInfo.InvariantCulture))
						.Distinct()
						.ToList();
					appointmentYearCount = YearList.Count;
				}
				else
				{
					var apiHelper = new ApiHelper();
					List<Appointment> _appointments = apiHelper.GetBookedPatients();
					if (_appointments != null && _appointments.Count > 0)
					{
						foreach (Appointment item in _appointments)
						{
							item.BookedPatient.Gender = item.BookedPatient.Gender.DecryptAES256();
							item.BookedPatient.Title = item.BookedPatient.Title.DecryptAES256();
							item.BookedPatient.CallingName = item.BookedPatient.CallingName.DecryptAES256();
							item.BookedPatient.FirstName = item.BookedPatient.FirstName.DecryptAES256();
							item.BookedPatient.FamilyName = item.BookedPatient.FamilyName.DecryptAES256();
							item.BookedPatient.Email = item.BookedPatient.Email.DecryptAES256();
							item.BookedPatient.Mobile = item.BookedPatient.Mobile.DecryptAES256();
							item.BookedPatient.HomeTelephone = item.BookedPatient.HomeTelephone.DecryptAES256();
							item.BookedPatient.WorkTelephone = item.BookedPatient.WorkTelephone.DecryptAES256();
							item.BookedPatient.PostCode = item.BookedPatient.PostCode.DecryptAES256();
							item.BookedPatient.Dob = item.BookedPatient.Dob.DecryptAES256();
						}

						List<Appointment> appointments1 = _appointmentRepository.GetMatchingAppointments(_appointments, true);
						if (appointments1 != null && appointments1.Count > 0)
						{
							YearList =
								appointments1.Select(
									appointment => Convert.ToDateTime(appointment.BookedPatient.Dob).Year.ToString(CultureInfo.InvariantCulture))
									.Distinct()
									.ToList();
							appointmentYearCount = YearList.Count;
						}
					}
					else
					{
						YearList = new List<string>();
					}
				}

				if (appointmentYearCount < 6)
				{
					var randomYears = new List<string>()
					{
						"1976",
						"1965",
						"1982",
						"1990",
						"1988",
						"1973",
						"1978",
						"1969",
						"1953",
						"1986",
						"1992",
						"1998",
						"1981",
						"1977",
						"1958",
						"1961",
						"2001",
						"2003",
						"2004"
					};
					while (YearList.Count < 6)
					{
						int maxValue = randomYears.Count - 1;
						int index = random.Next(maxValue);
						var year = randomYears[index];
						if (!YearList.Contains(year))
						{
							randomYears.RemoveAt(index);
							YearList.Add(year);
						}
					}
				}

				YearList = YearList.OrderBy(yearList => random.Next()).ToList();
				YearList.Sort();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		/// <summary>
		/// Method to set navigate to next patient match criteria
		/// </summary>
		/// <param name="selectedYear">selected year</param>
		private void ForwardNavigation(string selectedYear)
		{
			try
			{
				GlobalVariables.Year = selectedYear;
				EnableScreenTap = false;
				IsProgressBarVisible = true;
				Task.Factory.StartNew(Utilities.MatchPatient).ContinueWith(
						t =>
						{
							EnableScreenTap = true;
							IsProgressBarVisible = null;
						},
					TaskScheduler.FromCurrentSynchronizationContext());
				Utilities.PatientMatchForwardNavigation();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}
	}
}