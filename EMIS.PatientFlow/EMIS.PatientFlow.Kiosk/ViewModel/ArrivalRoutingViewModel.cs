using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Appointment = EMIS.PatientFlow.Kiosk.Model.Appointment;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class ArrivalRoutingViewModel : ViewModelBase
	{
		private IConfigurationRepository _configRepository;
		private IAppointmentRepository _appointmentRepository;
		private List<Divert> _lstDiverts;
		private AppointmentCollection _appointmentCollection;
		private List<Appointment> _appointments;
		private int _orgDbId; // Used to check Wrong Location
		private int _distinctPatientCount;
		private bool _exceptionFlag;
		private bool _isPatientNotFound;
		private bool _isSynched;
		private RelayCommand<string> _loadedCommand;
		private readonly ApiHelper _apiHelper = new ApiHelper();
		private bool? _isProgressBarVisible;

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

		public RelayCommand<string> LoadedCommand
		{
			get
			{
				return _loadedCommand
					?? (_loadedCommand = new RelayCommand<string>(
										  p =>
										  {
											  RouteToAppointmentsPage();
										  }));
			}
		}

		private AppPages NextRedirect()
		{
			if (GlobalVariables.KioskArrival == null)
			{
				GlobalVariables.KioskArrival = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
			}

			if (!AppointmentCollection.Any())
			{
				// PNF page
				GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
				return AppPages.MultiplePatientsExceptionPage;
			}

			AppointmentCollection.OrderByDescending(x => x.AppointmentDate);
			int arrivableAppointmentsCount = AppointmentCollection.Count(c => c.IsEnabled);

			if (AppointmentCollection.Count > 1 && GlobalVariables.KioskArrival.AutoConfirmMultipleArrival)
			{
				if (arrivableAppointmentsCount == AppointmentCollection.Count)
				{
					// api page					
					if (GlobalVariables.AppointmentCollection.Count != arrivableAppointmentsCount)
					{
						GlobalVariables.AppointmentCollection = AppointmentCollection;
					}

					GlobalVariables.CommandParameter = 2;
					return AppPages.ArrivalConfirmationAndRouting;
				}
				else
				{
					GlobalVariables.AppointmentCollection = AppointmentCollection;
					return AppPages.MultipleAppointments;
				}
			}
			else if (AppointmentCollection.Count == 1 && GlobalVariables.KioskArrival.AutoConfirmArrival)
			{
				if (arrivableAppointmentsCount == AppointmentCollection.Count)
				{
					GlobalVariables.CommandParameter = 3;
					return AppPages.ArrivalConfirmationAndRouting;
				}
				else
				{
					GlobalVariables.AppointmentCollection = AppointmentCollection;
					return AppPages.SingleAppointment;
				}
			}
			else
			{
				if (AppointmentCollection.Count > 1)
				{
					GlobalVariables.AppointmentCollection = AppointmentCollection;
					return AppPages.MultipleAppointments;
				}
				else if (AppointmentCollection.Count == 1)
				{
					GlobalVariables.AppointmentCollection = AppointmentCollection;
					return AppPages.SingleAppointment;
				}
				else
				{
					//PNF
					GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
					return AppPages.MultiplePatientsExceptionPage;
				}
			}
		}

		private AppPages ExceptionDivert()
		{
			Logger.Instance.WriteLog(LogType.Info, ActionType.MultipleMatchesInValid.GetDisplayName(), null, KioskId);
			GlobalVariables.ErrorCode = ErrorCodes.MultiplePatientsFound;
			return AppPages.MultiplePatientsExceptionPage;
		}

		public AppointmentCollection AppointmentCollection
		{
			get
			{
				return _appointmentCollection;
			}

			set
			{
				_appointmentCollection = value;
				RaisePropertyChanged("AppointmentCollection");
			}
		}

		public ArrivalRoutingViewModel()
		{
			InitializeControls();
			AppointmentCollection = new AppointmentCollection();
			IsProgressBarVisible = true;
		}

		private void RouteToAppointmentsPage()
		{
			Task.Factory.StartNew(() =>
			{
				FillAppointmentList();
			}).ContinueWith(
							t =>
							{
								if (_exceptionFlag || !GlobalVariables.IsDbConnected)
								{
									Messenger.Default.Send(AppPages.ExceptionDivert);
								}
								else if (_isPatientNotFound)
								{
									//route to PNF page
									GlobalVariables.ErrorCode = ErrorCodes.PatientNotFound;
									Messenger.Default.Send(AppPages.MultiplePatientsExceptionPage);
								}
								else if (_distinctPatientCount > 1)
								{
									Messenger.Default.Send(GlobalVariables.IsMuliplePatientCheckDone
									 ? ExceptionDivert()
									 : AppPages.SelectPostCode);
								}
								else
								{
									Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(NextRedirect())));
								}
							}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void InitializeControls()
		{
			GlobalVariables.AppointmentCollection = new AppointmentCollection();
			GlobalVariables.Appointments = new List<Appointment>();
			GlobalVariables.CommandParameter = -1;

			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			_appointmentRepository = DiResolver.CurrentInstance.Reslove<IAppointmentRepository>();
			_lstDiverts = _configRepository.GetKioskConfiguration<List<Divert>>(KioskConfigType.Divert.ToString());
			GlobalVariables.UserDemographicDetailsSettings = _configRepository.GetKioskConfiguration<UserDemographicSetting>(KioskConfigType.UserDemographicDetails.ToString());
		}

		private void FillAppointmentList()
		{
			var kioskArrival = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
			GlobalVariables.KioskArrival = kioskArrival;

			try
			{
				_appointments = _appointmentRepository.GetMatchingAppointments(_appointments, _isSynched);

				if (_appointments == null)
					_appointments = new List<Appointment>();

				if (!GlobalVariables.IsMuliplePatientCheckDone)
				{
					if (!_appointments.Any())
					{
						ResyncAppointmentDetails();
					}
					if (GlobalVariables.SelectedOrganisation.SystemType != SystemType.TPPSystmOne.GetDisplayName())
						if (kioskArrival.AllowUntimed)
						{
							SyncUntimedAppointments();
						}
						else
						{
							_appointments.RemoveAll(a => a.AppointmentTime == DateTime.Today);
						}
				}
				MatchAppointmentList();
				if (_distinctPatientCount <= 1)
				{
					_distinctPatientCount = AppointmentCollection.Select(x => x.PatientId).Distinct().Count();
				}
				if (AppointmentCollection.Any() && _distinctPatientCount == 1)
				{
					UpdateAppointmentsCollection();
				}
				else
				{
					if (_distinctPatientCount == 0)
						_isPatientNotFound = true;
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		private void UpdateAppointmentsCollection()
		{
			if (GlobalVariables.SelectedOrganisation.MainLocation && !GlobalVariables.SelectedOrganisation.SystemType.Equals(SystemType.TPPSystmOne.GetDisplayName()))
			{
				_orgDbId = _apiHelper.GetOrganisationId();
			}

			AppointmentCollection.OrderBy(x => x.AppointmentDate);

			if (AppointmentCollection.Count == 1)
			{
				AppointmentCollection.FirstOrDefault(app => app.IsChecked = true);
				SetEnablePropertyForAppointments();
			}
			else
			{
				SetEnablePropertyForAppointments();
				foreach (var appointment in AppointmentCollection.Where(appointment => appointment.IsEnabled))
				{
					appointment.IsCheckBoxVisible = true;
				}
			}

			GlobalVariables.AppointmentCollection = AppointmentCollection;
			_appointments.ForEach(m => m.DelayText = GetDelayText(m));
			GlobalVariables.Appointments = _appointments;

			var arrivedPatient = _appointments.FirstOrDefault(app => app.IsUnTimedAppointment == false);
			GlobalVariables.ArrivedPatientDetails = arrivedPatient ?? _appointments.FirstOrDefault();

			string arrivedPatientName = (string.IsNullOrEmpty(GlobalVariables.ArrivedPatientDetails.BookedPatient.Title) ? string.Empty : GlobalVariables.ArrivedPatientDetails.BookedPatient.Title + ". ") +
				GlobalVariables.ArrivedPatientDetails.BookedPatient.FirstName + " " +
				GlobalVariables.ArrivedPatientDetails.BookedPatient.FamilyName;
			GlobalVariables.ArrivedPatientName = arrivedPatientName;
		}

		private void SyncUntimedAppointments()
		{
			var patients = new List<PatientMatchesPatient>();

			PatientMatches patientMatches = _apiHelper.GetUntimedMatchedPatients(GlobalVariables.PatientMatchSurname);
			if (patientMatches != null && patientMatches.PatientList != null)
			{
				patients = GetMatchingPatients(patientMatches.PatientList.ToList());
			}

			if (!patients.Any()) return;
			_isPatientNotFound = false;

			foreach (var patient in patients)
			{
				GetPatientAppointments(patient);
			}
		}

		private void GetPatientAppointments(PatientMatchesPatient patient)
		{
			if (IsEmisWeb)
			{
				var emisPatientAppointments = _apiHelper.GetEmisPatientAppointmentList(Convert.ToInt32(patient.DBID));

				if (emisPatientAppointments != null && emisPatientAppointments.Appointment.Any())
				{
					var emisPatientApointments = emisPatientAppointments.Appointment.ToList();

					var unavailablePatientAppointments = emisPatientApointments.Where(e => e.Status != "Slot Available").ToList();
					_appointments.RemoveAll(a => unavailablePatientAppointments.Exists(x => x.SlotID == a.Id));

					var patientApointments = (emisPatientApointments
						.Where(patientAppointment => patientAppointment.Status == "Slot Available"))
						.Where(patientApointment => !_appointments.Exists(a => a.Id == patientApointment.SlotID && a.Duration > 0))
						.ToList();

					emisPatientAppointments.Appointment = patientApointments.ToArray();

					if (emisPatientAppointments.Appointment.Any())
					{
						var app = _apiHelper.SaveUntimedAppointments(emisPatientAppointments, patient);
						foreach (var appointment in _appointments.Where(a => app.Exists(p => p.Id == a.Id)))
						{
							appointment.Duration = app.Where(x => x.Id == appointment.Id).FirstOrDefault().Duration;
						}
						app = app.Where(patientApointment => !_appointments.Exists(a => a.Id == patientApointment.Id)).ToList();
						DecryptPatientData(app);
						_appointments.AddRange(app);
					}
				}
			}
			else
			{
				PCSPatientsAppointmentList.PatientAppointmentList pcsPatientAppointments =
					_apiHelper.GetPatientAppointmentList(Convert.ToInt32(patient.DBID));

				if (pcsPatientAppointments != null && pcsPatientAppointments.Appointment.Any())
				{
					var pcsPatientApointments = pcsPatientAppointments.Appointment.ToList();
					List<Int64> arrivedUntimedAppointments = _appointmentRepository.GetAppointmentsStatus(
						Convert.ToInt32(patient.DBID), Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId));

					var patientApointments = (pcsPatientApointments
						.Where(patientApointment => string.IsNullOrWhiteSpace(patientApointment.StartTime))
						.Where(patientApointment => !_appointments.Exists(a => a.Id == patientApointment.SlotID))
						.Where(patientApointment => !arrivedUntimedAppointments.Contains(patientApointment.SlotID))).ToList();

					pcsPatientAppointments.Appointment = patientApointments.ToArray();

					if (pcsPatientAppointments.Appointment.Any())
					{
						var app = _apiHelper.SaveUntimedAppointments(pcsPatientAppointments, patient);
						DecryptPatientData(app);
						_appointments.AddRange(app);
					}
				}
			}
		}

		private void DecryptPatientData(List<Appointment> appointments)
		{
			appointments.ForEach((item) =>
			{
				item.BookedPatient.CallingName = item.BookedPatient.CallingName.DecryptAES256();
				item.BookedPatient.FirstName = item.BookedPatient.FirstName.DecryptAES256();
				item.BookedPatient.FamilyName = item.BookedPatient.FamilyName.DecryptAES256();
				item.BookedPatient.Email = item.BookedPatient.Email.DecryptAES256();
				item.BookedPatient.Title = item.BookedPatient.Title.DecryptAES256();
				item.BookedPatient.Gender = item.BookedPatient.Gender.DecryptAES256();
				item.BookedPatient.Mobile = item.BookedPatient.Mobile.DecryptAES256();
				item.BookedPatient.HomeTelephone = item.BookedPatient.HomeTelephone.DecryptAES256();
				item.BookedPatient.WorkTelephone = item.BookedPatient.WorkTelephone.DecryptAES256();
				item.BookedPatient.PostCode = item.BookedPatient.PostCode.DecryptAES256();
				item.BookedPatient.PatientIdentifiers = item.BookedPatient.PatientIdentifiers.DecryptAES256();
				item.BookedPatient.Dob = item.BookedPatient.Dob.DecryptAES256(); //Has to check DOB validations yet in GetUntimedAppointments
			});
		}

		private List<PatientMatchesPatient> GetMatchingPatients(List<PatientMatchesPatient> patients)
		{
			int selectedDay = Convert.ToInt32(GlobalVariables.Day);
			int selectedYear = Convert.ToInt32(GlobalVariables.Year);
			int selectedMonth = Convert.ToInt32(GlobalVariables.PatientMatchSelectedMonth);
			string selectedGender = GlobalVariables.PatientMatchGender;
			string selectedSurname = GlobalVariables.PatientMatchSurname;
			string postcode = GlobalVariables.PatientMatchPinCode;
			DateTime? selectedDob = GlobalVariables.PatientMatchDob;
			try
			{
				if (patients != null)
				{
					patients = (selectedDay > 0) ? patients.Where(x => Convert.ToDateTime(x.DateOfBirth).Day == selectedDay).ToList() : patients;
					patients = (selectedYear > 0) ? patients.Where(x => Convert.ToDateTime(x.DateOfBirth).Year == selectedYear).ToList() : patients;
					patients = (selectedMonth > 0) ? patients.Where(x => Convert.ToDateTime(x.DateOfBirth).Month == selectedMonth).ToList() : patients;
					patients = (!string.IsNullOrEmpty(selectedGender)) ?
						GetAppointmentsBasedOnGender(selectedGender, patients) :
						patients;
					patients = (!string.IsNullOrEmpty(selectedSurname)) ? patients.Where(x => x.FamilyName.StartsWith(selectedSurname)).ToList() :
									patients;
					patients = (selectedDob != null) ? patients.Where(x => Convert.ToDateTime(x.DateOfBirth).Date == selectedDob.Value.Date).ToList() :
									patients;
					patients = (!string.IsNullOrEmpty(postcode)) ? patients.Where(x => x.Address.PostCode != null && x.Address.PostCode.ToUpper().Equals(postcode.ToUpper())).ToList() :
									patients;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}

			return patients;
		}

		private List<PatientMatchesPatient> GetAppointmentsBasedOnGender(string selectedGender, List<PatientMatchesPatient> patients)
		{
			return ((selectedGender != Constants.GenderOther) ? patients.Where(x => x.Sex == selectedGender).ToList() :
									patients.Where(x => (x.Sex != Constants.GenderMale && x.Sex != Constants.GenderFemale)).ToList());
		}

		private void SetEnablePropertyForAppointments()
		{
			if (GlobalVariables.SelectedOrganisation.SystemType.Equals(SystemType.EmisWeb.GetDisplayName()))
			{
				AppointmentCollection.ForEach(x => x.IsEnabled = !CheckForWrongLocation(x) && !CheckForDivert(x) && (x.ArrivalType == AppointmentArrivalStatus.OnTime));
			}
			if (GlobalVariables.SelectedOrganisation.SystemType.Equals(SystemType.EmisPcs.GetDisplayName()))
			{
				AppointmentDetail firstOrDefault = AppointmentCollection.FirstOrDefault();
				if (firstOrDefault != null)
				{
					int patientId = firstOrDefault.PatientId;
					var result = _apiHelper.GetPatientAppointmentList(patientId);
					var patientAppointments = result.Appointment != null ? result.Appointment.ToList() : null;
					AppointmentCollection.ForEach(x => x.IsEnabled = !CheckForPcsWrongLocation(x, patientAppointments) && !CheckForDivert(x) && (x.ArrivalType == AppointmentArrivalStatus.OnTime));
				}
			}
			if (GlobalVariables.SelectedOrganisation.SystemType.Equals(SystemType.TPPSystmOne.GetDisplayName()))
			{
				AppointmentCollection.ForEach(x => x.IsEnabled = true && !CheckForDivert(x) && (x.ArrivalType == AppointmentArrivalStatus.OnTime));
			}
			foreach (AppointmentDetail appointment in AppointmentCollection.Where(x => !x.IsEnabled))
			{
				appointment.DelayText = string.Empty;
			}
		}

		private bool CheckForDivert(AppointmentDetail appointment)
		{
			try
			{
				var isDivertPresent = false;
				var doctorId = appointment.DoctorId;

				if (appointment.ArrivalType == AppointmentArrivalStatus.WrongLocation)
					return true;

				if (_lstDiverts != null)
				{
					if (GlobalVariables.SelectedOrganisation.SystemType == SystemType.TPPSystmOne.GetDisplayName())
					{
						List<string> sessionHolderIdList = _lstDiverts.Where(x => x.OrganisationId.ToString(CultureInfo.InvariantCulture) == GlobalVariables.SelectedOrganisation.OrganisationId).Select(x => x.LoginId).ToList();
						isDivertPresent = sessionHolderIdList.Contains(appointment.DoctorUserName);
					}
					else
					{
						List<int> sessionHolderIdList = _lstDiverts.Where(x => x.OrganisationId.ToString(CultureInfo.InvariantCulture) == GlobalVariables.SelectedOrganisation.OrganisationId).Select(x => x.SessionHolderId).ToList();
						isDivertPresent = sessionHolderIdList.Contains(doctorId);
					}
				}

				if (isDivertPresent)
				{
					appointment.ArrivalType = AppointmentArrivalStatus.DoctorDivert;
					string message = ActionType.DoctorDivert.GetDisplayName();
					message = message + " - " + appointment.Name + " - " + appointment.Time;
					Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
				}

				return isDivertPresent;
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return true;
			}

		}

		private bool CheckForWrongLocation(AppointmentDetail appointment)
		{
			try
			{
				if (appointment.ArrivalType == AppointmentArrivalStatus.WrongLocation)
					return true;

				bool isInWrongLocation = GlobalVariables.SelectedOrganisation.SiteId != null && appointment.SiteId != GlobalVariables.SelectedOrganisation.SiteId && appointment.SiteId != _orgDbId;

				if (isInWrongLocation)
				{
					appointment.ArrivalType = AppointmentArrivalStatus.WrongLocation;
					string message = ActionType.WrongLocation.GetDisplayName();
					message = message + " - " + appointment.Name + " - " + appointment.Time + " - " + appointment.SiteId;
					Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
				}

				return isInWrongLocation;
			}

			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return true;
			}
		}

		private bool CheckForPcsWrongLocation(AppointmentDetail appointment, List<PCSPatientsAppointmentList.AppointmentStruct> patientApointments)
		{
			try
			{
				if (appointment.ArrivalType == AppointmentArrivalStatus.WrongLocation)
					return true;

				if (patientApointments == null)
					return false;

				var app = patientApointments.FirstOrDefault(a => a.SlotID == appointment.AppointmentId);
				int appSiteId = app != null ? app.SiteID : 0;
				if (appSiteId == 0)
					return false;

				bool isInWrongLocation =
					GlobalVariables.SelectedOrganisation.SiteId != null && appSiteId != GlobalVariables.SelectedOrganisation.SiteId;

				if (isInWrongLocation)
				{
					appointment.ArrivalType = AppointmentArrivalStatus.WrongLocation;
					string message = ActionType.WrongLocation.GetDisplayName();
					message = message + " - " + appointment.Name + " - " + appointment.Time + " - " + appSiteId;
					Logger.Instance.WriteLog(LogType.Info, message, null, KioskId);
				}

				return isInWrongLocation;
			}

			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return true;
			}
		}

		private void MatchAppointmentList()
		{
			try
			{
				if (_appointments.Any())
				{
					_appointments = _appointments.OrderBy(a => a.AppointmentTime).ToList();
					//Has to check how to set isuntimedAppointment to true for untimed(In time slot session)
					_appointments.ForEach(a => a.IsUnTimedAppointment = (a.AppointmentTime == DateTime.Today));
					MatchMemberData();

					_distinctPatientCount = _appointments.GroupBy(appointment => appointment.BookedPatient.Id).ToList().Count;
					if (_distinctPatientCount == 1)
					{
						AddAppointmentsToList(_appointments);
					}
					if (_distinctPatientCount > 1)
					{
						Logger.Instance.WriteLog(LogType.Info, ActionType.MultipleMatches.GetDisplayName(), null, KioskId);
					}
					else if (_isSynched && _distinctPatientCount == 0)
					{
						FillAppointmentListNone();
					}
				}
				else if (_isSynched)
				{
					FillAppointmentListNone();
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void MatchMemberData()
		{
			try
			{
				var memberList = _configRepository.GetKioskConfiguration<List<LinkedMember>>(KioskConfigType.LinkedMember.ToString());

				if (memberList != null)
				{
					memberList = memberList.Where(linkedMember => linkedMember.OrganisationId.ToString(CultureInfo.InvariantCulture).Equals(GlobalVariables.SelectedOrganisation.OrganisationId)).ToList();
					if (memberList.Count > 0)
					{
						List<Appointment> list = new List<Appointment>();
						if (GlobalVariables.SelectedOrganisation.SystemType.Equals(SystemType.TPPSystmOne.GetDisplayName()))
						{
							list = _appointments.Join(memberList, x => x.SessionHolder.Code, y => y.LoginId, (x, y) => new Appointment
							{
								Id = x.Id,
								AppointmentTime = x.AppointmentTime,
								SiteId = x.SiteId,
								SessionHolder = x.SessionHolder,
								BookedPatient = x.BookedPatient,
								TPPAppointmentID=x.TPPAppointmentID
							}).ToList();
						}
						else
						{
							list =(_appointments.Join(memberList, idAppointment => idAppointment.SessionHolder.Id, idMember => idMember.SessionHolderId, (idAppointment, idMember) => new Appointment
							{
								Id = idAppointment.Id,
								AppointmentTime = idAppointment.AppointmentTime,
								SiteId = idAppointment.SiteId,
								SessionHolder = idAppointment.SessionHolder,
								BookedPatient = idAppointment.BookedPatient
							})).ToList();
						}
						_appointments = list;
					}
					else
						_appointments = _appointments.ToList();
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
		}

		private void ResyncAppointmentDetails()
		{
			_isSynched = true;

			try
			{
				_appointments = _apiHelper.GetBookedPatients();
				if (_appointments.Any())
				{
					_appointments = _appointments.OrderBy(a => a.AppointmentTime).ToList();
					DecryptPatientData(_appointments);

					if (_appointments.Any(a => a.BookedPatient.Id < 1))
					{
						_isSynched = false;
					}

					_appointments = _appointmentRepository.GetMatchingAppointments(_appointments, _isSynched);

					if (_appointments == null)
						_appointments = new List<Appointment>();

					if (!_appointments.Any())
					{
						FillAppointmentListNone();
					}
				}
				else
				{
					FillAppointmentListNone();
				}
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				Messenger.Default.Send(AppPages.ExceptionDivert);
			}
		}

		private void AddAppointmentsToList(List<Appointment> appointments)
		{
			var kioskArrivalSettings = _configRepository.GetKioskConfiguration<KioskArrival>(KioskConfigType.KioskArrival.ToString());
			GlobalVariables.KioskArrival = kioskArrivalSettings;

			if (!kioskArrivalSettings.AllowUntimed)
			{
				appointments.RemoveAll(a => a.IsUnTimedAppointment);
			}

			foreach (Appointment appointment in appointments)
			{
				IDictionary<bool, AppointmentArrivalStatus> kioskArrival = new Dictionary<bool, AppointmentArrivalStatus>();
				if (!appointment.IsUnTimedAppointment)
				{
					kioskArrival = CompareArrivalTime(kioskArrivalSettings, appointment.AppointmentTime, appointment.Duration);
				}
				else
				{
					kioskArrival.Add(true, AppointmentArrivalStatus.OnTime);
				}

				AppointmentCollection.Add(new AppointmentDetail
				{
					DoctorId = appointment.SessionHolder.Id,
					SiteId = appointment.SiteId,
					AppointmentId = appointment.Id,
					Name = appointment.SessionHolder.DisplayName,
					Time = GetAppointmentTime(appointment.AppointmentTime),
					AppointmentDate = appointment.AppointmentTime,
					IsEnabled = kioskArrival.Count <= 1,
					ArrivalType = kioskArrival.Last().Value,
					DelayText = GetDelayText(appointment),
					ReceptionName = appointment.Reception,
					PatientId = appointment.BookedPatient.Id,
					TPPAppointmentId = appointment.TPPAppointmentID,
					PatientIdentifier = appointment.BookedPatient.PatientIdentifiers,
					DoctorUserName = appointment.SessionHolder.Code
				});
			}
		}

		private static string GetAppointmentTime(DateTime appointmentTime)
		{
			string appTime = appointmentTime.ToString("h:mm tt");
			return appTime != "12:00 AM" ? appTime : "Appointment";
		}

		private void FillAppointmentListNone()
		{
			_isPatientNotFound = true;
		}

		private IDictionary<bool, AppointmentArrivalStatus> CompareArrivalTime(KioskArrival kioskArrival,
			DateTime appointmentTime, int duration)
		{
			var result = new Dictionary<bool, AppointmentArrivalStatus>();

			try
			{
				result.Add(true, AppointmentArrivalStatus.OnTime);
				duration = duration > 75 ? duration : 0;
				int earlyArrival = (kioskArrival != null) ? kioskArrival.EarlyArrival : 0;
				int lateArrival = (kioskArrival != null) ? kioskArrival.LateArrival : 0;
				if (appointmentTime < DateTime.Now)
				{
					if (lateArrival > 0 && ((DateTime.Now - appointmentTime).TotalMinutes > lateArrival + duration))
						result.Add(false, AppointmentArrivalStatus.LateArrival);
				}
				else if (appointmentTime > DateTime.Now)
				{
					if (earlyArrival > 0 && ((appointmentTime - DateTime.Now).TotalMinutes > earlyArrival))
						result.Add(false, AppointmentArrivalStatus.EarlyArrival);
				}

				return result;
			}
			catch (Exception ex)
			{
				_exceptionFlag = true;
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return null;
			}
		}

		private static string GetDelayText(Appointment appointment)
		{
			if (GlobalVariables.KioskArrival != null && GlobalVariables.KioskArrival.ShowDoctorDelay)
			{
				return appointment.SessionHolder.WaitingTime > 0 ?
					Regex.Replace(GlobalVariables.SelectedLanguageIdText[LanguageText.RunningLateText], "##", appointment.SessionHolder.WaitingTime.ToString()) :
					GlobalVariables.SelectedLanguageIdText[LanguageText.OnTimeText];
			}
			else
			{
				return string.Empty;
			}
		}

		private static bool IsEmisWeb
		{
			get
			{
				return GlobalVariables.SelectedOrganisation != null &&
					   (GlobalVariables.SelectedOrganisation.SystemType.Equals("EMIS - Web"));
			}
		}

	}
}