if object_id ('[PatientFlow].[AddQuestionAnswerOptions]') is not null
	drop procedure [PatientFlow].[AddQuestionAnswerOptions];
go

create procedure [PatientFlow].[AddQuestionAnswerOptions]
	@QuestionId int,
	@OptionValue varchar(1000),	
	@OptionCode varchar(100),
    @NestedQuestionId int,
	@OptionId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	begin try
		begin transaction;
		insert into [PatientFlow].[QuestionAnswerOptions]
		(
			QuestionId,
			OptionValue,
			OptionCode,
			NestedQuestionId,
			OptionId
		)
		values
		(
			@QuestionId,
			@OptionValue,
			@OptionCode,
			@NestedQuestionId,
			@optionId
		);

		commit transaction;
		select @OptionId;
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
		raiserror('AddQuestionAnswerOptions : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch
end
