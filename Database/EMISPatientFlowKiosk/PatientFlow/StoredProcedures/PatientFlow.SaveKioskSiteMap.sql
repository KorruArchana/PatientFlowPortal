if object_id ('PatientFlow.SaveKioskSiteMap') is not null
	drop procedure PatientFlow.SaveKioskSiteMap;
go

create procedure PatientFlow.SaveKioskSiteMap
   @KioskId varchar(50),
   @SiteMapList PatientFlow.KioskSiteMapList readonly 
as
begin
	set nocount on;
	set transaction isolation level read committed;

	delete from Patientflow.KioskSiteMap where KioskId = @KioskId;	
	
	insert into Patientflow.KioskSiteMap 
		(
			KioskId,
			SiteDescription,
			SiteMap
		) 	
		select 
			@KioskId,
			s.SiteDescription,
			s.SiteMap
		from @SiteMapList  s 
		
end