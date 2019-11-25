/*
Description: Added two new columns for Forced Survey and Skip Questions
Author: Aravind
Reviewer: Sathish
Patch Number: 1.0024
Dependant Patch Number = 1.0022
*/

alter table PatientFlow.Kiosk 
add AppointmentReason bit null
