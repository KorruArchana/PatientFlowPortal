using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMIS.PatientFlow.API;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.ViewModel;

namespace EMIS.PatientFlow.Web.Helper
{
	public class WebApiResult<T>
	{
		public bool IsSuccess { get; set; }
		public T Result { get; set; }
		public int Outcome { get; set; }
		public string Error { get; set; }
	}

	public class ApiHelper
	{
		private readonly IKioskRepository _kioskRepository;
		private readonly IMemberRepository _memberRepository;
		private readonly IOrganisationRepository _organisationRepository;
		public ApiHelper()
		{
			_kioskRepository = new KioskRepository();
			_organisationRepository = new OrganisationRepository();
			_memberRepository = new MemberRepository();
		}

		public WebApiResult<T> GetWebApiResult<T>(T data, int outcome, string error)
		{
			var result = new WebApiResult<T>
			{
				IsSuccess = Convert.ToInt32(outcome) == 1,
				Outcome = outcome,
				Error = error,
				Result = data
			};

			return result;
		}

		public async Task<List<AppointmentSlotType>> GetAppointmentSlotType(int organisationId = 0)
		{
			try
			{
				List<AppointmentSlotType> appointmentSlotTypes = await _kioskRepository.GetAppointmentSlotTypes(organisationId);
				if (appointmentSlotTypes == null || appointmentSlotTypes.Count == 0)
				{
					await SaveAppointmentSlot(organisationId);
					appointmentSlotTypes = await _kioskRepository.GetAppointmentSlotTypes(organisationId);
				}

				return appointmentSlotTypes.OrderBy(a => a.Description).ToList();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
				return null;
			}
		}

		public async Task<WebApiResult<List<Site>>> SaveAppointmentSlotAndSite(Organisation organisation)
		{
			string exception = string.Empty;
			var resultData = new WebApiResult<List<Site>>();

			try
			{
				DateTime startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
				DateTime endDate = startDate.AddDays(1).AddMinutes(-1);

				var credential = new Credential();
				credential.IpAddress = organisation.User.IpAddress;
				credential.OrganisationKey = organisation.DatabaseName;
				credential.SupplierId = organisation.User.SupplierId.Decrypt() ?? string.Empty;
				credential.Password = organisation.User.Password.Decrypt() ?? string.Empty;
				credential.UserName = organisation.User.Username.Decrypt() ?? string.Empty;
				credential.WebServiceUrl = organisation.User.WebServiceUrl;
				credential.SystemType = ConvertToSystemType(organisation.SystemType);
				var siteList = new List<Site>();

				List<AppointmentSlotType> appointmentSlotTypes = new List<AppointmentSlotType>();

				if (credential.SystemType.Equals(SystemType.EmisWeb))
				{
					using (WebClient client = new WebClient(credential))
					{
						ApiResult<AppointmentConfiguration> result = client.GetAppointmentConfiguration(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								foreach (var item in result.Result.SlotTypeList)
								{
									if (item.ID > 0)
									{
										AppointmentSlotType slotType = new AppointmentSlotType();
										slotType.SlotTypeId = Convert.ToString(item.ID);
										slotType.Description = item.Description;
										slotType.OrganisationId = organisation.Id;
										appointmentSlotTypes.Add(slotType);
									}
								}

								foreach (var configurationSiteList in result.Result.SiteList)
								{
									siteList.Add(new Site { OrganisationId = organisation.Id, SiteDbId = configurationSiteList.DBID, SiteName = configurationSiteList.Name });
								}

								await _kioskRepository.SaveAppointmentSlotType(organisation.Id, appointmentSlotTypes);
							}
						}
						resultData = GetWebApiResult<List<Site>>(siteList, result.Outcome, result.Error);
					}
				}

				else if (credential.SystemType.Equals(SystemType.EmisPcs))
				{
					using (PcsClient client = new PcsClient(credential))
					{
						ApiResult<PCSAppointmentConfiguration.AppointmentConfiguration> result = client.GetAppointmentConfiguration(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								foreach (var item in result.Result.SlotTypeList)
								{
									AppointmentSlotType slotType = new AppointmentSlotType();
									slotType.SlotTypeId = Convert.ToString(item.ID);
									slotType.Description = item.Description;
									slotType.OrganisationId = organisation.Id;
									appointmentSlotTypes.Add(slotType);
								}

								foreach (var configurationSiteList in result.Result.SiteList)
								{
									siteList.Add(new Site { OrganisationId = organisation.Id, SiteDbId = configurationSiteList.DBID, SiteName = configurationSiteList.Name });
								}

								await _kioskRepository.SaveAppointmentSlotType(organisation.Id, appointmentSlotTypes);
							}
						}
						resultData = GetWebApiResult<List<Site>>(siteList, result.Outcome, result.Error);
					}
				}
				else if (credential.SystemType.Equals(SystemType.TPPSystmOne))
				{
					TPPHelper tppHelper = new TPPHelper();
					string request = tppHelper.GetOrganisation(credential);

					using (TppClient client = new TppClient(credential))
					{
						var response = client.GetOrganisation(request, credential.WebServiceUrl);
						int slotTypeId = 1000;

						if (response.IsSuccess && response.Result.Response.Any())
						{
							foreach (var item in response.Result.Response[0].SlotType)
							{
								AppointmentSlotType slotType = new AppointmentSlotType();
								slotType.SlotTypeId = slotTypeId.ToString();
								slotType.Description = item.Value;
								slotType.OrganisationId = organisation.Id;
								appointmentSlotTypes.Add(slotType);
							}
							foreach (var site in response.Result.Response[0].Site)
							{
								siteList.Add(new Site() { SiteDbId = slotTypeId++, Name = site.ID, SiteName = site.Name, OrganisationId = organisation.Id });
							}
						}
					}
					resultData = GetWebApiResult<List<Site>>(siteList, 1, "");

					await _kioskRepository.SaveAppointmentSlotType(organisation.Id, appointmentSlotTypes);
				}

