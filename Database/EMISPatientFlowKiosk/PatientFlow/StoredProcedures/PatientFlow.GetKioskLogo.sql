if object_id ('[PatientFlow].[GetKioskLogo]') is not null
	drop procedure [PatientFlow].[GetKioskLogo];
go

create procedure [PatientFlow].[GetKioskLogo]
   @KioskId varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select  
		Logo		
	from  [PatientFlow].[KioskLogo]
	where  KioskId = @KioskId;
end