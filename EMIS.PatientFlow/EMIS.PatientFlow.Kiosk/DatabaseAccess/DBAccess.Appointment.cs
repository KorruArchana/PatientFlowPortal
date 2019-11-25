using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess
{
	public partial class DbAccess
	{
		private const string _spAppointmentList = "[PatientFlow].[GetAppointments]";
		private List<Appointment> appointments;
		/// <summary>
		/// Fetching appointments from database and filtering appointments based on patient match criteria or 
		/// filtering synced appointments based on patient match criteria
		/// </summary>
		/// <param name="syncedAppointments">synced appointments</param>
		/// <param name="isSynced">bool to check whether appointments is synced or from local database</param>
		/// <returns>appointment list</returns>
		public List<Appointment> GetMatchingAppointments(List<Appointment> syncedAppointments, bool isSynced)
		{
			appointments = null;
			int selectedDay = Convert.ToInt32(GlobalVariables.Day);
			int selectedYear = Convert.ToInt32(GlobalVariables.Year);
			int selectedMonth = Convert.ToInt32(GlobalVariables.PatientMatchSelectedMonth);
			string selectedGender = GlobalVariables.PatientMatchGender;
			string selectedSurname = GlobalVariables.PatientMatchSurname;
			string postcode = GlobalVariables.PatientMatchPinCode;
			DateTime? selectedDob = GlobalVariables.PatientMatchDob;
			try
			{
				appointments = (!isSynced) ? GetAppointments() : syncedAppointments;
				if (appointments != null)
				{
					appointments = (selectedDay > 0) ? appointments.Where(x => Convert.ToDateTime(x.BookedPatient.Dob).Day == selectedDay).ToList() : appointments;
					appointments = (selectedYear > 0) ? appointments.Where(x => Convert.ToDateTime(x.BookedPatient.Dob).Year == selectedYear).ToList() : appointments;
					appointments = (selectedMonth > 0) ? appointments.Where(x => Convert.ToDateTime(x.BookedPatient.Dob).Month == selectedMonth).ToList() : appointments;
					appointments = (!string.IsNullOrEmpty(selectedGender)) ?
						GetAppointmentsBasedOnGender(selectedGender) :
						appointments;
					appointments = (!string.IsNullOrEmpty(selectedSurname)) ? appointments.Where(x => x.BookedPatient.FamilyName.StartsWith(selectedSurname)).ToList() :
									appointments;
					appointments = (selectedDob != null) ? appointments.Where(x => Convert.ToDateTime(x.BookedPatient.Dob).Date == selectedDob.Value.Date).ToList() :
									appointments;
					appointments = (!string.IsNullOrEmpty(postcode)) ? appointments.Where(x => x.BookedPatient.PostCode != null && x.BookedPatient.PostCode.ToUpper().Equals(postcode.ToUpper())).ToList() :
									appointments;
					appointments = (GlobalVariables.IsBarCodeArrivalDone && GlobalVariables.SelectedSlotId > 0) ? (appointments.Where(x => x.Id == GlobalVariables.SelectedSlotId)).ToList() : appointments;
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}

			return appointments;
		}

		private List<Appointment> GetAppointmentsBasedOnGender(string selectedGender)
		{
			return ((selectedGender != Constants.GenderOther) ? appointments.Where(x => x.BookedPatient.Gender == selectedGender).ToList() :
									appointments.Where(x => (x.BookedPatient.Gender != Constants.GenderMale && x.BookedPatient.Gender != Constants.GenderFemale)).ToList());
		}

		private List<Appointment> GetAppointments()
		{
			List<Appointment> appointments = null;
			if (GlobalVariables.IsArrive)
			{
				appointments = GetUnArrivedAppointments();
				foreach (var item in appointments)
				{
					item.BookedPatient.Gender = item.BookedPatient.Gender.DecryptAES256();
					item.BookedPatient.Dob = item.BookedPatient.Dob.DecryptAES256();
					item.BookedPatient.FamilyName = item.BookedPatient.FamilyName.DecryptAES256();
					item.BookedPatient.Title = item.BookedPatient.Title.DecryptAES256();
					item.BookedPatient.FirstName = item.BookedPatient.FirstName.DecryptAES256();
					item.BookedPatient.CallingName = item.BookedPatient.CallingName.DecryptAES256();
					item.BookedPatient.PostCode = item.BookedPatient.PostCode.DecryptAES256();
					item.BookedPatient.Mobile = item.BookedPatient.Mobile.DecryptAES256();
					item.BookedPatient.HomeTelephone = item.BookedPatient.HomeTelephone.DecryptAES256();
					item.BookedPatient.Email = item.BookedPatient.Email.DecryptAES256();
					item.BookedPatient.PatientIdentifiers = item.BookedPatient.PatientIdentifiers.DecryptAES256();
				}
			}
			else
			{
				appointments = GetAppointmentsFromAPI();
			}

			return appointments;
		}

		private List<Appointment> GetUnArrivedAppointments()
		{
			var appointments = new List<Appointment>();
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand(_spAppointmentList);
				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", DateTime.Today));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ToDate", DateTime.Today.AddDays(1).AddSeconds(-1)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Status", 0));

				using (var dr = spCommand.ExecuteReader())
				{
					appointments = AddAppointmentsToList(dr);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return appointments;
			}
			finally
			{
				DbManager.Close();
			}

			return appointments;
		}

		private List<Appointment> AddAppointmentsToList(SqlDataReader dr)
		{
			var appointments = new List<Appointment>();

			while (dr.Read())
			{
				appointments.Add(new Appointment()
				{
					Id = dr["AppointmentId"] == DBNull.Value ? 0 : Convert.ToInt64(dr["AppointmentId"]),
					TPPAppointmentID = dr["Reception"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Reception"]),
					SiteId = dr["SiteId"] == DBNull.Value ? 0 : Convert.ToInt64(dr["SiteId"]),
					AppointmentTime = Convert.ToDateTime(dr["AppointmentDate"]),
					BookedPatient = new Patient()
					{
						Gender = dr["Gender"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Gender"]),
						Id = dr["PatientId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PatientId"]),
						Title = dr["Title"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Title"]),
						CallingName = dr["CallingName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CallingName"]),
						FirstName = dr["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["FirstName"]),
						FamilyName = dr["FamilyName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["FamilyName"]),
						Email = dr["Email"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Email"]),
						Dob = dr["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DOB"]),
						Mobile = dr["MobileNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MobileNumber"]),
						HomeTelephone = dr["HomePhoneNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["HomePhoneNumber"]),
						WorkTelephone = dr["WorkPhoneNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["WorkPhoneNumber"]),
						PostCode = dr["PostCode"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PostCode"]),
						PatientIdentifiers = dr["PatientIdentifier"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PatientIdentifier"]),
					},
					SessionHolder = new Member()
					{
						FirstName = dr["MemberFirstName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MemberFirstName"]),
						Id = dr["MemberId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MemberId"]),
						LastName = dr["MemberLastName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MemberLastName"]),
						Title = dr["MemberTitle"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MemberTitle"]),
						WaitingTime = dr["WaitingTime"] == DBNull.Value ? 0 : Convert.ToInt32(dr["WaitingTime"]),
						PracticeName = dr["PracticeName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PracticeName"]),
						Code = dr["Code"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Code"])
					},
					Reception = dr["Reception"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Reception"]),
					Duration = dr["Duration"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Duration"])
				});
			}

			return appointments;
		}

		private List<Appointment> GetAppointmentsFromAPI()
		{
			return (from patient in GlobalVariables.PatientMatches.PatientList
					select new Appointment()
					{
						BookedPatient = new Patient()
						{
							Gender = patient.Sex,
							Id = Convert.ToInt32(patient.DBID),
							Title = patient.Title,
							CallingName = patient.CallingName,
							FirstName = patient.FirstNames,
							FamilyName = patient.FamilyName,
							Email = patient.Email,
							Dob = patient.DateOfBirth,
							Mobile = patient.Mobile,
							HomeTelephone = patient.HomeTelephone,
							WorkTelephone = patient.WorkTelephone,
							PostCode = patient.Address.PostCode
						}
					}).ToList();
		}

		/// <summary>
		/// Get count of total appointments in local database for current date
		/// </summary>
		/// <returns>appointment count</returns>
		private long GetAppointmentsCount()
		{
			long count = default(long);
			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand(_spAppointmentList);
				var outputParameter = DbManager.CreateOutputParameter("@TotalCount", SqlDbType.BigInt);
				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", DateTime.Today));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ToDate", DateTime.Today.AddDays(1).AddSeconds(-1)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Status", 0));
				spCommand.Parameters.Add(DbManager.CreateParameter("@PageNo", 1));
				spCommand.Parameters.Add(DbManager.CreateParameter("@PageSize", 1));
				spCommand.Parameters.Add(outputParameter);
				spCommand.ExecuteNonQuery();
				count = Convert.ToInt64(outputParameter.Value);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				DbManager.Close();
			}

			return count;
		}

		/// <summary>
		/// Update appointment status to local database during patient arrival
		/// </summary>
		/// <param name="appointmentId">selected appointment id</param>
		public void UpdateAppointmentStatus(int appointmentId)
		{
			try
			{
				DbManager.Open();
				const string spUpdateAppointmentStatus = "[PatientFlow].[UpdateAppointmentStatus]";
				SqlCommand spCommand = DbManager.GetSprocCommand(spUpdateAppointmentStatus);

				spCommand.Parameters.Add(DbManager.CreateParameter("@AppointmentId", appointmentId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));

				spCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				DbManager.Close();
			}
		}

		/// <summary>
		/// Fetching session details from database for selected date
		/// </summary>
		/// <param name="date">selected date</param>
		/// <param name="slotTypeId">selected slot type</param>
		/// <returns>session holder details</returns>
		public List<DoctorDetailsBooking> GetAppointmentSessions(DateTime date, string slotTypeId)
		{
			var sessionHolderList = new List<DoctorDetailsBooking>();
			try
			{
				const string spAppointmentSessionList = "[PatientFlow].[GetAppointmentSessions]";
				slotTypeId = string.IsNullOrEmpty(slotTypeId) ? null : slotTypeId;
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand(spAppointmentSessionList);
				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", date));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ToDate", date));
				spCommand.Parameters.Add(DbManager.CreateParameter("@SlotTypeId", slotTypeId, 100));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					sessionHolderList = AddtoSessionHolderList(dr);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				DbManager.Close();
			}

			return sessionHolderList;
		}

		private List<DoctorDetailsBooking> AddtoSessionHolderList(SqlDataReader dr)
		{
			var sessionHolderList = new List<DoctorDetailsBooking>();
			while (dr.Read())
			{
				int doctorId = dr["SessionHolderId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SessionHolderId"]);
				var doctor = sessionHolderList.FirstOrDefault(x => x.DoctorId == doctorId);
				if (doctor == null)
				{
					int sessionId = dr["SessionId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SessionId"]);

					string title = Convert.ToString(dr["MemberTitle"]);
					string firstname = Convert.ToString(dr["MemberFirstName"]);
					string lastName = Convert.ToString(dr["MemberLastName"]);

					var doctorDetailsBooking = new DoctorDetailsBooking()
					{
						DoctorId = doctorId,
						SiteId = dr["SiteId"] == DBNull.Value ? 0 : Convert.ToInt64(dr["SiteId"]),
						SiteName = new Dictionary<int, string>(),
						SessionIDs = new List<int> { sessionId },
						DoctorName = string.Format("{0} {1} {2}", title, firstname, lastName),
						DoctorNameToDisplay = string.IsNullOrEmpty(title) ?
											  string.Format("{0} {1}", firstname, lastName) :
											  string.Format("{0} {1} {2}", title, firstname, lastName),
					};
					doctorDetailsBooking.SiteName.Add(sessionId, dr["SiteName"] == DBNull.Value ? "" : dr["SiteName"].ToString());
					sessionHolderList.Add(doctorDetailsBooking);
				}
				else
				{
					int sessionId = Convert.ToInt32(dr["SessionId"]);
					if (!doctor.SessionIDs.Contains(sessionId))
					{
						doctor.SessionIDs.Add(Convert.ToInt32(dr["SessionId"]));
						doctor.SiteName[sessionId] = dr["SiteName"] == DBNull.Value ? "" : dr["SiteName"].ToString();
					}
				}
			}

			return sessionHolderList;
		}

		/// <summary>
		/// Saving appointment sessions for selected date to database
		/// </summary>
		/// <param name="appointmentSessions">sessions returned from web client/pcs</param>
		/// <param name="selectedDate">selected date</param>
		public void SaveAppointmentSessions(List<AppointmentSession> appointmentSessions, DateTime selectedDate)
		{
			try
			{
				DbManager.Open();
				const string spSaveAppointmentSession = "[PatientFlow].[SaveAppointmentSessions]";
				DataTable dtAppointmentSessions = AddtoAppointmentSessionDatatable(appointmentSessions);

				SqlCommand spCommand = DbManager.GetSprocCommand(spSaveAppointmentSession);

				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", selectedDate.Date));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ToDate", selectedDate.Date));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", KioskId, 50));
				spCommand.Parameters.Add(DbManager.CreateParameter("@AppointmentSession", dtAppointmentSessions, "PatientFlow.MemberAppointmentSession"));

				spCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				DbManager.Close();
			}
		}

		private DataTable AddtoAppointmentSessionDatatable(List<AppointmentSession> appointmentSessions)
		{
			var dtAppointmentSessions = new DataTable();
			dtAppointmentSessions.Columns.Add("SessionId", typeof(int));
			dtAppointmentSessions.Columns.Add("Date", typeof(DateTime));
			dtAppointmentSessions.Columns.Add("StartTime", typeof(string));
			dtAppointmentSessions.Columns.Add("EndTime", typeof(string));
			dtAppointmentSessions.Columns.Add("SlotLength", typeof(int));
			dtAppointmentSessions.Columns.Add("TotalSlots", typeof(int));
			dtAppointmentSessions.Columns.Add("BookedSlots", typeof(int));
			dtAppointmentSessions.Columns.Add("AvailableSlots", typeof(int));
			dtAppointmentSessions.Columns.Add("SessionHolderId", typeof(int));
			dtAppointmentSessions.Columns.Add("SessionHolderTitle", typeof(string));
			dtAppointmentSessions.Columns.Add("SessionHolderFirstName", typeof(string));
			dtAppointmentSessions.Columns.Add("SessionHolderLastName", typeof(string));
			dtAppointmentSessions.Columns.Add("SlotTypeId", typeof(string));
			dtAppointmentSessions.Columns.Add("SystemType", typeof(string));
			dtAppointmentSessions.Columns.Add("SiteId", typeof(long));
			dtAppointmentSessions.Columns.Add("SiteName", typeof(string));

			foreach (var item in appointmentSessions)
			{
				string slotTypeId = (GlobalVariables.SelectedSlotType == null) ? null : item.SlotType.SlotTypeId;
				dtAppointmentSessions.Rows.Add(
					item.SessionId,
					item.Date,
					item.StartTime,
					item.EndTime,
					item.SlotLength,
					0,
					0,
					0,
					item.Member.Id,
					item.Member.Title,
					item.Member.FirstName,
					item.Member.LastName,
					slotTypeId,
					GlobalVariables.SelectedOrganisation.SystemType,
					item.SiteId,
					item.SiteName);
			}

			return dtAppointmentSessions;
		}

		public bool IsPatientNewsletterSubscribed(int patientId)
		{
			bool isNewsletterSubscribed = default(bool);
			try
			{
				DbManager.Open();
				const string spPatientnewsletterSub = "[PatientFlow].[IsNewsletterSubscribed]";
				SqlCommand spCommand = DbManager.GetSprocCommand(spPatientnewsletterSub);
				var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);
				spCommand.Parameters.Add(DbManager.CreateParameter("@PatientId", patientId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));
				spCommand.Parameters.Add(outputParameter);
				spCommand.ExecuteNonQuery();
				isNewsletterSubscribed = Convert.ToBoolean(outputParameter.Value);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				DbManager.Close();
			}

			return isNewsletterSubscribed;
		}

		public void UpdateNewsletterSubscription()
		{
			try
			{
				DbManager.Open();
				const string spPatientnewsletterUpdate = "[PatientFlow].[UpdateNewsletterSubscription]";
				SqlCommand spCommand = DbManager.GetSprocCommand(spPatientnewsletterUpdate);

				spCommand.Parameters.Add(DbManager.CreateParameter("@PatientId", GlobalVariables.ArrivedPatientDetails.BookedPatient.Id));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));
				spCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				DbManager.Close();
			}
		}

		public void SaveAppointments(List<Appointment> appointments, DateTime fromDate,
		SystemType systemType, Boolean status, Boolean isUntimed)
		{
			try
			{
				DbManager.Open();

				var dtAppointments = new DataTable();
				dtAppointments.Columns.Add("AppointmentId", typeof(int));
				dtAppointments.Columns.Add("AppointmentDate", typeof(DateTime));
				dtAppointments.Columns.Add("PatientId", typeof(int));
				dtAppointments.Columns.Add("PatientTitle", typeof(string));
				dtAppointments.Columns.Add("PatientFirstName", typeof(string));
				dtAppointments.Columns.Add("PatientCallingName", typeof(string));
				dtAppointments.Columns.Add("PatientFamilyName", typeof(string));
				dtAppointments.Columns.Add("PatientGender", typeof(string));
				dtAppointments.Columns.Add("PatientPostCode", typeof(string));
				dtAppointments.Columns.Add("PatientDOB", typeof(string));
				dtAppointments.Columns.Add("SessionHolderId", typeof(int));
				dtAppointments.Columns.Add("SessionHolderTitle", typeof(string));
				dtAppointments.Columns.Add("SessionHolderFirstName", typeof(string));
				dtAppointments.Columns.Add("SessionHolderLastName", typeof(string));
				dtAppointments.Columns.Add("WaitingTime", typeof(int));
				dtAppointments.Columns.Add("PracticeName", typeof(string));
				dtAppointments.Columns.Add("PatientEmail", typeof(string));
				dtAppointments.Columns.Add("PatientMobileNumber", typeof(string));
				dtAppointments.Columns.Add("PatientWorkPhoneNumber", typeof(string));
				dtAppointments.Columns.Add("PatientHomePhoneNumber", typeof(string));
				dtAppointments.Columns.Add("ModifiedBy", typeof(string));
				dtAppointments.Columns.Add("SiteId", typeof(string));
				dtAppointments.Columns.Add("PatientIdentifierValue", typeof(string));
				dtAppointments.Columns.Add("MemberIdentifierValue", typeof(string));
				dtAppointments.Columns.Add("Reception", typeof(string));
				dtAppointments.Columns.Add("SystemType", typeof(int));
				dtAppointments.Columns.Add("Duration", typeof(int));

				foreach (var item in appointments)
				{
					dtAppointments.Rows.Add(
						item.Id,
						item.AppointmentTime,
						item.BookedPatient.Id,
						item.BookedPatient.Title,
						item.BookedPatient.FirstName,
						item.BookedPatient.CallingName,
						item.BookedPatient.FamilyName,
						item.BookedPatient.Gender,
						item.BookedPatient.PostCode,
						item.BookedPatient.Dob,
						item.SessionHolder.Id,
						item.SessionHolder.Title,
						item.SessionHolder.FirstName,
						item.SessionHolder.LastName,
						item.SessionHolder.WaitingTime,
						item.SessionHolder.PracticeName,
						item.BookedPatient.Email,
						item.BookedPatient.Mobile,
						item.BookedPatient.WorkTelephone,
						item.BookedPatient.HomeTelephone,
						KioskId,
						item.SiteId,
						item.BookedPatient.PatientIdentifiers,
						item.SessionHolder.Code,
						item.Reception,
						(int)item.BookedPatient.PatientSystemType,
						item.Duration);
				}

				string procName = "[PatientFlow].[SaveAppointments]";

				SqlCommand spCommand = DbManager.GetSprocCommand(procName);
				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", fromDate));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Status", status));
				spCommand.Parameters.Add(DbManager.CreateParameter("@IsUntimed", isUntimed));
				spCommand.Parameters.Add(DbManager.CreateParameter(
					"@Appointment",
					dtAppointments,
					"PatientFlow.BookedAppointment"));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}

		public void SaveTPPAppointments(List<Appointment> appointments, DateTime fromDate,
		SystemType systemType, Boolean status, Boolean isUntimed)
		{
			try
			{
				DbManager.Open();

				var dtAppointments = new DataTable();
				dtAppointments.Columns.Add("AppointmentId", typeof(int));
				dtAppointments.Columns.Add("AppointmentDate", typeof(DateTime));
				dtAppointments.Columns.Add("PatientId", typeof(int));
				dtAppointments.Columns.Add("PatientTitle", typeof(string));
				dtAppointments.Columns.Add("PatientFirstName", typeof(string));
				dtAppointments.Columns.Add("PatientCallingName", typeof(string));
				dtAppointments.Columns.Add("PatientFamilyName", typeof(string));
				dtAppointments.Columns.Add("PatientGender", typeof(string));
				dtAppointments.Columns.Add("PatientDOB", typeof(string));
				dtAppointments.Columns.Add("SessionHolderId", typeof(int));
				dtAppointments.Columns.Add("SessionHolderTitle", typeof(string));
				dtAppointments.Columns.Add("SessionHolderFirstName", typeof(string));
				dtAppointments.Columns.Add("SessionHolderLastName", typeof(string));
				dtAppointments.Columns.Add("WaitingTime", typeof(int));
				dtAppointments.Columns.Add("PostCode", typeof(string));
				dtAppointments.Columns.Add("PracticeName", typeof(string));
				dtAppointments.Columns.Add("ModifiedBy", typeof(string));
				dtAppointments.Columns.Add("SiteId", typeof(string));
				dtAppointments.Columns.Add("PatientIdentifierValue", typeof(string));
				dtAppointments.Columns.Add("MemberIdentifierValue", typeof(string));
				dtAppointments.Columns.Add("TPPAppointmentId", typeof(string));
				dtAppointments.Columns.Add("SystemType", typeof(int));
				dtAppointments.Columns.Add("Duration", typeof(int));
				dtAppointments.Columns.Add("Email", typeof(string));
				dtAppointments.Columns.Add("MobileNumber", typeof(string));
				dtAppointments.Columns.Add("WorkPhoneNumber", typeof(string));
				dtAppointments.Columns.Add("HomePhoneNumber", typeof(string));

				foreach (var item in appointments)
				{
					dtAppointments.Rows.Add(
						item.Id,
						item.AppointmentTime,
						item.BookedPatient.Id,
						item.BookedPatient.Title,
						item.BookedPatient.FirstName,
						item.BookedPatient.CallingName,
						item.BookedPatient.FamilyName,
						item.BookedPatient.Gender,
						item.BookedPatient.Dob,
						item.SessionHolder.Id,
						item.SessionHolder.Title,
						item.SessionHolder.FirstName,
						item.SessionHolder.LastName,
						item.SessionHolder.WaitingTime,
						item.BookedPatient.PostCode,
						item.SessionHolder.PracticeName,
						KioskId,
						item.SiteId,
						item.BookedPatient.PatientIdentifiers,
						item.SessionHolder.MemberIdentifiers,
						item.TPPAppointmentID,
						(int)item.BookedPatient.PatientSystemType,
						item.Duration,
						item.BookedPatient.Email,
						item.BookedPatient.Mobile,
						item.BookedPatient.WorkTelephone,
						item.BookedPatient.HomeTelephone);
				}

				string procName = "[PatientFlow].[SaveTPPAppointments]";

				SqlCommand spCommand = DbManager.GetSprocCommand(procName);
				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", fromDate));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Status", status));
				spCommand.Parameters.Add(DbManager.CreateParameter("@IsUntimed", isUntimed));
				spCommand.Parameters.Add(DbManager.CreateParameter(
					"@Appointment",
					dtAppointments,
					"PatientFlow.TPPBookedAppointment"));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}

		public List<Int64> GetAppointmentsStatus(int patientId, int organisationId)
		{
			List<Int64> arrivedUntimedAppointments = new List<Int64>();

			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAppointmentsStatus]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@PatientId", patientId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						arrivedUntimedAppointments.Add(Convert.ToInt64(dr["AppointmentId"]));
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				DbManager.Close();
			}

			return arrivedUntimedAppointments;
		}

	}
}
