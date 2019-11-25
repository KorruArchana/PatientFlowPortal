if object_id('[PatientFlow].[AddAlertLinkedToDepMemDetails]') is not null
drop Procedure [PatientFlow].[AddAlertLinkedToDepMemDetails]
go


create procedure [PatientFlow].[AddAlertLinkedToDepMemDetails]
@AlertId int,
@Departments as [PatientFlow].[List] readonly,
@Members as [PatientFlow].[List] readonly,
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

begin transaction
	delete from Patientflow.AlertsLinkedToDepMem where AlertId=@AlertId

	insert into Patientflow.AlertsLinkedToDepMem
	(
		Alertid,
		LinkTypeId,
		TypeId,
		ModifiedBy,
		Modified
	)
	(
	select
		@AlertId,
		@DeptLinkTypeId,
		Id,
		@ModifiedBy,
		GetDate()
	from @Departments
	);
  
	insert into Patientflow.AlertsLinkedToDepMem
	(
		Alertid,
		LinkTypeId,
		TypeId,
		ModifiedBy,
		Modified
	)
	(
	select 
		@AlertId,
		@MemberLinkTypeId,
		Id,
		@ModifiedBy,
		getdate()
	from @Members
	);
 
commit transaction
end try

begin catch
	declare @Error int, @Message varchar(4000)		
		select 
			@Error = error_number(), 
			@Message = error_message()
		if xact_state() <> 0 
			begin
				rollback transaction
			end
		raiserror('AddAlertLinkedToDepMemDetails : %d: %s', 16, 1, @error, @message) ;
	rollback transaction
end catch
end


