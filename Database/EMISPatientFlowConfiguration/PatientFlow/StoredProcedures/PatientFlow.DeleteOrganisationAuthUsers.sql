if object_id ('PatientFlow.DeleteOrganisationAuthUsers') is not null
	drop procedure PatientFlow.DeleteOrganisationAuthUsers;
go

create procedure PatientFlow.DeleteOrganisationAuthUsers
	@UserName varchar(128)
as
begin
set nocount on;
set transaction isolation level read committed;


delete from [PatientFlow].[OrganisationAccessMapping]
	where UserName=@UserName

end