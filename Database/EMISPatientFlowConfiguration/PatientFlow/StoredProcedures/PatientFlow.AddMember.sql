if object_id ('[PatientFlow].[AddMember]') is not null
	drop procedure [PatientFlow].[AddMember];
go

create procedure [PatientFlow].[AddMember]
	@Firstname varchar(100),
	@Surname varchar(100)=null,
	@Title varchar(50),
	@LoginId varchar(100)=null,
	@WorkStartTime datetime=null,
	@WorkEndTime datetime=null,
	@DepartmentId int,
	@GPCode varchar(100)=null,
	@SecurityGroup varchar(100)=null,
	@StaffCategory varchar(100)=null,
	@SessionHolderId int,
	@ModifiedBy varchar(50)=null
as
begin
set nocount on;
set transaction isolation level read committed
insert into [PatientFlow].[Member]
(
Firstname,
Surname,
Title,
LoginId,
WorkStartTime,
WorkEndTime,
DepartmentId,
GPCode ,
SecurityGroup ,
StaffCategory ,
SessionHolderId,
ModifiedBy,
Modified
)
values
(
@Firstname,
@Surname,
@Title,
@LoginId,
@WorkStartTime,
@WorkEndTime,
@DepartmentId,
@GPCode ,
@SecurityGroup ,
@StaffCategory ,
@SessionHolderId,
@ModifiedBy,
getdate()
)

declare @MemberId int
set @MemberId= (select cast(scope_identity() AS int))

End





