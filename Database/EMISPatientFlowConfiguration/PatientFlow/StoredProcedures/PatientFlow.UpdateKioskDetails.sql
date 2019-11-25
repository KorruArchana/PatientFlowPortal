if object_id('[PatientFlow].[UpdateKioskDetails]') is not null
	drop procedure [PatientFlow].[UpdateKioskDetails]
go

create procedure [PatientFlow].[UpdateKioskDetails] 
	@KioskId int,
	@PatientMatchId int,
	@AppointmentMatchId int,
	@ModuleIdList as [PatientFlow].[List] readonly,
	@Status int,
	@Languages as [PatientFlow].[ListWithOrder] readonly,
	@KioskName varchar(100),
	@PcName varchar(200) = null,
	@Organisations as [PatientFlow].[ListWithOrder] readonly,
	@Organisationsite as [PatientFlow].[KioskLinkOrganisationSites] readonly,
	@KioskLogo varbinary(max) = null,	
	@AutoConfirmArrival bit,
	@ShowDoctorDelay bit,
	@AllowUntimed bit,
	@ForceSurvey bit,
	@SkipSurveyQuestion bit,
	@QOFKioskUser bit,
	@AppointmentReason bit = null,
	@EarlyArrival int,
	@LateArrival int,
	@ScreenTimeOut int,
	@Title varchar(200),
	@ShowDemographicDetails bit,
	@AdminPassword varchar(50) = null,
	@ModifiedBy varchar(50),
	@DemographicDetailsDuration int,
	@ScrambleDemographicDetails bit,
	@AutoConfirmMultipleArrival bit,
	@SlotTypeList as [PatientFlow].[KioskLinkedFields] readonly,
	@KioskSessionHolderList as [PatientFlow].[KioskLinkedFields] readonly,
	@DemographicDetailsList as [PatientFlow].[List] readonly,
	@SiteMapList as PatientFlow.KioskSiteMapList readonly as

begin
	set nocount on;
	set transaction isolation level read committed

	begin try
		begin transaction

		update [PatientFlow].[Kiosk]
		set PatientMatchId = @PatientMatchId,
			KioskName = @KioskName,
			AppointmentMatchId = @AppointmentMatchId,
			KioskLogo = @KioskLogo,			
			AppointmentReason = @AppointmentReason,
			ScreenTimeOut = @ScreenTimeOut,
			Title = @Title,
			ModifiedBy = @ModifiedBy,
			Modified = getdate(),
			AdminPassword = @AdminPassword
		where KioskId = @KioskId;

		if (@Status = 0 or @Status = 1)
		begin
			update [PatientFlow].Kiosk
			set [ConnectionStatus] = @Status,
				KioskStatus = @Status
			where KioskId = @KioskId
		end
		
		if(@PcName is not null)
		begin
			update [PatientFlow].[Kiosk]
			set PCName =@PcName
			where KioskId = @KioskId; 
		end

		delete
		from Patientflow.KioskModule
		where KioskId = @KioskId

		delete
		from PatientFlow.KioskArrivalConfiguration
		where KioskId = @KioskId

		insert PatientFlow.KioskArrivalConfiguration 
		(
			KioskId,
			EarlyArrival,
			LateArrival,
			AutoConfirmArrival,
			ShowDemographicDetails,
			DemographicDetailsDuration,
			QOFKioskUser,
			AutoConfirmMultipleArrival,
			ScrambleDemographicDetails,
			ShowDoctorDelay,
			ForceSurvey,
			SkipSurveyQuestion,
			AllowUntimed
			)
		values 
		(
			@KioskId,
			@EarlyArrival,
			@LateArrival,
			@AutoConfirmArrival,
			@ShowDemographicDetails,
			@DemographicDetailsDuration,
			@QOFKioskUser,
			@AutoConfirmMultipleArrival,
			@ScrambleDemographicDetails,
			@ShowDoctorDelay,
			@ForceSurvey,
			@SkipSurveyQuestion,
			@AllowUntimed
			);

		insert into Patientflow.KioskModule 
		(
			KioskId,
			ModuleId
			) 			
		select 
			@KioskId,
			Id 
		from @ModuleIdList
			

		delete
		from Patientflow.KioskLanguage
		where KioskId = @KioskId

		insert into Patientflow.KioskLanguage 
		(
			KioskId,
			LanguageId,
			LanguageOrder
			) 
		select 
			@KioskId,
			Id,
			ListOrder 
		from @Languages

		delete
		from Patientflow.KioskSessionHolder
		where KioskId = @KioskId;

		insert into Patientflow.KioskSessionHolder (
			KioskId,
			OrganisationId,
			SessionHolderId
			)
		select 
			@KioskId,
			OrganisationId,
			FieldId 
		from @KioskSessionHolderList
			
		delete
		from Patientflow.KioskSiteMap
		where KioskId = @KioskId;

		insert into Patientflow.KioskSiteMap (
			KioskId,
			SiteDescription,
			SiteMap
			) 	
		select 
			@KioskId,
			s.SiteDescription,
			s.SiteMap
		from @SiteMapList  s 
		
		delete
		from Patientflow.KioskDemographicDetailsType
		where KioskId = @KioskId;

		insert into PatientFlow.KioskDemographicDetailsType 
		(
			kioskid,
			DemographicDetailsTypeId
			)
		select 
			@KioskId,
			Id
		from @DemographicDetailsList;

		delete
		from Patientflow.KioskLinkedToDetails
		where KioskId = @KioskId

		insert into Patientflow.KioskLinkedToDetails 
		(
			kioskid,
			TypeId,
			SiteId,
			MainLocation
			) 
		select 
			@KioskId,
			s.OrganisationId,
			case 
				when s.SiteId = 0
					then null
				else s.SiteId
				end as SiteId,
			MainLocation
			from @Organisationsite as s;
				
		--=========================================================================
		delete
		from Patientflow.KioskSlotType
		where KioskId = @KioskId

		insert into Patientflow.KioskSlotType (
			KioskId,
			OrganisationId,
			SlotTypeId
			)
		select 
			@KioskId,
			OrganisationId,
			FieldId 
		from @SlotTypeList;
		
		--=========================================================================
		select @KioskId as KioskId		
		
		declare @SyncProductKey as uniqueidentifier 
		
		select 
			@SyncProductKey = ProductKey  
		from PatientFlow.SyncService 
		where KioskId=@KioskId		
		
		declare @list as PatientFlow.List
		insert into @list select id from @Organisations
		exec PatientFlow.SaveSyncServiceOrganisations @list, @SyncProductKey, true, @ModifiedBy , @KioskId  
		
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
		raiserror ('UpdateKioskDetails : %d: %s', 16, 1, @error, @message);
		rollback transaction
	end catch
end
