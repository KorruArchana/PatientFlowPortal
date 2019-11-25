using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface ISyncServiceRepository
    {
        bool IsExistSyncService(Guid productKey);
        List<KeyValuePair<int, string>> GetSyncServiceOrganisations(Guid productKey);
        List<WebUser> GetPatientFlowUsers(Guid productKey);
        SyncService GetSyncServiceById(int serviceId);
        List<string> GetSyncServiceConnections(int organisationId, char connectionType);
        List<SyncService> GetSyncServices(string organisation, int pageNo, int pageSize, out int recordCount);
        int SyncServiceConnected(Guid productKey, Guid connectionId, char connectionType);
        int SyncServiceDisconnected(Guid productKey, Guid connectionId, char connectionType);
        void SaveSyncService(SyncService service);
        void SaveSyncService(Guid productKey, List<int> organisationIds, bool isActive,int kioskId);
        void DeleteSyncService(int serviceId);
        void UpdateSyncServiceStatus(int serviceId, bool status);
        void RemoveClientConnections(char type);
        List<string> GetActiveGroupsForSyncService();
    }
}
