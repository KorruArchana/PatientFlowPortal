if object_id('[PatientFlow].[SaveLog]') is not null
drop procedure [PatientFlow].[SaveLog]
go

create procedure [PatientFlow].[SaveLog]
	@Data PatientFlow.LogEntry readonly
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
    
insert into [PatientFlow].[UsageLog]  
(
	[Date],		
	[Level],
	[Logger],
	[User],
	[Message]		
)
select 
	LogDate, 		
	LogLevel,
	p.Logger,
	LogUser,
	LogMessage	from @data p 
	left outer join PatientFlow.UsageLog ulog
	on p.LogMessage = ulog.Message and p.LogDate = ulog.Date 
	where ulog.UsageLogId is null 
	and exists ( select 1 from #Temp temp where p.LogMessage like temp.ReportString)    
end



