/*
Description: Allowing Patient Message to have non English Text
Author: Aravind
Patch Number: 1.0030
Dependant Patch Number = 1.0001
*/

alter table PatientFlow.PatientMessage 
alter column Message nvarchar (4000) not null
