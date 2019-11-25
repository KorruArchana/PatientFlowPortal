if object_id ('[PatientFlow].[GetModuleDetails]') is not null
drop procedure [PatientFlow].[GetModuleDetails];
go

create procedure [PatientFlow].[GetModuleDetails]  
@LanguageCode varchar(50)
as
begin
set nocount on;
set transaction isolation level read committed;
declare @languageid int;
set @languageid=(
					select 
						languageid 
					from PatientFlow.Language 
					where UPPER(LanguageCode)=UPPER(@LanguageCode)
				)
if not exists (select [moduleid], 
                      [modulename], 
                      t.translationtext, 
                      isnull(M.translationrefid, 0) as TranslationRefId 
               from   [PatientFlow].[module] as M 
                      left join [PatientFlow].[translation] as T 
                             on M.translationrefid = T.translationrefid 
               where  T.languageid = @languageid) 
  begin 
      select distinct [moduleid], 
                      [modulename], 
                      ''                            as TranslationText, 
                      isnull(M.translationrefid, 0) as TranslationRefId 
      from   [PatientFlow].[module] as M 
             left join [PatientFlow].[translation] as T 
                    on M.translationrefid = T.translationrefid 
  end 
else 
  begin 
      select [moduleid], 
             [modulename], 
             t.translationtext, 
             isnull(M.translationrefid, 0) as TranslationRefId 
      from   [PatientFlow].[module] as M 
             left join [PatientFlow].[translation] as T 
                    on M.translationrefid = T.translationrefid 
      where  T.languageid = @languageid 
  end 

end

