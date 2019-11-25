if object_id ('[PatientFlow].[GetAuditTrail]') is not null
	drop procedure [PatientFlow].[GetAuditTrail];
go

create procedure [PatientFlow].[GetAuditTrail]
	@KioskId int,
	@StartDate varchar(100),
	@EndDate varchar(100)
as
begin
set nocount on;
set transaction isolation level read committed;

with #Temp as
(
	select ReportString from 
	(
		values 
			('Booked%'),
			('%SurveyDone%'),
			('Arrived%'),
			('BarcodeArrival%'),
			('%Demographic Details%'),
			('%Doctor Divert%'),
			('%Wrong Location%'),
			('%PF-NAA%')
     ) V (ReportString)
) 

select 
	[UsageLogId] as LogId,
	[Date],
	[Level],
	[Message] 
from [PatientFlow].[UsageLog] as L
join [PatientFlow].[Kiosk] as K 
	on cast(K.KioskGuid AS varchar(max)) = L.[User] 
where k.KioskId=@KioskId
and cast([date] as date) >= cast(@StartDate as date)
and cast([date] as date) <= cast(@EndDate as date) 
and exists (select 1 from #Temp temp where l.Message like temp.ReportString)
order by [UsageLogId] desc

end