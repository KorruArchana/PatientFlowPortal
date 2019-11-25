/*
Description: Added new System Type for Topas
Author: Venkat
Reviewer: Aravind
Patch Number: 1.0023
Dependant Patch Number = 1.0019
*/

insert into PatientFlow.OrganisationSystemType (SystemTypeId, SystemType) values (5,'TOPAS');
go

alter table PatientFlow.PatientFlowUser 
add WebServiceUrl varchar(500)  null;
