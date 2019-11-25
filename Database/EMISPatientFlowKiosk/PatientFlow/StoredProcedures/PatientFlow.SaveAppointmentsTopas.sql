if object_id('[PatientFlow].[SaveAppointmentsTopas]') is not null
	drop procedure [PatientFlow].[SaveAppointmentsTopas];
go

create procedure [PatientFlow].[SaveAppointmentsTopas] 
	@FromDate datetime,
	@OrganisationId int,
	@Appointment PatientFlow.BookedAppointment readonly
as
set nocount on;
set transaction isolation level read committed;
	
begin try
		;
	
begin transaction; 

	-- Update patient details for patients who are already known.
update piv
	set Value = a.PatientIdentifierValue,
		CallingName = a.PatientFirstName + a.PatientCallingName + a.patientFamilyName,
		DOB = a.PatientDOB,
		SystemType = a.SystemType
from @Appointment a
left join PatientFlow.PatientIdentifier piv
on
(
	a.PatientIdentifierValue is null
	and piv.[CallingName] = a.PatientFirstName + a.PatientCallingName + a.patientFamilyName
	and piv.DOB = a.PatientDOB
)
left join PatientFlow.PatientIdentifier piv1
on a.PatientIdentifierValue = piv1.Value 
 join PatientFlow.Patient p
on p.PatientId = case 
				when piv1.PatientFlowPatientIdentifierId is not null
					  then piv1.PatientFlowPatientIdentifierId
				when piv.PatientFlowPatientIdentifierId is not null
	   				  then piv.PatientFlowPatientIdentifierId
				end
where p.OrganisationId = @OrganisationId
and a.SystemType = 5;

	 
insert into PatientFlow.PatientIdentifier
(
	Value,
	CallingName,
	DOB,
	SystemType
)
select distinct 
		a.PatientIdentifierValue,
		a.PatientFirstName + a.PatientCallingName + a.patientFamilyName,
		a.PatientDOB,
		a.SystemType
from @Appointment a
	left outer join PatientFlow.PatientIdentifier piv
		on a.PatientIdentifierValue = piv.Value 
	left outer join PatientFlow.PatientIdentifier piv1
		on a.PatientIdentifierValue is null
			and a.PatientFirstName + a.PatientCallingName + a.patientFamilyName = piv1.[CallingName]
			and a.PatientDOB = piv1.DOB
	left join PatientFlow.Patient p
		on p.PatientId = case 
				when piv.PatientFlowPatientIdentifierId is not null
			  then piv.PatientFlowPatientIdentifierId 
				when piv1.PatientFlowPatientIdentifierId is not null
			  then piv1.PatientFlowPatientIdentifierId 
		 end   
		and p.OrganisationId = @OrganisationId
	where a.SystemType = 5
		and (
			piv.PatientFlowPatientIdentifierId is null
			and piv1.PatientFlowPatientIdentifierId is null
			);

update PatientFlow.Patient
	set PatientId = case 
			when piv.PatientFlowPatientIdentifierId is not null
						 then piv.PatientFlowPatientIdentifierId 
			when piv1.PatientFlowPatientIdentifierId is not null
						 then piv1.PatientFlowPatientIdentifierId
			end,
		[Title] = a.PatientTitle,
		[FirstName] = a.PatientFirstName,
		[CallingName] = a.PatientCallingName,
		[FamilyName] = a.PatientFamilyName,
		[Gender] = a.PatientGender,
		[PostCode] = a.PatientPostCode,
		[DOB] = a.PatientDOB,
		[Email] = a.PatientEmail,
		[MobileNumber] = a.PatientMobileNumber,
		[WorkPhoneNumber] = a.PatientWorkPhoneNumber,
		[HomePhoneNumber] = a.PatientHomePhoneNumber,
		[ModifiedBy] = a.ModifiedBy,
		[Modified] = getdate()
