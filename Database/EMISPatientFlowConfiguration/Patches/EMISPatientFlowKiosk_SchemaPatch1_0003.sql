create type [PatientFlow].[ModuleTranslations] as table
(
	[Id] [int] not null,
	[LanguageCode] [varchar](50) not null,
	[TranslatedText] [nvarchar](1000) not null,
	[TranslationRefId] [int] not null,
	[ModifiedBy] [varchar](50) not null
)

create type [PatientFlow].[Member] as table
(
	[MemberId] [int] null,
	[Firstname] [varchar](100) null,
	[Surname] [varchar](100) null,
	[Title] [varchar](100) null,
	[SessionHolderId] [int] null,
	[DepartmentId] [int] null
)

create type [PatientFlow].[LogEntry] as table
(
    [LogDate] [datetime] NOT NULL,
	[Thread] [varchar](255) NULL,
	[LogLevel] [varchar](50) NULL,
	[Logger] [varchar](255) NULL,
	[LogUser] [varchar](50) NULL,
	[LogMessage] [varchar](4000) NULL,
	[Exception] [varchar](2000) NULL
);

create type [PatientFlow].[ListWithOrder] as table
(
	[Id] [int] not null,
	[ListOrder] [int] not null
);

create type [PatientFlow].[List] as table
(
	[Id] [int] not null
);

create type [PatientFlow].[AppointmentSlotType] as table
(
	[OrganisationId] [int] not null,
	[SlotTypeId] [int] not null,
	[Description] [varchar](100) null
);

create type [PatientFlow].[Permission] as table
(
	[AccessTypeId] [int] not null,
	[ViewAccess] [bit] null,
	[AddAccess] [bit] null,
	[EditAccess] [bit] null,
	[DeleteAccess] [bit] null
);