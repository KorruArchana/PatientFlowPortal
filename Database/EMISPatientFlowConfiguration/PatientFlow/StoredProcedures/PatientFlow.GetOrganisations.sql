if object_id('[PatientFlow].[GetOrganisations]') is not null
	drop Procedure [PatientFlow].[GetOrganisations]
go

create procedure [PatientFlow].[GetOrganisations] 
as
begin
	
	set nocount on;
	set transaction isolation level read committed;   
	
	select   
		Organisation.OrganisationName,
		Organisation.OrganisationId,
		Organisation.SystemTypeId,
		isnull(SystemType,'None') as SystemType,
		SiteNumber,
		(
			case when count(Department.OrganisationId)> 0 then 1
				 when count(KioskOrgs.TypeId)> 0 then 1
				 when count(questionnaire.OrganisationId)> 0 then 1
				 when count(OrgAccessMapping.OrganisationId)> 0 then 1
				 else 0 
			end
		) as LinkCount                    
	 from PatientFlow.Organisation as Organisation
	 join PatientFlow.OrganisationSystemType SystemType on Organisation.SystemTypeId=SystemType.SystemTypeId
	 left join PatientFlow.Department Department on Department.OrganisationId=Organisation.OrganisationId
	 left join PatientFlow.KioskLinkedToDetails KioskOrgs on KioskOrgs.TypeId=Organisation.OrganisationId
	 left join PatientFlow.Questionnaire Questionnaire on questionnaire.OrganisationId=Organisation.OrganisationId  
	 left join PatientFlow.OrganisationAccessMapping OrgAccessMapping on Organisation.OrganisationId = OrgAccessMapping.OrganisationId
	 group by 
		Organisation.OrganisationName, 
		Organisation.OrganisationId,
		Organisation.SystemTypeId,
		SystemType,
		SiteNumber
	
end




