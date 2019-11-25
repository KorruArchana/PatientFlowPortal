if object_id('[PatientFlow].[SaveSyncServiceOrganisations]') is not null
drop procedure [PatientFlow].[SaveSyncServiceOrganisations]
go

create procedure [PatientFlow].[SaveSyncServiceOrganisations]
	@Organisations [PatientFlow].[List] readonly,
	@ProductKey uniqueidentifier,
	@Active bit,
	@ModifiedBy varchar(50),
	@KioskId integer
as
begin
	
set nocount on;
set transaction isolation level read committed;
	
begin try;
begin transaction; 
				-- Update known syncservice.

				 update [PatientFlow].[SyncService]
					set 
						[IsActivated] = @Active,
						[ModifiedBy] = @ModifiedBy,
						[KioskId] = @KioskId,
						[Modified] = getdate()
				from @Organisations o
					inner join PatientFlow.SyncService s
						on o.Id = s.OrganisationId where (s.ProductKey =@ProductKey);
				-- insert new sync service		
				if not exists ( select  1 from PatientFlow.SyncService where (ProductKey =@ProductKey))				
									insert into [PatientFlow].[SyncService]
									(
										[ProductKey],
										[OrganisationId],
										[IsActivated],
										[ModifiedBy],
										[Modified],
										[KioskId]
									)
									select 	
											@ProductKey,
											Id,
											@Active,
											@ModifiedBy,
											GETDATE(),
											@KioskId 
									from @Organisations   
				 else
					 begin	
									insert into [PatientFlow].[SyncService]  
									( 
										[ProductKey], 
										[OrganisationId],
										[IsActivated], 
										[ModifiedBy], 
										[Modified],
										[KioskId]
									)
									select 
										@ProductKey,
										o.Id,
										@Active, 
										@ModifiedBy, 
										getdate(),
										@KioskId
									from @Organisations o 
									left outer join (select OrganisationId from  PatientFlow.SyncService where (ProductKey =@ProductKey))as m
											on o.Id = m.OrganisationId
									where m.OrganisationId is null ;
					 end
									   
				 -- DELETE RECORDS
					delete  from  PatientFlow.SyncService  where (ProductKey = @ProductKey) and  OrganisationId not in ( select  Id from @Organisations )  
			
		commit transaction;
		end try
		begin catch
				declare @Error int, @Message varchar(4000)		
				select 
					@Error = error_number(), 
					@Message = error_message()
				If xact_state() <> 0 begin
					rollback transaction
				end
				raiserror('SaveSyncServiceOrganisations : %d: %s', 16, 1, @error, @message) ;
		end catch     
end