if object_id ('[PatientFlow].[GetKioskIdfromkey]') is not null
	drop procedure [PatientFlow].[GetKioskIdfromkey];
go

create procedure [PatientFlow].[GetKioskIdfromkey]
	@Key varchar(50)
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	 KioskId
from [PatientFlow].Kiosk
where KioskGuid=@key
end
