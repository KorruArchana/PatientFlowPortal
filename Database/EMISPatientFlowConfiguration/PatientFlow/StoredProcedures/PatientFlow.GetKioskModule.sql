if object_id ('[PatientFlow].[GetKioskModule]') is not null
drop procedure [PatientFlow].[GetKioskModule];
go
create procedure [PatientFlow].[GetKioskModule]
	@KioskId int
as
begin
set nocount on;
set transaction isolation level read committed;
select 
	[Module].ModuleId,
	[Module].ModuleName,
	[Module].TranslationRefId
from [PatientFlow].[Module] [Module]
join [PatientFlow].KioskModule [KioskModule] 
on [Module].ModuleId=[KioskModule].ModuleId
where [KioskModule].KioskId=@KioskId
END
