if object_id ('[PatientFlow].[GetModules]') is not null
	drop procedure [PatientFlow].[GetModules];
go

create procedure[PatientFlow].[GetModules]
	@PageNo int,
	@PageSize int,
	@TotalCount bigint output
as
begin
set nocount on;
set transaction isolation level read committed;

	select @TotalCount= Count(*) from
	(
		select  Row_number() over(order by ModuleName) as RowNo ,* FROM
		(
			select 
			   0 as ModuleId,
			  'Home Screen' as ModuleName,
			   1 as TranslationTypeId 
			union 
			select 
			  k.ScreenId as ModuleId,
			  K.ScreenName as ModuleName,
			  3 as TranslationTypeId		  
			from  [PatientFlow].[KioskScreen] as K    
		) as TBL
	) as OutTbl
	
	select 
		RowNo,
		ModuleId,
		ModuleName,
		TranslationTypeId
	from
	(
		select  Row_number() over(order by ModuleName) as RowNo ,* FROM
		(
			select 
				0 as ModuleId,
				'Home Screen' as ModuleName,
				1 as TranslationTypeId 
			union 
			select 
				k.ScreenId as ModuleId,
				K.ScreenName as ModuleName,
				3 as TranslationTypeId		  
			from [PatientFlow].[KioskScreen] as K  
	   ) as TBL
	) as OutTbl
	where  OutTbl.RowNo between ((@PageNo - 1) * @PageSize) + 1 and (@PageNo * @PageSize)
end