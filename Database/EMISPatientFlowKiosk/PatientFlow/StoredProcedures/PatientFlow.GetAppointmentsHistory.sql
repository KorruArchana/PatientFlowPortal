if object_id ('[PatientFlow].[GetAppointmentsHistory]') is not null
	drop procedure [PatientFlow].[GetAppointmentsHistory];
go

create procedure [PatientFlow].[GetAppointmentsHistory]	
	@FromDate datetime, 
	@ToDate datetime,
	@OrganisationId int,
	@Status int,
	@PageNo int,
    @PageSize int,
    @TotalCount bigint output
as
begin
	set nocount on;
	set transaction isolation level read committed;
	
	if @Status <> -1	
	begin	
		select	@TotalCount = Count(*)  
		from	PatientFlow.Appointment  a join PatientFlow.Patient p
			on a.PatientFlowPatientId = p.PatientFlowPatientId
		where	(cast(a.AppointmentDate as date) >= cast(@fromDate as date) 
				 and cast(a.AppointmentDate as date) <= cast(@toDate as date)) 
				 and	(p.OrganisationId = @OrganisationId )
				 and isnull(a.AppointmentStatusId, 0) = @Status;

		 select 
			RowNo,
			AppointmentId, 
			AppointmentDate,
			SessionHolderId, 
			OrganisationId, 
			PatientId as PatientId,
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
			MemberId, 
			MemberTitle, 
			MemberFirstName, 
			MemberLastName,
			WaitingTime,
			AppointmentStatus 
	from
		(
			select  Row_number() over( Order by Appointment.AppointmentDate desc) as RowNo,
					Appointment.AppointmentId, 
					Appointment.AppointmentDate,
					Member.MemberId as SessionHolderId, 
					Member.OrganisationId, 
					Patient.PatientId as PatientId,
					Patient.Title, 
					Patient.FirstName, 
					Patient.CallingName, 
					Patient.FamilyName, 
					Patient.Gender, 
					Patient.PostCode, 
					Patient.DOB, 
					Patient.Email, 
					Patient.MobileNumber, 
					Patient.WorkPhoneNumber, 
					Patient.HomePhoneNumber, 
					Member.MemberId, 
					Member.Title as MemberTitle, 
					Member.FirstName as MemberFirstName, 
					Member.LastName as MemberLastName,
					Member.WaitingTime as WaitingTime,
					isnull(S.AppointmentStatusDesc, 'Booked') as AppointmentStatus
			from    PatientFlow.Appointment 
					inner join 	PatientFlow.Patient
					on Appointment.PatientFlowPatientId = Patient.PatientFlowPatientId
					inner join	PatientFlow.Member
					on Appointment.PatientFlowMemberId = Member.PatientFlowMemberId and Member.OrganisationId = Patient.OrganisationId
					left outer join PatientFlow.AppointmentStatus as S
					on Appointment.AppointmentStatusId = S.AppointmentStatusId
			where   (cast(Appointment.AppointmentDate as date) >= cast(@fromDate as date)
			         and cast(Appointment.AppointmentDate as date) <= cast(@toDate as date)) 
			         and (Patient.OrganisationId = @OrganisationId)
			         and isnull(Appointment.AppointmentStatusId,0) = @Status) 
			as TBL
		 where TBL.RowNo between ((@PageNo - 1) * @PageSize) + 1 and (@PageNo * @PageSize);  
	end
	else
	begin
		select @TotalCount = Count(*)  
		from  PatientFlow.Appointment 
		inner join PatientFlow.Patient
		on Appointment.PatientFlowPatientId = Patient.PatientFlowPatientId 
		where   (cast(Appointment.AppointmentDate as date) >= cast(@fromDate as date) 
				and cast(Appointment.AppointmentDate as date) <= cast(@toDate as date))
				and Patient.OrganisationId = @OrganisationId;

		select 
			RowNo,
			AppointmentId, 
			AppointmentDate,
			SessionHolderId, 
			OrganisationId, 
			PatientId as PatientId,
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
			MemberId, 
			MemberTitle, 
			MemberFirstName, 
			MemberLastName,
			WaitingTime,
			AppointmentStatus
		from
		(
			select    Row_number() over( Order by Appointment.AppointmentDate desc) as RowNo,
				Appointment.AppointmentId, 
				Appointment.AppointmentDate,
				Member.MemberId as SessionHolderId, 
				Member.OrganisationId, 
				Patient.PatientId as PatientId,
				Patient.Title, 
				Patient.FirstName, 
				Patient.CallingName, 
				Patient.FamilyName, 
				Patient.Gender, 
				Patient.PostCode, 
				Patient.DOB, 
				Patient.Email, 
				Patient.MobileNumber, 
				Patient.WorkPhoneNumber, 
				Patient.HomePhoneNumber, 
				Member.MemberId, 
				Member.Title as MemberTitle, 
				Member.FirstName as MemberFirstName, 
				Member.LastName as MemberLastName,
				Member.WaitingTime as WaitingTime,
				Member.PracticeName as PracticeName,
				isnull(S.AppointmentStatusDesc,'Booked') as AppointmentStatus
			from  PatientFlow.Appointment 
				  inner join PatientFlow.Patient 
				  on Appointment.PatientFlowPatientId = Patient.PatientFlowPatientId
				  inner join PatientFlow.Member 
				  on Appointment.PatientFlowMemberId = Member.PatientFlowMemberId and Member.OrganisationId = Patient.OrganisationId
				  left outer join PatientFlow.AppointmentStatus as S 
				  on Appointment.AppointmentStatusId = S.AppointmentStatusId
			where  (cast(Appointment.AppointmentDate as date) >= cast(@fromDate as date) 
			        and cast(Appointment.AppointmentDate as date) <= cast(@toDate as date)) 
			        and (Patient.OrganisationId = @OrganisationId) ) 
			as TBL
		where TBL.RowNo between ((@PageNo - 1) * @PageSize) + 1 and (@PageNo * @PageSize);
	end                     
end
