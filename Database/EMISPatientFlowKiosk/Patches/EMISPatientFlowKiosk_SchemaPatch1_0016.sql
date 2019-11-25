/*
Description: Adding a new webservice field for Topas
Author: Aravind
Reviewer: 
Patch Number: 1.0016
Dependant Patch Number = 1.0015
*/


alter table PatientFlow.Member 
add WaitingTime int default 0;

go

if object_id ('[PatientFlow].[SaveAppointments]') is not null
	drop procedure [PatientFlow].[SaveAppointments];
go

drop type [PatientFlow].[BookedAppointment]
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
    [PatientGender] varchar(20) null,
    [PatientPostCode] varchar(50) null,
    [PatientDOB] varchar(150) null,
    [SessionHolderId] int null,
    [SessionHolderTitle] varchar(100) null,
    [SessionHolderFirstName] varchar(150) null,
    [SessionHolderLastName] varchar(150) null,
    [WaitingTime] int default 0,
    [PatientEmail] varchar(256) null,
    [PatientMobileNumber] varchar(150) null,
    [PatientWorkPhoneNumber] varchar(150) null,
    [PatientHomePhoneNumber] varchar(150) null,
    [ModifiedBy] varchar(50) null,
	[SiteId] bigint null,
	[PatientIdentifierValue] varchar(50),
	[SystemType] int,
    primary key clustered ([AppointmentId] asc)
);

go

