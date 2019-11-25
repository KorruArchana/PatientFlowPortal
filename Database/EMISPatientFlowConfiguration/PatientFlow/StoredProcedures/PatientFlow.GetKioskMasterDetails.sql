if object_id('[PatientFlow].[GetKioskMasterDetails]') is not null
	drop procedure [PatientFlow].[GetKioskMasterDetails];
go

create procedure [PatientFlow].[GetKioskMasterDetails] 
as
begin

	set nocount on;
	set transaction isolation level read committed

	exec [PatientFlow].[GetModulesList]
	exec [PatientFlow].[GetLanguageList]
	exec [PatientFlow].[GetPatientMatchList]
	exec [PatientFlow].[GetAppointmentMatchList]
	exec [PatientFlow].[GetDemographicDetailsList]

end