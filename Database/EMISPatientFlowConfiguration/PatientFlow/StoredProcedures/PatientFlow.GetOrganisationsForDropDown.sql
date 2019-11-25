if object_id('[PatientFlow].[GetOrganisationsForDropDown]') is not null
	drop Procedure [PatientFlow].[GetOrganisationsForDropDown]
go

create procedure [PatientFlow].[GetOrganisationsForDropDown]
as
begin
	
	set nocount on;
	set transaction isolation level read committed;   
				 
	select   
		OrganisationId,
		OrganisationName,
		OrganisationKey,
		SystemTypeId
	from PatientFlow.Organisation
	order by OrganisationName
   	
end




