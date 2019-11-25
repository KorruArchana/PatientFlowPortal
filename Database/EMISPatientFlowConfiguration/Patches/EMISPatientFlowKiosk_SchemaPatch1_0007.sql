alter table PatientFlow.Log drop column Id
alter table PatientFlow.Log add LogId int Identity(1,1) constraint PK_PatientFlow_Log_LogId primary key (LogId)
alter table PatientFlow.__MigrationHistory drop constraint PK_PatientFlow___MigrationHistory_MigrationId_ContextKey
alter table PatientFlow.__MigrationHistory alter column ContextKey varchar(300) not null
alter table PatientFlow.__MigrationHistory alter column MigrationId varchar(150) not null
alter table PatientFlow.__MigrationHistory alter column ProductVersion varchar(32) not null
alter table PatientFlow.__MigrationHistory add constraint PK_PatientFlow___MigrationHistory_MigrationId_ContextKey
primary key clustered (MigrationId, ContextKey)
alter table PatientFlow.KioskScreenControl alter column ControlLabel varchar(2000) not null
alter table PatientFlow.Status alter column StatusName varchar(50) null
alter table PatientFlow.Survey alter column AnswerText varchar(1000) null
alter table PatientFlow.TranslationType alter column TranslationTypeName varchar(100) null
alter table PatientFlow.PatientFlowUser drop constraint PK_PatientFlow_PatientFlowUser_UserId
alter table PatientFlow.PatientFlowUser alter column UserId int not null
alter table PatientFlow.PatientFlowUser add constraint PK_PatientFlow_PatientFlowUser_UserId
primary key clustered (UserId)
alter table PatientFlow.AccessType add constraint UQ_PatientFlow_AccessType_Name unique(Name)
alter table PatientFlow.AlertType add constraint UQ_PatientFlow_AlertType_AlertTypeText unique(AlertTypeText)
alter table PatientFlow.LinkTypes add constraint UQ_PatientFlow_LinkTypes_LinkType unique(LinkType)
alter table PatientFlow.OrganisationSystemType 
add constraint UQ_PatientFlow_OrganisationSystemType_SystemType unique(SystemType)
alter table PatientFlow.Status add constraint UQ_PatientFlow_Status_StatusName unique(StatusName)
-- PatientFlow.Survey
alter table PatientFlow.Survey drop constraint PK_PatientFlow_Survey_Id
alter table PatientFlow.Survey drop column Id
alter table PatientFlow.Survey add SurveyId bigint not null constraint PK_PatientFlow_Survey_SurveyId primary key (SurveyId)
exec sp_rename 'PatientFlow.Survey.QuestionnaireId', 'RefQuestionnaireId', 'COLUMN';
exec sp_rename 'PatientFlow.Survey.QuestionId', 'RefQuestionId', 'COLUMN';
exec sp_rename 'PatientFlow.Survey.OptionId', 'RefOptionId', 'COLUMN';
exec sp_rename 'PatientFlow.Survey.AnswerId', 'RefAnswerId', 'COLUMN';

