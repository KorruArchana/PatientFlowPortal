if object_id('[PatientFlow].[GetPatientFlowUserByProductKey]') is not null
drop Procedure [PatientFlow].[GetPatientFlowUserByProductKey]
go

create procedure PatientFlow.GetPatientFlowUserByProductKey
	@ProductKey uniqueidentifier
as
begin
	
	set nocount on;
        set transaction isolation level read committed;
select distinct 
	U.UserName, 
	U.[Password], 
	U.SupplierId, 
	U.IPAddress, 
	U.InternalIPAddress, 
	O.OrganisationKey, 
	T.SystemType, 
	S.OrganisationId,
	O.OrganisationName,
	U.WebServiceUrl
from PatientFlow.SyncService as S 
inner join PatientFlow.PatientFlowUser as U on S.OrganisationId = U.OrganisationId 
inner join PatientFlow.Organisation as O on S.OrganisationId = O.OrganisationId 
inner join PatientFlow.OrganisationSystemType as T on O.SystemTypeId = T.SystemTypeId
where (S.ProductKey = @ProductKey)
end

