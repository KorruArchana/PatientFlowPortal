if object_id('[PatientFlow].[GetKioskSyncKeys]') is not null 
	drop procedure [PatientFlow].[GetKioskSyncKeys]
go

create procedure  [PatientFlow].[GetKioskSyncKeys]
     @KioskId int
as
begin
	set nocount on;
	set transaction isolation level read committed

	select 
		distinct
		k.KioskGuid,
		s.ProductKey,
		k.KioskName,
		s.SyncConnectionId,	
		k.ConnectionGuid as KioskConnectionGuid,
		OrganisationName = stuff
		(
			(
				select distinct ', '+ cast(org.OrganisationName as varchar(max))  
				from PatientFlow.SyncService sync
				join PatientFlow.Organisation org on sync.OrganisationId = org.OrganisationId
				where KioskId = @KioskId
				for xml path('')   
			),1,1,'' 
		)
	from PatientFlow.Kiosk as k 
	join PatientFlow.SyncService as s on k.KioskId=s.KioskId
	where k.KioskId=@KioskId

end