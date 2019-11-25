if object_id ('[PatientFlow].[GetSessionHolderId_Organisation]') is not null
	drop procedure [PatientFlow].[GetSessionHolderId_Organisation];
go

create procedure [PatientFlow].[GetSessionHolderId_Organisation]
	@OrganisationId int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	select SessionHolderId from [PatientFlow].[Member] 
	where OrganisationId=@OrganisationId;
end
