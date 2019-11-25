if object_id ('[PatientFlow].[GetAlertOrganisationList]') is not null
	drop procedure [PatientFlow].[GetAlertOrganisationList];
go

create procedure [PatientFlow].[GetAlertOrganisationList]
	@AlertId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	[Alertmaster].OrganisationId,
	OrganisationName,
	OrganisationKey   
from [PatientFlow].[Organisation] [Org]
join [PatientFlow].[AlertLinkToOrganisation] [Alertmaster] 
on [Org].OrganisationId=[Alertmaster].OrganisationId
where [Alertmaster].[AlertId]=@AlertId
end
