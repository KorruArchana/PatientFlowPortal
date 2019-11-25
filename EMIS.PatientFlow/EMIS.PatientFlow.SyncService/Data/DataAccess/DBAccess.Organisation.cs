using System;
using System.Collections.Generic;
using System.Data;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.SyncService.Filters;
using EMIS.PatientFlow.Common.Enums;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess
{
	using System.Data.SqlClient;

	public partial class DbAccess
	{
		public List<Appointment> GetAppointments(AppointmentFilter filter, int pageNo, int pageSize, out long recordCount)
		{
			var appointments = new List<Appointment>();

			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAppointmentsHistory]");
				var outputParameter = DbManager.CreateOutputParameter("@TotalCount", SqlDbType.BigInt);
				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", filter.FromDate));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ToDate", filter.ToDate));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(filter.OrganisationId)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Status", filter.StatusId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@PageNo", pageNo));
				spCommand.Parameters.Add(DbManager.CreateParameter("@PageSize", pageSize));
				spCommand.Parameters.Add(outputParameter);

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						appointments.Add(new Appointment()
						{
							AppointmentTime = Convert.ToDateTime(dr["AppointmentDate"]),
							AppointmentStatus = Convert.ToString(dr["AppointmentStatus"]),
							BookedPatient = BookedPatientDetails(dr),
							SessionHolder = GetSessionHolder(dr)
						});
					}
				}

				recordCount = Convert.ToInt64(outputParameter.Value);
			}
			finally
			{
				DbManager.Close();
			}

			return appointments;
		}

		public void SaveAppointments(List<Appointment> appointments, AppointmentFilter filter, string modifiedBy,
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
						new object[]
							{
								item.Id, item.AppointmentTime, item.BookedPatient.Id, item.BookedPatient.Title,
								item.BookedPatient.FirstName, item.BookedPatient.CallingName,
								item.BookedPatient.FamilyName, item.BookedPatient.Gender, item.BookedPatient.PostCode,
								item.BookedPatient.Dob, item.SessionHolder.Id, item.SessionHolder.Title,
								item.SessionHolder.FirstName, item.SessionHolder.LastName,item.SessionHolder.WaitingTime,item.SessionHolder.PracticeName, item.BookedPatient.Email,
								item.BookedPatient.Mobile, item.BookedPatient.WorkTelephone,
								item.BookedPatient.HomeTelephone, modifiedBy, item.SiteId,
								item.BookedPatient.PatientIdentifiers,item.SessionHolder.Code, item.Reception,
								(int)item.BookedPatient.PatientSystemType, item.Duration
							});
				}

				string procName = "[PatientFlow].[SaveAppointments]";
				SqlCommand spCommand = DbManager.GetSprocCommand(procName);
				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", filter.FromDate));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", filter.OrganisationId));
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

		public void SaveTPPAppointments(List<Appointment> appointments, AppointmentFilter filter, string modifiedBy,
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
						modifiedBy,
						item.SiteId,
						item.BookedPatient.PatientIdentifiers,
						item.SessionHolder.MemberIdentifiers,
						item.TPPAppointmentId,
						(int)item.BookedPatient.PatientSystemType,
						item.Duration,
						item.BookedPatient.Email,
						item.BookedPatient.Mobile,
						item.BookedPatient.WorkTelephone,
						item.BookedPatient.HomeTelephone);
				}

				string procName = "[PatientFlow].[SaveTPPAppointments]";
				SqlCommand spCommand = DbManager.GetSprocCommand(procName);
				spCommand.Parameters.Add(DbManager.CreateParameter("@FromDate", filter.FromDate));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", filter.OrganisationId));
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

		private Patient BookedPatientDetails(SqlDataReader dr)
		{
			return new Patient()
			{
				Gender = Convert.ToString(dr["Gender"]),
				Id = Convert.ToInt32(dr["PatientId"]),
				Title = dr["Title"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Title"]).DecryptAES256(),
				CallingName = dr["CallingName"] == DBNull.Value ? string.Empty :
								  Convert.ToString(dr["CallingName"]).DecryptAES256(),
				FirstName = dr["FirstName"] == DBNull.Value ? string.Empty :
								Convert.ToString(dr["FirstName"]).DecryptAES256(),
				FamilyName = dr["FamilyName"] == DBNull.Value ? string.Empty :
								 Convert.ToString(dr["FamilyName"]).DecryptAES256(),
				Email = dr["Email"] == DBNull.Value ? string.Empty :
							Convert.ToString(dr["Email"]).DecryptAES256(),
				Mobile = dr["MobileNumber"] == DBNull.Value ? string.Empty :
							 Convert.ToString(dr["MobileNumber"]).DecryptAES256(),
				HomeTelephone = dr["HomePhoneNumber"] == DBNull.Value ? string.Empty :
									Convert.ToString(dr["HomePhoneNumber"]).DecryptAES256(),
				WorkTelephone = dr["WorkPhoneNumber"] == DBNull.Value ? string.Empty :
									Convert.ToString(dr["WorkPhoneNumber"]).DecryptAES256(),
				PostCode = dr["PostCode"] == DBNull.Value ? string.Empty :
							   Convert.ToString(dr["PostCode"]).DecryptAES256()
			};
		}
		private Member GetSessionHolder(SqlDataReader dr)
		{
			return new Member()
			{
				FirstName = Convert.ToString(dr["MemberFirstName"]),
				Id = Convert.ToInt32(dr["MemberId"]),
				LastName = Convert.ToString(dr["MemberLastName"]),
				Title = Convert.ToString(dr["MemberTitle"]),
				WaitingTime = Convert.ToInt32(dr["WaitingTime"]),
				PracticeName = string.Empty,
			};
		}
	}
}