from @Appointment a
	 left join PatientFlow.PatientIdentifier piv
		on (
				a.PatientIdentifierValue is null
				and piv.[CallingName] = a.PatientFirstName + a.PatientCallingName + a.patientFamilyName
				and piv.DOB = a.PatientDOB
				)
	 left join PatientFlow.PatientIdentifier piv1
	 on a.PatientIdentifierValue = piv1.Value 
	 join PatientFlow.Patient p
		on p.PatientId = case 
				when piv1.PatientFlowPatientIdentifierId is not null
		  then piv1.PatientFlowPatientIdentifierId
				when piv.PatientFlowPatientIdentifierId is not null
	   	 then piv.PatientFlowPatientIdentifierId
	 end
	where p.OrganisationId = @OrganisationId
		and a.SystemType = 5;

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
	case 
		when piv1.PatientFlowPatientIdentifierId is not null
			  then piv1.PatientFlowPatientIdentifierId
		when piv.PatientFlowPatientIdentifierId is not null 
	   		  then piv.PatientFlowPatientIdentifierId
		end,
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
	left join PatientFlow.PatientIdentifier piv
		on (
				a.PatientIdentifierValue is null
				and piv.[CallingName] = a.PatientFirstName + a.PatientCallingName + a.patientFamilyName
				and piv.DOB = a.PatientDOB
				)
	left join PatientFlow.PatientIdentifier piv1
		 on a.PatientIdentifierValue = piv1.Value 
	left outer join PatientFlow.Patient p
		on p.PatientId = case 
				when piv1.PatientFlowPatientIdentifierId is not null
			  then piv1.PatientFlowPatientIdentifierId
				when piv.PatientFlowPatientIdentifierId is not null
	   		  then piv.PatientFlowPatientIdentifierId
		 end
		 and p.OrganisationId = @OrganisationId
	where p.PatientId is null 
		 and a.SystemType = 5;
		 
insert into PatientFlow.MemberIdentifier 
(
	Value,
	OrganisationId,
	SystemType
)
	select distinct 
		a.MemberIdentifierValue,
		@OrganisationId,
		a.SystemType
from @Appointment a
	left outer join PatientFlow.MemberIdentifier m
	on a.MemberIdentifierValue = m.Value
	and m.OrganisationId = @OrganisationId
where m.Value is null;
		 
-- Update known members.
update PatientFlow.Member
	set [Title] = a.SessionHolderTitle,
		[FirstName] = a.SessionHolderFirstName,
		[LastName] = a.SessionHolderLastName,
		[ModifiedBy] = a.ModifiedBy,
		PracticeName = a.PracticeName,
		[Modified] = getdate()
from @Appointment a
	 inner join PatientFlow.MemberIdentifier mi
		on a.MemberIdentifierValue = mi.Value
			and mi.OrganisationId = @OrganisationId
	 inner join PatientFlow.Member m		
  	 on mi.PatientFlowMemberIdentifierId = m.MemberId
	 and m.OrganisationId = @OrganisationId;
  
-- Insert new members.
insert into [PatientFlow].[Member]
 (
	[MemberId],
	[Title],
	[FirstName],
	[LastName],
	[ModifiedBy],
	PracticeName,	
	[OrganisationId]
)
select distinct 
	mi.PatientFlowMemberIdentifierId,
	a.SessionHolderTitle,
	a.SessionHolderFirstName,
	a.SessionHolderLastName,
	a.ModifiedBy,
	null,	
	@OrganisationId
from @Appointment a
	 inner join PatientFlow.MemberIdentifier mi
		on a.MemberIdentifierValue = mi.Value
			and mi.OrganisationId = @OrganisationId
	left outer join PatientFlow.Member m
	 on mi.PatientFlowMemberIdentifierId = m.MemberId
	and m.OrganisationId = @OrganisationId
where m.MemberId is null;

