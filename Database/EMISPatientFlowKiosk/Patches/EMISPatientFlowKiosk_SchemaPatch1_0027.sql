/*
Description: Adding duration to BookedAppointment type to differentiate untimed Appointment
Author: Archana
Patch Number: 1.0027
Dependant Patch Number: 1.0024
*/

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
	Duration int default 0,
    primary key clustered ([AppointmentId] asc)
);

go

alter table [PatientFlow].[Appointment]
add Duration int 
constraint DF_PatientFlow_Appointment_Duration default (0) not null

go
