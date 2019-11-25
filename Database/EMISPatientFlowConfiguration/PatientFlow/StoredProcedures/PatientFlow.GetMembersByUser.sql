if object_id ('PatientFlow.GetMembersByUser') is not null
	drop procedure PatientFlow.GetMembersByUser;
go

create procedure PatientFlow.GetMembersByUser 
	@User varchar(128)	
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
join PatientFlow.OrganisationAccessMapping mapping on Org.OrganisationId = mapping.OrganisationId	
where mapping.UserName = @User
order by OrganisationName,DepartmentName,Firstname	
	
end

