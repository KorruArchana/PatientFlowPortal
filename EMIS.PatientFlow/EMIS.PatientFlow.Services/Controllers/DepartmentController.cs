using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;

namespace EMIS.PatientFlow.Services.Controllers
{
     [Authorize(Roles = "Practice Admin, EMIS Super User, Egton Engineer")]
    public class DepartmentController : ApiController
    {
        private readonly ILoggerRepository _logger;
        private readonly IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository departmentReposiotory, ILoggerRepository loggerRepository)
        {
            _logger = loggerRepository;
            _repository = departmentReposiotory;
        }

        public int AddDepartment([FromBody]string value)
        {
            int result;
            try
            {
                Department department = value.ConvertFromJsonString<Department>();
                result = _repository.AddDepartment(department);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return result;
        }

        public Department GetDepartmentDetails(int departmentId)
        {
            Department department;
            try
            {
                department = _repository.GetDepartmentDetails(departmentId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return department;
        }
		

		public List<Department> GetDepartmentList(int organisationId)
        {
            List<Department> departmentList;
            try
            {
                departmentList = _repository.GetDepartmentList(organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return departmentList;
        }

        public IEnumerable<Department> GetDepartments()
        {
            try
            {
				if (!HttpContext.Current.User.IsInRole("Practice Admin"))
				{
					return _repository.GetDepartments();
				}
				else
				{
					return _repository.GetDepartmentsByUser();
				}
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [AcceptVerbs("GET")]
        public int DeleteDepartment(int departmentId)
        {
            int result = 0;
            try
            {
                result = _repository.DeleteDepartment(departmentId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
            }

            return result;
        }

        public int UpdateDepartment([FromBody]string value)
        {
            int result = 0;
            try
            {
                Department department = value.ConvertFromJsonString<Department>();
                result = _repository.UpdateDepartment(department);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
            }

            return result;
        }

        [AcceptVerbs("GET")]
        public bool ValidateDepartmentName(string departmentName, int departmentId, int organisationId)
        {
            bool status;
            _repository.ValidateDepartmentName(departmentName, departmentId, organisationId, out status);
            return status;
        }
	}
}
