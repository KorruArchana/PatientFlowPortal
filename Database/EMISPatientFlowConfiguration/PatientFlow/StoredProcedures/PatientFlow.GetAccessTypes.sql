if object_id ('PatientFlow.GetAccessTypes') is not null
	drop procedure PatientFlow.GetAccessTypes;
go

create procedure PatientFlow.GetAccessTypes
as
begin
set nocount on;
set transaction isolation level read committed;
select	
	AccessTypeId, 
	Name 
from PatientFlow.AccessType
end
