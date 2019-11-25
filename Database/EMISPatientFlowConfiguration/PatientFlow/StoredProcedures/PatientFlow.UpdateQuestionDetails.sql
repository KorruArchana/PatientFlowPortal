if object_id('[PatientFlow].[UpdateQuestionDetails]') is not null
drop procedure [PatientFlow].[UpdateQuestionDetails]
go

create procedure [PatientFlow].[UpdateQuestionDetails]
	@QuestionId int,
	@QuestionnaireId int,
	@QuestionText varchar(200),
	@AgeCriteria int,
	@QuestionType int,
	@OptionCharLimit int,
	@Age1 int,
	@Age2 int,
	@Gender varchar(50),
	@Operation varchar(50),
	@QuestionOrder int
as
begin
	set nocount on;
	set transaction isolation level read committed;

	begin try
	begin transaction
		
		update [PatientFlow].[Question]
		set
			QuestionnaireId=@QuestionnaireId ,
			QuestionText=@QuestionText,
			AgeCriteria=@AgeCriteria,
			QuestionType=@QuestionType,
			OptionCharLimit=@OptionCharLimit,
			Age1=@Age1,
			Age2=@Age2,
			Gender=@Gender,
			Operation=@Operation,
			Modified=getdate(),
			QuestionOrder = @QuestionOrder		
		where QuestionId=@QuestionId
			
		delete from [PatientFlow].[QuestionAnswerOptions] 
		where QuestionId=@QuestionId
		
		update [PatientFlow].Questionnaire
		set Modified=getdate()
		where QuestionnaireId=@QuestionnaireId
		
		select @QuestionId as result  
			
	commit transaction
	end try

	begin catch
		declare @Error int, @Message varchar(4000)		
		select 
		@Error = error_number(), 
		@Message = error_message()
		if xact_state() <> 0 
			begin
				rollback transaction
			end
		raiserror('UpdateQuestionDetails : %d: %s', 16, 1, @error, @message) ;  
	end catch
end




