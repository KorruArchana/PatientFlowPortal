/*
Description: Allowing the TOPAS Sessionholder to have string codes
Author: Aravind
Reviewer: 
Patch Number: 1.0020
Dependant Patch Number = 1.0015
*/

if exists(select 1 from PatientFlow.Member where OrganisationId in(select OrganisationId from PatientFlow.Organisation where upper(SystemType) ='TOPAS'))
begin

delete app 
from PatientFlow.Appointment app
inner join PatientFlow.Member mem
	on app.PatientFlowMemberId = mem.PatientFlowMemberId
join PatientFlow.Organisation org
	on mem.OrganisationId = org.OrganisationId
where upper(org.SystemType) ='TOPAS';

delete from PatientFlow.Member where OrganisationId in(select OrganisationId from PatientFlow.Organisation where upper(SystemType) ='TOPAS');

end

go

create table PatientFlow.MemberIdentifier
(
	PatientFlowMemberIdentifierId int not null identity(1,1) primary key clustered,
	Value varchar(80) not null,
	OrganisationId int not null,
	SystemType int not null
);

alter table PatientFlow.MemberIdentifier 
add constraint FK_PatientFlow_MemberIdentifier_OrganisationId foreign key (OrganisationId) 
    references PatientFlow.Organisation (OrganisationId);
go

alter table PatientFlow.MemberIdentifier
add constraint UQ_PatientFlow_MemberIdentifier_Value_OrganisationId unique(Value,OrganisationId);
go


exec sys.sp_addextendedproperty 
@name = N'PatientFlow.MemberIdentifier', 
@value = N'Member Identifier information for Topas Session Holders', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'MemberIdentifier'; 

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
    [PracticeName] varchar(80) null,
    [PatientEmail] varchar(256) null,
    [PatientMobileNumber] varchar(150) null,
    [PatientWorkPhoneNumber] varchar(150) null,
    [PatientHomePhoneNumber] varchar(150) null,
    [ModifiedBy] varchar(50) null,
	[SiteId] bigint null,
	[PatientIdentifierValue] varchar(50),
	[MemberIdentifierValue] varchar(80),
	[SystemType] int,
    primary key clustered ([AppointmentId] asc)
);

go