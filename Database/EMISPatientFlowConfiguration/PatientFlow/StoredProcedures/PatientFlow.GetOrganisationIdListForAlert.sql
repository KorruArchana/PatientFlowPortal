if object_id('[PatientFlow].[GetOrganisationIdListForAlert]') is not null
drop Procedure [PatientFlow].[GetOrganisationIdListForAlert]
go

create procedure [PatientFlow].[GetOrganisationIdListForAlert]
	@AlertId int
as
begin
	
	set nocount on;
        set transaction isolation level read committed;
	select OrganisationId as OrganisationId 
	from [PatientFlow].[AlertLinkToOrganisation]
	where AlertId=@AlertId
end
