if object_id ('[PatientFlow].[GetDepartmentMemberTreeListByOrgs]') is not null
drop procedure [PatientFlow].[GetDepartmentMemberTreeListByOrgs];
go

create procedure [PatientFlow].[GetDepartmentMemberTreeListByOrgs]
 @OrgIdList as [PatientFlow].[List] readonly
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	MenuId,
	null as ParentMenuId,
	NodeId,
	NodeTypeId,
	OrganisationName  as MenuName,
	'true' as Selected
	into #TempOrgList 
	from PatientFlow.Sitemenu Sitemenu
	inner join  PatientFlow.Organisation Organisation
	on Sitemenu.NodeId =Organisation.OrganisationId 
	where NodeTypeId =23 and Organisation.OrganisationId in (select Id from @OrgIdList)
select 
	Sitemenu.MenuId,
	TempOrg.MenuId as ParentMenuId,
	Sitemenu.NodeId,
	Sitemenu.NodeTypeId,
	DepartmentName as MenuName,
	'true' as Selected
    into #TempDepList 
	from PatientFlow.Sitemenu Sitemenu
	left join  PatientFlow.Department Department
	on Sitemenu.NodeId =Department.DepartmentId and NodeTypeId=31
	inner join #TempOrgList as TempOrg
	on TempOrg.NodeId=Department.OrganisationId
	left join  PatientFlow.Organisation Organisation
	on Department.OrganisationId =Organisation.OrganisationId
	where Sitemenu.NodeTypeId =31 and Organisation.OrganisationId in (select Id from @OrgIdList)

select 
	Sitemenu.MenuId, 
	Sitemenu.ParentMenuId,  
	Sitemenu.NodeId, 
	Sitemenu.NodeTypeId, 
	Sitemenu.MenuName, 
	'true' as selected
	 into #TempMemFullList
from #TempDepList temp inner join
PatientFlow.Sitemenu Sitemenu on temp.MenuId=SiteMenu.ParentMenuId 

select 
	temp1.MenuId, 
	temp1.ParentMenuId,  
	temp1.NodeId, 
	temp1.NodeTypeId, 
	temp1.MenuName, 
	'true' as Selected
	into #TempMemSelList
from #TempMemFullList temp1 left join 
[PatientFlow].[KioskLinkedToDepMemDetails]KioskLinkDetails 
on temp1.NodeId=KioskLinkDetails.TypeId and LinkTypeId=3 and NodeTypeId=35 
select * from #TempOrgList
union
select * from #TempDepList
union
select * from #TempMemSelList

drop table #TempOrgList
drop table #TempDepList
drop table #TempMemFullList
drop table #TempMemSelList
end

