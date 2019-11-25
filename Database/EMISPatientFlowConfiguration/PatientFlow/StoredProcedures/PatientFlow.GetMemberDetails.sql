if object_id ('[PatientFlow].[GetMemberDetails]') is not null
drop procedure [PatientFlow].[GetMemberDetails];
go
create procedure [PatientFlow].[GetMemberDetails]
	@MemberId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	MemberId,
	Firstname,
	Surname,
	Title,
	member.DepartmentId,
	DepartmentName,
	organisation.OrganisationId,
	organisation.OrganisationName
from [PatientFlow].[Member] member
join [PatientFlow].Department department on member.DepartmentId=department.DepartmentId
join [PatientFlow].Organisation organisation on department.OrganisationId=organisation.OrganisationId
where MemberId=@MemberId
end



