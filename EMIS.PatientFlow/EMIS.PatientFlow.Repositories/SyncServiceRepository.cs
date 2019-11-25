using System;
using System.Collections.Generic;
using System.Linq;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class SyncServiceRepository : BaseRepository, ISyncServiceRepository
    {
        public bool IsExistSyncService(Guid productKey)
        {
			try
			{
				return DbAccess.IsExistSyncService(productKey);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return false;
			}
        }

        public List<KeyValuePair<int, string>> GetSyncServiceOrganisations(Guid productKey)
        {
			try
			{
				return DbAccess.GetSyncServiceOrganisations(productKey);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<KeyValuePair<int, string>>();
			}
        }

        public int SyncServiceConnected(Guid productKey, Guid connectionId, char connectionType)
        {
			try
			{
				return DbAccess.SaveSyncServiceConnection(productKey, connectionId, connectionType, false);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
        }

        public int SyncServiceDisconnected(Guid productKey, Guid connectionId, char connectionType)
        {
			try
			{
				return DbAccess.SaveSyncServiceConnection(productKey, connectionId, connectionType, true);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
        }

        public List<WebUser> GetPatientFlowUsers(Guid productKey)
        {
			try
			{
				return DbAccess.GetPatientFlowUsers(productKey);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<WebUser>();
			}
        }

        public List<string> GetSyncServiceConnections(int organisationId, char connectionType)
        {
			try
			{
				return DbAccess.GetSyncServiceConnections(organisationId, connectionType);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<string>();
			}
        }

        public void SaveSyncService(SyncService service)
        {
			try
			{
				DbAccess.SaveSyncService(service, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
			}
        }

        public List<SyncService> GetSyncServices(string organisation, int pageNo, int pageSize, out int recordCount)
        {
			try
			{
				return DbAccess.GetSyncServices(organisation, pageNo, pageSize, out recordCount);
			}
			catch (Exception ex)
			{
				recordCount = -1;
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<SyncService>();
			}
        }

        public SyncService GetSyncServiceById(int serviceId)
        {
			try
			{
				SyncService service = DbAccess.GetSyncServiceById(serviceId);
				service.OrganisationIds = GetSyncServiceOrganisations(service.ProductKey).Select(x => x.Key).ToList();
				return service;
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new SyncService();
			}
        }

        public void DeleteSyncService(int serviceId)
        {
			try
			{
				DbAccess.DeleteSyncService(serviceId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
			}
        }

        public void SaveSyncService(Guid productKey, List<int> organisationIds, bool isActive,int kioskId)
        {
			try
			{
				DbAccess.SaveSyncService(productKey, organisationIds, isActive, CurrentUser, kioskId);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
			}
        }

        public void UpdateSyncServiceStatus(int serviceId, bool status)
        {
			try
			{
				SyncService service = DbAccess.GetSyncServiceById(serviceId);

				service.IsActive = status;
				service.Id = serviceId;
				DbAccess.SaveSyncService(service, CurrentUser);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
			}
        }

        public void RemoveClientConnections(char type)
        {
			try
			{
				DbAccess.RemoveClientConnections(type);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
			}
        }

        public List<string> GetActiveGroupsForSyncService()
        {
			try
			{
				return DbAccess.GetActiveGroupsForSyncService();
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<string>();
			}
        }
    }
}
