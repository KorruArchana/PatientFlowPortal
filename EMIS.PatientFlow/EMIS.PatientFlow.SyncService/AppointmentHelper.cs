using EMIS.PatientFlow.API;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.API.TopasServiceReference;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.SyncService.Data;
using EMIS.PatientFlow.SyncService.Data.DataAccess.Repository.Interfaces;
using EMIS.PatientFlow.SyncService.Filters;
using EMIS.PatientFlow.SyncService.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Appointment = EMIS.PatientFlow.SyncService.Data.Appointment;

namespace EMIS.PatientFlow.SyncService
{
	internal static class AppointmentHelper
	{
		private static readonly IConfigurationRepository Repository = DiResolver.CurrentInstance.Reslove<IConfigurationRepository>();
		private const string ServiceName = "EMIS PatientFlow SyncService";
		private static readonly string RegistrationKey = Utility.GetAppSettingValue("ProductKey");

		private static Patient GetPatientDetails(BookedPatientsBooked item)
		{
			var patient = new Patient
			{
				Gender = item.Patient.Sex.EncryptAES256(),
				Id = Convert.ToInt32(item.Patient.DBID),
				Title = item.Patient.Title.EncryptAES256(),
				CallingName = item.Patient.CallingName.EncryptAES256(),
				FirstName = item.Patient.FirstNames.EncryptAES256(),
				FamilyName = item.Patient.FamilyName.EncryptAES256(),
				Email = item.Patient.Email.EncryptAES256(),
				Mobile = item.Patient.Mobile.EncryptAES256(),
				HomeTelephone = item.Patient.HomeTelephone.EncryptAES256(),
				WorkTelephone = item.Patient.WorkTelephone.EncryptAES256(),
				PostCode = item.Patient.Address.PostCode.EncryptAES256()
			};

			DateTime dt;

			if (DateTime.TryParseExact(
				item.Patient.DateOfBirth,
				"dd/MM/yyyy",
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out dt)) patient.Dob = item.Patient.DateOfBirth.EncryptAES256();
			return patient;
		}

		internal static bool SyncBookedAppointments(API.Credential credential)
		{
			var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
			DateTime endDate = startDate.AddDays(1).AddMinutes(-1);

			switch (credential.SystemType)
			{
				case SystemType.EmisPcs:
					return SyncEmisPcsBookedAppointments(credential, startDate, endDate);
				case SystemType.EmisWeb:
					return SyncEmisWebBookedAppointments(credential, startDate, endDate);
				case SystemType.TPPSystmOne:
					return SyncTPPBookedAppointments(credential, startDate, endDate);
			}
			return false;
		}

		private static bool SyncEmisWebBookedAppointments(API.Credential credential, DateTime startDate, DateTime endDate)
		{
			bool isZeroAppointmentPresent;
			using (var client = new API.WebClient(credential))
			{
				var within = (int)(endDate - DateTime.UtcNow).TotalMinutes;

				if ((DateTime.UtcNow.TimeOfDay.Hours < 10 && DateTime.UtcNow.TimeOfDay.Hours > 19))
				{
					within = (int)(endDate.AddDays(1) - DateTime.UtcNow).TotalMinutes;
				}

				var before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
				if (before > 300)
					before = 300;

				var patients = client.GetBookedPatients(within, before);

				if (patients.IsSuccess)
				{
					SaveAppointments(startDate, patients.Result, credential.OrganisationId);
					isZeroAppointmentPresent = false;
				}
				else
				{
					string message = string.Format("WebClient Method : {0}; Message : {1} ; Parameters : {2}", "GetBookedPatients",
							client.Message, (new { Within = within, Before = before }).ConvertToJsonString());
					Logger.Instance.WriteLog(LogType.Error, message, null, RegistrationKey);
					isZeroAppointmentPresent = true;
				}

				GetArrivedPatients(credential, startDate, endDate, client);
			}

			return isZeroAppointmentPresent;
		}

