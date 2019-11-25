if object_id ('[PatientFlow].[GetKioskOrganisationList]') is not null
drop procedure [PatientFlow].[GetKioskOrganisationList];
go
create procedure [PatientFlow].[GetKioskOrganisationList]
@KioskId	int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	org.OrganisationId,
	OrganisationName,
	org.OrganisationKey as DatabaseName
from [PatientFlow].[Organisation] org
join [PatientFlow].KioskLinkedToDetails [Kioskmaster]
on org.OrganisationId=TypeId
left join [PatientFlow].[PatientFlowUser] [user] 
on org.OrganisationId=[user].OrganisationId
where [Kioskmaster].KioskId=@KioskId
order by OrganisationName
end

