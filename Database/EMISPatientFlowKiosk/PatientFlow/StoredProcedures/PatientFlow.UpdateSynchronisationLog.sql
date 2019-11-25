if object_id ('[PatientFlow].UpdateSynchronisationLog') is not null
	drop procedure [PatientFlow].[UpdateSynchronisationLog];
go

create procedure [PatientFlow].[UpdateSynchronisationLog]
	@Type smallint,
	@LastItemID bigint,
	@ProductKey varchar(50)
as

begin
	set nocount on;
	set transaction isolation level read committed;


    if exists (			
				select 1 from PatientFlow.Log 
				where Message like 'Info%' and [date] < GETDATE()-2
				)
	begin	
		delete from PatientFlow.Log where Message like 'Info%' and [date] < GETDATE()-2;
	end
	
	  if exists (			
				select 1 from PatientFlow.Log 
				where Message like 'Completed - SyncBookedAppointments%' and [date] < GETDATE()-1
				)
	begin	
		delete from PatientFlow.Log where Message like 'Completed - SyncBookedAppointments%' and [date] < GETDATE()-1;
	end

	if not exists (
			select 1 
			from PatientFlow.SynchronisationLog 
			where SyncType=@Type and ProductKey=@ProductKey)
	begin
		insert into [PatientFlow].[SynchronisationLog]
        (
			[SyncType],
			[LastRowID],
			[Modified],
			ProductKey
		)
		values
		(
			@Type,
			@LastItemID,
			getdate(),
			@ProductKey
		);
    end
    else
	begin
		update [PatientFlow].[SynchronisationLog]
		set 
			[LastRowID] = @LastItemID,
			[Modified] = getdate()
		where 
			[SyncType] = @Type
		and [ProductKey] = @ProductKey;
    end     
end

    
   
    
