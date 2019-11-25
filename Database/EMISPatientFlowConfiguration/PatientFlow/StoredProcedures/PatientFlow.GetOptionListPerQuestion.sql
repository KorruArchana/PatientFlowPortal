if object_id('[PatientFlow].[GetOptionListPerQuestion]') is not null
drop Procedure [PatientFlow].[GetOptionListPerQuestion]
go

create procedure [PatientFlow].[GetOptionListPerQuestion]
	@QuestionId int
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
	select 
		OptionId,
		QuestionId,
		OptionValue,
		NestedQuestionId,
		OptionCode,
		ModifiedBy,
		Modified 
	from [PatientFlow].[QuestionAnswerOptions]
	where QuestionId=@QuestionId
END

