if object_id ('[PatientFlow].[GetSyncModifiedDate]') is not null
	drop procedure [PatientFlow].[GetSyncModifiedDate];
go

create procedure [PatientFlow].[GetSyncModifiedDate]
	@SyncType int,
	@ProductKey varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;
	if not exists(select * from [PatientFlow].[SynchronisationLog] where  SyncType = @SyncType and ProductKey = @Productkey)
		begin
			insert into [PatientFlow].[SynchronisationLog]
			(
				SyncType,
				LastRowID,
				Modified,
				ProductKey
			)
			values
			(
				@SyncType,
				0,
				cast('1753-1-1' as datetime),
				@ProductKey
			)
		end

	select Modified 
	from   [PatientFlow].[SynchronisationLog]
	where  SyncType = @SyncType
	and ProductKey = @ProductKey
end
