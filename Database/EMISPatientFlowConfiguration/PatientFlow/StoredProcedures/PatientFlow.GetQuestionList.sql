if object_id ('[PatientFlow].[GetQuestionList]') is not null
	drop procedure [PatientFlow].[GetQuestionList];
go

create procedure [PatientFlow].[GetQuestionList]
as
begin
	set nocount on;
	set transaction isolation level read committed;
    select 
		QuestionId,
		QuestionnaireId,
		QuestionText,
		QuestionOrder,
		AgeCriteria,
		QuestionType,
		OptionCharLimit,
		Gender,
		Age1,
		Age2,
		Operation
	from [PatientFlow].Question 
end



