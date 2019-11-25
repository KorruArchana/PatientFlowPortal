if object_id ('[PatientFlow].[GetModulesListForUser]') is not null
drop procedure [PatientFlow].[GetModulesListForUser];
go

create procedure [PatientFlow].[GetModulesListForUser]
as
begin
set nocount on;
set transaction isolation level read committed;

select 
	ModuleId,
	ModuleName 
from PatientFlow.Module
where ModuleName not in ('Survey' ,'Arrival by Barcode')

end