using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Models;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.Web.Repository
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        public async Task<string> GetToken(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(
                    "grant_type",
                    "password"),
                new KeyValuePair<string, string>(
                    "username",
                    userName),
                new KeyValuePair<string, string>(
                    "Password",
                    password)
            };

            var data = await PostAsync<JToken>("Token", pairs);

            return data.Value<string>("access_token");
        }

        public async Task<List<string>> GetUserInRole(string userName)
        {
            return await GetAsync<List<string>>("api/Account/GetUserInRoles?userName=" + userName);
        }

        public async Task<List<string>> GetUserInRole(
            string userName,
            string token)
        {
            return await GetAsync<List<string>>("api/Account/GetUserInRoles?userName=" + userName, token);
        }

        public async Task<bool> RegisterUser(ApplicationUser userData)
        {
            await PostAsJsonAsync<bool>("api/Account/Register", userData);
            return true;
        }

        public async Task<IEnumerable<AuthUser>> GetUsers()
        {
            return await GetAsync<IEnumerable<AuthUser>>("api/Account/GetUsers");
        }

        public async Task<List<AuthRole>> GetRoles()
        {
            return await GetAsync<List<AuthRole>>(string.Format("api/Account/GetRoles"));
        }

        public async Task<bool> DeleteUser(string userName)
        {
            await DeleteAsync<bool>("api/Account/DeleteUser?userName=" + userName);

            return true;
        }

        public async Task<AuthUser> GetUser(string userName)
        {
            return await GetAsync<AuthUser>(string.Format("api/Account/GetUser?userName=" + userName));
        }

        public async Task<bool> UpdateUser(AuthUser user)
        {
            await PostAsJsonAsync<bool>("api/Account/UpdateUser", user);

            return true;
        }

        public async Task<bool> CreateRole(string roleName)
        {
            await GetAsync<dynamic>("api/Account/CreateRole?roleName=" + roleName);

            return true;
        }

        public async Task<bool> DeleteRole(string roleName)
        {
            await DeleteAsync<bool>("api/Account/DeleteRole?roleName=" + roleName);
            return true;
        }

        public async Task<bool> UpdateRole(AuthRole role)
        {
            await PostAsJsonAsync<bool>("api/Account/EditRole", role);
            return true;
        }

        public async Task<bool> ResetPassword(ResetPassword password)
        {
            await PostAsJsonAsync<bool>("api/Account/ResetPassword", password);
            return true;
        }

        public async Task<bool> SaveAccessMapping(OrganisationAccess accesses)
        {
            await PostAsJsonAsync<bool>("api/Organisation/SaveAccessMapping", accesses);
            return true;
        }

        public async Task<bool> DeleteOrganisationAuthUsers(string userName)
        {
            return await DeleteAsync<bool>("api/Organisation/DeleteOrganisationAuthUsers?userName=" + userName);

        }
        public async Task<List<KeyValuePair<int, string>>> GetOrganisationAccessRights(string userName)
        {
            return await GetAsync<List<KeyValuePair<int, string>>>("api/Organisation/GetOrganisationAccessRights?userName=" + userName);
        }
    }
}