
if object_id ('[PatientFlow].[SaveQuestionOptioninitialData]') is not null
	drop procedure [PatientFlow].[SaveQuestionOptioninitialData];
go

create procedure [PatientFlow].[SaveQuestionOptioninitialData] 
	@QuestionOptionList PatientFlow.QuestionOption readonly
as
begin	
	set nocount on;
	set transaction isolation level read committed

	begin try
    
	begin transaction;  
    
    delete from [PatientFlow].QuestionAnswerOptions 
    where QuestionId in 
    (
		select distinct QuestionId 
		from @QuestionOptionList
    )

	insert into [PatientFlow].QuestionAnswerOptions
	(
		QuestionId,
		OptionId,
		OptionValue,
		NestedQuestionId,
		OptionCode,
		SnomedCode		
	)
	select 
		questionOption.QuestionId,
		questionOption.OptionId,
		questionOption.OptionValue,
		questionOption.NestedQuestionId,
		questionOption.OptionCode,
		questionOption.SnomedCode		
	from @QuestionOptionList questionOption
	
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
end catch;		
end