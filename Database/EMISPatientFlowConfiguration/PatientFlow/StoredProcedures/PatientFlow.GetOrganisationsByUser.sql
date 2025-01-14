if object_id('[PatientFlow].[GetOrganisationsByUser]') is not null
drop Procedure [PatientFlow].[GetOrganisationsByUser]
go

create procedure [PatientFlow].[GetOrganisationsByUser] 
	@UserName varchar(128)
as
begin
	
	set nocount on;
	set transaction isolation level read committed;   
	
	select 
		Org.OrganisationName,
		Org.OrganisationId,
		Org.SystemTypeId,
		isnull(SystemType,'None') as SystemType,
		SiteNumber,
		(
			case when count(Department.OrganisationId)> 0 then 1
				 when count(KioskOrgs.TypeId)> 0 then 1
				 when count(questionnaire.OrganisationId)> 0 then 1
				 else 0 
			end
		) as LinkCount                    
	 from PatientFlow.Organisation as Org
	 join PatientFlow.OrganisationAccessMapping OrgAccessMapping on Org.OrganisationId = OrgAccessMapping.OrganisationId
	 join PatientFlow.OrganisationSystemType systemType on Org.SystemTypeId=systemType.SystemTypeId
	 left join PatientFlow.Department department on department.OrganisationId=Org.OrganisationId
	 left join PatientFlow.KioskLinkedToDetails kioskOrgs on kioskOrgs.TypeId=Org.OrganisationId
	 left join PatientFlow.Questionnaire questionnaire on questionnaire.OrganisationId=Org.OrganisationId  
	 where OrgAccessMapping.UserName = @UserName
	 group by 
		Org.OrganisationName, 
		Org.OrganisationId,
		Org.SystemTypeId,
		SystemType,
		SiteNumber
	
end




