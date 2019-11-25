if object_id ('[PatientFlow].[SaveTranslation]') is not null
	drop procedure [PatientFlow].[SaveTranslation];
go

create procedure [PatientFlow].[SaveTranslation]
	@LastRowModifiedDate datetime=null,
	@TranslationList PatientFlow.Translation readonly
as
begin	
	set nocount on;
	
	set transaction isolation level read committed;
	begin try
    
	begin transaction;  
   
    update [PatientFlow].[Translation]
	set
		TranslationText=translation.TranslationText
	from @TranslationList translation
	inner join [PatientFlow].[Translation] t
	on t.LanguageId=translation.LanguageId and t.TranslationRefId=translation.TranslationRefId;
		 
	 insert into [PatientFlow].[Translation]
	(
		LanguageId,
		TranslationRefId,
		TranslationText
	)
	select 
		translation.LanguageId,
		translation.TranslationRefId,
		translation.TranslationText
	from @TranslationList translation
	left outer join [PatientFlow].[Translation] t
	on t.LanguageId=translation.LanguageId and t.TranslationRefId=translation.TranslationRefId
	where t.LanguageId is null and t.TranslationRefId is null

	update [PatientFlow].[SynchronisationLog]
	set Modified=@LastRowModifiedDate
	where SyncType=3;

commit transaction;

end try
begin catch
		declare @Error int, @Message varchar(4000)		
		select 
			@Error = error_number(), 
			@Message = error_message()
		if xact_state() <> 0 begin
			rollback transaction;
		end
		raiserror('SaveTranslation : %d: %s', 16, 1, @Error, @Message);
end catch		
end

