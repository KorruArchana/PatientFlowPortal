if object_id ('[PatientFlow].[SaveTPPAppointments]') is not null
	drop procedure [PatientFlow].[SaveTPPAppointments];
go

create procedure [PatientFlow].[SaveTPPAppointments]	
	@FromDate datetime,
	@OrganisationId int,
	@Status bit,
	@IsUntimed bit,
	@Appointment PatientFlow.TPPBookedAppointment readonly
as

set nocount on;
set transaction isolation level read committed;
	
begin try;
begin transaction; 

--Update patient identifier for patients

update PatientFlow.PatientIdentifier
	set
		[OrganisationId] = @OrganisationId,		
		[SystemType] = a.SystemType,
		[Value] = a.PatientIdentifierValue
from @Appointment a
	 inner join PatientFlow.PatientIdentifier p on a.PatientIdentifierValue = p.Value 
	 where p.OrganisationId = @OrganisationId and a.PatientIdentifierValue is not null;
	 	 
--insert into patient identifier for new patients

insert into PatientFlow.PatientIdentifier
(
OrganisationId,
SystemType,
Value
)
select distinct
	@OrganisationId,	
	a.SystemType,
	a.PatientIdentifierValue
from @Appointment a
	left outer join PatientFlow.PatientIdentifier p on a.PatientIdentifierValue = p.Value and p.OrganisationId = @OrganisationId
where p.Value is null;

-- Update patient details for patients who are already known.

update PatientFlow.Patient
	set
		[PatientId] = pid.PatientFlowPatientIdentifierId,
		[Title] = a.PatientTitle,
		[FirstName] = a.PatientFirstName,
		[CallingName] = a.PatientCallingName,
		[FamilyName] = a.PatientFamilyName,
		[Gender] = a.PatientGender,
		[DOB] = a.PatientDOB,
		[ModifiedBy] = a.ModifiedBy,
		[Modified] = getdate()
from @Appointment a
	join PatientFlow.PatientIdentifier pid on pid.Value = a.PatientIdentifierValue
	join PatientFlow.Patient p on pid.PatientFlowPatientIdentifierId = p.PatientId and p.OrganisationId = @OrganisationId 
	where a.PatientIdentifierValue is not null;	

update PatientFlow.Patient 
set 
		PostCode = IsNull(a.PostCode,p.PostCode),
        Email = IsNull(a.Email, p.Email),
        MobileNumber = IsNull(a.MobileNumber, p.MobileNumber),
        HomePhoneNumber =  IsNull(a.HomePhoneNumber, p.HomePhoneNumber),
        WorkPhoneNumber = IsNull(a.WorkPhoneNumber, p.WorkPhoneNumber)
from @Appointment a
	join PatientFlow.PatientIdentifier pid on pid.Value = a.PatientIdentifierValue
	join PatientFlow.Patient p on pid.PatientFlowPatientIdentifierId = p.PatientId and p.OrganisationId = @OrganisationId  


-- Insert new patients

insert into PatientFlow.Patient
(
	PatientId,
	OrganisationId,
	Title,
	FirstName,
	CallingName,
	FamilyName,
	PostCode,
	Gender,
	DOB,
	Email,
	MobileNumber,
	HomePhoneNumber,
	WorkPhoneNumber,
	ModifiedBy
)
select distinct 
	pid.PatientFlowPatientIdentifierId,
	@OrganisationId,
	a.PatientTitle,
	a.PatientFirstName,
	a.PatientCallingName,
	a.PatientFamilyName,
	a.PostCode,
	a.PatientGender,
	a.PatientDOB,
	a.Email,
	a.MobileNumber,
	a.HomePhoneNumber,
	a.WorkPhoneNumber,
	a.ModifiedBy
from @Appointment a
	join PatientFlow.PatientIdentifier pid on pid.Value = a.PatientIdentifierValue
	left join PatientFlow.Patient p on pid.PatientFlowPatientIdentifierId = p.PatientId and p.OrganisationId = @OrganisationId		
where p.PatientFlowPatientId is null;


--select distinct 
--	pid.PatientFlowPatientIdentifierId,
--	@OrganisationId,
--	a.PatientTitle,
--	a.PatientFirstName,
--	a.PatientCallingName,
--	a.PatientFamilyName,
--	a.PatientGender,
--	a.PatientDOB,
--	a.ModifiedBy
--from @Appointment a
--	join PatientFlow.PatientIdentifier pid on pid.Value = a.PatientIdentifierValue --
--	left join PatientFlow.Patient p on pid.PatientFlowPatientIdentifierId = p.PatientId and p.OrganisationId = @OrganisationId	
--where p.PatientFlowPatientId is null;


--Update member identifier for members

update PatientFlow.MemberIdentifier
	set
		[OrganisationId] = @OrganisationId,		
		[SystemType] = a.SystemType,
		[Value] = a.MemberIdentifierValue
from @Appointment a
	 inner join PatientFlow.MemberIdentifier m on a.MemberIdentifierValue = m.Value 
	 where m.OrganisationId = @OrganisationId and m.Value is not null;

