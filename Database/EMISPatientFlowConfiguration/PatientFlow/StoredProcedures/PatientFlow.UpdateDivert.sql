if object_id('[PatientFlow].[UpdateDivert]') is not null
drop procedure [PatientFlow].[UpdateDivert]
go

create procedure [PatientFlow].[UpdateDivert]
    @DivertId int,
	@DivertMessage varchar(250),
	@OrganisationId int,
	@Departments as [PatientFlow].[List] readonly,
	@Members As [PatientFlow].[List] readonly,
	@ModifiedBy varchar(50)
 

as
begin
	
	set nocount on;
	set  transaction isolation level read committed;
	begin try
		begin transaction
			update [PatientFlow].[Divert]
			set 
				DivertMessage=@DivertMessage,
				OrganisationId=@OrganisationId,
				ModifiedBy = @ModifiedBy,
				Modified = getdate()
			where DivertId=@DivertId

			delete from Patientflow.DivertLinkedToDetail where DivertId=@DivertId

			------------

			insert into Patientflow.DivertLinkedToDetail
			(	
				DivertId,
				LinkTypeId,
				TypeId,
				ModifiedBy,
				Modified
			)
			(
				select 
					@DivertId,
					2,
					Id,
					@ModifiedBy,
					getdate()
				from @Departments
			)
    
			insert into Patientflow.DivertLinkedToDetail
			(
				DivertId,
				LinkTypeId,
				TypeId,
				ModifiedBy,
				Modified
			)
			(
				select 
					@DivertId,
					3,
					Id,
					@ModifiedBy,
					getdate() 
				from @Members
			)
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
		raiserror('UpdateDivert : %d: %s', 16, 1, @error, @message);  
	end catch
end






