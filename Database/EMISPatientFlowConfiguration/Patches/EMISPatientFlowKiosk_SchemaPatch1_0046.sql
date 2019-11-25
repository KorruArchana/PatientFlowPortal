/*
	Description: Insert TPP as a new system type
	Author: JM Deepak
	Patch Number: 1.0046
	Dependant Patch Number = 1.0034
*/

insert into PatientFlow.OrganisationSystemType (SystemTypeId, SystemType) values (7, 'TPP - SystmOne');


drop type PatientFlow.Member

create type PatientFlow.Member as table
(
	SyncMemberId int not null  primary key,
	MemberId int not null,
	Firstname varchar(100) null,
	Surname varchar(100) null,
	Title varchar(100) null,
	SessionHolderId int null,
	DepartmentId int null,
	LoginId varchar(100) null
)