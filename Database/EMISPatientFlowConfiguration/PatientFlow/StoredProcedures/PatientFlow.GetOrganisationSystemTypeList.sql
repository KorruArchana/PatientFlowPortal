if object_id('[PatientFlow].[GetOrganisationSystemTypeList]') is not null
drop Procedure [PatientFlow].[GetOrganisationSystemTypeList]
go

create procedure [PatientFlow].[GetOrganisationSystemTypeList]
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
	select 
		SystemTypeId,
		SystemType
    from [PatientFlow].[OrganisationSystemType] 
    where SystemTypeId not in (3,4,5)
    order by SystemTypeId
		
end
