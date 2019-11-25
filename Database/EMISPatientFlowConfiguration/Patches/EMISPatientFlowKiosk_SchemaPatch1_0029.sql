/*
Description: Allowing AlertText to have non English Text
Author: Aravind
Patch Number: 1.0029
Dependant Patch Number = 1.0001
*/

alter table PatientFlow.Alert 
alter column AlertText nvarchar (4000) not null
