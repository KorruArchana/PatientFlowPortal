if object_id('[PatientFlow].[SaveLanguageTranslatedText]') is not null
drop procedure [PatientFlow].[SaveLanguageTranslatedText]
go

create procedure [PatientFlow].[SaveLanguageTranslatedText]
	@moduleid int,
	@translationTypeId int,
	@languagecode varchar(50),
	@translatedText nvarchar(1000),
	@translationRefId int,
	@modifiedBy varchar(50)
as
begin

	set nocount on;
	set transaction isolation level read committed
    begin try
	begin transaction
	declare @languageId int;
    set @languageId = (select languageid from PatientFlow.[Language] where upper(LanguageCode)=upper(@languagecode))
  	if not exists ( select  1 from  PatientFlow.[Language] where (TranslationRefId=@translationRefId))
	begin 
				insert into PatientFlow.TranslationRef 
							   ([TranslationTypeId])
					   values  (@translationTypeId)
   				set @translationRefId= (select cast(scope_identity() as int))
				update PatientFlow.[Language]
				set [TranslationRefId] =@translationRefId
				where LanguageId=@moduleid

			    insert into PatientFlow.Translation
							  ([LanguageId],[TranslationRefId],[TranslationText],[ModifiedBy],[Modified])
					  values  (@languageId,@translationRefId,@translatedText,@modifiedBy,getdate())
	end
	else
	begin
		if not exists ( select  1 from  PatientFlow.Translation where [TranslationRefId]=@translationRefId and [LanguageId]=@languageId)
		begin
			        insert into PatientFlow.Translation
							  ([LanguageId],[TranslationRefId],[TranslationText],[ModifiedBy],[Modified])
					  values  (@languageId,@translationRefId,@translatedText,@modifiedBy,getdate())
		end
		else
		begin
				update	PatientFlow.Translation
				set 
				[TranslationText]=@translatedText,
				[ModifiedBy]=@modifiedBy,
				[Modified]=getdate()
				where 
				[TranslationRefId]=@translationRefId and [LanguageId]=@languageId
	   end
	end
	commit transaction
	select @translationRefId      
	end try

	begin catch
		declare @Error int, @Message varchar(4000)		
		select @Error = error_number(), @Message = error_message()
		if xact_state() <> 0 
			begin
				rollback transaction
			end
		raiserror('SaveLanguageTranslatedText : %d: %s', 16, 1, @error, @message); 
	end catch
end
