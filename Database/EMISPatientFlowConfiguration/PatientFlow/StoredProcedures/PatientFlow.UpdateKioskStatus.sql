if object_id('[PatientFlow].[UpdateKioskStatus]') is not null
drop procedure [PatientFlow].[UpdateKioskStatus]
go

create procedure [PatientFlow].[UpdateKioskStatus]
	@KioskId	int,
	@Status		int
as
begin
	
	set nocount on;
	set  transaction isolation level read committed;

	update [PatientFlow].Kiosk
	set
		[ConnectionStatus]=@Status,
		KioskStatus=@Status
	where KioskId=@KioskId;
end
