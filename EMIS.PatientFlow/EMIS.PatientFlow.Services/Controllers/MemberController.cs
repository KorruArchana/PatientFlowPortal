using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Common.Validations;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Services.Helper;
using EMIS.PatientFlow.Services.Hubs;

namespace EMIS.PatientFlow.Services.Controllers
{
	[Authorize(Roles = "Practice Admin, EMIS Super User, Egton Engineer")]

	public class MemberController : ApiController
	{
		private readonly ILoggerRepository _logger;
		private readonly IMemberRepository _repository;
		private readonly IOrganisationRepository _orgRepository;
		private readonly IKioskRepository _kioskRepository;
		private readonly KioskHub _kioskHub;
		public MemberController(
			IMemberRepository departmentReposiotory,
			ILoggerRepository loggerRepository,
			IOrganisationRepository organisationRepository,
			KioskHub kioskHub,
			IKioskRepository kioskRepository)
		{
			_logger = loggerRepository;
			_repository = departmentReposiotory;
			_orgRepository = organisationRepository;
			_kioskHub = kioskHub;
			_kioskRepository = kioskRepository;
		}

		public int AddMember([FromBody]string value)
		{
			int result;
			try
			{
				Member member = JSONHelper.Deserialize<Member>(value);
				result = _repository.AddMember(member);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return result;
		}

		public void AddSyncedMember([FromBody]string value)
		{
			try
			{
				List<Member> member = value.ConvertFromJsonString<List<Member>>();
				_repository.AddSyncedMember(member);
				var organisationId = member.Select(m => m.OrganisationId).FirstOrDefault();
				_kioskHub.UpdateMember(organisationId);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

		[AcceptVerbs("GET")]
		public int DeleteMember(int memberId, int OrganisationId)
		{
			int result;
			try
			{
				ArgumentValidator.IsNegativeOrZero(memberId, "memberId");
				result = _repository.DeleteMember(memberId);
				_kioskHub.UpdateMember(OrganisationId);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return result;
		}

		public Member GetMemberDetails(int memberId)
		{
			Member member;
			try
			{
				ArgumentValidator.IsNegativeOrZero(memberId, "memberId");
				member = _repository.GetMemberDetails(memberId);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return member;
		}

		public int UpdateMember([FromBody]string value)
		{
			int result;
			try
			{
				Member member = value.ConvertFromJsonString<Member>();
				result = _repository.UpdateMember(member);
				var organisationId = member.OrganisationId;
				_kioskHub.UpdateMember(organisationId);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return result;
		}

		[AcceptVerbs("GET")]
		public bool ValidateSessionHolderId(int sessionHolderId)
		{
			try
			{
				bool status;
				_repository.ValidateSessionHolderId(sessionHolderId, out status);
				return status;
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

		public List<int> GetSessionHolderIdOrganisation(int organisationId)
		{
			List<int> sessionHolderIdList;

			try
			{
				ArgumentValidator.IsNegativeOrZero(organisationId, "organisationId");
				sessionHolderIdList = _repository.GetSessionHolderIdOrganisation(organisationId);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return sessionHolderIdList;
		}

		[AcceptVerbs("GET")]
		public bool SetDivert(bool status, int sessionHolderId, int organisationId)
		{
			try
			{
				Member member = _repository.SetDivert(status, sessionHolderId, organisationId);
				var org = _orgRepository.GetOrganisationDetail(organisationId);
				string organisationName = org.OrganisationName;
				if (org.SystemTypeId != 7)
					_kioskHub.SetDivert(status, sessionHolderId, organisationId, organisationName);
				else
					_kioskHub.SetTPPDivert(status, sessionHolderId, member.LoginId, organisationId, organisationName);
				return true;
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}
		public IEnumerable<Member> GetMembers()
		{
			try
			{
				if (!HttpContext.Current.User.IsInRole("Practice Admin"))
				{
					return _repository.GetMembers();
				}
				else
				{
					return _repository.GetMembersByUser();
				}
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

		public IEnumerable<Member> GetMemberByOrganisationId(int OrganisationId)
		{
			try
			{
				return _repository.GetMembersByOrganisationId(OrganisationId);
			}
			catch (Exception ex)
			{

				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

		[AcceptVerbs("GET")]
		public bool EnableMessageForMember(int alertId, int memberId)
		{
			try
			{
				bool status;
				_repository.EnableMessageForMember(alertId, memberId, out status);
				return status;
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}
	}
}
