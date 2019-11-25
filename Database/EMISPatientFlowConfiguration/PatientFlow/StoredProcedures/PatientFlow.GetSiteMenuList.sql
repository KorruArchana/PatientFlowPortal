if object_id ('[PatientFlow].[GetSiteMenuList]') is not null
	drop procedure [PatientFlow].[GetSiteMenuList];
go

create procedure [PatientFlow].[GetSiteMenuList]
	@ParentMenuId	int
as
begin	
	set nocount on;
	set transaction isolation level read committed;
    select 
		SiteMenu.MenuId,
		SiteMenu.MenuName,
		SiteMenu.NavigationUrl,
		SiteMenu.ParentMenuId,
		SiteMenu.NodeTypeId,
		SiteMenu.NodeId
	from PatientFlow.SiteMenu as SiteMenu
	where SiteMenu.ParentMenuId=@ParentMenuId;	
end




