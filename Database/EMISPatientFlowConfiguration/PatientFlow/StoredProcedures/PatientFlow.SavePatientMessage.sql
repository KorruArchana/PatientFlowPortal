if object_id('[PatientFlow].[SavePatientMessage]') is not null
	drop procedure [PatientFlow].[SavePatientMessage]
go

create procedure [PatientFlow].[SavePatientMessage]
	@PatientId int,
	@Message nvarchar(400),
	@Firstname varchar(100),
	@Surname varchar(100)=null,
	@Dob varchar(150),
	@OrganisationId int,
	@PatientMessageId int,
	@Modifiedby Varchar(100)
as
begin
	set nocount on;
	set transaction isolation level read committed;
	begin try
	begin transaction
	if not exists ( select  1 from PatientFlow.[Patient] where (PatientId =@PatientId and OrganisationId=@OrganisationId))
		begin
			insert into [PatientFlow].[Patient]
			(
				Firstname,
				Surname,
				PatientId,
				OrganisationId,
				DOB,
				ModifiedBy,
				Modified 
			)
			values
			(
				@Firstname,
				@Surname,
				@PatientId ,
				@OrganisationId ,
				@Dob,
				@ModifiedBy ,
				getdate()
			)

			insert into [PatientFlow].[PatientMessage]
			(
				PatientId,
				OrganisationId,
				[Message],
				Modified,
				ModifiedBy
			)
			values
			(
				@PatientId,
				@OrganisationId,
				@Message,
				getdate(),
				@ModifiedBy
			)

			set @PatientMessageId=(select cast(scope_identity() as int))
		end
     else
		begin
			if not exists ( select  1 from PatientFlow.[PatientMessage] where (PatientMessageId =@PatientMessageId))
				begin
					insert into [PatientFlow].[PatientMessage]
					(
						PatientId,
						OrganisationId,
						[Message],
						Modified,
						ModifiedBy
					)
					values
					(
						@PatientId,
						@OrganisationId,
						@Message,
						getdate(),
						@ModifiedBy
					)
					set @PatientMessageId=(select cast(scope_identity() as int))
				end
			else
				update [PatientFlow].[PatientMessage]
				set
					[Message]=@Message,
					Modified=getdate()
				where PatientMessageId=@PatientMessageId
		end
	 select @PatientMessageId as PatientMessageId
	 commit transaction
     end try  
	 begin catch
		declare @Error int, @ErrorMessage varchar(4000);		
		select 
			@Error = error_number(), 
			@ErrorMessage = error_message();
		if xact_state() <> 0 begin
			rollback transaction;
		end
		raiserror('SavePatientMessage : %d: %s', 16, 1, @Error, @ErrorMessage);    
		rollback transaction
	 end catch
end

go

exec sys.sp_addextendedproperty 
	@name = N'UnitTestException_TestForInvalidDataTypeUse',
	@value = N'Patient Message should support multiple language text.',
	@level0type = N'SCHEMA',
	@level0name = 'PatientFlow',
	@level1type = N'PROCEDURE',
	@level1name = 'SavePatientMessage',
	@level2type = N'PARAMETER',
	@level2name = '@Message';