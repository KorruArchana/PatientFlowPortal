if object_id ('[PatientFlow].[GetLogs]') is not null
	drop procedure [PatientFlow].[GetLogs];
go

create procedure [PatientFlow].[GetLogs]
	@ProductKey varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;
    declare @LastRowID bigint;
    
    select  @LastRowID = LastRowID 
	from PatientFlow.SynchronisationLog 
	where (SyncType = 1) and ProductKey=@ProductKey;
    
    
with FilteredReportString as
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
    
        
select top 50 Row_number() over( order by LogId asc) as RowNo,
				LogId,
				[Date], 
				Thread, 
				[Level],
				Logger, 
				[User],
				[Message], 
				Exception 
		from  [PatientFlow].[Log] l
		join FilteredReportString temp on l.Message like temp.ReportString
		where (LogId > isnull(@LastRowID, 0))
   
end
