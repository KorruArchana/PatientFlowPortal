if object_id ('[PatientFlow].[DeleteQuestion]') is not null
	drop procedure [PatientFlow].[DeleteQuestion];
go

create procedure [PatientFlow].[DeleteQuestion]
	@QuestionId int

as
begin
	set nocount on;
	set transaction isolation level read committed
	
	begin try
	begin transaction

		delete from [PatientFlow].[QuestionAnswerOptions] 
		where QuestionId=@QuestionId
		
		delete from  [PatientFlow].[Question] 
		where QuestionId=@QuestionId

		select 1 as Success 
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
			raiserror('DeleteQuestion : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch
end






