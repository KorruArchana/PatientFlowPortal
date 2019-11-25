using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Common.Extensions;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public bool IsExistSyncService(Guid productKey)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[IsExistSyncService]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", productKey));
					var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);

					spCommand.Parameters.Add(outputParameter);

					SqlDataReader dr = spCommand.ExecuteReader();
					dr.Close();

					return Convert.ToBoolean(outputParameter.Value);
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
		}

		public List<KeyValuePair<int, string>> GetSyncServiceOrganisations(Guid productKey)
		{
			var organisationList = new List<KeyValuePair<int, string>>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSyncServiceOrganisationList]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", productKey));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							organisationList.Add(new KeyValuePair<int, string>(
								Convert.ToInt32(dr["OrganisationId"]),
								Convert.ToString(dr["OrganisationName"])));
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}

			return organisationList;
		}

		public List<string> GetSyncServiceConnections(int organisationId, char connectionType)
		{
			var connections = new List<string>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSyncServiceConnection]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Type", Convert.ToString(connectionType), 1));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							connections.Add(Convert.ToString(dr["ConnectionId"]));
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}

			return connections;
		}

		public List<WebUser> GetPatientFlowUsers(Guid productKey)
		{
			var users = new List<WebUser>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatientFlowUserByProductKey]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", productKey));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							users.Add(new WebUser()
							{
								DatabaseName = Convert.ToString(dr["OrganisationKey"]),
								IpAddress = GetIPAddress(dr),
								Password = Convert.ToString(dr["Password"]),
								SupplierId = Convert.ToString(dr["SupplierId"]),
								Username = Convert.ToString(dr["UserName"]),
                                Type = GetSystemType(dr),
                                OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								WebServiceUrl = Convert.ToString(dr["WebServiceUrl"])
							});
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}

			return users;
		}

        private static string GetSystemType(SqlDataReader dr)
        {
            var type = Convert.ToString(dr["SystemType"]);

            if (type == SystemType.EmisPcsLan.GetDisplayName())
            {
                return SystemType.EmisPcs.GetDisplayName();
            }

            return type;
        }

        private static string GetIPAddress(SqlDataReader dr)
		{
			return dr["InternalIPAddress"] == DBNull.Value ? Convert.ToString(dr["IPAddress"]) : Convert.ToString(dr["InternalIPAddress"]);
		}

		public List<SyncService> GetSyncServices(string organisation, int pageNo, int pageSize, out int recordCount)
		{
			var services = new List<SyncService>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSyncServices]", connection);
					var outputParameter = DbManager.CreateOutputParameter("@TotalCount", SqlDbType.BigInt);
					spCommand.Parameters.Add(DbManager.CreateParameter("@PageNo", pageNo));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PageSize", pageSize));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationName", organisation, 100));

					spCommand.Parameters.Add(outputParameter);

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							services.Add(new SyncService()
							{
								IsActive = Convert.ToBoolean(dr["IsActivated"]),
								OrganisationName = Convert.ToString(dr["OrganisationName"]),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								ProductKey = new Guid(Convert.ToString(dr["ProductKey"])),
								IsConnected = Convert.ToBoolean(dr["IsConnected"]),
								Id = Convert.ToInt32(dr["SyncServiceId"]),
								KioskId = dr["KioskId"] == DBNull.Value ? -1 : Convert.ToInt32(dr["KioskId"]),
								KioskName = dr["KioskName"] == DBNull.Value ? "" : Convert.ToString(dr["KioskName"]),
							});
						}
					}

					recordCount = Convert.ToInt32(outputParameter.Value);
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return services;
		}

		public SyncService GetSyncServiceById(int serviceId)
		{
			var service = new SyncService();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSyncServiceById]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@SyncServiceId", serviceId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							service = new SyncService()
							{
								IsActive = Convert.ToBoolean(dr["IsActivated"]),
								OrganisationName = Convert.ToString(dr["OrganisationName"]),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								ProductKey = new Guid(Convert.ToString(dr["ProductKey"])),
								IsConnected = Convert.ToBoolean(dr["IsConnected"]),
								KioskId = dr["KioskId"] == DBNull.Value ? -1 : Convert.ToInt32(dr["KioskId"])
							};
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return service;
		}

		public void SaveSyncService(SyncService service, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveSyncService]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@SyncServiceId", service.Id));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", service.ProductKey));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", service.OrganisationId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Active", service.IsActive));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
		}

		public void SaveSyncService(Guid productKey, List<int> organisationIds, bool isActive, string user, int kioskId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					var dt = new DataTable();
					dt.Columns.Add("Id");

					foreach (var item in organisationIds)
						dt.Rows.Add(item);

					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveSyncServiceOrganisations]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", productKey));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Organisations", dt, "[PatientFlow].[List]"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Active", isActive));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
		}

		public int SaveSyncServiceConnection(Guid productKey, Guid connectionId, char connectionType, bool isDisconnect)
		{
			int result;
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveSyncServiceConnection]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", productKey));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ConnectionId", connectionId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Disconnect", isDisconnect));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Type", Convert.ToString(connectionType), 1));
					var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Int);

					spCommand.Parameters.Add(outputParameter);
					spCommand.ExecuteNonQuery();
					result = (int)outputParameter.Value;
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}

			return result;
		}

		public void DeleteSyncService(int serviceId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteSyncServiceById]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@SyncServiceId", serviceId));

					spCommand.ExecuteNonQuery();
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
		}

		public void RemoveClientConnections(char type)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("PatientFlow.RemoveClientConnections", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@Type", type.ToString(), 1));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
		}

		public List<string> GetActiveGroupsForSyncService()
		{
			var groups = new List<string>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetActiveGroupsForSyncService]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							groups.Add(Convert.ToString(dr["OrganisationName"]));
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return groups;
		}
	}
}
