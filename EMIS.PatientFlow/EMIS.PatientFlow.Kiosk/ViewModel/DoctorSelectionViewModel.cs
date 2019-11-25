using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
	public class DoctorSelectionViewModel : ViewModelBase
	{
		private IConfigurationRepository _configRepository;

		private string _selectedDateText;
		private DateTime _selectedDate;
		private SlotWeekCalender _slotWeekCalender;
		private List<List<AppointmentSlots>> _appointmentSessionSlots;
		private List<DoctorDetailsBooking> _doctorDetailsBooking;
		private string _noAppointmentsText;

		private bool _enableScreenTap;
		private bool? _isNoAppointmentsTextVisible;
		private bool? _isDoctorAppointmentSlotsVisible;
		private bool? _isProgressBarVisible;
		private bool? _isPreviousButtonEnabled;

		private DateTime date;
		private List<AppointmentSlots> AppointmentSlots;

		private RelayCommand<string> _loadedCommand;
		private RelayCommand<DaySlotMapping> _previousWeekCommand;
		private RelayCommand<DaySlotMapping> _nextWeekCommand;
		private RelayCommand<object> _calenderCommand;
		private RelayCommand<string> _dateSelectedCommand;
		private RelayCommand<string> _selectDoctorCommand;
		private RelayCommand<int> _slotCommand;


		public string SelectedDateText
		{
			get { return _selectedDateText; }
			set
			{
				_selectedDateText = value;
				RaisePropertyChanged("SelectedDateText");
			}
		}

		public DateTime SelectedDate
		{
			get
			{
				return _selectedDate;
			}
			set
			{
				_selectedDate = value;
				RaisePropertyChanged("SelectedDate");
				SlotWeekCalender.SetIsSelected(SelectedDate);
			}
		}

		public SlotWeekCalender SlotWeekCalender
		{
			get
			{
				return _slotWeekCalender;
			}
			set
			{
				_slotWeekCalender = value;
				RaisePropertyChanged("SlotWeekCalender");
			}
		}

		public List<List<AppointmentSlots>> AppointmentSessionSlots
		{
			get
			{
				return _appointmentSessionSlots;
			}

			set
			{
				_appointmentSessionSlots = value;
				RaisePropertyChanged("AppointmentSessionSlots");
			}
		}

		public List<DoctorDetailsBooking> DoctorDetailsBooking
		{
			get
			{
				return _doctorDetailsBooking;
			}

			set
			{
				_doctorDetailsBooking = value;
				RaisePropertyChanged("DoctorDetailsBooking");
			}
		}

		public string NoAppointmentsText
		{
			get { return _noAppointmentsText; }
			set
			{
				_noAppointmentsText = value;
				RaisePropertyChanged("NoAppointmentsText");
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

		public bool? IsNoAppointmentsTextVisible
		{
			get
			{
				return _isNoAppointmentsTextVisible;
			}

			set
			{
				_isNoAppointmentsTextVisible = value;
				RaisePropertyChanged("IsNoAppointmentsTextVisible");
			}
		}

		public bool? IsDoctorAppointmentSlotsVisible
		{
			get
			{
				return _isDoctorAppointmentSlotsVisible;
			}

			set
			{
				_isDoctorAppointmentSlotsVisible = value;
				RaisePropertyChanged("IsDoctorAppointmentSlotsVisible");
			}
		}

		public bool? IsProgressBarVisible
		{
			get
			{
				return _isProgressBarVisible;
			}

			set
			{
				_isProgressBarVisible = value;
				RaisePropertyChanged("IsProgressBarVisible");
			}
		}

		public bool? IsPreviousButtonEnabled
		{
			get
			{
				return _isPreviousButtonEnabled;
			}
			set
			{
				_isPreviousButtonEnabled = value;
				RaisePropertyChanged("IsPreviousButtonEnabled");
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


		public RelayCommand<DaySlotMapping> PreviousWeekCommand
		{
			get
			{
				return _previousWeekCommand
					?? (_previousWeekCommand = new RelayCommand<DaySlotMapping>(
						p =>
						{
							DaySlotMapping daySlotMapping = p;
							date = daySlotMapping.Date;

							date = date.AddDays(-1);
							GetSlotWeekCalender();
						}
						));
			}
		}

		public RelayCommand<DaySlotMapping> NextWeekCommand
		{
			get
			{
				return _nextWeekCommand
					?? (_nextWeekCommand = new RelayCommand<DaySlotMapping>(
						p =>
						{
							DaySlotMapping daySlotMapping = p;
							date = daySlotMapping.Date;

							date = date.AddDays(1);
							GetSlotWeekCalender();
						}
						));
			}
		}

		public RelayCommand<object> CalenderCommand
		{
			get
			{
				return _calenderCommand
					?? (_calenderCommand = new RelayCommand<object>(
						p =>
						{
							Messenger.Default.Send(AppPages.BookingTimeSelection);
						}
						));
			}
		}

		public RelayCommand<string> DateSelectedCommand
		{
			get
			{
				return _dateSelectedCommand
					?? (_dateSelectedCommand = new RelayCommand<string>(
						 p =>
						 {
							 SelectedDate = Convert.ToDateTime(p);
							 ChangeDate(SelectedDate);
						 }
						 ));
			}
		}

		public RelayCommand<string> SelectDoctorCommand
		{
			get
			{
				return _selectDoctorCommand
					?? (_selectDoctorCommand = new RelayCommand<string>(
										  SelectDoctor));
			}
		}

		public RelayCommand<int> SlotCommand
		{
			get
			{
				return _slotCommand
					?? (_slotCommand = new RelayCommand<int>(
										  SelectSlot));
			}
		}

		public DoctorSelectionViewModel()
		{
			InitializeControls();
		}

		//Has to Recheck the need once...
		public RelayCommand<RoutedEventArgs> ListLoaded { get; set; }

		private void InitializeControls()
		{
			IsPreviousButtonEnabled = false;
			date = Convert.ToDateTime(GlobalVariables.Appointment.SessionDate);

			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			var deferredEventArgs = new Queue<RoutedEventArgs>();
			ListLoaded = new RelayCommand<RoutedEventArgs>(e => deferredEventArgs.Enqueue(e));
			EnableScreenTap = true;
			IsProgressBarVisible = true;

			GlobalVariables.BookAppointmentSettings = _configRepository.GetKioskConfiguration<BookingAppointment>(KioskConfigType.BookingAppointment.ToString());
			Task.Factory.StartNew(() =>
			{
				IsDoctorAppointmentSlotsVisible = null;
				IsNoAppointmentsTextVisible = null;
				NoAppointmentsText = GlobalVariables.SelectedLanguageIdText[LanguageText.NoAppointmentsAvailable];

				if (GlobalVariables.SelectedOrganisation.SystemType == SystemType.TPPSystmOne.GetDisplayName())
				{
					SetTPPSlotsAndDoctorDetails(Convert.ToDateTime(GlobalVariables.Appointment.SessionDate).Date);
				}
				else
				{
					DoctorDetailsBooking = GetDoctorsList(Convert.ToDateTime(GlobalVariables.Appointment.SessionDate).Date);
					FillSlots();
				}
				ListLoaded = new RelayCommand<RoutedEventArgs>(ListViewLoaded);
			}).ContinueWith(
				t =>
				{
					while (deferredEventArgs.Any())
						ListViewLoaded(deferredEventArgs.Dequeue());
					IsProgressBarVisible = false;
					if ((DoctorDetailsBooking == null || AppointmentSlots == null) || (AppointmentSlots != null && AppointmentSlots.Count == 0))
					{
						IsDoctorAppointmentSlotsVisible = null;
						IsNoAppointmentsTextVisible = true;
					}
					else
					{
						IsDoctorAppointmentSlotsVisible = true;
						IsNoAppointmentsTextVisible = null;
					}
				},
				TaskScheduler.FromCurrentSynchronizationContext());

			GetSlotWeekCalender();
			SelectedDate = date;
		}

		private void SetTPPSlotsAndDoctorDetails(DateTime date)
		{
			SelectedDateText = String.Format("{0:ddd dd MMMM yyyy}", date);
			TPPHelper tppHelper = new TPPHelper();
			var appointmentSessionList = tppHelper.GetSlots(date);
			SetTPPDoctorDetails(appointmentSessionList);

			var firstDoctor = DoctorDetailsBooking.FirstOrDefault();
			if (firstDoctor != null)
			{
				firstDoctor.IsSelected = true;
				GlobalVariables.Appointment.DoctorName = firstDoctor.DoctorName;
			}
			AppointmentSlots = appointmentSessionList.Where(o => o.Member.FirstName == GlobalVariables.Appointment.DoctorName).SelectMany(i => i.AppointmentSlotsList).ToList();

			AppointmentSessionSlots = AppointmentSlots.GroupBy(m => new { m.SiteName, m.ClinicType }).Select(grp => grp.ToList()).ToList();
		}

		private void SetTPPDoctorDetails(List<AppointmentSession> appointmentSessionList)
		{
			List<DoctorDetailsBooking> doctorTPPList = new List<DoctorDetailsBooking>();
			int memberId = 10000;

			foreach (var session in appointmentSessionList)
			{
				DoctorDetailsBooking doctorBookingDetails = new DoctorDetailsBooking();
				doctorBookingDetails.SiteName = new Dictionary<int, string>();
				doctorBookingDetails.DoctorId = memberId;
				doctorBookingDetails.DoctorName = session.Member.FirstName;
				doctorBookingDetails.DoctorNameToDisplay = session.Member.FirstName;

				if (!doctorTPPList.Any(o => o.DoctorName == session.Member.FirstName))
				{
					doctorTPPList.Add(doctorBookingDetails);
					memberId++;
				}
			}

			DoctorDetailsBooking = doctorTPPList;
		}

		private void GetSlotWeekCalender()
		{
			IsPreviousButtonEnabled = false;

			List<SlotWeekCalender> slotCalender = AppointmentHelper.GetWeekAvailability(date);
			SlotWeekCalender = slotCalender.FirstOrDefault();

			DateTime Monday = SlotWeekCalender.Monday.Date;
			DateTime LastSunday = Monday.AddDays(-1);

			if (LastSunday >= DateTime.Today)
			{
				IsPreviousButtonEnabled = true;
			}
		}

		private void SetGridVisibility(List<DoctorDetailsBooking> doctorList, List<List<AppointmentSlots>> AppointmentSessionSlots)
		{
			IsProgressBarVisible = null;
			if ((doctorList != null && doctorList.Count > 0) || (AppointmentSessionSlots != null && AppointmentSessionSlots.Count == 0))
			{
				IsNoAppointmentsTextVisible = null;
				IsDoctorAppointmentSlotsVisible = true;
			}
			else
			{
				IsNoAppointmentsTextVisible = true;
				IsDoctorAppointmentSlotsVisible = null;
			}
		}

		private void FillSlots()
		{
			try
			{
				SelectedDateText = GlobalVariables.Appointment.SessionDate;
				if (DoctorDetailsBooking != null && DoctorDetailsBooking.Count > 0)
				{
					DoctorDetailsBooking defaultDoctor = DoctorDetailsBooking[0];
					ApiHelper apiHelper = new ApiHelper();
					AppointmentSlots = apiHelper.GetSlotForSessions(defaultDoctor.SessionIDs);
					foreach (var slot in AppointmentSlots)
					{
						slot.SiteName = defaultDoctor.SiteName[slot.SessionId];
					}
					AppointmentSessionSlots = AppointmentSlots.GroupBy(m => m.SessionId).Select(grp => grp.ToList()).ToList();
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void ListViewLoaded(RoutedEventArgs e)
		{
			ListView doctorListView = e.Source as ListView;
			if (doctorListView != null)
			{
				List<DoctorDetailsBooking> doctorList = (List<DoctorDetailsBooking>)doctorListView.ItemsSource;
				if (doctorList != null && doctorList.Count > 0)
				{
					IsNoAppointmentsTextVisible = null;
					DoctorDetailsBooking defaultDoctor = (DoctorDetailsBooking)doctorListView.Items[0];
					GlobalVariables.Appointment.DoctorId = defaultDoctor.DoctorId;
				}
				else
				{
					IsNoAppointmentsTextVisible = true;
				}
			}
		}

		private List<DoctorDetailsBooking> GetDoctorsList(DateTime date)
		{
			try
			{
				string slotType = (GlobalVariables.SelectedSlotType == null) ? string.Empty : GlobalVariables.SelectedSlotType.SlotTypeId;

				ApiHelper apiHelper = new ApiHelper();

				var allSessionHolderList = apiHelper.GetAppointmentSessions(date, slotType);

				var doctorsList = new List<DoctorDetailsBooking>();
				if (!string.IsNullOrEmpty(GlobalVariables.BookAppointmentSettings.SelectedMemberList))
				{
					List<int> memberList = GlobalVariables.BookAppointmentSettings.SelectedMemberList.Split(',').Select(int.Parse).ToList();

					foreach (int id in memberList)
					{
						foreach (DoctorDetailsBooking detailsBooking in allSessionHolderList.Where(detailsBooking => detailsBooking.DoctorId == id))
						{
							doctorsList.Add(detailsBooking);
						}
					}
				}
				else
					doctorsList = allSessionHolderList;

				doctorsList = MatchSiteData(doctorsList);
				doctorsList = MatchMemberData(doctorsList);
				doctorsList.FirstOrDefault(m => m.IsSelected = true);
				GlobalVariables.DoctorDetailsBooking = doctorsList;
				return doctorsList;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return null;
			}
		}

		private List<Model.DoctorDetailsBooking> MatchSiteData(List<Model.DoctorDetailsBooking> doctorsList)
		{
			try
			{
				if (GlobalVariables.SelectedOrganisation.SiteId != null)
				{
					return doctorsList.Where(x => x.SiteId == GlobalVariables.SelectedOrganisation.SiteId).ToList();
				}
				else
				{
					return doctorsList;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return doctorsList;
			}
		}

		private List<Model.DoctorDetailsBooking> MatchMemberData(List<Model.DoctorDetailsBooking> doctorsList)
		{
			try
			{
				List<DoctorDetailsBooking> doctorDetailsList = new List<DoctorDetailsBooking>();

				List<LinkedMember> memberList = _configRepository.GetKioskConfiguration<List<LinkedMember>>(
												KioskConfigType.LinkedMember.ToString());
				if (memberList != null)
				{
					memberList = memberList.Where(linkedMember => linkedMember.OrganisationId.ToString().Equals(
						GlobalVariables.SelectedOrganisation.OrganisationId)).ToList();

					if (memberList.Count > 0)
					{
						foreach (var member in memberList)
							doctorDetailsList.AddRange(doctorsList.Where(a => a.DoctorId == member.SessionHolderId));
					}
					else
						doctorDetailsList = doctorsList;
				}
				else
					doctorDetailsList = doctorsList;

				return doctorDetailsList;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return doctorsList;
			}
		}

		private void SelectDoctor(string doctor)
		{
			IsProgressBarVisible = true;
			IsDoctorAppointmentSlotsVisible = null;
			IsNoAppointmentsTextVisible = null;
			EnableScreenTap = false;
			AppointmentSessionSlots = null;
			List<List<AppointmentSlots>> listOfAppointmentSlots = new List<List<Model.AppointmentSlots>>();

			Task.Factory.StartNew(() =>
			{
				DoctorDetailsBooking.ForEach(m => m.IsSelected = false);

				if (GlobalVariables.SelectedOrganisation.SystemType == SystemType.TPPSystmOne.GetDisplayName())
				{
					GlobalVariables.Appointment.DoctorName = doctor;
					TPPHelper tppHelper = new TPPHelper();
					var appointmentSessionList = tppHelper.GetSlots(SelectedDate);
					DoctorDetailsBooking.Where(d => d.DoctorName == doctor).FirstOrDefault().IsSelected = true;
					AppointmentSlots = appointmentSessionList.Where(o => o.Member.FirstName == doctor).SelectMany(i => i.AppointmentSlotsList).ToList();
					listOfAppointmentSlots = AppointmentSlots.GroupBy(m => new { m.SiteName, m.ClinicType }).Select(grp => grp.ToList()).ToList();
				}
				else
				{
					GlobalVariables.Appointment.DoctorId = Convert.ToInt32(doctor);
					foreach (var item in DoctorDetailsBooking.Where(w => w.DoctorId == GlobalVariables.Appointment.DoctorId))
					{
						item.IsSelected = true;
						ApiHelper apiHelper = new ApiHelper();
						AppointmentSlots = apiHelper.GetSlotForSessions(item.SessionIDs);
						foreach (var slot in AppointmentSlots)
						{
							slot.SiteName = item.SiteName[slot.SessionId];
						}
						listOfAppointmentSlots = AppointmentSlots.GroupBy(m => m.SessionId).Select(grp => grp.ToList()).ToList();
					}
				}
			}).ContinueWith(
				t =>
				{
					IsDoctorAppointmentSlotsVisible = true;
					EnableScreenTap = true;
					if (!AppointmentSlots.Any())
					{
						IsNoAppointmentsTextVisible = true;
					}
					AppointmentSessionSlots = listOfAppointmentSlots;
					Dispatcher.CurrentDispatcher.BeginInvoke(
						new Action(() => IsProgressBarVisible = null));
				},
				TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void ChangeDate(DateTime selectedDate)
		{
			IsProgressBarVisible = true;
			IsDoctorAppointmentSlotsVisible = null;
			IsNoAppointmentsTextVisible = null;
			EnableScreenTap = false;
			AppointmentSessionSlots = null;
			List<List<AppointmentSlots>> listOfAppointmentSlots = new List<List<Model.AppointmentSlots>>();
			List<DoctorDetailsBooking> doctorList = null;

			Task.Factory.StartNew(() =>
			{
				ApiHelper apiHelper = new ApiHelper();
				DoctorDetailsBooking = null;
				AppointmentSlots = null;
				doctorList = new List<DoctorDetailsBooking>();
				GlobalVariables.Appointment.SessionDate = String.Format("{0:ddd dd MMMM yyyy}", selectedDate);

				if (GlobalVariables.SelectedOrganisation.SystemType == SystemType.TPPSystmOne.GetDisplayName())
				{
					SetTPPSlotsAndDoctorDetails(selectedDate);
					doctorList = DoctorDetailsBooking;
					doctorList.FirstOrDefault(m => m.IsSelected = true);
				}
				else
				{
					doctorList = GetDoctorsList(selectedDate);
					DoctorDetailsBooking = doctorList;

					SelectedDateText = GlobalVariables.Appointment.SessionDate;

					if (doctorList != null && doctorList.Count > 0)
					{
						GlobalVariables.Appointment.DoctorId = doctorList[0].DoctorId;
						AppointmentSlots = apiHelper.GetSlotForSessions(doctorList[0].SessionIDs);
						foreach (var slot in AppointmentSlots)
							slot.SiteName = doctorList[0].SiteName[slot.SessionId];
						listOfAppointmentSlots = AppointmentSlots.GroupBy(m => m.SessionId).Select(grp => grp.ToList()).ToList();
					}
				}
			}).ContinueWith(
				t =>
				{
					EnableScreenTap = true;
					if (GlobalVariables.SelectedOrganisation.SystemType != SystemType.TPPSystmOne.GetDisplayName())
					{
						AppointmentSessionSlots = listOfAppointmentSlots;
					}
					Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => SetGridVisibility(doctorList, AppointmentSessionSlots)));
				},
				TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void SelectSlot(int p)
		{
			IsProgressBarVisible = true;
			IsDoctorAppointmentSlotsVisible = null;
			EnableScreenTap = false;

			Task.Factory.StartNew(() =>
			{
				foreach (var item in AppointmentSlots)
				{
					if (p == item.SlotId)
					{
						GlobalVariables.Appointment.SessionTime = string.Concat(item.StartTime, " - ", item.EndTime);
						GlobalVariables.Appointment.SlotId = item.SlotId;
						GlobalVariables.Appointment.SiteName = item.SiteName;
						GlobalVariables.Appointment.SlotName = item.SlotTypeDescription;

						if (GlobalVariables.SelectedOrganisation.SystemType == SystemType.TPPSystmOne.GetDisplayName())
						{
							GlobalVariables.Appointment.DoctorId = GlobalVariables.Appointment.MemberId;
							var doctorDetailsBooking = new DoctorDetailsBooking()
							{
								DoctorId = GlobalVariables.Appointment.MemberId++,
								DoctorName = GlobalVariables.Appointment.DoctorName,
								DoctorNameToDisplay = GlobalVariables.Appointment.DoctorName,
							};
							GlobalVariables.DoctorDetailsBooking.Add(doctorDetailsBooking);
							GlobalVariables.Appointment.SlotStartTime = item.StartTime;
							GlobalVariables.Appointment.ClinicType = item.ClinicType;
							GlobalVariables.Appointment.Duration = item.SlotLength;
							GlobalVariables.Appointment.UserName = item.UserName;
						}
					}
				}
			}).ContinueWith(
				t =>
				{
					IsDoctorAppointmentSlotsVisible = true;
					EnableScreenTap = true;
					Dispatcher.CurrentDispatcher.BeginInvoke(
						new Action(() => IsProgressBarVisible = null));
				},
				TaskScheduler.FromCurrentSynchronizationContext());
			Messenger.Default.Send(AppPages.ConfirmBooking);
		}
	}
}