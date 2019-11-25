if object_id ('[PatientFlow].[AddDivert]') is not null
	drop procedure [PatientFlow].[AddDivert];
go

create procedure [PatientFlow].[AddDivert]
	@DivertMessage varchar(250),
	@OrganisationId int,
	@Departments As [PatientFlow].[List] readonly,
	@Members As [PatientFlow].[List] readonly,
    @ModifiedBy varchar(50)
as
begin
set nocount on;
set transaction isolation level read committed

begin try

declare @DeptLinkTypeId int
set @DeptLinkTypeId=2
declare @MemberLinkTypeId int
set @MemberLinkTypeId=3

begin Transaction

insert into [PatientFlow].[Divert]
(
DivertMessage,
OrganisationId,
ModifiedBy,
Modified	
	
)
values
(
@DivertMessage,
@OrganisationId,
@ModifiedBy ,
getdate()
	
)

declare @DivertId int
set @DivertId= (select cast(scope_identity() as int))
 
delete from Patientflow.DivertLinkedToDetail where DivertId=@DivertId

insert into Patientflow.DivertLinkedToDetail
(

DivertId,
LinkTypeId,
TypeId,
ModifiedBy,
Modified

)
(

select 
@DivertId,
@DeptLinkTypeId,
Id,
@ModifiedBy,
getdate() 
from @Departments

)
  
insert into Patientflow.DivertLinkedToDetail
(

DivertId,
LinkTypeId,
TypeId,
ModifiedBy,
Modified

)
(

select 
@DivertId,
@MemberLinkTypeId,
Id,
@ModifiedBy,
getdate() 
from @Members

)
commit transaction
select @DivertId
end Try

begin catch
select 0           
rollback transaction
end catch

end











