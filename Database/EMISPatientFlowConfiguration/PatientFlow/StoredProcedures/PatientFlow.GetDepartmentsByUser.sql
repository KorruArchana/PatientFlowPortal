if object_id('[PatientFlow].[GetDepartmentsByUser]') is not null
drop procedure [PatientFlow].[GetDepartmentsByUser]
go
create procedure [PatientFlow].[GetDepartmentsByUser]
	@User varchar(128)
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
join PatientFlow.OrganisationAccessMapping mapping on Org.OrganisationId = mapping.OrganisationId	
where mapping.UserName = @User
group by 
	dept.DepartmentId, 
	DepartmentName, 
	dept.OrganisationId, 
	OrganisationName

end