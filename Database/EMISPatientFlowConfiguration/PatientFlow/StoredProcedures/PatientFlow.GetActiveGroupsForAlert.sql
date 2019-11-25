if object_id ('[PatientFlow].[GetActiveGroupsForAlert]') is not null
	drop procedure [PatientFlow].[GetActiveGroupsForAlert];
go
create procedure [PatientFlow].[GetActiveGroupsForAlert] 
	@AlertId int
as
begin
set nocount on;
set transaction isolation level read committed;

select distinct
	OrganisationName 
from [PatientFlow].[AlertLinkToOrganisation] LinkedOrg
join [PatientFlow].[Organisation] org 
	on LinkedOrg.OrganisationId=org.OrganisationId
join [PatientFlow].[KioskLinkedToDetails] KioskLinks 
	on LinkedOrg.OrganisationId=KioskLinks.typeId
join [PatientFlow].[Kiosk] kiosk 
	on KioskLinks.kioskId=kiosk.KioskId
where AlertId=@AlertId and kiosk.ConnectionGuid is not null

end
