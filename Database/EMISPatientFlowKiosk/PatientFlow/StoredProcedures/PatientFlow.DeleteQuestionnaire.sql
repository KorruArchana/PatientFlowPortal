if object_id ('[PatientFlow].[DeleteQuestionnaire]') is not null
	drop procedure [PatientFlow].[DeleteQuestionnaire];
go

create procedure [PatientFlow].[DeleteQuestionnaire]
	@QuestionnaireId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	begin try
		begin transaction

		delete from [PatientFlow].[KioskQuestionnaire] 
		where QuestionnaireId = @QuestionnaireId;

		delete QuestionAnswerOptions
		from [PatientFlow].[Question] as Question
			inner join [PatientFlow].[QuestionAnswerOptions] as QuestionAnswerOptions
			on Question.QuestionId=QuestionAnswerOptions.QuestionId
		where Question.QuestionnaireId = @QuestionnaireId;

		delete from  [PatientFlow].[Question] 
		where QuestionnaireId = @QuestionnaireId;

		delete from [PatientFlow].[Questionnaire] 
		where QuestionnaireId = @QuestionnaireId;

		select 1;  
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
		raiserror('DeleteQuestionnaire : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch
end
