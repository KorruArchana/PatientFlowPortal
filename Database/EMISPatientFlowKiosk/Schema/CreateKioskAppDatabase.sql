/*
  (c) Copyright EMIS Ltd 2010

  This script will drop and re-create the Kiosk database.
  
  Note:   This script has been designed to work straight out of the box on a developers laptop
  You should alter the file paths and default sizes of the databases to appropriate levels when
  creating a database on a production system.
*/
Use master
Go

If exists( Select *
           From sysdatabases
           Where name='EMISPatientFlowKiosk' )
Begin
  Drop Database EMISPatientFlowKiosk
End
Go

Create Database EMISPatientFlowKiosk On
  Primary(
  Name = EMIS_PatientFlow_Kiosk,
  Filename = 'ParamPath\EMISPatientFlowKiosk.mdf',
  Size = 100MB,
  MaxSize = Unlimited,
  FileGrowth = 200MB
  )
Log On
  ( Name = EMIS_PatientFlow_Kiosk_Log,
  FileName = 'ParamPath\EMISPatientFlowKiosk_Log.ldf',
  Size = 10,
  MaxSize = Unlimited,
  FileGrowth = 10)
Collate SQL_Latin1_General_CP1_CI_AS
Go

Alter Database EMISPatientFlowKiosk Set
  Recovery Simple With NO_WAIT
Go

