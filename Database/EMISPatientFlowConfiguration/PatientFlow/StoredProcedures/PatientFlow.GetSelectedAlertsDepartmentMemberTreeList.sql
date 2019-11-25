if object_id ('[PatientFlow].[GetSelectedAlertsDepartmentMemberTreeList]') is not null
	drop procedure [PatientFlow].[GetSelectedAlertsDepartmentMemberTreeList];
go

create procedure [PatientFlow].[GetSelectedAlertsDepartmentMemberTreeList]
	@AlertId int
as
begin
set nocount on;
set transaction isolation level read committed;

select distinct 
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
	inner join PatientFlow.AlertLinkToOrganisation AlertLinkOrg
	on AlertLinkOrg.OrganisationId =Organisation.OrganisationId
	where NodeTypeId =23 and AlertLinkOrg.AlertId=@AlertId;
	
select distinct 
	Sitemenu.MenuId as MenuId ,
	TempOrg.MenuId as ParentMenuId,
	Department.DepartmentId as NodeId,
	Sitemenu.NodeTypeId,
	Temporg.MenuName +'/'+DepartmentName as MenuName,
	case when AlertLinkDetails.AlertDepMemLinkId > 0 
	then 'true' 
	else 'false' end as Selected
into #TempDepartments
from PatientFlow.Sitemenu Sitemenu
left join PatientFlow.Department Department
on Department.DepartmentId=Sitemenu.NodeId and Sitemenu.NodeTypeId=31
inner join #TempOrgList as TempOrg
on TempOrg.NodeId=Department.OrganisationId
left join [PatientFlow].[AlertsLinkedToDepMem]AlertLinkDetails 
on Department.DepartmentId=AlertLinkDetails.TypeId and LinkTypeId=2 and AlertLinkDetails.AlertId=@AlertId

select 
	Sitemenu.MenuId, 
	Sitemenu.ParentMenuId,  
	Sitemenu.NodeId, 
	Sitemenu.NodeTypeId, 
	temp.MenuName+'/'+Sitemenu.MenuName as MenuName,
	case 
		when temp.selected ='true' 
		then 'true'
		else 'false' 
		end as Selected
into #TempMembers
from #TempDepartments temp 
inner join PatientFlow.Sitemenu Sitemenu 
on temp.MenuId=SiteMenu.ParentMenuId

select 
	temp1.MenuId, 
	temp1.ParentMenuId,  
	temp1.NodeId, 
	temp1.NodeTypeId, 
	temp1.MenuName, 
	case 
		when (temp1.Selected='true') 
		then 'true'
		when (temp1.Selected='false' and AlertLinkDetails.AlertDepMemLinkId >0)  
		then 'true'
		else 'false' 
		end as Selected
into #TempMemSelected
from #TempMembers temp1 
left join [PatientFlow].[AlertsLinkedToDepMem] AlertLinkDetails 
on temp1.NodeId=AlertLinkDetails.TypeId 
and LinkTypeId=3 and NodeTypeId=35 and AlertId=@AlertId

select MenuName from #TempOrgList 
where Selected='true'  
and MenuId not in (select ParentMenuId from #TempDepartments where Selected='true') 
and MenuId not in 
(
	select ParentMenuId from #TempDepartments 
	where MenuId  in (select ParentMenuId from #TempMemSelected where Selected='true')
) 
union 
select MenuName from #TempDepartments where Selected='true'
union 
select MenuName from #TempMemSelected 
where Selected='true' 
and ParentMenuId not in (select MenuId from #TempDepartments where Selected='true')
order by MenuName

drop table #TempOrgList
drop table #TempDepartments
drop table #TempMembers
drop table #TempMemSelected

end
