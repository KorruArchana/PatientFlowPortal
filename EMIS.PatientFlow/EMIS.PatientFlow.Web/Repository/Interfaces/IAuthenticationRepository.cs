using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.Web.Repository.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<bool> DeleteUser(string userName);
        Task<string> GetToken(string userName, string password);
        Task<List<string>> GetUserInRole(string userName);
        Task<List<string>> GetUserInRole(string userName, string token);
        Task<List<AuthRole>> GetRoles();
        Task<bool> RegisterUser(ApplicationUser user);
        Task<bool> UpdateUser(AuthUser user);
        Task<bool> CreateRole(string roleName);
        Task<bool> DeleteRole(string roleName);
        Task<bool> UpdateRole(AuthRole role);
        Task<bool> ResetPassword(ResetPassword password);
        Task<IEnumerable<AuthUser>> GetUsers();
        Task<AuthUser> GetUser(string userName);
        Task<List<KeyValuePair<int, string>>> GetOrganisationAccessRights(string userName);
        Task<bool> SaveAccessMapping(OrganisationAccess accesses);
        Task<bool> DeleteOrganisationAuthUsers(string userName);
   }
}