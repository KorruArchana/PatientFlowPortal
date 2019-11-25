if object_id ('[PatientFlow].[GetQuestionnaireListForKiosk]') is not null
	drop procedure [PatientFlow].[GetQuestionnaireListForKiosk];
go

create procedure [PatientFlow].[GetQuestionnaireListForKiosk]
	@KioskId int,
	@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
    select 
		Questionnaire.QuestionnaireId,
		Questionnaire.Title,
		Questionnaire.Frequency,
		Questionnaire.IsAnonymous
	from [PatientFlow].Questionnaire as Questionnaire
		join [PatientFlow].KioskQuestionnaire as KioskQuestionnaire 
		on Questionnaire.QuestionnaireId = KioskQuestionnaire.QuestionnaireId
	where KioskQuestionnaire.KioskId = @KioskId
		  and Questionnaire.IsAnonymous = 'false'
		  and Questionnaire.OrganisationId=@OrganisationId
	order by KioskQuestionnaire.QuestionnaireOrder;
end



