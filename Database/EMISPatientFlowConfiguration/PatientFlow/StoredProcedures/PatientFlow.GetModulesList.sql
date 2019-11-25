if object_id ('[PatientFlow].[GetModulesList]') is not null
drop procedure [PatientFlow].[GetModulesList];
go

create procedure [PatientFlow].[GetModulesList]
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
select 
	ModuleId,
	ModuleName 
from PatientFlow.Module
where ModuleName not in ('Arrival by Barcode')

end
