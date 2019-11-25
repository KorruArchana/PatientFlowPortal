create table [PatientFlow].[OrganisationAccessMapping]( 
	[AccessMappingId] int identity (1, 1) not null, 
	[UserName] varchar(128) not null,
	[OrganisationId] int not null ,
	[Modified] datetime null, 
	[ModifiedBy] varchar (50) null, 
	constraint [PK_PatientFlow_OrganisationAccessMapping_AccessMappingId] primary key clustered ( [AccessMappingId] asc), 
	constraint [FK_PatientFlow_OrganisationAccessMapping_OrganisationId] foreign key([OrganisationId]) references [PatientFlow].[Organisation] ([OrganisationId])
); 

exec sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Captures the organisation mapping details' , 
@level0type=N'SCHEMA',
@level0name=N'PatientFlow', 
@level1type=N'TABLE',
@level1name=N'OrganisationAccessMapping'


alter table [PatientFlow].[Log]
alter column [User] varchar(128)

create nonclustered index IDX_PatientFlow_Log_Level_User
    on [PatientFlow].[Log]([Level],[User]);

create nonclustered index IDX_PatientFlow_Log_Date_User
    on [PatientFlow].[Log]([Date],[User])

