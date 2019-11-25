if object_id ('[PatientFlow].[GetTranslations]') is not null
	drop procedure [PatientFlow].[GetTranslations];
go

create procedure [PatientFlow].[GetTranslations]
	@Pagesize int, 
	@SyncedTranslationDate datetime,
	@LastRowModifiedDate datetime output
as
begin
	set nocount on;
	set transaction isolation level read committed 
	
    select @LastRowModifiedDate = max(Modified) from
        (
			select  
			    row_number() over( Order by Modified asc) as RowNo,		
				Modified
			from PatientFlow.Translation 
			where (Modified >= isnull(@SyncedTranslationDate,'01/01/1900'))
        ) 
		as TBL
		where TBL.RowNo between  1 and  @PageSize;
    
    select 
		RowNo,  
		LanguageId, 
		TranslationText, 
		TranslationRefId
    from
        (
			select   
				row_number() over( Order by Modified asc) as RowNo,  
				LanguageId, 
				TranslationText, 
				TranslationRefId
			from PatientFlow.Translation 
			where (Modified >= isnull(@SyncedTranslationDate,'01/01/1900'))
        ) 
		as TBL
		where TBL.RowNo between  1 and  @PageSize;
end
