if object_id('[PatientFlow].[GetOrganisationDetails]') is not null
drop Procedure [PatientFlow].[GetOrganisationDetails]
go

create procedure [PatientFlow].[GetOrganisationDetails] 
	@OrganisationId	int
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    select 
		Organisation.OrganisationId,
		OrganisationName,
		Organisation.[SystemTypeId],
		isnull([SystemType],'None') as SystemType,
		PatientFlowUser.IPAddress,
		PatientFlowUser.InternalIPAddress,
		SiteNumber,
		PatientFlowUser.UserName,
		[Password],
		SiteNumber,
		SupplierId,
		Organisation.OrganisationKey,
		PatientFlowUser.WebServiceUrl,
		Organisation.Modified,
		(
			case when count(Department.OrganisationId)> 0 then 1
				 when count(KioskOrgs.TypeId)> 0 then 1
				 when count(questionnaire.OrganisationId)> 0 then 1
				 when count(OrgAccessMapping.OrganisationId)> 0 then 1
				 else 0 
			end
		) as LinkCount
	from [PatientFlow].Organisation as Organisation
	join [PatientFlow].[OrganisationSystemType] SystemType on Organisation.SystemTypeId=SystemType.SystemTypeId
	join [PatientFlow].[PatientFlowUser] PatientFlowUser on Organisation.OrganisationId=PatientFlowUser.OrganisationId
	left join PatientFlow.Department Department on Department.OrganisationId=Organisation.OrganisationId
	left join PatientFlow.KioskLinkedToDetails KioskOrgs on KioskOrgs.TypeId=Organisation.OrganisationId
	left join PatientFlow.Questionnaire Questionnaire on questionnaire.OrganisationId=Organisation.OrganisationId
	left join PatientFlow.OrganisationAccessMapping OrgAccessMapping on Organisation.OrganisationId = OrgAccessMapping.OrganisationId
	where Organisation.OrganisationId=@OrganisationId
	group by 
		Organisation.OrganisationId,
		Organisation.OrganisationName,
		Organisation.SystemTypeId,
		SystemType.SystemType,
		PatientFlowUser.IPAddress,
		PatientFlowUser.InternalIPAddress,
		SiteNumber,
		PatientFlowUser.UserName,
		[Password],
		SiteNumber,
		SupplierId,
		Organisation.OrganisationKey,
		PatientFlowUser.WebServiceUrl,
		Organisation.Modified
		
	
	select 
		s.SiteId,
		s.SiteDBID,
		s.SiteName 
	from [PatientFlow].[Site] as s 
	where s.OrganisationId= @OrganisationId order by s.SiteName

end




