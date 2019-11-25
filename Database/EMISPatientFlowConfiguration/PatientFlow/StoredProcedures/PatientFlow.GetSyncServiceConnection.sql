if object_id ('[PatientFlow].[GetSyncServiceConnection]') is not null
	drop procedure [PatientFlow].[GetSyncServiceConnection];
go

create procedure [PatientFlow].[GetSyncServiceConnection]	
	@OrganisationId int,
	@Type char(1)
as
begin
	set nocount on;
	set transaction isolation level read committed;
   select ConnectionId from 
    (select case 
		when @Type = 'S' then SyncConnectionId 
	    when @Type =  'O'  then OrgConnectionId
	    end as ConnectionId
	from PatientFlow.SyncService
    where (OrganisationId = @OrganisationId)) as tbl where ConnectionId is not null
end
