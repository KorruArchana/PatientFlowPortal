if object_id('[PatientFlow].[GetPatientFlowUserByKioskKey]') is not null
drop Procedure [PatientFlow].[GetPatientFlowUserByKioskKey]
go

create procedure [PatientFlow].[GetPatientFlowUserByKioskKey]
	@ProductKey uniqueidentifier
as
begin
	
	set nocount on;
	set transaction isolation level read committed;
    
	select  
		U.UserName, 
		U.[Password], 
		U.SupplierId, 
		U.IPAddress, 
		O.OrganisationKey, 
		T.SystemType, 
		O.OrganisationId,
		O.OrganisationName,
		U.WebServiceUrl
	from PatientFlow.Kiosk as K 
	inner join PatientFlow.KioskLinkedToDetails kd on kd.KioskId = k.KioskId
	inner join PatientFlow.Organisation as O on O.OrganisationId = kd.TypeId
	inner join PatientFlow.PatientFlowUser as U on O.OrganisationId = U.OrganisationId 
	inner join PatientFlow.OrganisationSystemType as T on O.SystemTypeId = T.SystemTypeId
	Where k.KioskGuid=@ProductKey; 

end
