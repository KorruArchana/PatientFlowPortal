if object_id ('[PatientFlow].[DeleteKiosk]') is not null
	drop procedure [PatientFlow].[DeleteKiosk];
go

create procedure [PatientFlow].[DeleteKiosk]
	 @KioskId int
as

begin
	set nocount on;
	set transaction isolation level read committed
	
	begin try
	begin transaction
			
		delete from PatientFlow.KioskArrivalConfiguration 
		where KioskId = @KioskId;
		
		delete from PatientFlow.SyncService 
		where KioskId = @KioskId;

		delete from Patientflow.KioskDemographicDetailsType
		where KioskId=@KioskId 
	
		delete from PatientFlow.KioskQuestionnaire
		where KioskId = @KioskId

		delete from PatientFlow.AlertLinkToKiosk 
		where KioskId = @KioskId;

		delete from Patientflow.KioskLinkedToDetails
		where KioskId=@KioskId
		
		if(not exists (select top 1.* from [PatientFlow].[KioskLinkedToDetails] where KioskId = @KioskId))
		begin								
			delete from Patientflow.KioskLanguage where KioskId=@KioskId;
			delete from Patientflow.KioskSessionHolder where KioskId=@KioskId;
			delete from Patientflow.KioskModule where KioskId=@KioskId;				
			delete from Patientflow.KioskSlotType where KioskId=@KioskId;
			delete from Patientflow.KioskSiteMap where KioskId=@KioskId;
			delete from PatientFlow.Kiosk where KioskId=@KioskId;				
		end		
			
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