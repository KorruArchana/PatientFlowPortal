if object_id ('[PatientFlow].[GetKioskStatus]') is not null
drop procedure [PatientFlow].[GetKioskStatus];
go

create procedure [PatientFlow].[GetKioskStatus]
	@KioskId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
KioskStatus 
from [PatientFlow].Kiosk
where KioskId=@KioskId
end
