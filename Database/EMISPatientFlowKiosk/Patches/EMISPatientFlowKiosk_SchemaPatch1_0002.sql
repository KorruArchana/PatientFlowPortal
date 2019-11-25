create type [PatientFlow].[KioskConfiguration] as table 
(
    [ConfigType] varchar(100) not null,
    [KioskId] varchar(50) not null,
    [Value] varchar(4000) not null
);

create type [PatientFlow].[BookedAppointment] as table 
(
    [AppointmentId] int not null,
    [AppointmentDate] datetime null,
    [PatientId] int null,
    [PatientTitle] varchar(100) null,
    [PatientFirstName] varchar(250) null,
    [PatientCallingName] varchar(250) null,
    [PatientFamilyName] varchar(250) null,
    [PatientGender] varchar(20) null,
    [PatientPostCode] varchar(50) null,
    [PatientDOB] varchar(150) null,
    [SessionHolderId] int null,
    [SessionHolderTitle] varchar(100) null,
    [SessionHolderFirstName] varchar(150) null,
    [SessionHolderLastName] varchar(150) null,
    [PatientEmail] varchar(150) null,
    [PatientMobileNumber] varchar(50) null,
    [PatientWorkPhoneNumber] varchar(50) null,
    [PatientHomePhoneNumber] varchar(50) null,
    [ModifiedBy] varchar(50) null,
    primary key clustered ([AppointmentId] asc)
);

create type [PatientFlow].[AnonymousSurvey] as table 
(
    [KioskId] varchar(50) not null,
    [QuestionnaireId] int null,
    [QuestionnaireTitle] varchar(1000) null,
    [QuestionId] int null,
    [QuestionText] varchar(1000) null,
    [OptionId] varchar(50) null,
    [AnswerText] varchar(1000) null
);

create type [PatientFlow].[Translation] as table 
(
    [LanguageId] int not null,
    [TranslationRefId] int not null,
    [TranslationText] nvarchar(2000) null,
    primary key clustered ([LanguageId] asc, [TranslationRefId] asc)
);

create type [PatientFlow].[ScreenControl] as table 
(
    [ControlId] int not null,
    [TranslationRefId] int null
);

create type [PatientFlow].[QuestionOption] as table
(
    [QuestionId] int null,
    [OptionId] int not null,
    [OptionValue] varchar (50) null,
    [NestedQuestionId] int not null,
    [OptionCode] varchar (50) null,
    primary key clustered ([OptionId] asc)
);

create type [PatientFlow].[Questionnaire] as table 
(
    [QuestionnaireId] int not null,
    [Title] varchar(1000) not null,
    [Frequency] int null,
    [CreateConsultation] bit null,
    [IsAnonymous] bit null,
    primary key clustered ([QuestionnaireId] asc)
);

create type [PatientFlow].[Question] as table 
(
    [QuestionId] int not null,
    [QuestionnaireId] int not null,
    [QuestionText] varchar(1000) not null,
    [QuestionType] int null,
    [OptionCharLimit] int null,
    [Gender] varchar(20) null,
    [QuestionOrder] int null,
    [Age1] int null,
    [Age2] int null,
    [Operation] varchar(50) null,
    primary key clustered ([QuestionId] asc)
);

create type [PatientFlow].[MemberAppointmentSession] as table 
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
    [SessionHolderTitle] varchar(100) null,
    [SessionHolderFirstName] varchar(150) null,
    [SessionHolderLastName] varchar(150) null,
    [SlotTypeId] varchar(100) null,
    [SystemType] varchar(100) null,
    primary key clustered ([SessionId] asc)
);

create type [PatientFlow].[ListWithOrder] as table
(
	[Id] int not null,
	[ListOrder] int not null
);

create type [PatientFlow].[KioskModule] as table 
(
    [ModuleId] int not null,
    [ModuleName] varchar(100) not null,
    [ModuleNameToDisplay] varchar(100) not null,
    [TranslationRefId] bigint null
);

create type [PatientFlow].[KioskLanguageOrder] as table 
(
    [LanguageId] int not null,
    [Order] int not null
);