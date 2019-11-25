if object_id('[PatientFlow].[GetPatientMatchList]') is not null
drop Procedure [PatientFlow].[GetPatientMatchList]
go

create procedure [PatientFlow].[GetPatientMatchList]
as
begin
	set nocount on;
    set transaction isolation level read committed;
    
	select 
		PatientMatchId,
		PatientMatchTitle,
		ScreenOrder
	from PatientFlow.KioskPatientMatch
end
