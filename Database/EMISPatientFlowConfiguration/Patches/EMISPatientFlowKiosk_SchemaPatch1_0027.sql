/*
Description: Adding a new module in Kiosk
Author: Aravind
Reviewer: Sathish
Patch Number: 1.0027
Dependant Patch Number = 1.0026
*/


insert into PatientFlow.TranslationRef(TranslationTypeId) values(1)

insert into PatientFlow.Translation values (1,111,'Arrival By Barcode',null,GETDATE())

insert into PatientFlow.Module(Moduleid,ModuleName,TranslationRefId) values (5,'Arrival by Barcode',111)
