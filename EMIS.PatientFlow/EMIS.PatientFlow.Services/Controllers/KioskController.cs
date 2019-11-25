using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Services.Helper;
using EMIS.PatientFlow.Services.Hubs;
using EMIS.PatientFlow.Common.Enums;
using System.Web;

namespace EMIS.PatientFlow.Services.Controllers
{
    [Authorize(Roles = "Practice Admin, EMIS Super User,Egton Engineer")]
    public class KioskController : ApiController
    {
        private readonly IKioskRepository _repository;
        private readonly ILoggerRepository _logger;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly KioskHub _kioskHub;
		private readonly SyncHub _syncHub;

		public KioskController(
            IKioskRepository kioskRepository,
            ILoggerRepository loggerRepository,
            IOrganisationRepository orgRepository,
           KioskHub kioskHub,
		   SyncHub syncHub)
        {
            _logger = loggerRepository;
            _repository = kioskRepository;
            _organisationRepository = orgRepository;
            _kioskHub = kioskHub;
			_syncHub = syncHub;
        }

        public List<KioskDetails> GetKioskList()
        {
            try
            {
                return _repository.GetKioskList();
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

		public IEnumerable<Kiosk> GetKiosks()
		{
			try
			{
				if (!HttpContext.Current.User.IsInRole("Practice Admin"))
				{
					return _repository.GetKiosks();
				}
				else
				{
					return _repository.GetKiosksByUser();
				}
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

		public IEnumerable<Kiosk> GetKiosksWithUsageLog()
        {
            try
            {
                var kiosksData = GetKiosks();

				IEnumerable<Kiosk> kioskList = kiosksData.ToList();
                try
                {
                    var kioskGuids = kioskList.Where(k => IsValidKioskGuid(k.KioskGuid)).Select(k => new Guid(k.KioskGuid)).Distinct().ToList();
                    var kioskUsageLogs = _repository.GetKioskUsageLog(kioskGuids);

					foreach (var kiosk in kioskList.Where(kiosk => kioskUsageLogs.Any(a => a.KioskGuid == kiosk.KioskGuid)))
					{
						kiosk.Usage = kioskUsageLogs.First(k => k.KioskGuid == kiosk.KioskGuid).UsageLog;
					}
				}
				catch (Exception ex1)
                {
                    _logger.WriteLog(Entities.Enums.LogType.Fatal, ex1.Message);
                }

				return kioskList;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Fatal, ex.Message);
                throw;
            }
        }

        private static bool IsValidKioskGuid(string kioskGuid)
        {
            Guid kioskGuid1;
            return !string.IsNullOrEmpty(kioskGuid) && Guid.TryParse(kioskGuid, out kioskGuid1);
        }

        public IEnumerable<Kiosk> GetKioskListForOrganisation(int organisationId)
        {
			IEnumerable<Kiosk> kioskList;
            try
            {
                kioskList = _repository.GetKioskListForOrganisation(organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return kioskList;
        }

        public IEnumerable<Kiosk> GetKioskDetailListForOrganisation(int organisationId)
        {
            IEnumerable<Kiosk> kioskList;
            try
            {
                kioskList = _repository.GetKioskDetailListForOrganisation(organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return kioskList;
        }

        public Kiosk GetKioskDetails(int kioskId, int organisationId)
        {
            Kiosk kiosk;
            try
            {
                kiosk = GetKioskDetails(kioskId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return kiosk;
        }

		public Kiosk GetKioskDetails(int kioskId)
		{
			Kiosk kiosk;
			try
			{
				kiosk = _repository.GetKioskDetails(kioskId);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return kiosk;
		}

		public int EditKiosk([FromBody]string value)
        {
            var kiosk = value.ConvertFromJsonString<Kiosk>();

            try
            {
                int status = _repository.GetKioskStatus(kiosk.Id);
				var productkeys = _repository.GetKioskSyncServiceKeys(kiosk.Id);

                List<string> removedOrganisations = _organisationRepository.GetOrganisationListForKiosk(kiosk.Id).Select(item => item.OrganisationName).ToList();

                int surveyModule = Convert.ToInt32(KioskModule.Survey);
                if (!kiosk.Module.Select(item => item.Id).SequenceEqual(kiosk.SelectedModules) &&
                    kiosk.Module.Select(item => item.Id).Contains(surveyModule) &&
                    HttpContext.Current.User.IsInRole("Practice Admin"))
                {
                    kiosk.SelectedModules.Add(surveyModule);
                }
                int resultKiosk = _repository.EditKiosk(kiosk);
				if(resultKiosk == -1)
				{
					return -1;
				}
				kiosk = GetKioskDetails(kiosk.Id);
				if (status != kiosk.Status)
                    _kioskHub.UpdateKioskStatus(kiosk.ConnectionGuid, Convert.ToInt32(kiosk.Status));

                _kioskHub.UpdateKioskConfiguration(kiosk);
				_syncHub.UpdateClientConfiguration(new Guid(productkeys.SyncGuid), productkeys.SyncConnectionGuid);
                _kioskHub.RemoveGroup(kiosk.ConnectionGuid, removedOrganisations);

                List<string> newOrgs = kiosk.OrganisationList.Select(item => item.OrganisationName).ToList();

                _kioskHub.JoinGroup(kiosk.ConnectionGuid, newOrgs);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }

            return kiosk.Id;
        }

        [AcceptVerbs("GET", "POST")]
        public string UpdateKiosk(int kioskId, string connectionId, int status)
        {
            try
            {
                if (status != (Convert.ToInt32(ConnectionStatus.Restart)))
                {
                    _repository.UpdateKioskStatus(kioskId, status);
                }
				string connectionGuid = String.IsNullOrEmpty(connectionId) ? _repository.GetConnectionId(kioskId) : connectionId;
                _kioskHub.UpdateKioskStatus(connectionGuid, status);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                return null;
            }

            return connectionId;
        }

		[AcceptVerbs("GET", "POST")]
		public string UpdateKioskStatusWithMessage(int kioskId, string connectionId, int status, String message)
		{
			try
			{
				if (status != (Convert.ToInt32(ConnectionStatus.Restart)))
				{
					_repository.UpdateKioskStatus(kioskId, status);
				}
				string connectionGuid = String.IsNullOrEmpty(connectionId) ? _repository.GetConnectionId(kioskId) : connectionId;
				_kioskHub.UpdateKioskStatus(connectionGuid, status, message);
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}

			return "value";
		}

		[HttpPost]
        public int AddKioskDetails([FromBody]string value)
        {
            try
            {
                var kiosk = JSONHelper.Deserialize<Kiosk>(value);
				return _repository.AddKioskDetails(kiosk);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IEnumerable<PatientMatch> GetPatientMatchList()
        {
            try
            {
                return _repository.GetPatientMatchList();
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IEnumerable<PatientMatch> GetAppointmentMatchList()
        {
            try
            {
                return _repository.GetAppointmentMatchList();
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IEnumerable<Module> GetModulesList()
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Practice Admin"))
                {
                    return _repository.GetModulesList();
                }
                else
                {
                    return _repository.GetModulesListForUser();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public IEnumerable<DemographicDetails> GetDemographicDetails()
        {
            try
            {
                return _repository.GetDemographicDetails();

            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

		public KioskMasterDetails GetKioskMasterDetails()
		{
			try
			{
				if (!HttpContext.Current.User.IsInRole("Practice Admin"))
				{
					return _repository.GetKioskMasterDetails();
				}
				else
				{
					return _repository.GetKioskMasterDetailsByUser();
				}
			}
			catch (Exception ex)
			{
				_logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
				throw;
			}
		}

		public void SaveAppointmentSlotType([FromBody]string value)
        {
            try
            {
                List<AppointmentSlotType> slotType = value.ConvertFromJsonString<List<AppointmentSlotType>>();
                _repository.SaveAppointmentSlotType(slotType);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        public List<AppointmentSlotType> GetAppointmentSlotTypes(int organisationId)
        {
            try
            {
                return _repository.GetAppointmentSlotTypes(organisationId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [AcceptVerbs("GET")]
        public bool ValidateKioskName(string kioskName, int kioskId, string organisationList)
        {
            bool status;
            _repository.ValidateKioskName(kioskName, kioskId, organisationList.ConvertFromJsonString<List<int>>(), out status);
            return status;
        }

        [AcceptVerbs("GET")]
        public bool DeleteKiosk(int kioskId)
        {
            try
            {
                _kioskHub.DisconnectKiosk(kioskId);
                var result = _repository.DeleteKiosk(kioskId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                return false;
            }
        }

        [AcceptVerbs("GET")]
        public KioskSyncKeys GetKioskSyncKeys(int kioskId)
        {
            try
            {
                return _repository.GetKioskSyncServiceKeys(kioskId);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }
    }
}