		private static bool SyncEmisPcsBookedAppointments(API.Credential credential, DateTime startDate, DateTime endDate)
		{
			bool isZeroAppointmentPresent;
			using (var client = new API.PcsClient(credential))
			{
				var within = (int)(endDate - DateTime.UtcNow).TotalMinutes;
				var before = (int)(DateTime.UtcNow - startDate).TotalMinutes;

				if (before > 30)
					before = 30;

				var patients = client.GetBookedPatients(within, before);

				if (patients.IsSuccess)
				{
					SavePCSAppointments(startDate, patients.Result, credential.OrganisationId);
					isZeroAppointmentPresent = false;
				}
				else
				{
					string message = string.Format("WebClient Method : {0}; Message : {1} ; Parameters : {2}", "GetBookedPatients",
							client.Message, (new { Within = within, Before = before }).ConvertToJsonString());

					Logger.Instance.WriteLog(LogType.Error, message, null, RegistrationKey);
					isZeroAppointmentPresent = true;
				}
				GetArrivedPatients(credential, startDate, endDate, client);
			}

			return isZeroAppointmentPresent;
		}

		private static bool SyncTPPBookedAppointments(API.Credential credential, DateTime startDate, DateTime endDate)
		{
			bool isZeroAppointmentPresent;
			try
			{
				using (var client = new TppClient(credential))
				{
					var within = (int)(endDate - DateTime.UtcNow).TotalMinutes;

					if ((DateTime.UtcNow.TimeOfDay.Hours < 10 && DateTime.UtcNow.TimeOfDay.Hours > 19))
					{
						within = (int)(endDate.AddDays(1) - DateTime.UtcNow).TotalMinutes;
					}

					var before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
					if (before > 300)
						before = 300;

					var response = client.GetTodaysAppointments();
					var appointmentList = new List<Appointment>();

					if (response.Any())
					{
						SaveTPPAppointments(client,startDate, appointmentList, credential.OrganisationId, response);
						isZeroAppointmentPresent = false;
					}
					else
					{
						string message = string.Format("TppClient Method : {0}; Message : {1} ; Parameters : {2}", "GetBookedPatients",
								"Failed to get appointments in TPP", (new { Within = within, Before = before }).ConvertToJsonString());
						Logger.Instance.WriteLog(LogType.Error, message, null, RegistrationKey);
						isZeroAppointmentPresent = true;
					}
				}
			}
			catch (Exception ex)
			{
				string message = string.Format("TppClient Method : {0}; Message : {1} ; Parameters : {2}", "GetBookedPatients",
								"Failed to get appointments in TPP", ex.Message);
				Logger.Instance.WriteLog(LogType.Error, message, null, RegistrationKey);
				isZeroAppointmentPresent = true;
			}

			return isZeroAppointmentPresent;
		}

		private static void GetArrivedPatients(API.Credential credential, DateTime startDate, DateTime endDate, PcsClient client)
		{
			var before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
			var within = (int)(endDate - DateTime.UtcNow).TotalMinutes;

			var arrivedPatients = client.GetArrivedPatients(before, within);
			var sentInPatients = client.GetSentInPatients(before, within);

			List<Appointment> result = new List<Appointment>();

			if (arrivedPatients.IsSuccess)
			{
				result.AddRange(GetSaveAppointmentList(arrivedPatients.Result));
			}
			if (sentInPatients.IsSuccess)
			{
				result.AddRange(GetSaveAppointmentList(sentInPatients.Result));
			}

			if (result.Any())
				SaveAppointmentsWithStatus(startDate, result, credential.OrganisationId, SystemType.EmisPcs);
		}

		private static void GetArrivedPatients(Credential credential, DateTime startDate, DateTime endDate, WebClient client)
		{
			var before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
			var within = (int)(endDate - DateTime.UtcNow).TotalMinutes;

			var arrivedPatients = client.GetArrivedPatients(before, within);
			var sentInPatients = client.GetSentInPatients(before, within);

			List<Appointment> result = new List<Appointment>();

			if (arrivedPatients.IsSuccess)
			{
				result.AddRange(GetSaveAppointmentList(arrivedPatients.Result));
			}
			if (sentInPatients.IsSuccess)
			{
				result.AddRange(GetSaveAppointmentList(sentInPatients.Result));
			}

			if (result.Any())
				SaveAppointmentsWithStatus(startDate, result, credential.OrganisationId, SystemType.EmisWeb);
		}

