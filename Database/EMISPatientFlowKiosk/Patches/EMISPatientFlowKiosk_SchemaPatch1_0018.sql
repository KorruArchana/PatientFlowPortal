/*
Description: Adding a table for Demographic Frequency
Author: Aravind
Reviewer: 
Patch Number: 1.0018
Dependant Patch Number = 1.0015
*/


create table PatientFlow.DemographicDetailsFrequency
(
	FrequencyId int not null identity(1,1),
	PatientFlowPatientId int not null,
	AccessedOn Datetime not null,
	ValidDetails bit not null
	constraint [PK_PatientFlow_DemographicDetailsFrequency_FrequencyId] primary key (FrequencyId),
	constraint FK_PatientFlow_DemographicDetailsFrequency_PatientFlowPatientId  foreign KEY(PatientFlowPatientId)  REFERENCES PatientFlow.Patient(PatientFlowPatientId)
)


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture the date when a patient demographic details was last validated', 
@level0type = N'schema', @level0name = 'PatientFlow',
@level1type = N'table',  @level1name = 'DemographicDetailsFrequency'; 

go

alter table PatientFlow.Member
add PracticeName varchar(80) null

go

if object_id ('[PatientFlow].[SaveAppointments]') is not null
	drop procedure [PatientFlow].[SaveAppointments];
go

if object_id ('[PatientFlow].[SaveAppointmentsTopas]') is not null
	drop procedure [PatientFlow].[SaveAppointmentsTopas];
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
    PracticeName varchar(80) null,
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