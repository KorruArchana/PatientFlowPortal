if object_id ('[PatientFlow].[GetPatientFlowUserByConfigOrganisationId]') is not null
	drop procedure [PatientFlow].[GetPatientFlowUserByConfigOrganisationId];
go

create procedure [PatientFlow].[GetPatientFlowUserByConfigOrganisationId]
   @ConfigOrganisationId varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select  UserName, 
			[Password], 
			SupplierId, 
			OrganisationId, 
			IPAddress
	from PatientFlow.PatientFlowUser 
	where  ConfigOrganisationId = @ConfigOrganisationId;
end