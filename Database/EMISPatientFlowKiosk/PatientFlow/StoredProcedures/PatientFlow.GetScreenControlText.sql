if object_id ('[PatientFlow].[GetScreenControlText]') is not null
	drop procedure [PatientFlow].[GetScreenControlText] ;
go

create procedure [PatientFlow].[GetScreenControlText] 
	@ScreenCode varchar(20),
	@ControlUniqueId varchar(50),
	@LanguageId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select	
		(case when TT.TranslationText is null then SC.ControlLabel else TT.TranslationText end) as ControlLabel  
	from [PatientFlow].[Language] LAN
	left join( 
		select 
			T.LanguageId, 
			T.TranslationText 
		from [PatientFlow].[KioskScreenControl] KSC 
		inner join [PatientFlow].[KioskScreen] KS 
		on KSC.ScreenId = KS.ScreenId and KS.ScreenCode = @ScreenCode
		inner join [PatientFlow].[Translation] T on KSC.TranslationRefId = T.TranslationRefId 
		where  KSC.UniqueId = @ControlUniqueId and T.LanguageId = @LanguageId  ) TT
		on LAN.LanguageId = TT.LanguageId
	cross join (
				select 
					ControlLabel  
				from  [PatientFlow].[KioskScreenControl] 
				where UniqueId = @ControlUniqueId
				) SC
	where LAN.LanguageId = @LanguageId;
end