		private static void SavePCSAppointments(DateTime fromDate, BookedPatients patients, int organisationId)
		{
			if (patients == null)
				return;

			var appointmentList = new List<Appointment>();

			if (patients.SessionHolderList != null)
			{
				foreach (var item in patients.BookedList)
				{
					if (item.Patient.DBID == null || item.Appointment.SessionHolder == null)
						continue;
					try
					{
						GetAppointmentDetails(patients, appointmentList, item);
					}
					catch (Exception ex)
					{
						Logger.Instance.WriteLog(LogType.Error, "Failed while saving PCS Appointments for patient: " + item.Patient.DBID, ex, RegistrationKey);
					}
				}
			}

			Repository.SaveAppointments(
				appointmentList,
				new AppointmentFilter
				{
					FromDate = fromDate,
					OrganisationId = organisationId
				},
				ServiceName,
				SystemType.EmisPcs,
				false,
				false);
		}

		private static void SaveAppointmentsWithStatus(DateTime fromDate, List<Appointment> appointmentList, int organisationId, SystemType systemType)
		{
			Repository.SaveAppointments(
				appointmentList,
				new AppointmentFilter
				{
					FromDate = fromDate,
					OrganisationId = organisationId
				},
				ServiceName,
				systemType,
				true,
				false);
		}

		private static List<Appointment> GetSaveAppointmentList(BookedPatients patients)
		{
			var appointmentList = new List<Appointment>();

			if (patients.SessionHolderList != null)
			{
				foreach (var item in patients.BookedList)
				{
					if (item.Patient.DBID == null || item.Appointment.SessionHolder == null)
						continue;
					try
					{
						GetAppointmentDetails(patients, appointmentList, item);
					}
					catch (Exception ex)
					{
						Logger.Instance.WriteLog(LogType.Error, "Failed while saving PCS Appointments for patient: " + item.Patient.DBID, ex, RegistrationKey);
					}
				}
			}

			return appointmentList;
		}

