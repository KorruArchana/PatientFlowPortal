if object_id ('[PatientFlow].GetAnswerOptionsByQuestion') is not null
	drop procedure [PatientFlow].GetAnswerOptionsByQuestion;
go

create procedure [PatientFlow].GetAnswerOptionsByQuestion
	@QuestionId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
		select 
			OptionId, 
			OptionValue, 
			NestedQuestionId,
			OptionCode,
			SnomedCode
		from
		( 
		select 
			OptionId, 
			OptionValue, 
			NestedQuestionId,
			OptionCode,
			SnomedCode,
			ROW_NUMBER() over(PARTITION BY  OptionValue,OptionCode order by OptionId desc) as rownum
		from [PatientFlow].[QuestionAnswerOptions] 
		where QuestionId = @QuestionId 
		)t
	where t.rownum = 1 order by OptionId	
end