/*
	Description: Alert Display Format based on Configuration
	Author: Aravind
	Patch Number: 1.0036
	Dependant Patch Number = 1.0034
*/

create table PatientFlow.AlertDisplayFormatType
(
	AlertDisplayFormatTypeId smallint,
	DisplayFormatDescription varchar(30),
	constraint PK_PatientFlow_AlertDisplayFormatType_AlertDisplayFormatTypeId primary key (AlertDisplayFormatTypeId),
	constraint UQ_PatientFlow_AlertDisplayFormatType_DisplayFormatDescription unique (DisplayFormatDescription)
);

go

exec sys.sp_addextendedproperty
	@name=N'MS_Description',
	@value= N'List of Different Display Types used to show the Alert for Users in Kiosk',
	@level0type=N'SCHEMA',
	@level0name=N'PatientFlow',
	@level1type=N'TABLE',
	@level1name=N'AlertDisplayFormatType';
	
go

insert into PatientFlow.AlertDisplayFormatType (AlertDisplayFormatTypeId,DisplayFormatDescription) values (1,'Standard'),(2,'Directional'), (3,'Important')

go

alter table PatientFlow.Alert
add AlertDisplayFormatTypeId smallint constraint DF_PatientFlow_Alert_AlertDisplayFormatTypeId default (1) not null,
constraint FK_PatientFlow_Alert_AlertDisplayFormatTypeId foreign key (AlertDisplayFormatTypeId) references PatientFlow.AlertDisplayFormatType (AlertDisplayFormatTypeId)
  
go