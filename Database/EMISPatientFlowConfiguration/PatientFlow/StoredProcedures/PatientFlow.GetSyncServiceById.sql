if object_id ('[PatientFlow].[GetSyncServiceById]') is not null
	drop procedure [PatientFlow].[GetSyncServiceById];
go

create procedure [PatientFlow].[GetSyncServiceById]	
	@SyncServiceId int
as
begin	
	set nocount on;
	set transaction isolation level read committed;
select 
	S.ProductKey, 
	S.OrganisationId, 
	S.SyncServiceId, 
	S.IsActivated, 
	O.OrganisationName,
	s.KioskId,
	case when S.SyncConnectionId is not null then 1  
	when S.OrgConnectionId is not null then 1 else 0 end as IsConnected                  
from PatientFlow.SyncService as S 
inner join PatientFlow.Organisation as O 
on S.OrganisationId = O.OrganisationId
where (S.SyncServiceId = @SyncServiceId);   
end
