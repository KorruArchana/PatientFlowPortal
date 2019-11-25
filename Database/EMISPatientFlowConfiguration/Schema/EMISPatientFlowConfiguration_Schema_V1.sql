use EMISPatientFlowConfiguration

go

if not exists 
(
  select * 
  from sys.schemas 
  where name = 'Patching'
)
begin
  declare @SQL varchar(100) = 'create schema Patching'
  execute (@SQL)
end

go

create table Patching.Audit
(
  Id integer identity(1,1) not null constraint PK_Patching_Audit_Id primary key,
  PatchNumber decimal (9,4) null,
  DatetimePatched datetime not null constraint DF_Patching_Audit_DateTimePatched default getdate(),
  Information varchar(max) null,
  PatchType TinyInt Not Null Constraint DF_Patching_Audit_PatchType Default 0,
  ReleaseVersion varchar(15) null,
  constraint CK_Patching_Audit_PatchType check (PatchType In (0, 1, 2, 3))
)

execute sp_addextendedproperty
	N'MS_Description', 'Patch Type.
	0 = None
	1 = Database build system UI
	2 = MKBRuntime CareRecord code update
	3 = SDS',
	N'schema', Patching,
	N'table', Audit,
	N'column', PatchType;
	
exec sys.sp_addextendedproperty 
	@name='MS_Description', 
	@value='Contains information regarding patching audits.',
	@level0type='SCHEMA', 
	@level0Name='Patching',
	@level1type='Table', 
	@level1Name='Audit';
	
go

create table DBVersion
(
  DBVersionId integer identity(1,1) not null,
  DatePatched datetime not null constraint DF_DBVersion_DatePatched default getdate(),
  VersionNumber decimal(9, 4) not null,
  PatchApplied bit not null,
  Duration integer null,
  Comments varchar(max) null,

  constraint PK_DBVersion_DBVersionId primary key (DBVersionId),
  constraint UQ_DBVersion_VersionNumber unique nonclustered (VersionNumber)
)

exec sys.sp_addextendedproperty 
	@name='MS_Description', 
	@value='Contains information regarding versions',
	@level0type='SCHEMA', 
	@level0Name='dbo',
	@level1type='Table', 
	@level1Name='DBVersion';

go

insert into DBVersion (VersionNumber, PatchApplied) Values (1.0000, 1)

go
