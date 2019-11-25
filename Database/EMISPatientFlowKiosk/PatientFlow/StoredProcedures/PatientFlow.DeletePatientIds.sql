if (object_id('PatientFlow.DeletePatientIds') is not null)
		Drop Procedure PatientFlow.DeletePatientIds
Go

create procedure PatientFlow.DeletePatientIds
	@DeletedOrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;

	declare @List PatientFLow.List
	insert into @List (Id)
	(
		select PatientFlowPatientId from PatientFlow.Patient 
		where OrganisationId= @DeletedOrganisationId
	)
	delete from [PatientFlow].[NonAnonymousSurveyFrequency] 
	where PatientFlowPatientId in (select Id from @List)

	delete from [PatientFlow].[Appointment] 
	where PatientFlowPatientId in (select Id from @List) 

	delete from [PatientFlow].[Patient]
	where OrganisationId = @DeletedOrganisationId

	delete from @List
end