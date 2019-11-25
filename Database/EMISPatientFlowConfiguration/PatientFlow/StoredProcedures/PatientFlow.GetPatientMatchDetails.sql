if object_id('[PatientFlow].[GetPatientMatchDetails]') is not null
drop Procedure [PatientFlow].[GetPatientMatchDetails]
go

create procedure [PatientFlow].[GetPatientMatchDetails] 
		@PatientMatchId	int
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
	select 
		PatientMatchId,
		PatientMatchTitle, 
		ScreenOrder
	from [PatientFlow].[KioskPatientMatch]
	where PatientMatchId=@PatientMatchId
end
