if object_id ('[PatientFlow].[DeleteQuestionnaireIds]') is not null
	drop procedure [PatientFlow].[DeleteQuestionnaireIds];
go

create procedure PatientFlow.DeleteQuestionnaireIds
	@DeletedOrganisationId int
as
begin

	set nocount on;
	set transaction isolation level read committed;

	declare @List PatientFLow.List
	insert into @List (Id)
	(
		select QuestionnaireId from PatientFlow.Questionnaire where OrganisationId= @DeletedOrganisationId
	)
	delete from [PatientFlow].[KioskQuestionnaire] 
	where QuestionnaireId in (select Id from @List)

	delete QuestionAnswerOptions
	from [PatientFlow].[Question]as Question
	inner join [PatientFlow].[QuestionAnswerOptions] as QuestionAnswerOptions
	on Question.QuestionId=QuestionAnswerOptions.QuestionId
	where Question.QuestionnaireId in (select Id from @List)

	delete from  [PatientFlow].[Question] 
	where QuestionnaireId in (select Id from @List)

	delete from [PatientFlow].[Questionnaire] 
	where QuestionnaireId in (select Id from @List)

	delete from @List
end