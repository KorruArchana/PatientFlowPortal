/*
  This script will drop and re-create the SignalR database.
  
  Note:   This script has been designed to work straight out of the box on a developers laptop
  You should alter the file paths and default sizes of the databases to appropriate levels when
  creating a database on a production system.
*/
use master;
go

if exists( select *
           from sysdatabases
           where name='EMISPatientFlowSignalR' )
begin
  drop database EMISPatientFlowSignalR;
end
go

create database EMISPatientFlowSignalR on
  Primary(
  Name = EMIS_PatientFlow_SignalR,
  Filename = 'ParamPath\EMISPatientFlowSignalR.mdf',
  Size = 100MB,
  MaxSize = Unlimited,
  FileGrowth = 200MB
  )
Log On
  ( Name = EMIS_PatientFlow_SignalR_Log,
  FileName = 'ParamPath\EMISPatientFlowSignalR_Log.ldf',
  Size = 10,
  MaxSize = Unlimited,
  FileGrowth = 10)
collate SQL_Latin1_General_CP1_CI_AS
go

alter database EMISPatientFlowSignalR set
  recovery simple with NO_WAIT;
go

alter database EMISPatientFlowSignalR set enable_broker;
go