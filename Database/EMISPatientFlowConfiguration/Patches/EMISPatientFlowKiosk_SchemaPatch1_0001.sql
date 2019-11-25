create schema [PatientFlow]
go

create table [PatientFlow].[__MigrationHistory] 
(
    [MigrationId] nvarchar(150) not null,
    [ContextKey] nvarchar(300) not null,
    [Model] varbinary(max) not null,
    [ProductVersion] nvarchar(32) not null,
    constraint [PK_PatientFlow.__MigrationHistory] primary key clustered ([MigrationId] asc, [ContextKey] asc)
);

create table [PatientFlow].[AccessType] (
    [AccessTypeId] int not null,
    [Name] varchar(100) null,
    constraint [PK_AccessType] primary key clustered ([AccessTypeId] asc)
);

create table [PatientFlow].[AlertType] 
  ( 
     [AlertTypeId] int not null, 
     [AlertTypeText] varchar (400) not null, 
     constraint [PK_AlertType] primary key clustered ([AlertTypeId] asc) 
  ); 

create table [PatientFlow].[AspNetRoles] 
  ( 
     [Id] nvarchar (128) not null, 
     [Name] nvarchar (256) not null, 
     constraint [PK_PatientFlow.AspNetRoles] primary key clustered ([Id] asc) 
  ); 

go 

create unique nonclustered index [RoleNameIndex] 
  on [PatientFlow].[AspNetRoles]([Name] asc); 

create table [PatientFlow].[AspNetUsers] 
  ( 
     [Id] nvarchar (128) not null, 
     [Email] nvarchar (256) null, 
     [EmailConfirmed] bit not null, 
     [PasswordHash] nvarchar (max) null, 
     [SecurityStamp] nvarchar (max) null, 
     [PhoneNumber] nvarchar (max) null, 
     [PhoneNumberConfirmed] bit not null, 
     [TwoFactorEnabled] bit not null, 
     [LockoutEndDateUtc] datetime null, 
     [LockoutEnabled] bit not null, 
     [AccessFailedCount] int not null, 
     [UserName] nvarchar (256) not null, 
     constraint [PK_PatientFlow.AspNetUsers] primary key clustered ([Id] asc) 
  ); 

go 

create unique nonclustered index [UserNameIndex] 
  on [PatientFlow].[AspNetUsers]([UserName] asc); 
  
create table [PatientFlow].[AuditTrail] 
( 
    [AuditTrailId] bigint identity (1, 1) not null, 
    [AuditEventType] char (1) not null, 
    [TableName] varchar (100) not null, 
    [PrimaryKeyField] varchar (1000) null, 
    [PrimaryKeyValue] varchar (1000) null, 
    [FieldName] varchar (128) null, 
    [NewValue] varchar (2000) null, 
    [OldValue] varchar (2000) null, 
    [UserName] varchar (100) null, 
    [Modified] datetime not null, 
    constraint [PK__AuditTra__41B2DDB32180FB33] primary key clustered ([AuditTrailId] asc)
);
go

execute sp_addextendedproperty 
	@name = N'MS_Description', 
	@value = N'Insert - I, Update - U, Delete - D', 
	@level0type = N'SCHEMA', 
	@level0name = N'PatientFlow', 
	@level1type = N'TABLE', 
	@level1name = N'AuditTrail',
	@level2type = N'COLUMN', 
	@level2name = N'AuditEventType';

create table [PatientFlow].[KioskAppointmentMatch] 
( 
    [AppointmentMatchId] int not null, 
    [AppointmentMatchTitle] varchar (4000) not null, 
    [ScreenOrder] varchar (4000) not null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime null, 
    constraint [PK_KioskAppointmentMatch] primary key clustered ([AppointmentMatchId] asc) 
); 

