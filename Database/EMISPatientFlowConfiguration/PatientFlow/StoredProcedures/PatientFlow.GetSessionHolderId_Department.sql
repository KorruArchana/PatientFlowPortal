if object_id ('[PatientFlow].[GetSessionHolderId_Department]') is not null
	drop procedure [PatientFlow].[GetSessionHolderId_Department];
go

create procedure [PatientFlow].[GetSessionHolderId_Department]
	@DepartmentId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select SessionHolderId from [PatientFlow].[Member] 
	where DepartmentId=@DepartmentId;
end
