if object_id ('[PatientFlow].[GetDepartmentDetails]') is not null
drop procedure [PatientFlow].[GetDepartmentDetails];
go
create procedure [PatientFlow].[GetDepartmentDetails]
	@DepartmentId int
as
begin
set nocount on;	
set transaction isolation level read committed
	select 
		dept.DepartmentId,
		DepartmentName,
		dept.OrganisationId,
		count(MemberId) as LinkCount,
		count(AlertId) as LinkedMessageCount
	from [PatientFlow].Department dept
	left join PatientFlow.Member memb on dept.DepartmentId = memb.DepartmentId
	left join PatientFlow.AlertsLinkedToDepMem aldm on aldm.TypeId = dept.DepartmentId and LinkTypeId = 2
	where dept.DepartmentId=@DepartmentId
	group by 
	dept.DepartmentId,
	DepartmentName,
	dept.OrganisationId

end
go
