if object_id ('[PatientFlow].[GetDepartmentListForOrganisation]') is not null
	drop procedure [PatientFlow].[GetDepartmentListForOrganisation];
go
create procedure [PatientFlow].[GetDepartmentListForOrganisation]
	@OrganisationId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	DepartmentId,
	DepartmentName,
	(select Count(ParentMenuId) from PatientFlow.SiteMenu  where ParentMenuId in (select menuID from PatientFlow.SiteMenu where nodeId=DepartmentId and NodeTypeId=31)) as LinkCount
from [PatientFlow].Department
where OrganisationId=@OrganisationId
order by DepartmentName
end

