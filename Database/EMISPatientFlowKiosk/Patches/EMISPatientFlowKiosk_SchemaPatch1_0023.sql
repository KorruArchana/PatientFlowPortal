/*
Description: Adding Site Name and Map for Appointment Arriving patients
Author: Aravind
Patch Number: 1.0023
Dependant Patch Number = 1.0001
*/

alter table PatientFlow.AppointmentSession 
add SiteName varchar (50) null

go

alter table PatientFlow.KioskLogo
add SiteMap varbinary(max) null

go

if object_id ('PatientFlow.SaveAppointmentSessions') is not null
	drop procedure PatientFlow.SaveAppointmentSessions;
go

drop type PatientFlow.MemberAppointmentSession;

create type PatientFlow.MemberAppointmentSession as TABLE
(
	SessionId int not null,
	Date datetime not null,
	StartTime varchar(100) not null,
	EndTime varchar(100) not null,
	SlotLength int not null,
	TotalSlots int not null,
	BookedSlots int not null,
	AvailableSlots int not null,
	SessionHolderId int null,
	SessionHolderTitle varchar(100) null,
	SessionHolderFirstName varchar(150) null,
	SessionHolderLastName varchar(150) null,
	SlotTypeId varchar(100) null,
	SystemType varchar(100) null,
	SiteId bigint null,
	SiteName varchar(50) null,
	primary key clustered (SessionId asc)
);

go
