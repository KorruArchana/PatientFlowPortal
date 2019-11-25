if object_id ('[PatientFlow].[GetConnectionId]') is not null
	drop procedure [PatientFlow].[GetConnectionId];
go

create procedure [PatientFlow].[GetConnectionId]
	@KioskId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
    ConnectionGuid 
from [PatientFlow].Kiosk
where KioskId=@KioskId
end
