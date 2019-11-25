if object_id('[PatientFlow].[GetPatients]') is not null
drop Procedure [PatientFlow].[GetPatients]
go

create procedure [PatientFlow].[GetPatients]
as
begin

	set nocount on;
	set transaction isolation level read committed;    
	
	select
		PatientMessage.PatientMessageId,
		PatientMessage.Message,							  
		Patient.OrganisationId,
		Organisation.OrganisationName,
		Patient.PatientId,
		Firstname,
		Surname
	from PatientFlow.PatientMessage PatientMessage 
	join PatientFlow.Patient Patient on PatientMessage.PatientId=Patient.PatientId
	join PatientFlow.Organisation Organisation on Patient.OrganisationId=Organisation.OrganisationId


end
go


