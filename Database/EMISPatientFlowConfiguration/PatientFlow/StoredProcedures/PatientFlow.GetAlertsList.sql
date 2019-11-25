if object_id ('[PatientFlow].[GetAlertsList]') is not null
	drop procedure [PatientFlow].[GetAlertsList];
go

create procedure [PatientFlow].[GetAlertsList]
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	 [AlertId],
	 [AlertType],
	 [AlertText],
	 [Gender],
	 [Age1],
	 [Operation],
	 [Age2],
	 Alerts.[OrganisationId],
	 Organisation.OrganisationName
from [PatientFlow].[Alert] Alerts
left join [PatientFlow].[Organisation] Organisation 
on Alerts.OrganisationId = Organisation.OrganisationId
order by [AlertText]
end

