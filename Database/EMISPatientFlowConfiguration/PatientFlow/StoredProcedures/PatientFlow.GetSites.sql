if object_id ('[PatientFlow].[GetSites]') is not null
drop procedure [PatientFlow].[GetSites];
go

create procedure [PatientFlow].[GetSites]
@OrganisationId int
as
begin
set nocount on;
set transaction isolation level read committed;
select
	SiteId,
	SiteName 
from [PatientFlow].[Site] as s 
where s.OrganisationId=@OrganisationId
order by SiteName 
end
