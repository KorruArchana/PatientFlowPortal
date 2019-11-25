if object_id ('[PatientFlow].[DeleteDivert]') is not null
	drop procedure [PatientFlow].[DeleteDivert];
go

create procedure [PatientFlow].[DeleteDivert]
	@DivertId int

as
begin
set nocount on;
set transaction isolation level read committed;
begin try
begin transaction

delete from [PatientFlow].[DivertLinkedToDetail] where DivertId=@DivertId
delete from [PatientFlow].[Divert] where DivertId=@DivertId
	
commit transaction
end try
begin catch
rollback transaction
end catch
END



