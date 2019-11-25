if object_id('[PatientFlow].[UpdateMemberDetails]') is not null
drop procedure [PatientFlow].[UpdateMemberDetails]
go

create procedure [PatientFlow].[UpdateMemberDetails]
	@MemberId int,
	@DepartmentId int,
	@ModifiedBy varchar(50)=null
AS
BEGIN
	
	set nocount on;
	set transaction isolation level read committed;

	begin try
		begin transaction
			update [PatientFlow].[Member]
			set
				DepartmentId = @DepartmentId,
				ModifiedBy = @ModifiedBy,
				Modified = getdate()
			where MemberId=@MemberId

			select @MemberId as Result
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
		raiserror('UpdateMemberDetails : %d: %s', 16, 1, @error, @message);  
	end catch
end






