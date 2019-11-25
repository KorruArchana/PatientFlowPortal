alter table [PatientFlow].[Appointment] add [SiteId] [bigint] null
if object_id ('[PatientFlow].[SaveAppointments]') is not null
	drop procedure [PatientFlow].[SaveAppointments];
go
drop type [PatientFlow].[BookedAppointment]
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
	[SiteId] bigint null
    primary key clustered ([AppointmentId] asc)
);

alter table [PatientFlow].[AppointmentSession] add [SiteId] [bigint] null
if object_id ('[PatientFlow].[SaveAppointmentSessions]') is not null
drop procedure [PatientFlow].[SaveAppointmentSessions];
go
drop type [PatientFlow].[MemberAppointmentSession]
create type [PatientFlow].[MemberAppointmentSession] as table(
	[SessionId] [int] not null,
	[Date] [datetime] not null,
	[StartTime] [nvarchar](100) not null,
	[EndTime] [nvarchar](100) not null,
	[SlotLength] [int] not null,
	[TotalSlots] [int] not null,
	[BookedSlots] [int] not null,
	[AvailableSlots] [int] not null,
	[SessionHolderId] [int] null,
	[SessionHolderTitle] [varchar](100) null,
	[SessionHolderFirstName] [varchar](150) null,
	[SessionHolderLastName] [varchar](150) null,
	[SlotTypeId] [varchar](100) null,
	[SystemType] [varchar](100) null,
	[SiteId] [bigint] null,
	primary key clustered ([SessionId] asc)
);

