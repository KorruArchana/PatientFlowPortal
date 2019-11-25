if object_id ('[PatientFlow].[DeletePatient]') is not null
	drop procedure [PatientFlow].[DeletePatient];
go

create procedure [PatientFlow].[DeletePatient]
	@PatientMessageId int
as
begin
set nocount on;
set transaction isolation level read committed
begin try

delete from [PatientFlow].PatientMessage where PatientMessageId=@PatientMessageId 

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
		raiserror('DeletePatient : %d: %s', 16, 1, @error, @message) ;
	rollback transaction
end catch
end

