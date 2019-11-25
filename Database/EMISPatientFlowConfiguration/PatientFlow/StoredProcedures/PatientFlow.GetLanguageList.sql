if object_id ('[PatientFlow].[GetLanguageList]') is not null
	drop procedure [PatientFlow].[GetLanguageList];
go

create procedure [PatientFlow].[GetLanguageList]
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select 
		[Language].LanguageId,
		[Language].LanguageCode,
		[Language].LanguageName
	from [PatientFlow].[Language] [Language]		
end

