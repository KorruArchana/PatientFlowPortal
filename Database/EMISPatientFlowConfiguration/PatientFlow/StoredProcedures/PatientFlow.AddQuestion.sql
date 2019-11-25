if object_id ('[PatientFlow].[AddQuestion]') is not null
	drop procedure [PatientFlow].[AddQuestion];
go

create procedure [PatientFlow].[AddQuestion]
	@QuestionnaireId int,
	@QuestionText varchar(1000),
	@QuestionType int,
	@OptionCharLimit int = null,
	@Age1 int,
	@Age2 int,
	@Gender varchar(50),
	@Operation varchar(50),
	@QuestionOrder int

as
begin
	set nocount on;
	set transaction isolation level read committed
	
	begin try
	begin transaction

		insert into [PatientFlow].[Question]
		(
			QuestionnaireId,
			QuestionText,
			QuestionType,
			OptionCharLimit,
			Age1,
			Age2,
			Gender,
			Operation,
			QuestionOrder,
			Modified
		)
		values
		(
			@QuestionnaireId ,
			@QuestionText,
			@QuestionType,
			@OptionCharLimit,
			@Age1,
			@Age2,
			@Gender,
			@Operation,
			@QuestionOrder,
			getdate()
		);

		update [PatientFlow].Questionnaire
		set Modified=getdate()
		where QuestionnaireId=@QuestionnaireId

		declare @QuestionId int
		set @QuestionId= (select cast(scope_identity() as int))

		select @QuestionId as QuestionId
		
	commit transaction
	end Try

	begin catch
		declare @Error int, @Message varchar(4000)		
			select 
				@Error = error_number(), 
				@Message = error_message()
			if xact_state() <> 0 
				begin
					rollback transaction
				end
			raiserror('AddQuestion : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch

end






