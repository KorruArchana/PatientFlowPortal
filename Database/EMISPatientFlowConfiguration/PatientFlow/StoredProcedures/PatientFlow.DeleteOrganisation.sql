if object_id ('[PatientFlow].[DeleteOrganisation]') is not null
	drop procedure [PatientFlow].[DeleteOrganisation];
go

create procedure [PatientFlow].[DeleteOrganisation]
	@OrganisationId int	
as
begin
set nocount on;
set transaction isolation level read committed
begin try
begin transaction

	delete from [PatientFlow].PatientFlowUser
	where OrganisationId=@OrganisationId

	delete Member from [PatientFlow].Member
	inner join [PatientFlow].[Department] Department
		on Member.DepartmentId=Department.DepartmentId
	where Department.OrganisationId=@OrganisationId

	delete from patientflow.[Site] 
	where OrganisationId=@OrganisationId
	    
	--delete node Kiosk
	delete from [PatientFlow].Department
	where Department.OrganisationId=@OrganisationId
	 
	delete from [PatientFlow].SyncService
	where OrganisationId=@OrganisationId

	delete from [PatientFlow].[PatientMessage] 
	where OrganisationId=@OrganisationId

	delete from [patientFlow].[Patient]
	where OrganisationId= @OrganisationId

	delete from [PatientFlow].[AppointmentSlotType]
	where OrganisationId=@OrganisationId

	delete from [PatientFlow].[OrganisationAccessMapping]
	where OrganisationId=@OrganisationId

	declare @List PatientFlow.List;
	insert into @List (Id)
	(
		select AlertId from PatientFlow.AlertLinkToOrganisation
		where OrganisationId=@OrganisationId
	)

	delete from PatientFlow.AlertLinkToKiosk
	where AlertId in (select Id from @List)

	delete from PatientFlow.AlertsLinkedToDepMem
	where AlertId in (select Id from @List)

	delete from PatientFlow.AlertLinkToOrganisation
	where OrganisationId=@OrganisationId
	
	delete Alert from patientFlow.Alert Alert
	where AlertId in (select Id from @List)

	delete kld from patientFlow.KioskLinkedToDetails kld
	where kld.TypeId = @OrganisationId
	
	delete from [PatientFlow].[Organisation] 
	where OrganisationId=@OrganisationId
	
	declare @result bit =1 
	select @result as result


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
		raiserror('DeleteOrganisation : %d: %s', 16, 1, @error, @message) ;
	rollback transaction
end catch
end
