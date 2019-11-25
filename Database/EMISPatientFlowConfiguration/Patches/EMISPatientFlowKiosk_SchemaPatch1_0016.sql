
alter table [PatientFlow].[Member]
add  IsDivertSet bit null

create table PatientFlow.Site
(
    [SiteId] [int] identity(1,1) not null,
	[SiteDBID] [bigint] not null,
	[SiteName] [varchar](200) null,
	[OrganisationId] [int] not null,
    constraint PK_PatientFlow_Site_SiteId primary key clustered (SiteId),
    constraint FK_PatientFlow_Site_OrganisationId foreign key (OrganisationId) references [PatientFlow].[Organisation] (OrganisationId)
);

alter table [PatientFlow].[KioskLinkedToDetails] add [SiteId] [int] null 
alter table [PatientFlow].[KioskLinkedToDetails]
add constraint FK_PatientFlow_KioskLinkedToDetails_SiteId foreign key(SiteId) references [PatientFlow].[Site](SiteId)

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Capture the site details of organisation', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Site';

create type [PatientFlow].[OrganisationSites] as table(
	[OrganisationId] [int] not null,
	[SiteDBID] [bigint] not null,
	[SiteName] [varchar](100) not null
)
go

create type [PatientFlow].[KioskLinkOrganisationSites] as table(
	[OrganisationId] [int] not null,
	[SiteId] [int] null
)
go


