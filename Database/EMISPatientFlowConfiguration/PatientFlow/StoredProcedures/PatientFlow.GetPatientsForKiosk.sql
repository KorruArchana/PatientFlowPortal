if object_id('[PatientFlow].[GetPatientsForKiosk]') is not null
drop Procedure [PatientFlow].[GetPatientsForKiosk]
go

create procedure [PatientFlow].[GetPatientsForKiosk]
	@KioskAddress varchar(50)
as
begin
	
	set nocount on;
	set transaction isolation level read committed;	
    select 
		Patient.PatientId,
		PatientMessage.PatientMessageId, 
		Patient.OrganisationId,
		PatientMessage.[Message],
		[Patient].FirstName,
		[Patient].[Surname], 
		[Patient].DOB
	from [PatientFlow].[PatientMessage] PatientMessage
	left join [PatientFlow].[Patient] Patient on Patient.PatientId=PatientMessage.PatientId
	join [PatientFlow].[KioskLinkedToDetails] KioskLink on patient.OrganisationId=KioskLink.TypeId
	join [PatientFlow].[Kiosk] kiosk on KioskLink.kioskId=Kiosk.KioskId
	where Kiosk.KioskGuid=@KioskAddress
end