alter table PatientFlow.KioskQuestionnaire add constraint FK_PatientFlow_KioskQuestionnaire_KioskId foreign key (KioskId) references PatientFlow.Kiosk(KioskId);
exec sp_rename 'PatientFlow.KioskLinkedToDetails.kioskId', 'KioskId', 'COLUMN';
alter table PatientFlow.KioskLinkedToDetails add constraint FK_PatientFlow_KioskLinkedToDetails_KioskId foreign key (KioskId) references PatientFlow.Kiosk(KioskId);
alter table PatientFlow.KioskLanguage add constraint FK_PatientFlow_KioskLanguage_KioskId foreign key (KioskId) references PatientFlow.Kiosk(KioskId);
alter table PatientFlow.Member add constraint FK_PatientFlow_Member_OrganisationId foreign key (OrganisationId) references PatientFlow.Organisation(OrganisationId);
exec sp_rename 'PatientFlow.Survey.KioskId', 'RefKioskId', 'COLUMN';
alter table PatientFlow.Divert add constraint FK_PatientFlow_Divert_OrganisationId foreign key (OrganisationId) references PatientFlow.Organisation(OrganisationId);
alter table PatientFlow.DivertLinkedToDetail add constraint FK_PatientFlow_DivertLinkedToDetail_LinkTypeId foreign key (LinkTypeId) references PatientFlow.LinkTypes(LinkTypeId);

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture the migration history of membership provider', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = '__MigrationHistory';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table for type of access', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AccessType';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture the access permissions for users', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AccessTypeMapping';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture all the alerts with conditions', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Alert';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture mapping of alerts to organisation', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AlertLinkToOrganisation';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture mapping of alerts to department and member', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AlertsLinkedToDepMem';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table that captures the alert type', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AlertType';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table that captures the slot types', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AppointmentSlotType';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table that captures the user role', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AspNetRoles';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the claims', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AspNetUserClaims';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the login details', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AspNetUserLogins';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the user roles', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AspNetUserRoles';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the different users', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AspNetUsers';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Records changes made in database', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AuditTrail';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table to hold the departments in organisation', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Department';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures all the diverts', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Divert';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the linked details of diverts', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'DivertLinkedToDetail';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures all the kiosk details', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Kiosk';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table for patient matching criterias for appointment booking', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskAppointmentMatch';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture the languages mapped to kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskLanguage';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Department and member level mapping details of kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskLinkedToDepMemDetails';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the type of linking to kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskLinkedToDetails';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the modules in kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskModule';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table for patient matching criterias for arrival', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskPatientMatch';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture questionaires related to kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskQuestionnaire';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture all screens used in kiosk for language translation', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskScreen';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =  N'Captures the slot types in kiosk',  
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskSlotType';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =  N'Master table that contains the languages',  
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Language';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =  N'Master table that contains the linking levels',  
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'LinkTypes';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Log of all errors and informations related to config app and sync service',   
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Log';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures all the member details',   
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Member';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table that captures all the modules',   
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Module';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures all the organisation details',   
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Organisation';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the organisation mapping details',   
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'OrganisationAccessMapping';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Master table for system type',   
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'OrganisationSystemType';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the patient details and per patient message',   
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Patient';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'User information required for api call', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'PatientFlowUser';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =  N'Capture all question related information', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Question';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =  N'Capture all answer options related to question', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'QuestionAnswerOptions';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Capture all questionnaire related information', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Questionnaire';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Master table to capture the report types', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Report';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Captures the menu and navigation details', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'SiteMenu';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Master table to capture kiosk status', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Status';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Captures all the anonymous survey feedback from kiosk database', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Survey';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Captures the sync service details', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'SyncService';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Captures the language translation details', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Translation';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Captures the language mapping to type of translation', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'TranslationRef';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Captures the type of translation', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'TranslationType';

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value =   N'Master table to hold all kiosk screen controls', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskScreenControl';

update [PatientFlow].[KioskAppointmentMatch]
set AppointmentMatchTitle = 'Full Date of Birth, First Letter of Surname', Screenorder='P_DAY,P_MONTH,P_DOB_YEAR,P_SURNAME'
where appointmentmatchid=1

update [PatientFlow].[KioskAppointmentMatch]
set AppointmentMatchTitle = 'Gender, Full Date of Birth, First Letter of Surname', Screenorder='P_GENDER,P_DAY,P_MONTH,P_DOB_YEAR,P_SURNAME'
where appointmentmatchid=2

delete from [PatientFlow].[KioskAppointmentMatch]
where appointmentmatchid=3 

delete from [PatientFlow].[KioskAppointmentMatch]
where appointmentmatchid=4


update [PatientFlow].[KioskPatientMatch]
set PatientMatchTitle ='First letter of surname, Month of birth, Year of birth', ScreenOrder ='P_SURNAME,P_MONTH,P_YEAR'
where PatientMatchId=6

update [PatientFlow].[KioskPatientMatch]
set ScreenOrder ='P_DAY,P_MONTH,P_DOB_YEAR,P_SURNAME'
where PatientMatchId=5


insert into [PatientFlow].[KioskScreen](ScreenCode, ScreenName, IsConfigurable)
values('P_DOB_YEAR', 'Patient Matching - Select DOB Year', 0)

insert into [PatientFlow].[KioskScreenControl]( ScreenId, UniqueId, ControlLabel)
values(26,'YEAR_LBL_TITLE', 'Please select your year of birth')

insert into [PatientFlow].[KioskScreenControl](ScreenId, UniqueId, ControlLabel)
values(26,'NEXT_BTN_TEXT', 'Next')

insert [PatientFlow].[KioskScreen] ([ScreenCode], [ScreenName], [IsConfigurable], [TranslationRefId]) values (N'P_MESSAGES', N'Arrival Messages', 0, null)

insert [PatientFlow].[KioskScreenControl] ([ScreenId], [UniqueId], [ControlLabel], [TranslationRefId]) values (27, N'MSG_TITLETEXT', N'Messages', null)

insert [PatientFlow].[KioskScreenControl] ([ScreenId], [UniqueId], [ControlLabel], [TranslationRefId]) values (27, N'MSG_BTN_NEXT', N'Next', null)