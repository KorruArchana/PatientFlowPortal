/*
Description: Increase Field Length for BookAppoinmentType
Author: Aravind
Reviewer: Sathish
Patch Number: 1.0013
Dependant Patch Number = 1.0010
*/


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
    [PatientEmail] varchar(256) null,
    [PatientMobileNumber] varchar(150) null,
    [PatientWorkPhoneNumber] varchar(150) null,
    [PatientHomePhoneNumber] varchar(150) null,
    [ModifiedBy] varchar(50) null,
	[SiteId] bigint null
    primary key clustered ([AppointmentId] asc)
);

go


alter table PatientFlow.Patient
alter column [Email] varchar(256)


alter table PatientFlow.Patient
alter column [MobileNumber] varchar(150)


alter table PatientFlow.Patient
alter column [WorkPhoneNumber] varchar(150)


alter table PatientFlow.Patient
alter column [HomePhoneNumber] varchar(150)

go