go
create table [PatientFlow].[KioskPatientMatch] 
( 
    [PatientMatchId] int identity (1, 1) not null, 
    [PatientMatchTitle] varchar (200) not null, 
    [ScreenOrder] varchar (100) not null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime null, 
    constraint [PK_KioskPatientMatch] primary key clustered ([PatientMatchId] asc) 
); 
go
create table [PatientFlow].[LinkTypes] 
( 
    [LinkTypeId] int not null, 
    [LinkType] varchar (100) null, 
    constraint [PK_LinkTypes] primary key clustered ([LinkTypeId] asc) 
); 
go
create table [PatientFlow].[Log] 
( 
    [Id] bigint identity (1, 1) not null, 
    [Date] datetime not null, 
    [Thread] varchar (255) not null, 
    [Level] varchar (50) not null, 
    [Logger] varchar (255) not null, 
    [User] varchar (max) not null, 
    [Message] varchar (4000) not null, 
    [Exception] varchar (2000) null 
); 
go
create table [PatientFlow].[Member] 
( 
    [MemberId]  int identity (1, 1) not null, 
    [Firstname] varchar (100) not null, 
    [DepartmentId] int not null, 
    [Surname] varchar (100) null, 
    [Title] varchar (50) not null, 
    [LoginId] varchar (100) null, 
    [WorkStartTime] smalldatetime null, 
    [WorkEndTime] smalldatetime null, 
    [GPCode] varchar (100) null, 
    [SecurityGroup] varchar (100) null, 
    [StaffCategory] varchar (100) null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime null, 
    [SessionHolderId] int null, 
    [OrganisationId] int null, 
    constraint [PK_Member] primary key clustered ([MemberId] asc) 
); 

create table [PatientFlow].[Organisation]( 
     [OrganisationId] int identity (1, 1) not null, 
     [OrganisationName] varchar (100) null, 
     [SystemTypeId] int null, 
     [IPAddress] varchar (50) null, 
     [SiteNumber] varchar (50) null, 
     [DatabaseName] varchar (50) null, 
     [Modified] datetime null, 
     [ModifiedBy] varchar (50) null, 
     constraint [PK_Organisation] primary key clustered ([OrganisationId] asc) 
); 

create table [PatientFlow].[OrganisationSystemType]( 
	[SystemTypeId] int not null, 
	[SystemType] varchar (50) null, 
	constraint [PK_OrganisationSytemType] primary key clustered ([SystemTypeId] asc) 
); 
go
create table [PatientFlow].[Questionnaire]( 
	[QuestionnaireId] int identity (1, 1) not null, 
	[Title] varchar (300) not null, 
	[Frequency] int null, 
	[CreateConsultation] bit null, 
	[IsAnonymous] bit null, 
	[ModifiedBy] varchar (50) null, 
	[Modified] datetime null, 
	constraint [PK_Questionnaire] primary key clustered ([QuestionnaireId] asc) 
); 


go

create table [PatientFlow].[Report]( 
	[ReportId] int identity (1, 1) not null, 
	[ReportName] varchar (100) not null, 
	[ReportOrder] int not null, 
	constraint [PK_Report] primary key clustered ([ReportId] asc) 
);
go
create table [PatientFlow].[SiteMenu]( 
	[MenuId] int identity (1, 1) not null, 
	[MenuName] varchar (100) not null, 
	[NavigationUrl] varchar (100) null, 
	[ParentMenuId] int null, 
	[NodeTypeId] int null, 
	[NodeId] int null, 
	[ModifiedBy] varchar (50) null, 
	[Modified] datetime null, 
	constraint [PK_SiteMenu] primary key clustered ([MenuId] asc) 
); 
go
create table [PatientFlow].[Status]( 
	[StatusId] int not null, 
	[StatusName] nvarchar (50) null, 
	constraint [PK_Status] primary key clustered ([StatusId] asc) 
); 
go
create table [PatientFlow].[Survey]( 
	[Id] bigint identity (1, 1) not null, 
	[AnswerId] bigint not null, 
	[KioskId] varchar (50) not null, 
	[QuestionnaireId] int null, 
	[QuestionId] int null, 
	[OptionId] varchar (50) null, 
	[AnswerText] nvarchar (1000) null, 
	[ModifiedBy] varchar (50) null, 
	[Modified] datetime null, 
	[QuestionText] varchar (1000) null, 
	[QuestionnaireTitle] varchar (1000) null, 
	constraint [PK_Survey_1] primary key clustered ([Id] asc) 
); 
go

create table [PatientFlow].[TranslationType] ( 
	[TranslationTypeId] int identity(1, 1) not null, 
	[TranslationTypeName] nvarchar(100) not null, 
	constraint [PK_TranslationType] primary key clustered ([TranslationTypeId] asc) 
); 

