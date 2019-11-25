if object_id ('[PatientFlow].[GetLinkedKiosksForQuestionnaire]') is not null
	drop procedure [PatientFlow].[GetLinkedKiosksForQuestionnaire];
go

create procedure [PatientFlow].[GetLinkedKiosksForQuestionnaire]
	@QuestionaireId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select	
		ConnectionGuid,
		Kiosk.KioskId
	from	[PatientFlow].[Kiosk] Kiosk
			join [PatientFlow].[KioskQuestionnaire] KioskQuestionnaire 
			on Kiosk.KioskId = KioskQuestionnaire.KioskId
	where	KioskQuestionnaire.QuestionnaireId = @QuestionaireId
			and ConnectionGuid is not null
end
