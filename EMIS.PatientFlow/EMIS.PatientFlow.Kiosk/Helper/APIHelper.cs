using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Xml;
using EMIS.PatientFlow.API;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.API.Enums;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Model;
using Appointment = EMIS.PatientFlow.Kiosk.Model.Appointment;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public class ApiHelper
	{
		private readonly IConfigurationRepository _configRepository;
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly string _kioskId = Utilities.GetAppSettingValue("RegistrationKey");
		private OrganisationInformationUser _currentUser;
		private int _arrivedPatientDoctor = -1;

		public int ArrivedPatientDoctor
		{
			get
			{
				return _arrivedPatientDoctor > 0 ? _arrivedPatientDoctor : GetPatientDoctor();
			}
		}

		private int GetPatientDoctor()
		{
			_arrivedPatientDoctor = GlobalVariables.KioskArrival != null && (_currentUser != null && GlobalVariables.KioskArrival.QOFKioskUser) ? Convert.ToInt32(_currentUser.DBID) : GlobalVariables.ArrivedPatientDetails.SessionHolder.Id;
			return _arrivedPatientDoctor;
		}

		public ApiHelper()
		{
			_configRepository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
			_appointmentRepository = DiResolver.CurrentInstance.Reslove<IAppointmentRepository>();
		}

		public bool BookAppointment(API.Data.Appointment appointment)
		{
			try
			{
				Credential credential = _configRepository.GetPatientFlowUser();
				bool isSuccess = false;
				switch (credential.SystemType)
				{
					case SystemType.EmisWeb:
						isSuccess = BookAppointment(credential, appointment);
						break;
					case SystemType.EmisPcs:
						isSuccess = PcsBookAppointment(credential, appointment);
						break;
					case SystemType.TPPSystmOne:
						isSuccess = TPPBookAppointment(credential, appointment);
						break;
				}
				return isSuccess;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return false;
			}
		}

		private bool TPPBookAppointment(Credential credential, API.Data.Appointment appointment)
		{
			try
			{
				ApiResult<string> result;
				TPPHelper tppHelper = new TPPHelper();

				var startTime = appointment.SessionDate + " " + appointment.SlotStartTime;
				DateTime slotDateTime = DateTime.Parse(startTime);
				string bookAppRequest = tppHelper.TPPBookAppointment(appointment.PatientIdentifier, slotDateTime, appointment, credential);

				using (var client = new TppClient(credential))
				{
					result = client.BookAppointment(bookAppRequest);
					if (!result.IsSuccess)
					{
						string message =
								string.Format(
									"TppClient Method: {0};  Message: {1}; Parameters: {2} ",
									"BookAppointment",
									"Failed to book an appoinment",
									appointment.ConvertToJsonString());
						Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
					}
				}
				return result.IsSuccess;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return false;
			}
		}
		private bool PcsBookAppointment(Credential credential, API.Data.Appointment appointment)
		{
			using (var client = new PcsClient(credential))
			{
				ApiResult<Slot> apiResult = client.BookAppointment(appointment);
				if (!apiResult.IsSuccess)
				{
					string message = string.Format(
						"WebClient Method: {0};  Message: {1}; Parameters: {2} ",
						"BookAppointment",
						client.Message,
						appointment.ConvertToJsonString());
					Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
				}

				return apiResult.IsSuccess;
			}
		}

		private bool BookAppointment(Credential credential, API.Data.Appointment appointment)
		{
			using (var client = new WebClient(credential))
			{
				ApiResult<BookedAppointment> apiResult = client.BookAppointment(appointment);
				if (!apiResult.IsSuccess)
				{
					string message = string.Format(
						"WebClient Method: {0};  Message: {1}; Parameters: {2} ",
						"BookAppointment",
						client.Message,
						appointment.ConvertToJsonString());
					Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
				}

				return apiResult.IsSuccess;
			}
		}

		public List<DoctorDetailsBooking> GetAppointmentSessions(DateTime selectedDate, string slotType)
		{
			try
			{
				Credential credential = _configRepository.GetPatientFlowUser();
				List<DoctorDetailsBooking> doctorList = (GlobalVariables.SelectedOrganisation.SystemType == SystemType.EmisPcs.GetDisplayName()) ?
							  GetPcsAppointmentSessions(credential, selectedDate, slotType) :
							  GetEmisWebAppointmentSessions(credential, selectedDate, slotType);
				return doctorList;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return new List<DoctorDetailsBooking>();
			}
		}

		private List<DoctorDetailsBooking> GetEmisWebAppointmentSessions(Credential credential, DateTime selectedDate, string slotType)
		{
			using (var client = new WebClient(credential))
			{
				string date = Convert.ToString(selectedDate.Date);
				ApiResult<AppointmentSessionList> result = client.GetAppointmentSessions(date, date, slotType);
				if (result.IsSuccess)
				{
					var appointmentSessions = AddtoAppointmentSessionList(result.Result);
					_appointmentRepository.SaveAppointmentSessions(appointmentSessions, selectedDate);
					return _appointmentRepository.GetAppointmentSessions(selectedDate, slotType);
				}
				string message =
					string.Format(
						"WebClient Method: {0};  Message: {1}; Parameters: StartDate: {2}, EndDate: {3}",
						"GetAppointmentSessions",
						client.Message,
						Convert.ToString(selectedDate.Date),
						Convert.ToString(selectedDate.Date));
				Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
				return new List<DoctorDetailsBooking>();
			}
		}

		private static List<AppointmentSession> AddtoAppointmentSessionList(AppointmentSessionList appointmentsessionList)
		{
			return appointmentsessionList.AppointmentSession.
				Where(session => session.HolderList != null
				&& int.Parse(session.SlotLength) > 0
				&& GetAppAvaiable(session.SlotTypeList)
				&& GetSiteAppointment(session.Site)).
				Select(item => new AppointmentSession
				{
					SessionId = item.DBID,
					Date = item.Date,
					StartTime = Convert.ToString(item.StartTime),
					EndTime = Convert.ToString(item.EndTime),
					SlotLength = item.SlotLength,
					SiteId = item.Site.DBID,
					SiteName = item.Site.Name,
					AvaiableSlots = GetTotalAvaiable(item.SlotTypeList),
					SlotType = new AppointmentSlotType
					{
						SlotTypeId = item.SlotTypeList[0].TypeID,
						Description = item.SlotTypeList[0].Description,
					},
					Member = new Member
					{
						Id = item.HolderList[0].DBID,
						FirstName = item.HolderList[0].FirstNames,
						LastName = item.HolderList[0].LastName,
						Title = item.HolderList[0].Title,
					}
				}).ToList();
		}

		private static int GetTotalAvaiable(SlotsStruct[] slotTypeList)
		{
			if (slotTypeList == null) return 0;
			if (!slotTypeList.Any()) return 0;

			if (GlobalVariables.SelectedSlotType != null)
			{
				slotTypeList = slotTypeList.Where(a => a.TypeID == GlobalVariables.SelectedSlotType.SlotTypeId.ToString(CultureInfo.InvariantCulture)).ToArray();
			}

			return slotTypeList.Sum(slotsStruct => slotsStruct.Available);
		}

		private static int GetPcsTotalAvaiable(PCSAppointmentSessions.AppointmentSessionSlotTypeList[] slotTypeList)
		{
			if (slotTypeList == null) return 0;
			if (!slotTypeList.Any()) return 0;

			if (GlobalVariables.SelectedSlotType != null)
			{
				slotTypeList = slotTypeList.Where(a => a.TypeID == GlobalVariables.SelectedSlotType.SlotTypeId.ToString(CultureInfo.InvariantCulture)).ToArray();
			}

			return slotTypeList.Sum(slotsStruct => slotsStruct.Available);
		}

		private static bool GetSiteAppointment(SiteStruct site)
		{
			if (site == null)
				return false;

			return GlobalVariables.SelectedOrganisation != null && (GlobalVariables.SelectedOrganisation.SiteId == null ||
																	site.DBID == GlobalVariables.SelectedOrganisation.SiteId);
		}

		private static bool GetAppAvaiable(SlotsStruct[] slotTypeList)
		{
			if (slotTypeList == null) return false;
			if (!slotTypeList.Any()) return false;

			bool isAvaiable = false;

			if (GlobalVariables.SelectedSlotType != null)
			{
				slotTypeList = slotTypeList.Where(a => a.TypeID == GlobalVariables.SelectedSlotType.SlotTypeId.ToString(CultureInfo.InvariantCulture)).ToArray();
			}

			foreach (SlotsStruct slotsStruct in slotTypeList)
			{
				if (isAvaiable && slotsStruct.Available > 0)
					continue;
				if (!isAvaiable && slotsStruct.Available > 0)
					isAvaiable = true;
			}

			return isAvaiable;
		}

		private static bool GetPcsAppAvaiable(PCSAppointmentSessions.AppointmentSessionSlotTypeList[] slotTypeList)
		{
			if (slotTypeList == null) return false;
			if (!slotTypeList.Any()) return false;

			bool isAvaiable = false;

			if (GlobalVariables.SelectedSlotType != null)
			{
				slotTypeList = slotTypeList.Where(a => a.TypeID == GlobalVariables.SelectedSlotType.SlotTypeId.ToString(CultureInfo.InvariantCulture)).ToArray();
			}

			foreach (var slotsStruct in slotTypeList)
			{
				if (isAvaiable && slotsStruct.Available > 0)
					continue;
				if (!isAvaiable && slotsStruct.Available > 0)
					isAvaiable = true;
			}

			return isAvaiable;
		}

		private List<DoctorDetailsBooking> GetPcsAppointmentSessions(Credential credential, DateTime selectedDate, string slotType)
		{
			using (var client = new PcsClient(credential))
			{
				string date = Convert.ToString(selectedDate.Date);
				ApiResult<PCSAppointmentSessions.AppointmentSessionList> result = client.GetAppointmentSessions(date, date, slotType);
				if (result.IsSuccess)
				{
					List<AppointmentSession> appointmentSessions = AddtoAppointmentSessionList(result.Result);
					appointmentSessions = appointmentSessions.Where(session => int.Parse(session.SlotLength) != 0).ToList();
					_appointmentRepository.SaveAppointmentSessions(appointmentSessions, selectedDate);
					return _appointmentRepository.GetAppointmentSessions(selectedDate, slotType);
				}
				string message =
					string.Format(
						"WebClient Method: {0};  Message: {1}; Parameters: StartDate: {2}, EndDate: {3}",
						"GetAppointmentSessions",
						client.Message,
						Convert.ToString(selectedDate.Date),
						Convert.ToString(selectedDate.Date));
				Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
				return new List<DoctorDetailsBooking>();
			}
		}

		private List<AppointmentSession> AddtoAppointmentSessionList(PCSAppointmentSessions.AppointmentSessionList appointmentsessionList)
		{
			return appointmentsessionList.AppointmentSession
				.Where(session => session.HolderList != null && GetPcsAppAvaiable(session.SlotTypeList))
				.Select(
				item => new AppointmentSession
				{
					SessionId = Convert.ToInt32(item.DBID),
					Date = item.Date,
					StartTime = item.StartTime.TimeOfDay.ToString(),
					EndTime = item.EndTime.TimeOfDay.ToString(),
					SlotLength = GetSlotLength(item.SlotLength),
					AvaiableSlots = GetPcsTotalAvaiable(item.SlotTypeList),
					SiteId = (long)item.Site.DBID,
					SiteName = item.Site.Name,
					SlotType = GetPcsAppointmentSlotType(item),
					Member = (item.HolderList != null)
						? new Member
						{
							Id = Convert.ToInt32(item.HolderList.Holder.DBID),
							FirstName = item.HolderList.Holder.FirstNames,
							LastName = item.HolderList.Holder.LastName,
							Title = item.HolderList.Holder.Title,
						}
						: null
				}).ToList();
		}

		private static string GetSlotLength(string item)
		{
			TimeSpan dt;
			return TimeSpan.TryParse(item, out dt) ? dt.Minutes.ToString() : "0";
		}

		private static AppointmentSlotType GetPcsAppointmentSlotType(PCSAppointmentSessions.AppointmentSessionListAppointmentSession item)
		{
			if (item.SlotTypeList == null) return null;

			if (GlobalVariables.SelectedSlotType == null)
				return new AppointmentSlotType
				{
					SlotTypeId = item.SlotTypeList[0].TypeID,
					Description = item.SlotTypeList[0].Description,
				};

			PCSAppointmentSessions.AppointmentSessionSlotTypeList appointmentSessionSlotTypeList = item.SlotTypeList.First(a => a.TypeID == GlobalVariables.SelectedSlotType.SlotTypeId);
			return appointmentSessionSlotTypeList != null
				? new AppointmentSlotType
				{
					SlotTypeId = appointmentSessionSlotTypeList.TypeID,
					Description = appointmentSessionSlotTypeList.Description
				}
				: new AppointmentSlotType
				{
					SlotTypeId = item.SlotTypeList[0].TypeID,
					Description = item.SlotTypeList[0].Description,
				};
		}

		public AppointmentSlots GetFirstAppointmentSlots(DateTime date)
		{
			DateTime endDateCheck = DateTime.Today.AddDays(21);
			date = DateTime.Now.Date;
			while (true)
			{
				GlobalVariables.DoctorDetailsBooking = new List<DoctorDetailsBooking>();
				string slotType = (GlobalVariables.SelectedSlotType == null) ? string.Empty : GlobalVariables.SelectedSlotType.SlotTypeId;
				var credential = _configRepository.GetPatientFlowUser();
				DateTime endDate = date.AddDays(11);
				if (credential.SystemType == SystemType.TPPSystmOne)
				{
					AppointmentSlots appointmentSlots = new AppointmentSlots();
					appointmentSlots = GetAppointmentSlotForTpp(credential, date, endDate);
					if (appointmentSlots == null)
					{
						date = endDate.AddDays(1);
						if (date > endDateCheck)
							return null;
						continue;
					}
					return appointmentSlots;
				}
				else
				{
					var allSessionHolderList = new List<AppointmentSession>();
					allSessionHolderList = GetAllSessionHolderList(credential, date, endDate, slotType);
					if (allSessionHolderList == null || allSessionHolderList.Count == 0)
					{
						date = endDate.AddDays(1);
						if (date > endDateCheck)
							return null;

						continue;
					}
					var doctorsList = new List<AppointmentSession>();
					if (string.IsNullOrEmpty(GlobalVariables.BookAppointmentSettings.SelectedMemberList))
						doctorsList = allSessionHolderList;
					else
					{
						var memberList = GlobalVariables.BookAppointmentSettings.SelectedMemberList.Split(',').Select(int.Parse).ToList();

						doctorsList.AddRange(memberList.SelectMany(id => allSessionHolderList.Where(session => session.Member.Id == id)));
					}

					doctorsList = MatchSiteData(doctorsList);
					doctorsList = MatchMemberData(doctorsList);

					if (doctorsList != null)
					{
						var datetime = new List<DateTime>();
						foreach (AppointmentSession session in doctorsList.Where(session => !datetime.Contains(DateTime.Parse(session.Date))))
						{
							datetime.Add(DateTime.Parse(session.Date));
						}

						if (datetime.Count > 0)
						{
							foreach (var appDate in datetime)
							{
								List<int> sessionid = doctorsList.Where(a => DateTime.Parse(a.Date) == appDate).Select(session => session.SessionId).ToList();
								GlobalVariables.Appointment.SessionDate = String.Format("{0:ddd dd MMMM yyyy}", appDate);
								var slots = GetSlotForSessions(sessionid);

								if (!slots.Any() && slots.Exists(a => a.SlotStatus))
									continue;

								slots = slots.Where(a => a.SlotStatus).ToList();
								slots = slots.OrderBy(a => DateTime.Parse(a.StartTime).TimeOfDay).ToList();
								foreach (AppointmentSlots slot in slots)
								{
									var session = doctorsList.FirstOrDefault(a => a.SessionId == slot.SessionId);
									if (session != null)
									{
										var doctorDetailsBooking = new DoctorDetailsBooking
										{
											DoctorId = session.Member.Id,
											DoctorName = session.Member.DisplayName,
											DoctorNameToDisplay = session.Member.DisplayName
										};

										GlobalVariables.DoctorDetailsBooking.Add(doctorDetailsBooking);
										GlobalVariables.Appointment.SiteName = session.SiteName;
										GlobalVariables.Appointment.DoctorId = session.Member.Id;
										GlobalVariables.Appointment.SessionDate = String.Format("{0:ddd dd MMMM yyyy}", DateTime.Parse(session.Date));
										return slot;
									}
								}
							}
						}
					}
				}
				date = endDate.AddDays(1);
				if (date > endDateCheck)
					return null;
			}
		}

		internal List<AppointmentSession> MatchSiteData(List<AppointmentSession> doctorsList)
		{
			return GlobalVariables.SelectedOrganisation.SiteId != null ?
						doctorsList.Where(x => x.SiteId == GlobalVariables.SelectedOrganisation.SiteId).ToList() :
						doctorsList;
		}
		internal AppointmentSlots GetAppointmentSlotForTpp(Credential credential, DateTime selectedDate, DateTime endDate)
		{
			using (var client = new TppClient(credential))
			{
				List<AppointmentSlots> appointmentSlots = new List<AppointmentSlots>();
				var results = client.GetAppointmentSlotsList(selectedDate, endDate);
				var tppSlots = new List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic>();
				tppSlots = results.ToList();
				tppSlots = GetMatchTppSite(tppSlots);
				tppSlots = GetMatchMember(tppSlots);
				GlobalVariables.Appointment.MemberId = 10000;

				appointmentSlots.AddRange(tppSlots.Where(x => !string.IsNullOrEmpty(x.UserName)).Select(item => new AppointmentSlots()
				{
					SiteName = item.SiteName.ToString(),
					StartDateTime = item.Slot.Where(a => DateTime.Now < DateTime.Parse(a.DateTimeStart)).Select(x => x.DateTimeStart).FirstOrDefault(),
					SlotLength = Convert.ToInt32(item.Slot.Select(x => x.Duration).FirstOrDefault()),
					SlotTypeDescription = item.Slot.Select(x => x.SlotType).FirstOrDefault(),
					DoctorName = item.UserName,
					ClinicType = item.ClinicType,
					UserName = item.UserName
				}));
				var data = appointmentSlots.Where(x => x.StartTime != null).OrderBy(x => x.StartDateTime).FirstOrDefault();
				if (data != null)
				{
					var organisation = client.GetOrganisation();
					string doctorname = organisation[0].User.Where(x => x.UserName == data.UserName).Select(x => x.FirstName + " " + x.Surname).FirstOrDefault();
					GlobalVariables.Appointment.DoctorId = GlobalVariables.Appointment.MemberId;
					var doctorDetailsBooking = new DoctorDetailsBooking()
					{
						DoctorId = GlobalVariables.Appointment.MemberId++,
						DoctorName = !string.IsNullOrEmpty(doctorname) ? doctorname : data.UserName,
						DoctorNameToDisplay=doctorname
					};
					GlobalVariables.DoctorDetailsBooking.Add(doctorDetailsBooking);
					GlobalVariables.Appointment.SiteName = data.SiteName.ToString();
					GlobalVariables.Appointment.SessionDate = String.Format("{0:ddd dd MMMM yyyy}", DateTime.Parse(data.StartDateTime));
					GlobalVariables.Appointment.ClinicType = data.ClinicType.ToString();
					GlobalVariables.Appointment.Duration = data.SlotLength;
					GlobalVariables.Appointment.UserName = data.UserName;
				}
				return data;

			}

		}
		internal List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> GetMatchTppSite(List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> tppSlots)
		{
			var siteName = GlobalVariables.SelectedOrganisation.SiteName;
			return !string.IsNullOrEmpty(siteName) ? tppSlots.Where(x => x.SiteName == siteName).ToList() : tppSlots;
		}
		internal List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> GetMatchMember(List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> tppSlots)
		{
			List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic> tppSlotList = new List<GetAppointmentSlots.ClientIntegrationResponseResponseClinic>();
			try
			{
				var memberList = _configRepository.GetKioskConfiguration<List<LinkedMember>>(KioskConfigType.LinkedMember.ToString());
				if (memberList != null && memberList.Count > 0)
				{
					memberList = memberList.Where(linkedMember => linkedMember.OrganisationId.ToString().Equals(
							GlobalVariables.SelectedOrganisation.OrganisationId)).ToList();
					foreach (var item in memberList)
					{
						tppSlotList.AddRange(tppSlots.Where(x => x.UserName == item.LoginId));
					}
					tppSlots = tppSlotList;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
			}
			return tppSlots;

		}
		internal List<AppointmentSession> GetAllSessionHolderList(Credential credential, DateTime selectedDate, DateTime endDate, string slotType)
		{
			string date = Convert.ToString(selectedDate.Date);
			string lastDate = Convert.ToString(endDate.Date);
			var appointmentSessions = new List<AppointmentSession>();

			if (credential.SystemType == SystemType.EmisPcs)
			{
				using (var client = new PcsClient(credential))
				{
					ApiResult<PCSAppointmentSessions.AppointmentSessionList> result = client.GetAppointmentSessions(date, lastDate, slotType);
					if (!result.IsSuccess)
						return appointmentSessions;
					appointmentSessions = AddtoAppointmentSessionList(result.Result);
					appointmentSessions = appointmentSessions.Where(session => int.Parse(session.SlotLength) != 0).ToList();
				}
			}
			else
			{
				using (var client = new WebClient(credential))
				{
					ApiResult<AppointmentSessionList> result = client.GetAppointmentSessions(date, lastDate, slotType);
					if (!result.IsSuccess)
						return appointmentSessions;
					appointmentSessions = AddtoAppointmentSessionList(result.Result);
				}
			}

			return appointmentSessions.OrderBy(a => DateTime.Parse(a.Date)).ToList();
		}

		internal List<AppointmentSession> MatchMemberData(List<AppointmentSession> doctorsList)
		{
			List<AppointmentSession> doctorDetailsList;
			try
			{
				var memberList = _configRepository.GetKioskConfiguration<List<LinkedMember>>(KioskConfigType.LinkedMember.ToString());
				if (memberList != null && memberList.Count > 0)
				{
					memberList = memberList.Where(linkedMember => linkedMember.OrganisationId.ToString().Equals(
						GlobalVariables.SelectedOrganisation.OrganisationId)).ToList();


					if (memberList.Count > 0)
					{
						foreach (var member in memberList)
						{
							LinkedMember member1 = member;
							foreach (var session in doctorsList.Where(session => session.Member.Id != member1.SessionHolderId))
							{
								doctorsList.Remove(session);
							}
						}
					}
					doctorDetailsList = doctorsList;
				}
				else
					doctorDetailsList = doctorsList;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return doctorsList;
			}

			return doctorDetailsList;
		}

		public List<AppointmentSlots> GetSlotForSessions(List<int> sessionIDs)
		{
			try
			{
				Credential credential = _configRepository.GetPatientFlowUser();

				List<AppointmentSlots> appointmentSlots =
					(GlobalVariables.SelectedOrganisation.SystemType == SystemType.EmisPcs.GetDisplayName()) ?
									GetPcsSlotsForSession(credential, sessionIDs) :
									GetSlotsForSession(credential, sessionIDs);

				if (GlobalVariables.SelectedSlotType != null)
				{
					return appointmentSlots.Where(a => a.SlotTypeId == GlobalVariables.SelectedSlotType.SlotTypeId).ToList();
				}

				return appointmentSlots;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return new List<AppointmentSlots>();
			}
		}

		private List<AppointmentSlots> GetSlotsForSession(Credential credential, List<int> sessionIDs)
		{
			using (var client = new WebClient(credential))
			{
				var appointmentSlots = new List<AppointmentSlots>();

				foreach (int sessionId in sessionIDs)
				{
					ApiResult<SlotList> result = client.GetSlotForSessions(sessionId);

					if (result.IsSuccess)
					{
						if (result.Result != null)
						{
							var slotListSlots = new List<SlotListSlot>(result.Result.Slot);
							if (GlobalVariables.Appointment.SessionDate == String.Format("{0:ddd dd MMMM yyyy}", DateTime.Today))
							{
								slotListSlots = slotListSlots.Where(a => TimeSpan.Parse(a.StartTime) > DateTime.Now.TimeOfDay).ToList();
							}
							slotListSlots = slotListSlots.Where(a => a.Status == "Slot Available").ToList();
							appointmentSlots.AddRange(slotListSlots.OrderBy(a => a.StartTime).Select(item => new AppointmentSlots
							{
								SlotId = item.DBID,
								SlotStatus = item.Status == "Slot Available",
								StartTime = item.StartTime,
								SlotLength = item.SlotLength,
								SlotTypeId = Convert.ToString(item.Type.TypeID),
								SlotTypeDescription = item.Type.Description,
								SessionId = sessionId
							}));
						}
					}
					else
					{
						string message =
							string.Format(
								"WebClient Method: {0};  Message: {1}; Parameters: AppointmentSessionId: {2};",
								"GetSlotForSessions",
								client.Message,
								sessionIDs[0]);
						Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
						return new List<AppointmentSlots>();
					}
				}
				appointmentSlots = appointmentSlots.OrderBy(a => DateTime.Parse(a.StartTime).TimeOfDay).ToList();
				return appointmentSlots;
			}
		}

		private List<AppointmentSlots> GetPcsSlotsForSession(Credential credential, List<int> sessionIDs)
		{
			using (var client = new PcsClient(credential))
			{
				var appointmentSlots = new List<AppointmentSlots>();

				foreach (int sessionId in sessionIDs)
				{
					ApiResult<PCSSlotList.SlotList> result;
					try
					{
						result = client.GetSlotForSessions(sessionId);

						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								var slotListSlots = new List<PCSSlotList.SlotListSlot>(result.Result.Slot);
								if (GlobalVariables.Appointment.SessionDate == String.Format("{0:ddd dd MMMM yyyy}", DateTime.Today))
								{
									slotListSlots = slotListSlots.Where(a => a.StartTime.TimeOfDay > DateTime.Now.TimeOfDay).ToList();
								}

								appointmentSlots.AddRange(slotListSlots.OrderBy(a => a.StartTime).Select(item => new AppointmentSlots
								{
									SlotId = (int)item.DBID,
									SlotStatus = item.Status == "Slot Available" || item.Status == "Available",
									StartTime = item.StartTime.ToString("HH:mm").ToUpper().Trim('.'),
									SlotLength = item.SlotLength.Minute,
									SlotTypeId = item.Type.TypeID,
									SlotTypeDescription = item.Type.Description,
									SessionId = sessionId
								}));
							}
						}
						else
						{
							string message =
								string.Format(
									"WebClient Method: {0};  Message: {1}; Parameters: AppointmentSessionId: {2};",
									"GetSlotForSessions",
									client.Message,
									sessionIDs[0]);
							Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
							return null;
						}
					}
					catch (Exception ex)
					{
						Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
						continue;
					}
				}
				appointmentSlots = appointmentSlots.OrderBy(a => DateTime.Parse(a.StartTime).TimeOfDay).ToList();
				return appointmentSlots;
			}
		}

		public void GetMatchedPatients(string filter)
		{
			try
			{
				Credential credential = _configRepository.GetPatientFlowUser();
				if (credential != null)
				{
					if (credential.SystemType == SystemType.EmisPcs)
					{
						using (var client = new PcsClient(credential))
						{
							ApiResult<PatientMatches> result = client.GetMatchedPatients(filter);
							GlobalVariables.PatientMatches = GetPatientResult(result);
						}
					}
					else if (credential.SystemType == SystemType.EmisWeb)
					{
						using (var client = new WebClient(credential))
						{
							ApiResult<PatientMatches> result = client.GetMatchedPatients(filter);
							GlobalVariables.PatientMatches = GetPatientResult(result);
						}
					}
					else if (credential.SystemType == SystemType.TPPSystmOne)
					{
						using (TppClient client = new TppClient(credential))
						{
							var patientMatches = client.GetPatientRecord(filter, GlobalVariables.PatientMatchGender);
							GlobalVariables.PatientMatches = patientMatches;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
			}
		}

		private static PatientMatches GetPatientResult(ApiResult<PatientMatches> result)
		{
			PatientMatches result1 = new PatientMatches();
			List<string> DisallowedStatus = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "19" };  // This vaue maybe we can bring from Config file
			result1.PatientList = result.Result.PatientList.Where(a => DisallowedStatus.Contains(a.CurrentStatus.Code.Value)).ToArray();
			return result1;
		}

		public PatientMatches GetUntimedMatchedPatients(string filter)
		{
			PatientMatches patientMatches = new PatientMatches();
			try
			{
				Credential credential = _configRepository.GetPatientFlowUser();
				if (credential != null)
				{
					if (credential.SystemType == SystemType.EmisPcs)
					{
						using (var client = new PcsClient(credential))
						{
							ApiResult<PatientMatches> result = client.GetMatchedPatients(filter);
							patientMatches = result.Result;
						}
					}
					else
					{
						using (var client = new WebClient(credential))
						{
							ApiResult<PatientMatches> result = client.GetMatchedPatients(filter);
							patientMatches = result.Result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
			}
			return patientMatches;
		}

		public string SetAppointmentStatus(int appointmentId)
		{
			Credential credential = _configRepository.GetPatientFlowUser();

			switch (credential.SystemType)
			{
				case SystemType.EmisWeb:
					return SetAppointmentStatusForEmisWeb(credential, appointmentId);
				case SystemType.EmisPcs:
					return SetAppointmentStatusForEmisPcs(credential, appointmentId);
				default:
					throw new NotImplementedException("Operation not implemented for the system type: " + credential.SystemType);
			}
		}

		public string SetTPPAppointmentStatus(string TPPAppointmentId, string PatientNHSNumber)
		{
			Credential credential = _configRepository.GetPatientFlowUser();
			return SetAppointmentStatusForTPP(credential, TPPAppointmentId, PatientNHSNumber);
		}

		private string SetAppointmentStatusForTPP(Credential credential, string TPPAppointmentId, string PatientNHSNumber)
		{
			using (var client = new TppClient(credential))
			{
				try
				{
					var result = client.UpdatePatientAppointmentStatus(PatientNHSNumber, new string[] { TPPAppointmentId });

					XmlDocument xdoc = new XmlDocument();
					xdoc.LoadXml(result);

					var errorMessage = ValidateElementName(xdoc, "ErrorMessage");
					if (!string.IsNullOrWhiteSpace(errorMessage))
					{
						string message =
							string.Format(
								"TppClient Method: {0};  Message: {1}; Parameters: AppointmentID: {2}, AppointmentStatus: {3};",
								"SetAppointmentStatus",
								errorMessage,
								TPPAppointmentId,
								AppointmentStatus.Arrived);
						Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
						return "Failure:" + errorMessage;
					}

					return "Success"; // Get client message and set how it goes
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
					return string.Empty;
				}
			}
		}

		private static string ValidateElementName(XmlDocument xdoc, string elementName)
		{
			if (xdoc.GetElementsByTagName(elementName).Count > 0)
			{
				return xdoc.GetElementsByTagName(elementName)[0].InnerText;
			}

			return "";
		}

		private string SetAppointmentStatusForEmisPcs(Credential credential, int appointmentId)
		{
			using (var client = new PcsClient(credential))
			{
				try
				{
					ApiResult<string> result = client.SetAppointmentStatus(appointmentId, AppointmentStatus.Arrived);
					if (!result.IsSuccess)
					{
						string message =
							string.Format(
								"WebClient Method: {0};  Message: {1}; Parameters: AppointmentID: {2}, AppointmentStatus: {3};",
								"SetAppointmentStatus",
								client.Message,
								appointmentId,
								AppointmentStatus.Arrived);
						Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
					}

					return client.Message;
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
					return string.Empty;
				}
			}
		}

		private string SetAppointmentStatusForEmisWeb(Credential credential, int appointmentId)
		{
			using (var client = new WebClient(credential))
			{
				try
				{
					ApiResult<string> result = client.SetAppointmentStatus(appointmentId, AppointmentStatus.Arrived);
					if (!result.IsSuccess)
					{
						string message =
							string.Format(
								"WebClient Method: {0};  Message: {1}; Parameters: AppointmentID: {2}, AppointmentStatus: {3};",
								"SetAppointmentStatus",
								client.Message,
								appointmentId,
								AppointmentStatus.Arrived);
						Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
					}

					return client.Message;
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
					return string.Empty;
				}
			}
		}

		public List<Model.Appointment> GetBookedPatients()
		{
			var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
			DateTime endDate = startDate.AddDays(1).AddMinutes(-1);
			var appointments = new List<Model.Appointment>();

			Credential credential = _configRepository.GetPatientFlowUser();
			if (credential != null)
			{
				switch (credential.SystemType)
				{
					case SystemType.EmisPcs:
						appointments = GetEmisPCSBookedAppointments(credential, startDate, endDate);
						break;
					case SystemType.EmisWeb:
						appointments = GetEmisWebBookedAppointments(credential, startDate, endDate);
						break;
					case SystemType.TPPSystmOne:
						appointments = GetTPPBookedAppointments(credential, startDate, endDate);
						break;
					default:
						throw new NotImplementedException("Operation not implemented for the system type: " + credential.SystemType);
				}
			}
			return appointments;
		}

		public List<Appointment> SaveUntimedAppointments(
			PCSPatientsAppointmentList.PatientAppointmentList patientAppointments,
			PatientMatchesPatient patient)
		{
			var startDate = DateTime.UtcNow.Date;

			List<Appointment> appointments = new List<Appointment>();
			appointments = AddUntimedApptToAppointmentList(patientAppointments, patient);
			_appointmentRepository.SaveAppointments(appointments, startDate, SystemType.EmisPcs, false, true);
			return appointments;
		}

		public List<Appointment> SaveUntimedAppointments(
		EMISWebPA.PatientAppointmentList patientAppointments,
		PatientMatchesPatient patient)
		{
			var startDate = DateTime.UtcNow.Date;

			List<Appointment> appointments = new List<Appointment>();
			appointments = AddEmisUntimedApptToAppointmentList(patientAppointments, patient);
			_appointmentRepository.SaveAppointments(appointments, startDate, SystemType.EmisWeb, false, true);
			return appointments;
		}

		private List<Appointment> AddEmisUntimedApptToAppointmentList(EMISWebPA.PatientAppointmentList patientAppointments, PatientMatchesPatient patient)
		{
			var appointments = new List<Appointment>();

			foreach (var appt in patientAppointments.Appointment)
			{
				try
				{
					DateTime dt;
					var sessionHolder = appt.HolderList.FirstOrDefault();
					if (sessionHolder == null)
					{
						sessionHolder = new EMISWebPA.HolderStruct
						{
							DBID = 1,
							Title = "Dr",
							FirstNames = "Unknown",
							LastName = "Clinician",
							//WaitingTime = "0"
						};
					}

					appointments.Add(new Appointment()
					{
						//Start time will be null...Has to write conditiin for that
						AppointmentTime = DateTime.TryParseExact(
							appt.Date + " " + appt.StartTime,
							"dd/MM/yyyy HH:mm",
							CultureInfo.InvariantCulture,
							DateTimeStyles.None,
							out dt)
							? dt
							: Convert.ToDateTime(appt.Date + " " + appt.StartTime),
						Id = Convert.ToInt32(appt.SlotID),
						SiteId = GetSiteId(appt.SiteID.ToString()),
						BookedPatient = new Patient()
						{
							Gender = patient.Sex.EncryptAES256(),
							Id = Convert.ToInt32(patient.DBID),
							Title = patient.Title.EncryptAES256(),
							CallingName = patient.CallingName.EncryptAES256(),
							FirstName = patient.FirstNames.EncryptAES256(),
							FamilyName = patient.FamilyName.EncryptAES256(),
							Email = patient.Email.EncryptAES256(),
							Dob = DateTime.TryParseExact(
								patient.DateOfBirth,
								"dd/MM/yyyy",
								CultureInfo.InvariantCulture,
								DateTimeStyles.None,
								out dt)
								? patient.DateOfBirth.EncryptAES256()
								: null,
							Mobile = patient.Mobile.EncryptAES256(),
							HomeTelephone = patient.HomeTelephone.EncryptAES256(),
							WorkTelephone = patient.WorkTelephone.EncryptAES256(),
							PostCode = patient.Address.PostCode.EncryptAES256(),
							PatientSystemType = SystemType.EmisPcs
						},
						SessionHolder = new Member()
						{
							FirstName = sessionHolder.FirstNames,
							Id = Convert.ToInt32(sessionHolder.DBID),
							LastName = sessionHolder.LastName,
							Title = sessionHolder.Title,
							//WaitingTime = Convert.ToInt32(sessionHolder.WaitingTime)
						},
						Reception = null,
						Duration = int.Parse(appt.Duration),
						IsUnTimedAppointment = (appt.StartTime == "00:00" || appt.StartTime == "12:00 AM") //Has to update it for untimed(Timed slots)
					});
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, "Failed while saving PCS Appointments for patient: " + patient.DBID, ex, _kioskId);
				}
			}

			return appointments;
		}

		private List<Appointment> AddUntimedApptToAppointmentList(PCSPatientsAppointmentList.PatientAppointmentList patientAppointments, PatientMatchesPatient patient)
		{
			var appointments = new List<Appointment>();

			foreach (var appt in patientAppointments.Appointment)
			{
				try
				{
					DateTime dt;
					var sessionHolder = (appt.HolderList == null) ? GetDefaultSessionHolder() : appt.HolderList.FirstOrDefault();
					if (sessionHolder == null)
					{
						sessionHolder = GetDefaultSessionHolder();
					}

					if (string.IsNullOrWhiteSpace(appt.StartTime))
						appt.StartTime = "00:00";

					appointments.Add(new Appointment()
					{
						//Start time will be null...Has to write conditiin for that
						AppointmentTime = DateTime.TryParseExact(
							appt.Date + " " + appt.StartTime,
							"dd/MM/yyyy HH:mm",
							CultureInfo.InvariantCulture,
							DateTimeStyles.None,
							out dt)
							? dt
							: Convert.ToDateTime(appt.Date + " " + appt.StartTime),
						Id = Convert.ToInt32(appt.SlotID),
						SiteId = GetSiteId(appt.SiteID.ToString()),
						BookedPatient = new Patient()
						{
							Gender = patient.Sex.EncryptAES256(),
							Id = Convert.ToInt32(patient.DBID),
							Title = patient.Title.EncryptAES256(),
							CallingName = patient.CallingName.EncryptAES256(),
							FirstName = patient.FirstNames.EncryptAES256(),
							FamilyName = patient.FamilyName.EncryptAES256(),
							Email = patient.Email.EncryptAES256(),
							Dob = DateTime.TryParseExact(
								patient.DateOfBirth,
								"dd/MM/yyyy",
								CultureInfo.InvariantCulture,
								DateTimeStyles.None,
								out dt)
								? patient.DateOfBirth.EncryptAES256()
								: null,
							Mobile = patient.Mobile.EncryptAES256(),
							HomeTelephone = patient.HomeTelephone.EncryptAES256(),
							WorkTelephone = patient.WorkTelephone.EncryptAES256(),
							PostCode = patient.Address.PostCode.EncryptAES256(),
							PatientSystemType = SystemType.EmisPcs
						},
						SessionHolder = new Member()
						{
							FirstName = sessionHolder.FirstNames,
							Id = Convert.ToInt32(sessionHolder.DBID),
							LastName = sessionHolder.LastName,
							Title = sessionHolder.Title,
							//WaitingTime = Convert.ToInt32(sessionHolder.WaitingTime)
						},
						Reception = null,
						Duration = int.Parse(TimeSpan.Parse(appt.Duration).TotalMinutes.ToString()),
						IsUnTimedAppointment = true //Has to update this condition for PCS..same as EMIS-WEB
					});
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, "Failed while saving PCS Appointments for patient: " +
						patient.DBID, ex, _kioskId);
				}
			}

			return appointments;
		}

		private PCSPatientsAppointmentList.HolderStruct GetDefaultSessionHolder()
		{
			return new PCSPatientsAppointmentList.HolderStruct
			{
				DBID = 1,
				Title = "Dr",
				FirstNames = "Unknown",
				LastName = "Clinician",
				//WaitingTime = "0"
			};
		}

		private List<Model.Appointment> GetEmisWebBookedAppointments(Credential credential, DateTime startDate, DateTime endDate)
		{
			var appointments = new List<Model.Appointment>();

			try
			{
				using (var client = new WebClient(credential))
				{
					var within = (int)(endDate - DateTime.UtcNow).TotalMinutes;
					var before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
					before = (GlobalVariables.SelectedOrganisation.SystemType == SystemType.EmisWeb.GetDisplayName()) ? (before < 300 ? before : 300) : (before < 30 ? before : 30);

					ApiResult<BookedPatients> bookedPatients = client.GetBookedPatients(within, before);

					if (bookedPatients.IsSuccess)
					{
						appointments = AddtoAppointmentList(bookedPatients);
						_appointmentRepository.SaveAppointments(appointments, startDate, SystemType.EmisWeb, false, false);
					}
					else
					{
						string message =
							string.Format(
								"WebClient Method: {0};  Message: {1}; Parameters: Within: {2}, Before: {3}",
								"GetBookedPatients",
								client.Message,
								within,
								before);
						Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
					}
				}

				return appointments;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Info, ex.Message, null, _kioskId);
				return new List<Model.Appointment>();
			}
		}

		private List<Model.Appointment> GetEmisPCSBookedAppointments(Credential credential, DateTime startDate, DateTime endDate)
		{
			var appointments = new List<Model.Appointment>();

			try
			{
				using (var client = new PcsClient(credential))
				{
					var within = (int)(endDate - DateTime.UtcNow).TotalMinutes;
					var before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
					before = before < 30 ? before : 30;

					ApiResult<BookedPatients> bookedPatients = client.GetBookedPatients(within, before);

					if (bookedPatients.IsSuccess)
					{
						appointments = AddtoAppointmentList(bookedPatients);
						_appointmentRepository.SaveAppointments(appointments, startDate, SystemType.EmisWeb, false, false);
					}
					else
					{
						string message =
							string.Format(
								"WebClient Method: {0};  Message: {1}; Parameters: Within: {2}, Before: {3}",
								"GetBookedPatients",
								client.Message,
								within,
								before);
						Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
					}
				}

				return appointments;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Info, ex.Message, null, _kioskId);
				return new List<Model.Appointment>();
			}
		}

		private List<Model.Appointment> GetTPPBookedAppointments(Credential credential, DateTime startDate, DateTime endDate)
		{
			var appointments = new List<Model.Appointment>();

			try
			{
				using (var client = new TppClient(credential))
				{
					var within = (int)(endDate - DateTime.UtcNow).TotalMinutes;
					var before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
					before = before < 30 ? before : 30;

					var response = client.GetTodaysAppointments();
					AddtoTPPAppointmentList(client, appointments, response);
					if (appointments.Any())
					{
						_appointmentRepository.SaveTPPAppointments(appointments, startDate, SystemType.TPPSystmOne, false, false);
					}
					else
					{
						string message =
							string.Format(
								"TppClient Method: {0};  Message: {1}; Parameters: Within: {2}, Before: {3}",
								"GetBookedPatients",
								"No Appointments found", // Has to see and keep updated value
								within,
								before);
						Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
					}
				}

				return appointments;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Info, ex.Message, null, _kioskId);
				return new List<Model.Appointment>();
			}
		}

		private static void AddtoTPPAppointmentList(TppClient client, List<Appointment> appointments, GetDairy.ClientIntegrationResponseResponseResult[] response)
		{
			int appointmentId = 100000;
			int patientIdentifier = 50000;
			int memberId = 10000;
			TPPHelper helper = new TPPHelper();

			foreach (GetDairy.ClientIntegrationResponseResponseResult responseResult in response)
			{
				if (responseResult.Appointment.Any(a => a.Status == "Booked"))
				{

					var nhsstring = responseResult.Identity.Select(a => a.NHSNumber).ToList();
					List<GetPatientRecordDetails.ClientIntegrationResponseResponse> detailsList = helper.GetPatientRecordDetails(client, nhsstring);

					foreach (var appointment in responseResult.Appointment.Where(a => a.Status == "Booked"))
					{
						var demoList = detailsList.FirstOrDefault(a => a.Identity[0].NHSNumber == responseResult.Identity[0].NHSNumber).Demographics[0];
						appointments.Add(new Model.Appointment()
						{
							Id = appointmentId++,
							AppointmentTime = Convert.ToDateTime(appointment.Clinic[0].Slot[0].DateTimeStart),
							TPPAppointmentID = appointment.AppointmentUID,
							BookedPatient = new Patient()
							{
								Gender = responseResult.Demographics[0].Sex.EncryptAES256(),
								Id = patientIdentifier++,
								Title = responseResult.Demographics[0].Title.EncryptAES256(),
								FirstName = responseResult.Demographics[0].FirstName.EncryptAES256(),
								CallingName = responseResult.Demographics[0].MiddleNames.EncryptAES256(),
								Dob = DateTime.Parse(responseResult.Demographics[0].DateOfBirth).ToString().EncryptAES256(),
								FamilyName = responseResult.Demographics[0].Surname.EncryptAES256(),
								PatientIdentifiers = responseResult.Identity[0].NHSNumber.EncryptAES256(),
								PostCode = demoList != null ? demoList.Address[0].Postcode.EncryptAES256() : null,
								Email = demoList != null ? demoList.EmailAddress.EncryptAES256() : null,
								Mobile = demoList != null ? demoList.MobileTelephone.EncryptAES256() : null,
								HomeTelephone = demoList != null ? demoList.HomeTelephone.EncryptAES256() : null,
								WorkTelephone = demoList != null ? demoList.WorkTelephone.EncryptAES256() : null,
								PatientSystemType = SystemType.TPPSystmOne,
							},
							SessionHolder = new Member()
							{
								Id = memberId++,
								WaitingTime = 0,
								MemberIdentifiers = appointment.Clinic[0].UserName,
								FirstName = appointment.Clinic[0].UserName,
							},
							Duration = string.IsNullOrWhiteSpace(appointment.Clinic[0].Slot[0].Duration) ? 0 : Convert.ToInt32(appointment.Clinic[0].Slot[0].Duration),
							Reception = appointment.AppointmentUID,
						});
					}
				}
			}
		}

		private List<Model.Appointment> AddtoAppointmentList(ApiResult<BookedPatients> bookedPatients)
		{
			var appointments = new List<Model.Appointment>();
			if (bookedPatients.Result.SessionHolderList != null)
			{
				foreach (var item in bookedPatients.Result.BookedList.Where(item => item.Patient.DBID != null && item.Appointment.SessionHolder != null))
				{
					try
					{
						GetBookedPatientDetails(bookedPatients, appointments, item);
					}
					catch (Exception ex)
					{
						Logger.Instance.WriteLog(LogType.Error, "Failed while saving PCS Appointments for patient: " +
							item.Patient.DBID, ex, _kioskId);
					}
				}
			}

			return appointments;
		}

		private static void GetBookedPatientDetails(ApiResult<BookedPatients> bookedPatients, List<Model.Appointment> appointments, BookedPatientsBooked item)
		{
			DateTime dt;
			var sessionHolder = bookedPatients.Result.SessionHolderList.FirstOrDefault(x => x.DBID == item.Appointment.SessionHolder.DBID);
			if (sessionHolder == null)
			{
				sessionHolder = new BookedPatientsSessionHolder
				{
					DBID = "1",
					Title = "Dr",
					FirstNames = "Unknown",
					LastName = "Clinician",
					WaitingTime = "0"
				};
			}

			appointments.Add(new Model.Appointment()
			{
				AppointmentTime = DateTime.TryParseExact(
					item.Appointment.Date + " " + item.Appointment.Time,
					"dd/MM/yyyy HH:mm",
					CultureInfo.InvariantCulture,
					DateTimeStyles.None,
					out dt)
					? dt
					: Convert.ToDateTime(item.Appointment.Date + " " + item.Appointment.Time),
				Id = Convert.ToInt32(item.Appointment.ID.DBID),
				SiteId = GetSiteId(item.Appointment.AppointmentSite),
				BookedPatient = new Patient()
				{
					Gender = item.Patient.Sex.EncryptAES256(),
					Id = Convert.ToInt32(item.Patient.DBID),
					Title = item.Patient.Title.EncryptAES256(),
					CallingName = item.Patient.CallingName.EncryptAES256(),
					FirstName = item.Patient.FirstNames.EncryptAES256(),
					FamilyName = item.Patient.FamilyName.EncryptAES256(),
					Email = item.Patient.Email.EncryptAES256(),
					Dob = DateTime.TryParseExact(
						item.Patient.DateOfBirth,
						"dd/MM/yyyy",
						CultureInfo.InvariantCulture,
						DateTimeStyles.None,
						out dt)
						? item.Patient.DateOfBirth.EncryptAES256()
						: null,
					Mobile = item.Patient.Mobile.EncryptAES256(),
					HomeTelephone = item.Patient.HomeTelephone.EncryptAES256(),
					WorkTelephone = item.Patient.WorkTelephone.EncryptAES256(),
					PostCode = item.Patient.Address.PostCode.EncryptAES256(),
					PatientSystemType = SystemType.EmisWeb
				},
				SessionHolder = new Member()
				{
					FirstName = sessionHolder.FirstNames,
					Id = Convert.ToInt32(sessionHolder.DBID),
					LastName = sessionHolder.LastName,
					Title = sessionHolder.Title,
					WaitingTime = Convert.ToInt32(sessionHolder.WaitingTime)
				},
				Reception = null
			});
		}

		private static long GetSiteId(string id)
		{
			long siteId;
			long.TryParse(id, out siteId);
			return siteId;
		}

		public string SetPatientMedicalRecord(List<Questions> questionList)
		{
			try
			{
				string medicalRecordXml = string.Empty;
				const int doctorListCount = 1;
				var credential = _configRepository.GetPatientFlowUser();

				switch (credential.SystemType)
				{
					case SystemType.EmisWeb:
						medicalRecordXml = EmisWebMedicalRecordMethod(questionList, doctorListCount, credential);
						break;
					case SystemType.EmisPcs:
						medicalRecordXml = PCSMedicalRecordMethod(questionList, doctorListCount, credential);
						break;
					case SystemType.TPPSystmOne:
						medicalRecordXml = TPPMedicalRecordMethod(questionList, credential);
						break;
				}

				return medicalRecordXml;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return string.Empty;
			}
		}

		private string TPPMedicalRecordMethod(List<Questions> questionList, Credential credential)
		{
			string medicalRecordXml;
			try
			{
				TPPHelper tppHelper = new TPPHelper();
				medicalRecordXml = tppHelper.TPPPatientMedicalRecord(GlobalVariables.ArrivedPatientDetails.BookedPatient.PatientIdentifiers, questionList, credential);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return string.Empty;
			}

			return medicalRecordXml;
		}

		private string EmisWebMedicalRecordMethod(List<Questions> questionList, int doctorListCount, Credential credential)
		{
			string medicalRecordXml;
			using (var client = new WebClient(credential))
			{
				var organisation = client.GetOrganisation();
				_currentUser = organisation.Result.UserList.FirstOrDefault(a => a.Security1 == credential.UserName);

				var medicalRecord = new MedicalRecord
				{
					PatientID = GlobalVariables.ArrivedPatientDetails.BookedPatient.Id,
					ConsultationList = new MedicalRecordConsultationList
					{
						Consultation = new MedicalRecordConsultationListConsultation
						{
							LocationTypeID = new MedicalRecordConsultationListConsultationLocationTypeID
							{
								RefID = organisation.Result.OrganisationList.Organisation.LocationTypeID.RefID
							}
						}
					},
					PeopleList = new MedicalRecordPerson
					{
						Person = new MedicalRecordPeopleListPerson[doctorListCount]
					},
					LocationTypeList = new MedicalRecordLocationTypeList()
					{
						LocationType = new MedicalRecordLocationTypeListLocationType()
						{
							DBID = organisation.Result.OrganisationList.Organisation.LocationTypeID.DBID,
							RefID = organisation.Result.OrganisationList.Organisation.LocationTypeID.RefID,
							Description = organisation.Result.LocationTypeList.FirstOrDefault(d => d.GUID == organisation.Result.OrganisationList.Organisation.LocationTypeID.GUID).Description
						}
					}
				};

				for (int i = 0; i < doctorListCount; i++)
				{
					medicalRecord.PeopleList.Person[i] = new MedicalRecordPeopleListPerson()
					{
						RefID = ArrivedPatientDoctor,
						GUID = organisation.Result.UserList.FirstOrDefault(user => user.DBID == ArrivedPatientDoctor).GUID
					};
				}

				medicalRecord = SetConsultationList(medicalRecord);
				medicalRecord = SetConsultationElement(medicalRecord, questionList);

				medicalRecordXml = Convertor.ConvertObjectToXmlString(medicalRecord);
			}

			return medicalRecordXml;
		}

		private string PCSMedicalRecordMethod(List<Questions> questionList, int doctorListCount, Credential credential)
		{
			string medicalRecordXml;
			using (var client = new PcsClient(credential))
			{
				var organisation = client.GetOrganisation();
				var currentUser = client.GetCurrentUser();

				_currentUser = organisation.Result.UserList.FirstOrDefault(a => a.DBID == currentUser.Result.Person.DBID);

				var medicalRecord = new PcsMedicalRecord.MedicalRecordType
				{
					PatientID = GlobalVariables.ArrivedPatientDetails.BookedPatient.Id.ToString(CultureInfo.InvariantCulture),
					PeopleList = new PcsMedicalRecord.PersonType[doctorListCount]
				};

				var consultation = new PcsMedicalRecord.ConsultationType
				{
					AssignedDate = string.Format("{0:dd/MM/yyyy}", DateTime.UtcNow),
					GUID = "{" + Guid.NewGuid() + "}",
					LocationID = new PcsMedicalRecord.IdentType
					{
						//Has to assign values for this
						//	RefID = 2.ToString()
						RefID = organisation.Result.OrganisationList.Organisation.LocationTypeID.RefID.ToString(CultureInfo.InvariantCulture),
						//DBID = organisation.Result.OrganisationList.Organisation.LocationTypeID.DBID.ToString(CultureInfo.InvariantCulture)
					},
					LocationTypeID = new PcsMedicalRecord.IdentType
					{
						RefID = organisation.Result.OrganisationList.Organisation.LocationTypeID.RefID.ToString(CultureInfo.InvariantCulture),
						DBID = organisation.Result.OrganisationList.Organisation.LocationTypeID.DBID.ToString(CultureInfo.InvariantCulture)
					},
					UserID = new PcsMedicalRecord.IdentType
					{
						RefID = ArrivedPatientDoctor.ToString(CultureInfo.InvariantCulture)
					},
					OriginalAuthor = new PcsMedicalRecord.AuthorType()
					{
						User = new PcsMedicalRecord.IdentType()
						{
							RefID = ArrivedPatientDoctor.ToString()
						}
					},
					ElementList = SetPCSConsultationElements(questionList),
				};
				medicalRecord.ConsultationList = new[] { consultation };

				for (int i = 0; i < doctorListCount; i++)
				{
					var User = organisation.Result.UserList.FirstOrDefault(user => user.DBID == ArrivedPatientDoctor);
					medicalRecord.PeopleList[i] = new PcsMedicalRecord.PersonType
					{
						DBID = User.DBID.ToString(),
						RefID = ArrivedPatientDoctor.ToString(),
						GUID = User.GUID,
						Title = User.Title,
						FirstNames = User.FirstNames,
						LastName = User.LastName
					};
				}

				var locationType = new PcsMedicalRecord.TypeOfLocationType()
				{
					DBID = organisation.Result.OrganisationList.Organisation.LocationTypeID.DBID.ToString(),
					RefID = organisation.Result.OrganisationList.Organisation.LocationTypeID.RefID.ToString(),
					Description = organisation.Result.LocationTypeList.FirstOrDefault(d => d.RefID == organisation.Result.OrganisationList.Organisation.LocationTypeID.RefID).Description
				};

				medicalRecord.LocationTypeList = new PcsMedicalRecord.TypeOfLocationType[] { locationType };

				var location = new PcsMedicalRecord.LocationType()
				{
					//Has to update values for location
					DBID = organisation.Result.OrganisationList.Organisation.DBID.ToString(),
					//RefID = organisation.Result.OrganisationList.Organisation.LocationTypeID.RefID.ToString(), // Not sure of the value
					GUID = organisation.Result.OrganisationList.Organisation.GUID,
					LocationName = organisation.Result.OrganisationList.Organisation.LocationName,
					LocationTypeID = new PcsMedicalRecord.IdentType
					{
						RefID = organisation.Result.OrganisationList.Organisation.LocationTypeID.RefID.ToString() //Has to update value
					}
				};

				medicalRecord.LocationList = new PcsMedicalRecord.LocationType[] { location };

				medicalRecordXml = Convertor.ConvertObjectToXmlString(medicalRecord);
			}

			return medicalRecordXml;
		}

		private PcsMedicalRecord.ElementListTypeConsultationElement[] SetPCSConsultationElements(List<Questions> questionList)
		{
			var ElementList = new PcsMedicalRecord.ElementListTypeConsultationElement[0];
			try
			{
				questionList = questionList.Where(a => a.SelectedAnswer != Constants.SkippedAnswer).ToList();

				int count1 = questionList.Count(d => (d.AnswerControlType == AnswerControlType.Textbox.ToString() ||
												d.AnswerControlType == AnswerControlType.NumericTextBox.ToString() ||
												d.SelectedAnswer == Constants.SkippedAnswer ||
												d.SelectedAnswer == Constants.NotAnswered));
				int count2 = questionList.Sum(d => d.AnswerOptions.Count(e => e.IsChecked));
				int totalQnCount = count1 + count2;

				ElementList = new PcsMedicalRecord.ElementListTypeConsultationElement[totalQnCount];
				int consultationIndex = 0, questionIndex = 0;
				while (consultationIndex < totalQnCount)
				{
					if (questionList[questionIndex].AnswerOptions.Any(d => d.IsChecked))
					{
						foreach (var userOptions in questionList[questionIndex].AnswerOptions.Where(d => d.IsChecked))
						{
							string optionCode = userOptions.OptionCode;
							if (!string.IsNullOrEmpty(optionCode))
							{
								ElementList[consultationIndex] = SetPCSConsulationRecord(ElementList, consultationIndex);

								ElementList[consultationIndex].Item = new PcsMedicalRecord.EventType
								{
									RefID = 3.ToString(), //not sure what value to assign
									GUID = "{" + Guid.NewGuid().ToString() + "}",
									AssignedDate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'),
									AuthorID = new PcsMedicalRecord.IdentType
									{
										RefID = ArrivedPatientDoctor.ToString()
									},
									OriginalAuthor = new PcsMedicalRecord.AuthorType
									{
										User = new PcsMedicalRecord.IdentType
										{
											RefID = ArrivedPatientDoctor.ToString()
										},
									},
									Code = new PcsMedicalRecord.StringCodeType
									{
										Value = optionCode,
										Scheme = PcsMedicalRecord.StringCodeTypeScheme.READ2,
										Term = userOptions.AnswerOptionText
									},
									TermID = new PcsMedicalRecord.StringCodeType
									{
										Value = optionCode,
										Scheme = PcsMedicalRecord.StringCodeTypeScheme.READ2,
										Term = userOptions.AnswerOptionText
									},
									DisplayTerm = userOptions.AnswerOptionText
								};

							}
							else
							{
								ElementList[consultationIndex] = SetPCSConsulationRecord(ElementList, consultationIndex);
								string displayTerm = questionList[questionIndex].QuestionText + " " + userOptions.AnswerOptionText;

								ElementList[consultationIndex].Item = new PcsMedicalRecord.EventType
								{
									RefID = 1.ToString(), //not sure what value to assign
									GUID = "{" + Guid.NewGuid().ToString() + "}",
									AssignedDate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'),
									AuthorID = new PcsMedicalRecord.IdentType
									{
										RefID = ArrivedPatientDoctor.ToString()
									},
									OriginalAuthor = new PcsMedicalRecord.AuthorType
									{
										User = new PcsMedicalRecord.IdentType
										{
											RefID = ArrivedPatientDoctor.ToString()
										},
									},
									DisplayTerm = displayTerm.Length > 200 ? displayTerm.Substring(0, 200) : displayTerm
								};
							}
							consultationIndex++;
						}
						questionIndex++;
					}
					else
					{
						ElementList[consultationIndex] = SetPCSConsulationRecord(ElementList, consultationIndex);
						string displayTerm = questionList[questionIndex].QuestionText + " " + questionList[questionIndex].SelectedAnswer;

						ElementList[consultationIndex].Item = new PcsMedicalRecord.EventType
						{
							RefID = 1.ToString(), //not sure what value to assign
							GUID = "{" + Guid.NewGuid().ToString() + "}",
							AssignedDate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'),
							AuthorID = new PcsMedicalRecord.IdentType
							{
								RefID = ArrivedPatientDoctor.ToString()
							},
							OriginalAuthor = new PcsMedicalRecord.AuthorType
							{
								User = new PcsMedicalRecord.IdentType
								{
									RefID = ArrivedPatientDoctor.ToString()
								},
							},
							DisplayTerm = displayTerm.Length > 200 ? displayTerm.Substring(0, 200) : displayTerm
						};
						consultationIndex++;
						questionIndex++;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
			}

			return ElementList;
		}

		private static PcsMedicalRecord.ElementListTypeConsultationElement SetPCSConsulationRecord(PcsMedicalRecord.ElementListTypeConsultationElement[] ElementList, int consultationIndex)
		{
			ElementList[consultationIndex] = new PcsMedicalRecord.ElementListTypeConsultationElement
			{
				RefID = 3.ToString(), //not sure what value to assign
				GUID = "{" + Guid.NewGuid() + "}",
				FileStatus = "0",
				Header = new PcsMedicalRecord.IntegerCodeType
				{
					Term = "EGTON Kiosk Data"
				}
			};

			return ElementList[consultationIndex];
		}

		public MedicalRecord SetConsultationList(MedicalRecord medicalRecord)
		{
			try
			{
				medicalRecord.ConsultationList.Consultation.AssignedDate = GlobalVariables.ArrivedPatientDetails.AppointmentTime.ToString("dd/MM/yyyy").Replace('-', '/');
				medicalRecord.ConsultationList.Consultation.GUID = GlobalVariables.SelectedOrganisation.SystemType == SystemType.EmisPcs.GetDisplayName() ?
																	"{" + Guid.NewGuid() + "}" : Guid.NewGuid().ToString();
				medicalRecord.ConsultationList.Consultation.UserID = new MedicalRecordConsultationListConsultationUserID
				{
					RefID = ArrivedPatientDoctor
				};
				medicalRecord.ConsultationList.Consultation.OriginalAuthor = new MedicalRecordConsultationListConsultationOriginalAuthor()
				{
					User = new MedicalRecordConsultationListConsultationOriginalAuthorUser()
					{
						RefID = ArrivedPatientDoctor
					}
				};
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
			}

			return medicalRecord;
		}

		public MedicalRecord SetConsultationElement(MedicalRecord medicalRecord, List<Questions> questionList)
		{
			try
			{
				questionList = questionList.Where(a => a.SelectedAnswer != Constants.SkippedAnswer).ToList();

				int count1 = questionList.Count(d => (d.AnswerControlType == AnswerControlType.Textbox.ToString() ||
												d.AnswerControlType == AnswerControlType.NumericTextBox.ToString() ||
												d.SelectedAnswer == Constants.SkippedAnswer ||
												d.SelectedAnswer == Constants.NotAnswered));
				int count2 = questionList.Sum(d => d.AnswerOptions.Count(e => e.IsChecked));
				int totalQnCount = count1 + count2;
				medicalRecord.ConsultationList.Consultation.ElementList = new MedicalRecordConsultationListConsultationElementList
				{
					ConsultationElement = new MedicalRecordConsultationListConsultationElementListConsultationElement[totalQnCount]
				};

				int consultationIndex = 0, questionIndex = 0;
				while (consultationIndex < totalQnCount)
				{
					if (questionList[questionIndex].AnswerOptions.Any(d => d.IsChecked))
					{
						foreach (var userOptions in questionList[questionIndex].AnswerOptions.Where(d => d.IsChecked))
						{
							string optionCode = userOptions.OptionCode;
							long snomedCode = userOptions.SnomedCode;
							if (!string.IsNullOrEmpty(optionCode))
							{
								SetConsulationRecord(medicalRecord, consultationIndex);
								medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex].Event.Code =
									new MedicalRecordConsultationListConsultationElementListConsultationElementEventCode()
									{
										Value = optionCode,
										Scheme = "READ2"
									};

								medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex].Event.TermID =
									new MedicalRecordConsultationListConsultationElementListConsultationElementEventTerm()
									{
										Value = optionCode,
										Scheme = "READ2",
									};

								medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex].Event.DisplayTerm = userOptions.AnswerOptionText;
							}
							else if (snomedCode > 0)
							{
								SetConsulationRecord(medicalRecord, consultationIndex);
								medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex].Event.Code =
									new MedicalRecordConsultationListConsultationElementListConsultationElementEventCode()
									{
										Value = snomedCode.ToString(),
										Scheme = "SNOMED"
									};

								medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex].Event.TermID =
									new MedicalRecordConsultationListConsultationElementListConsultationElementEventTerm()
									{
										Value = snomedCode.ToString(),
										Scheme = "SNOMED",
									};

								medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex].Event.DisplayTerm = userOptions.AnswerOptionText;
							}
							else
							{
								SetConsulationRecord(medicalRecord, consultationIndex);
								string displayTerm = questionList[questionIndex].QuestionText + " " + userOptions.AnswerOptionText;
								medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex].Event.DisplayTerm =
									displayTerm.Length > 200 ? displayTerm.Substring(0, 200) : displayTerm;
							}
							consultationIndex++;
						}
						questionIndex++;
					}
					else
					{
						SetConsulationRecord(medicalRecord, consultationIndex);
						string displayTerm = questionList[questionIndex].QuestionText + " " + questionList[questionIndex].SelectedAnswer;
						medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex].Event.DisplayTerm =
							displayTerm.Length > 200 ? displayTerm.Substring(0, 200) : displayTerm;
						consultationIndex++;
						questionIndex++;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
			}

			return medicalRecord;
		}

		public void SetConsulationRecord(MedicalRecord medicalRecord, int consultationIndex)
		{
			try
			{
				medicalRecord.ConsultationList.Consultation.ElementList.ConsultationElement[consultationIndex] =
					new MedicalRecordConsultationListConsultationElementListConsultationElement
					{
						RefID = 1,
						FileStatus = 0,
						GUID = GlobalVariables.SelectedOrganisation.SystemType == SystemType.EmisPcs.GetDisplayName() ? "{" + Guid.NewGuid() + "}" : null,
						Header = new MedicalRecordConsultationListConsultationElementListConsultationElementHeader
						{
							Term = "EGTON Kiosk Data"
						},
						Event = new MedicalRecordConsultationListConsultationElementListConsultationElementEvent
						{
							RefID = 1,
							GUID = Guid.NewGuid().ToString(),
							AssignedDate = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/'),
							AuthorID = new MedicalRecordConsultationListConsultationElementListConsultationElementEventAuthorID
							{
								RefID = ArrivedPatientDoctor
							},
							OriginalAuthor = new MedicalRecordConsultationListConsultationElementListConsultationElementEventOriginalAuthor
							{
								User = new MedicalRecordConsultationListConsultationElementListConsultationElementEventOriginalAuthorUser
								{
									RefID = ArrivedPatientDoctor
								},
							}
						}
					};
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
			}
		}

		public bool SavePatientSurvey(string medicalRecordXml, string attachmentPath)
		{
			try
			{
				Credential credential = _configRepository.GetPatientFlowUser();
				string patientId = Convert.ToString(GlobalVariables.ArrivedPatientDetails.BookedPatient.Id);
				string apiPassword = ConfigurationManager.AppSettings["FileRecordAPIPwd"];
				ApiResult<string> result;

				if (credential.SystemType == SystemType.EmisPcs)
				{
					using (var client = new PcsClient(credential))
					{
						result = client.SavePatientSurvey(apiPassword, patientId, medicalRecordXml, attachmentPath);
						if (!result.IsSuccess)
						{
							string message =
									string.Format(
										"WebClient Method: {0};  Message: {1}; Parameters: PatientId: {2}, MedicalRecordXML: {3};",
										"FileRecord",
										GlobalVariables.SelectedOrganisation.SystemType == SystemType.EmisPcs.GetDisplayName()
											? GlobalVariables.PcsPatientRecordErrMsg
											: client.Message,
										patientId,
										medicalRecordXml);
							Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
						}
					}
				}
				else if (credential.SystemType == SystemType.TPPSystmOne)
				{
					using (var client = new TppClient(credential))
					{
						result = client.SavePatientSurvey(medicalRecordXml);
						if (!result.IsSuccess)
						{
							string message =
									string.Format(
										"TppClient Method: {0};  Message: {1}; Parameters: PatientId: {2}, MedicalRecordXML: {3};",
										"FileRecord",
										result.Error,
										patientId,
										medicalRecordXml);
							Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
						}
					}
				}
				else
				{
					using (var client = new WebClient(credential))
					{
						result = client.SavePatientSurvey(apiPassword, patientId, medicalRecordXml, attachmentPath);
						if (!result.IsSuccess)
						{
							string message =
									string.Format(
										"WebClient Method: {0};  Message: {1}; Parameters: PatientId: {2}, MedicalRecordXML: {3};",
										"FileRecord",
										GlobalVariables.SelectedOrganisation.SystemType == SystemType.EmisPcs.GetDisplayName()
											? GlobalVariables.PcsPatientRecordErrMsg
											: client.Message,
										patientId,
										medicalRecordXml);
							Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
						}
					}
				}

				return result.IsSuccess;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return false;
			}
		}

		public int GetOrganisationId()
		{
			Credential credential = _configRepository.GetPatientFlowUser();
			if (credential.SystemType == SystemType.EmisPcs)
			{
				using (var client = new PcsClient(credential))
				{
					var result = client.GetOrganisation();

					if (result.IsSuccess)
					{
						return (int)result.Result.OrganisationList.Organisation.DBID;
					}
				}
			}
			else
			{
				using (var client = new WebClient(credential))
				{
					var result = client.GetOrganisation();

					if (result.IsSuccess)
					{
						return (int)result.Result.OrganisationList.Organisation.DBID;
					}
				}
			}
			return 0;
		}

		public bool AnySlotsForSessions(Credential credential, List<int> sessionIDs, bool istoday = false)
		{
			if (sessionIDs == null || sessionIDs.Count == 0)
			{
				return false;
			}

			using (var client = new WebClient(credential))
			{
				return (from sessionId in sessionIDs select client.GetSlotForSessions(sessionId) into result where result.IsSuccess where result.Result != null select new List<SlotListSlot>(result.Result.Slot)).Select(slotListSlots => AnySlots(istoday ? Enumerable.Where<SlotListSlot>(slotListSlots, a => TimeSpan.Parse(a.StartTime) > DateTime.Now.TimeOfDay).ToList() : slotListSlots)).FirstOrDefault();
			}
		}

		private static bool AnySlots(IEnumerable<SlotListSlot> slotListSlots)
		{
			return GlobalVariables.SelectedSlotType == null
				? slotListSlots.Any(a => a.Status == "Slot Available")
				: slotListSlots.Any(
					a => a.Status == "Slot Available" && a.Type.TypeID == int.Parse(GlobalVariables.SelectedSlotType.SlotTypeId));
		}

		public PCSPatientsAppointmentList.PatientAppointmentList GetPatientAppointmentList(int patientId)
		{
			try
			{
				Credential credential = _configRepository.GetPatientFlowUser();

				if (credential.SystemType == SystemType.EmisPcs)
				{
					using (var client = new PcsClient(credential))
					{
						var result = client.GetPatientAppointments(patientId.ToString());

						if (!result.IsSuccess)
						{
							string message =
								string.Format(
									"WebClient Method: {0};  Message: {1}; Parameters: PatientId: {2}",
									"GetPatientAppointments",
									client.Message,
									patientId);
							Logger.Instance.WriteLog(LogType.Error, message, null, _kioskId);
						}
						return result.Result;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return null;
			}
			return null;
		}

		public EMISWebPA.PatientAppointmentList GetEmisPatientAppointmentList(int patientId)
		{
			try
			{
				Credential credential = _configRepository.GetPatientFlowUser();

				if (credential.SystemType == SystemType.EmisWeb)
				{
					using (var client = new WebClient(credential))
					{
						var result = client.GetPatientAppointments(patientId.ToString());

						if (result.IsSuccess)
						{
							return result.Result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, _kioskId);
				return null;
			}
			return null;
		}
	}

}
