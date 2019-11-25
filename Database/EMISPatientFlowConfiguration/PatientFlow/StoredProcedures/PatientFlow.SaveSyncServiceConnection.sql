if object_id('[PatientFlow].[SaveSyncServiceConnection]') is not null
drop procedure [PatientFlow].[SaveSyncServiceConnection]
go

create procedure [PatientFlow].[SaveSyncServiceConnection] 
	@ProductKey uniqueidentifier,
	@ConnectionId uniqueidentifier,
	@Disconnect bit,
	@Type char(1),
	@Result int output
as
begin
	set nocount on;
	set transaction isolation level read committed;
	declare @OldConnctionId uniqueidentifier
	
	set @Result = 0;
	
	select @OldConnctionId=case 
								when @Type = 'S' then SyncConnectionId 
	                            when @Type =  'O' then OrgConnectionId
	                       end        
	            from  PatientFlow.SyncService  where (ProductKey = @ProductKey) AND (IsActivated = 1)
   
    if @Disconnect = 1
           update [PatientFlow].[SyncService]
			set 
				[SyncConnectionId] = case  when @Type = 'S' then null else [SyncConnectionId] end,
				[OrgConnectionId] = case  when @Type = 'O' then null else [OrgConnectionId] end,
				[Modified]= getdate()
           where (ProductKey = @ProductKey) and (IsActivated = 1)
    
    else if @OldConnctionId is null
           update [PatientFlow].[SyncService]
			set 
				[SyncConnectionId] = case  when @Type = 'S' then @ConnectionId  else [SyncConnectionId] end,
				[OrgConnectionId] = case  when @Type = 'O' then @ConnectionId  else [OrgConnectionId] end,
				[Modified]= getdate()
           where (ProductKey = @ProductKey) and (IsActivated = 1)
    else if @OldconnctionId <> @ConnectionId
             set @Result = -1;
end
