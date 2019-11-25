using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public List<SiteMenu> GetSiteMenuByParentId(int parentId)
		{
			var menuItems = new List<SiteMenu>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSiteMenuByParentId]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ParentId", parentId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							menuItems.Add(new SiteMenu()
							{
								Id = Convert.ToInt32(dr["ID"]),
								Name = Convert.ToString(dr["NAME"]),
								ParentMenuId = Convert.ToInt32(dr["PARENT_ID"]),
								Depth = Convert.ToInt32(dr["Depth"]),
								Path = Convert.ToString(dr["Path"])
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
			return menuItems;
		}

		public int GetMenuBypermission(int parentId)
		{
			var menuItems = new List<SiteMenu>();
			int result = 0;
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetMenuBypermission]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ParentMenuId", parentId));

					result = Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return result;
		}

		public List<SiteMenu> GetSiteMenuList(int parentMenuId)
		{
			var sitemenulist = new List<SiteMenu>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSiteMenuList]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ParentMenuId", parentMenuId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var menuname = Convert.ToString(dr["MenuName"]);
							if (menuname.Contains("Appointments"))
								continue;

							var sitemenu = new SiteMenu
							{
								Id = Convert.ToInt32(dr["MenuId"]),
								Name = Convert.ToString(dr["MenuName"]),
								ParentMenuId = Convert.ToInt32(dr["ParentMenuId"]),
								NodeTypeId = (dr["NodeTypeId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NodeTypeId"]),
								NodeId = (dr["NodeId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NodeId"])
							};
							sitemenulist.Add(sitemenu);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return sitemenulist;
		}

		public List<SiteMenu> GetKioskDepartmentMemberTreeListDetails(Kiosk kiosk)
		{
			var siteMenuList = new List<SiteMenu>();
			DataTable dtOrganisations = new DataTable();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSelectedDepartmentMemberTreeList]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kiosk.Id));

					if (kiosk.OrganisationList != null && kiosk.OrganisationList.Count > 0)
						dtOrganisations = CreateListDataTable(kiosk.SelectedOrganisationList);

					spCommand.Parameters.Add(
						DbManager.CreateParameter(
							"@OrgIdList",
							dtOrganisations,
							"PatientFlow.List"));
					using (var dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var siteMenu = new SiteMenu()
							{
								Name = Convert.ToString(dr["MenuName"])
							};
							siteMenuList.Add(siteMenu);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return siteMenuList;
		}

		public List<SiteMenu> GetKioskDepartmentMemberTreeList(Kiosk kiosk)
		{
			var siteMenuList = new List<SiteMenu>();
			DataTable dtOrganisations = new DataTable();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetDepartmentMemberTreeList]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kiosk.Id));

					if (kiosk.OrganisationList != null && kiosk.OrganisationList.Count > 0)
						dtOrganisations = CreateListDataTable(kiosk.SelectedOrganisationList);

					spCommand.Parameters.Add(
						DbManager.CreateParameter(
							"@OrgIdList",
							dtOrganisations,
							"PatientFlow.List"));
					using (var dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var siteMenu = new SiteMenu()
							{
								Id = (dr["MenuId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MenuId"]),
								ParentMenuId = (dr["ParentMenuId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ParentMenuId"]),
								Name = Convert.ToString(dr["MenuName"]),
								NodeTypeId = (dr["NodeTypeId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NodeTypeId"]),
								NodeId = (dr["NodeId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NodeId"]),
								Selected = Convert.ToString(dr["Selected"]).ToLower()
							};
							siteMenuList.Add(siteMenu);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return siteMenuList;
		}

		public List<WebUser> GetPatientFlowUserByKioskKey(Guid kioskProductKey)
		{
			var users = new List<WebUser>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatientFlowUserByKioskKey]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ProductKey", kioskProductKey));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							users.Add(new WebUser()
							{
								DatabaseName = Convert.ToString(dr["OrganisationKey"]),
								IpAddress = Convert.ToString(dr["IPAddress"]),
								Password = Convert.ToString(dr["Password"]),
								SupplierId = Convert.ToString(dr["SupplierId"]),
								Username = Convert.ToString(dr["UserName"]),
								Type = Convert.ToString(dr["SystemType"]),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								WebServiceUrl = Convert.ToString(dr["WebServiceUrl"])
							});
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return users;
		}

		public List<SiteMenu> GetDepartmentMemberTreeListByOrganisationId(string organisationIds, out List<Site> site)
		{
			var siteMenuList = new List<SiteMenu>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					List<int> orgIds = organisationIds.Split(',').Select(int.Parse).ToList();
					int orgId = orgIds.LastOrDefault();
					site = GetSiteDetailsByOrganisationId(orgId);
					DataTable dtOrganisations = new DataTable();
					if (orgIds != null && orgIds.Count > 0)
						dtOrganisations = CreateListDataTable(orgIds);

					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetDepartmentMemberTreeListByOrgs]",connection);
					spCommand.Parameters.Add(
						DbManager.CreateParameter(
							"@OrgIdList",
							dtOrganisations,
							"PatientFlow.List"));
					using (var dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var siteMenu = new SiteMenu()
							{
								Id = (dr["MenuId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MenuId"]),
								ParentMenuId = (dr["ParentMenuId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ParentMenuId"]),
								Name = Convert.ToString(dr["MenuName"]),
								NodeTypeId = (dr["NodeTypeId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NodeTypeId"]),
								NodeId = (dr["NodeId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NodeId"]),
								Selected = Convert.ToString(dr["Selected"]).ToLower()
							};
							siteMenuList.Add(siteMenu);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}

			return siteMenuList;
		}

		public List<Site> GetSiteDetailsByOrganisationId(int organisationId)
		{
			var SiteList = new List<Site>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSites]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var site = new Site()
							{
								Id = (dr["SiteId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SiteId"]),
								SiteName = Convert.ToString(dr["SiteName"])
							};
							SiteList.Add(site);
						}
					}

				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return SiteList;
		}

		public List<SiteMenu> GetAlertDepartmentMemberTreeList(int alertId)
		{
			var siteMenuList = new List<SiteMenu>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAlertsDepartmentMemberTreeList]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var siteMenu = new SiteMenu()
							{
								Id = (dr["MenuId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MenuId"]),
								ParentMenuId = (dr["ParentMenuId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ParentMenuId"]),
								Name = Convert.ToString(dr["MenuName"]),
								NodeTypeId = (dr["NodeTypeId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NodeTypeId"]),
								NodeId = (dr["NodeId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NodeId"]),
								Selected = Convert.ToString(dr["Selected"]).ToLower()
							};
							siteMenuList.Add(siteMenu);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return siteMenuList;
		}

		public List<SiteMenu> GetAlertDepartmentMemberTreeListDetails(int alertId)
		{
			var siteMenuList = new List<SiteMenu>();

			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					//Open connection.
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSelectedAlertsDepartmentMemberTreeList]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var siteMenu = new SiteMenu()
							{
								Name = Convert.ToString(dr["MenuName"]),
							};
							siteMenuList.Add(siteMenu);
						}
					}
				}
				finally
				{
					//Close connection
					DbManager.Close(connection);
				}
			}
			return siteMenuList;
		}
	}
}