go

create table [PatientFlow].[AccessTypeMapping] 
(
    [AccessTypeMappingId] int identity(1,1) not null,
    [AccessTypeId] int not null,
    [UserId] nvarchar(128) not null,
    [AllowAdd] bit constraint [DF_AccessTypeMapping_AllowAdd] default ((0)) null,
    [AllowEdit] bit constraint [DF_AccessTypeMapping_AllowEdit] default ((0)) null,
    [AllowDelete] bit constraint [DF_AccessTypeMapping_AllowDelete] default ((0)) null,
    [ModifiedBy] varchar(100) null,
    [Modified] datetime null,
    constraint [PK_AccessTypeMapping] primary key clustered ([AccessTypeMappingId] asc),
    constraint [FK_AccessTypeMapping_AccessType] foreign key ([AccessTypeId]) references [PatientFlow].[AccessType] ([AccessTypeId]),
    constraint [FK_AccessTypeMapping_AspNetUsers] foreign key ([UserId]) references [PatientFlow].[AspNetUsers] ([Id])
);

go

create table [PatientFlow].[Alert] (
    [AlertId] int identity (1,1) not null,
    [AlertType] int not null,
    [AlertText] varchar(4000) not null,
    [Gender] varchar(50) null,
    [Age1] int null,
    [Operation] varchar(50) null,
    [Age2] int null,
    [OrganisationId] int null,
    [ModifiedBy] varchar(50) null,
    [Modified] datetime null,
    constraint [PK_Alerts] primary key clustered ([AlertId] ASC),
    constraint [FK_Alerts_Organisation] foreign key ([OrganisationId]) references [PatientFlow].[Organisation] ([OrganisationId])
);

go

create table [PatientFlow].[AppointmentSlotType] 
  ( 
     [OrganisationId] int not null, 
     [SlotTypeId] int not null, 
     [Description] varchar (100) null, 
     constraint [PK_AppointmentSlotType] primary key clustered ([OrganisationId] asc, [SlotTypeId] asc), 
     constraint [FK_AppointmentSlotType_Organisation] foreign key ([OrganisationId]) references [PatientFlow].[Organisation] ([OrganisationId]) 
  ); 
go
create table [PatientFlow].[AspNetUserClaims] 
  ( 
     [Id] int identity (1, 1) not null, 
     [UserId] nvarchar (128) not null, 
     [ClaimType] nvarchar (max) null, 
     [ClaimValue] nvarchar (max) null, 
     constraint [PK_PatientFlow.AspNetUserClaims] primary key clustered ([Id] asc), 
     constraint [FK_PatientFlow.AspNetUserClaims_PatientFlow.AspNetUsers_UserId] foreign key ([UserId]) references [PatientFlow].[AspNetUsers] ([Id]) on delete cascade 
  ); 

go 

create nonclustered index [IX_UserId] 
  on [PatientFlow].[AspNetUserClaims]([UserId] asc); 

create table [PatientFlow].[AspNetUserLogins] 
  ( 
     [LoginProvider] nvarchar (128) not null, 
     [ProviderKey] nvarchar (128) not null, 
     [UserId] nvarchar (128) not null, 
     constraint [PK_PatientFlow.AspNetUserLogins] primary key clustered ([LoginProvider] asc, [ProviderKey] asc, [UserId] asc), 
     constraint [FK_PatientFlow.AspNetUserLogins_PatientFlow.AspNetUsers_UserId] foreign key ([UserId]) references [PatientFlow].[AspNetUsers] ([Id]) on delete cascade 
  ); 

go 

create nonclustered index [IX_UserId] 
  on [PatientFlow].[AspNetUserLogins]([UserId] asc); 

create table [PatientFlow].[AspNetUserRoles] 
  ( 
     [UserId] nvarchar (128) not null, 
     [RoleId] nvarchar (128) not null, 
     constraint [PK_PatientFlow.AspNetUserRoles] primary key clustered ([UserId] asc, [RoleId] asc), 
     constraint [FK_PatientFlow.AspNetUserRoles_PatientFlow.AspNetRoles_RoleId] foreign key ([RoleId]) references [PatientFlow].[AspNetRoles] ([Id]) on delete cascade, 
     constraint [FK_PatientFlow.AspNetUserRoles_PatientFlow.AspNetUsers_UserId] foreign key ([UserId]) references [PatientFlow].[AspNetUsers] ([Id]) on delete cascade 
  ); 

