alter table [PatientFlow].[Organisation]
drop column IPAddress

alter table [PatientFlow].[Patient]
drop column [Message]

alter table [PatientFlow].[Patient]
drop constraint [PK_Patient]

alter table [PatientFlow].[Patient]
drop constraint [FK_Patient_Organisation]

alter table [PatientFlow].[Patient]
add constraint [PK_PatientFlow_PatientId_OrganisationId] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC,
	[OrganisationId] ASC
)

alter table [PatientFlow].[Patient]  with check add  constraint [FK_PatientFlow_Patient_OrganisationId] foreign key([OrganisationId])
references [PatientFlow].[Organisation] ([OrganisationId])

exec sp_RENAME '[PatientFlow].[Organisation].DatabaseName' , 'OrganisationKey', 'COLUMN'

create table [PatientFlow].[PatientMessage](
	[PatientMessageId] [int] identity(1,1) not null,
	[Message] [varchar](400) not null,
	[PatientId] [int] not null,
	[OrganisationId] [int] not null,
	[ModifiedBy] [varchar](50) null,
	[Modified] [datetime] null
 constraint [PK_PatientMessageId] primary key clustered ([PatientMessageId] asc)
 );
 
alter table [PatientFlow].[PatientMessage]  add constraint [FK_PatientFlow_PatientMessage_PatientId_OrganisationId] foreign key([PatientId], [OrganisationId])
references [PatientFlow].[Patient] ([PatientId], [OrganisationId])

exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Captures the message for patients', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'PatientMessage';