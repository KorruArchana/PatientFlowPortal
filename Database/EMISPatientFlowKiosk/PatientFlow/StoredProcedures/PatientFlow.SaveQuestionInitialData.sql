if object_id ('[PatientFlow].[SaveQuestioninitialData]') is not null
	drop procedure [PatientFlow].[SaveQuestioninitialData];
go

create procedure [PatientFlow].[SaveQuestioninitialData]
	@QuestionList PatientFlow.Question readonly
as
begin	
	set nocount on;
	
	set transaction isolation level read committed
	begin try
    
	begin transaction;  
		update [PatientFlow].[Question]
		set
			QuestionnaireId=question.QuestionnaireId ,
			QuestionText=question.QuestionText,
			QuestionType=question.QuestionType,
			OptionCharLimit=question.OptionCharLimit,
			QuestionOrder=question.QuestionOrder,
			Age1=question.Age1,
			Age2=question.Age2,
			Gender=question.Gender,
			Operation=question.Operation
		from @QuestionList question
		inner join [PatientFlow].[Question] q 
		on question.QuestionId=q.QuestionId;
		 
		insert into [PatientFlow].[Question]
		(
			QuestionId,
			QuestionnaireId,
			QuestionText,
			QuestionType,
			OptionCharLimit,
			Gender,
			QuestionOrder,
			Age1,
			Age2,					
			Operation
		)
		select 
			question.QuestionId,
			question.QuestionnaireId,
			question.QuestionText,
			question.QuestionType,
			question.OptionCharLimit,
			question.Gender,
			question.QuestionOrder,
			question.Age1,
			question.Age2,					
			question.Operation
		from @QuestionList question
		left outer join [PatientFlow].[Question] q
		on question.QuestionId=q.QuestionId
		where q.QuestionId is null
		
		delete qa from PatientFlow.QuestionAnswerOptions qa
		join PatientFlow.Question q on q.QuestionId = qa.QuestionId
		where q.QuestionId not in (select QuestionId from @QuestionList) 
			and QuestionnaireId = (select distinct QuestionnaireId from @QuestionList)
		
		delete q from PatientFlow.Question q
		where q.QuestionId not in (select QuestionId from @QuestionList) 
			and QuestionnaireId = (select distinct QuestionnaireId from @QuestionList)
		
commit transaction;
end try
begin catch;
		declare @Error int, @Message varchar(4000);		
		select 
			@Error = error_number(), 
			@Message = error_message();
		if xact_state() <> 0 begin
			rollback transaction;
		end
		raiserror('SaveAppointments : %d: %s', 16, 1, @Error, @Message);
end catch	;	
end
