if object_id('[PatientFlow].[UpdateOrganisationDetails]') is not null
drop procedure [PatientFlow].[UpdateOrganisationDetails]
go

create procedure [PatientFlow].[UpdateOrganisationDetails]
	@OrganisationId int,
	@OrganisationName varchar(100),
	@OrganisationKey varchar(100)=null,
	@SiteNumber varchar(50),
	@Username varchar(150)=null,
	@Password varchar(150)=null,
	@IpAddress varchar(150)=null,
	@InternalIPAddress varchar(40)=null,
	@SupplierId varchar(150)=null,
	@WebServiceUrl varchar(500)=null,
	@ModifiedBy varchar(50),
	@Branches PatientFlow.OrganisationSites readonly
as
begin
	set nocount on;
	set transaction isolation level read committed
	begin try
		begin transaction

			update [PatientFlow].[Organisation]
			set
				OrganisationName=@OrganisationName,
				SiteNumber=@SiteNumber,
				OrganisationKey=@OrganisationKey,
				ModifiedBy = @ModifiedBy,
				Modified = getdate()	
			where OrganisationId=@OrganisationId;
			
			declare @SystemTypeId int;
			set @SystemTypeId=(select systemTypeId from [PatientFlow].[Organisation] where OrganisationId=@OrganisationId)
	        
	        if(@SystemTypeId=1 or @SystemTypeId = 2 or @SystemTypeId = 6)  
			begin
					
			update kld
				set SiteId = null
			from patientflow.KioskLinkedToDetails kld
			join patientflow.site s 
			on kld.SiteId = s.SiteId 
			left join @Branches b on s.SiteDBID = b.SiteDBID and s.OrganisationId = b.OrganisationId
			where kld.TypeId = @OrganisationId
			and b.SiteDBID is null							
			
			delete 
			from PatientFlow.[Site]
			where OrganisationId=@OrganisationId 
			and SiteDBID not in 
			(
				select SiteDBID
				from @Branches b 
				where b.OrganisationId = @OrganisationId
			)		
	
			update site
			set SiteName = b.SiteName
			from  PatientFlow.Site site
			join @Branches b 
			on site.SiteDBID = b.SiteDBID
			and site.OrganisationId = b.OrganisationId		


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
			where s.SiteDBID not in( select SiteDBID from [PatientFlow].[Site] where OrganisationId=@OrganisationId)
					
			end
			
			if(@SystemTypeId=1 or @SystemTypeId = 2 or @SystemTypeId = 6) 
			begin
				update [PatientFlow].[PatientFlowUser]
				set
					Username=@Username,
					[Password]=@Password,
					IPAddress=@IpAddress,
					InternalIPAddress=@InternalIPAddress,
					SupplierId=@SupplierId,
					WebServiceUrl =@WebServiceUrl
				where Organisationid=@OrganisationId
			end
			select @OrganisationId as result  
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
		raiserror('UpdateOrganisationDetails : %d: %s', 16, 1, @error, @message) ;
	rollback transaction
	end catch
end