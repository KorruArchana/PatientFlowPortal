if object_id ('[PatientFlow].[SaveQuestion]') is not null
	drop procedure [PatientFlow].[SaveQuestion];
go

create procedure [PatientFlow].[SaveQuestion]
	@QuestionId int,
	@QuestionnaireId int,
	@QuestionText varchar(1000),
	@QuestionType int,
	@OptionCharLimit int =null,
	@Age1 int,
	@Age2 int,
	@Gender varchar(50),
	@Operation varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;
	begin try
	begin transaction;
	
	if not exists ( 
	select 1 
	from PatientFlow.Question 
	where (QuestionId = @QuestionId))
	   begin			 
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
					QuestionId,
					QuestionOrder
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
					@QuestionId,					
					(
						select count(QuestionOrder)+1 
						from [PatientFlow].[Question] 
						where QuestionnaireId=@QuestionnaireId
					)
				);
	   end
	 else
	  begin
	     update [PatientFlow].[Question]
		set
			QuestionnaireId=@QuestionnaireId ,
			QuestionText=@QuestionText,
			QuestionType=@QuestionType,
			OptionCharLimit=@OptionCharLimit,
			Age1=@Age1,
			Age2=@Age2,
			Gender=@Gender,
			Operation=@Operation
		where QuestionId=@QuestionId;

		delete from [PatientFlow].[QuestionAnswerOptions] 
		where QuestionId=@QuestionId;
	    
	  end
	
	select 1
	commit transaction;
	end try
	begin catch;
		declare @Error int, @Message varchar(4000)		
		select 
			@Error = error_number(), 
			@Message = error_message()
		if xact_state() <> 0 
			begin
				rollback transaction
			end
		raiserror('SaveQuestion : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch;
end
