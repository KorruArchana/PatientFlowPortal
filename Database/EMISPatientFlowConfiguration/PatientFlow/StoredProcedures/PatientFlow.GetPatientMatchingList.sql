if object_id('[PatientFlow].[GetPatientMatchingList]') is not null
drop Procedure [PatientFlow].[GetPatientMatchingList]
go

create procedure [PatientFlow].[GetPatientMatchingList]
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
	select  
		[PatientMatchId], 
		[PatientMatchTitle]
    from [PatientFlow].[KioskPatientMatch]
end