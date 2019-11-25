if object_id ('[PatientFlow].[GetSelectedDepartmentMemberTreeList]') is not null
drop procedure [PatientFlow].[GetSelectedDepartmentMemberTreeList];
go

create procedure [PatientFlow].[GetSelectedDepartmentMemberTreeList]
@OrgIdList as [PatientFlow].[List] readonly,
@KioskId int
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
	where NodeTypeId =23 and Organisation.OrganisationId in (select Id from @OrgIdList);
	
select 
	sitemenu.MenuId,
	TempOrg.MenuId as ParentMenuId,
	sitemenu.NodeId,
	sitemenu.NodeTypeId,
	Temporg.MenuName +'/'+DepartmentName as MenuName,
	case when KioskLinkDetails.KioskDepMemLinkId > 0 
        then 'true'
	else 'false' end as Selected
        into #TempDepList 
	from PatientFlow.Sitemenu Sitemenu
	left join  PatientFlow.Department Department
	on Sitemenu.NodeId =Department.DepartmentId and NodeTypeId=31
	inner join #TempOrgList as TempOrg
	on TempOrg.NodeId=Department.OrganisationId
	left join  PatientFlow.Organisation Organisation
	on Department.OrganisationId =Organisation.OrganisationId
	left join [PatientFlow].[KioskLinkedToDepMemDetails] KioskLinkDetails 
	on SiteMenu.NodeId=KioskLinkDetails.TypeId and LinkTypeId=2 and KioskId=@KioskId
	where sitemenu.NodeTypeId =31 and Organisation.OrganisationId in (select Id from @OrgIdList);
	
select Sitemenu.MenuId,
       Sitemenu.ParentMenuId,
       Sitemenu.NodeId,
       Sitemenu.NodeTypeId,
       temp.MenuName + '/' + Sitemenu.MenuName as MenuName,
       case when temp.selected = 'true' then 'true' else 'false' end as Selected
into   #TempMemFullList
from   #TempDepList as temp
       inner join
       PatientFlow.Sitemenu as Sitemenu
       on temp.MenuId = SiteMenu.ParentMenuId
select 
	temp1.MenuId, 
	temp1.ParentMenuId,  
	temp1.NodeId, 
	temp1.NodeTypeId, 
	temp1.MenuName, 
	case when (temp1.Selected='true') 
	then 'true'
	when (temp1.Selected='false' and KioskLinkDetails.KioskDepMemLinkId >0) 
	then 'true'
	else 'false' end as Selected
	into #TempMemSelList
        from #TempMemFullList temp1 
        left join [PatientFlow].[KioskLinkedToDepMemDetails] KioskLinkDetails 
        on temp1.NodeId=KioskLinkDetails.TypeId and LinkTypeId=3 and NodeTypeId=35 and KioskId=@KioskId
select MenuName from #TempOrgList where Selected='true'  and MenuId not in (select ParentMenuId from #TempDepList where Selected='true')  and 
MenuId not in (select ParentMenuId from #TempDepList where MenuId  in (select ParentMenuId from #TempMemSelList where Selected='true')) 
union 
select MenuName from #TempDepList where Selected='true' --and MenuId  in (select ParentMenuId from #TempMemSelList where Selected='true')
union 
select MenuName from #TempMemSelList where Selected='true' and ParentMenuId not in (select MenuId from #TempDepList where Selected='true')
order by MenuName
drop table #TempOrgList
drop table #TempDepList
drop table #TempMemFullList
drop table #TempMemSelList
end