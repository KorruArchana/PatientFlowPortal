if object_id ('[PatientFlow].[GetQuestionAnswerOptions]') is not null
	drop procedure [PatientFlow].[GetQuestionAnswerOptions];
go

create procedure [PatientFlow].[GetQuestionAnswerOptions]
	@QuestionId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
    select	
		OptionId,
		QuestionId,
		OptionValue,
		SnomedCode,
		NestedQuestionId,
		OptionCode
	from PatientFlow.QuestionAnswerOptions
    where QuestionId = @QuestionId
end


