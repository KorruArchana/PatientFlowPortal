if object_id ('[PatientFlow].[GetSyncServiceOrganisationList]') is not null
	drop procedure [PatientFlow].[GetSyncServiceOrganisationList];
go

create procedure [PatientFlow].[GetSyncServiceOrganisationList]	
	@ProductKey varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;
select     
	O.OrganisationName, 
	O.OrganisationId
from PatientFlow.SyncService as S 
inner join PatientFlow.Organisation as O 
on S.OrganisationId = O.OrganisationId
where (S.ProductKey = @ProductKey);    
end
