if object_id ('[PatientFlow].[GetAdditionalInformationForKiosk]') is not null
	drop procedure [PatientFlow].[GetAdditionalInformationForKiosk];
go

create procedure [PatientFlow].[GetAdditionalInformationForKiosk]
	@KioskId int
as
begin
set nocount on;
set transaction isolation level read committed;



select		
	KioskLink.SessionHolderId,
	KioskLink.OrganisationId as OrganisationId,
	LoginId
from [PatientFlow].KioskSessionHolder KioskLink	
left join PatientFlow.Member Member on KioskLink.SessionHolderId =	Member.SessionHolderId and KioskLink.OrganisationId = Member.OrganisationId
where KioskLink.KioskId = @KioskId
-----------------------------------------------------------------------------
select 
	Patient.PatientId,
	PatientMessage.PatientMessageId, 
	Patient.OrganisationId,
	PatientMessage.[Message],
	[Patient].FirstName,
	[Patient].[Surname], 
	[Patient].DOB
from [PatientFlow].[PatientMessage] PatientMessage
join [PatientFlow].[Patient] Patient on Patient.PatientId=PatientMessage.PatientId
join [PatientFlow].[KioskLinkedToDetails] KioskLink on patient.OrganisationId=KioskLink.TypeId
where KioskLink.KioskId = @KioskId

--------------------------------------------------------------------------------------
select 
       Alert.ALertId,
       Alert.AlertType,
       Alert.AlertText,
       Alert.Gender,
       Alert.Age1,
       Alert.Gender,
       Alert.Operation,
       Alert.Age2,
       AlertLink.OrganisationId,
       case 
              when AlertKiosk.KioskId is null 
                     then null 
              else kiosk.KioskGuid 
       end as KioskGuid,
       Alert.AlertDisplayFormatTypeId  
from patientflow.Alert alert
left join PatientFlow.AlertLinkToKiosk alertkiosk on alert.AlertId = alertkiosk.AlertId 
join  [PatientFlow].[AlertLinkToOrganisation] AlertLink on alert.AlertId = AlertLink.AlertId
join [PatientFlow].[KioskLinkedToDetails] [KioskLink] on KioskLink.TypeId=AlertLink.OrganisationId
join PatientFlow.Kiosk on Kiosk.KioskId = KioskLink.KioskId
where KioskLink.KioskId = @kioskId and (alertkiosk.KioskId = @kioskId or alertkiosk.AlertId is null)

---------------------------------------Diverts list-------------------------------------------------------
select 
		member.OrganisationId,
		member.SessionHolderId,
		member.LoginId
from [PatientFlow].[KioskLinkedToDetails] details
join [PatientFlow].[Member] member on details.TypeId=member.OrganisationId 
where member.IsDivertSet=1
and details.KioskId=@KioskId

end

