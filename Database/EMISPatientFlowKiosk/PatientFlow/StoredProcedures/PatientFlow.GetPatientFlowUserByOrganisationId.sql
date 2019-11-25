if object_id ('[PatientFlow].[GetPatientFlowUserByOrganisationId]') is not null
	drop procedure [PatientFlow].[GetPatientFlowUserByOrganisationId];
go

create procedure [PatientFlow].[GetPatientFlowUserByOrganisationId]
   @OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
select  
		UserName, 
		[Password], 
		SupplierId, 
		OrganisationKey, 
		IPAddress,
		WebServiceUrl,
		SystemType
	from PatientFlow.PatientFlowUser p inner join 
	PatientFlow.Organisation o on p.OrganisationId = o.OrganisationId
	where p.OrganisationId = @OrganisationId
end