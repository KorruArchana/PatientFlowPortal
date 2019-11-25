if object_id ('[PatientFlow].[GetKioskConnectionStatus]') is not null
drop procedure [PatientFlow].[GetKioskConnectionStatus];
go

create procedure [PatientFlow].[GetKioskConnectionStatus]
	@KioskId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
ConnectionStatus 
from [PatientFlow].Kiosk
where KioskId=@KioskId
end
