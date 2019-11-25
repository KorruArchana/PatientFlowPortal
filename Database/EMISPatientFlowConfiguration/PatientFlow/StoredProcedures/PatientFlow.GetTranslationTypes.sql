if object_id ('[PatientFlow].[GetTranslationTypes]') is not null
	drop procedure [PatientFlow].[GetTranslationTypes];
go

create procedure [PatientFlow].[GetTranslationTypes]
as
begin
	set nocount on;
	set transaction isolation level read committed;
    select 
		TranslationTypeId, 
		TranslationTypeName 
	from [PatientFlow].[TranslationType];
end

