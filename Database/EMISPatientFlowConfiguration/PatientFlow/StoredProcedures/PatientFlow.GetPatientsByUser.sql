if object_id('[PatientFlow].[GetPatientsByUser]') is not null
	drop Procedure [PatientFlow].[GetPatientsByUser]
go

create procedure PatientFlow.GetPatientsByUser
	@User varchar(200)
as
begin

	set nocount on;
	set transaction isolation level read committed;    
	
	select
		PatientMessage.PatientMessageId,
		PatientMessage.Message,							  
		Patient.OrganisationId,
		Org.OrganisationName,
		Patient.PatientId,
		Firstname,
		Surname
	from PatientFlow.PatientMessage PatientMessage 
	join PatientFlow.Patient Patient on PatientMessage.PatientId=Patient.PatientId
	join PatientFlow.Organisation Org on Patient.OrganisationId=Org.OrganisationId
	join PatientFlow.OrganisationAccessMapping accessMapping on Org.OrganisationId = accessMapping.OrganisationId	
	where accessMapping.UserName = @User;


end
go