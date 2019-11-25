if object_id ('PatientFlow.GetAlertsByMember') is not null
	drop procedure PatientFlow.GetAlertsByMember;
go

create procedure PatientFlow.GetAlertsByMember
	@MemberId integer	
as
begin

declare @DepartmentId int = (select DepartmentId from PatientFlow.Member where memberId = @MemberId)
declare @OrganisationId int = (select OrganisationId from PatientFlow.Member where memberId = @MemberId)

set nocount on;
set transaction isolation level read committed;

create table #AlertsData 
(
	AlertId int primary key,
	AlertText nvarchar(4000),
	IsOrganisationAlert int,
	IsDeparmentAlert int,
	IsMemberAlert int 
)

insert into #AlertsData
(
	AlertId,
	AlertText,
	IsOrganisationAlert,
	IsDeparmentAlert,
	IsMemberAlert 
)
select distinct
	a.AlertId,
	AlertText,
	(case when aldm.LinkTypeId is null then 1 else 0 end) as IsOrganisationAlert,
	0 as IsDeparmentAlert,
	0 as IsMemberAlert 
from PatientFlow.Alert a
join PatientFlow.AlertLinkToOrganisation alo on a.AlertId = alo.AlertId
left join PatientFlow.AlertsLinkedToDepMem aldm on aldm.AlertId = a.AlertId
where alo.OrganisationId = @OrganisationId


update ad
set IsDeparmentAlert = 1
from 
(
select distinct
	alert.AlertId	
from PatientFlow.Alert alert
join PatientFlow.AlertLinkToOrganisation org on alert.AlertId = org.AlertId
join PatientFlow.AlertsLinkedToDepMem aldm on alert.AlertId = aldm.AlertId 
and aldm.LinkTypeId = 2 and aldm.typeid = @DepartmentId
)tbl
join #AlertsData ad
on ad.AlertId = tbl.AlertId

update ad
set IsMemberAlert = 1
from 
(
select distinct
	alert.AlertId	
from PatientFlow.Alert alert
join PatientFlow.AlertLinkToOrganisation org on alert.AlertId = org.AlertId
join PatientFlow.AlertsLinkedToDepMem aldm on alert.AlertId = aldm.AlertId 
and aldm.LinkTypeId = 3 and aldm.typeid = @MemberId
)tbl
join #AlertsData ad 
on ad.AlertId = tbl.AlertId

select 
	AlertId,
	AlertText,
	IsOrganisationAlert,
	IsDeparmentAlert,
	IsMemberAlert
from #AlertsData

drop table #AlertsData;

end