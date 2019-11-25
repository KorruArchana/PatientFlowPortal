if object_id ('[PatientFlow].[DeleteDepartment]') is not null
drop procedure [PatientFlow].[DeleteDepartment];
go
create procedure [PatientFlow].[DeleteDepartment]
 @DepartmentId int
as
begin
set nocount on;
set transaction isolation level read committed

 begin try
 begin transaction

	delete from [PatientFlow].Department
	where DepartmentId=@DepartmentId

commit transaction
end try

begin catch
	declare @Error int, @Message varchar(4000)		
	select 
		@Error = error_number(), 
		@Message = error_message()
	if xact_state() <> 0 begin
		rollback transaction
	end
	raiserror('DeleteDepartment : %d: %s', 16, 1, @error, @message) ;           
	rollback transaction
end catch
 
end
go

