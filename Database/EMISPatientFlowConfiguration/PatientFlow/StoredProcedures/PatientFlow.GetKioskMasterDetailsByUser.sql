if object_id('[PatientFlow].[GetKioskMasterDetailsByUser]') is not null
	drop procedure [PatientFlow].[GetKioskMasterDetailsByUser];
go

create procedure [PatientFlow].[GetKioskMasterDetailsByUser] 
as
begin

	set nocount on;
	set transaction isolation level read committed

	exec [PatientFlow].[GetModulesListForUser]
	exec [PatientFlow].[GetLanguageList]
	exec [PatientFlow].[GetPatientMatchList]
	exec [PatientFlow].[GetAppointmentMatchList]
	exec [PatientFlow].[GetDemographicDetailsList]

end