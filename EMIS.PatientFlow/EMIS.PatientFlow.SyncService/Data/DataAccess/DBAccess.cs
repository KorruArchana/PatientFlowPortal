using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EMIS.PatientFlow.Common.Database;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.SyncService.Helper;

namespace EMIS.PatientFlow.SyncService.Data.DataAccess
{
	public partial class DbAccess
	{
		private string SyncKey = Utility.GetAppSettingValue("ProductKey");
		public DbManager DbManager { get; set; }

		public DbAccess(DbManager dbManager)
		{
			DbManager = dbManager;
		}

		public List<Log> GetLogs()
		{
			var logs = new List<Log>();
			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetLogs]");

				spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", SyncKey, 50));

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						logs.Add(new Log
						{
							Id = Convert.ToInt64(dr["LogId"]),
							Date = Convert.ToDateTime(dr["Date"]),
							Level = Convert.ToString(dr["Level"]),
							Logger = Convert.ToString(dr["Logger"]),
							Message = Convert.ToString(dr["Message"]),
							Thread = Convert.ToString(dr["Thread"]),
							User = Convert.ToString(dr["User"]),
							Exception = Convert.ToString(dr["Exception"]),
							UsageDate = Convert.ToDateTime(dr["Date"]).ConvertToJsonString()
						});
					}
				}

			}
			finally
			{
				DbManager.Close();
			}

			return logs;
		}

		public List<Survey> GetAnonymousSurveys()
		{
			var surveys = new List<Survey>();
			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAnonymousSurveys]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", SyncKey, 50));

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						var date = GetModifiedTime(dr);
						surveys.Add(new Survey
						{
							AnswerId = Convert.ToInt64(dr["AnswerId"]),
							AnswerText = dr["AnswerText"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AnswerText"]),
							QuestionnaireTitle = dr["QuestionnaireTitle"] == DBNull.Value ? string.Empty :
													Convert.ToString(dr["QuestionnaireTitle"]),
							QuestionText = dr["QuestionText"] == DBNull.Value ? string.Empty : Convert.ToString(dr["QuestionText"]),
							KioskId = dr["KioskId"] == DBNull.Value ? string.Empty : Convert.ToString(dr["KioskId"]),
							QuestionnaireId = dr["QuestionnaireId"] == DBNull.Value ? -1 : Convert.ToInt32(dr["QuestionnaireId"]),
							QuestionId = dr["QuestionId"] == DBNull.Value ? -1 : Convert.ToInt32(dr["QuestionId"]),
							OptionId = dr["OptionId"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OptionId"]),
							ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedBy"]),
							Modified = date,
							ModifiedDate = date.ConvertToJsonString()
						});
					}
				}

			}
			finally
			{
				DbManager.Close();
			}

			return surveys;
		}

		private static DateTime GetModifiedTime(SqlDataReader dr)
		{
			return dr["Modified"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["Modified"]);
		}

		public List<string> GetWebClientConfiguration(string systemType)
		{
			var values = new List<string>();

			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatientFlowUser]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@SystemType", systemType, 50));
				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						var config = new
						{
							IPAddress = Convert.ToString(dr["IPAddress"]),
							OrganisationID = Convert.ToInt32(dr["OrganisationId"]),
							SupplierID = Convert.ToString(dr["SupplierId"]),
							UserName = Convert.ToString(dr["UserName"]),
							Password = Convert.ToString(dr["Password"]),
							SystemType = GetType(dr),
							OrganisationKey = Convert.ToString(dr["OrganisationKey"]),
							WebServiceUrl = dr["WebServiceUrl"] == DBNull.Value ? string.Empty : Convert.ToString(dr["WebServiceUrl"])
						};

						values.Add(config.ConvertToJsonString());
					}
				}
			}
			finally
			{
				DbManager.Close();
			}

			return values;
		}

		private static string GetType(SqlDataReader dr)
		{
			switch (Convert.ToString(dr["Type"]).ToUpper())
			{
				case "EMIS - WEB":
					return "1";
				case "EMIS - PCS":
					return "2";
				case "TPP - SYSTMONE":
					return "7";
				default:
					return "1";
			}
			//return Convert.ToString(dr["Type"]);
		}

		public void SaveSyncLog(SyncType type, long lastItemId)
		{
			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateSynchronisationLog]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@Type", (int)type));
				spCommand.Parameters.Add(DbManager.CreateParameter("@LastItemID", (decimal)lastItemId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", SyncKey, 50));
				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}


		public DataTable CreateListDataTable(List<int> entitylist)
		{
			var dtlist = new DataTable("List");
			int i = 1;
			dtlist.Columns.Add("ListId", typeof(int));
			dtlist.Columns.Add("Id", typeof(int));
			if (entitylist != null && entitylist.Any())
			{
				foreach (var item in entitylist)
				{
					DataRow dr = dtlist.NewRow();
					dr["ListId"] = i++;
					dr["Id"] = item;
					dtlist.Rows.Add(dr);
				}
			}

			return dtlist;
		}

		public void UpdateClientConfiguration(List<int> OrgIdList)
		{
			var dtOrgIdList = CreateListDataTable(OrgIdList);

			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					connection.Open();
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateClientConfiguration]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrgIdList", dtOrgIdList, "PatientFlow.List"));
					spCommand.ExecuteNonQuery();
				}

				finally
				{
					DbManager.Close(connection);
				}
			}
		}
		public void SaveWebClientConfiguration(ApiUser user)
		{
			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SavePatientFlowUser]");

				spCommand.Parameters.Add(DbManager.CreateParameter("@UserName", user.UserName.Decrypt().EncryptAES256(), 150));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Password", user.Password.Decrypt().EncryptAES256(), 150));
				spCommand.Parameters.Add(DbManager.CreateParameter("@SupplierId", user.SupplierId.Decrypt().EncryptAES256(), 150));
				spCommand.Parameters.Add(DbManager.CreateParameter("@DatabaseName", user.DatabaseName, 50));
				spCommand.Parameters.Add(DbManager.CreateParameter("@IPAddress", user.IpAddress, 50));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Type", user.Type, 15));
				spCommand.Parameters.Add(DbManager.CreateParameter("@WebServiceUrl", user.WebServiceUrl, 500));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", user.OrganisationId));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}

		public DateTime GetModifiedDate(int syncType)
		{
			var modifiedDate = new DateTime();
			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSyncModifiedDate]");

				spCommand.Parameters.Add(DbManager.CreateParameter("@SyncType", syncType));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", SyncKey, 50));

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						modifiedDate = Convert.ToDateTime(dr["Modified"]);
					}
				}

				return modifiedDate;
			}
			finally
			{
				DbManager.Close();
			}
		}

		public void SaveTranslation(List<Translation> translation, DateTime lastRowModifiedDate)
		{
			try
			{
				DbManager.Open();

				var dtTranslation = new DataTable();
				dtTranslation.Columns.Add("LanguageId", typeof(int));
				dtTranslation.Columns.Add("TranslationRefId", typeof(int));
				dtTranslation.Columns.Add("TranslationText", typeof(string));

				foreach (var item in translation)
				{
					dtTranslation.Rows.Add(
						new object[] { item.LanguageId, item.TranslationRefId, item.TranslationText });
				}

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveTranslation]");
				spCommand.Parameters.Add(
					DbManager.CreateParameter("@TranslationList", dtTranslation, "PatientFlow.Translation"));
				spCommand.Parameters.Add(DbManager.CreateParameter("@LastRowModifiedDate", lastRowModifiedDate));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}

		public void SaveScreenControl(List<ScreenControl> screenControl, DateTime lastRowModifiedDate)
		{
			try
			{
				DbManager.Open();

				var dtScreenControl = new DataTable();
				dtScreenControl.Columns.Add("ControlId", typeof(int));
				dtScreenControl.Columns.Add("TranslationRefId", typeof(int));

				foreach (var item in screenControl)
				{
					dtScreenControl.Rows.Add(new object[] { item.ControlId, item.TranslationRefId });
				}

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveScreenControl]");
				spCommand.Parameters.Add(
					DbManager.CreateParameter("@ScreenControlList", dtScreenControl, "PatientFlow.ScreenControl"));
				spCommand.Parameters.Add(DbManager.CreateParameter("@LastRowModifiedDate", lastRowModifiedDate));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				DbManager.Close();
			}
		}

		internal void SaveProductKey(string productKey)
		{
			const string configType = "KioskDetails";
			List<KioskProductDetails> divertList = GetKioskConfiguration(configType);
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					const string syncservice = "SyncService";
					if (divertList == null || divertList.Count <= 0) return;
					DbManager.Open(connection);
					foreach (var kioskdetails in divertList.Where(kioskdetails => kioskdetails != null && kioskdetails.KioskGuid != null))
					{
						kioskdetails.SyncServiceGuid = productKey;
						SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SetKioskConfiguration]", connection);
						spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", "SyncDetails", 100));
						spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskdetails.KioskGuid, 50));
						spCommand.Parameters.Add(DbManager.CreateParameter("@Value", kioskdetails.ConvertToJsonString(), 4000));
						spCommand.Parameters.Add(DbManager.CreateParameter("@Username", syncservice, 50));
						spCommand.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, "Error While Saving Sync Service Product key", ex, productKey);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<Organisation> GetKioskOrganisation()
		{
			const string configType = "Organisation";
			var resultObj = new List<Organisation>();
			const string spKioskConfig = "[PatientFlow].[GetKioskConfiguration]";

			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					using (var spCommand = DbManager.GetSprocCommand(spKioskConfig, connection))
					{
						spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", configType, 100));
						spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", DBNull.Value, 50));

						using (var dr = spCommand.ExecuteReader())
						{
							while (dr.Read())
							{
								if (dr["Value"] != DBNull.Value)
								{
									string value = Convert.ToString(dr["Value"]);
									resultObj.AddRange(value.ConvertFromJsonString<List<Organisation>>());
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncKey);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return resultObj;
		}
		public List<KioskProductDetails> GetKioskConfiguration(string configType)
		{
			var resultObj = new List<KioskProductDetails>();
			const string spKioskConfig = "[PatientFlow].[GetKioskConfiguration]";

			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					using (var spCommand = DbManager.GetSprocCommand(spKioskConfig, connection))
					{
						spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", configType, 100));
						spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", DBNull.Value, 50));

						using (var dr = spCommand.ExecuteReader())
						{
							while (dr.Read())
							{
								if (dr["Value"] != DBNull.Value)
								{
									string value = Convert.ToString(dr["Value"]);
									resultObj.Add(value.ConvertFromJsonString<KioskProductDetails>());
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, SyncKey);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return resultObj;
		}

		public void SyncMembers(List<Member> members, int organisationId, SystemType systemType, string modifiedBy)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					var dtMember = new DataTable();
					dtMember.Columns.Add("MemberId", typeof(int));
					dtMember.Columns.Add("Title", typeof(string));
					dtMember.Columns.Add("FirstName", typeof(string));
					dtMember.Columns.Add("LastName", typeof(string));
					dtMember.Columns.Add("MemberIdentifierValue", typeof(string));
					dtMember.Columns.Add("ModifiedBy", typeof(string));
					dtMember.Columns.Add("WaitingTime", typeof(int));

					foreach (var item in members)
					{
						if (!string.IsNullOrEmpty(item.MemberIdentifiers))
							dtMember.Rows.Add(
								item.Id,
								item.Title,
								item.FirstName,
								item.LastName,
								item.MemberIdentifiers,
								modifiedBy,
								item.WaitingTime);
					}
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SyncMembers]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@MemberList", dtMember, "PatientFlow.Member"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SystemType", (int)systemType));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}
	}
}
