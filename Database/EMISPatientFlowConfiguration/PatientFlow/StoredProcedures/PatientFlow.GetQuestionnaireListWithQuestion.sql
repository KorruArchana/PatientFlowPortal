if object_id ('[PatientFlow].[GetQuestionnaireListWithQuestion]') is not null
	drop procedure [PatientFlow].[GetQuestionnaireListWithQuestion];
go

create procedure [PatientFlow].[GetQuestionnaireListWithQuestion]
@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select 
		Questionnaire.QuestionnaireId,
		Title,
		Frequency,
		CreateConsultation,
		IsAnonymous
	from [PatientFlow].Questionnaire as Questionnaire
	where	( 
				( select COUNT(*) 
				  from [PatientFlow].[Question] 
				  where QuestionnaireId = Questionnaire.QuestionnaireId
				) > 0 
			)
	and IsAnonymous = 'false'
	and Questionnaire.OrganisationId=@OrganisationId
end





