/*
	Description: New Kiosk Arrival Configuration table and Site Details
	Author: Aravind
	Patch Number: 1.0034
	Dependant Patch Number: 1.0001
*/

create table PatientFlow.KioskArrivalConfiguration
(
	KioskId int not null, 	
	EarlyArrival int,
	LateArrival int,
	AutoConfirmArrival bit,
	AutoConfirmMultipleArrival bit constraint [DF_PatientFlow_KioskArrivalConfiguration_AutoConfirmMultipleArrival] default (0),     
	ShowDemographicDetails bit constraint [DF_PatientFlow_KioskArrivalConfiguration_ShowDemographicDetails] default (0),     
	DemographicDetailsDuration int,
	ScrambleDemographicDetails bit constraint [DF_PatientFlow_KioskArrivalConfiguration_ScrambleDemographicDetails] default (0),     
	QOFKioskUser bit constraint [DF_PatientFlow_KioskArrivalConfiguration_QOFKioskUser] default (1),     
	ShowDoctorDelay bit constraint [DF_PatientFlow_KioskArrivalConfiguration_ShowDoctorDelay] default (1),     
	ForceSurvey bit constraint [DF_PatientFlow_KioskArrivalConfiguration_ForceSurvey] default (0),
	SkipSurveyQuestion bit constraint [DF_PatientFlow_KioskArrivalConfiguration_SkipSurveyQuestion] default (0),
    constraint [PK_PatientFlow_KioskArrivalConfiguration_KioskId] primary key  (KioskId), 
	constraint [FK_PatientFlow_KioskArrivalConfiguration_KioskId] foreign key ([KioskId]) references [PatientFlow].[Kiosk] ([KioskId])	
);

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Table contains Kiosk Arrival Configuration', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'KioskArrivalConfiguration'; 

go


insert PatientFlow.KioskArrivalConfiguration
(	
	KioskId,
	EarlyArrival,
	LateArrival,
	AutoConfirmArrival,    
	ShowDemographicDetails,
	DemographicDetailsDuration,
	QOFKioskUser,
	ShowDoctorDelay,
	ForceSurvey,
	SkipSurveyQuestion
) 
select 
	KioskId,
	EarlyArrival,
	LateArrival,
	AutoConfirmArrival,     
	ShowDemographicDetails,
	DemographicDetailsDuration,
	QOFKioskUser,
	ShowDoctorDelay,
	ForceSurvey,
	SkipSurveyQuestion
from PatientFlow.Kiosk;


alter table PatientFlow.kiosk
drop 
constraint DF_PatientFlow_Kiosk_AutoConfirmArrival,
		   DF_PatientFlow_Kiosk_ForceSurvey,
		   DF_PatientFlow_Kiosk_SkipSurveyQuestion,
column EarlyArrival, 
	   LateArrival,
	   AutoConfirmArrival,     
	   ShowDemographicDetails,
	   DemographicDetailsDuration,
	   QOFKioskUser,
	   ShowDoctorDelay,
	   SkipSurveyQuestion,
	   ForceSurvey	;
go

alter table Patientflow.KioskLinkedToDetails
add MainLocation BIT null;

alter table PatientFlow.Kiosk
add SiteMap varbinary (max) null;

alter table PatientFlow.SyncService
add constraint [FK_PatientFlow_SyncService_KioskId] foreign key ([KioskId]) references [PatientFlow].[Kiosk] ([KioskId]);

alter table [PatientFlow].[Kiosk] 
with check check constraint [FK_PatientFlow_Kiosk_AppointmentMatchId]; 
  
go