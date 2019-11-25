if object_id ('[PatientFlow].[GetQuestionById]') is not null
	drop procedure [PatientFlow].[GetQuestionById];
go

create procedure [PatientFlow].[GetQuestionById]
	@QuestionId int
as
begin
    set nocount on;
	set transaction isolation level read committed;
	select  
		QuestionId, 
		QuestionnaireId, 
		QuestionText, 
		QuestionType, 
		isnull(Gender, 'None') as Gender,
		isnull(QuestionOrder, 0) as QuestionOrder, 
		ModifiedBy, 
		Modified, 
		isnull(Age1, 0) as Age1,
		isnull(Age2, 0) as Age2,
		isnull(Operation, 'None') as Operation,
		isnull(OptionCharLimit, 200) as OptionCharLimit
	from [PatientFlow].[Question]
	where QuestionId = @QuestionId;
end

