/*
Description: Added two new columns for Forced Survey and Skip Questions
Author: Hassan
Reviewer: Aravind
Patch Number: 1.0022
Dependant Patch Number = 1.0019
*/

alter table PatientFlow.Kiosk 
add ForceSurvey bit
constraint DF_PatientFlow_Kiosk_ForceSurvey default 0 with values

alter table PatientFlow.Kiosk 
add SkipSurveyQuestion bit
constraint DF_PatientFlow_Kiosk_SkipSurveyQuestion default 0 with values