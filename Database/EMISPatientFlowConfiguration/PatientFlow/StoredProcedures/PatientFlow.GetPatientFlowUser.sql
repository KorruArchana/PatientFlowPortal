if object_id('[PatientFlow].[GetPatientFlowUser]') is not null
drop Procedure [PatientFlow].[GetPatientFlowUser]
go

create procedure [PatientFlow].[GetPatientFlowUser] --[PatientFlow].[GetOrganisationDetails] 6
	@OrganisationId	int
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    select 
		PatientFlowUser.Username,
		PatientFlowUser.[Password],
		Organisation.OrganisationKey,
		PatientFlowUser.IPAddress,
		PatientFlowUser.SupplierId,
		SystemType.SystemType,
		WebServiceUrl
	from [PatientFlow].Organisation as Organisation
	left join [PatientFlow].[PatientFlowUser] PatientFlowUser on Organisation.OrganisationId=PatientFlowUser.OrganisationId
	join [PatientFlow].[OrganisationSystemType] SystemType on Organisation.SystemTypeId=SystemType.SystemTypeId
	where Organisation.OrganisationId=@OrganisationId

end




