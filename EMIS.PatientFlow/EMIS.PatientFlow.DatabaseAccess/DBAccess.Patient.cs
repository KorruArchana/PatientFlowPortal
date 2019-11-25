using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{

		public IEnumerable<Patient> GetPatientMessageList()
		{
			var patientList = new List<Patient>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatients]", connection);

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							ToPatientObject(patientList, dr);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return patientList;
		}

		private static void ToPatientObject(List<Patient> patientList, SqlDataReader dr)
		{
			try
			{
				var patient = new Patient()
				{
					PatientId = Convert.ToInt32(dr["PatientId"]),
					OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
					OrganisationName = Convert.ToString(dr["OrganisationName"]),
					Firstname = Convert.ToString(dr["FirstName"]).DecryptAES256(),
					Surname = Convert.ToString(dr["SurName"]).DecryptAES256(),
					Message = Convert.ToString(dr["Message"]),
					PatientMessageId = Convert.ToInt32(dr["PatientMessageId"]),
				};
				patientList.Add(patient);
			}
			catch (Exception)
			{
			//	Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
			}
		}

		public IEnumerable<Patient> GetPatientMessageList(string currentUser)
		{
			var patientList = new List<Patient>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatientsByUser]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@User", currentUser, 200));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							ToPatientObject(patientList, dr);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return patientList;
		}

		public int SavePatientMessage(Patient patient, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SavePatientMessage]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@PatientId", patient.PatientId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Firstname", patient.Firstname, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Surname", patient.Surname, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Dob", patient.Dob, 150));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", patient.OrganisationId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PatientMessageId", patient.PatientMessageId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Message", patient.Message, 400));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
		}

		public int DeletePatient(int patientMessageId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeletePatient]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@PatientMessageId", patientMessageId));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
		}

		public Patient GetPatientDetails(int patientMessageId)
		{
			var patient = new Patient();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatientDetails]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@PatientMessageId", patientMessageId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							patient.Firstname = Convert.ToString(dr["Firstname"]).DecryptAES256();
							patient.Surname = Convert.ToString(dr["Surname"]).DecryptAES256();
							patient.PatientId = Convert.ToInt32(dr["PatientId"]);
							patient.PatientMessageId = Convert.ToInt32(dr["PatientMessageId"]);
							patient.OrganisationId = Convert.ToInt32(dr["OrganisationId"]);
							patient.Dob = dr["DOB"] == DBNull.Value ? String.Empty : Convert.ToString(dr["DOB"]).DecryptAES256();
							patient.Message = Convert.ToString(dr["Message"]);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return patient;
		}
	}
}
