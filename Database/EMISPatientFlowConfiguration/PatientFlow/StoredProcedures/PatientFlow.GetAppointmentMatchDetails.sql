if object_id ('[PatientFlow].[GetAppointmentMatchDetails]') is not null
	drop procedure [PatientFlow].[GetAppointmentMatchDetails];
go
create procedure [PatientFlow].[GetAppointmentMatchDetails] 
	@AppointmentMatchId	int
as
begin
set nocount on;
set transaction isolation level read committed;
SELECT       
	AppointmentMatchId, 
	AppointmentMatchTitle,
	ScreenOrder,
	ModifiedBy,
	Modified
FROM PatientFlow.KioskAppointmentMatch
where AppointmentMatchId=@AppointmentMatchId
end
