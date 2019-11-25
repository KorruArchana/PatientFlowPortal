if object_id ('[PatientFlow].[DeleteKiosk]') is not null
	drop procedure [PatientFlow].[DeleteKiosk];
go

create procedure [PatientFlow].[DeleteKiosk]
	 @KioskId varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed
	begin try
		begin transaction

			delete from [PatientFlow].[KioskLogo] where KioskId=@KioskId
			delete from [PatientFlow].[KioskQuestionnaire] where KioskGuId=@KioskId
			delete from [PatientFlow].[KioskConfiguration] where KioskId=@KioskId

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
	raiserror('DeleteKiosk : %d: %s', 16, 1, @error, @message) ;        
	rollback transaction
end catch
end