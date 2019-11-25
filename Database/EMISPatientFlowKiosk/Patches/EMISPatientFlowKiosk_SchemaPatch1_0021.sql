/*
Description: Allowing AlertText to have non English Text
Author: Aravind
Patch Number: 1.0021
Dependant Patch Number = 1.0001
*/

alter table PatientFlow.KioskConfiguration 
alter column Value nvarchar (4000) not null

go

if object_id ('[PatientFlow].[SetKioskInitialConfiguration]') is not null
	drop procedure [PatientFlow].[SetKioskInitialConfiguration];
go

drop type [PatientFlow].[KioskConfiguration];

go

CREATE TYPE [PatientFlow].[KioskConfiguration] AS TABLE(
	[ConfigType] [varchar](100) NOT NULL,
	[KioskId] [varchar](50) NOT NULL,
	[Value] [nvarchar](4000) NOT NULL
)
GO