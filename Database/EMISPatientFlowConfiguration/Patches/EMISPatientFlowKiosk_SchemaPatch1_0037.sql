/*
	Description: Adding Mulitple Site Map to Kiosk
	Author: Aravind
	Patch Number: 1.0037
	Dependant Patch Number = 1.0034
*/


alter table PatientFlow.Kiosk
drop column SiteMap;

go

create table PatientFlow.KioskSiteMap
(
	KioskSiteMapId smallint identity(1,1) not null,
	KioskId int not null,
	SiteDescription varchar(50) not null,
	SiteMap varbinary (max) not null
	constraint PK_PatientFlow_KioskSiteMap_KioskSiteMapId primary key nonclustered (KioskSiteMapId),
	constraint FK_PatientFlow_KioskSiteMap_KioskId foreign key ([KioskId]) references [PatientFlow].[Kiosk] ([KioskId]),
	constraint UQ_PatientFlow_KioskSiteMap_KioskId_KioskSiteMapId unique clustered (KioskId,KioskSiteMapId)
);

go

exec sys.sp_addextendedproperty
	@name=N'MS_Description',
	@value= N'Allowing Kiosk to hold multiple site map images and name of site',
	@level0type=N'SCHEMA',
	@level0name=N'PatientFlow',
	@level1type=N'TABLE',
	@level1name=N'KioskSiteMap';
	
go


create type PatientFlow.KioskSiteMapList as table
(
	KioskSiteMapListId int primary key,
	SiteDescription varchar(50) not null,
	SiteMap varbinary (max) not null
);
