if object_id ('[PatientFlow].[UpdateAppointmentStatus]') is not null
	drop procedure [PatientFlow].[UpdateAppointmentStatus];
go

create procedure [PatientFlow].[UpdateAppointmentStatus] 
	@AppointmentId int,
	@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;

    update a
	set      
		AppointmentStatusId = 1
		from [PatientFlow].[Appointment] a 
		join PatientFlow.Member m
		on a.PatientFlowMemberId = m.PatientFlowMemberId
	where (AppointmentId = @AppointmentId) and 
		  (OrganisationId = @OrganisationId);
end
