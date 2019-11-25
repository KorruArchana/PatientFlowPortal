
/*
Description: Adding a new Column to Enable/Disable AutoArrival 
Author: Aravind
Reviewer: Hassan
Patch Number: 1.0020
Dependant Patch Number = 1.0008
*/

alter table PatientFlow.Kiosk 
add AutoConfirmArrival bit
constraint DF_PatientFlow_Kiosk_AutoConfirmArrival default 0 with values