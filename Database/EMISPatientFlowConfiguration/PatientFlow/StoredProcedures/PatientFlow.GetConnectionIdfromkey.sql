if object_id ('[PatientFlow].[GetConnectionIdfromkey]') is not null
	drop procedure [PatientFlow].[GetConnectionIdfromkey];
go

create procedure [PatientFlow].[GetConnectionIdfromkey]
	@Key varchar(50)
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	 ConnectionGuid
from [PatientFlow].Kiosk
where KioskGuid=@key
end
