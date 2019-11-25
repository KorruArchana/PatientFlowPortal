if object_id ('[PatientFlow].[GetMemberForOrganisation]') is not null
drop procedure [PatientFlow].[GetMemberForOrganisation];
go
create procedure [PatientFlow].[GetMemberForOrganisation] 
	@DepartmentId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	MemberId,
	Firstname,
	Surname,
	Title,
	LoginId,
	SessionHolderId
from [PatientFlow].[Member]
where DepartmentId=@DepartmentId
end
