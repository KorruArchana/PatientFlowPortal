if object_id ('[PatientFlow].[AddOrganisation]') is not null
	drop procedure [PatientFlow].[AddOrganisation];
go


create procedure [PatientFlow].[AddOrganisation]
	@OrganisationName varchar(100),
	@SystemTypeId int,
	@SiteNumber varchar(100)=null,
	@OrganisationKey varchar(50)=null,
	@Username varchar(150)=null,
	@Password varchar(150)=null,
	@IpAddress varchar(150)=null,
	@SupplierId varchar(150)=null,
	@WebServiceUrl varchar(500) =null,
	@InternalIPAddress varchar(40) = null,
	@ModifiedBy varchar(50),
    @Branches PatientFlow.OrganisationSites readonly
as
begin
set nocount on;
set transaction isolation level read committed
	begin try
		begin transaction
			insert into [PatientFlow].[Organisation]
			(
				OrganisationName,
				SystemTypeId,
				SiteNumber,
				ModifiedBy,
				Modified,
				OrganisationKey
			)
			values
			(
				@OrganisationName,
				@SystemTypeId,
				@SiteNumber,
				@ModifiedBy,
				getdate(),
				@OrganisationKey
			)
			declare @OrganisationId int
			set @OrganisationId= (select cast(scope_identity() as int))
            
			
			insert into [PatientFlow].[Site]
			(
				OrganisationId,
				SiteDBID,
				SiteName
			)			
			select 
				@OrganisationId,
				s.SiteDBID,
				s.SiteName
			from @Branches as s
           
				insert into PatientFlow.PatientFlowUser
				(
					UserName,
					[Password],
					SupplierId,
					IPAddress,
					OrganisationId,
					WebServiceUrl,
					InternalIPAddress
				)
				values
				(
					@Username,
					@Password,
					@SupplierId,
					@IpAddress,
					@OrganisationId,
					@WebServiceUrl,
					@InternalIPAddress
				)

		commit transaction
		select @OrganisationId as OrganisationId
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
		raiserror('AddOrganisation : %d: %s', 16, 1, @error, @message) ;
		rollback transaction
	end catch

end