go 

create nonclustered index [IX_UserId] 
  on [PatientFlow].[AspNetUserRoles]([UserId] asc); 

go 

create nonclustered index [IX_RoleId] 
  on [PatientFlow].[AspNetUserRoles]([RoleId] asc); 

create table [PatientFlow].[Department] 
( 
    [DepartmentId] int identity (1, 1) not null, 
    [DepartmentName] varchar (50) not null, 
    [OrganisationId] int null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime null, 
    constraint [PK_Department] primary key clustered ([DepartmentId] asc), 
    constraint [FK_Department_Organisation] foreign key ([OrganisationId]) references [PatientFlow].[Organisation] ([OrganisationId]) 
); 

go

create table [PatientFlow].[Divert] 
( 
    [DivertId] int identity (1, 1) not null, 
    [DivertMessage] varchar (4000) null, 
    [OrganisationId] int null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime null, 
    constraint [PK_Divert] primary key clustered ([DivertId] asc) 
); 
go
create table [PatientFlow].[Kiosk] 
  ( 
     [KioskId] int identity (1, 1) not null, 
     [KioskName] varchar (50) null, 
     [ConnectionGuid] varchar (50) null, 
     [PCName] varchar (50) null, 
     [IPAddress] varchar (50) null, 
     [ConnectionStatus] int null, 
     [Title] varchar (50) null, 
     [PatientMatchId] int not null, 
     [KioskStatus] int null, 
     [KioskGuid] uniqueidentifier null, 
     [ModifiedBy] varchar (50) null, 
     [Modified] datetime constraint [DF_Kiosk_Modified] default (getdate()) null, 
     [KioskLogo] varbinary (max) null, 
     [ShowDateTime] bit null, 
     [DisplayLanguageFlag] bit null, 
     [EarlyArrival] int null, 
     [LateArrival] int null, 
     [LinkTypeId] int null, 
     [ScreenTimeOut] int null, 
     [WakeUpTime] smalldatetime null, 
     [SleepTime] smalldatetime null, 
     [AppointmentMatchId] int null, 
     [DelayInMinutes] int null, 
     [GeneralMessage] varchar (400) null, 
     [ShowNewsletter] bit null, 
     constraint [PK_Kiosk] primary key clustered ([KioskId] asc), 
     constraint [FK_Kiosk_KioskAppointmentMatch] foreign key ([AppointmentMatchId]) references [PatientFlow].[KioskAppointmentMatch] ([AppointmentMatchId]), 
     constraint [FK_Kiosk_KioskPatientMatch] foreign key ([PatientMatchId]) references [PatientFlow].[KioskPatientMatch] ([PatientMatchId]) 
  ); 

go 

alter table [PatientFlow].[Kiosk] 
  nocheck constraint [FK_Kiosk_KioskAppointmentMatch]; 
  
 create table [PatientFlow].[KioskLinkedToDetails] 
( 
    [Id] int identity (1, 1) not null, 
    [kioskId] int not null, 
    [TypeId] int not null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime null, 
    constraint [PK_KioskLinkedToDetails] primary key clustered ([Id] asc), 
    constraint [FK_KioskLinkedToDetails_Kiosk] foreign key ([TypeId]) references [PatientFlow].[Organisation] ([OrganisationId]) 
); 
go

create table [PatientFlow].[KioskQuestionnaire] 
  ( 
     [KioskId] int not null, 
     [QuestionnaireId] int not null, 
     [QuestionnaireOrder] int null, 
     [ModifiedBy] varchar (50) null, 
     [Modified] datetime null, 
     constraint [PK_KioskQuestionnaire] primary key clustered ([KioskId] asc, [QuestionnaireId] asc), 
     constraint [FK_KioskQuestionnaire_Questionnaire] foreign key ( [QuestionnaireId]) references [PatientFlow].[Questionnaire] ([QuestionnaireId]) 
  ); 


go

