/*
	Description: Adding a new type to save slots and sessionholders linked with kiosk.
				 Add a new column organisationId to SessionHolderList table.
	Author: Archana
	Patch Number: 1.0041
	Dependant Patch Number = 1.0025
*/

create type PatientFlow.KioskLinkedFields as table
(
	KioskLinkedFieldsId int primary key,
	OrganisationId int not null,
	FieldId int null
);
go

--Adding OrganisationId column in KioskSessionHolder

alter table PatientFlow.KioskSessionHolder
add OrganisationId int null 
constraint FK_PatientFlow_KioskSessionHolder_OrganisationId foreign key (OrganisationId) 
references PatientFlow.Organisation(OrganisationId)

go

update ksh
	set ksh.OrganisationId = kld.TypeId
from PatientFlow.KioskSessionHolder ksh
Join PatientFlow.KioskLinkedToDetails kld on ksh.KioskId = kld.KioskId

alter table PatientFlow.KioskSessionHolder
alter column OrganisationId int not null;

go