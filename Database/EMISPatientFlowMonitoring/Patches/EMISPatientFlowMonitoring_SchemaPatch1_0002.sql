/*
Description: 
	Creating other details usage log table
Author: Archana
Patch Number: 1.0002
Dependant Patch Number: 1.0001
*/


create table PatientFlow.AdditionalUsageLog
( 
    AdditionalUsageLogId bigint identity (1, 1) not null, 
    UsageDate datetime2(7) not null,     
    KioskGuid uniqueidentifier not null, 
    UsageDetails varchar (max) not null

    constraint PK_PatientFlow_AdditionalUsageLog_AdditionalUsageLogId primary key nonclustered(AdditionalUsageLogId)
);

create clustered index IDX_PatientFlow_AdditionalUsageLog_UsageDate_KioskGuid
on PatientFlow.AdditionalUsageLog (UsageDate,KioskGuid);


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'List of all additional usage logs of Kiosk', 
@level0type = N'schema', 
@level0name = 'PatientFlow',
@level1type = N'table',  
@level1name = 'AdditionalUsageLog'; 

go

