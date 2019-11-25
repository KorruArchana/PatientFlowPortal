if object_id(' [PatientFlow].[GetAuditTrailForSyncService]') is not null
drop Procedure  [PatientFlow].[GetAuditTrailForSyncService]
go

create procedure [PatientFlow].[GetAuditTrailForSyncService]
	@OrganisationId int,
	@StartDate datetime,
	@EndDate datetime
as
begin
set nocount on;
set transaction isolation level read committed;
select 
 [LogId],
 [Date],
 [Level],
 [Message],
 [Exception] 
  from [PatientFlow].[Log] as L
inner join [PatientFlow].[SyncService] as S 
on cast(s.ProductKey AS varchar(max)) = L.[User] 
where S.OrganisationId=@OrganisationId
and cast([date] as date) >= cast(@StartDate as date)
and cast([date] as date) <= cast(@EndDate as date) 
order by [date] desc
end









