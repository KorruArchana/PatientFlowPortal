/*
	Description: Creating indexes on kiosk table to improve performance and creating InternalIPAddress column in PFUser.
	Author: Archana
	Patch Number: 1.0043
	Dependant Patch Number = 1.0001
*/

create nonclustered index IDX_PatientFlow_Kiosk_KioskGuid on PatientFlow.Kiosk (KioskGuid) Include(KioskName)

drop index PatientFlow.KioskLinkedToDetails.IDX_PatientFlow_KioskLinkedToDetails_TypeId
create nonclustered index IDX_PatientFlow_KioskLinkedToDetails_TypeId on PatientFlow.KioskLinkedToDetails (TypeId) Include(KioskId)
create nonclustered index IDX_PatientFlow_Member_DepartmentId on PatientFlow.Member (DepartmentId) Include(MemberId, SessionHolderId)
create nonclustered index IDX_PatientFlow_PatientMessage_PatientId on PatientFlow.PatientMessage (PatientId) Include ([Message])

go

alter table PatientFlow.PatientFlowUser
add InternalIPAddress varchar(40) null

go

update PatientFlow.DemographicDetailsType
set DemographicDetailsTypeName = 'Email Address'
where DemographicDetailsTypeId = 4

go



