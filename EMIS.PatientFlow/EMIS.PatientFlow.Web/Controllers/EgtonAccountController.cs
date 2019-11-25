using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Helper;
using EMIS.PatientFlow.Web.Models;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.Security;
using EMIS.PatientFlow.Web.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Controllers
{
    [Authorize(Roles = "EMIS Super User, Egton Engineer")]
    public class EgtonAccountController : Controller
    {
        private readonly IAuthenticationRepository _repository;

        public EgtonAccountController(IAuthenticationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult> Index()
        {
            UserListViewModel usersList = new UserListViewModel();
            usersList = await GetAuthUsers();
            return PartialView("Index", usersList);
        }

        public async Task<ActionResult> GetUsersData()
        {
            var model = new UserListViewModel();
            try
            {
                model = await GetAuthUsers();
                List<ExtendedAuthUser> UsersData = model.Users.ToList();

                int sortColumn = -1;
                string sortDirection = "asc";
                var result = new List<ExtendedAuthUser>();
                if (Request.QueryString["order[0][dir]"] != null)
                {
                    sortDirection = Request.QueryString["order[0][dir]"];
                }
                if (Request.QueryString["order[0][column]"] != null)
                {
                    sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
                    switch (sortColumn)
                    {
                        //case 1:
                        //	result = sortDirection == "desc" ? UsersData.OrderByDescending(m => m.Name).ToList()
                        //							 : UsersData.OrderBy(m => m.Name).ToList();

                        //	break;
                        case 1:
                            result = sortDirection == "desc" ? UsersData.OrderByDescending(m => m.UserName).ToList()
                                                     : UsersData.OrderBy(m => m.UserName).ToList();

                            break;
                        case 2:
                            result = sortDirection == "desc" ? UsersData.OrderByDescending(m => m.Email).ToList()
                                                     : UsersData.OrderBy(m => m.Email).ToList();

                            break;
                        case 3:
                            result = sortDirection == "desc" ? UsersData.OrderByDescending(m => m.OrganisationName).ToList()
                                                     : UsersData.OrderBy(m => m.OrganisationName).ToList();

                            break;
                        default:
                            result = UsersData.OrderBy(m => m.UserName).ToList();
                            break;
                    }
                }

                //string nameFilter = Request.QueryString["columns[1][search][value]"] ?? Request.QueryString["columns[1][search][value]"].ToString();
                string userNameFilter = Request.QueryString["columns[1][search][value]"] ?? Request.QueryString["columns[1][search][value]"].ToString();
                string emailFilter = Request.QueryString["columns[2][search][value]"] ?? Request.QueryString["columns[2][search][value]"].ToString();
                string organisationNameFilter = Request.QueryString["columns[3][search][value]"] ?? Request.QueryString["columns[3][search][value]"].ToString();
                organisationNameFilter = Regex.Replace(organisationNameFilter, "[;.^#$]+", "", RegexOptions.Compiled);
                organisationNameFilter = Regex.Replace(organisationNameFilter, "[|]+", ",", RegexOptions.Compiled);

                //if (!string.IsNullOrWhiteSpace(nameFilter))
                //{
                //	result = result.Where(x => x.Name.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                //}
                if (!string.IsNullOrWhiteSpace(userNameFilter))
                {
                    result = result.Where(x => x.UserName.IndexOf(userNameFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                if (!string.IsNullOrWhiteSpace(emailFilter))
                {
                    result = result.Where(x => x.Email.IndexOf(emailFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }               
                if (!string.IsNullOrWhiteSpace(organisationNameFilter))
                {
                    string[] organisationNameFilters = organisationNameFilter.Split(',');
                    // Has to ignore case here                 
					result = result.Where(x => organisationNameFilters.Any(s => x.OrganisationName.Contains(s))).ToList();                   
                }

                model.draw = int.Parse(Request.QueryString["draw"]);
                int start = int.Parse(Request.QueryString["start"]);
                int length = int.Parse(Request.QueryString["length"]);

                model.data = result.Skip(start).Take(length).ToArray();
                model.recordsTotal = UsersData.Count();
                model.recordsFiltered = result.Count();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);
                return (Json(model, JsonRequestBehavior.AllowGet));
            }
        }

        [HttpGet]
        [AuthorizeUser(Roles = "EMIS Super User,Egton Engineer")]
        public async Task<UserListViewModel> GetAuthUsers()
        {
            var model = new UserListViewModel();

            try
            {
                var users = await _repository.GetUsers();

                model.Users = new List<ExtendedAuthUser>();

                if (users != null && users.ToList().Count > 0)
                {
                    var roles = await _repository.GetRoles();
                    model.Users = users.Select(x => new ExtendedAuthUser()
                    {
                        Name = x.Name,
                        UserName = x.UserName,
                        Email = x.Email,
                        RolesAsCommaSeparatedString = string.Join(",", x.Roles.Select(user => roles.First(y => y.UniqueId == user).DisplayText).ToArray()),
                        OrganisationId = x.OrganisationId,
                        OrganisationName = x.OrganisationName
                    }).ToList();
                }

                var orgNameList = new List<string>();
                foreach (var org in model.Users.Select(a => a.OrganisationName).Distinct())
                {
                    orgNameList.AddRange(org.Split(',').Where(item => !orgNameList.Contains(item.Trim())).Select(item => item.Trim()));
                }
                model.OrganisationNameList = orgNameList;
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, ((TokenGenericPrincipal)HttpContext.User).Instance.Name);

                ModelState.AddModelError("CustomError", ex.Message);
            }
            return model;
        }
    }
}