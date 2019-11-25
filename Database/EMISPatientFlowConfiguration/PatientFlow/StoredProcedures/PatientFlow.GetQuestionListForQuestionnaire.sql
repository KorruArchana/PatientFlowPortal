if object_id ('[PatientFlow].[GetQuestionListForQuestionnaire]') is not null
	drop procedure [PatientFlow].[GetQuestionListForQuestionnaire];
go

create procedure [PatientFlow].[GetQuestionListForQuestionnaire]
	@QuestionnaireId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select 
		QuestionId, 
		QuestionnaireId, 
		QuestionText, 
		AgeCriteria, 
		QuestionType, 
		Age1, 
		Age2, 
		Operation,
		Gender
	from [PatientFlow].Question 
	where QuestionnaireId = @QuestionnaireId 
	order by [QuestionOrder]; 
end