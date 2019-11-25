using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using System.Linq;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public IEnumerable<Organisation> GetOrganisationList(List<int> organisationIds)
		{
			var organisationList = new List<Organisation>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					var dt = new DataTable();
					dt.Columns.Add("Id");

					foreach (var item in organisationIds)
						dt.Rows.Add(item);

					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationsByIds]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@Organisations", dt, "[PatientFlow].[List]"));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var organisation = new Organisation
							{
								Id = Convert.ToInt32(dr["OrganisationId"]),
								OrganisationName =Convert.ToString(dr["OrganisationName"]),
								SystemTypeId = TryParseInt(dr, "SystemTypeId"),
								SiteNumber = TryParseString(dr, "SiteNumber"),
							};
							organisationList.Add(organisation);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return organisationList;
		}

		public IEnumerable<Organisation> GetOrganisationListForKiosk(int kioskId)
		{
			var organisationList = new List<Organisation>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationListForKiosk]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var organisation = new Organisation
							{
								Id = Convert.ToInt32(dr["OrganisationId"]),
								OrganisationName =Convert.ToString(dr["OrganisationName"]),
								SystemTypeId = TryParseInt(dr, "SystemTypeId"),
								SiteNumber = TryParseString(dr, "SiteNumber"),
							};
							organisationList.Add(organisation);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return organisationList;
		}

		public IEnumerable<Organisation> GetOrganisationListForAlert(int alertId)
		{
			var organisationList = new List<Organisation>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationListForAlert]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var organisation = new Organisation
							{
								Id = Convert.ToInt32(dr["OrganisationId"]),
								OrganisationName =Convert.ToString(dr["OrganisationName"])
							};
							organisationList.Add(organisation);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return organisationList;
		}

		public IEnumerable<Organisation> GetOrganisations()
		{
			var organisationList = new List<Organisation>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisations]", connection);
					organisationList = ReadOrganisationList(organisationList, spCommand);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return organisationList;
		}

		public IEnumerable<Organisation> GetOrganisationsByUser(string userName)
		{
			var organisationList = new List<Organisation>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationsByUser]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@UserName", userName, 128));
					organisationList = ReadOrganisationList(organisationList, spCommand);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return organisationList;
		}

		private List<Organisation> ReadOrganisationList(List<Organisation> organisationList, SqlCommand spCommand)
		{
			using (SqlDataReader dr = spCommand.ExecuteReader())
			{
				while (dr.Read())
				{
					var organisation = new Organisation
					{
						Id = Convert.ToInt32(dr["OrganisationId"]),
						OrganisationName =Convert.ToString(dr["OrganisationName"]),
						SystemTypeId =TryParseInt(dr, "SystemTypeId"),
						SystemType =TryParseString(dr, "SystemType"),
						SiteNumber =TryParseString(dr, "SiteNumber"),
						LinkCount = Convert.ToInt32(dr["LinkCount"])
					};
					organisationList.Add(organisation);
				}
			}

			return organisationList;
		}

		public IEnumerable<Organisation> GetOrganisationsForDropDown()
		{
			var organisationList = new List<Organisation>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationsForDropDown]", connection);
					organisationList = ReadOrganisationListForDropDown(organisationList, spCommand);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return organisationList;
		}

		public IEnumerable<Organisation> GetOrganisationsByUserForDropDown(string userName)
		{
			var organisationList = new List<Organisation>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationsByUserForDropDown]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@UserName", userName, 128));
					organisationList = ReadOrganisationListForDropDown(organisationList, spCommand);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return organisationList;
		}

		private List<Organisation> ReadOrganisationListForDropDown(List<Organisation> organisationList, SqlCommand spCommand)
		{
			using (SqlDataReader dr=spCommand.ExecuteReader())
			{
				while (dr.Read())
				{
					var organisation = new Organisation()
					{
						Id = Convert.ToInt32(dr["OrganisationId"]),
						OrganisationName =Convert.ToString(dr["OrganisationName"]),
						OrganisationKey = dr["OrganisationKey"].ToString(),
						SystemTypeId=Convert.ToInt32(dr["SystemTypeId"])
					};
					organisationList.Add(organisation);
				}
			}
			return organisationList;
		}

		public Organisation GetOrganisationDetails(int organisationId)
		{
			var organisation = new Organisation { User = new WebUser() };

			if (organisationId > 0)
			{
				using (var connection = DbManager.GetNewConnection())
				{
					try
					{
						DbManager.Open(connection);
						var ds = new DataSet();
						SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationDetails]", connection);
						spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
						using (var da = new SqlDataAdapter())
						{
							da.SelectCommand = spCommand;
							da.Fill(ds);
							ToOrganisationObject(ds, organisation);

							if (ds.Tables[1].Rows.Count > 0)
							{
								organisation.SiteList = new List<Site>();
								foreach (DataRow dr in ds.Tables[1].Rows)
								{
									Site site = new Site();
									site.Id = GetIntValue(dr, "SiteId");
									site.SiteDbId = GetLongValue(dr, "SiteDBID");
									site.SiteName = GetStringValue(dr, "SiteName");
									site.OrganisationId = organisation.Id;
									organisation.SiteList.Add(site);
								}
							}
						}

					}
					finally
					{
						DbManager.Close(connection);
					}
				}
			}
			organisation.SystemTypeList = GetSystemTypeList();

			return organisation;
		}

		public Organisation GetOrganisationDetail(int organisationId)
		{
			var organisation = new Organisation { User = new WebUser() };

			if (organisationId <= 0) return organisation;
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					var ds = new DataSet();
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationDetails]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					using (var da = new SqlDataAdapter())
					{
						da.SelectCommand = spCommand;
						da.Fill(ds);
						ToOrganisationObject(ds, organisation);
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return organisation;
		}

		private void ToOrganisationObject(DataSet ds, Organisation organisation)
		{
			if (ds.Tables[0].Rows.Count > 0)
			{
				var datarow = ds.Tables[0].Rows[0];
				organisation.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["OrganisationId"]);
				organisation.OrganisationName = ds.Tables[0].Rows[0]["OrganisationName"].ToString();
				organisation.SystemTypeId = GetIntValue(datarow, "SystemTypeId");
				organisation.SystemType = GetStringValue(datarow, "SystemType");
				organisation.SiteNumber = GetStringValue(datarow, "SiteNumber");
				organisation.ModifiedDate = Convert.ToDateTime(datarow["Modified"]);
                organisation.LinkCount = Convert.ToInt32(ds.Tables[0].Rows[0]["LinkCount"]);
				organisation.User.IpAddress = GetStringValue(datarow, "IPAddress");
				organisation.User.InternalIpAddress = GetStringValue(datarow, "InternalIPAddress");
				organisation.User.SupplierId = GetStringValue(datarow, "SupplierId");
				organisation.User.Username = GetStringValue(datarow, "Username");
				organisation.User.Password = GetStringValue(datarow, "Password");
				organisation.DatabaseName = GetStringValue(datarow, "OrganisationKey");
				organisation.User.WebServiceUrl = GetStringValue(datarow, "WebServiceUrl");
			}
		}

		public List<Entity> GetSystemTypeList()
		{
			var systemTypeList = new List<Entity>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationSystemTypeList]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var systemType = new Entity
							{
								Id = Convert.ToInt32(dr["SystemTypeId"]),
								Name =Convert.ToString(dr["SystemType"])
							};
							systemTypeList.Add(systemType);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return systemTypeList;
		}

		public DataTable CreateOrganisationSitesDataTable(List<Site> entitylist)
		{
			var dtlist = new DataTable("PatientFlow.OrganisationSites");

			dtlist.Columns.Add("OrganisationId", typeof(int));
			dtlist.Columns.Add("SiteDBID", typeof(long));
			dtlist.Columns.Add("SiteName", typeof(string));
			if (entitylist != null && entitylist.Count > 0)
			{
				foreach (var item in entitylist)
				{
					DataRow dr = dtlist.NewRow();
					dr["OrganisationId"] = item.OrganisationId;
					dr["SiteDBID"] = item.SiteDbId;
					dr["SiteName"] = item.SiteName;
					dtlist.Rows.Add(dr);
				}
			}

			return dtlist;
		}

		public int AddOrganisation(Organisation organisation, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					var dtSites = CreateOrganisationSitesDataTable(organisation.SiteList);
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[AddOrganisation]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationName", organisation.OrganisationName, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SystemTypeId", organisation.SystemTypeId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SiteNumber", organisation.SiteNumber, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationKey", organisation.DatabaseName, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@UserName", organisation.User.Username, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Password", organisation.User.Password, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@IPAddress", organisation.User.IpAddress, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@InternalIPAddress", organisation.User.InternalIpAddress, 40));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SupplierId", organisation.User.SupplierId, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@WebServiceUrl", organisation.User.WebServiceUrl, 500));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Branches", dtSites, "PatientFlow.OrganisationSites"));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int DeleteOrganisation(int organisationId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteOrganisation]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int UpdateOrganisation(Organisation organisation, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					var dtSites = CreateOrganisationSitesDataTable(organisation.SiteList);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateOrganisationDetails]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisation.Id));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationName", organisation.OrganisationName, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationKey", organisation.DatabaseName, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SiteNumber", organisation.SiteNumber, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Username", organisation.User.Username, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Password", organisation.User.Password, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@IPAddress", organisation.User.IpAddress, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@InternalIPAddress", organisation.User.InternalIpAddress, 40));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SupplierId", organisation.User.SupplierId, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@WebServiceUrl", organisation.User.WebServiceUrl, 500));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Branches", dtSites, "PatientFlow.OrganisationSites"));

					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public WebUser GetPatientFlowUser(int organisationId)
		{
			var webUser = new WebUser();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatientFlowUser]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							webUser.IpAddress = Convert.ToString(dr["IPAddress"]);
							webUser.SupplierId = Convert.ToString(dr["SupplierId"]).Decrypt();
							webUser.DatabaseName = Convert.ToString(dr["OrganisationKey"]);
							webUser.Username = Convert.ToString(dr["Username"]).Decrypt();
							webUser.Password = Convert.ToString(dr["Password"]).Decrypt();
							webUser.Type = Convert.ToString(dr["SystemType"]);
							webUser.WebServiceUrl = TryParseString(dr, "WebServiceUrl");
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return webUser;
		}

		public void ValidateOrganisationName(string organisationName, int organisationId, out bool result)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[ValidateOrganisationName]", connection);
					var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationName", organisationName, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					spCommand.Parameters.Add(outputParameter);
					spCommand.ExecuteScalar();
					result = Convert.ToBoolean(outputParameter.Value);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public void ValidateOrganisationKey(string organisationKey, int organisationId, out bool result)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[ValidateOrganisationKey]", connection);
					var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationKey", organisationKey, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					spCommand.Parameters.Add(outputParameter);
					spCommand.ExecuteScalar();
					result = Convert.ToBoolean(outputParameter.Value);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public void ValidateOrganisationSiteNumber(string organisationSiteNumber, int organisationId, out bool result)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[ValidateOrganisationSiteNumber]", connection);
					var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationSiteNumber", organisationSiteNumber, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					spCommand.Parameters.Add(outputParameter);
					spCommand.ExecuteScalar();
					result = Convert.ToBoolean(outputParameter.Value);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<KeyValuePair<int, string>> GetOrganisationAccessRights(string userName)
		{
			var organisationList = new List<KeyValuePair<int, string>>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationAccessRights]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@UserName", userName, 128));

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
					DbManager.Close(connection);
				}
			}
			return organisationList;
		}

        public bool DeleteOrganisationAuthUsers(string userName)
        {            
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);
                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteOrganisationAuthUsers]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@UserName", userName, 128));

                    return Convert.ToInt32(spCommand.ExecuteScalar()) > 0;
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }            
        }

        public void SaveAccessMapping(string userName, List<int> accesses, string modified)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{

					var dtAccess = new DataTable();
					dtAccess.Columns.Add("Id", typeof(int));
					foreach (var item in accesses)
						dtAccess.Rows.Add(item);

					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveAccessMapping]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@UserName", userName, 128));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AccessList", dtAccess, "[PatientFlow].[List]"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", modified, 50));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<string> GetIPAddresses()
        {
            List<string> ipAddresses = new List<string>();
			using (var connection=DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spcommand = DbManager.GetSprocCommand("[PatientFlow].[GetIpAddress]", connection);
					using (SqlDataReader dr=spcommand.ExecuteReader())
					{
						while (dr.Read())
						{
							ipAddresses.Add(Convert.ToString((dr["IPAddress"])));
						}
					}
				}
				finally
				{
					DbManager.Close();
				}
			}
            return ipAddresses;
        }

		public List<AuthUser> GetAuthUsers(string user)
		{
			List<AuthUser> users = new List<AuthUser>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAuthUsers]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@User", user, 128));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var authuser = new AuthUser()
							{
								UserName = Convert.ToString(dr["UserName"]),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								OrganisationName = Convert.ToString(dr["OrganisationName"])
							};
							users.Add(authuser);
						}
					}
				}
				finally
				{
					DbManager.Close();
				}
			}
			return users;
		}
	}
}

