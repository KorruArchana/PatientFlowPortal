if object_id ('[PatientFlow].[GetSurveyQuestionnaire]') is not null
	drop procedure [PatientFlow].[GetSurveyQuestionnaire];
go

create procedure [PatientFlow].[GetSurveyQuestionnaire]	
@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;	
	
	select 
		QuestionnaireId,
		Title,
		Frequency,
		CreateConsultation,
		IsAnonymous
	from [PatientFlow].Questionnaire as Questionnaire
	where IsAnonymous='true' and OrganisationId=@OrganisationId;
		
end