create table [PatientFlow].[OrganisationAccessMapping]( 
	[AccessMappingId] int identity (1, 1) not null, 
	[UserId] nvarchar (128) not null, 
	[MenuId] int not null, 
	[Modified] datetime null, 
	[ModifiedBy] varchar (50) null, 
	constraint [PK_OrganisationAccessMapping] primary key clustered ( [AccessMappingId] asc), 
	constraint [FK_OrganisationAccessMapping_AspNetUsers] foreign key ([UserId]) references [PatientFlow].[AspNetUsers] ([Id]), 
	constraint [FK_OrganisationAccessMapping_SiteMenu] foreign key ([MenuId]) references [PatientFlow].[SiteMenu] ([MenuId]) 
); 

create table [PatientFlow].[Patient]( 
	[Id] int identity (1, 1) not null, 
	[PatientId] int not null, 
	[OrganisationId] int not null, 
	[Firstname] varchar (100) not null, 
	[Surname] varchar (100) null, 
	[DOB] date null, 
	[Message] varchar (4000) null, 
	[ModifiedBy] varchar (100) null, 
	[Modified] datetime null, 
	constraint [PK_Patient] primary key clustered ([Id] asc), 
	constraint [FK_Patient_Organisation] foreign key ([OrganisationId]) references [PatientFlow].[Organisation] ([OrganisationId]) 
); 

create table [PatientFlow].[PatientFlowUser]( 
	[UserId] nvarchar (128) not null, 
	[UserName] varchar (150) not null, 
	[Password] varchar (150) not null, 
	[SupplierId] varchar (150) not null, 
	[OrganisationId] int not null, 
	[IPAddress] varchar (50) not null, 
	[DatabaseName] varchar (50) null, 
	[ModifiedBy] varchar (50) null, 
	[Modified] datetime null, 
	constraint [PK_WebUser] primary key clustered ([UserId] asc), 
	constraint [FK_PatientFlowUser_Organisation] foreign key ([OrganisationId])	references [PatientFlow].[Organisation] ([OrganisationId]) 
); 

create table [PatientFlow].[Question]( 
	[QuestionId] int identity (1, 1) not null, 
	[QuestionnaireId] int not null, 
	[QuestionText] varchar (1000) not null, 
	[QuestionCode] varchar (20) null, 
	[AgeCriteria] int null, 
	[QuestionType] int null, 
	[OptionCharLimit] int null, 
	[Gender] varchar (20) null, 
	[QuestionOrder] int null, 
	[Age1] int null, 
	[Age2] int null, 
	[Operation] varchar (50) null, 
	[ModifiedBy] varchar (50) null, 
	[Modified] datetime null, 
	constraint [PK_Question] primary key clustered ([QuestionId] asc), 
	constraint [FK_Question_Questionnaire] foreign key ([QuestionnaireId]) 
	references [PatientFlow].[Questionnaire] ([QuestionnaireId]) 
  ); 
  
create table [PatientFlow].[SyncService]( 
     [SyncServiceId]int identity (1, 1) not null, 
     [ProductKey] uniqueidentifier not null, 
     [OrganisationId] int not null, 
     [SyncConnectionId] uniqueidentifier null, 
     [OrgConnectionId] uniqueidentifier null, 
     [IsActivated] bit constraint [DF_SyncService_IsActive] default ((1)) not null,
     [ModifiedBy] varchar (50) null, 
     [Modified] datetime null, 
     constraint [PK_PatientFlow.SyncService] primary key clustered ([SyncServiceId] asc), 
     constraint [FK_SyncService_Organisation] foreign key ([OrganisationId]) references [PatientFlow].[Organisation] ([OrganisationId]) 
); 

create table [PatientFlow].[TranslationRef]( 
	[TranslationRefId] bigint identity (1, 1) not null, 
	[TranslationTypeId] int not null, 
	constraint [PK_TranslationRef] primary key clustered ([TranslationRefId] asc), 
	constraint [FK_TranslationRef_TranslationType] foreign key ([TranslationTypeId]) references [PatientFlow].[TranslationType] ([TranslationTypeId]) 
); 

go

