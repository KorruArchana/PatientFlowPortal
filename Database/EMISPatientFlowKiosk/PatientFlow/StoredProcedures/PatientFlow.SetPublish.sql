if object_id ('[PatientFlow].[SetPublish]') is not null
	drop procedure [PatientFlow].[SetPublish];
go

create procedure [PatientFlow].[SetPublish]
	@QuestionnaireId int,
	@Status bit,
	@KioskId varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;

	begin try
	begin transaction

	if(@Status=0)
		begin
			delete from Patientflow.KioskQuestionnaire where QuestionnaireId=@QuestionnaireId;
		end
	else
		begin
			insert into Patientflow.KioskQuestionnaire
			(
				KioskGuid,
				QuestionnaireId,
				QuestionnaireOrder
			)
			values
			(
				@KioskId, 
				@QuestionnaireId, 
				IsNull((select max(QuestionnaireOrder) from PatientFlow.KioskQuestionnaire),0)+ 1
			 );
		end
		
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
		raiserror('SetPublish : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch;
end



