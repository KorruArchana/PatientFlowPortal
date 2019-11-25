exec sp_rename '[PatientFlow].[PK_PatientMessageId]','PK_PatientFlow_PatientMessage_PatientMessageId','object'
exec sp_rename '[PatientFlow].[PK_PatientFlow_PatientId_OrganisationId]','PK_PatientFlow_Patient_PatientId_OrganisationId','object'

alter table [PatientFlow].[Alert]
drop constraint FK_PatientFlow_Alert_OrganisationId

alter table [PatientFlow].[Alert]
drop column OrganisationId

alter table [PatientFlow].[Kiosk] 
drop column LinkTypeId


alter table PatientFlow.Patient
alter column dob varchar(150)

alter table [PatientFlow].[PatientFlowUser]
drop constraint [PK_PatientFlow_PatientFlowUser_UserId]
alter table [PatientFlow].[PatientFlowUser] drop column [UserID];


alter table [PatientFlow].[PatientFlowUser]
add [UserId] int identity(1,1) not null ;
	
alter table PatientFlow.PatientFlowUser add constraint PK_PatientFlow_PatientFlowUser_UserId
primary key clustered (UserId)
