if object_id ('[PatientFlow].[GetKioskDetails]') is not null
drop procedure [PatientFlow].[GetKioskDetails];
go

create procedure [PatientFlow].[GetKioskDetails]
	@KioskId int
as
begin
set nocount on;
set transaction isolation level read committed
    select 
		kiosk.KioskId,
		KioskName,
		PCName,
		[Kiosk].IPAddress,
		[ConnectionStatus] as [Status],
		[Status].StatusName as StatusName,
		Title ,
		ConnectionGuid,
		[KioskPatientMatch].PatientMatchTitle,
		[KioskAppointmentMatch].AppointmentMatchTitle,
		[KioskPatientMatch].ScreenOrder,
		[Kiosk].PatientMatchId,
		[Kiosk].AppointmentMatchId,
		[Kiosk].KioskGuid,
		KioskLogo,		
		AutoConfirmArrival,
		ForceSurvey,
		SkipSurveyQuestion,
		QOFKioskUser,
		ShowDoctorDelay,
		AppointmentReason,
		EarlyArrival,
		LateArrival,
		ScreenTimeOut,
		Kioskversion,
		LastStatusUpdate,
		GeneralMessage,
		ShowDemographicDetails,
		AutoConfirmMultipleArrival,
		ScrambleDemographicDetails,
		DemographicDetailsDuration,
		Kiosk.BannerSpeed,
		kiosk.AdminPassword,
		arrival.AllowUntimed
	from [PatientFlow].Kiosk as Kiosk
	join PatientFlow.KioskArrivalConfiguration arrival on Kiosk.KioskId = arrival.KioskId
	join [PatientFlow].[KioskPatientMatch] as KioskPatientMatch on Kiosk.PatientMatchId=KioskPatientMatch.PatientMatchId
	join [PatientFlow].[KioskAppointmentMatch] as KioskAppointmentMatch on Kiosk.AppointmentMatchId=KioskAppointmentMatch.AppointmentMatchId
	join [PatientFlow].[Status] as [Status] on kiosk.ConnectionStatus=[Status].StatusId
	where Kiosk.KioskId=@KioskId

	--=================Kiosk Language =================================
	select 
		[Language].LanguageId,
		[Language].LanguageCode,
		[Language].LanguageName,
		[Language].TranslationRefId,
		[KioskLanguage].[LanguageOrder]
    from [PatientFlow].[Language] [Language]
	join [PatientFlow].KioskLanguage [KioskLanguage] 
		on [Language].LanguageId=[KioskLanguage].LanguageId
	where [KioskLanguage].KioskId=@KioskId

	--=============== Kiosk Module ================================
	select 
		[Module].ModuleId,
		[Module].ModuleName,
		[Module].TranslationRefId
    from [PatientFlow].[Module] [Module]
	join [PatientFlow].KioskModule [KioskModule] 
		on [Module].ModuleId=[KioskModule].ModuleId
	where [KioskModule].KioskId=@KioskId

	--================== Kiosk Questionnaire ===========================
	select 
		Questionnaire.QuestionnaireId,
		Questionnaire.Title,
		Questionnaire.Frequency,
		Questionnaire.IsAnonymous
	from [PatientFlow].Questionnaire as Questionnaire
	join [PatientFlow].KioskQuestionnaire as KioskQuestionnaire 
		on Questionnaire.QuestionnaireId=KioskQuestionnaire.QuestionnaireId
	where KioskQuestionnaire.KioskId=@KioskId and IsActive = 1
	order by KioskQuestionnaire.QuestionnaireOrder

	--=================== Kiosk Organisation =========================
	select 
		org.OrganisationId,
		OrganisationName,
		[org].OrganisationKey,
		org.SystemTypeId,
		[Kioskmaster].SiteId,
		[site].SiteDBID,
		[site].SiteName,
		Kioskmaster.MainLocation
	from [PatientFlow].[Organisation] org
    join [PatientFlow].KioskLinkedToDetails [Kioskmaster] on org.OrganisationId=TypeId
	left join [PatientFlow].[Site] [site] on [site].SiteId=[Kioskmaster].SiteId
	where [Kioskmaster].KioskId=@KioskId
	order by OrganisationName


	--=============== Kiosk Patient match ======================
	select 
		[KioskPatientMatch].PatientMatchId, 
		[KioskPatientMatch].PatientMatchTitle, 
		[KioskPatientMatch].ScreenOrder 
	from [PatientFlow].[KioskPatientMatch] [KioskPatientMatch]
	join [PatientFlow].Kiosk Kiosk on [KioskPatientMatch].PatientMatchId=Kiosk.PatientMatchId
	where Kiosk.KioskId=@KioskId

	--===================== Kiosk Appointment match ===============
	select 
		[KioskAppointmentMatch].AppointmentMatchId, 
		[KioskAppointmentMatch].AppointmentMatchTitle, 
		[KioskAppointmentMatch].ScreenOrder 
	from [PatientFlow].[KioskAppointmentMatch] [KioskAppointmentMatch]
	join [PatientFlow].Kiosk Kiosk on [KioskAppointmentMatch].AppointmentMatchId=Kiosk.AppointmentMatchId
	where Kiosk.KioskId=@KioskId

	---================== Kiosk SiteList============----------------
    select 
		s.SiteId,
		SiteName,
		s.OrganisationId,
		MainLocation		    
   from [PatientFlow].[Site] as s
   join [PatientFlow].KioskLinkedToDetails [Kioskmaster] on s.OrganisationId=[Kioskmaster].TypeId
   where  [Kioskmaster].KioskId=@KioskId
   order by s.SiteName
   
   
	---================== Kiosk SessionHolder List============----------------
	
	select 
		SessionHolderId,
		OrganisationId
	from PatientFlow.KioskSessionHolder
	where KioskId = @KioskId

   
	---================== Kiosk Demographic List============----------------
   
	select 
		kdt.DemographicDetailsTypeId,
		DemographicDetailsTypeName
	from  [PatientFlow].KioskDemographicDetailsType kdt
	join PatientFlow.DemographicDetailsType ddt on kdt.DemographicDetailsTypeId = ddt.DemographicDetailsTypeId
	where  KioskId=@KioskId 


	---================== Kiosk SiteMap List ============----------------

	select 
		SiteDescription,
		SiteMap,
		KioskSiteMapId  
	from PatientFlow.KioskSiteMap
	where  KioskId=@KioskId  
	order by KioskSiteMapId
	

	---================== Kiosk Linked Sync Service ============----------------

	select distinct ProductKey 
	from PatientFlow.SyncService
	where KioskId = @KioskId
end