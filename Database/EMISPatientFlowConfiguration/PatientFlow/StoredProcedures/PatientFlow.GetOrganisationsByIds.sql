if object_id('[PatientFlow].[GetOrganisationsByIds]') is not null
drop Procedure [PatientFlow].[GetOrganisationsByIds]
go
create procedure [PatientFlow].[GetOrganisationsByIds]
	-- Add the parameters for the stored procedure here
	@Organisations [PatientFlow].[List] readonly
AS
BEGIN
	set nocount on;
    set transaction isolation level read committed;

	select 
		OrganisationName, 
		OrganisationId,
		SystemTypeId,
		SiteNumber
	from       PatientFlow.Organisation 
	where OrganisationId in (
								select 
									id 
								from @Organisations
							)
END
GO
