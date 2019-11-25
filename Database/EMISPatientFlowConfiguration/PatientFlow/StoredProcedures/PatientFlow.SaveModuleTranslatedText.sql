if object_id('[PatientFlow].[SaveModuleTranslatedText]') is not null
drop procedure [PatientFlow].[SaveModuleTranslatedText]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [PatientFlow].[SaveModuleTranslatedText]
  @ModuleTranslations [PatientFlow].[ModuleTranslations] readonly
as
begin
	set nocount on;
	set transaction isolation level read committed
	declare @pmoduleid int
	declare @planguagecode varchar(1000)
	declare @ptranslatedtext nvarchar(1000)
	declare @ptranslationrefid int
	declare @pmodifiedby varchar(50)
	declare @rowcount int
	declare @SelectedRow int
    begin try
	begin transaction
	select 
		Row_Number() over (order by Id) as RowID,
		a.*
	into #TmpModuleTranslations
	from @ModuleTranslations a
	set @rowcount = (SELECT  count(*) FROM @ModuleTranslations)
	set @selectedrow = 0  
	while @selectedrow <= @rowcount
    begin 
            set @selectedrow = @selectedrow + 1
			select 
					@pmoduleid=Id ,
					@planguagecode=LanguageCode,
					@ptranslatedtext=TranslatedText,
					@ptranslationrefid=TranslationRefId ,
					@pmodifiedby=ModifiedBy						
		   from  #TmpModuleTranslations as Tmp where  Tmp.RowID=@SelectedRow
    
	declare @languageid int;
    set @languageid = (select languageid from PatientFlow.[Language] where upper(LanguageCode)=upper(@planguagecode))
  	if not exists ( select  1 from    PatientFlow.Module where (TranslationRefId=@ptranslationrefid))
	begin 
	insert into PatientFlow.TranslationRef 
	([TranslationTypeId])
	 values  
	(1)
	declare @TranslationRefId int
   	set @TranslationRefId= (select cast(scope_identity() as int))
	update PatientFlow.Module
	set [TranslationRefId] =@TranslationRefId
	where [ModuleId]=@pmoduleid
	insert into PatientFlow.Translation
	(

	[LanguageId],
	[TranslationRefId],
	[TranslationText],
	[ModifiedBy],
	[Modified]
	
	)
	values  
	(

	@languageId,
	@TranslationRefId,
	@ptranslatedtext,
	@pmodifiedby,
	GETDATE()
	
	)
	end
	else
	begin	
	if not exists ( select  1 from PatientFlow.Translation where [TranslationRefId]=@ptranslationrefid and [LanguageId]=@languageId)
	begin
	insert into PatientFlow.Translation
	(

	[LanguageId],
	[TranslationRefId],
	[TranslationText],
	[ModifiedBy],
	[Modified]
	
	)
	values 
	 (

	 @languageId,
	 @ptranslationrefid,
	 @ptranslatedtext,
	 @pmodifiedby,
	 getdate()
	 
	 )
	end
	else
	begin
			update	PatientFlow.Translation
			set 
			[TranslationText]=@ptranslatedtext,
			[ModifiedBy]=@pmodifiedby,
			[Modified]=getdate()
			where 
			[TranslationRefId]=@ptranslationrefid and [LanguageId]=@languageId
	end

	end
	end
	Commit Transaction
	select 1      
	end try
	begin catch
	declare @Error int, @Message varchar(4000);		
	select 
		@Error = error_number(), 
		@Message = error_message();
	if xact_state() <> 0 begin
		rollback transaction;
	end
	raiserror('SaveModuleTranslatedText : %d: %s', 16, 1, @Error, @Message);    
	rollback transaction
	end catch
end