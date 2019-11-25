if object_id ('[PatientFlow].[GetKioskDetailsForOrganisation]') is not null
	drop procedure [PatientFlow].[GetKioskDetailsForOrganisation];
go

create procedure [PatientFlow].[GetKioskDetailsForOrganisation]
	@OrganisationId int,
	@KioskName varchar(200)=null
as
begin
set nocount on;
set transaction isolation level read committed
	
	select 
		Kiosk.KioskId,
		KioskName,
		KioskGuid,
		ConnectionStatus,
		ConnectionGuid,
		PCName,
		IPAddress,
		KioskVersion,
		LastStatusUpdate
	from [PatientFlow].Kiosk as Kiosk
join [PatientFlow].KioskLinkedToDetails as Kioskorganisation
on Kiosk.KioskId=Kioskorganisation.KioskId
join [PatientFlow].[Status] [Status] 
on Kiosk.ConnectionStatus=[Status].[StatusId]
where Kioskorganisation.TypeId= @OrganisationId
and Kiosk.KioskName like ISNULL(@KioskName,'')+'%'
end
