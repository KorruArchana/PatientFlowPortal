if object_id('[PatientFlow].[UpdateWebUser]') is not null
drop procedure [PatientFlow].[UpdateWebUser]
go

create procedure [PatientFlow].[UpdateWebUser]
	@Username varchar(50),
	@Password varchar(50),
	@IpAddress varchar(100),
	@OrganisationId varchar(100),
	@DatabaseName varchar(100),
	@SupplierId varchar(200)
as
begin
	
	set nocount on;
	set  transaction isolation level read committed;

	update [PatientFlow].[PatientFlowUser]
	set
		Username=@Username,
		[Password]=@Password,
		IPAddress=@IpAddress,
		DatabaseName=@DatabaseName,
		SupplierId=@SupplierId
	where Organisationid=@OrganisationId
end
