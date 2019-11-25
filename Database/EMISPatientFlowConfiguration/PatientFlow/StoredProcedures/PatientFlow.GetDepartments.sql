if object_id ('[PatientFlow].[GetDepartments]') is not null
drop procedure [PatientFlow].[GetDepartments];
go

create procedure [PatientFlow].[GetDepartments] 
as
begin
set nocount on;
set transaction isolation level read committed;	

select
	dept.DepartmentId,
	DepartmentName,
	dept.OrganisationId,
	org.OrganisationName,
	count(MemberId) as LinkCount,
	count(AlertId) as LinkedMessageCount
from PatientFlow.Department dept
join PatientFlow.Organisation org on dept.OrganisationId = org.OrganisationId
left join PatientFlow.Member memb on dept.DepartmentId = memb.DepartmentId
left join PatientFlow.AlertsLinkedToDepMem aldm on aldm.TypeId = dept.DepartmentId and LinkTypeId = 2
group by 
	dept.DepartmentId, 
	DepartmentName, 
	dept.OrganisationId, 
	OrganisationName

end
