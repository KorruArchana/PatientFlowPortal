﻿if object_id ('[PatientFlow].[GetKiosks]') is not null
drop procedure [PatientFlow].[GetKiosks];
go

create procedure [PatientFlow].[GetKiosks] 
as
begin
	set nocount on;
	set transaction isolation level read committed;	   

	create table #KioskList
	(
		KioskId int primary key,
		KioskName varchar(100),
		Title nvarchar(100),
		KioskGuid uniqueidentifier,	
		PCName varchar(50),
		IPAddress varchar(50),
		Kioskversion varchar(50),
		[Status] varchar(50),
		StatusName varchar(50),
		Usage int,
		ConnectionGuid uniqueidentifier,		
		OrganisationName varchar(4000)
	);

	insert into #KioskList
	(
		KioskId,
		KioskName,
		Title,
		KioskGuid,
		PCName,
		IPAddress,
		Kioskversion,
		[Status],
		StatusName,
		Usage,
		ConnectionGuid
		
	)
	select  
		KioskId,   
		KioskName,
		Title,
		Kiosk.KioskGuid,
		PCName,
		IPAddress,
		Kioskversion,
		[ConnectionStatus] as [Status],
		[Status].StatusName,
		0 as Usage,
		Kiosk.ConnectionGuid		
	from [PatientFlow].Kiosk as Kiosk 
	join [PatientFlow].[Status] [Status] on Kiosk.ConnectionStatus=[Status].[StatusId]
	where KioskId is not null;	
	
	with #Temp2 as
	(
	select distinct 
		alertList.KioskId,
		OrganisationName = stuff
		(
			(
				 select distinct ', '+ cast(org.SiteNumber AS varchar(max))  
				 from  PatientFlow.KioskLinkedToDetails kld
				 join PatientFlow.Organisation org on kld.TypeId = org.OrganisationId
				 where kld.KioskId = alertList.KioskId for xml path('')  
			),1,1,'' 
		)
		from #KioskList alertList
	)


	update #KioskList
	set OrganisationName = t.OrganisationName 
	from #Temp2 t 
	join #KioskList a on  t.KioskId = a.KioskId;


	select 
		KioskId,
		KioskName,
		Title,
		KioskGuid,
		PCName,
		IPAddress,
		Kioskversion,
		[Status],
		StatusName,
		Usage,
		ConnectionGuid,
		OrganisationName
	from  #KioskList
	
   	
   	drop table #KioskList;		
end