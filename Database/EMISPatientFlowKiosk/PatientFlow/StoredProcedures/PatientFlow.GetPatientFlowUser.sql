if object_id ('[PatientFlow].[GetPatientFlowUser]') is not null
	drop procedure [PatientFlow].[GetPatientFlowUser];
go

create procedure [PatientFlow].[GetPatientFlowUser]
@SystemType varchar(50) = ''
as
begin
	set nocount on;
	set transaction isolation level read committed;
	if @SystemType = ''
		select    
			p.UserName, 
			p.[Password], 
			p.SupplierId, 
			p.OrganisationId, 
			p.IPAddress, 
			o.SystemType as [Type],
			o.OrganisationKey,
			p.WebServiceUrl
		from PatientFlow.PatientFlowUser as p 
		inner join PatientFlow.Organisation as o 
		on p.OrganisationId = o.OrganisationId 
	else
	   	select    
			p.UserName, 
			p.[Password], 
			p.SupplierId, 
			p.OrganisationId, 
			p.IPAddress, 
			o.SystemType as [Type],
			o.OrganisationKey,
			p.WebServiceUrl
		from PatientFlow.PatientFlowUser as p 
		inner join PatientFlow.Organisation as o 
		on p.OrganisationId = o.OrganisationId where o.SystemType = @SystemType
end
