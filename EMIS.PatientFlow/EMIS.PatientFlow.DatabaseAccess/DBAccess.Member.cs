using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public int AddMember(Member member, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[AddMember]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@Firstname", member.Firstname, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Surname", member.Surname, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Title", member.Title, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@LoginId", member.LoginId, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@WorkStartTime", member.WorkStartTime));
					spCommand.Parameters.Add(DbManager.CreateParameter("@WorkEndTime", member.WorkEndTime));
					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentId", member.DepartmentId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@GPCode", member.GpCode, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SecurityGroup", member.SecurityGroup, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@StaffCategory", member.StaffCategory, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SessionHolderId", member.SessionHolderId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public void AddSyncedMember(List<Member> member, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					var dtMember = new DataTable();
					dtMember.Columns.Add("SyncMemberId", typeof(int));
					dtMember.Columns.Add("MemberId", typeof(int));
					dtMember.Columns.Add("Firstname", typeof(string));
					dtMember.Columns.Add("Surname", typeof(string));
					dtMember.Columns.Add("Title", typeof(string));
					dtMember.Columns.Add("SessionHolderId", typeof(int));
					dtMember.Columns.Add("DepartmentId", typeof(int));
					dtMember.Columns.Add("LoginId", typeof(string));
					int rowcount = 1;
					foreach (var item in member)
					{
						dtMember.Rows.Add(rowcount++, item.Id, item.Firstname, item.Surname, item.Title, item.SessionHolderId,
							item.DepartmentId,item.LoginId);
					}
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveSyncedMember]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@MemberList", dtMember, "PatientFlow.Member"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", member.First().OrganisationId));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int DeleteMember(int memberId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteMember]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@MemberId", memberId));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public Member GetMemberDetails(int memberId)
		{
			var member = new Member();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetMemberDetails]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@MemberId", memberId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							member.Id = Convert.ToInt32(dr["MemberId"]);
							member.Firstname = Convert.ToString(dr["Firstname"]);
							member.Surname = dr["Surname"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Surname"]);
							member.Title = Convert.ToString(dr["Title"]);
							member.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
							member.DepartmentName = dr["DepartmentName"].ToString();
							member.OrganisationName = dr["OrganisationName"].ToString();
							member.OrganisationId = Convert.ToInt32(dr["OrganisationId"]);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return member;
		}

		public int UpdateMember(Member member, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateMemberDetails]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@MemberId", member.Id));
					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentId", member.DepartmentId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public bool ValidateSessionHolderId(int sessionHolderId, out bool result)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[ValidateSessionHolderId]", connection);
					var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);
					spCommand.Parameters.Add(DbManager.CreateParameter("@SessionHolderId", sessionHolderId));
					spCommand.Parameters.Add(outputParameter);
					spCommand.ExecuteScalar();
					result = Convert.ToBoolean(outputParameter.Value);
					return result;
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<int> GetSessionHolderIdOrganisation(int organisationId)
		{
			var sessionHolderIdList = new List<int>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetSessionHolderId_Organisation]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
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

		public Member ToMemberObject(DataRow row)
		{
			var member = new Member
			{
				Id = row["MemberId"] == DBNull.Value ? 0 : Convert.ToInt32(row["MemberId"]),
				Firstname = Convert.ToString(row["Firstname"]),
				Surname = Convert.ToString(row["Surname"]),
				Title = Convert.ToString(row["Title"]),
				LoginId = row["LoginId"] == DBNull.Value ? String.Empty : Convert.ToString(row["LoginId"]),
				SessionHolderId = Convert.ToInt32(row["SessionHolderId"]),
                OrganisationName = Convert.ToString(row["OrganisationName"])

            };
			return member;
		}

		public Member SetDivert(bool status, int sessionHolderId,int organisationId)
		{
			var member = new Member();
			var ds = new DataSet();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SetDivert]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@Status", status));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SessionHolderId", sessionHolderId));
                    spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
                    using (var da = new SqlDataAdapter())
					{
						da.SelectCommand = spCommand;
						da.Fill(ds);


						if (ds.Tables[0].Rows != null)
						{
							member = ToMemberObject(ds.Tables[0].Rows[0]);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return member;
		}
		public IEnumerable<Member> GetMembers()
		{
			var memberList = new List<Member>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetMembers]", connection);
					GetMembersDetails(memberList, spCommand);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return memberList;
		}

		public IEnumerable<Member> GetMembersByUser(string user)
		{
			var memberList = new List<Member>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetMembersByUser]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@User", user, 200));
					GetMembersDetails(memberList, spCommand);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return memberList;
		}

		private void GetMembersDetails(List<Member> memberList, SqlCommand spCommand)
		{
			using (SqlDataReader dr = spCommand.ExecuteReader())
			{
				while (dr.Read())
				{
					var member = new Member
					{
						Id = Convert.ToInt32(dr["MemberId"]),
						Firstname = dr["Firstname"].ToString(),
						Surname = dr["Surname"].ToString(),
						Title = dr["Title"].ToString(),
						IsDivertSet = TryParseBoolean(dr, "IsDivertSet"),
						SessionHolderId = Convert.ToInt32(dr["SessionHolderId"]),

						OrganisationId = TryParseInt(dr, "OrganisationId"),
						DepartmentId = TryParseInt(dr, "DepartmentId"),
						DepartmentName = TryParseString(dr, "DepartmentName"),
						OrganisationName = TryParseString(dr, "OrganisationName")
					};
					memberList.Add(member);
				}
			}
		}
		public List<Member> GetMemberByOrganisationId(int OrganisationId)
		{
			var memberList = new List<Member>();
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetMembersByOrganisationId]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", OrganisationId));
				SqlDataReader dr = spCommand.ExecuteReader();
				if (dr.HasRows)
				{
					while (dr.Read())
					{
						Member member = new Member();
						member.Id = Convert.ToInt32(dr["MemberId"]);
						member.Firstname = dr["Firstname"].ToString();
						member.Surname = dr["Surname"].ToString();
						member.Title = dr["Title"].ToString();
						member.SessionHolderId = Convert.ToInt32(dr["SessionHolderId"]);
						member.IsDivertSet = TryParseBoolean(dr, "IsDivertSet");
						member.DepartmentName = dr["DepartmentName"].ToString();
						member.OrganisationName = dr["OrganisationName"].ToString();
						member.DepartmentId = Convert.ToInt32(dr["DepartmentId"]);
						member.OrganisationId = Convert.ToInt32(dr["OrganisationId"]);
						memberList.Add(member);
					}
				}
			}
			finally
			{
				DbManager.Close();
			}
			return memberList;
		}

		public bool EnableMessageForMember(int alertId, int memberId, out bool result)
		{
			var memberList = new List<Member>();
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[EnableMessageForMember]");
				spCommand.Parameters.Add(DbManager.CreateParameter("@AlertId", alertId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@MemberId", memberId));
				var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);
				spCommand.Parameters.Add(outputParameter);
				spCommand.ExecuteScalar();
				result = Convert.ToBoolean(outputParameter.Value);
				return result;
			}
			finally
			{
				DbManager.Close();
			}
		}
	}
}
