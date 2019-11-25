/*
Description: Fixing SQL COP issues 
Author: Aravind
Patch Number: 1.0024
Dependant Patch Number: 1.0023
*/


alter table PatientFlow.Appointment 
alter column Reception varchar (1000) null

go

alter table PatientFlow.Patient 
alter column Gender varchar (100) null

go

if object_id ('[PatientFlow].[SaveAppointments]') is not null
	drop procedure PatientFlow.SaveAppointments
go

if object_id ('[PatientFlow].[SaveAppointmentsTopas]') is not null
	drop procedure PatientFlow.SaveAppointmentsTopas
go

 if object_id ('PatientFlow.SaveAnonymousSurvey') is not null
	drop procedure PatientFlow.SaveAnonymousSurvey
go

if object_id ('PatientFlow.SetKioskInitialConfiguration') is not null
	drop procedure PatientFlow.SetKioskInitialConfiguration;
go

if object_id ('PatientFlow.GetLanguages') is not null
	drop procedure PatientFlow.GetLanguages;
go

if object_id ('PatientFlow.GetModules') is not null
	drop procedure PatientFlow.GetModules;
go

if object_id ('PatientFlow.SaveQuestionnaire') is not null
	drop procedure PatientFlow.SaveQuestionnaire;
go

if object_id ('PatientFlow.UpdateKioskQuestionnaire') is not null
	drop procedure PatientFlow.UpdateKioskQuestionnaire;
go

if object_id ('PatientFlow.SaveQuestionnaireinitialData') is not null
	drop procedure PatientFlow.SaveQuestionnaireinitialData;
go

if object_id ('PatientFlow.SaveScreenControl') is not null
	drop procedure PatientFlow.SaveScreenControl;
go

drop type PatientFlow.AnonymousSurvey;
drop type PatientFlow.KioskConfiguration;
drop type PatientFlow.KioskLanguageOrder;
drop type PatientFlow.KioskModule;
drop type PatientFlow.List;
drop type PatientFlow.ListWithOrder;
drop type PatientFlow.Questionnaire;
drop type PatientFlow.ScreenControl;
drop type PatientFlow.BookedAppointment

go

create type PatientFlow.AnonymousSurvey as table 
(
	AnonymousSurveyId int primary key,
    KioskId varchar(50) not null,
    QuestionnaireId int null,
    QuestionnaireTitle varchar(1000) null,
    QuestionId int null,
    QuestionText varchar(1000) null,
    OptionId varchar(50) null,
    AnswerText varchar(1000) null
);

create type  PatientFlow.KioskConfiguration as table
(
	ConfigType varchar(100) not null,
	KioskId varchar(50) not null,
	Value nvarchar(4000) not null
	primary key clustered(ConfigType , KioskId)
)
go

create type PatientFlow.KioskLanguageOrder as table 
(
    LanguageId int not null primary key,
    [Order] int not null
);

create type PatientFlow.KioskModule as table 
(
	KioskModuleId int primary key,
    ModuleId int not null,
    ModuleName varchar(100) not null,
    ModuleNameToDisplay varchar(100) not null,
    TranslationRefId bigint null
);

create type PatientFlow.List as table
(
	ListId int primary key,
	Id int not null
)

create type PatientFlow.ListWithOrder as table
(
	Id int not null primary key,
	ListOrder int not null
);

create type PatientFlow.Questionnaire as table
(
	QuestionnaireId int not null primary key,
	Title varchar(1000) not null,
	Frequency int null,
	CreateConsultation bit null,
	IsAnonymous bit null,
	OrganisationId int not null
);

create type PatientFlow.ScreenControl as table 
(
	ScreenControlId  int not null primary key,
    ControlId int not null,
    TranslationRefId int null
);
  
go
                
exec sys.sp_addextendedproperty 
    @name = N'UnitTestException_TestForInvalidDataTypeUse',
    @value = N'Kiosk text should support multiple language text.',
    @level0type = N'SCHEMA',
    @level0name = 'PatientFlow',
    @level1type = N'TYPE',
    @level1name = 'Translation',
    @level2type = N'COLUMN',
    @level2name = 'TranslationText';
                
exec sys.sp_addextendedproperty 
    @name = N'UnitTestException_TestForInvalidDataTypeUse',
    @value = N'Value should support multiple language text.',
    @level0type = N'SCHEMA',
    @level0name = 'PatientFlow',
    @level1type = N'TYPE',
    @level1name = 'KioskConfiguration',
    @level2type = N'COLUMN',
    @level2name = 'Value';
                
go

create type [PatientFlow].[BookedAppointment] as table
(
	[AppointmentId] int not null,
    [AppointmentDate] datetime null,
    [PatientId] int null,
    [PatientTitle] varchar(100) null,
    [PatientFirstName] varchar(250) null,
    [PatientCallingName] varchar(250) null,
    [PatientFamilyName] varchar(250) null,
    [PatientGender] varchar(80) null,
    [PatientPostCode] varchar(50) null,
    [PatientDOB] varchar(150) null,
    [SessionHolderId] int null,
    [SessionHolderTitle] varchar(100) null,
    [SessionHolderFirstName] varchar(150) null,
    [SessionHolderLastName] varchar(150) null,
    [WaitingTime] int default 0,
    [PracticeName] varchar(80) null,
    [PatientEmail] varchar(256) null,
    [PatientMobileNumber] varchar(150) null,
    [PatientWorkPhoneNumber] varchar(150) null,
    [PatientHomePhoneNumber] varchar(150) null,
    [ModifiedBy] varchar(50) null,
	[SiteId] bigint null,
	[PatientIdentifierValue] varchar(50),
	[MemberIdentifierValue] varchar(80),
	[Reception] varchar(1000),
	[SystemType] int,
    primary key clustered ([AppointmentId] asc)
);

go

exec sp_help 'PatientFlow.Patient'