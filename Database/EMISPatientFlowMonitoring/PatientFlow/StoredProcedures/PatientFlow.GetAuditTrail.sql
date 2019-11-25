if object_id ('PatientFlow.GetAuditTrail') is not null
	drop procedure PatientFlow.GetAuditTrail;
go

create procedure PatientFlow.GetAuditTrail
	@KioskGuid uniqueidentifier,
	@StartDate date,
	@EndDate date
as
begin

	set nocount on;
	set transaction isolation level read committed;
	
	set @EndDate = Cast(DATEADD(day, 1, @EndDate) As date);
	
	select
		UsageLogId as LogId,
		UsageDate,
		UsageDetails
	from PatientFlow.UsageLog
	where UsageDate >= @StartDate
	and UsageDate <= @EndDate
	and KioskGuid =@KioskGuid
	
	union
	
	select 
		AdditionalUsageLogId as LogId,
		UsageDate,
		UsageDetails
	from PatientFlow.AdditionalUsageLog
	where UsageDate >= @StartDate
	and UsageDate <= @EndDate
	and KioskGuid =@KioskGuid
	order by UsageDate desc

end