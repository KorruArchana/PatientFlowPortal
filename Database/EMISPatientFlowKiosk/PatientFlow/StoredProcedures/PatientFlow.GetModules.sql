if object_id ('[PatientFlow].[GetModules]') is not null
	drop procedure [PatientFlow].[GetModules];
go

create procedure [PatientFlow].[GetModules]
   @Module PatientFlow.KioskModule readonly,
   @LanguageId int,
   @ModuleId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select	
	MO.ModuleName,	
	(case when TT.TranslationText is null then MO.ModuleNameToDisplay else TT.TranslationText end) as ModuleNameToDisplay  
	from [PatientFlow].[Language] LAN
	left join( 
		select 
			T.LanguageId, 
			T.TranslationText 
		from @Module M
		inner join [PatientFlow].[Translation] T on M.TranslationRefId = T.TranslationRefId 
		where  T.LanguageId = @LanguageId and M.ModuleId=@ModuleId ) TT
		on LAN.LanguageId = TT.LanguageId
	cross join (
				select 
					ModuleName,  
					ModuleNameToDisplay
				from  @Module 
				where ModuleId = @ModuleId
				) MO
	where LAN.LanguageId = @LanguageId;
end