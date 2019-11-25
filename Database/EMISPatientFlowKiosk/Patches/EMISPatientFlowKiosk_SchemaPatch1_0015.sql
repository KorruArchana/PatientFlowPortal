/*
Description: Adding a new webservice field for Topas
Author: Venkat
Reviewer: Aravind
Patch Number: 1.0015
Dependant Patch Number = 1.0014
*/


alter table PatientFlow.PatientFlowUser 
add WebServiceUrl varchar(500) null;

go

create table PatientFlow.PatientIdentifier
(
PatientFlowPatientIdentifierId int not null identity(1,1) primary key clustered,
Value varchar(50) null,
CallingName varchar(255) not null,
DOB  varchar(150) not null,
SystemType int not null
);


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Patient Identifier information for Patients', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'PatientIdentifier'; 

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

