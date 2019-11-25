if object_id('PolicyTest.TestForInvalidDataTypeUse') is not null
drop procedure PolicyTest.TestForInvalidDataTypeUse;
go

create procedure PolicyTest.TestForInvalidDataTypeUse
	@GenerateUnitTestExceptions bit = 0 -- Don't use this as a substitute for fixing your schemas. This is for legacy only.
as
/*******************************************************************************

PolicyTest.TestForInvalidDataTypeUse

Ensures that the use of certain types are controlled. 

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TestForInvalidDataTypeUse%';

if exists (
	select *
	from sys.extended_properties
	where name like @PolicyTestExceptionName
	and class = 0
	and class_desc = 'DATABASE'
	and major_id = 0
	and minor_id = 0
)
begin 
	return;
end;

create table #PolicyTestActual (
	ObjectType sysname,
	SchemaName sysname,
	ObjectName sysname,
	ParameterName sysname,
	DataType sysname
);

create table #Exceptions (
	SchemaName sysname,
	ObjectName sysname,
	ParameterName sysname,
	DataType sysname,
 primary key (SchemaName, ObjectName, ParameterName)
);

create table #InvalidDataTypes (
	DataTypeName sysname
	primary key (DataTypeName)
);

insert into #InvalidDataTypes 
(
	DataTypeName
) values
	('sysname'),
	('nvarchar'),
	('ntext'),
	('text'),
	('float'), -- Float/Real are in here because they are not precise. Unless you know what you're doing, use decimal(p,s) instead.
	('real');


insert into #Exceptions (
	SchemaName,
	ObjectName, 
	ParameterName,
	DataType
)
select 
	ep.SchemaName,
	ep.ObjectName, 
	c.name as ParameterName,
	idt.DataTypeName as DataType
from PolicyTest.ProductObject o 
	inner join sys.all_columns c
		on  o.object_id = c.object_id
	inner join PolicyTest.ObjectExtendedProperty ep
		on  c.object_id = ep.major_id
		and c.column_id = ep.minor_id
	inner join sys.systypes stUserType
		on  c.system_type_id = stUserType.xtype
		and c.user_type_id = stUserType.xusertype
	inner join sys.systypes st
		on  stUserType.xtype = st.xusertype
	inner join #InvalidDataTypes idt
		on  st.name = idt.DataTypeName
where ep.name like @PolicyTestExceptionName
and ep.class_desc in ('OBJECT_OR_COLUMN', 'TYPE_COLUMN')
union all
select 
	object_schema_name(o.object_id) as SchemaName,
	object_name(o.object_id) as ObjectName, 
	ap.name as ParameterName,
	idt.DataTypeName as DataType
from PolicyTest.ProductObject o 
	inner join sys.all_parameters ap 
		on  o.object_id = ap.object_id
	inner join PolicyTest.ObjectExtendedProperty ep
		on  o.object_id = ep.major_id
		and ap.parameter_id = ep.minor_id
	inner join sys.systypes stUserType
		on  ap.system_type_id = stUserType.xtype
		and ap.user_type_id = stUserType.xusertype
	inner join sys.systypes st
		on  stUserType.xtype = st.xusertype
	inner join #InvalidDataTypes idt
		on  st.name = idt.DataTypeName
where ep.name like @PolicyTestExceptionName
and (
	ep.class_desc = 'PARAMETER'
	or (ep.class_desc = 'OBJECT_OR_COLUMN' and o.type = 'FN' and ep.minor_id = 0)
);

-- Always allow SSMS's diagramming stuff
insert into #Exceptions (
	SchemaName,
	ObjectName,
	ParameterName,
	DataType
)
values
	('dbo', 'sp_alterdiagram', '@diagramname', 'sysname'),
	('dbo', 'sp_creatediagram', '@diagramname', '|sysname'),
	('dbo', 'sp_dropdiagram', '@diagramname', 'sysname'),
	('dbo', 'sp_helpdiagramdefinition', '@diagramname', 'sysname'),
	('dbo', 'sp_helpdiagrams', '@diagramname', 'sysname'),
	('dbo', 'sp_renamediagram', '@diagramname', 'sysname'),
	('dbo', 'sp_renamediagram', '@new_diagramname', 'sysname');

insert into #PolicyTestActual (
	ObjectType,
	SchemaName,
	ObjectName, 
	ParameterName,
	DataType
)
select
	o.type_desc as ObjectType,
	ss.name as SchemaName,
	o.name as ObjectName,
	ap.name as ParameterName,
	t.name as DataType
from PolicyTest.ProductObject o
join sys.schemas ss on ss.schema_id = o.schema_id
join sys.all_parameters ap on ap.object_id = o.object_id
join sys.types t on ap.system_type_id = t.system_type_id and ap.user_type_id = t.user_type_id
left join #Exceptions e on e.SchemaName = ss.name and e.ObjectName = o.name and e.ParameterName = ap.name
join sys.sql_modules m on m.object_id = o.object_id
join #InvalidDataTypes lt on t.name = lt.DataTypeName
where e.ObjectName is null
and not exists (
	select *
	from sys.extended_properties epSchema
	where epSchema.name like @PolicyTestExceptionName
	and epSchema.class_desc = 'SCHEMA'
	and epSchema.major_id = o.schema_id
);

insert into #PolicyTestActual (
	ObjectType,
	SchemaName,
	ObjectName, 
	ParameterName,
	DataType
)
select 
	o.type_desc as ObjectType,
	ss.name as SchemaName,
	o.name as ObjectName, 
	ac.name as ParameterName,
	ParentType.name as DataType
from PolicyTest.ProductObject o
inner join sys.[all_columns] ac on o.object_id = ac.object_id
inner join sys.types ChildType on ac.[system_type_id] = ChildType.[system_type_id] and ac.[user_type_id] = ChildType.[user_type_id]
inner join sys.types ParentType on ChildType.system_type_id = ParentType.user_type_id
inner join sys.schemas ss on o.schema_id = ss.schema_id
left join #Exceptions e on e.SchemaName = ss.name and e.ObjectName = o.name and e.ParameterName = ac.name
inner join #InvalidDataTypes lt on ParentType.name = lt.DataTypeName
where e.ObjectName is null
and not exists (
	select *
	from sys.extended_properties epSchema
	where epSchema.name like @PolicyTestExceptionName
	and epSchema.class_desc = 'SCHEMA'
	and epSchema.major_id = o.schema_id
);

if @GenerateUnitTestExceptions = 1
begin
	select
		'exec sys.sp_addextendedproperty
		@name = N''' + replace(@PolicyTestExceptionName, '%', '_' + DataType) + ''',
		@value = N''Created prior to unit test being added'',
		@level0type = ''SCHEMA'',
		@level0name = ''' + SchemaName + ''',
		@level1type = ''TABLE'',
		@level1name = ''' + ObjectName + ''',
		@level1type = ''COLUMN'',
		@level1name = ''' + ParameterName + ''''
	from #PolicyTestActual
	order by SchemaName, Objectname, ParameterName;
end
else
	exec tSQLt.AssertEmptyTable '#PolicyTestActual', 'A table Column or @Parameter is using a controlled data type:'

go