if object_id('[PatientFlow].[UpdateKioskConnection]') is not null
drop procedure [PatientFlow].[UpdateKioskConnection]
go

create procedure [PatientFlow].[UpdateKioskConnection]
	@KioskAddress uniqueidentifier,
	@ConnectionGuid uniqueidentifier
as
begin
	
	set nocount on;
	set  transaction isolation level read committed;

	update [PatientFlow].[Kiosk]
	set 
		ConnectionGuid=@ConnectionGuid,
		ConnectionStatus=KioskStatus,
		LastStatusUpdate = getdate()
	where KioskGuid=@KioskAddress

	select KioskId from [PatientFlow].[Kiosk]
	where  KioskGuid=@KioskAddress;

end
