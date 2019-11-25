/*
Description: Allowing the user link Alerts to Kiosk
Author: Aravind
Patch Number: 1.0033
Dependant Patch Number: 1.0001
*/

alter table [patientflow].[AlertLinktoOrganisation]
add kioskguid uniqueidentifier null


create type [patientflow].[stringlist] as table(
	value nvarchar(4000) null
)

alter table PatientFlow.Kiosk
add KioskVersion varchar(50) null,
LastStatusUpdate datetime null

go

update PatientFlow.Kiosk
set ShowDateTime = 1,
ShowSysInfo = 1,
ShowNewsletter = 0,
DisplayLanguageFlag = 1

go


create table [PatientFlow].[UsageLog] 
( 
    [UsageLogId] bigint identity (1, 1) not null, 
    [Date] datetime not null,     
    [Level] varchar (50) not null, 
    [Logger] varchar (255) not null, 
    [User] varchar (max) not null, 
    [Message] varchar (4000) not null    

  constraint [PK_PatientFlow_UsageLog_UsageLogId] primary key ([UsageLogId]));


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'List of all usage logs of Kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'UsageLog'; 

go
