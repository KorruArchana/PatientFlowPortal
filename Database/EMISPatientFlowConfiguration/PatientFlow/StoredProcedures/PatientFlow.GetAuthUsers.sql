if object_id ('[PatientFlow].[GetAuthUsers]') is not null
	drop procedure [PatientFlow].[GetAuthUsers];
go

create procedure [PatientFlow].[GetAuthUsers]
	@User varchar(128) = null
as
begin
set nocount on;
set transaction isolation level read committed;

	select 
		UserName,
		org.OrganisationId,
		OrganisationName
	from PatientFlow.OrganisationAccessMapping oam
	join PatientFlow.Organisation org on oam.OrganisationId = org.OrganisationId
	where org.OrganisationId in 
	(select 
		distinct OrganisationId 
	from PatientFlow.OrganisationAccessMapping 
	where UserName like '%' + isnull(@User,'') + '%')

end

