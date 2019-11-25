create schema [PatientFlow]
go
create table [PatientFlow].[AnonymousSurvey] 
(
    [AnswerId] bigint identity(1,1) not null,
    [KioskId] varchar(50) not null,
    [QuestionnaireId] int null,
    [QuestionnaireTitle] nchar(1000) null,
    [QuestionId] int null,
    [QuestionText] nchar(1000) null,
    [OptionId] varchar(50) null,
    [AnswerText] nvarchar(1000) null,
    [ModifiedBy] varchar(50) null,
    [Modified] datetime       
    constraint [DF_AnonymousSurvey_Modified] default (getdate()) null,
    constraint [PK_AnonymousQuestionnaireAnswers] primary key clustered ([AnswerId] asc)
);
go
create table [PatientFlow].[AppointmentStatus] 
(
    [AppointmentStatusId] tinyint not null,
    [AppointmentStatusDesc] varchar(50) not null,
    constraint [PK_AppointmentStatus] primary key clustered ([AppointmentStatusId] asc)
);
go
create table [PatientFlow].[AuditTrail]
(
    [AuditTrailId] bigint identity (1,1) not null,
    [AuditEventType] char(1) not null,
    [TableName] varchar(100) not null,
    [PrimaryKeyField] varchar(1000) null,
    [PrimaryKeyValue] varchar(1000) null,
    [FieldName] varchar(128)  null,
    [NewValue]  varchar(2000) null,
    [OldValue]  varchar(2000) null,
    [ModifiedBy] varchar(100)  null,
    [Modified] datetime not null,
    constraint [PK__AuditTrail] primary key clustered ([AuditTrailId] asc)
);
go
execute sp_addextendedproperty 
	@name = N'MS_Description', 
	@value = N'insert - I, update - U, delete - D', 
	@level0type = N'SCHEMA', 
	@level0name = N'PatientFlow', 
	@level1type = N'table', 
	@level1name = N'AuditTrail', 
	@level2type = N'COLUMN', 
	@level2name = N'AuditEventType';

create table [PatientFlow].[KioskConfiguration] (
    [ConfigId] int identity (1,1) not null,
    [ConfigType] nvarchar(100) not null,
    [KioskId] nvarchar(50) constraint [df_kioskid] default('00.00.00.07') null,
    [Value] nvarchar(4000) not null,
    [ModifiedBy] nvarchar(50) null,
    [Modified] datetime constraint [DF_KioskConfiguration_Modified] default (getdate()) null,
    constraint [PK_KioskConfiguration] primary key clustered ([ConfigId] asc),
    constraint [UC_ConfigType_KioskID] unique nonclustered ([ConfigType] asc, [KioskId] asc)
);
go

create table [PatientFlow].[KioskLogo] 
(
    [KioskId] varchar(50) not null,
    [Logo] varbinary(max) not null,
    constraint [PK_KioskLogo] primary key clustered ([KioskId] asc)
);
go
create table [PatientFlow].[KioskQuestionnaire] 
(
    [KioskGuid] nvarchar(50) not null,
    [QuestionnaireId] int not null,
    [QuestionnaireOrder] int null,
    [ModifiedBy] varchar(50) null,
    [Modified] datetime null,
    constraint [PK_KioskQuestionnaire] primary key clustered ([KioskGuid] asc, [QuestionnaireId] asc)
);
go
create table [PatientFlow].[KioskScreen] 
(
    [ScreenId] int not null,
    [ScreenCode] nvarchar(10) not null,
    [ScreenName] nvarchar(100) not null,
    [IsConfigurable] bit not null,
    [TranslationRefId] bigint null,
    constraint [PK_Screen] primary key clustered ([ScreenId] asc)
);
go
create table [PatientFlow].[Language] 
(
    [LanguageId] int not null,
    [LanguageCode] nvarchar(5) not null,
    [LanguageName] nvarchar(250) not null,
    [TranslationRefId] bigint null,
    constraint [PK_Language] primary key clustered ([LanguageId] asc)
);
go
create table [PatientFlow].[Log] (
    [Id] bigint identity (1,1) not null,
    [Date] datetime not null,
    [Thread] varchar(255) not null,
    [level] varchar(50) not null,
    [Logger] varchar(255) not null,
    [User] varchar(50) not null,
    [Message] varchar(4000) not null,
    [Exception] varchar(2000) null
);
go
create table [PatientFlow].[Member] 
(
    [MemberId] int not null,
    [Title] nvarchar(100) null,
    [FirstName] nvarchar(150) null,
    [LastName] nvarchar(150) null,
    [ModifiedBy] nvarchar(50) null,
    [Modified] datetime constraint [DF_Member_Modified] default (getdate()) null,
    constraint [PK__Member] primary key clustered ([MemberId] asc)
);

