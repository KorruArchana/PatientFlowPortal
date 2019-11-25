if object_id ('[PatientFlow].[AddWebUser]') is not null
	drop procedure [PatientFlow].[AddWebUser];
go

create procedure [PatientFlow].[AddWebUser]
	@Userame varchar(50),
	@Password varchar(50),
	@IpAddress varchar(100),
	@SupplierId varchar(100),
	@OrganisationId varchar(100),
	@DatabaseName varchar(100)
as
begin
set nocount on;
set transaction isolation level read committed
begin try
begin transaction
insert into [PatientFlow].[PatientFlowUser]
(

UserName,
[Password],
SupplierId,
IPAddress,
OrganisationId,
DatabaseName

)
values
(

@Userame,
@Password,
@SupplierId,
@IpAddress,
@OrganisationId,
@DatabaseName

);
commit transaction
select 1
end try

begin catch
select 0
rollback transaction
end catch

end
