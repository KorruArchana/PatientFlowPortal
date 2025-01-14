if object_id('[PatientFlow].[GetOrganisationListByUser]') is not null
drop Procedure [PatientFlow].[GetOrganisationListByUser]
go

create procedure [PatientFlow].[GetOrganisationListByUser]
	@UserName varchar(128)
as
begin
	
	set nocount on;
        set transaction isolation level read committed;
		select 
			Organisation.[OrganisationId],
			Organisation.[OrganisationName],
			Organisation.[SystemTypeId],
			isnull([SystemType],'None') as SystemType,
			[SiteNumber],
			(case when count(Department.OrganisationId)>0
			then 1 
			when count(KioskOrgs.TypeId)>0
			then 1
			else 0 
			End) as LinkCount                   
		from PatientFlow.Organisation
		inner join [PatientFlow].OrganisationAccessMapping on [PatientFlow].Organisation.OrganisationId = [PatientFlow].OrganisationAccessMapping.OrganisationId
		left join [PatientFlow].[OrganisationSystemType] SystemType on Organisation.SystemTypeId=SystemType.SystemTypeId
		left join [PatientFlow].[Department] Department on Department.OrganisationId=Organisation.OrganisationId
		left join [PatientFlow].KioskLinkedToDetails KioskOrgs on KioskOrgs.TypeId=Organisation.OrganisationId
		where upper([PatientFlow].OrganisationAccessMapping.UserName) = upper(@UserName)			
			group by Organisation.[OrganisationId],Organisation.[OrganisationName], Organisation.[SystemTypeId], SystemType,[SiteNumber]
		order by Organisation.[OrganisationName]
		
end