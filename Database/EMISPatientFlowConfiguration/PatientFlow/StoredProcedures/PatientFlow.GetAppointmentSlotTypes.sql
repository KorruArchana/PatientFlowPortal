if object_id ('[PatientFlow].[GetAppointmentSlotTypes]') is not null
drop procedure [PatientFlow].[GetAppointmentSlotTypes];
go

create procedure [PatientFlow].[GetAppointmentSlotTypes] 
@OrganisationId int
as
begin
set nocount on;
set transaction isolation level read committed;	
	select 
		slot.OrganisationId,
		SlotTypeId,
		Description,
		OrganisationName
	from [PatientFlow].[AppointmentSlotType] slot
	join PatientFlow.Organisation org on slot.OrganisationId = org.OrganisationId
	where slot.OrganisationId=@OrganisationId
	order by Description
end


