using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface IDepartmentRepository
    {
        int AddDepartment(Department department);
        Department GetDepartmentDetails(int departmentId);
        int DeleteDepartment(int departmentId);
        List<Department> GetDepartmentList(int organisationId);
		int UpdateDepartment(Department department);
        bool ValidateDepartmentName(string departmentName, int departmentId, int organisationId, out bool status);
		IEnumerable<Department> GetDepartments();
		IEnumerable<Department> GetDepartmentsByUser();
	}
}
