if object_id ('[PatientFlow].[GetQuestionOptionList]') is not null
	drop procedure [PatientFlow].[GetQuestionOptionList];
go

create procedure [PatientFlow].[GetQuestionOptionList]
	
as
begin
	set nocount on;
        set transaction isolation level read committed;
	select	OptionId, 
			QuestionId,
			OptionValue,
			NestedQuestionId,
			OptionCode 
	from [PatientFlow].[QuestionAnswerOptions]
end
