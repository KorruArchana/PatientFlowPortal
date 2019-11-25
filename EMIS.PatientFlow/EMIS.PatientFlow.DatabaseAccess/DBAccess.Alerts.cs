using EMIS.PatientFlow.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace EMIS.PatientFlow.DatabaseAccess
{
    public partial class DbAccess
    {
        public IEnumerable<Alert> GetAlerts()
        {
            var alertsList = new List<Alert>();
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    //Open connection.
                    DbManager.Open(connection);
                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAlerts]", connection);
                    spCommand.CommandTimeout = 150;
                    using (SqlDataReader dr = spCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var alert = new Alert
                            {
                                Id = Convert.ToInt32(dr["AlertId"]),
                                AlertText = dr["AlertText"].ToString(),
                                AlertType = Convert.ToInt32(dr["AlertType"]),
                                OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
                                OrganisationName = dr["OrganisationName"].ToString(),
                                KioskName = dr["KioskName"].ToString(),
                                AlertsDisplayType = Convert.ToString(dr["AlertDisplayFormatTypeId"]),
                                Operation = Convert.ToString(dr["Operation"]),
                                Age1 = Convert.ToInt32(dr["Age1"]),
                                Age2 = Convert.ToInt32(dr["Age2"]),
                                Gender = Convert.ToString(dr["Gender"]),
                                DepartmentName = Convert.ToString(dr["DepartmentName"]) == "" ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                                MemberName = Convert.ToString(dr["MemberName"]) == "" ? string.Empty : Convert.ToString(dr["MemberName"]),
                            };
                            alertsList.Add(alert);

                        }
                    }
                    alertsList = AlertsTarget(alertsList);
                }
                finally
                {
                    //Close connection
                    DbManager.Close(connection);
                }
            }
            return alertsList;
        }

        public IEnumerable<Alert> GetAlertsByUser(string user)
        {
            var alertsList = new List<Alert>();
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {

                    DbManager.Open(connection);
                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAlertsByUser]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@User", user, 200));
                    spCommand.CommandTimeout = 120;
                    using (SqlDataReader dr = spCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var alert = new Alert
                            {
                                Id = Convert.ToInt32(dr["AlertId"]),
                                AlertText = dr["AlertText"].ToString(),
                                AlertType = Convert.ToInt32(dr["AlertType"]),
                                OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
                                OrganisationName = dr["OrganisationName"].ToString(),
                                KioskName = dr["KioskName"].ToString(),
                                AlertsDisplayType = Convert.ToString(dr["AlertDisplayFormatTypeId"]),
                                Operation = Convert.ToString(dr["Operation"]),
                                Age1 = Convert.ToInt32(dr["Age1"]),
                                Age2 = Convert.ToInt32(dr["Age2"]),
                                Gender = Convert.ToString(dr["Gender"]),
                                DepartmentName = Convert.ToString(dr["DepartmentName"]) == "" ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                                MemberName = Convert.ToString(dr["MemberName"]) == "" ? string.Empty : Convert.ToString(dr["MemberName"]),
                            };
                            alertsList.Add(alert);
                        }
                    }
                    alertsList = AlertsTarget(alertsList);
                }
                finally
                {
                    //Close connection
                    DbManager.Close(connection);
                }
            }
            return alertsList;
        }

        public List<int> GetSessionHolderIdForAlert(int alertId)
        {
            var sessionHolderIdList = new List<int>();
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSessionHolderListForAlert]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));
                    using (SqlDataReader dr = spCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            sessionHolderIdList.Add(Convert.ToInt32(dr["SessionHolderId"]));
                        }
                    }
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
            return sessionHolderIdList;
        }

        public List<int> GetOrganisationIdListForAlert(int alertId)
        {
            var organisationIdList = new List<int>();
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetOrganisationIdListForAlert]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));
                    using (SqlDataReader dr = spCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            organisationIdList.Add(Convert.ToInt32(dr["OrganisationId"]));
                        }
                    }
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
            return organisationIdList;
        }

        public List<Alert> GetAlertsListForOrganisation(List<int> organisationIds)
        {
            var alertsList = new List<Alert>();
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    var dt = new DataTable();
                    dt.Columns.Add("Id");

                    foreach (var item in organisationIds)
                        dt.Rows.Add(item);

                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAlertsListForOrganisationIds]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Organisations", dt, "[PatientFlow].[List]"));
                    using (SqlDataReader dr = spCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var alert = new Alert
                            {
                                Id = Convert.ToInt32(dr["AlertId"]),
                                AlertText = Convert.ToString(dr["AlertText"]),
                                AlertType = Convert.ToInt32(dr["AlertType"]),
                                Gender = Convert.ToString(dr["Gender"]),
                                Age2 = Convert.ToInt32(dr["Age2"]),
                                Age1 = Convert.ToInt32(dr["Age1"]),
                                AlertsDisplayType = Convert.ToString("AlertDisplayFormatTypeId"),
                                Operation = Convert.ToString(dr["Operation"])
                            };
                            alertsList.Add(alert);
                        }
                    }
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
            return alertsList;
        }

        public int AddAlerts(Alert alert, string user)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    var dtKiosk = CreateStringListValueTable(alert.LinkedKiosk);
                    var dtDepartments = CreateListDataTable(alert.SelectedDepartments);
                    var dtMembers = CreateListDataTable(alert.SelectedMembers);
                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[AddAlert]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertText", alert.AlertText, 300));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Gender", alert.Gender, 200));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Age1", alert.Age1));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Age2", alert.Age2));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertDisplayFormatTypeId", int.Parse(alert.AlertsDisplayType)));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Operation", alert.Operation, 200));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", alert.OrganisationId));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskList", dtKiosk, "PatientFlow.StringList"));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Departments", dtDepartments, "PatientFlow.List"));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Members", dtMembers, "PatientFlow.List"));
                    return Convert.ToInt32(spCommand.ExecuteScalar());
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public int UpdateAlerts(Alert alert, string user)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    var dtKiosk = CreateStringListValueTable(alert.LinkedKiosk);
                    var dtDepartments = CreateListDataTable(alert.SelectedDepartments);
                    var dtMembers = CreateListDataTable(alert.SelectedMembers);
                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateAlert]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertText", alert.AlertText, 300));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Gender", alert.Gender, 200));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Age1", alert.Age1));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Age2", alert.Age2));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertDisplayFormatTypeId", int.Parse(alert.AlertsDisplayType)));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Operation", alert.Operation, 200));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alert.Id));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", alert.OrganisationId));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@KioskList", dtKiosk, "PatientFlow.StringList"));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Departments", dtDepartments, "PatientFlow.List"));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@Members", dtMembers, "PatientFlow.List"));
                    return Convert.ToInt32(spCommand.ExecuteScalar());
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public Alert GetAlertDetails(int alertId)
        {
            var alert = new Alert();
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    var ds = new DataSet();
                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAlertDetails]", connection);

                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));

                    using (var da = new SqlDataAdapter())
                    {
                        da.SelectCommand = spCommand;
                        da.Fill(ds);

                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                alert = ToAlertObject(ds.Tables[0].Rows[0]);
                            }

                            if (ds.Tables[1].Rows.Count > 0)
                            {

                                List<string> sb = new List<string>();
                                foreach (DataRow dr in ds.Tables[1].Rows)
                                {
                                    var org = new Organisation
                                    {
                                        Id = Convert.ToInt32(dr["OrganisationId"]),
                                        OrganisationName = dr["OrganisationName"].ToString(),
                                        DatabaseName = dr["OrganisationKey"].ToString()
                                    };
                                    alert.Organisation.Add(org);
                                    if (dr["KioskGuid"] != DBNull.Value)
                                    {
                                        alert.LinkedKiosk.Add(dr["KioskGuid"].ToString());
                                        sb.Add(dr["KioskName"].ToString());
                                    }
                                }

                                alert.KioskName = sb.Count > 0 ? string.Join(",", sb.ToArray()) : "All Kiosks";

                            }
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[2].Rows)
                                {
                                    if (dr["LinkTypeId"] != DBNull.Value)
                                    {
                                        if (int.Parse(dr["LinkTypeId"].ToString()) == 2)
                                        {
                                            alert.SelectedDepartments.Add(dr["value"].ToString());
                                        }
                                        else if (int.Parse(dr["LinkTypeId"].ToString()) == 3)
                                        {
                                            alert.SelectedMembers.Add(dr["value"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
            return alert;
        }

        private Alert ToAlertObject(DataRow dr)
        {
            return new Alert
            {
                Id = Convert.ToInt32(dr["AlertId"]),
                AlertText = Convert.ToString(dr["AlertText"]),
                Gender = Convert.ToString(dr["Gender"]),
                Age2 = Convert.ToInt32(dr["Age2"]),
                Age1 = Convert.ToInt32(dr["Age1"]),
                AlertsDisplayType = Convert.ToString(dr["AlertDisplayFormatTypeId"]),
                Operation = Convert.ToString(dr["Operation"]),
                Modifed = Convert.ToDateTime(dr["Modified"]),
                ModifiedBy = Convert.ToString(dr["ModifiedBy"])
            };
        }

        public int DeleteAlert(int alertId)
        {
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);
                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteAlert]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));
                    return Convert.ToInt32(spCommand.ExecuteScalar());
                }
                finally
                {
                    DbManager.Close(connection);
                }
            }
        }

        public List<string> GetActiveGroupsForAlert(int alertId)
        {
            var groups = new List<string>();
            using (var connection = DbManager.GetNewConnection())
            {
                try
                {
                    DbManager.Open(connection);

                    SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetActiveGroupsForAlert]", connection);
                    spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));
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

                return groups;
            }
        }

        public DataTable CreateStringListValueTable(List<string> entitylist)
        {
            var dtlist = new DataTable("List");

            dtlist.Columns.Add("StringListId", typeof(int));
            dtlist.Columns.Add("Value", typeof(string));

            int i = 1;
            if (entitylist != null && entitylist.Count > 0)
            {
                foreach (var item in entitylist)
                {
                    DataRow dr = dtlist.NewRow();
                    dr["StringListId"] = i++;
                    dr["Value"] = item;
                    dtlist.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtlist.NewRow();
                dr["StringListId"] = i;
                dr["Value"] = null;
                dtlist.Rows.Add(dr);
            }

            return dtlist;
        }

        public DataTable CreateStringListValueTable(List<int> entitylist)
        {
            var dtlist = new DataTable("List");
            dtlist.Columns.Add("StringListId", typeof(int));
            dtlist.Columns.Add("Value", typeof(string));

            int i = 1;
            if (entitylist != null && entitylist.Count > 0)
            {
                foreach (var item in entitylist)
                {
                    DataRow dr = dtlist.NewRow();
                    dr["StringListId"] = i++;
                    dr["Value"] = item;
                    dtlist.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtlist.NewRow();
                dr["StringListId"] = i;
                dr["Value"] = null;
                dtlist.Rows.Add(dr);
            }

            return dtlist;
        }

        public DataTable CreateListValueTable(List<int> entitylist)
        {
            var dtlist = new DataTable("List");

            dtlist.Columns.Add("Value", typeof(string));
            if (entitylist != null && entitylist.Count > 0)
            {
                foreach (var item in entitylist)
                {
                    DataRow dr = dtlist.NewRow();

                    dr["Value"] = item;
                    dtlist.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtlist.NewRow();
                dr["Value"] = null;
                dtlist.Rows.Add(dr);
            }

            return dtlist;
        }
        public IEnumerable<Alert> GetAlertsByMember(int memberId)
        {
            return GetMemberAlerts(memberId, "[PatientFlow].[GetAlertsByMember]");
        }

        // delete this and combine getmemberalerts in above call
        public IEnumerable<Alert> GetAllAlertsByMemberOrganisation(int memberId)
        {
            return GetMemberAlerts(memberId, "[PatientFlow].[GetAlertsByMemberOrganisation]");
        }

        private IEnumerable<Alert> GetMemberAlerts(int memberId, string storedProcedure)
        {
            var alertsList = new List<Alert>();
            try
            {

                DbManager.Open();
                SqlCommand spCommand = DbManager.GetSprocCommand(storedProcedure);

                spCommand.Parameters.Add(DbManager.CreateParameter("@MemberId", memberId));

                using (SqlDataReader dr = spCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var alert = new Alert
                        {
                            Id = Convert.ToInt32(dr["AlertId"]),
                            AlertText = dr["AlertText"].ToString(),
                            IsMemberAlert = TryParseBoolean(dr, "IsMemberAlert"),
                            IsDeparmentAlert = TryParseBoolean(dr, "IsDeparmentAlert"),
                            IsOrganisationAlert = TryParseBoolean(dr, "IsOrganisationAlert")
                        };
                        alertsList.Add(alert);
                    }
                }
            }
            finally
            {
                DbManager.Close();
            }

            return alertsList;
        }

        private List<Alert> AlertsTarget(List<Alert> alertsList)
        {
            foreach (var item in alertsList)
            {
                string result = string.Empty;

                if (item.Gender == "None" && item.Operation == "None" && string.IsNullOrEmpty(item.DepartmentName) && string.IsNullOrEmpty(item.MemberName))
                {
                    result += "All patients";
                }
                else
                {

                    if (!string.IsNullOrWhiteSpace(item.MemberName))
                    {
                        result += "Some staff" + ", ";
                    }
                    if (!string.IsNullOrWhiteSpace(item.DepartmentName))
                    {
                        result += item.DepartmentName + ", ";
                    }
                    if (item.Gender != "None")
                    {
                        result += item.Gender == "F" ? "Female " : "Male ";
                    }
                    if (item.Operation == "Between")
                    {
                        result += item.Operation + " " + item.Age1 + " and " + item.Age2;
                    }
                    else if (item.Operation != "None")
                    {
                        result += GetTargets(item);
                    }
                }
                item.Target = result.TrimEnd().TrimEnd(',');
            }
            return alertsList;
        }

        private static string GetTargets(Alert item)
        {
            return ((item.Age1 != 0) ? item.Age1 : item.Age2) + (item.Operation == "GreaterThan" ? " and over" : " and under");
        }
    }
}
