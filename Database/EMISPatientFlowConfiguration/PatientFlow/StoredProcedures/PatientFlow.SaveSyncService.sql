if object_id('[PatientFlow].[SaveSyncService]') is not null
drop procedure [PatientFlow].[SaveSyncService]
go

create procedure [PatientFlow].[SaveSyncService]
	@SyncServiceId int,
	@OrganisationId int,
	@ProductKey uniqueidentifier,
	@Active bit,
	@ModifiedBy varchar(50)
as
begin
	
	set nocount on;
    set transaction isolation level read committed;
    if not exists ( select  1 from PatientFlow.SyncService where (SyncServiceId =@SyncServiceId))
	
		insert into [PatientFlow].[SyncService]
		(
			[ProductKey],
			[OrganisationId],
			[IsActivated],
			[ModifiedBy],
			[Modified]
		)
		values
		(
			@ProductKey,
			@OrganisationId,
			@Active,
			@ModifiedBy,
			GETDATE()
		)
     else
        update [PatientFlow].[SyncService]
		set 
			[OrganisationId] = @OrganisationId, 
			[IsActivated] = @Active,
			[ModifiedBy] = @ModifiedBy,
			[Modified] = getdate()
		where (SyncServiceId =@SyncServiceId)  
end
