if object_id ('[PatientFlow].[AddPatient]') is not null
	drop procedure [PatientFlow].[AddPatient];
go

create procedure [PatientFlow].[AddPatient]
	@Firstname varchar(100),
	@Surname varchar(100)=null,
	@PatientId int,
	@OrganisationId int,
	@Dob date=null,
	@Message varchar(400),
	@Modifiedby Varchar(100)
as
begin
set nocount on;
set transaction isolation level read committed
begin try
begin transaction
insert into [PatientFlow].[Patient]
(
Firstname,
Surname,
PatientId,
OrganisationId,
[Message],
DOB,
ModifiedBy,
Modified 
)
values
(
@Firstname,
@Surname,
@PatientId ,
@OrganisationId ,
@Message,
@Dob,
@ModifiedBy ,
getdate()
)
declare @Id int
set @Id= (select cast(scope_identity() as int))

commit transaction
select @Id
end Try

begin catch
select 0         
rollback transaction
end catch

end




