/*
	Description: Fixing unit test failure scenarios.
	Author: Archana
	Patch Number: 1.0040
	Dependant Patch Number = 1.0001
*/


alter table PatientFlow.Organisation add constraint FK_PatientFlow_Organisation_SystemTypeId
foreign key (SystemTypeId) references PatientFlow.OrganisationSystemType (SystemTypeId)

alter table PatientFlow.Kiosk add constraint FK_PatientFlow_Kiosk_KioskStatus
foreign key (KioskStatus) references PatientFlow.Status (StatusId)

alter table PatientFlow.Kiosk
alter column ConnectionGuid uniqueidentifier

alter table PatientFlow.Kiosk
drop constraint [UQ_PatientFlow_Kiosk_KioskGuid]

drop table PatientFlow.AccessType
drop table PatientFlow.AlertType

go

