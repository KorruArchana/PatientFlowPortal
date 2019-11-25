if object_id ('[PatientFlow].[GetAppointmentSessions]') is not null
	drop procedure [PatientFlow].[GetAppointmentSessions];
go

create procedure [PatientFlow].[GetAppointmentSessions] 
	@FromDate datetime, 
	@ToDate datetime,
	@SlotTypeId varchar(100) = null,
	@OrganisationId int
as
begin
	
set nocount on;
set transaction isolation level read committed;

	select    
		a.[Date] as AppointmentSessionDate,
		a.StartTime, 
		a.EndTime, 
		a.SlotLength, 
		a.TotalSlots, 
		a.BookedSlots, 
		a.AvailableSlots, 
		a.SlotTypeId, 
		m.MemberId as SessionHolderId,
		a.SiteId, 
		m.Title as MemberTitle, 
		m.FirstName as MemberFirstName, 
		m.LastName as MemberLastName, 
		a.SessionId,
		a.SiteName
    from PatientFlow.AppointmentSession as a  
	join PatientFlow.Member as m 
		 on a.PatientFlowMemberId = m.PatientFlowMemberId
    where (@SlotTypeId is null or a.SlotTypeId = @SlotTypeId) 
	and (a.Date between @fromDate and @toDate) 
	and (m.OrganisationId = @OrganisationId) 
end
