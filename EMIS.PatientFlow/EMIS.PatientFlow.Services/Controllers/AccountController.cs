using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using EMIS.PatientFlow.Common.Validations;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories;
using EMIS.PatientFlow.Services.Models;
using EMIS.PatientFlow.Services.Providers;
using EMIS.PatientFlow.Services.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;


namespace EMIS.PatientFlow.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string _localLoginProvider = "Local";

        public AccountController()
            : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
        {
        }

        public AccountController(
            UserManager<ApplicationUser> userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [Authorize(Roles = "EMIS Super User,Egton Engineer, Practice Admin")]
        [Route("GetRoles")]
        public async Task<IEnumerable<AuthRole>> GetRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!HttpContext.Current.User.IsInRole("Practice Admin"))
            {
                return await Task.Run<IEnumerable<AuthRole>>(() =>
                {
                    return roleManager.Roles.Select(x => new AuthRole()
                    {
                        UniqueId = x.Id,
                        Name = x.Name
                    });
                });
            }
            else
            {
                return await Task.Run<IEnumerable<AuthRole>>(() =>
                {
                    return roleManager.Roles.Where(a => a.Name.Equals("Practice Admin", StringComparison.InvariantCultureIgnoreCase)).Select(x => new AuthRole()
                    {
                        UniqueId = x.Id,
                        Name = x.Name
                    });
                });
            }
        }

        [Route("GetUserInRoles")]
        public async Task<IEnumerable<string>> GetUserInRoles(string userName)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(userName);

            return await UserManager.GetRolesAsync(user.Id);
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [Route("CreateRole")]
        [HttpGet]
        public async Task<IHttpActionResult> CreateRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            IdentityResult result = await roleManager.CreateAsync(new IdentityRole(roleName.Trim()));

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [Route("DeleteRole")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole(string roleName)
        {
            if (roleName.ToUpper() == "ADMINISTRATOR" ||
                roleName.ToUpper() == "STANDARD USER" ||
                roleName.ToUpper() == "EMIS SUPER USER")
            {
                return BadRequest("This action isn't allowed");
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var isExist = await roleManager.RoleExistsAsync(roleName);

            if (isExist)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                IdentityResult result = await roleManager.DeleteAsync(role);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }
            }

            return Ok();
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [Route("EditRole")]
        public async Task<IHttpActionResult> EditRole(AuthRole role)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var data = await roleManager.FindByIdAsync(role.UniqueId);
            data.Name = role.Name.Trim();

            await roleManager.UpdateAsync(data);

            return Ok();
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [Route("AddUserToRole")]
        public async Task<IHttpActionResult> AddUserToRole(string userName, string roleName)
        {
            ApplicationUser user = UserManager.FindByName(userName);

            if (!UserManager.IsInRole(user.Id, roleName))
            {
                IdentityResult result = await UserManager.AddToRoleAsync(user.Id, roleName);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }
            }

            return Ok();
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [Route("DeleteRoleForUser")]
        public async Task<IHttpActionResult> DeleteRoleForUser(string userName, string roleName)
        {
            ApplicationUser user = UserManager.FindByName(userName);

            if (UserManager.IsInRole(user.Id, roleName))
            {
                IdentityResult result = await UserManager.RemoveFromRoleAsync(user.Id, roleName);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }
            }

            return Ok();
        }

		[Authorize(Roles = "EMIS Super User,Egton Engineer,Practice Admin")]
		public async Task<IHttpActionResult> UpdateUser(AuthUser user)
		{
			ApplicationUser appUser = UserManager.FindByName(user.UserName.Trim());

            appUser.Email = user.Email.Trim();

            IdentityResult result = await UserManager.UpdateAsync(appUser);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }
            var roles = await GetRoles();
            var updatedRoles = roles.Where(x => user.Roles.Contains(x.UniqueId));
            //add user to  roles
            var authRoles = updatedRoles as IList<AuthRole> ?? updatedRoles.ToList();
            foreach (var role in authRoles.Where(role => !UserManager.IsInRole(appUser.Id, role.Name)))
            {
                result = await UserManager.AddToRoleAsync(appUser.Id, role.Name);

                errorResult = GetErrorResult(result);

                if (errorResult != null) return errorResult;
            }

            var userInRoles = await GetUserInRoles(user.UserName.Trim());

            var rolesToRemove = userInRoles.Where(x => authRoles.All(y => y.Name != x));
            //remove user from roles
            foreach (var role in rolesToRemove.Where(role => UserManager.IsInRole(appUser.Id, role)))
            {
                result = await UserManager.RemoveFromRoleAsync(appUser.Id, role);

                errorResult = GetErrorResult(result);

                if (errorResult != null) return errorResult;
            }

            return Ok();
        }

        [Authorize]
        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUser(string userName)
        {
            if (userName.ToUpper() == "ADMINISTRATOR")
            {
                return BadRequest("This action isn't allowed");
            }

            if (HttpContext.Current.User.Identity.GetUserName() == userName)
            {
                return BadRequest("This action isn't allowed");
            }

            if (HttpContext.Current.User.IsInRole("Practice Admin"))
            {
                IOrganisationRepository _repository = new OrganisationRepository();
                var usersByRole = _repository.GetAuthUsersForPracticeAdmin();
                if (!usersByRole.Exists(a => a.UserName == userName))
                    return BadRequest("This action isn't allowed");
            }
            ApplicationUser user = UserManager.FindByName(userName.Trim());

            IdentityResult result = await UserManager.DeleteAsync(user);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                UserName = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        [Authorize]
        [Route("GetUsers")]
        public IEnumerable<AuthUser> GetUsers()
        {
            IOrganisationRepository _repository = new OrganisationRepository();
            List<ApplicationUser> appUsers = new List<ApplicationUser>();
            List<AuthUser> AuthUsers = new List<AuthUser>();
            List<AuthUser> usersByRole = new List<AuthUser>();


            if (!HttpContext.Current.User.IsInRole("Practice Admin"))
            {
                usersByRole = _repository.GetAuthUsers();
                appUsers = UserManager.Users.ToList();
            }
            else
            {
                usersByRole = _repository.GetAuthUsersForPracticeAdmin();
                foreach (var item in UserManager.Users)
                {
                    if (usersByRole.Exists(x => x.UserName == item.UserName))
                    {
                        appUsers.Add(item);
                    }
                }
            }

            foreach (var user in appUsers)
            {
                var authUser1 = usersByRole.Where(u => u.UserName == user.UserName).ToList();
                var authUser = new AuthUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = user.Roles.Select(x => x.RoleId).ToList(),
                    OrganisationId = authUser1 != null && authUser1.Any() ? authUser1.First().OrganisationId : 0,
                    OrganisationName = authUser1 != null && authUser1.Any() ? string.Join(", ", authUser1.Select(x => x.OrganisationName).ToArray()) : "Egton"
                };
                AuthUsers.Add(authUser);
            }
            return AuthUsers;
        }

        [Authorize]
        [Route("GetUser")]
        public async Task<AuthUser> GetUser(string userName)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(userName.Trim());

            if (user == null)
                return null;

            return new AuthUser()
            {
                Email = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
                Roles = user.Roles.Select(x => x.RoleId).ToList()
            };
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            var logins = user.Logins.Select(linkedAccount => new UserLoginInfoViewModel
            {
                LoginProvider = linkedAccount.LoginProvider,
                ProviderKey = linkedAccount.ProviderKey
            }).ToList();

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = _localLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = _localLoginProvider,
                UserName = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result =
                await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        //	[Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await UserManager.FindByNameAsync(model.UserName.Trim());

            IdentityResult result = await UserManager.RemovePasswordAsync(user.Id);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            result = await UserManager.AddPasswordAsync(user.Id, model.NewPassword);

            errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result =
                await
                UserManager.AddLoginAsync(
                    User.Identity.GetUserId(),
                    new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == _localLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result =
                    await
                    UserManager.RemoveLoginAsync(
                        User.Identity.GetUserId(),
                        new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            var user = await UserManager.FindAsync(
                new UserLoginInfo(
                    externalLogin.LoginProvider,
                    externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity oAuthIdentity = await UserManager.CreateIdentityAsync(
                    user,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await UserManager.CreateIdentityAsync(
                    user,
                    CookieAuthenticationDefaults.AuthenticationType);
                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                var identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route(
                        "ExternalLogin",
                        new
                        {
                            provider = description.AuthenticationType,
                            response_type = "token",
                            client_id = Startup.PublicClientId,
                            redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                            state = state
                        }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        //[AllowAnonymous]

        //[Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [Authorize]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName.Trim(),
                Email = model.Email.Trim()
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            if (model.Roles != null && model.Roles.Length > 0)
            {
                var usr = await UserManager.FindByNameAsync(model.UserName);

                result = await UserManager.AddToRolesAsync(usr.Id, model.Roles);

                errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }
            }

            return Ok();
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName
            };
            user.Logins.Add(new IdentityUserLogin
            {
                LoginProvider = externalLogin.LoginProvider,
                ProviderKey = externalLogin.ProviderKey
            });
            IdentityResult result = await UserManager.CreateAsync(user);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; private set; }
            public string ProviderKey { get; private set; }
            private string UserName { get; set; }

            public IEnumerable<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static readonly RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                var data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
