/*
Description: Added two new columns for Forced Survey and Skip Questions
Author: Aravind
Reviewer: Sathish
Patch Number: 1.0026
Dependant Patch Number = 1.0022
*/



alter table PatientFlow.Kiosk 
add ShowDemographicDetails bit null


alter table PatientFlow.Kiosk 
add DemographicDetailsDuration int null

go

create table PatientFlow.DemographicDetailsType
(
	[DemographicDetailsTypeId] int not null identity(1,1), 
	[DemographicDetailsTypeName] varchar(30) not null,     
    constraint [PK_PatientFlow_DemographicDetailsType_DemographicDetailsTypeId] primary key ([DemographicDetailsTypeId]),
    constraint [UQ_PatientFlow_DemographicDetailsType_DemographicDetailsTypeName] unique ([DemographicDetailsTypeName])
  );


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'List of all Demographic Details Type Shown in Kiosk', 
@level0type = N'schema', @level0name = 'PatientFlow',
@level1type = N'table',  @level1name = 'DemographicDetailsType'; 

go

insert into PatientFlow.DemographicDetailsType values('Name');
insert into PatientFlow.DemographicDetailsType values('GP Practice Name');
insert into PatientFlow.DemographicDetailsType values('Postcode');
insert into PatientFlow.DemographicDetailsType values('Email Id');
insert into PatientFlow.DemographicDetailsType values('Mobile');
insert into PatientFlow.DemographicDetailsType values('Telephone');


go

create table PatientFlow.KioskDemographicDetailsType
(
	[KioskId] int not null, 
	[DemographicDetailsTypeId] int not null,     
    constraint [PK_PatientFlow_KioskDemographicDetailsType_KioskId_DemographicDetailsTypeId] primary key clustered ([KioskId] , [DemographicDetailsTypeId]), 
	constraint [FK_PatientFlow_KioskDemographicDetailsType_KioskId] foreign key ([KioskId]) references [PatientFlow].[Kiosk] ([KioskId]),
    constraint [FK_PatientFlow_KioskDemographicDetailsType_DemographicDetailsTypeId] foreign key ([DemographicDetailsTypeId]) references [PatientFlow].[DemographicDetailsType] ([DemographicDetailsTypeId])
);


exec sys.sp_addextendedproperty 
@name = N'MS_Description', 
@value = N'Mapping between Kiosk and DemographicDetailsType', 
@level0type = N'schema', @level0name = 'PatientFlow',
@level1type = N'table',  @level1name = 'KioskDemographicDetailsType'; 

go