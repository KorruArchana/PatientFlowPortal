if object_id ('[PatientFlow].[GetAppointmentMatchList]') is not null
	drop procedure [PatientFlow].[GetAppointmentMatchList];
go

create procedure [PatientFlow].[GetAppointmentMatchList]
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select 
		AppointmentMatchId,
		AppointmentMatchTitle,
		ScreenOrder
	from PatientFlow.KioskAppointmentMatch
end