go
create table [PatientFlow].[NonAnonymousSurveyFrequency] 
(
    [FrequencyId] bigint identity(1,1) not null,
    [PatientId] int null,
    [QuestionnaireId] int null,
    [AccessedOn] date null,
    [ModifiedBy] varchar(50) null,
    [Modified] datetime null,
    constraint [PK_NonAnonymousSurveyFrequency] primary key clustered ([FrequencyId] asc)
);
go
create table [PatientFlow].[Patient] 
(
    [PatientId] int not null,
    [Title] nvarchar(100) null,
    [FirstName] nvarchar(250) null,
    [CallingName] nvarchar(250) null,
    [FamilyName] nvarchar(250) null,
    [Gender] nvarchar(20) null,
    [PostCode] nvarchar(50) null,
    [DOB] nvarchar(150) null,
    [Email] nvarchar(150) null,
    [MobileNumber] nvarchar(50) null,
    [WorkPhoneNumber] nvarchar(50) null,
    [HomePhoneNumber] nvarchar(50) null,
    [ModifiedBy] nvarchar(50) null,
    [Modified] datetime constraint [DF_Patient_Modified] default (getdate()) null,
    [IsNewsletterSubscribed] bit null,
    constraint [PK__Patient] primary key clustered ([PatientId] asc)
);

go

create table [PatientFlow].[PatientFlowUser] 
(
    [UserId] int identity(1,1) not null,
    [UserName] varchar(150) not null,
    [Password] varchar(150) not null,
    [SupplierId] varchar(150) not null,
    [OrganisationId] varchar(50) not null,
    [IPAddress] varchar(50)  not null,
    [Type] varchar(15) null,
    [ConfigOrganisationId] int null,
    constraint [PK_PatientFlowUser] primary key clustered ([UserId] asc)
);
go
create table [PatientFlow].[Question] 
(
    [Id] int identity (1,1) not null,
    [QuestionId] int not null,
    [QuestionnaireId] int not null,
    [QuestionText] varchar(1000) not null,
    [QuestionType] int null,
    [OptionCharLimit] int null,
    [Gender] varchar(20) null,
    [QuestionOrder] int null,
    [ModifiedBy] varchar(50) null,
    [Modified] datetime null,
    [Age1] int null,
    [Age2] int null,
    [Operation] varchar(50) null,
    constraint [PK_Question] primary key clustered ([QuestionId] asc)
);
go
create table [PatientFlow].[Questionnaire] 
(
    [Id] int identity (1,1) not null,
    [QuestionnaireId] int not null,
    [Title] varchar (300) not null,
    [Frequency] int null,
    [CreateConsultation] bit null,
    [IsAnonymous] bit null,
    [ModifiedBy] varchar(50) null,
    [Modified] datetime null,
    constraint [PK_Questionnaire] primary key clustered ([QuestionnaireId] asc)
);
go
create table [PatientFlow].[SynchronisationLog] 
(
    [SyncType] smallint not null,
    [LastRowID] bigint not null,
    [Modified] datetime not null,
    constraint [PK_SynchronisationLog] primary key clustered ([SyncType] asc)
);

