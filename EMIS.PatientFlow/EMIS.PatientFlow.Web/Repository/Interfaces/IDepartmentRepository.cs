using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentDetails(int departmentId);
        Task<List<Department>> GetDepartments();
        Task<int> AddDepartment(Department department);
        Task<int> UpdateDepartment(Department member);
        Task<int> DeleteDepartment(int nodeId);
        Task<List<Department>> GetDepartmentList(int organisationId);
        Task<bool> ValidateDepartmentName(string departmentName, int id, int organisationId);
	}
}