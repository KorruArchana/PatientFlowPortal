/*
Description: Creating new type TPPBookedAppointment
Author: Archana
Patch Number: 1.0030
Dependant Patch Number: 1.0002
*/

if object_id ('[PatientFlow].[SaveTPPAppointments]') is not null
	drop procedure [PatientFlow].[SaveTPPAppointments];
go

create type [PatientFlow].[TPPBookedAppointment] as table
(
    [AppointmentId] int not null,
    [AppointmentDate] datetime null,
    [PatientId] int null,
    [PatientTitle] varchar(100) null,
    [PatientFirstName] varchar(250) null,
    [PatientCallingName] varchar(250) null,
    [PatientFamilyName] varchar(250) null,
    [PatientGender] varchar(80) null,
    [PatientDOB] varchar(150) null,
    [SessionHolderId] int null,
    [SessionHolderTitle] varchar(100) null,
    [SessionHolderFirstName] varchar(150) null,
    [SessionHolderLastName] varchar(150) null,
    [WaitingTime] int default 0,
    [PracticeName] varchar(80) null,
    [ModifiedBy] varchar(50) null,
	[SiteId] bigint null,
	[PatientIdentifierValue] varchar(50),
	[MemberIdentifierValue] varchar(80),
	[TPPAppointmentId] varchar(20),
	[SystemType] int,
	Duration int default 0,
    primary key clustered ([TPPAppointmentId] asc)
);

go

drop table PatientFlow.PatientIdentifier

create table PatientFlow.PatientIdentifier
(
PatientFlowPatientIdentifierId int not null identity(1,1) primary key clustered,
Value varchar(50) null,
SystemType int not null,
OrganisationId int not null,
constraint UQ_PatientFlow_Value_OrganisationId unique(Value,OrganisationId)
);

go

create type PatientFlow.Member as table
(
	MemberId int not null primary key,
	Title varchar(100) null,
	FirstName varchar(100) null,
	LastName varchar(100) null,
	MemberIdentifierValue varchar(100) not null,
	ModifiedBy varchar(100) null,
	WaitingTime int null
)
