if object_id ('[PatientFlow].[AddKioskLinkedToDepMemDetails]') is not null
	drop procedure [PatientFlow].[AddKioskLinkedToDepMemDetails];
go

create procedure [PatientFlow].[AddKioskLinkedToDepMemDetails]
@KioskId int,
@Departments as [PatientFlow].[List] readonly,
@Members as [PatientFlow].[List] readonly,
@ModifiedBy varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed
	
	begin try
	begin transaction
	
		delete from Patientflow.[KioskLinkedToDepMemDetails] where KioskId=@KioskId

		insert into Patientflow.[KioskLinkedToDepMemDetails] 
		(
			kioskid,
			LinkTypeId,
			TypeId,
			ModifiedBy,
			Modified
		)
		(
			select 
				@kioskId,
				2,
				Id,
				@ModifiedBy,
				getdate()
				from @Departments
		)
		  
		insert into Patientflow.[KioskLinkedToDepMemDetails]
		(
			kioskid,
			LinkTypeId,
			TypeId,
			ModifiedBy,
			Modified
		)
		(
			select 
				@kioskId,
				3,
				Id,
				@ModifiedBy,
				getdate()
			from @Members
		)
		   
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
			raiserror('AddKioskLinkedToDepMemDetails : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch
end



