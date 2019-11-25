using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
	public class DepartmentRepository : BaseRepository, IDepartmentRepository
	{
		public int AddDepartment(Department department)
		{
			try
			{
				return DbAccess.AddDepartment(department, CurrentUser);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public List<Department> GetDepartmentList(int organisationId)
		{
			try
			{
				return DbAccess.GetDepartmentListForOrganisation(organisationId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Department>();
			}
		}

		public IEnumerable<Department> GetDepartments()
		{
			try
			{
				return DbAccess.GetDepartments();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Department>();
			}
		}

		public IEnumerable<Department> GetDepartmentsByUser()
		{
			try
			{
				return DbAccess.GetDepartmentsByUser(CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Department>();
			}
		}


		public Department GetDepartmentDetails(int departmentId)
		{
			try
			{
				Department department = DbAccess.GetDepartmentDetails(departmentId);
				return department;
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new Department();
			}
		}

		public int DeleteDepartment(int departmentId)
		{
			try
			{
				return DbAccess.DeleteDepartment(departmentId);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
			
		}

		public int UpdateDepartment(Department department)
		{
			try
			{
				return DbAccess.UpdateDepartment(department, CurrentUser);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
		}

		public bool ValidateDepartmentName(string departmentName, int departmentId, int organisationId, out bool status)
		{
			try
			{
				return DbAccess.ValidateDepartmentName(departmentName, departmentId, organisationId, out status);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				status = false;
				return false;
			}
			
		}

	}
}
