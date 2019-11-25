if object_id('[PatientFlow].[GetOrganisationListForAlert]') is not null
drop Procedure [PatientFlow].[GetOrganisationListForAlert]
go

create procedure [PatientFlow].[GetOrganisationListForAlert]
	@AlertId int
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
	select 
		Organisation.OrganisationId, 
		Organisation.OrganisationName as OrganisationName 
	from [PatientFlow].[AlertLinkToOrganisation] AlertLinkToOrganisation
	join [PatientFlow].[Organisation] Organisation on Organisation.OrganisationId=AlertLinkToOrganisation.OrganisationId
	where AlertId=@AlertId
end
