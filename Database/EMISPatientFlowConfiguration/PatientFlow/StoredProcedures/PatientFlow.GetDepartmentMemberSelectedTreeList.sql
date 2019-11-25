if object_id ('[PatientFlow].[GetDepartmentMemberSelectedTreeList]') is not null
	drop procedure [PatientFlow].[GetDepartmentMemberSelectedTreeList];
go

create procedure [PatientFlow].[GetDepartmentMemberSelectedTreeList]
	 @OrgIdList varchar(300),
	 @KioskId int
as
begin
set nocount on;
set transaction isolation level read committed;
select  
	MenuId ,
	NULL as ParentMenuId,
	NodeId,
	NodeTypeId,
	OrganisationName + ' - '+ DepartmentName as MenuName,
	case when KioskLinkDetails.KioskDepMemLinkId > 0 
	then 'true'
	else 'false' 
	end as Selected
into #TempDepList 
from PatientFlow.Sitemenu Sitemenu
left join  PatientFlow.Department Department
on Sitemenu.NodeId =Department.DepartmentId and NodeTypeId=31
left join  PatientFlow.Organisation Organisation
on Department.OrganisationId =Organisation.OrganisationId
left join [PatientFlow].[KioskLinkedToDepMemDetails]KioskLinkDetails 
on SiteMenu.NodeId=KioskLinkDetails.TypeId and LinkTypeId=2 and KioskId=@KioskId
where NodeTypeId =31 and Organisation.OrganisationId in (select Ids  from [dbo].[fnSplitIds] (@OrgIdList, ','))

select 
	Sitemenu.MenuId, 
	Sitemenu.ParentMenuId,  
	Sitemenu.NodeId, 
	Sitemenu.NodeTypeId, 
	Sitemenu.MenuName, 
	Case when temp.selected ='true' 
	then 'true'
	else 'false' 
	end as Selected
into #tempMemFullList
from #TempDepList temp 
inner join PatientFlow.Sitemenu Sitemenu 
on temp.MenuId=SiteMenu.ParentMenuId 
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
	else 'false' 
	end as Selected
into #tempMemSelList
from #tempMemFullList temp1 left join [PatientFlow].[KioskLinkedToDepMemDetails] KioskLinkDetails 
on temp1.NodeId=KioskLinkDetails.TypeId and LinkTypeId=3 and NodeTypeId=35 and KioskId=@KioskId 
select * 
from #TempDepList
where selected='true'  
union
select * 
from  #TempDepList
where MenuId in (select ParentMenuId from #tempMemSelList) and ParentMenuId=Null
union
select * from #tempMemSelList
where selected='true'
drop table #TempDepList
drop table #tempMemFullList
drop table #tempMemSelList
End