--insert into member identifier for new members

insert into PatientFlow.MemberIdentifier
(
	OrganisationId,
	SystemType,
	Value
)
select distinct
	@OrganisationId,	
	a.SystemType,
	a.MemberIdentifierValue
from @Appointment a
	left outer join PatientFlow.MemberIdentifier m on a.MemberIdentifierValue = m.Value and m.OrganisationId = @OrganisationId
where m.Value is null;
	 
	 
-- Update known members.

update PatientFlow.Member
	set 
		[MemberId] = mid.PatientFlowMemberIdentifierId,
		[ModifiedBy] = a.ModifiedBy,
		[Modified] = getdate(),
		[WaitingTime] = (case
							when @IsUntimed = 0 then a.WaitingTime
							else m.[WaitingTime]
						 end)
from @Appointment a
	join PatientFlow.Memberidentifier mid on mid.Value = a.MemberIdentifierValue
	join PatientFlow.Member m on mid.PatientFlowMemberIdentifierId = m.MemberId 
where m.OrganisationId = @OrganisationId 
	and a.MemberIdentifierValue is not null;

   
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
	[MemberId] = mid.PatientFlowMemberIdentifierId,
	a.SessionHolderTitle,
	a.SessionHolderFirstName,
	a.SessionHolderLastName,
	a.ModifiedBy,	
	@OrganisationId,
	a.WaitingTime
from @Appointment a
	join PatientFlow.Memberidentifier mid on mid.Value = a.MemberIdentifierValue and mid.OrganisationId = @OrganisationId
	left join PatientFlow.Member m on mid.PatientFlowMemberIdentifierId = m.MemberId 
where m.PatientFlowMemberId is null;


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
		[Reception] = a.TPPAppointmentId,
		[AppointmentStatusId] = @Status
    from @Appointment a
	    join PatientFlow.Appointment existingAppointment on a.TPPAppointmentId = existingAppointment.Reception
		join PatientFlow.PatientIdentifier pid on pid.Value = a.PatientIdentifierValue and pid.OrganisationId = @OrganisationId
		join PatientFlow.Patient p on pid.PatientFlowPatientIdentifierId = p.PatientId and p.OrganisationId = @OrganisationId
		join PatientFlow.MemberIdentifier mid on mid.Value = a.MemberIdentifierValue and mid.OrganisationId = @OrganisationId
		join PatientFlow.Member m on mid.PatientFlowMemberIdentifierId = m.MemberId 

-- Insert new appointments

declare @MaxAppointmentId int = (select ISNULL(Max(AppointmentId), 0) from PatientFlow.Appointment a
	join PatientFlow.Member m on m.PatientFlowMemberId = a.PatientFlowMemberId
	where OrganisationId = @OrganisationId)

insert into PatientFlow.Appointment
(
	[AppointmentId],
	[AppointmentDate],	
	[PatientFlowMemberId],
	[PatientFlowPatientId],	
	[ModifiedBy],
	[SiteId],
	[Duration],
	[Reception],
	[AppointmentStatusId]
)
select
	@maxAppointmentId + a.AppointmentId,
	a.AppointmentDate,	
	m.PatientFlowMemberId,
	p.PatientFlowPatientId,	
	a.ModifiedBy,
	a.SiteId,
	a.Duration,
	a.TPPAppointmentId,
	@Status
from @Appointment a
	left outer join PatientFlow.Appointment existingAppointment on a.TPPAppointmentId = existingAppointment.Reception
	join PatientFlow.PatientIdentifier pid on pid.Value = a.PatientIdentifierValue and pid.OrganisationId = @OrganisationId
	join PatientFlow.Patient p on pid.PatientFlowPatientIdentifierId = p.PatientId and p.OrganisationId = @OrganisationId
	join PatientFlow.MemberIdentifier mid on mid.Value = a.MemberIdentifierValue and mid.OrganisationId = @OrganisationId
	join PatientFlow.Member m on mid.PatientFlowMemberIdentifierId = m.MemberId and m.OrganisationId = @OrganisationId
where existingAppointment.Reception is null ;

if(@Status = 0)
begin

/* This will delete the previous day appointments */
delete app
from PatientFlow.Appointment app
join PatientFlow.Patient pat on app.PatientFlowPatientId = pat.PatientFlowPatientId
where 
	AppointmentDate < @fromDate
	and OrganisationId = @OrganisationId 

--delete app
--from PatientFlow.Appointment app
--join PatientFlow.Patient pat on app.PatientFlowPatientId = pat.PatientFlowPatientId
--where 
--	AppointmentDate > dateadd(minute, 2, @fromDate) 
--	and OrganisationId = @OrganisationId 
--	and app.AppointmentStatusId = 0
--	and app.AppointmentId not in ( 
--	select AppointmentId 
--		from @Appointment 
--		where AppointmentDate > dateadd(minute, 2, @fromDate)
--		) 
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
		raiserror('SaveTPPAppointments : %d: %s', 16, 1, @Error, @Message);
end catch;