go
execute sp_addextendedproperty 
	@name = N'MS_Description', 
	@value = N'1 - Log, 2 - Anonymous Survey', 
	@level0type = N'SCHEMA', 
	@level0name = N'PatientFlow', 
	@level1type = N'table', 
	@level1name = N'SynchronisationLog', 
	@level2type = N'COLUMN', 
	@level2name = N'SyncType';

go
execute sp_addextendedproperty 
	@name = N'MS_Description', 
	@value = N'Log =1, Survey = 2', 
	@level0type = N'SCHEMA', 
	@level0name = N'PatientFlow', 
	@level1type = N'table', 
	@level1name = N'SynchronisationLog', 
	@level2type = N'COLUMN', 
	@level2name = N'Modified';
go
create table [PatientFlow].[Translation] 
(
    [LanguageId] int not null,
    [TranslationRefId] bigint not null,
    [TranslationText] nvarchar(2000) not null,
    constraint [PK_Translation] primary key clustered ([LanguageId] asc, [TranslationRefId] asc)
);
go
create table [PatientFlow].[Appointment] 
(
    [AppointmentId] int not null,
    [AppointmentDate] datetime not null,
    [AppointmentStatusId] tinyint null,
    [PatientId] int not null,
    [SessionHolderId] int not null,
    [OrganisationId] varchar(50) null,
    [ModifiedBy] varchar(50) null,
    [Modified] datetime constraint [DF_Appointment_Modified] default (getdate()) null,
    [SystemType] varchar(50) null,
    constraint [PK__Appointment] primary key clustered ([AppointmentId] asc),
    constraint [FK_Appointment_AppointmentStatus] foreign key ([AppointmentStatusId]) 
	references [PatientFlow].[AppointmentStatus] ([AppointmentStatusId]),
    constraint [FK_Appointment_Member] foreign key ([SessionHolderId]) 
	references [PatientFlow].[Member] ([MemberId]),
    constraint [FK_Appointment_Patient] foreign key ([PatientId]) 
	references [PatientFlow].[Patient] ([PatientId])
);

go
create table [PatientFlow].[AppointmentSession] 
(
    [SessionId] int not null,
    [Date] datetime not null,
    [StartTime] nvarchar(100) not null,
    [EndTime] nvarchar(100) not null,
    [SlotLength] int not null,
    [TotalSlots] int not null,
    [BookedSlots] int not null,
    [AvailableSlots] int not null,
    [SessionHolderId] int null,
    [OrganisationId] varchar(50) null,
    [ModifiedBy] nvarchar(50) null,
    [Modified] datetime constraint [DF_AppointmentSession_Modified] default (getdate()) null,
    [SlotTypeId] nvarchar(100) null,
    [SystemType] nvarchar(100) null, 
    constraint [PK_AppointmentSession] primary key clustered ([SessionId] asc),
    constraint [FK_AppointmentSession_Member] foreign key ([SessionHolderId]) references [PatientFlow].[Member] ([MemberId])
);
go
create table [PatientFlow].[KioskScreenControl] 
(
    [ControlId] int not null,
    [ScreenId] int not null,
    [UniqueId] nvarchar(50) not null,
    [ControlLabel] nvarchar(2000) not null,
    [TranslationRefId] bigint null,
    constraint [PK_ScreenControl] primary key clustered ([ScreenId] asc, [ControlId] asc),
    constraint [FK_KioskScreenControl_KioskScreen] foreign key ([ScreenId]) references [PatientFlow].[KioskScreen] ([ScreenId])
);
go
create table [PatientFlow].[QuestionAnswerOptions] 
(
    [Id] int identity (1,1) not null,
    [OptionId] int not null,
    [QuestionId] int null,
    [OptionValue] varchar(200) null,
    [NestedQuestionId] int null,
    [OptionCode] varchar(50) null,
    [ModifiedBy] varchar(50) null,
    [Modified] datetime null,
    constraint [PK_QuestionAnswerOptions] primary key clustered ([OptionId] asc)
);