		private static void GetAppointmentDetails(BookedPatients patients, List<Appointment> appointmentList, BookedPatientsBooked item)
		{
			var appointment = new Appointment();
			DateTime dt;
			appointment.SiteId = GetSiteId(item.Appointment.AppointmentSite);
			appointment.Id = Convert.ToInt64(item.Appointment.ID.DBID);
			appointment.BookedPatient = GetPatientDetails(item);

			if (DateTime.TryParseExact(item.Appointment.Date + " " + item.Appointment.Time, "dd/MM/yyyy HH:mm",
				CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
			{
				appointment.AppointmentTime = dt;
			}
			else if (DateTime.TryParseExact(item.Appointment.Date + " " + item.Appointment.Time, "dd/MM/yyyy HH:mm:ss",
				CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
			{
				appointment.AppointmentTime = dt;
			}

			var holder = patients.SessionHolderList.FirstOrDefault(x => x.DBID == item.Appointment.SessionHolder.DBID);
			if (holder != null)
			{
				appointment.SessionHolder = new Member
				{
					Id = Convert.ToInt32(holder.DBID),
					Title = holder.Title,
					FirstName = holder.FirstNames,
					LastName = holder.LastName,
					WaitingTime = WaitingTime(holder),
				};
			}
			else
			{
				appointment.SessionHolder = new Member
				{
					Id = 1,
					Title = "Dr",
					FirstName = "Unknown",
					LastName = "Clinician",
					WaitingTime = 0,
				};
			}

			List<ValidationResult> validationResult;
			if (Utility.TryValidate(appointment, out validationResult))
				appointmentList.Add(appointment);
		}

		private static long GetSiteId(string id)
		{
			long siteId;
			long.TryParse(id, out siteId);
			return siteId;
		}

		private static void SaveAppointments(DateTime fromDate, BookedPatients patients, int organisationId)
		{
			if (patients == null)
				return;

			var appointmentList = new List<Appointment>();

			if (patients.SessionHolderList != null)
			{
				foreach (var item in patients.BookedList)
				{
					if (item.Patient.DBID == null || item.Appointment.SessionHolder == null)
						continue;

					var appointment = new Appointment();
					DateTime dt;
					appointment.SiteId = GetSiteId(item.Appointment.AppointmentSite);
					appointment.Id = Convert.ToInt64(item.Appointment.ID.DBID);
					appointment.BookedPatient = GetPatientDetails(item);

					if (DateTime.TryParseExact(item.Appointment.Date + " " + item.Appointment.Time, "dd/MM/yyyy HH:mm",
						CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
					{
						appointment.AppointmentTime = dt;
					}
					else if (DateTime.TryParseExact(item.Appointment.Date + " " + item.Appointment.Time, "dd/MM/yyyy HH:mm:ss",
						CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
					{
						appointment.AppointmentTime = dt;
					}

					var holder = patients.SessionHolderList.First(x => x.DBID == item.Appointment.SessionHolder.DBID);
					appointment.SessionHolder = new Member
					{
						Id = Convert.ToInt32(holder.DBID),
						Title = holder.Title,
						FirstName = holder.FirstNames,
						LastName = holder.LastName,
						WaitingTime = WaitingTime(holder),
					};

					List<ValidationResult> validationResult;
					if (Utility.TryValidate(appointment, out validationResult))
						appointmentList.Add(appointment);
				}
			}

			Repository.SaveAppointments(
				appointmentList,
				new AppointmentFilter
				{
					FromDate = fromDate,
					OrganisationId = organisationId
				},
				ServiceName,
				SystemType.EmisWeb,
				false,
				false);
		}

		private static void SaveTPPAppointments(TppClient client,DateTime fromDate, List<Appointment> appointmentList, int organisationId, GetDairy.ClientIntegrationResponseResponseResult[] response)
		{
			int id = 1;
			foreach (GetDairy.ClientIntegrationResponseResponseResult responseResult in response)
			{
				if (responseResult.Appointment.Any(a => a.Status == "Booked"))
				{
					var nhsstring = responseResult.Identity.Select(a => a.NHSNumber).ToList();
					List<GetPatientRecordDetails.ClientIntegrationResponseResponse> detailsList = client.GetPatientRecordDetails( nhsstring);

					foreach (var appointment in responseResult.Appointment)
					{
						if(appointment.Status == "Booked")
						{
							var demoList = detailsList.FirstOrDefault(a => a.Identity[0].NHSNumber == responseResult.Identity[0].NHSNumber).Demographics[0];
							appointmentList.Add(new Appointment()
							{
								Id = id++,
								AppointmentTime = Convert.ToDateTime(appointment.Clinic[0].Slot[0].DateTimeStart),
								TPPAppointmentId = appointment.AppointmentUID,
								BookedPatient = new Patient()
								{
									Gender = responseResult.Demographics[0].Sex.EncryptAES256(),
									Id = id,
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
									Id = id,
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

			Repository.SaveTPPAppointments(
				appointmentList,
				new AppointmentFilter
				{
					FromDate = fromDate,
					OrganisationId = organisationId
				},
				ServiceName,
				SystemType.TPPSystmOne,
				false,
				false);
		}

		private static int WaitingTime(BookedPatientsSessionHolder holder)
		{
			int temp;
			return int.TryParse(holder.WaitingTime, out temp) ? temp : 0;
		}

		internal static void SyncMembers(API.Credential credential, int organisationId)
		{
			try
			{
				List<Member> members = new List<Member>();
				using (var client = new TppClient(credential))
				{
					var response = client.GetOrganisation();
					int i = 1;
					if (response[0].User.Any())
					{
						foreach(var user in response[0].User)
						{
							members.Add(new Member()
							{
								Id = i++,
								FirstName = user.FirstName,
								LastName = user.Surname,
								MemberIdentifiers = user.UserName,
								Title = user.Title,
								WaitingTime = 0,
							});
						}
					}

					if (members.Any())
					{
						Repository.SyncMembers(members, organisationId, SystemType.TPPSystmOne, ServiceName);
					}
				}
			}
			catch(Exception ex)
			{
				string message = string.Format("SyncMembers Method : {0}; Message : {1} ; Parameters : {2}", "SyncMembers",
								"Failed to sync members in TPP", ex.Message);
				Logger.Instance.WriteLog(LogType.Error, message, null, RegistrationKey);
			}
		}
	}
}