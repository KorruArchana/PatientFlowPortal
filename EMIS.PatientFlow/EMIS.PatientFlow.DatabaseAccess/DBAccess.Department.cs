using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public int AddDepartment(Department department, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[AddDepartment]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentName", department.DepartmentName, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", department.OrganisationId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));

					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<Department> GetDepartmentListForOrganisation(int organisationId)
		{
			var departmentList = new List<Department>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetDepartmentListForOrganisation]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var department = new Department
							{
								Id = Convert.ToInt32(dr["DepartmentId"]),
								DepartmentName = dr["DepartmentName"].ToString(),
								LinkCount = Convert.ToInt32(dr["LinkCount"])
							};
							departmentList.Add(department);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}

				return departmentList;
			}
		}

		public IEnumerable<Department> GetDepartments()
		{
			var departmentList = new List<Department>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetDepartments]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var department = new Department
							{
								Id = Convert.ToInt32(dr["DepartmentId"]),
								DepartmentName = dr["DepartmentName"].ToString(),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								OrganisationName = dr["OrganisationName"].ToString(),
								LinkCount = Convert.ToInt32(dr["LinkCount"]),
								LinkedMessageCount = Convert.ToInt32(dr["LinkedMessageCount"])
							};
							departmentList.Add(department);
						}
					}

				}
				finally
				{
					DbManager.Close(connection);
				}

				return departmentList;
			}
		}

		public IEnumerable<Department> GetDepartmentsByUser(string user)
		{
			var departmentList = new List<Department>();
			using (var connection=DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetDepartmentsByUser]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@User", user, 200));
					using (SqlDataReader dr=spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var department = new Department
							{
								Id = Convert.ToInt32(dr["DepartmentId"]),
								DepartmentName = dr["DepartmentName"].ToString(),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								OrganisationName = dr["OrganisationName"].ToString(),
								LinkCount = Convert.ToInt32(dr["LinkCount"]),
								LinkedMessageCount = Convert.ToInt32(dr["LinkedMessageCount"])
							};
							departmentList.Add(department);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return departmentList;
		}

		

		public Department GetDepartmentDetails(int departmentId)
		{
			var department = new Department();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetDepartmentDetails]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentId", departmentId));
					SqlDataReader dr = spCommand.ExecuteReader();
					if (dr.HasRows)
					{
						while (dr.Read())
						{
							department.Id = Convert.ToInt32(dr[0]);
							department.DepartmentName = dr[1].ToString();
							department.OrganisationId = Convert.ToInt32(dr[2]);
							department.LinkCount = Convert.ToInt32(dr[3]);
							department.LinkedMessageCount = Convert.ToInt32(dr[4]);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return department;
		}

		public int DeleteDepartment(int departmentId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteDepartment]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentId", departmentId));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int UpdateDepartment(Department department, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateDepartment]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentName", department.DepartmentName, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentId", department.Id));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", department.OrganisationId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public bool ValidateDepartmentName(string departmentName, int departmentId, int organisationId, out bool result)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[ValidateDepartmentName]", connection);
					var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);
					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentName", departmentName, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@DepartmentId", departmentId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
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
	}
}
