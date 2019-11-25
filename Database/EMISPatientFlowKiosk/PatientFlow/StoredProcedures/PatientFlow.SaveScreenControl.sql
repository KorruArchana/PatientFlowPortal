if object_id ('[PatientFlow].[SaveScreenControl]') is not null
	drop procedure [PatientFlow].[SaveScreenControl];
go

create procedure [PatientFlow].[SaveScreenControl]
	@LastRowModifiedDate datetime,
	@ScreenControlList PatientFlow.ScreenControl readonly
as
begin
	set nocount on;
	
	set transaction isolation level read committed;
	begin try
    
	begin transaction;   
		update [PatientFlow].[KioskScreenControl]
		set
			TranslationRefId=screenControl.TranslationRefId
		from @ScreenControlList screenControl
		inner join [PatientFlow].[KioskScreenControl] s
		on s.ControlId=screenControl.ControlId;

		update [PatientFlow].[SynchronisationLog]
		set Modified=@LastRowModifiedDate
		where SyncType=4;
		
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
		raiserror('SaveScreenControl : %d: %s', 16, 1, @Error, @Message);
end catch;	
end



