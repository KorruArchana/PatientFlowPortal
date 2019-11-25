declare @temp table 
				(
					Id int primary key identity(1,1),
					MenuId int,
					OrganisationId int
				)
			insert into @temp 
				(
					MenuId,
					OrganisationId
				)
  (
	select 
		MenuId,
		NodeId 
	from [patientFlow].[SiteMenu] 
	where NodeTypeId=23 and ParentMenuId=1
  )

  declare @RowNum int
select @RowNum = Count(*) from @temp

while @RowNum > 0
				 begin 

						declare @ParentMenuId int
						set @ParentMenuId = (select 
												MenuId 
											from @temp 
											where Id=@RowNum )

						declare @OrganisationId int
						set @OrganisationId = ( select
													OrganisationId
												from @temp
												where Id=@RowNum
											  )

					if not exists (select top 1 * from [PatientFlow].[SiteMenu] where NodeTypeId=7 and NodeId=@OrganisationId)
					  insert into [PatientFlow].[SiteMenu] (MenuName, ParentMenuId,NodeTypeId,NodeId)
						values('Questionnaire',@ParentMenuId,7, @OrganisationId)
			
					set @RowNum = @RowNum - 1 
				end
