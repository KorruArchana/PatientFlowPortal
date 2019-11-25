if object_id ('[PatientFlow].[GetMembers]') is not null
drop procedure [PatientFlow].[GetMembers];
go

create procedure [PatientFlow].[GetMembers] 
as
begin
set nocount on;
set transaction isolation level read committed;


select 
	MemberId,
	Firstname,
	Surname,
	Title,
	SessionHolderId,
	IsDivertSet,
	DepartmentName,
	OrganisationName,
	dept.DepartmentId,
	org.OrganisationId
from [PatientFlow].[Member] mem		
join PatientFlow.Department dept on dept.DepartmentId = mem.DepartmentId
join PatientFlow.Organisation org on org.OrganisationId = mem.OrganisationId
order by OrganisationName,DepartmentName,Firstname	
	
end

