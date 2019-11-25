/*
Description: Allowing the user choose the QOF user to Kiosk user or Appointment Doctor
Author: Aravind
Patch Number: 1.0032
Dependant Patch Number = 1.0001
*/


alter table PatientFlow.Kiosk 
add QOFKioskUser bit null

go

alter table PatientFlow.Kiosk 
add ShowDoctorDelay bit null

go


update PatientFlow.Kiosk 
set QOFKioskUser = 1,
ShowDoctorDelay = 1