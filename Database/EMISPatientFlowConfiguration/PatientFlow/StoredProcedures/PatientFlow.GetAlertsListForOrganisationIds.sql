if object_id ('[PatientFlow].[GetAlertsListForOrganisationIds]') is not null
	drop procedure [PatientFlow].[GetAlertsListForOrganisationIds];
go

create procedure [PatientFlow].[GetAlertsListForOrganisationIds]
	@Organisations [PatientFlow].[List] readonly
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
	 AlertDisplayFormatTypeId,
	 Age2,
	 Link.OrganisationId 
from [PatientFlow].[Alert] Alert
join [PatientFlow].[AlertLinkToOrganisation] [Link] 
	on Alert.AlertId=Link.AlertId
where Link.Organisationid in( select id from @Organisations);

end

