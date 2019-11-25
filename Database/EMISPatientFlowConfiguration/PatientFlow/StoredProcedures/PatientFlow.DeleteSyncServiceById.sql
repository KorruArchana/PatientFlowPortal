if object_id ('PatientFlow.DeleteSyncServiceById') is not null
	drop procedure PatientFlow.DeleteSyncServiceById;
go

create procedure [PatientFlow].[DeleteSyncServiceById]
	@SyncServiceId int
as
begin
set nocount on;
set transaction isolation level read committed;
	delete from PatientFlow.SyncService where  PatientFlow.SyncService.SyncServiceId = @SyncServiceId  
end
