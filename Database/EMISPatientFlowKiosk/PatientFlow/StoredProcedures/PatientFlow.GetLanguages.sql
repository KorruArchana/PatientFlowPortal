if object_id ('[PatientFlow].[GetLanguages]') is not null
	drop procedure [PatientFlow].[GetLanguages];
go

create procedure [PatientFlow].[GetLanguages]
   @KioskLanguageOrder PatientFlow.KioskLanguageOrder readonly 
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select  
		L.LanguageId,
		L.LanguageName,
		L.LanguageCode,
		(case when T.TranslationRefId is null then L.LanguageName else T.TranslationText end) as LanguageDisplayName 
	from [PatientFlow].[Language] L 
	inner join @KioskLanguageOrder LO on L.LanguageId = LO.LanguageId
	left join [PatientFlow].[Translation] T on L.TranslationRefId = T.TranslationRefId;
end