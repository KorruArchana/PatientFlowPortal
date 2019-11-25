if object_id('[PatientFlow].[IsExistSyncService]') is not null
drop Procedure [PatientFlow].[IsExistSyncService]
go

create procedure [PatientFlow].[IsExistSyncService]
	@ProductKey varchar(50),
	@Result bit output
as
begin
	set nocount on;
    set transaction isolation level read committed;
 if exists (select 1 from  PatientFlow.SyncService where (ProductKey = @ProductKey) and (IsActivated = 1))
    set @Result= 1;
 else
     set @Result= 0;   

end
