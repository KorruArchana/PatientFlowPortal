if object_id ('[PatientFlow].[GetAppointments]') is not null
	drop procedure [PatientFlow].[GetAppointments];
go

create procedure [PatientFlow].[GetAppointments]
	@FromDate datetime, 
	@ToDate datetime,
	@OrganisationId int,
	@Status int
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	select
		 a.AppointmentId, 
		 a.Reception,
		 a.AppointmentDate,
		 a.SiteId, 
		 a.Reception,
		 a.Duration,
		 p.OrganisationId, 
		 p.PatientId, 
		 p.Title, 
		 p.FirstName, 
		 p.CallingName, 
		 p.FamilyName, 
		 p.Gender, 
		 p.PostCode, 
		 p.DOB, 
		 pid.Value as PatientIdentifier,
		 p.Email, 
		 p.MobileNumber, 
		 p.WorkPhoneNumber, 
		 p.HomePhoneNumber, 
		 m.MemberId, 
		 m.Title as MemberTitle, 
		 m.FirstName as MemberFirstName, 
		 m.LastName as MemberLastName,
		 m.WaitingTime as WaitingTime,
		 m.PracticeName as PracticeName,
		 mi.Value as Code
	from PatientFlow.Appointment as a 
	join PatientFlow.Patient as p on a.PatientFlowPatientId = p.PatientFlowPatientId
	left join PatientFlow.PatientIdentifier pid on p.PatientId = pid.PatientFlowPatientIdentifierId
	join PatientFlow.Member as m on a.PatientFlowMemberId = m.PatientFlowMemberId
	left join PatientFlow.MemberIdentifier mi on m.MemberId = mi.PatientFlowMemberIdentifierId
	where (a.AppointmentDate between @fromDate and @toDate) 
		and (p.OrganisationId = @OrganisationId) 
		and (isnull(a.AppointmentStatusId, 0) = @Status)
	          
end