if object_id ('[PatientFlow].[GetQuestionDetails]') is not null
	drop procedure [PatientFlow].[GetQuestionDetails];
go

create procedure [PatientFlow].[GetQuestionDetails]
	@QuestionId	int
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
		OptionCharLimit,
		Age1,
		Age2,
		Gender,
		Operation
	from [PatientFlow].Question as Question
	where Question.QuestionId = @QuestionId;
end



