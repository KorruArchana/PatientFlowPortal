if object_id ('[PatientFlow].[DisconnectKiosk]') is not null
	drop procedure [PatientFlow].[DisconnectKiosk];
go

create procedure [PatientFlow].[DisconnectKiosk]
	@ConnectionGuid varchar(50)
AS
BEGIN
	
	set NOCOUNT on;
    set transaction isolation level read committed;
    
	update [PatientFlow].[Kiosk]
		set 
		ConnectionGuId=null,
		ConnectionStatus=3,
		LastStatusUpdate = getdate()
	where KioskGuid=@ConnectionGuid
END
