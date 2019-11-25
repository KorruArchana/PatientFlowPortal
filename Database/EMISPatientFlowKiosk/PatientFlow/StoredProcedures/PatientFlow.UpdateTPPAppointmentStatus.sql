if object_id ('[PatientFlow].[UpdateTPPAppointmentStatus]') is not null
	drop procedure [PatientFlow].[UpdateTPPAppointmentStatus];
go

create procedure [PatientFlow].[UpdateTPPAppointmentStatus] 
	@TPPAppointmentId varchar(20),
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
	where (Reception = @TPPAppointmentId) and 
		  (OrganisationId = @OrganisationId);
end
