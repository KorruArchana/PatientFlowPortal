if object_id ('[PatientFlow].[GetKioskSlotType]') is not null
drop procedure [PatientFlow].[GetKioskSlotType];
go

create procedure [PatientFlow].[GetKioskSlotType]
	@KioskId int
as
begin

	set nocount on;
	set transaction isolation level read committed;

	select 
		SlotType.SlotTypeId,
		SlotType.[Description],
		KioskSlotType.OrganisationId
	from PatientFlow.AppointmentSlotType SlotType
	join PatientFlow.KioskSlotType KioskSlotType 
		on SlotType.SlotTypeId=KioskSlotType.SlotTypeId and SlotType.OrganisationId=KioskSlotType.OrganisationId
	where KioskSlotType.KioskId=@KioskId
		
end
