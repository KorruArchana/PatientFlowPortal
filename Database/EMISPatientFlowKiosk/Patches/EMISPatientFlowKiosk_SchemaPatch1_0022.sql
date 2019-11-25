/*
Description: Allowing Reception name to be stored in Appointment Details
Author: Archana
Patch Number: 1.0022
Dependant Patch Number = 1.0020
*/

Alter table PatientFlow.Appointment
add Reception nvarchar(1000) 

go

if object_id ('[PatientFlow].[SaveAppointments]') is not null
	drop procedure PatientFlow.SaveAppointments

if object_id ('[PatientFlow].[SaveAppointmentsTopas]') is not null
	drop procedure PatientFlow.SaveAppointmentsTopas

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


