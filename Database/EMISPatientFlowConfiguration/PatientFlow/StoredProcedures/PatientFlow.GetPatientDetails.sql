if object_id('[PatientFlow].[GetPatientDetails]') is not null
drop Procedure [PatientFlow].[GetPatientDetails]
go

create procedure [PatientFlow].[GetPatientDetails]
	@PatientMessageId int
as
begin
	
	set nocount on;
	set transaction isolation level read committed;	
    select 
		[Patient].PatientId,
		[Patient].OrganisationId,
		Firstname,
		Surname,
		DOB,
		[PatientMessage].[Message],
		[PatientMessage].[PatientMessageId]
	 from [PatientFlow].[PatientMessage] as PatientMessage
	 join [PatientFlow].[Patient] on PatientMessage.PatientId=Patient.PatientId and PatientMessage.OrganisationId = Patient.OrganisationId
	 where PatientMessage.PatientMessageId=@PatientMessageId
end

