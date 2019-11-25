if object_id ('[PatientFlow].[DeleteKioskLogo]') is not null
	drop procedure [PatientFlow].[DeleteKioskLogo];
go

create procedure [PatientFlow].[DeleteKioskLogo]
   @KioskId varchar(50)  
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	if exists(select 1 from [PatientFlow].[KioskLogo] where KioskId=@KioskId)
	begin
		delete from PatientFlow.KioskLogo where KioskId=@KioskId;
	end
	
end
