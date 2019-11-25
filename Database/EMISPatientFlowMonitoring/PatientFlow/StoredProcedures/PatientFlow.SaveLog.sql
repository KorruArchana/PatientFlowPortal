if object_id('PatientFlow.SaveLog') is not null
drop procedure PatientFlow.SaveLog
go

create procedure PatientFlow.SaveLog
	@Data PatientFlow.LogEntry readonly
as
begin

	set nocount on;
    set transaction isolation level read committed;
        
    
	with FilteredReportString as
	(
		select ReportString from 
		(
			values 
				('Booked%'),
				('%SurveyDone%'),
				('Arrived%'),
				('BarcodeArrival%')
		 ) V (ReportString)
	)    
	    
	insert into PatientFlow.UsageLog
	(
		UsageDate,		
		KioskGuid,
		UsageDetails
	)
	select 
		LogDate, 		
		LogUser,
		LogMessage
	from @data p 
	left outer join PatientFlow.UsageLog ulog
		on p.LogMessage = ulog.UsageDetails and p.LogDate = ulog.UsageDate 
	join FilteredReportString temp on p.LogMessage like temp.ReportString
	where ulog.UsageLogId is null;
	

	with FilteredAdditionalReportString as
	(
		select ReportString from 
		(
			values 
				('%Demographic Details%'),
				('%Doctor Divert%'),
				('%Wrong Location%'),
				('%PF-NAA%')
		 ) V (ReportString)
	)
	
	insert into PatientFlow.AdditionalUsageLog
	(
		UsageDate,		
		KioskGuid,
		UsageDetails
	)
	select 
		LogDate, 		
		LogUser,
		LogMessage
	from @data p 
	left outer join PatientFlow.AdditionalUsageLog oulog
		on p.LogMessage = oulog.UsageDetails and p.LogDate = oulog.UsageDate 
	join FilteredAdditionalReportString temp on p.LogMessage like temp.ReportString
	where oulog.AdditionalUsageLogId is null
		 
end

