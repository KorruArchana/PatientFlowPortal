if object_id ('[PatientFlow].[SaveKioskDetails]') is not null
	drop procedure [PatientFlow].[SaveKioskDetails];
go

create procedure [PatientFlow].[SaveKioskDetails]
   @KioskGuid varchar(50),
   @PCName varchar(50),
   @IpAddress varchar(50),
   @KioskVersion varchar(50) = null
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	update PatientFlow.Kiosk
	set 
		PCName = @PCName,
		IPAddress = @IpAddress,
		KioskVersion = @KioskVersion
	where KioskGuid = @KioskGuid
	
end