if object_id ('[PatientFlow].[GetIpAddress]') is not null
	drop procedure [PatientFlow].[GetIpAddress]
go

create procedure [PatientFlow].[GetIpAddress]
as
begin

	set nocount on;
	set transaction isolation level read committed;

	select 
		distinct([IPAddress]) as IPAddress
	from PatientFlow.PatientFlowUser
	 
end
go
