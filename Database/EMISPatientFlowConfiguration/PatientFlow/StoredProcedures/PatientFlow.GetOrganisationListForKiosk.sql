if object_id('[PatientFlow].[GetOrganisationListForKiosk]') is not null
drop Procedure [PatientFlow].[GetOrganisationListForKiosk]
go

create procedure [PatientFlow].[GetOrganisationListForKiosk]
@KioskId int
as
begin
	
set nocount on;
set transaction isolation level read committed;
select [organisationid], 
       [organisationname], 
       organisation.[systemtypeid],
       [sitenumber] 
       
from   [PatientFlow].[organisation] Organisation 
join [PatientFlow].[KioskLinkedToDetails] as KioskLinkedToDetails 
    on organisation.OrganisationId = KioskLinkedToDetails.TypeId 
	where KioskLinkedToDetails.KioskId=@KioskId
order  by organisation.[organisationname] 
end


