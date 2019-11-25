insert into [PatientFlow].[KioskScreen](ScreenId, ScreenCode, ScreenName, IsConfigurable)
values(26,'P_DOB_YEAR', 'Patient Matching - Select DOB Year', 0)
insert into [PatientFlow].[KioskScreenControl](ControlId, ScreenId, UniqueId, ControlLabel)
values(72,26,'YEAR_LBL_TITLE', 'Please select your year of birth')
insert into [PatientFlow].[KioskScreenControl](ControlId, ScreenId, UniqueId, ControlLabel)
values(73, 26,'NEXT_BTN_TEXT', 'Next')
alter table patientFlow.log add constraint PK_PatientFlow_Log_Id primary key (Id)
alter table PatientFlow.AppointmentStatus add constraint 
UQ_PatientFlow_AppointmentStatus_AppointmentStatusDesc unique(AppointmentStatusDesc)
alter table PatientFlow.AnonymousSurvey alter column AnswerText varchar(1000)
alter table PatientFlow.AppointmentSession alter column EndTime varchar(100) not null
alter table PatientFlow.AppointmentSession alter column ModifiedBy varchar(50)
alter table PatientFlow.AppointmentSession alter column SlotTypeId varchar(100)
alter table PatientFlow.AppointmentSession alter column StartTime varchar(100) not null
alter table PatientFlow.AppointmentSession alter column SystemType varchar(100)
alter table PatientFlow.KioskConfiguration drop constraint UC_ConfigType_KioskID
alter table PatientFlow.KioskConfiguration drop constraint DF_KioskConfiguration_Modified
alter table PatientFlow.KioskConfiguration drop constraint df_kioskid
alter table PatientFlow.KioskConfiguration alter column ConfigType varchar(100) not null
alter table PatientFlow.KioskConfiguration alter column KioskId varchar(50)
alter table PatientFlow.KioskConfiguration alter column ModifiedBy varchar(50)
alter table PatientFlow.KioskConfiguration alter column Value varchar(4000) not null
alter table PatientFlow.KioskConfiguration add constraint UQ_PatientFlow_KioskConfiguration_ConfigType_KioskId 
unique nonclustered(ConfigType asc, KioskId asc)
alter table PatientFlow.KioskConfiguration add constraint DF_PatientFlow_KioskConfiguration_Modified 
default (getdate()) for Modified
alter table PatientFlow.KioskConfiguration add constraint DF_PatientFlow_KioskConfiguration_KioskId 
default ('00.00.00.07') for KioskId
alter table PatientFlow.KioskQuestionnaire drop constraint PK_KioskQuestionnaire
alter table PatientFlow.KioskQuestionnaire alter column KioskGuid varchar(50) not null
alter table PatientFlow.KioskQuestionnaire add constraint PK_PatientFlow_KioskQuestionnaire_KioskGuid_QuestionnaireId
primary key clustered (KioskGuid asc, QuestionnaireId asc)
alter table PatientFlow.KioskScreen alter column ScreenCode varchar(10) not null
alter table PatientFlow.KioskScreen alter column ScreenName varchar(100) not null
alter table PatientFlow.KioskScreenControl alter column ControlLabel varchar(2000) not null
alter table PatientFlow.KioskScreenControl alter column UniqueId varchar(50) not null
alter table PatientFlow.Language alter column LanguageCode varchar(5) not null
alter table PatientFlow.Language alter column LanguageName varchar(250) not null
alter table PatientFlow.Member alter column FirstName varchar(150)
alter table PatientFlow.Member alter column LastName varchar(150)
alter table PatientFlow.Member alter column ModifiedBy varchar(50)
alter table PatientFlow.Member alter column Title varchar(100)
alter table PatientFlow.Patient alter column CallingName varchar(250)
alter table PatientFlow.Patient alter column DOB varchar(150)
alter table PatientFlow.Patient alter column Email varchar(150)
alter table PatientFlow.Patient alter column FamilyName varchar(250)
alter table PatientFlow.Patient alter column FirstName varchar(250)
alter table PatientFlow.Patient alter column Gender varchar(20)
alter table PatientFlow.Patient alter column HomePhoneNumber varchar(50)
alter table PatientFlow.Patient alter column MobileNumber varchar(50)
alter table PatientFlow.Patient alter column ModifiedBy varchar(50)
alter table PatientFlow.Patient alter column PostCode varchar(50)
alter table PatientFlow.Patient alter column Title varchar(100)
alter table PatientFlow.Patient alter column WorkPhoneNumber varchar(50)

