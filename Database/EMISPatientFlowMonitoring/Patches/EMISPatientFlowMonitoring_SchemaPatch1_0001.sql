/*
Description: 
	Creating UsageLog and Log tables.
	Creating LogEntry and KioskGuidList as types.
Author: Archana
Patch Number: 1.0001
Dependant Patch Number: 0.0000
*/

create schema PatientFlow
go

create table PatientFlow.UsageLog
( 
    UsageLogId bigint identity (1, 1) not null, 
    UsageDate datetime2(7) not null,     
    KioskGuid uniqueidentifier not null, 
    UsageDetails varchar (max) not null

    constraint PK_PatientFlow_UsageLog_UsageLogId primary key nonclustered(UsageLogId)
 );

create clustered index IDX_PatientFlow_UsageLog_UsageDate_KioskGuid
on PatientFlow.UsageLog (UsageDate,KioskGuid);


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'List of all usage logs of Kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'UsageLog'; 

go

create table PatientFlow.Log
( 
    LogId int Identity(1,1) not null,
    [Date] datetime2(7) not null, 
    Thread varchar (255) not null, 
    [Level] varchar (50) not null, 
    Logger varchar (255) not null, 
    [User] varchar (100) not null, 
    [Message] varchar (max) not null, 
    Exception varchar (max) null,
    
    constraint [PK_PatientFlow_Log_LogId] primary key nonclustered(LogId)
); 

create clustered index IDX_PatientFlow_Log_Date_User on PatientFlow.Log([Date], [User])


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'List of all logs of Kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'Log'; 

go

create type PatientFlow.LogEntry as table
(
	LogId int not null  primary key,
    LogDate datetime not null,	
	LogUser uniqueidentifier not null,
	LogMessage varchar (max) not null
);
go

create type PatientFlow.KioskGuidList as table
(
	Value uniqueidentifier not null primary key
);

go
