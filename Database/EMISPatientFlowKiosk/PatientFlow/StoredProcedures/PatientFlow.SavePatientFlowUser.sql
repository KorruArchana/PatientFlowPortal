if object_id ('[PatientFlow].[SavePatientFlowUser]') is not null
	drop procedure [PatientFlow].[SavePatientFlowUser];
go

create procedure [PatientFlow].[SavePatientFlowUser]
	@UserName varchar(150),
	@Password varchar(150),
	@SupplierId varchar(150),
	@DatabaseName varchar(50),
	@IPAddress varchar(100),
	@Type varchar(15),
	@WebServiceUrl varchar(500) ='',
	@OrganisationId int	
as
begin
	set nocount on;
	set transaction isolation level read committed;
	begin try;
    begin transaction; 
	if not exists (select 1 from PatientFlow.Organisation where (OrganisationId = @OrganisationId))
	   begin

			if exists (select 1 from PatientFlow.Organisation where OrganisationKey=@DatabaseName)
			begin
				declare @DeletedOrganisationId int
				set @DeletedOrganisationId = (select OrganisationId from [PatientFlow].[Organisation] where OrganisationKey=@DatabaseName)
			
				exec PatientFlow.DeleteQuestionnaireIds @DeletedOrganisationId
				exec PatientFlow.DeletePatientIds @DeletedOrganisationId

				delete from [PatientFlow].[AppointmentSession] where PatientFlowMemberId in ( select PatientFlowMemberId from PatientFlow.Member where OrganisationId=@DeletedOrganisationId);

				delete from [PatientFlow].[PatientFlowUser] where OrganisationId=@DeletedOrganisationId	
				delete from [PatientFlow].[Organisation] where OrganisationKey=@DatabaseName
			end
			 insert into [PatientFlow].[Organisation]
			   (
				  [OrganisationId]
				 ,[OrganisationKey]
				 ,[SystemType]
				)
			 values
			   (
				   @OrganisationId,
				   @DatabaseName,
				   @Type
			   );
	   end
	 else
	  begin
	     update [PatientFlow].[Organisation]
			  set 
				[OrganisationKey] = @DatabaseName,
				[SystemType] = @Type
			where  OrganisationId= @OrganisationId;
	  end
	
	
	if not exists (select 1 from PatientFlow.PatientFlowUser where (OrganisationId = @OrganisationId))
	   begin
			 insert into [PatientFlow].[PatientFlowUser]
			   (
				   [UserName],
				   [Password],
				   [SupplierId],
				   [OrganisationId],
				   [IPAddress],
				   [WebServiceUrl]
				)
			 values
			   (
				   @UserName,
				   @Password,
				   @SupplierId,
				   @OrganisationId,
				   @IPAddress,
				   @WebServiceUrl
			   );
	   end
	 else
	  begin
	     update [PatientFlow].[PatientFlowUser]
			  set 
				[UserName] = @UserName,
				[Password] = @Password,
				[SupplierId] = @SupplierId,
				[IPAddress] = @IPAddress,
				[WebServiceUrl]= @WebServiceUrl
			where  OrganisationId= @OrganisationId;
	  end	   
commit transaction;

end try
begin catch;
		declare @Error int, @Message varchar(4000);		
		select 
			@Error = error_number(), 
			@Message = error_message();
		if xact_state() <> 0 begin
			rollback transaction;
		end
		raiserror('SavePatientFlowUser : %d: %s', 16, 1, @Error, @Message);
end catch;		
end