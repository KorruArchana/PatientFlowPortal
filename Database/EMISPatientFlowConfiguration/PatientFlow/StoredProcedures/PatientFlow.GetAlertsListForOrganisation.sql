if object_id ('[PatientFlow].[GetAlertsListForOrganisation]') is not null
	drop procedure [PatientFlow].[GetAlertsListForOrganisation];
go

create procedure [PatientFlow].[GetAlertsListForOrganisation]
	@OrganisationId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	 Alert.ALertId,
	 AlertType,
	 AlertText,
	 Gender,
	 Age1,
	 Gender,
	 Operation,
	 Age2,
	 Link.OrganisationId 
from [PatientFlow].[Alert] Alert
join [PatientFlow].[AlertLinkToOrganisation] [Link] 
on Alert.AlertId=Link.AlertId
where LInk.Organisationid=@OrganisationId
end
