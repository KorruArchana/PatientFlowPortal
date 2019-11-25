using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EMIS.PatientFlow.Services.Controllers
{
    [Providers.KioskAuthentication]
	[RoutePrefix("api/KioskData")]
	[System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
	public class KioskDataController : ApiController
	{
		private readonly KioskRepository kioskRepository = new KioskRepository();
		private readonly AlertsRepository alertRepository = new AlertsRepository();
		
		[Route("GetKioskData")]
		public Kiosk GetKioskData()
		{
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;
			int kioskId = kioskRepository.GetKioskIdFromKey(productKey);

			Kiosk kiosk = kioskRepository.GetKioskDetails(kioskId);
			return kiosk;
		}

		[Route("GetAdditionalInformation")]
		public AdditionalInformation GetAdditionalInformation()
		{
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;
			try
			{
				AdditionalInformation additionalInformation = kioskRepository.GetAdditionalInformation(productKey);
				foreach (Alert alert in additionalInformation.AlertList)
				{
					alert.OrganisationIds = alertRepository.GetOrganisationIdListForAlert(alert.Id).Distinct().ToList();
					alert.SessionHolderIdList = alertRepository.GetSessionHolderIdForAlert(alert.Id);
				}

				return additionalInformation;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Entities.Enums.LogType.Error, ex.Message, ex, "KioskAPISync");
				return null;
			}
		}

		[Route("GetAlerts")]
		public List<Alert> GetAlerts()
		{
			Kiosk kiosk = GetKioskData();

			List<Alert> alertsList = alertRepository.GetAlertsListForOrganisation(kiosk.SelectedOrganisationList);
			foreach (Alert alert in alertsList)
			{
				alert.OrganisationIds = alertRepository.GetOrganisationIdListForAlert(alert.Id);
				alert.SessionHolderIdList = alertRepository.GetSessionHolderIdForAlert(alert.Id);
			}

			return alertsList;
		}

		[Route("GetLinkedMembersForKiosk")]
		public List<Member> GetLinkedMembersForKiosk()
		{
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;
			List<Member> memberList = kioskRepository.GetLinkedMembersForKiosk(productKey);

			return memberList;
		}

		[Route("SaveKioskDetails")]
		[HttpPost]
		public void SaveKioskDetails(KioskSystemInformation systemInformation)
		{
			string registrationKey = System.Web.HttpContext.Current.User.Identity.Name;
			try
			{
				kioskRepository.SaveKioskDetails(registrationKey, systemInformation);
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(Entities.Enums.LogType.Error, ex.Message, ex, "KioskAPISync : SaveKioskDetails");
			}
		}

		[Route("GetPatientFlowUser")]
		public List<WebUser> GetPatientFlowUser()
		{
			string KioskProductKey = System.Web.HttpContext.Current.User.Identity.Name;
			return kioskRepository.GetPatientFlowUserByKioskKey(new Guid(KioskProductKey));
		}
	}


	[Providers.SyncAuthentication]
	[RoutePrefix("api/SyncData")]
	public class SyncDataController : ApiController
	{
		private readonly ILoggerRepository _logRepository = new LoggerRepository();
		readonly SyncServiceRepository _syncRepository = new SyncServiceRepository();
		private readonly ISurveyRepository _surveyRepository = new SurveyRepository();
		

		[Route("GetPatientFlowUser")]
		public List<WebUser> GetPatientFlowUser()
		{
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;

			return _syncRepository.GetPatientFlowUsers(new Guid(productKey));
		}

		[Route("GetAllQuestionnaires")]
		public List<Questionnaire> GetAllQuestionnaires()
		{
			DateTime lastvalue;
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;
			IQuestionnaireRepository questionnaireRepository = new QuestionnaireRepository();
			var list = questionnaireRepository.GetQuestionnaireList(productKey, new DateTime(2000, 1, 1), out lastvalue);

			return list.Select(a => a.Id).Select(questionnaireRepository.GetQuestionnaireDetails).ToList();
		}

		[Route("GetQuestionnaire")]
		public Questionnaire GetQuestionnaire(int questionnaireId)
		{
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;

			if (_syncRepository.IsExistSyncService(new Guid(productKey)))
			{
				IQuestionnaireRepository questionnaireRepository = new QuestionnaireRepository();
				return questionnaireRepository.GetQuestionnaireDetails(questionnaireId);
			}
			return null;
		}

		[Route("GetRecentlyUpdatedQuestionnaire")]
		public List<Questionnaire> GetRecentlyUpdatedQuestionnaire(DateTime timeStamp)
		{
			DateTime lastvalue;
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;
			IQuestionnaireRepository questionnaireRepository = new QuestionnaireRepository();
			var list = questionnaireRepository.GetQuestionnaireList(productKey, DateTime.Today, out lastvalue);

			return list.Select(a => a.Id).Select(questionnaireRepository.GetQuestionnaireDetails).ToList();
		}

		[HttpPost]
		[Route("SaveLogs")]
		public IHttpActionResult SaveLogs(List<Log> items)
		{
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;
			try
			{
				_logRepository.WriteLog(items);
				var id = items.Select(a => a.Id).ToList().Last();
				return Ok(id);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : SaveLogs", ex, productKey);
			}
			return Ok(-1);
		}

		[HttpPost]
		[Route("SaveAnonymousSurvey")]
		public IHttpActionResult SaveAnonymousSurvey(List<Survey> surveys)
		{
			string productKey = System.Web.HttpContext.Current.User.Identity.Name;
			try
			{
				_surveyRepository.SurveyUpdate(surveys);
				return Ok(surveys.Last().AnswerId);
			}
			catch (Exception ex)
			{
				_logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : WriteLog", ex, productKey);
			}
			return Ok(-1);
		}

        [HttpPost]
        [Route("UpdateUsageRecords")]
        public IHttpActionResult UpdateUsageRecords(List<Log> items)
        {
            string productKey = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                items.ForEach(a => a.Date = a.UsageDate.ConvertFromJsonString<DateTime>());
                _logRepository.WriteLog(items);
                var id = items.Select(a => a.Id).ToList().Last();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : SaveLogs", ex, productKey);
            }
            return Ok(-1);
        }

        [HttpPost]
        [Route("SaveAnonymousSurveyResult")]
        public IHttpActionResult SaveAnonymousSurveyResult(List<Survey> surveys)
        {
            string productKey = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                surveys.ForEach(a => a.Modified = a.ModifiedDate.ConvertFromJsonString<DateTime>());
                _surveyRepository.SurveyUpdate(surveys);
                return Ok(surveys.Last().AnswerId);
            }
            catch (Exception ex)
            {
                _logRepository.WriteLog(Entities.Enums.LogType.Error, "SyncHub Method : WriteLog", ex, productKey);
            }
            return Ok(-1);
        }
    }
}