exec sp_rename '[PatientFlow].[FK_Appointment_AppointmentStatus]','FK_PatientFlow_Appointment_AppointmentStatusId','object'
exec sp_rename '[PatientFlow].[FK_Appointment_Member]','FK_PatientFlow_Appointment_SessionHolderId','object'
exec sp_rename '[PatientFlow].[FK_Appointment_Patient]','FK_PatientFlow_Appointment_PatientId','object'
exec sp_rename '[PatientFlow].[FK_AppointmentSession_Member]','FK_PatientFlow_AppointmentSession_SessionHolderId','object'
exec sp_rename '[PatientFlow].[FK_KioskScreenControl_KioskScreen]','FK_PatientFlow_KioskScreenControl_ScreenId','object'
exec sp_rename '[PatientFlow].[PK_AppointmentStatus]','PK_PatientFlow_AppointmentStatus_AppointmentStatusId','object'
exec sp_rename '[PatientFlow].[PK__AuditTrail]','PK_PatientFlow_AuditTrail_AuditTrailId','object'
exec sp_rename '[PatientFlow].[PK_KioskConfiguration]','PK_PatientFlow_KioskConfiguration_ConfigId','object'
exec sp_rename '[PatientFlow].[PK_KioskLogo]','PK_PatientFlow_KioskLogo_KioskId','object'
exec sp_rename '[PatientFlow].[PK_Screen]','PK_PatientFlow_KioskScreen_ScreenId','object'
exec sp_rename '[PatientFlow].[PK_Language]','PK_PatientFlow_Language_LanguageId','object'
exec sp_rename '[PatientFlow].[PK__Member]','PK_PatientFlow_Member_MemberId','object'
exec sp_rename '[PatientFlow].[PK_NonAnonymousSurveyFrequency]','PK_PatientFlow_NonAnonymousSurveyFrequency_FrequencyId','object'
exec sp_rename '[PatientFlow].[PK__Patient]','PK_PatientFlow_Patient_PatientId','object'
exec sp_rename '[PatientFlow].[PK_PatientFlowUser]','PK_PatientFlow_PatientFlowUser_UserId','object'
exec sp_rename '[PatientFlow].[PK_Question]','PK_PatientFlow_Question_QuestionId','object'
exec sp_rename '[PatientFlow].[PK_Questionnaire]','PK_PatientFlow_Questionnaire_QuestionnaireId','object'
exec sp_rename '[PatientFlow].[PK_SynchronisationLog]','PK_PatientFlow_SynchronisationLog_SyncType','object'
exec sp_rename '[PatientFlow].[PK_Translation]','PK_PatientFlow_Translation_LanguageId_TranslationRefId','object'
exec sp_rename '[PatientFlow].[PK__Appointment]','PK_PatientFlow_Appointment_AppointmentId','object'
exec sp_rename '[PatientFlow].[PK_AppointmentSession]','PK_PatientFlow_AppointmentSession_SessionId','object'
exec sp_rename '[PatientFlow].[PK_ScreenControl]','PK_PatientFlow_KioskScreenControl_ScreenId_ControlId','object'
exec sp_rename '[PatientFlow].[PK_QuestionAnswerOptions]','PK_PatientFlow_QuestionAnswerOptions_OptionId','object'
exec sp_rename '[PatientFlow].[PK_AnonymousQuestionnaireAnswers]','PK_PatientFlow_AnonymousSurvey_AnswerId','object'
exec sp_rename '[PatientFlow].[DF_AnonymousSurvey_Modified]','DF_PatientFlow_AnonymousSurvey_Modified','object'
exec sp_rename '[PatientFlow].[DF_Member_Modified]','DF_PatientFlow_Member_Modified','object'
exec sp_rename '[PatientFlow].[DF_Patient_Modified]','DF_PatientFlow_Patient_Modified','object'
exec sp_rename '[PatientFlow].[DF_Appointment_Modified]','DF_PatientFlow_Appointment_Modified','object'
exec sp_rename '[PatientFlow].[DF_AppointmentSession_Modified]','DF_PatientFlow_AppointmentSession_Modified','object'