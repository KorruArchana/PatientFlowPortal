using System.Collections.Generic;
using System.Threading.Tasks;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository.Interfaces;

namespace EMIS.PatientFlow.Web.Repository
{
    public class DepartmentRepository : BaseRepository,  IDepartmentRepository
    {
        public async Task<Department> GetDepartmentDetails(int departmentId)
        {
            return await GetAsync<Department>("api/Department/GetDepartmentDetails?departmentId=" + departmentId); 
        }

        public async Task<int> AddDepartment(Department department)
        {
            var jsonDepartment = department.ConvertToJsonString();
            return await PostAsJsonAsync<int>("api/Department/AddDepartment", jsonDepartment);
        }

        public async Task<int> UpdateDepartment(Department member)
        {
            var jsonDepartment = member.ConvertToJsonString();
            return await PostAsJsonAsync<int>("api/Department/UpdateDepartment", jsonDepartment);
        }

        public async Task<int> DeleteDepartment(int nodeId)
        {
            return await GetAsync<int>("api/Department/DeleteDepartment?departmentId=" + nodeId);
        }

        public async Task<List<Department>> GetDepartmentList(int organisationId)
        {
          return
            await GetAsync<List<Department>>("api/Department/GetDepartmentList?organisationId=" + organisationId);
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await GetAsync<List<Department>>(string.Format("api/Department/GetDepartments"));
        }

		public async Task<bool> ValidateDepartmentName(string departmentName, int id, int organisationId)
        {
            return
                await
                    GetAsync<bool>(
                        "api/Department/ValidateDepartmentName?departmentName=" + departmentName + "&organisationId="
                        + organisationId + "&departmentId=" + id);
        }
	}
}