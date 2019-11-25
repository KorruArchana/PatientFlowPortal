if object_id ('[PatientFlow].[GetQuestionsByQuestionnaire]') is not null
	drop procedure [PatientFlow].[GetQuestionsByQuestionnaire];
go

create procedure [PatientFlow].[GetQuestionsByQuestionnaire]
	 @QuestionnaireId as int,
	 @QuestionOrder as int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	if (@QuestionOrder > 0)
	begin
			declare @CurrentQnCount int, @MaxOrder int

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
			into #TotalQnList
			from Question
			where (QuestionnaireId = @QuestionnaireId)
			order by QuestionOrder;

			select @MaxOrder = max(QuestionOrder) from #TotalQnList;

			select  
				QuestionId, 
				QuestionnaireId, 
				QuestionText, 
				QuestionType, 
				isnull(Gender,'None') as Gender,
				isnull(QuestionOrder, 0) as QuestionOrder, 
				ModifiedBy, 
				Modified, 
				isnull(Age1, 0) as Age1,
				isnull(Age2, 0) as Age2,
				isnull(Operation, 'None') as Operation,
				isnull(OptionCharLimit, 200) as OptionCharLimit
			into #Temp
			from #TotalQnList
			where (QuestionnaireId = @QuestionnaireId) and 
				  (isnull(QuestionOrder, 0) = @QuestionOrder);
		
			select @CurrentQnCount = count(*) from #Temp;
			 
			if (@CurrentQnCount > 0)
				select * from #Temp;
			else if (@QuestionOrder < = @MaxOrder)
			begin
				set @QuestionOrder=@QuestionOrder + 1;
				exec PatientFlow.GetQuestionsByQuestionnaire @QuestionnaireId, @QuestionOrder;
			end

			drop table #Temp;
			drop table #TotalQnList;

	end
	else
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
			from Question
			where (QuestionnaireId = @QuestionnaireId)
			order by QuestionOrder;
end
	