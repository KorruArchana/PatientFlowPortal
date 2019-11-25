if object_id ('[PatientFlow].[GetActiveGroupsForSyncService]') is not null
	drop procedure [PatientFlow].[GetActiveGroupsForSyncService];
go
create procedure [PatientFlow].[GetActiveGroupsForSyncService] 
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	Organisation.OrganisationName 
from [patientFlow].[SyncService] SyncService
join [PatientFlow].[Organisation] Organisation on SyncService.OrganisationId=Organisation.OrganisationId
where SyncConnectionId is not null
end
