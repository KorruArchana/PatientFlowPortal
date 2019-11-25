/*
	Description: Creating non clustered indexes on table to improve performance.
	Author: Archana
	Patch Number: 1.0042
	Dependant Patch Number = 1.0001
*/


create nonclustered index IDX_PatientFlow_Questionnaire_OrganisationId on PatientFlow.Questionnaire (OrganisationId)

create nonclustered index IDX_PatientFlow_KioskLinkedToDetails_TypeId on PatientFlow.KioskLinkedToDetails (TypeId)

create nonclustered index IDX_PatientFlow_Department_OrganisationId on PatientFlow.Department (OrganisationId)

create nonclustered index IDX_PatientFlow_Organisation_OrganisationName on PatientFlow.Organisation (OrganisationName)

create nonclustered index IDX_PatientFlow_Organisation_OrganisationKey on PatientFlow.Organisation (OrganisationKey)

create nonclustered index IDX_PatientFlow_Organisation_SystemTypeId on PatientFlow.Organisation (SystemTypeId)

create nonclustered index IDX_PatientFlow_OrganisationAccessMapping_UserName on PatientFlow.OrganisationAccessMapping (UserName)