				return resultData;
			}
			catch (Exception ex)
			{
				exception = ex.Message.ToString();
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
				return resultData = GetWebApiResult<List<Site>>(null, -1, exception);
			}
		}

		public async Task<string> SaveAppointmentSlot(int organisationId)
		{
			String slotException = string.Empty;
			try
			{
				List<AppointmentSlotType> appointmentSlotTypes = new List<AppointmentSlotType>();
				WebUser webUser = await _organisationRepository.GetPatientFlowUser(organisationId);

				var credential = new Credential();
				credential.IpAddress = webUser.IpAddress;
				credential.OrganisationKey = webUser.DatabaseName;
				credential.SupplierId = webUser.SupplierId;
				credential.Password = webUser.Password;
				credential.UserName = webUser.Username;

				if (webUser.Type.Equals(SystemType.EmisWeb.GetDisplayName()))
				{
					using (WebClient client = new WebClient(credential))
					{
						ApiResult<AppointmentConfiguration> result = client.GetAppointmentConfiguration(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								foreach (var item in result.Result.SlotTypeList)
								{
									if (item.ID > 0)
									{
										AppointmentSlotType slotType = new AppointmentSlotType();
										slotType.SlotTypeId = Convert.ToString(item.ID);
										slotType.Description = item.Description;
										slotType.OrganisationId = organisationId;
										appointmentSlotTypes.Add(slotType);
									}
								}

								await _kioskRepository.SaveAppointmentSlotType(organisationId, appointmentSlotTypes);
							}
						}
						slotException = result.Error;
					}
				}
				else if (webUser.Type.Equals(SystemType.EmisPcs.GetDisplayName()))
				{
					using (PcsClient client = new PcsClient(credential))
					{
						ApiResult<PCSAppointmentConfiguration.AppointmentConfiguration> result = client.GetAppointmentConfiguration(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								foreach (var item in result.Result.SlotTypeList)
								{
									AppointmentSlotType slotType = new AppointmentSlotType();
									slotType.SlotTypeId = Convert.ToString(SwitchPCSSlotType(item.ID));
									slotType.Description = item.Description;
									slotType.OrganisationId = organisationId;
									appointmentSlotTypes.Add(slotType);
								}

								await _kioskRepository.SaveAppointmentSlotType(organisationId, appointmentSlotTypes);
							}
						}
						slotException = result.Error;
					}
				}
				return slotException;
			}
			catch (Exception ex)
			{
				slotException = ex.Message.ToString();
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
				return slotException;
			}
		}

		private string SwitchPCSSlotType(string typeId)
		{
			switch (typeId)
			{
				case "D1":
					return "6001";
				case "D2":
					return "6002";
				case "D3":
					return "6003";
				case "D4":
					return "6004";
				case "D5":
					return "6005";
				case "D6":
					return "6006";
				default:
					return typeId;
			}
		}

		public WebApiResult<List<Site>> GetSiteDetails(OrganisationViewModel organisation, out string exception)
		{
			exception = string.Empty;
			var resultData = new WebApiResult<List<Site>>();
			try
			{
				DateTime startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
				DateTime endDate = startDate.AddDays(1).AddMinutes(-1);
				var credential = new Credential();
				credential.IpAddress = organisation.IpAddress;
				credential.OrganisationKey = organisation.OrganisationKey;
				credential.SupplierId = organisation.SupplierId;
				credential.Password = organisation.Password;
				credential.UserName = organisation.Username;
				credential.WebServiceUrl = organisation.WebServiceUrl;
				credential.SystemType = ConvertToSystemType(organisation.SystemType);
				var siteList = new List<Site>();

				if (credential.SystemType == SystemType.EmisWeb)
				{
					using (WebClient client = new WebClient(credential))
					{
						int within = (int)(endDate - DateTime.UtcNow).TotalMinutes;
						int before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
						before = (before < 300 ? before : 300);
						ApiResult<AppointmentConfiguration> result = client.GetAppointmentConfiguration(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{

								foreach (var configurationSiteList in result.Result.SiteList)
								{
									siteList.Add(new Site { OrganisationId = organisation.Id, SiteDbId = configurationSiteList.DBID, SiteName = configurationSiteList.Name });
								}
								//siteList = result.Result.SiteList.Select(x => new Site { SiteDbId = Convert.ToInt64(x.Site.DBID), SiteName = x.Site.Name }).ToList();
							}
						}
						resultData = GetWebApiResult<List<Site>>(siteList, result.Outcome, result.Error);
					}
				}
				else if (credential.SystemType == SystemType.EmisPcs)
				{
					using (PcsClient client = new PcsClient(credential))
					{
						int within = (int)(endDate - DateTime.UtcNow).TotalMinutes;
						int before = (int)(DateTime.UtcNow - startDate).TotalMinutes;
						before = (before < 300 ? before : 300);
						ApiResult<PCSAppointmentConfiguration.AppointmentConfiguration> result = client.GetAppointmentConfiguration(DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								foreach (var configurationSiteList in result.Result.SiteList)
								{
									siteList.Add(new Site { OrganisationId = organisation.Id, SiteDbId = configurationSiteList.DBID, SiteName = configurationSiteList.Name });
								}
								//siteList = result.Result.SiteList.Select(x => new Site { SiteDbId = Convert.ToInt64(x.Site.DBID), SiteName = x.Site.Name }).ToList();
							}
						}
						resultData = GetWebApiResult<List<Site>>(siteList, result.Outcome, result.Error);
					}
				}
				else if (credential.SystemType == SystemType.TPPSystmOne)
				{
					using (TppClient client = new TppClient(credential))
					{
						var response = client.GetOrganisation();
						int siteId = 2000;
						foreach (var site in response[0].Site)
						{
							siteList.Add(new Site()
							{
								SiteDbId = siteId++,
								Name = site.ID,
								SiteName = site.Name,
								OrganisationId = organisation.Id
							});
						}
					}
					resultData = GetWebApiResult<List<Site>>(siteList, 1, "");
				}
				exception = resultData.Error;
				return resultData;
			}
			catch (Exception ex)
			{
				exception = ex.Message.ToString();
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
				return resultData = GetWebApiResult<List<Site>>(null, -1, exception);
			}
		}

		public async Task<List<Member>> GetMember(int organisationId, int departmentId)
		{
			var memberList = new List<Member>();

			if (await GetMemberList(organisationId, departmentId, memberList)) return null;

			return memberList;
		}

		public async Task<List<Member>> GetMember(int organisationId)
		{
			var memberList = new List<Member>();
			if (await GetMemberList(organisationId, 1, memberList))
				return null;
			return memberList;
		}

		public async Task<List<Member>> GetMember(int organisationId, string organisationName)
		{
			var memberList = new List<Member>();
			if (await GetMemberList(organisationId, organisationName, memberList))
				return null;
			return memberList;
		}

		private async Task<bool> GetMemberList(int organisationId, int departmentId, List<Member> memberList)
		{
			try
			{
				WebUser webUser = await _organisationRepository.GetPatientFlowUser(organisationId);

				var credential = new Credential
				{
					IpAddress = webUser.IpAddress,
					OrganisationKey = webUser.DatabaseName,
					SupplierId = webUser.SupplierId,
					Password = webUser.Password,
					UserName = webUser.Username,
					SystemType = ConvertToSystemType(webUser.Type)
				};

				string consulter = "*";
				if (Config.IsEmisWebPortal && credential.SystemType == SystemType.EmisWeb)
				{
					using (WebClient client = new WebClient(credential))
					{
						var result = client.GetOrganisation();
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								List<OrganisationInformationUser> users = consulter == "*"
									? result.Result.UserList.ToList()
									: result.Result.UserList.Where(item => item.Consulter.ToLower() == consulter.ToLower()).ToList();
								users = users.OrderBy(i => i.FirstNames).ToList();
								foreach (var item in users)
								{
									var member = new Member
									{
										SessionHolderId = Convert.ToInt32(item.DBID),
										Firstname = item.FirstNames,
										Surname = item.LastName,
										Title = item.Title,
										DepartmentId = departmentId,
										OrganisationId = organisationId,
										LoginId = item.Security1
									};
									member.FullName = member.Title + " " + member.Firstname + " " + member.Surname;
									memberList.Add(member);
								}
							}
						}
					}
				}
				else if (!Config.IsEmisWebPortal && credential.SystemType == SystemType.EmisPcs)
				{
					using (PcsClient client = new PcsClient(credential))
					{
						var result = client.GetOrganisation();
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								List<OrganisationInformationUser> users = consulter == "*"
									? result.Result.UserList.ToList()
									: result.Result.UserList.Where(item => item.Consulter.ToLower() == consulter.ToLower()).ToList();
								users = users.OrderBy(i => i.FirstNames).ToList();
								foreach (var item in users)
								{
									var member = new Member
									{
										SessionHolderId = Convert.ToInt32(item.DBID),
										Firstname = item.FirstNames,
										Surname = item.LastName,
										Title = item.Title,
										DepartmentId = departmentId,
										LoginId = item.Security1
									};
									memberList.Add(member);
								}
							}
						}
					}
				}
				else if (credential.SystemType == SystemType.TPPSystmOne)
				{
					using (TppClient client = new TppClient(credential))
					{
						var response = client.GetOrganisation();
						int memberId = 10000;
						foreach (var user in response[0].User)
						{
							var member = new Member
							{
								SessionHolderId = memberId++,
								Firstname = user.FirstName,
								Surname = user.Surname,
								Title = user.Title,
								DepartmentId = departmentId,
								LoginId = user.UserName
							};
							if (!string.IsNullOrEmpty(member.LoginId))
							{
								memberList.Add(member);
							}
						}
					}
				}
			}

			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
				return true;
			}
			return false;
		}

		private async Task<bool> GetMemberList(int organisationId, string orgName, List<Member> memberList)
		{
			try
			{
				Credential credential = await GetPatientUserCredentials(organisationId);

				string consulter = "*";
				if (Config.IsEmisWebPortal && credential.SystemType == SystemType.EmisWeb)
				{
					using (WebClient client = new WebClient(credential))
					{
						var result = client.GetOrganisation();
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								List<OrganisationInformationUser> users = consulter == "*"
									? result.Result.UserList.ToList()
									: result.Result.UserList.Where(item => item.Consulter.ToLower() == consulter.ToLower()).ToList();
								users = users.OrderBy(i => i.FirstNames).ToList();
								foreach (var item in users)
								{
									var member = new Member
									{
										SessionHolderId = Convert.ToInt32(item.DBID),
										Firstname = item.FirstNames,
										Surname = item.LastName,
										Title = item.Title,
										DepartmentId = 1,
										OrganisationId = organisationId,
										OrganisationName = orgName
									};
									member.FullName = member.Title + " " + member.Firstname + " " + member.Surname;

									memberList.Add(member);
								}
							}
						}
					}
				}
				else if (!Config.IsEmisWebPortal && credential.SystemType == SystemType.EmisPcs)
				{
					using (PcsClient client = new PcsClient(credential))
					{
						var result = client.GetOrganisation();
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								List<OrganisationInformationUser> users = consulter == "*"
									? result.Result.UserList.ToList()
									: result.Result.UserList.Where(item => item.Consulter.ToLower() == consulter.ToLower()).ToList();
								users = users.OrderBy(i => i.FirstNames).ToList();
								foreach (var item in users)
								{
									var member = new Member
									{
										SessionHolderId = Convert.ToInt32(item.DBID),
										Firstname = item.FirstNames,
										Surname = item.LastName,
										Title = item.Title,
										DepartmentId = 1,
										OrganisationName = orgName
									};
									member.FullName = member.Title + " " + member.Firstname + " " + member.Surname;
									memberList.Add(member);
								}
							}
						}
					}
				}
				else if (credential.SystemType == SystemType.TPPSystmOne)
				{
					var data = await _memberRepository.GetMembersByOrganisationId(organisationId);
					if (data.Any())
					{

						memberList.AddRange(data);
						memberList.ForEach(a => a.FullName = a.Title + " " + a.Firstname + " " + a.Surname);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
				return true;
			}
			return false;
		}

		private async Task<Credential> GetPatientUserCredentials(int organisationId)
		{
			WebUser webUser = await _organisationRepository.GetPatientFlowUser(organisationId);

			var credential = new Credential
			{
				IpAddress = webUser.IpAddress,
				OrganisationKey = webUser.DatabaseName,
				SupplierId = webUser.SupplierId,
				Password = webUser.Password,
				UserName = webUser.Username,
				SystemType = ConvertToSystemType(webUser.Type)
			};
			return credential;
		}

		private static SystemType ConvertToSystemType(string toString)
		{
			SystemType result = SystemType.None;
			switch (toString.ToUpper())
			{
				case "EMIS - WEB":
					result = SystemType.EmisWeb;
					break;
				case "EMIS - PCS":
					result = SystemType.EmisPcs;
					break;
				case "TPP - SYSTMONE":
					result = SystemType.TPPSystmOne;
					break;
				default:
					result = SystemType.None;
					break;
			}

			if (result == SystemType.TPPSystmOne)
				return result;
			if (Config.IsEmisWebPortal && result == SystemType.EmisWeb)
				return result;
			else if (!Config.IsEmisWebPortal && result == SystemType.EmisPcs)
				return result;

			Logger.Instance.WriteLog(LogType.Warn, "Invalid clincial type called", null, System.Web.HttpContext.Current.User.Identity.Name);

			return SystemType.None;
		}

		public async Task<List<PatientViewModel>> GetMatchedPatients(int organisationId, string filter)
		{
			try
			{
				WebUser webUser = await _organisationRepository.GetPatientFlowUser(organisationId);

				var credential = new Credential
				{
					IpAddress = webUser.IpAddress,
					OrganisationKey = webUser.DatabaseName,
					SupplierId = webUser.SupplierId,
					Password = webUser.Password,
					UserName = webUser.Username,
					SystemType = ConvertToSystemType(webUser.Type)
				};

				var patientList = new List<PatientViewModel>();

				if (credential.SystemType == SystemType.EmisPcs)
				{
					using (var client = new PcsClient(credential))
					{
						ApiResult<PatientMatches> result = client.GetMatchedPatients(filter);
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								List<PatientMatchesPatient> users = result.Result.PatientList.ToList();
								patientList.AddRange(users.Select(item => new PatientViewModel
								{
									PatientId = Convert.ToInt32(item.DBID),
									Firstname = item.FirstNames,
									Surname = item.FamilyName,
									Dob = item.DateOfBirth,
								}));
							}
						}
					}
				}
				else if (credential.SystemType == SystemType.EmisWeb)
				{
					using (var client = new WebClient(credential))
					{
						ApiResult<PatientMatches> result = client.GetMatchedPatients(filter);
						if (result.IsSuccess)
						{
							if (result.Result != null)
							{
								List<PatientMatchesPatient> users = result.Result.PatientList.ToList();
								patientList.AddRange(users.Select(item => new PatientViewModel
								{
									PatientId = Convert.ToInt32(item.DBID),
									Firstname = item.FirstNames,
									Surname = item.FamilyName,
									Dob = item.DateOfBirth,
								}));
							}
						}
					}
				}

				return patientList;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, System.Web.HttpContext.Current.User.Identity.Name);
				return null;
			}
		}
	}
}