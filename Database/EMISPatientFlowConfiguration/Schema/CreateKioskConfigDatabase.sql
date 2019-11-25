/*
  (c) Copyright EMIS Ltd 2010

  This script will drop and re-create the Kiosk Configuration database.
  
  Note:   This script has been designed to work straight out of the box on a developers laptop
  You should alter the file paths and default sizes of the databases to appropriate levels when
  creating a database on a production system.
*/
Use master
Go

If exists( Select *
           From sysdatabases
           Where name='EMISPatientFlowConfiguration' )
Begin
  Drop Database EMISPatientFlowConfiguration
End
Go

Create Database EMISPatientFlowConfiguration On
  Primary(
  Name = EMISPatientFlowConfiguration,
  Filename = 'ParamPath\EMISPatientFlowConfiguration.mdf',
  Size = 100MB,
  MaxSize = Unlimited,
  FileGrowth = 200MB
  )
Log On
  ( Name = EMIS_PatientFlow_Cfg_Log,
  FileName = 'ParamPath\EMISPatientFlowConfiguration.ldf',
  Size = 10,
  MaxSize = Unlimited,
  FileGrowth = 10)
Collate SQL_Latin1_General_CP1_CI_AS
Go

Alter Database EMISPatientFlowConfiguration Set
  Recovery Simple With NO_WAIT
Go
