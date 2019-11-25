if object_id ('[PatientFlow].[GetKioskLanguage]') is not null
drop procedure [PatientFlow].[GetKioskLanguage];
go
create procedure [PatientFlow].[GetKioskLanguage]
	@KioskId	int
as
begin

set nocount on;
set transaction isolation level read committed;
select
	[Language].LanguageId,
	[Language].LanguageCode,
	[Language].LanguageName,
	[Language].TranslationRefId
from [PatientFlow].[Language] [Language]
join [PatientFlow].KioskLanguage [KioskLanguage] 
on [Language].LanguageId=[KioskLanguage].LanguageId
where [KioskLanguage].KioskId=@KioskId
end