-- Update existing appointments
update PatientFlow.Appointment
	set [AppointmentDate] = a.AppointmentDate,		
		[PatientFlowMemberId] = m.PatientFlowMemberId,
		[PatientFlowPatientId] = p.PatientFlowPatientId,	
		[ModifiedBy] = a.ModifiedBy,
		[Modified] = getdate(),
		[SiteId] = a.SiteId,
		[Reception] = a.Reception
    from @Appointment a
		inner join PatientFlow.Appointment existingAppointment
			on a.AppointmentId = existingAppointment.AppointmentId 
		inner join PatientFlow.MemberIdentifier mi
		on a.MemberIdentifierValue = mi.Value
			and mi.OrganisationId = @OrganisationId
		inner join patientflow.Member m 
		on mi.PatientFlowMemberIdentifierId = m.MemberId
			and m.OrganisationId = @OrganisationId
		left join PatientFlow.PatientIdentifier piv
		on (
				a.PatientIdentifierValue is null
				and piv.[CallingName] = a.PatientFirstName + a.PatientCallingName + a.patientFamilyName
				and piv.DOB = a.PatientDOB
				)
		left join PatientFlow.PatientIdentifier piv1
			 on a.PatientIdentifierValue = piv1.Value 
		inner join PatientFlow.Patient p
		on p.PatientId = case 
				when piv1.PatientFlowPatientIdentifierId is not null
				 then piv1.PatientFlowPatientIdentifierId
				when piv.PatientFlowPatientIdentifierId is not null
	   			then piv.PatientFlowPatientIdentifierId
			 end
		 and p.OrganisationId = @OrganisationId
	where a.systemtype = 5;

-- Insert new appointments
insert into PatientFlow.Appointment 
(
	[AppointmentId],
	[AppointmentDate],	
	[PatientFlowMemberId],
	[PatientFlowPatientId],	
	[ModifiedBy],
	[SiteId],
	[Reception]
)
select
	a.AppointmentId,
	a.AppointmentDate,	
	m.PatientFlowMemberId,
	p.PatientFlowPatientId,	
	a.ModifiedBy,
	a.SiteId,
	a.Reception
from @Appointment a
	left outer join PatientFlow.Appointment existingAppointment
		on a.AppointmentId = existingAppointment.AppointmentId
		inner join PatientFlow.MemberIdentifier mi
		on a.MemberIdentifierValue = mi.Value
			and mi.OrganisationId = @OrganisationId
		inner join patientflow.Member m 
		on mi.PatientFlowMemberIdentifierId = m.MemberId
			and m.OrganisationId = @OrganisationId
		left join PatientFlow.PatientIdentifier piv
		on (
				a.PatientIdentifierValue is null
				and piv.[CallingName] = a.PatientFirstName + a.PatientCallingName + a.patientFamilyName
				and piv.DOB = a.PatientDOB
				)
		left join PatientFlow.PatientIdentifier piv1
			 on a.PatientIdentifierValue = piv1.Value 
		inner join PatientFlow.Patient p
		on p.PatientId = case 
				when piv1.PatientFlowPatientIdentifierId is not null
				 then piv1.PatientFlowPatientIdentifierId
				when piv.PatientFlowPatientIdentifierId is not null
	   			then piv.PatientFlowPatientIdentifierId
			 end	
		and p.OrganisationId = @OrganisationId
	where existingAppointment.AppointmentId is null
		and a.systemtype = 5

delete app
from PatientFlow.Appointment app
join PatientFlow.Patient pat on app.PatientFlowPatientId = pat.PatientFlowPatientId
where AppointmentDate < @fromDate and OrganisationId = @OrganisationId


commit transaction;
end try

begin catch	;

	declare @Error int,
		@Message varchar(4000);

		select 
			@Error = error_number(), 
			@Message = error_message();

	if xact_state() <> 0
	begin
			rollback transaction;
		end

	raiserror (
			'SaveAppointments : %d: %s',
			16,
			1,
			@Error,
			@Message
			);
end catch;