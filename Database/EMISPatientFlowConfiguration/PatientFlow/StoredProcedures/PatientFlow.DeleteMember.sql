if object_id ('[PatientFlow].[DeleteMember]') is not null
	drop procedure [PatientFlow].[DeleteMember];
go

create procedure [PatientFlow].[DeleteMember]
	@MemberId int
as
begin
set nocount on;
set transaction isolation level read committed
   
begin try
begin transaction

delete from [PatientFlow].Member
where MemberId=@MemberId

delete from PatientFlow.AlertsLinkedToDepMem
where TypeId=@MemberId

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
		raiserror('DeleteMember : %d: %s', 16, 1, @error, @message) ;
	rollback transaction
end catch
end


