if object_id('[PatientFlow].[SaveScreenControlTranslatedText]') is not null
drop procedure [PatientFlow].[SaveScreenControlTranslatedText]
go

create procedure [PatientFlow].[SaveScreenControlTranslatedText]
     @ModuleTranslations [PatientFlow].[ModuleTranslations] readonly
as
begin
	
	set nocount on;
	declare @pmoduleid int
	declare @planguagecode varchar(1000)
	declare @ptranslatedtext nvarchar(1000)
	declare @ptranslationrefid int 
	declare @pmodifiedby varchar(50)
	declare @rowcount int
	declare @SelectedRow int
	set transaction isolation level read committed
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
			@pModifiedBy=ModifiedBy						
	from  #TmpModuleTranslations as Tmp where  Tmp.RowID=@SelectedRow

	declare @languageid int;
    set @languageid = (select LanguageId from PatientFlow.[Language] where upper(LanguageCode)=upper(@planguagecode))

  	if not exists ( select  1 from  PatientFlow.KioskScreenControl where (TranslationRefId=@ptranslationrefid))
	begin 

	insert into PatientFlow.TranslationRef 
	([TranslationTypeId])
	values  
	(3)
   	set @ptranslationrefid= (select cast(scope_identity() as int))

	update PatientFlow.KioskScreenControl
	set 
		[TranslationRefId] =@ptranslationrefid,
		ModifiedBy=@pModifiedBy,
		Modified=getdate()
	where [ControlId]=@pmoduleid

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
	@pModifiedBy,
	getdate()
	
	)
	end
	else
	begin
	if not exists ( select  1 from  PatientFlow.Translation where [TranslationRefId]=@ptranslationrefid and [LanguageId]=@languageId)
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
	@pModifiedBy,
	getdate()
	
	)
	end
	else
	begin
	update	PatientFlow.Translation
	set 
	[TranslationText]=@ptranslatedtext,
	[ModifiedBy]=@pModifiedBy,
	[Modified]=getdate()
	where 
	[TranslationRefId]=@ptranslationrefid and [LanguageId]=@languageId
	end
	end
   
	end
	commit transaction
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
	raiserror('SaveScreenControlTranslatedText : %d: %s', 16, 1, @Error, @Message);    
	rollback transaction
	end catch
END