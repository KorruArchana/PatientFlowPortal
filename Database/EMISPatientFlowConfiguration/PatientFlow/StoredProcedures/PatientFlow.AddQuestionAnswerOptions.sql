if object_id ('[PatientFlow].[AddQuestionAnswerOptions]') is not null
	drop procedure [PatientFlow].[AddQuestionAnswerOptions];
go

create procedure [PatientFlow].[AddQuestionAnswerOptions]
	@QuestionId int,
	@OptionValue varchar(200),	
	@OptionCode varchar(50),
	@SnomedCode bigint,
    @NestedQuestionId int

as
begin
	set nocount on;
	set transaction isolation level read committed
	
	begin try
	begin transaction

		insert into [PatientFlow].[QuestionAnswerOptions]
		(
			QuestionId,
			OptionValue,
			OptionCode,
			SnomedCode,
			NestedQuestionId,
			Modified
		)
		values
		(
			@QuestionId,
			@OptionValue,
			@OptionCode,
			@SnomedCode,
			@NestedQuestionId,
			getdate()
		);
		
		declare @OptionId int
		set @OptionId= (select cast(scope_identity() as int))

		select @OptionId as OptionId

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
			raiserror('AddQuestionAnswerOptions : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch

end





