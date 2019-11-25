if object_id ('[PatientFlow].[GetKioskListForOrganisation]') is not null
drop procedure [PatientFlow].[GetKioskListForOrganisation];
go

create procedure [PatientFlow].[GetKioskListForOrganisation]
	@OrganisationId int
as
begin
set nocount on;
set transaction isolation level read committed;
select distinct 
	Kiosk.KioskId,
	KioskName,
	PCName,
	IPAddress,
	[ConnectionStatus] as [Status],
	Title ,
	Kiosk.PatientMatchId,
	Kioskversion,
	LastStatusUpdate,
	[Status].StatusName,
	Kiosk.KioskGuid,
	Kiosk.ConnectionGuid
from [PatientFlow].Kiosk as Kiosk
join [PatientFlow].KioskLinkedToDetails as Kioskorganisation
on Kiosk.KioskId=Kioskorganisation.KioskId
join [PatientFlow].[KioskPatientMatch] as KioskPatientMatch 
on Kiosk.PatientMatchId=KioskPatientMatch.PatientMatchId
join [PatientFlow].[Status] [Status] 
on Kiosk.ConnectionStatus=[Status].[StatusId]
where Kioskorganisation.TypeId= @OrganisationId
order by KioskName
end


