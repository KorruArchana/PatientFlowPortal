if object_id ('PatientFlow.GetKioskUsageLog') is not null
	drop procedure PatientFlow.GetKioskUsageLog;
go

create procedure PatientFlow.GetKioskUsageLog 
	@KiosksList PatientFlow.KioskGuidList readonly
as
begin

	set nocount on;
	set transaction isolation level read committed;
	
	select
		KioskGuid,   
		count(usagelog.UsageLogId) as Usage				
	from [PatientFlow].UsageLog usagelog
	join @KiosksList on usagelog.KioskGuid = value
	where usagelog.UsageDate > (cast(getdate() as date))
	group by KioskGuid

end
