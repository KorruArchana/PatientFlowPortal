if object_id ('[PatientFlow].[SaveAppointments]') is not null
	drop procedure [PatientFlow].[SaveAppointments];
go

create procedure [PatientFlow].[SaveAppointments]	
	@FromDate datetime,
	@OrganisationId int,
	@Status bit,
	@IsUntimed bit,
	@Appointment PatientFlow.BookedAppointment readonly
as

set nocount on;
set transaction isolation level read committed;
	
begin try;
begin transaction; 

-- Update patient details for patients who are already known.

update PatientFlow.Patient
	set
		[Title] = a.PatientTitle,
		[FirstName] = a.PatientFirstName,
		[CallingName] = a.PatientCallingName,
		[FamilyName] = a.PatientFamilyName,
		[Gender] = a.PatientGender,
		[PostCode] = a.PatientPostCode,
		[DOB] = a.PatientDOB,
		[Email] = (case
					  when @IsUntimed = 0 then a.PatientEmail
					  else p.[Email]
				   end),
		[MobileNumber] = (case
							when @IsUntimed = 0 then a.PatientMobileNumber
							else p.[MobileNumber]
						  end),
		[WorkPhoneNumber] = a.PatientWorkPhoneNumber,
		[HomePhoneNumber] = a.PatientHomePhoneNumber,
		[ModifiedBy] = a.ModifiedBy,
		[Modified] = getdate()
from @Appointment a
	 inner join PatientFlow.Patient p  on a.PatientId = p.PatientId and   p.OrganisationId = @OrganisationId 
	 where a.PatientId is not null;

-- Insert patient details for new patients.

insert into PatientFlow.Patient
(
	PatientId,
	OrganisationId,
	Title,
	FirstName,
	CallingName,
	FamilyName,
	Gender,
	PostCode,
	DOB,
	Email,
	MobileNumber,
	WorkPhoneNumber,
	HomePhoneNumber,
	ModifiedBy
)
select distinct 
	a.PatientId,
	@OrganisationId,
	a.PatientTitle,
	a.PatientFirstName,
	a.PatientCallingName,
	a.PatientFamilyName,
	a.PatientGender,
	a.PatientPostCode,
	a.PatientDOB,
	a.PatientEmail,
	a.PatientMobileNumber,
	a.PatientWorkPhoneNumber,
	a.PatientHomePhoneNumber,
	a.ModifiedBy
from @Appointment a
	left outer join PatientFlow.Patient p on a.PatientId = p.PatientId and p.OrganisationId = @OrganisationId
where p.PatientFlowPatientId is null;
	 
-- Update known members.

update PatientFlow.Member
	set 
		[Title] = a.SessionHolderTitle,
		[FirstName] = a.SessionHolderFirstName,
		[LastName] = a.SessionHolderLastName,
		[ModifiedBy] = a.ModifiedBy,
		[Modified] = getdate(),
		[WaitingTime] = (case
							when @IsUntimed = 0 then a.WaitingTime
							else m.[WaitingTime]
						 end)
from @Appointment a
	 inner join PatientFlow.Member m on a.SessionHolderId = m.MemberId and m.OrganisationId = @OrganisationId;
   
-- Insert new members.

insert into [PatientFlow].[Member]  
(
	[MemberId],
	[Title],
	[FirstName],
	[LastName],
	[ModifiedBy],	
	[OrganisationId],
	[WaitingTime]
)
select distinct 
	a.SessionHolderId,
	a.SessionHolderTitle,
	a.SessionHolderFirstName,
	a.SessionHolderLastName,
	a.ModifiedBy,	
	@OrganisationId,
	a.WaitingTime
from @Appointment a
	left outer join PatientFlow.Member m  on a.SessionHolderId = m.MemberId	and m.OrganisationId = @OrganisationId
where m.MemberId is null;

-- Update existing appointments

update PatientFlow.Appointment
	set [AppointmentDate] = a.AppointmentDate,		
		[PatientFlowMemberId] = m.PatientFlowMemberId,
		[PatientFlowPatientId] = p.PatientFlowPatientId,	
		[ModifiedBy] = a.ModifiedBy,
		[Modified] = getdate(),
		[SiteId] = a.SiteId,
		[Duration] = (case
						when @IsUntimed = 1 then a.Duration
						else existingAppointment.[Duration]
					   end),
		[AppointmentStatusId] = @Status
    from @Appointment a
	    inner join PatientFlow.Appointment existingAppointment on a.AppointmentId = existingAppointment.AppointmentId 
		inner join patientflow.Member m on 	a.SessionHolderId = m.MemberId and m.OrganisationId = @OrganisationId
		inner join PatientFlow.Patient p on a.PatientId = p.PatientId and p.OrganisationId = @OrganisationId;
		
-- Insert new appointments
insert into PatientFlow.Appointment
(
	[AppointmentId],
	[AppointmentDate],	
	[PatientFlowMemberId],
	[PatientFlowPatientId],	
	[ModifiedBy],
	[SiteId],
	[Duration],
	[AppointmentStatusId]
)
select
	a.AppointmentId,
	a.AppointmentDate,	
	m.PatientFlowMemberId,
	p.PatientFlowPatientId,	
	a.ModifiedBy,
	a.SiteId,
	a.Duration,
	@Status
from @Appointment a
	left outer join PatientFlow.Appointment existingAppointment on a.AppointmentId = existingAppointment.AppointmentId
	join patientflow.Member m on a.SessionHolderId = m.MemberId and m.OrganisationId = @OrganisationId
	join PatientFlow.Patient p on a.PatientId = p.PatientId and p.OrganisationId = @OrganisationId
where existingAppointment.AppointmentId is null ;

if(@Status = 0)
begin

/* This will delete the previous day appointments */
delete app
from PatientFlow.Appointment app
join PatientFlow.Patient pat on app.PatientFlowPatientId = pat.PatientFlowPatientId
where 
	AppointmentDate < @fromDate
	and OrganisationId = @OrganisationId 

delete app
from PatientFlow.Appointment app
join PatientFlow.Patient pat on app.PatientFlowPatientId = pat.PatientFlowPatientId
where 
	AppointmentDate > dateadd(minute, 20, @fromDate) 
	and OrganisationId = @OrganisationId 
	and app.AppointmentStatusId = 0
	and app.AppointmentId not in ( 
	select AppointmentId 
		from @Appointment 
		where AppointmentDate > dateadd(minute, 20, @fromDate)
		) 
end

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
		raiserror('SaveAppointments : %d: %s', 16, 1, @Error, @Message);
end catch;