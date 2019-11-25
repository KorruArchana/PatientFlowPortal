if object_id ('[PatientFlow].[GetScreenControls]') is not null
	drop procedure [PatientFlow].[GetScreenControls];
go

create procedure [PatientFlow].[GetScreenControls]
	@Pagesize int, 
	@LastSyncedDate datetime,
	@LastRowModifiedDate datetime output
as
begin
	set nocount on; 
	set transaction isolation level read committed;  
	
    select @LastRowModifiedDate = max(Modified) from
             (
				select   
					row_number() over( Order by ControlId asc) as RowNo, 
					Modified
				from PatientFlow.KioskScreenControl 
				where (Modified >= isnull(@LastSyncedDate,'01/01/1900'))
             ) 
			 as TBL
			 where TBL.RowNo between  1 and  @PageSize;   
			 
    select 
		RowNo,  
		ControlId, 
		ScreenId, 
		UniqueId,
		ControlLabel, 
		TranslationRefId 
	from
             (
				select   
					row_number() over( Order by ControlId asc) as RowNo,  
					ControlId, 
					ScreenId, 
					UniqueId,
					ControlLabel, 
					TranslationRefId
				from PatientFlow.KioskScreenControl 
				where  (Modified >= isnull(@LastSyncedDate,'01/01/1900'))
             ) 
			 as TBL
     where TBL.RowNo between  1 and  @PageSize;
end
