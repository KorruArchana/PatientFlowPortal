if object_id ('[PatientFlow].[GetAppointmentsStatus]') is not null
	drop procedure [PatientFlow].[GetAppointmentsStatus];
go

create procedure [PatientFlow].[GetAppointmentsStatus]
	@PatientId int,
	@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select AppointmentId
	from PatientFlow.Appointment app
	join PatientFlow.Patient patient on patient.PatientFlowPatientId = app.PatientFlowPatientId
	join PatientFlow.Organisation org on patient.OrganisationId = org.OrganisationId 
	where AppointmentDate = cast(getdate() as date)
		and AppointmentStatusId = 1
		and patient.PatientId = @PatientId
		and patient.OrganisationId = @OrganisationId
	
end
