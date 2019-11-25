if object_id ('PatientFlow.GetAlertsByMemberOrganisation') is not null
	drop procedure PatientFlow.GetAlertsByMemberOrganisation;
go

create procedure PatientFlow.GetAlertsByMemberOrganisation
	@MemberId integer	
as
begin
set nocount on;
set transaction isolation level read committed;

select distinct 
      alert.AlertId,
      AlertText   
from PatientFlow.Alert alert
join PatientFlow.AlertLinkToOrganisation org on alert.AlertId = org.AlertId 
join PatientFlow.Member mem on org.OrganisationId = mem.OrganisationId
where mem.MemberId = @MemberId

end 
