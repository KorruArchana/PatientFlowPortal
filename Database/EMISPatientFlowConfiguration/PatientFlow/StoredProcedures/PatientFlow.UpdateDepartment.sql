if object_id('[PatientFlow].[UpdateDepartment]') is not null
drop procedure [PatientFlow].[UpdateDepartment]
go

create procedure [PatientFlow].[UpdateDepartment]
	@DepartmentName varchar(100),
	@OrganisationId int,
	@DepartmentId int,
	@ModifiedBy varchar(50)
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    
	begin try
		begin transaction
			update [PatientFlow].[Department]
			set
				DepartmentName=@DepartmentName,
				OrganisationId=@OrganisationId,
				ModifiedBy = @ModifiedBy,
				Modified = getdate()
			where DepartmentId=@DepartmentId
	
			select @DepartmentId as result
		commit transaction
	end try

	begin catch
		declare @Error int, @Message varchar(4000)		
		select 
			@Error = error_number(), 
			@Message = error_message()
		if xact_state() <> 0 
			begin
				rollback transaction
			end
		raiserror('UpdateDepartment : %d: %s', 16, 1, @error, @message);  
	end catch
end









