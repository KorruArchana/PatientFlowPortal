
drop type [PatientFlow].[Questionnaire] 

create type [PatientFlow].[Questionnaire] as table(
	[QuestionnaireId] [int] not null,
	[Title] [varchar](1000) not null,
	[Frequency] [int] null,
	[CreateConsultation] [bit] null,
	[IsAnonymous] [bit] null,
	[OrganisationId] [int] not null
)

alter table [PatientFlow].[Question] drop constraint FK_PatientFlow_Question_QuestionnaireId
alter table [PatientFlow].[KioskQuestionnaire] drop constraint FK_PatientFlow_KioskQuestionnaire_QuestionnaireId
alter table [PatientFlow].[NonAnonymousSurveyFrequency] drop constraint FK_PatientFlow_NonAnonymousSurveyFrequency_QuestionnaireId

truncate table [PatientFlow].[QuestionAnswerOptions]
truncate table [PatientFlow].[Question]
truncate table [PatientFlow].[KioskQuestionnaire]
truncate table [PatientFlow].[NonAnonymousSurveyFrequency]
truncate table [PatientFlow].[Questionnaire]

alter table [PatientFlow].[Question] add constraint FK_PatientFlow_Question_QuestionnaireId 
foreign key(QuestionnaireId) references [PatientFlow].[Questionnaire](QuestionnaireId)

alter table [PatientFlow].[KioskQuestionnaire] add constraint FK_PatientFlow_KioskQuestionnaire_QuestionnaireId
foreign key(QuestionnaireId) references [PatientFlow].[Questionnaire](QuestionnaireId)

alter table [PatientFlow].[NonAnonymousSurveyFrequency] add constraint FK_PatientFlow_NonAnonymousSurveyFrequency_QuestionnaireId
foreign key(QuestionnaireId) references [PatientFlow].[Questionnaire](QuestionnaireId)


alter table [PatientFlow].[Questionnaire] add [OrganisationId] [int] not null 
alter table [PatientFlow].[Questionnaire]
add constraint FK_PatientFlow_Questionnaire_OrganisationId foreign key(OrganisationId) references [PatientFlow].[Organisation](OrganisationId)


truncate table [patientFlow].[SynchronisationLog]
alter table [patientFlow].[SynchronisationLog]
add ProductKey uniqueidentifier not null

alter table [patientFlow].[SynchronisationLog] drop constraint PK_PatientFlow_SynchronisationLog_SyncType
alter table [PatientFlow].[SynchronisationLog] add constraint [PK_PatientFlow_SynchronisationLog_SyncType_ProductKey]
primary  key (SyncType asc, ProductKey)