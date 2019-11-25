/*
	Description: Adding a new config option for kiosk to arrive for untimed appointment
	Author: Archana
	Patch Number: 1.0044
	Dependant Patch Number = 1.0034
*/

alter table PatientFlow.KioskArrivalConfiguration
add AllowUntimed bit 
constraint DF_PatientFlow_KioskArrivalConfiguration_AllowUntimed default (0) not null

go

