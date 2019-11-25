if object_id ('[PatientFlow].[GetAlerts]') is not null
	drop procedure [PatientFlow].[GetAlerts];
go

create procedure [PatientFlow].[GetAlerts]	
as
begin

set nocount on;
set transaction isolation level read committed;

	create table #AlertList
	(
		AlertId int primary key,
		AlertType int,
		AlertText nvarchar(4000),
		AlertDisplayFormatTypeId smallint,	
		OrganisationId int,
		OrganisationName nvarchar(4000),
		KioskName varchar(4000),
		Operation varchar(50),
		Age1 int,
		Age2 int,
		Gender varchar(50),
		DepartmentName varchar(4000),
		MemberName varchar(4000)
	);

	insert into #AlertList 
	(
		AlertId,
		AlertType,
		AlertText,
		AlertDisplayFormatTypeId,	
		OrganisationId,
		OrganisationName,
		KioskName,
		Operation,
		Age1,
		Age2,
		Gender,
		DepartmentName,
		MemberName 
	)	
	select 
		Alerts.AlertId,
		AlertType,
		AlertText,
		AlertDisplayFormatTypeId,
		Link.OrganisationId,
		org.OrganisationName,										
		null,
		Operation,
		Age1,
		Age2,
		Gender,
		null,
		null
	from PatientFlow.Alert Alerts
	join PatientFlow.AlertLinkToOrganisation Link on Alerts.AlertId=Link.AlertId
	join PatientFlow.Organisation org on Link.OrganisationId = org.OrganisationId
	where Link.OrganisationId > 0;

	with #Temp2 as
	(
		select distinct
			org1.AlertId,
			KioskName = stuff
			(
				(
					select 
						distinct ', '+ cast(kiosk.KioskName AS varchar(max))  
					from  PatientFlow.AlertLinkToOrganisation org
					left join PatientFlow.AlertLinkToKiosk kioskalert on org.AlertId = kioskalert.AlertId
					left join PatientFlow.Kiosk kiosk on kioskalert.KioskId = kiosk.KioskId 
					where org.AlertId = org1.AlertId for xml path('')   
				),1,1,'' 
			),
			DepartmentName = stuff
			(
				(
					select 
						distinct ', '+ cast(d.DepartmentName AS varchar(max))  
					from PatientFlow.AlertLinkToOrganisation as al
					join PatientFlow.AlertsLinkedToDepMem as m on al.AlertId=m.AlertId
					join PatientFlow.Department as d on m.TypeId=d.DepartmentId and m.LinkTypeId=2
					where al.AlertId = org1.AlertId  for xml path('')
				 ),1,1,''
			),
			MemberName = stuff
			(
				(
					select 
						distinct ', '+ cast((mem.Surname+','+mem.Firstname+'('+mem.Title+')') AS varchar(max))  
					from PatientFlow.AlertLinkToOrganisation as al
					join PatientFlow.AlertsLinkedToDepMem as m on al.AlertId=m.AlertId
					join PatientFlow.Member as mem on m.TypeId=mem.MemberId and m.LinkTypeId=3
					where al.AlertId = org1.AlertId for xml path('')
				 ),1,1,''
			)
		from PatientFlow.AlertLinkToOrganisation org1  
	)

	update #AlertList
	set 
		KioskName= isnull(t.KioskName,'All Kiosks'),
		DepartmentName= isnull(t.DepartmentName,'') ,
		MemberName=isnull(t.MemberName,'')
	from #Temp2 t 
	join #AlertList a on  t.AlertId = a.AlertID;

	update #AlertList
	set 
		KioskName = REPLACE(KioskName, '&amp;', '&')
	where KioskName LIKE '%&amp;%';
	 
	select 	
		AlertId,
		AlertType,
		AlertText,
		OrganisationId,
		OrganisationName,
		AlertDisplayFormatTypeId,
		KioskName,
		Operation,
		Age1,
		Age2,
		Gender,
		DepartmentName,
		MemberName	 
	from #AlertList
			
	end

	drop table #AlertList;		

go

exec sys.sp_addextendedproperty 
	@name = N'UnitTestException_TestForInvalidDataTypeUse',
	@value = N'Alert Message should support multiple language text.',
	@level0type = N'SCHEMA',
	@level0name = 'PatientFlow',
	@level1type = N'PROCEDURE',
	@level1name = 'GetAlerts'