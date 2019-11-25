/*
Description: Added two new columns for Forced Survey and Skip Questions
Author: Aravind
Reviewer: Sathish
Patch Number: 1.0025
Dependant Patch Number = 1.0022
*/

create table PatientFlow.KioskSessionHolder
(
	[KioskId] int not null, 
	[SessionHolderId] int not null,     
    [LastModifiedDate] datetime constraint [DF_PatientFlow_KioskSessionHolder_LastModifiedDate] default ( getdate()) null, 
    constraint [PK_PatientFlow_KioskSessionHolder_KioskId_SessionHolderId] primary key clustered ([KioskId] , [SessionHolderId]), 
    constraint [FK_PatientFlow_KioskSessionHolder_KioskId] foreign key ([KioskId]) references [PatientFlow].[Kiosk] ([KioskId])
);


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Mapping between Kiosk and SessionHolder', 
@level0type = N'schema', @level0name = 'PatientFlow',
@level1type = N'table',  @level1name = 'KioskSessionHolder'; 

go