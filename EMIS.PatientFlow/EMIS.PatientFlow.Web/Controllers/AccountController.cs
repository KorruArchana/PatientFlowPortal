using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Models;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EMIS.PatientFlow.Web.Controllers
{
    [OutputCache(Duration = 0)]
    [HandleError]
    public class AccountController : Controller
    {
        private readonly IAuthenticationRepository _repository;
        private readonly IOrganisationRepository _organisationRepository;

        public AccountController(
            IAuthenticationRepository repository,
            IOrganisationRepository organisationRepository)
        {
            _repository = repository;
            _organisationRepository = organisationRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName("Login")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cookie = await ValidateUser(model);

                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, model.UserName);

                    ModelState.AddModelError("CustomError", "Invalid Username and/or Password");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account", null);
        }

		public async Task<ActionResult> Index()
		{
			if (!User.IsInRole("Practice Admin"))
			{
				return RedirectToAction("Index", "EgtonAccount");
			}
			else
			{
				UserListViewModel usersList = new UserListViewModel();
				usersList = await GetAuthUsers();
				return PartialView("Index", usersList);
			}
		}

		[HttpGet]
		[AuthorizeUser(Roles = "Practice Admin")]
		public async Task<UserListViewModel> GetAuthUsers()
		{
			var model = new UserListViewModel();

			try
			{
				var users = await _repository.GetUsers();

				model.Users = new List<ExtendedAuthUser>();

				if (users != null && users.ToList().Count > 0)
				{
					model.Users = users.Select(x => new ExtendedAuthUser()
					{
						Name = x.Name,
						UserName = x.UserName,
						Email = x.Email,
						OrganisationId = x.OrganisationId,
						OrganisationName = x.OrganisationName
					}).ToList();
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

				ModelState.AddModelError("CustomError", ex.Message);
			}
			return model;
		}

		public async Task<bool> CheckUserNameAvailability(string userName)
		{
			var user = await _repository.GetUser(userName);
			if (user != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		[AuthorizeUser(Roles = "EMIS Super User,Egton Engineer,Practice Admin")]
        public async Task<ActionResult> Register()
        {
            var model = new RegisterViewModel();
            try
            {
                model.Roles = await _repository.GetRoles();

                if (model.Roles != null)
                {
                    model.RolesList = model.Roles.Select(roles => new SelectListItem
                    {
                        Text = roles.DisplayText,
                        Value = roles.Name
                    }).ToList();
                    model.SelectedRoles = new List<string>();

                    List<Organisation> organisationsList = await _organisationRepository.GetOrganisationsForDropDown();
                    var organisationList = new List<SelectListItem>();
                    if (organisationsList != null)
                    {
                        organisationList = organisationsList.Select(org => new SelectListItem
                        {
                            Text = org.OrganisationName,
                            Value = org.Id.ToString(),
                        }).ToList();
                    }
                    model.OrganisationList = organisationList;
                    model.OrganisationIds = new List<int>();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(
                    Common.Enums.LogType.Error,
                    ex.Message,
                    ex,
                    ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

                ModelState.AddModelError("CustomError", ex.Message);
            }
            return PartialView("AddEditUser", model);
        }

        [AuthorizeUser(Roles = "EMIS Super User,Egton Engineer,Practice Admin")]
        public async Task<ActionResult> UpdateUser(string userName)
        {
            var model = new RegisterViewModel();
            try
            {
                var user = await _repository.GetUser(userName);

                model.UserName = user.UserName;
                model.Email = user.Email;
                model.Roles = await _repository.GetRoles();
                model.UserId = user.UserId;
                model.OrganisationList = new List<SelectListItem>();
                model.Roles = await _repository.GetRoles();

                if (model.Roles != null)
                {
                    model.RolesList = model.Roles.Select(roles => new SelectListItem
                    {
                        Text = roles.DisplayText,
                        Value = roles.Name
                    }).ToList();
                }
                model.SelectedRoles = new List<string>();

                model.SelectedRole = user.Roles.Join(model.Roles,
                                                roles => roles,
                                                mroles => mroles.UniqueId,
                                                (roles, mroles) => new { roles, mroles })
                                                .Where(c => c.roles == c.mroles.UniqueId)
                                                .Select(c => c.mroles.Name).FirstOrDefault();

                model.RolesList = model.Roles.Select(roles => new SelectListItem
                {
                    Text = roles.DisplayText,
                    Value = roles.Name
                }).ToList();
                var selectedOrg = await _repository.GetOrganisationAccessRights(userName);

                List<Organisation> organisationsList = await _organisationRepository.GetOrganisationsForDropDown();
                var organisationList = new List<SelectListItem>();
                if (organisationsList != null)
                {
                    organisationList = organisationsList.Select(org => new SelectListItem
                    {
                        Text = org.OrganisationName,
                        Value = org.Id.ToString(),
                    }).ToList();
                }
                model.OrganisationList = organisationList;
                if (selectedOrg != null && selectedOrg.Count > 0)
                {
                    model.OrganisationIds = selectedOrg.Select(x => Convert.ToInt32(x.Key)).ToList();
                }
            }
            catch (Exception ex)
            {

                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

                ModelState.AddModelError("CustomError", ex.Message);
            }
            return PartialView("AddEditUser", model);
        }

        [HttpPost]
        [AuthorizeUser(Roles = "EMIS Super User,Egton Engineer,Practice Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveUser(RegisterViewModel model)
        {
            bool result = false;
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.UserId != null)
                    {
                        model.SelectedRole = model.SelectedRoles.FirstOrDefault();
                        result = await EditUserSave(model);
						TempData["SuccessMessage"] = "User edited";
					}
                    else
                    {
                        if (model.SelectedRoles == null) model.SelectedRoles = new List<string>();

                        result = await _repository.RegisterUser(new ApplicationUser()
                        {
                            UserName = model.UserName,
                            Password = model.Password,
                            ConfirmPassword = model.ConfirmPassword,
                            Email = model.Email,
                            Roles = model.SelectedRoles.ToArray()
                        });

                        if (!(model.SelectedRoles.Contains("Practice Admin")))
                            model.OrganisationIds = new List<int>();

                        result = await _repository.SaveAccessMapping(new OrganisationAccess()
                        {
                            UserName = model.UserName,
                            Accesses = model.OrganisationIds
                        });
						TempData["SuccessMessage"] = "User added";
					}

                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(
                        Common.Enums.LogType.Error,
                        ex.Message,
                        ex,
                        ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                    TempData["ErrorMessage"] = "User is not saved";
                    ModelState.AddModelError("CustomError", ex.Message);
                }
            }
            return null;
        }

        public async Task<bool> EditUserSave(RegisterViewModel model)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new AuthUser();
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    model.Roles = await _repository.GetRoles();
                    user.Roles = !string.IsNullOrEmpty(model.SelectedRole) ?
                        model.Roles.Where(x => x.Name == model.SelectedRole).Select(y => y.UniqueId).ToList() :
                        new List<string>();

                    if (model.SelectedRoles.Contains("EMIS Super User"))
                        model.OrganisationIds = new List<int>();

                    result = await _repository.UpdateUser(user);
                    result = await _repository.SaveAccessMapping(new OrganisationAccess()
                    {
                        UserName = model.UserName,
                        Accesses = model.OrganisationIds
                    });

                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                    TempData["ErrorMessage"] = "User is not saved";
                    ModelState.AddModelError("CustomError", ex.Message);
                }
            }
            return result;
        }

		[Authorize]
		public async Task<ActionResult> DeleteUser(string userName)
		{
			bool result = false;
			try
			{
				result = await _repository.DeleteUser(userName);
				if (result)
				{
					try
					{
						await _repository.DeleteOrganisationAuthUsers(userName);
					}
					catch (Exception ex)
					{
						Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
					}
				}
            }
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
				Session["DeleteErrorMessage"] = "User is not deleted";
				ModelState.AddModelError("CustomError", ex.Message);
			}
			Session["DeleteMessage"] = "User deleted";
			return RedirectToAction("Index", "Account");
		}

		[Authorize]
		public ActionResult ResetPassword(string userName)
		{
			ResetPasswordViewModel model = new ResetPasswordViewModel();
			model.UserName = userName;

			return PartialView("ResetPwd", model);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var result = await _repository.ResetPassword(new
				ResetPassword()
					{
						UserName = model.UserName,
						NewPassword = model.NewPassword,
						ConfirmPassword = model.ConfirmPassword
					});
					if (result)
						TempData["ResetPwdSuccess"] = result == true ? "Password has been updated." : "Password is not updated";
					return Json(Url.Action("Index", "Account"));
				}
				catch (Exception ex)
				{
					Logger.Instance.WriteLog(
						Common.Enums.LogType.Error,
						ex.Message,
						ex,
						((TokenGenericPrincipal)HttpContext.User).Instance.Name);
					TempData["ErrorMessage"] = "Password is not updated";
					ModelState.AddModelError("CustomError", ex.Message);
				}
			}

			return PartialView(model);
		}


		// We are not using below actions, Check and delete it if not needed.
		[Authorize(Roles = "EMIS Super User")]
        public ActionResult ManageRole(string userName)
        {
            RoleViewModel model = new RoleViewModel();

            model.UserName = userName;

            return PartialView(model);
        }

        [AuthorizeUser(Roles = "EMIS Super User")]
        public ActionResult Manage()
        {
            return PartialView();
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [HttpPost]
        public async Task<JsonResult> CreateRole(string roleName)
        {
            try
            {
                bool success = await _repository.CreateRole(roleName);

                return Json(new { Success = success, Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

                ModelState.AddModelError("CustomError", ex.Message);

                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [HttpPost]
        public async Task<JsonResult> DeleteRole(string roleName)
        {
            try
            {
                bool success = await _repository.DeleteRole(roleName);

                return Json(new { Success = success, Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

                ModelState.AddModelError("CustomError", ex.Message);

                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [HttpPost]
        public async Task<JsonResult> EditRole(AuthRole role)
        {
            try
            {
                bool success = await _repository.UpdateRole(role);

                return Json(new { Success = success, Message = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

                ModelState.AddModelError("CustomError", ex.Message);

                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "EMIS Super User,Egton Engineer")]
        [HttpGet]
        public async Task<JsonResult> GetAllRoles()
        {
            try
            {
                var result = await _repository.GetRoles();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

                ModelState.AddModelError("CustomError", ex.Message);

                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ValidateLogin")]
        public async Task<bool> ValidateLogin(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return !string.IsNullOrEmpty(await _repository.GetToken(model.UserName, model.Password));
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<HttpCookie> ValidateUser(LoginViewModel model)
        {
            var token = await _repository.GetToken(model.UserName, model.Password);
            var roles = await _repository.GetUserInRole(model.UserName, token);
            var data = new TokenPrincipalSerializeModel();
            data.Name = model.UserName;
            data.Token = token;
            data.Roles = roles.ToArray();
            var userData = JsonConvert.SerializeObject(data);
            var authTicket = new FormsAuthenticationTicket(
                     1,
                    model.UserName,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(FormsAuthentication.Timeout.Minutes),
                     false,
                     userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            return cookie;
        }

        public Dictionary<string, object> GetErrorsFromModelState()
        {
            var errors = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys.Where(key => ModelState[key].Errors.Count > 0))
            {
                errors[key] = ModelState[key].Errors;
            }

            return errors;
        }

    }
}
