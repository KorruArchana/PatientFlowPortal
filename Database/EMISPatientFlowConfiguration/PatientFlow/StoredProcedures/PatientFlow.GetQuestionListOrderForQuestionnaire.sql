if object_id ('[PatientFlow].[GetQuestionListOrderForQuestionnaire]') is not null
	drop procedure [PatientFlow].[GetQuestionListOrderForQuestionnaire];
go

create procedure [PatientFlow].[GetQuestionListOrderForQuestionnaire]
	@QuestionnaireId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select 
		Quest.QuestionId,
		QuestionText		
	from [PatientFlow].Question as Quest
	where Quest.QuestionnaireId = @QuestionnaireId 
	order by [QuestionOrder]
end
