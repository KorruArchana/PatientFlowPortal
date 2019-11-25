if object_id ('[PatientFlow].[DeleteAlert]') is not null
	drop procedure [PatientFlow].[DeleteAlert];
go

create procedure [PatientFlow].[DeleteAlert] 
	@AlertId int
as
begin
set nocount on;
set transaction isolation level read committed;
begin try
begin transaction

delete from [PatientFlow].[AlertsLinkedToDepMem] where AlertId=@AlertId
delete from [PatientFlow].[AlertLinkToOrganisation] where AlertId=@AlertId
delete from [PatientFlow].[AlertLinkToKiosk] where AlertId=@AlertId
delete from [PatientFlow].[Alert] where AlertId=@AlertId

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
		raiserror('DeleteAlert : %d: %s', 16, 1, @error, @message) ;
	rollback transaction
end catch
end

