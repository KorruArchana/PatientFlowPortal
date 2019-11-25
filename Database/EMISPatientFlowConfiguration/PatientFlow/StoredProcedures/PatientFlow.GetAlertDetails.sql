if object_id ('[PatientFlow].[GetAlertDetails]') is not null
	drop procedure [PatientFlow].[GetAlertDetails];
go

create procedure [PatientFlow].[GetAlertDetails]
	@AlertId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	AlertId,
	AlertText,
	Gender,
	Age2,
	Age1,
	Operation,
	ModifiedBy,
	Modified,
	AlertDisplayFormatTypeId
from [PatientFlow].[Alert] alert
where alert.AlertId=@AlertId

------------------------------------------------------------
select 
	[Alertmaster].OrganisationId,
	OrganisationName,
	OrganisationKey,
	kiosk.KioskGuid,
	isnull(kiosk.KioskName,'All Kiosk Linked') as KioskName
from [PatientFlow].[Organisation] [Org]
join [PatientFlow].[AlertLinkToOrganisation] [Alertmaster] on [Org].OrganisationId=[Alertmaster].OrganisationId
left join PatientFlow.AlertLinkToKiosk AlertKiosk on [Alertmaster].AlertId = AlertKiosk.AlertId
left join PatientFlow.Kiosk kiosk on AlertKiosk.KioskId = kiosk.KioskId
where [Alertmaster].[AlertId]=@AlertId

select 
	AlertId,
	LinkTypeId,
	TypeId as value 
from PatientFlow.AlertsLinkedToDepMem
where AlertId = @AlertId and LinkTypeId in (2,3)
end