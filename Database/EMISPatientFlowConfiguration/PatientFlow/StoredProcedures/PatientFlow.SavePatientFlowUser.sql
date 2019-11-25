if object_id('[PatientFlow].[SavePatientFlowUser]') is not null
drop procedure [PatientFlow].[SavePatientFlowUser]
go

create procedure [PatientFlow].[SavePatientFlowUser]
	@UserName varchar(150),
	@Password varchar(150),
	@SupplierId varchar(150),
	@OrganisationId varchar(50),
	@IPAddress varchar(50),
	@DatabaseName varchar(100),
	@InternalIPAddress varchar(40) = null
as
begin
	set nocount on;
    set transaction isolation level read committed;
    if not exists ( select  1 from PatientFlow.PatientFlowUser where (OrganisationId = @OrganisationId) )
	   begin
			 insert into [PatientFlow].[PatientFlowUser]
			   (
					[UserName],
					[Password],
					[SupplierId],
					[OrganisationId],
					[IPAddress],
					DatabaseName,
					InternalIPAddress
			   )
			 VALUES
			   (
					@UserName,
					@Password,
					@SupplierId,
					@OrganisationId,
					@IPAddress,
					@DatabaseName,
					@InternalIPAddress
				)
	   end
	 else
	  begin
	     update [PatientFlow].[PatientFlowUser]
		 set 
			[UserName] = @UserName,
			[Password] = @Password,
			[SupplierId] = @SupplierId,
			[IPAddress] = @IPAddress,
			DatabaseName=@DatabaseName,
			InternalIPAddress = @InternalIPAddress
		where [OrganisationId] = @OrganisationId 
	    
	  end
	   
end
