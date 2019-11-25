if object_id ('[PatientFlow].[SaveAppointmentSessions]') is not null
	drop procedure [PatientFlow].[SaveAppointmentSessions];
go

create procedure [PatientFlow].[SaveAppointmentSessions]	
	@FromDate datetime, 
	@ToDate datetime,
	@OrganisationId int,
	@ModifiedBy varchar(50),
	@AppointmentSession PatientFlow.MemberAppointmentSession readonly
as
begin	
	set nocount on;	
	set transaction isolation level read committed;
	begin try;
    begin transaction; 
    
    -- Update known members.

update PatientFlow.Member
	set 
		[Title] = a.SessionHolderTitle,
		[FirstName] = a.SessionHolderFirstName,
		[LastName] = a.SessionHolderLastName,
		[ModifiedBy] = @ModifiedBy,
		[Modified] = getdate()
from @AppointmentSession a
join PatientFlow.Member m	
	 on a.SessionHolderId = m.MemberId
and m.OrganisationId = @OrganisationId;
  
insert into [PatientFlow].[Member]  
(
	[MemberId],
	[Title],
	[FirstName],
	[LastName],
	[ModifiedBy],
	OrganisationId
)
select distinct 
	a.SessionHolderId,
	a.SessionHolderTitle,
	a.SessionHolderFirstName,
	a.SessionHolderLastName,
	@ModifiedBy,
	@OrganisationId
from @AppointmentSession a
left outer join PatientFlow.Member m
				on a.SessionHolderId = m.MemberId
				and m.OrganisationId = @OrganisationId
where m.MemberId is null;

 -- Update existing appointmentsession
update [PatientFlow].[AppointmentSession]
			   set  [Date] = a.[Date],
					[StartTime] = a.StartTime,
					[EndTime] = a.EndTime,
					[SlotLength] = a.SlotLength,
					[TotalSlots] = a.TotalSlots,
					[BookedSlots] = a.BookedSlots,
					[AvailableSlots] = a.AvailableSlots,
					[SlotTypeId] = a.SlotTypeId,
					[PatientFlowMemberId] = m.PatientFlowMemberId,
					[SiteId] = a.SiteId,
					SiteName = a.SiteName,					
					[ModifiedBy] = @ModifiedBy,
					[Modified] = getdate()
from @AppointmentSession a
join PatientFlow.AppointmentSession aps
	 on a.SessionId = aps.SessionId 
join patientflow.Member m 
	 on a.SessionHolderId = m.MemberId and m.OrganisationId = @OrganisationId;
		
		 
 -- Insert appointmentsession   
 insert into [PatientFlow].[AppointmentSession]
(
	[SessionId],
	[Date],
	[StartTime],
	[EndTime],
	[SlotLength],
	[TotalSlots],
	[BookedSlots],
	[AvailableSlots],
	[SlotTypeId],
	[PatientFlowMemberId],
	[SiteId],
	SiteName,
	[ModifiedBy],
	[Modified]
)
select 
	a.[SessionId],
	a.[Date],
	a.[StartTime],
	a.[EndTime],
	a.[SlotLength],
	a.[TotalSlots],
	a.[BookedSlots],
	a.[AvailableSlots],
	a.[SlotTypeId],
	m.PatientFlowMemberId,
	a.[SiteId],
	a.SiteName,
	@ModifiedBy,
	getdate()
from  @AppointmentSession a
left join PatientFlow.AppointmentSession aps
	 on a.SessionId = aps.SessionId 
join patientflow.Member m 
	 on a.SessionHolderId = m.MemberId and m.OrganisationId = @OrganisationId
where aps.SessionId is null;	   

delete sess from PatientFlow.AppointmentSession sess
join PatientFlow.Member mem
	 on sess.[PatientFlowMemberId] = mem.[PatientFlowMemberId]
where ([Date] BETWEEN @fromDate and @toDate) 
        and (OrganisationId = @OrganisationId) 
        and SessionId not in ( 
		select SessionId 
		from @AppointmentSession 
		where ([Date] BETWEEN @fromDate and @toDate)); 

commit transaction;

end try
begin catch;
		declare @Error int, @Message varchar(4000);		
		select 
			@Error = error_number(), 
			@Message = error_message();
		if xact_state() <> 0 begin
			rollback transaction;
		end
		raiserror('SaveAppointmentSessions : %d: %s', 16, 1, @Error, @Message);
end catch;		
end