create table [PatientFlow].[AlertLinkToOrganisation] 
  ( 
     [Id] int identity (1, 1) not null, 
     [AlertId] int not null, 
     [OrganisationId] int not null, 
     [Modified] datetime null, 
     [ModifiedBy] varchar (50) null, 
     constraint [PK_AlertLinkToOrganisation] primary key clustered ([Id] asc), 
     constraint [FK_AlertLinkToOrganisation_Alert] foreign key ([AlertId]) 
     references [PatientFlow].[Alert] ([AlertId]), 
     constraint [FK_AlertLinkToOrganisation_Organisation] foreign key ([OrganisationId]) references [PatientFlow].[Organisation] ([OrganisationId] ) 
  ); 

  go
  
create table [PatientFlow].[AlertsLinkedToDepMem] 
( 
 [Id] int identity (1, 1) not null, 
 [AlertId] int not null, 
 [LinkTypeId] int not null, 
 [TypeId] int not null, 
 [ModifiedBy] varchar (50) null, 
 [Modified] datetime null, 
 constraint [PK_AlertsLinkedToDepMem] primary key clustered ([Id] asc), 
 constraint [FK_AlertsLinkedToDepMem_Alert] foreign key ([AlertId]) references [PatientFlow].[Alert] ([AlertId]), 
 constraint [FK_AlertsLinkedToDepMem_LinkTypes] foreign key ([LinkTypeId]) references [PatientFlow].[LinkTypes] ([LinkTypeId]) 
); 

go

create table [PatientFlow].[DivertLinkedToDetail] 
( 
    [DivertMappingId] int identity (1, 1) not null, 
    [DivertId] int null, 
    [LinkTypeId] int null, 
    [TypeId] int null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime null, 
    constraint [PK_DivertLinkedToDetails] primary key clustered ([DivertMappingId] asc), 
    constraint [FK_DivertLinkedToDetails_Divert] foreign key ([DivertId]) references [PatientFlow].[Divert] ([DivertId]), 
    constraint [FK_DivertLinkedToDetails_DivertLinkedToDetails] foreign key ([DivertMappingId]) references [PatientFlow].[DivertLinkedToDetail] ( 
    [DivertMappingId]) 
); 

go

create table [PatientFlow].[KioskLinkedToDepMemDetails] 
( 
    [Id] int identity (1, 1) not null, 
    [KioskId] int not null, 
    [LinkTypeId] int not null, 
    [TypeId] int not null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime null, 
    constraint [PK_KioskLinkedToDepMemDetails] primary key clustered ([Id] asc), 
    constraint [FK_KioskLinkedToDepMemDetails_Kiosk] foreign key ([KioskId]) references [PatientFlow].[Kiosk] ([KioskId]), 
    constraint [FK_KioskLinkedToDepMemDetails_LinkTypes] foreign key ( [LinkTypeId]) references [PatientFlow].[LinkTypes] ([LinkTypeId]) 
); 
go

