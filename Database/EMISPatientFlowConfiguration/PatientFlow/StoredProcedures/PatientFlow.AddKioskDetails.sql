if object_id('[PatientFlow].[AddKioskDetails]') is not null
	drop procedure [PatientFlow].[AddKioskDetails];
go

create procedure [PatientFlow].[AddKioskDetails] 
	@KioskName varchar(50),
	@PCName varchar(50) = null,
	@IPAddress varchar(50) = null,
	@Title varchar(50),
	@Organisations as [PatientFlow].[ListWithOrder] readonly,
	@Organisationsite as [PatientFlow].[KioskLinkOrganisationSites] readonly,
	@PatientMatchId int,
	@AppointmentMatchId int,
	@Languages as [PatientFlow].[ListWithOrder] readonly,
	@ModuleIdList as [PatientFlow].[List] readonly,
	@KioskLogo varbinary(max) = null,
	@EarlyArrival int,
	@LateArrival int,
	@ScreenTimeOut int,
	@AutoConfirmArrival bit,
	@AutoConfirmMultipleArrival bit,
	@ShowDoctorDelay bit,
	@AllowUntimed bit,
	@ForceSurvey bit,
	@SkipSurveyQuestion bit,
	@QOFKioskUser bit,
	@AppointmentReason bit = null,
	@ModifiedBy varchar(50),
	@GeneralMessage varchar(400) = null,
	@BannerSpeed int,
	@ShowDemographicDetails bit,
	@ScrambleDemographicDetails bit,
	@DemographicDetailsDuration int,
	@AdminPassword varchar(50) = null,
	@SlotTypeList as [PatientFlow].[KioskLinkedFields] readonly ,
	@KioskSessionHolderList as [PatientFlow].[KioskLinkedFields] readonly,
	@DemographicDetailsList as [PatientFlow].[List] readonly,
	@SiteMapList as PatientFlow.KioskSiteMapList readonly as

begin
	set nocount on;
	set transaction isolation level read committed

	begin try
	begin transaction

		insert into Patientflow.Kiosk (
			KioskName,
			PCName,
			IPAddress,
			[ConnectionStatus],
			Title,
			PatientMatchId,
			AppointmentMatchId,
			KioskStatus,
			KioskGuid,
			KioskLogo,			
			AppointmentReason,
			ScreenTimeOut,
			GeneralMessage,
			BannerSpeed,
			AdminPassword,
			ModifiedBy,
			Modified
			)
		values (
			@KioskName,
			@PCName,
			@IPAddress,
			4,
			@Title,
			@PatientMatchId,
			@AppointmentMatchId,
			1,
			newid(),
			@KioskLogo,			
			@AppointmentReason,
			@ScreenTimeOut,
			@GeneralMessage,
			@BannerSpeed,
			@AdminPassword,
			@ModifiedBy,
			getdate()
			);

		declare @KioskId int
		set @KioskId = 	(select cast(scope_identity() as int));
				
		insert PatientFlow.KioskArrivalConfiguration 
		(
			KioskId,
			EarlyArrival,
			LateArrival,
			AutoConfirmArrival,
			AutoConfirmMultipleArrival,
			ShowDemographicDetails,
			DemographicDetailsDuration,
			ScrambleDemographicDetails,
			QOFKioskUser,
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
			@AutoConfirmMultipleArrival,
			@ShowDemographicDetails,
			@DemographicDetailsDuration,
			@ScrambleDemographicDetails,
			@QOFKioskUser,
			@ShowDoctorDelay,
			@ForceSurvey,
			@SkipSurveyQuestion,
			@AllowUntimed
		);

		insert into Patientflow.KioskLanguage (
			KioskId,
			LanguageId,
			LanguageOrder
			) 
		select 
			@KioskId,
			Id,
			ListOrder 
		from @Languages;

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

		insert into Patientflow.KioskModule (
			KioskId,
			ModuleId
			) 
		select 
			@KioskId,
			Id 
		from @ModuleIdList;

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
		
		insert into Patientflow.KioskQuestionnaire 
			(
				KioskId,
				QuestionnaireId
			)			
		select 
			@KioskId,
			QuestionnaireId 
		from PatientFlow.Questionnaire q
		join @Organisations org on q.OrganisationId = org.Id
	    where IsActive = 1

		insert into Patientflow.KioskLinkedToDetails (
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
		
		insert into PatientFlow.KioskDemographicDetailsType (
			kioskid,
			DemographicDetailsTypeId
			)
		select 
			@KioskId,
			Id
		from @DemographicDetailsList;
		
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
		
		select @KioskId as KioskId
		
		declare @list as PatientFlow.List
		insert into @list select id from @Organisations
		
		declare @syncid uniqueidentifier = newid()
		exec [PatientFlow].[SaveSyncServiceOrganisations] @list, @syncid, true, @ModifiedBy, @KioskId
		
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
		raiserror('AddKioskDetails : %d: %s', 16, 1, @error, @message);
		rollback transaction
	end catch
end
