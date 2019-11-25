if object_id ('[PatientFlow].[GetKioskList]') is not null
	drop procedure [PatientFlow].[GetKioskList];
go

create procedure [PatientFlow].[GetKioskList]
as
begin

	set nocount on;
	set transaction isolation level read committed;
	
	select 
		kiosk.KioskId,
		kiosk.KioskGuid,
		kiosk.ConnectionGuid,
		Title,
		KioskName,
		PCName,
		IPAddress,
		KioskStatus as [Status],
		[Status].StatusName,
		Kioskversion,
		LastStatusUpdate,
		org.OrganisationId,
		org.OrganisationName,
		org.OrganisationKey,
		org.SystemTypeId,
		st.SystemType
	from PatientFlow.Kiosk as kiosk
	join PatientFlow.KioskPatientMatch as kpm on kiosk.PatientMatchId = kpm.PatientMatchId
	join PatientFlow.[Status] [Status] on kiosk.KioskStatus = [Status].[StatusId]
	join PatientFlow.KioskLinkedToDetails details on details.KioskId = kiosk.KioskId
	join PatientFlow.Organisation org on details.TypeId = org.OrganisationId
	join PatientFlow.OrganisationSystemType st on org.SystemTypeId = st.SystemTypeId

end

