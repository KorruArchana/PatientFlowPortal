if object_id ('[PatientFlow].[GetKioskSiteMap]') is not null
	drop procedure [PatientFlow].[GetKioskSiteMap];
go

create procedure [PatientFlow].[GetKioskSiteMap]
   @KioskId varchar(50)
as
begin
	set nocount on;
	set transaction isolation level read committed;

	select  
		KioskSiteMapId, 
		SiteDescription, 
		SiteMap
	from  [PatientFlow].[KioskSiteMap]
	where  KioskId = @KioskId;
end
