create table PatientFlow.Organisation
(
       OrganisationId int not null ,
       OrganisationKey varchar(50) not null,
       SystemType varchar(50) not null,
       constraint PK_PatientFlow_Organisation_OrganisationId   primary key clustered (OrganisationId),
       constraint UQ_PatientFlow_Organisation_OrganisationKey  unique nonclustered (OrganisationKey)
);
exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Organisation information for Appointments and sessions', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Organisation'; 
alter table PatientFlow.Appointment drop constraint FK_PatientFlow_Appointment_PatientId
 --PatientFlow.Patient
alter table PatientFlow.Patient add  OrganisationId int not null;
alter table PatientFlow.Patient add constraint FK_PatientFlow_Patient_OrganisationId foreign key (OrganisationId) references PatientFlow.Organisation(OrganisationId);
alter table PatientFlow.Patient drop constraint PK_PatientFlow_Patient_PatientId;
alter table PatientFlow.Patient add constraint PK_PatientFlow_Patient_PatientId_OrganisationId primary key (PatientId,OrganisationId);

-- PatientFlow.Appointment
alter table PatientFlow.Appointment drop column SystemType;
alter table PatientFlow.Appointment alter column OrganisationId int not null;
alter table PatientFlow.Appointment add constraint FK_PatientFlow_Appointment_OrganisationId foreign key (OrganisationId) references PatientFlow.Organisation(OrganisationId);
alter table PatientFlow.Appointment drop constraint PK_PatientFlow_Appointment_AppointmentId;
alter table PatientFlow.Appointment add constraint FK_PatientFlow_Appointment_PatientId_OrganisationId foreign key (PatientId,OrganisationId) references PatientFlow.Patient(PatientId,OrganisationId);
alter table PatientFlow.Appointment add constraint PK_PatientFlow_Appointment_AppointmentId_OrganisationId primary key (AppointmentId,OrganisationId);
-- PatientFlow.AppointmentSession
alter table PatientFlow.AppointmentSession drop column SystemType;
alter table PatientFlow.AppointmentSession alter column OrganisationId int not null;
alter table PatientFlow.AppointmentSession add constraint FK_PatientFlow_AppointmentSession_OrganisationId foreign key (OrganisationId) references PatientFlow.Organisation(OrganisationId);
alter table PatientFlow.AppointmentSession drop constraint PK_PatientFlow_AppointmentSession_SessionId;
alter table PatientFlow.AppointmentSession add constraint PK_PatientFlow_AppointmentSession_SessionId_OrganisationId primary key (SessionId,OrganisationId);
-- PatientFlow.PatientFlowUser
alter table PatientFlow.PatientFlowUser drop column [Type];
alter table PatientFlow.PatientFlowUser drop column ConfigOrganisationId;
alter table PatientFlow.PatientFlowUser alter column OrganisationId int not null;
alter table PatientFlow.PatientFlowUser add constraint FK_PatientFlow_PatientFlowUser_OrganisationId foreign key (OrganisationId) references PatientFlow.Organisation(OrganisationId);
-- PatientFlow.Log
exec sp_rename 'PatientFlow.[Log].Id', 'LogId', 'COLUMN';
alter table PatientFlow.[Log] drop constraint PK_PatientFlow_Log_Id;
alter table PatientFlow.[Log] add constraint PK_PatientFlow_Log_LogId primary key (LogId);
-- PatientFlow.Questionnaire
alter table PatientFlow.Questionnaire drop column Id;
-- PatientFlow.Question
alter table PatientFlow.Question drop column Id
alter table PatientFlow.Question add constraint FK_PatientFlow_Question_QuestionnaireId foreign key (QuestionnaireId) references PatientFlow.Questionnaire(QuestionnaireId);
-- PatientFlow.QuestionAnswerOptions
alter table PatientFlow.QuestionAnswerOptions drop column Id;
-- PatientFlow.KioskQuestionnaire
alter table PatientFlow.KioskQuestionnaire add constraint FK_PatientFlow_KioskQuestionnaire_QuestionnaireId foreign key (QuestionnaireId) references PatientFlow.Questionnaire(QuestionnaireId);
-- PatientFlow.AnonymousSurvey
exec sp_rename 'PatientFlow.AnonymousSurvey.QuestionnaireId', 'RefQuestionnaireId', 'COLUMN';
exec sp_rename 'PatientFlow.AnonymousSurvey.QuestionId', 'RefQuestionId', 'COLUMN';
exec sp_rename 'PatientFlow.AnonymousSurvey.OptionId', 'RefOptionId', 'COLUMN';

-- PatientFlow.NonAnonymousSurveyFrequency
alter table PatientFlow.NonAnonymousSurveyFrequency add constraint FK_PatientFlow_NonAnonymousSurveyFrequency_QuestionnaireId foreign key (QuestionnaireId) references PatientFlow.Questionnaire(QuestionnaireId);
alter table PatientFlow.NonAnonymousSurveyFrequency add OrganisationId int not null;
alter table PatientFlow.NonAnonymousSurveyFrequency add constraint FK_PatientFlow_NonAnonymousSurveyFrequency_OrganisationId foreign key (OrganisationId) references PatientFlow.Organisation(OrganisationId);
alter table PatientFlow.NonAnonymousSurveyFrequency add constraint FK_PatientFlow_NonAnonymousSurveyFrequency_PatientId_OrganisationId foreign key (PatientId,OrganisationId) references PatientFlow.Patient(PatientId,OrganisationId);

