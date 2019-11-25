if object_id('PolicyTest.[TestTablesNotCreatedOnAHeap]') is not null
drop procedure PolicyTest.[TestTablesNotCreatedOnAHeap]
go
 
create procedure PolicyTest.[TestTablesNotCreatedOnAHeap]
/**************************************************************************************************************   
Description:  A test to check that all tables are not created on a heap and have a valid clustered index strategy.
**************************************************************************************************************/
as
set transaction isolation level read committed;
set nocount on;

declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TestTablesNotCreatedOnAHeap';

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

declare @DatabaseName sysname = db_name();

with EMISWebException (
		  DatabaseName
		, SchemaName
		, ObjectName
	) as (
	select 
		db_name(),
		schema_name(o.schema_id),
		o.name
	from sys.extended_properties ep
	join sys.objects o
		on o.object_id = ep.major_id
	where ep.name = @PolicyTestExceptionName
	), ObjectWithException (
		  DatabaseName
		, SchemaId
		, ObjectId
	) as (
		select 
			  DatabaseName
			, schema_id(ewe.SchemaName) as SchemaId 
			, object_id(quotename(ewe.SchemaName) + '.' + quotename(ewe.ObjectName)) as ObjectId
		from EMISWebException ewe
	)
select 
	  schema_name(po.schema_id) as SchemaName
	, po.name as ObjectName
	, i.type_desc as IndexTypeDescription
into #TablesCreatedOnAHeap
from sys.indexes i
	inner join PolicyTest.ProductObject po
		on  i.object_id = po.object_id
	left outer join ObjectWithException oweNotAnException
		on  po.object_id = oweNotAnException.ObjectId
		and po.schema_id = oweNotAnException.SchemaId
		and @DatabaseName like DatabaseName+'%'
where i.type = 0 -- HEAP
and po.[type] in ('U','V') -- (User Table and View [for indexed views]).
and oweNotAnException.ObjectId is null
and not exists (
	select *
	from sys.extended_properties epSchema
	where epSchema.name like @PolicyTestExceptionName
	and epSchema.class_desc = 'SCHEMA'
	and epSchema.major_id = po.schema_id
);

exec tSQLt.AssertEmptyTable '#TablesCreatedOnAHeap';

drop table #TablesCreatedOnAHeap;

go