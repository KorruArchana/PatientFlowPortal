if object_id ('[PatientFlow].[GetDepartmentList]') is not null
	drop procedure [PatientFlow].[GetDepartmentList];
go

create procedure [PatientFlow].[GetDepartmentList]
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	DepartmentId,
	DepartmentName,
	OrganisationId
from [PatientFlow].Department
end
