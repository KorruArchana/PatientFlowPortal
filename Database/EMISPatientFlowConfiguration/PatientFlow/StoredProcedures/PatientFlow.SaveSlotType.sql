if object_id('[PatientFlow].[SaveSlotType]') is not null
drop procedure [PatientFlow].[SaveSlotType]
go

create procedure [PatientFlow].[SaveSlotType]
	@SlotTypeList PatientFlow.AppointmentSlotType readonly
as
begin
	
	set nocount on;
	
	set transaction isolation level read committed
	begin try
		begin transaction;  
   
			delete from [PatientFlow].[AppointmentSlotType] where OrganisationId in(select distinct OrganisationId from @SlotTypeList);
		 
			insert into [PatientFlow].[AppointmentSlotType]
			(
				OrganisationId,
				SlotTypeId,
				[Description]
			)
			select 
				s.OrganisationId,
				s.SlotTypeId,
				s.[Description]
			from @SlotTypeList s
			left outer join [PatientFlow].[AppointmentSlotType] a 
			on a.OrganisationId = s.OrganisationId and a.SlotTypeId=s.SlotTypeId 
			where a.OrganisationId is null and a.SlotTypeId is null;
	
		commit transaction;

	end try
	begin catch
		declare @Error int, @Message varchar(4000)		
		select 
			@Error = error_number(), 
			@Message = error_message()
		if xact_state() <> 0 begin
			rollback transaction
		end
		raiserror('SaveSlotType : %d: %s', 16, 1, @error, @message) ;
	end catch		
end