create table [PatientFlow].[KioskScreen] 
( 
    [ScreenId] int identity (1, 1) not null, 
    [ScreenCode] varchar (10) not null, 
    [ScreenName] varchar (100) not null, 
    [IsConfigurable] bit constraint [DF_KioskScreen_IsConfigurable] default ((0)) not null, 
    [TranslationRefId] bigint null, 
    constraint [PK_KioskScreen] primary key clustered ([ScreenId] asc), 
    constraint [FK_KioskScreen_TranslationRef] foreign key ([TranslationRefId]) references [PatientFlow].[TranslationRef] ([TranslationRefId]) 
); 
go
create table [PatientFlow].[KioskSlotType] 
( 
    [KioskId] int not null, 
    [SlotTypeId] int not null, 
    [OrganisationId] int not null, 
    constraint [PK_KioskSlotType] primary key clustered ([KioskId] asc, [SlotTypeId] asc, [OrganisationId] asc), 
    constraint [FK_KioskSlotType_Kiosk] foreign key ([KioskId]) references [PatientFlow].[Kiosk] ([KioskId]), 
    constraint [FK_KioskSlotType_Organisation] foreign key ([OrganisationId]) references [PatientFlow].[Organisation] ([OrganisationId]) 
); 
go
create table [PatientFlow].[Language] 
( 
    [LanguageId] int identity (1, 1) not null, 
    [LanguageCode] varchar (5) not null, 
    [LanguageName] varchar (250) not null, 
    [TranslationRefId] bigint null, 
    constraint [PK_Language] primary key clustered ([LanguageId] asc), 
    constraint [FK_Language_TranslationRef] foreign key ([TranslationRefId]) references [PatientFlow].[TranslationRef] ([TranslationRefId]) 
); 
go
create table [PatientFlow].[Module] 
( 
    [ModuleId] int not null, 
    [ModuleName] varchar (100) not null, 
    [TranslationRefId] bigint null, 
    constraint [PK_Module] primary key clustered ([ModuleId] asc), 
    constraint [FK_Module_TranslationRef] foreign key ([TranslationRefId]) references [PatientFlow].[TranslationRef] ([TranslationRefId]) 
); 
go
create table [PatientFlow].[QuestionAnswerOptions]( 
	[OptionId] int identity (1, 1) not null, 
	[QuestionId] int null, 
	[OptionValue] varchar (200) null, 
	[NestedQuestionId] int null, 
	[OptionCode] varchar (50) null, 
	[ModifiedBy] varchar (50) null, 
	[Modified] datetime null, 
	constraint [PK_QuestionAnswerOptions] primary key clustered ([OptionId] asc), 
	constraint [FK_QuestionAnswerOptions_Question] foreign key ([QuestionId]) references [PatientFlow].[Question] ([QuestionId]) 
  ); 

go

create table [PatientFlow].[KioskLanguage] 
( 
    [KioskId] int not null, 
    [LanguageId] int not null, 
    [LanguageOrder] int null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime constraint [DF_KioskLanguage_Modified] default ( getdate()) null, 
    constraint [PK_KioskLanguage] primary key clustered ([KioskId] asc, [LanguageId] asc), 
    constraint [FK_KioskLanguage_Language] foreign key ([LanguageId]) references [PatientFlow].[Language] ([LanguageId]) 
); 


go

create table [PatientFlow].[KioskModule] 
( 
    [KioskId] int not null, 
    [ModuleId] int not null, 
    [ModifiedBy] varchar (50) null, 
    [Modified] datetime constraint [DF_KioskModule_Modified] default (getdate()) null, 
    constraint [PK_KioskModule] primary key clustered ([KioskId] asc, [ModuleId] asc), 
    constraint [FK_KioskModule_Kiosk] foreign key ([KioskId]) references [PatientFlow].[Kiosk] ([KioskId]), 
    constraint [FK_KioskModule_Module] foreign key ([ModuleId]) references [PatientFlow].[Module] ([ModuleId]) 
); 


go

create table [PatientFlow].[KioskScreenControl] 
( 
    [ControlId] int identity (1, 1) not null, 
    [ScreenId] int not null, 
    [UniqueId] varchar (50) not null, 
    [ControlLabel] nvarchar (2000) not null, 
    [TranslationRefId] bigint null, 
    [Modified] datetime null, 
    [ModifiedBy] varchar (100) null, 
    constraint [PK_KioskScreenControl] primary key clustered ([ControlId] asc), 
    constraint [FK_KioskScreenControl_KioskScreen] foreign key ([ScreenId]) references [PatientFlow].[KioskScreen] ([ScreenId]), 
    constraint [FK_KioskScreenControl_TranslationRef] foreign key ([TranslationRefId]) references [PatientFlow].[TranslationRef] ([TranslationRefId]), 
    constraint [UQ_ScreenControl] unique nonclustered ([ScreenId] asc, [UniqueId] asc) 
); 
go

create table [PatientFlow].[Translation]( 
	[LanguageId] int not null, 
	[TranslationRefId] bigint not null, 
	[TranslationText] nvarchar (2000) not null, 
	[ModifiedBy] varchar (50) null, 
	[Modified] datetime null, 
	constraint [PK_Translation] primary key clustered ([LanguageId] asc,[TranslationRefId] asc), 
	constraint [FK_Translation_Language] foreign key ([LanguageId]) references [PatientFlow].[Language] ([LanguageId]), 
	constraint [FK_Translation_TranslationRef] foreign key ([TranslationRefId]) references [PatientFlow].[TranslationRef] ([TranslationRefId]) 
); 
