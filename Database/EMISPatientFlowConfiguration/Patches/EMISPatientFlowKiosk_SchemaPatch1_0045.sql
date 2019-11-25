/*
	Description: Updating AutoConfirmArrival, AutoConfirmMultipleArrival and QOFKioskUser as enabled for all kiosks
	Author: Archana
	Patch Number: 1.0045
	Dependant Patch Number = 1.0034
*/

update PatientFlow.KioskArrivalConfiguration
set AutoConfirmArrival = 1, 
	AutoConfirmMultipleArrival = 1, 
	QOFKioskUser = 1