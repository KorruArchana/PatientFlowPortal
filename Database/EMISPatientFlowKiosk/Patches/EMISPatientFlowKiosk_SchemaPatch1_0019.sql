/*
Description: Allowing FullDomainName instead of only Ip Address
Author: Aravind
Reviewer: 
Patch Number: 1.0018
Dependant Patch Number = 1.0001
*/

alter table PatientFlow.PatientFlowUser
alter column IPAddress varchar(100) not null

go