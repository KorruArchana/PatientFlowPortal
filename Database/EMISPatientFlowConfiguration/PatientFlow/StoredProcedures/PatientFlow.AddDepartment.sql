if object_id ('[PatientFlow].[AddDepartment]') is not null
	drop procedure [PatientFlow].[AddDepartment];
go

create procedure [PatientFlow].[AddDepartment] 
	@DepartmentName varchar(100),
	@OrganisationId int,
	@ModifiedBy varchar(50)
as
begin
set nocount on;
set transaction isolation level read committed

begin try
begin transaction

	insert into [PatientFlow].[Department]
	(
		DepartmentName,
		OrganisationId,
		ModifiedBy,
		Modified	
	)
	values
	(
		@DepartmentName,
		@OrganisationId,
		@ModifiedBy ,
		getdate()
	);

	declare @DepartmentId int
	set @DepartmentId= (select cast(scope_identity() as int))
	select @DepartmentId as DepartmentId
	
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

raiserror('AddDepartment : %d: %s', 16, 1, @error, @message) ;        
rollback transaction
end catch

end
go


