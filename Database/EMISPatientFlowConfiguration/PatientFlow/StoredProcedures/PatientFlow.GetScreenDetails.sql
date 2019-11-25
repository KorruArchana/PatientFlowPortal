if object_id ('[PatientFlow].[GetScreenDetails]') is not null
	drop procedure [PatientFlow].[GetScreenDetails];
go

create procedure [PatientFlow].[GetScreenDetails]
@ModuleId int,
@LanguageCode varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;
	declare @languageid int;
	set @languageid= (select languageid from PatientFlow.Language 
		where UPPER(LanguageCode)=UPPER(@LanguageCode))

    if not exists(
		select 
	      KS.ControlId as ModuleId,
		  isnull(KS.TranslationRefId ,0) as TranslationRefId,		  
		  ControlLabel as ModuleName,
		  t.TranslationText
		from [PatientFlow].[KioskScreen] as K   
		  inner join [PatientFlow].[KioskScreenControl] as KS  
		  on k.ScreenId=ks.ScreenId 
		  left join [PatientFlow].[Translation] as T 
		  on KS.TranslationRefId =T.TranslationRefId
          where T.LanguageId=@languageid and  
			k.ScreenId=@ModuleId)
		  begin 
			select distinct 
				KS.ControlId as ModuleId,
				isnull(KS.TranslationRefId, 0) as TranslationRefId,
				ControlLabel as ModuleName,
				'' as TranslationText
			from   [PatientFlow].[KioskScreen] as K
				   inner join
				   [PatientFlow].[KioskScreenControl] as KS
				   on k.ScreenId = ks.ScreenId
				   left outer join
				   [PatientFlow].[Translation] as T
				   on KS.TranslationRefId = T.TranslationRefId
			where  k.ScreenId = @ModuleId
		  end
	else
		begin
		select 
			KS.ControlId as ModuleId,
			isnull(KS.TranslationRefId ,0) as TranslationRefId,		  
			ControlLabel as ModuleName,
			t.TranslationText
			from [PatientFlow].[KioskScreen] as K   
			inner join [PatientFlow].[KioskScreenControl] as KS  
			on k.ScreenId=ks.ScreenId 
			left join [PatientFlow].[Translation] as T 
			on KS.TranslationRefId = T.TranslationRefId
			where T.LanguageId=@languageid and  
				k.ScreenId=@ModuleId
		end

 
 	
end
