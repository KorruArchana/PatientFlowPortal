if object_id('[PatientFlow].[GetSiteMenuByParentId]') is not null
	drop procedure [PatientFlow].[GetSiteMenuByParentId];
go

create procedure [PatientFlow].[GetSiteMenuByParentId] @ParentId int
as
begin
	set nocount on;
	set transaction isolation level read committed;

	with Tree (
		ID,
		[NAME],
		PARENT_ID,
		Depth,
		[Path]
		)
	as (
		select 
			MenuId,
			MenuName,
			ParentMenuId,
			0 as Depth,
			CONVERT(varchar(255), MenuName) as [Path]
		from [PatientFlow].[SiteMenu]
		where ParentMenuId = 1
			and NodeTypeId <> 43
		
		union all
		
		select CT.MenuId,
			CT.MenuName,
			CT.ParentMenuId,
			Parent.Depth + 1 as Depth,
			CONVERT(varchar(255), Parent.[Path] + '/' + CT.MenuName) as [Path]
		from [PatientFlow].[SiteMenu] CT
		inner join Tree as Parent
			on Parent.ID = CT.ParentMenuId
		where CT.NodeTypeId <> 43
		)
	select 
		ID,
		[NAME],
		PARENT_ID,
		Depth,
		[Path]
	from Tree
	where [Path] like (
			select top (1) MenuName + '%'
			from PatientFlow.SiteMenu as S
			where (NodeId = @ParentId)
			)
	order by PARENT_ID,
		ID
end
