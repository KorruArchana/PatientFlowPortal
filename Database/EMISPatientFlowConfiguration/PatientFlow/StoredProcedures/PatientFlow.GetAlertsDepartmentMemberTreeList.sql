if object_id ('[PatientFlow].[GetAlertsDepartmentMemberTreeList]') is not null
	drop procedure [PatientFlow].[GetAlertsDepartmentMemberTreeList];
go

create procedure [PatientFlow].[GetAlertsDepartmentMemberTreeList]
	@AlertId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 	distinct
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
	where NodeTypeId =23 and AlertLinkOrg.AlertId=@AlertId
select 
	sitemenu.MenuId,
	TempOrg.MenuId as ParentMenuId,
	Department.DepartmentId as NodeId,
	sitemenu.NodeTypeId,
	OrganisationName + ' - '+ DepartmentName as MenuName,
	case 
		when AlertLinkDetails.AlertDepMemLinkId > 0 
		then 'true'
		else 'false' 
		end as Selected
into #TempDepartments
from 
PatientFlow.AlertLinkToOrganisation AlertLinkOrg
inner join  PatientFlow.Organisation Organisation
on AlertLinkOrg.OrganisationId =Organisation.OrganisationId
left join  PatientFlow.Department Department
on Organisation.OrganisationId =Department.OrganisationId 
inner join #TempOrgList as TempOrg
on TempOrg.NodeId=Department.OrganisationId
left join [PatientFlow].[AlertsLinkedToDepMem]AlertLinkDetails on
Department.DepartmentId=AlertLinkDetails.TypeId and LinkTypeId=2 and AlertLinkDetails.AlertId=@AlertId
inner join PatientFlow.Sitemenu Sitemenu
on Department.DepartmentId=Sitemenu.NodeId and sitemenu.NodeTypeId=31
where AlertLinkOrg.AlertId=@AlertId

select 
	Sitemenu.MenuId, 
	Sitemenu.ParentMenuId,  
	Sitemenu.NodeId, 
	Sitemenu.NodeTypeId, 
	Sitemenu.MenuName, 
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
on temp1.NodeId=AlertLinkDetails.TypeId and LinkTypeId=3 and NodeTypeId=35 and AlertId=@AlertId
select Row_Number() over ( order by MenuId) as RowID,
       a.*
into   #TempOrgFullList
from   #TempOrgList as a 
declare @MenuId int
declare @rowcount int
set @rowcount = (select Count(*) from #TempOrgFullList)
declare @SelectedRow int
set @selectedrow = 0 
while @selectedrow <= @rowcount
begin

    set @MenuId = (select top 1 MenuId
                       from #TempOrgFullList as Tmp
                       where  Tmp.RowID=@SelectedRow)
   
    if(not exists (select top 1.* from #TempDepartments where ParentMenuId = @MenuId and NodeTypeId=31 and  Selected='true')) and 
	(not exists(select top 1.* from #TempMemSelected where ParentMenuId in (select MenuId from #TempDepartments where ParentMenuId = @MenuId and NodeTypeId=31)  and Selected='true'))
	begin
	update  #TempDepartments
	set Selected='true' where  ParentMenuId=@MenuId
	update  #TempMemSelected
	set Selected='true' where  ParentMenuId in (select MenuId from #TempDepartments where ParentMenuId = @MenuId and NodeTypeId=31)	
	end
	 set @selectedrow = @selectedrow + 1
end
select * from #TemporgList
union
select * from #TempDepartments
union
select * from #TempMemSelected

drop table #TempOrgList
drop table #TempDepartments
drop table #TempMembers
drop table #TempMemSelected
drop table #TempOrgFullList
end
