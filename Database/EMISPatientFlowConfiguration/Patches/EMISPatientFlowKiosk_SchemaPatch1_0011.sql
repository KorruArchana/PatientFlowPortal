alter table [patientFlow].[AppointmentSlotType] drop constraint PK_PatientFlow_AppointmentSlotType_OrganisationId_SlotTypeId
alter table [PatientFlow].[AppointmentSlotType] alter column SlotTypeId varchar(100) not null
alter table [PatientFlow].[AppointmentSlotType] add constraint [PK_PatientFlow_AppointmentSlotType_OrganisationId_SlotTypeId] primary  key (OrganisationId asc, SlotTypeId asc)

drop type [PatientFlow].[AppointmentSlotType] 

create type [PatientFlow].[AppointmentSlotType] as table(
	[OrganisationId] [int] not null,
	[SlotTypeId] [varchar](100) not null,
	[Description] [varchar](100) null
)