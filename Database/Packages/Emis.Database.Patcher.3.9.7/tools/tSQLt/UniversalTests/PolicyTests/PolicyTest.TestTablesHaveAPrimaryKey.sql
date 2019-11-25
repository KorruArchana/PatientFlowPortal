if object_id('PolicyTest.[TestTablesHaveAPrimaryKey]') is not null
drop procedure PolicyTest.[TestTablesHaveAPrimaryKey]
go
 
create procedure PolicyTest.[TestTablesHaveAPrimaryKey]
/**************************************************************************************************************   
Description:  A test to check that all tables have a primary key defined.

It could be - in rare circumstances - that it is a valid design choice to not have a primary key however
these situations are the exception. In most cases either there will be a problem with the data modelling meaning
that a valid primary key is not intuitive.

**************************************************************************************************************/
as
set transaction isolation level read committed;
set nocount on;

declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TestTablesHaveAPrimaryKey';

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

select 
	po.type_desc as ObjectType,
	schema_name(po.schema_id) as SchemaName,
	po.name as ObjectName
into #TablesWithoutAPrimaryKey
from PolicyTest.ProductObject po
	left outer join sys.indexes iNoPrimaryKey
		on  po.object_id = iNoPrimaryKey.object_id
		and iNoPrimaryKey.is_primary_key = 1
	left outer join PolicyTest.ObjectExtendedProperty oweNotAnException
		on  po.object_id = oweNotAnException.object_id
		and po.schema_id = oweNotAnException.schema_id
		and oweNotAnException.name = 'UnitTestException_TestTablesHaveAPrimaryKey'
where po.[type] in ('U', 'TT') -- (User Table, User-Definied Table Type).
and iNoPrimaryKey.object_id is null	
and oweNotAnException.object_id is null
and not exists (
	select *
	from sys.extended_properties epSchema
	where epSchema.name like @PolicyTestExceptionName
	and epSchema.class_desc = 'SCHEMA'
	and epSchema.major_id = po.schema_id
)
order by
	SchemaName,
	ObjectName;

exec tSQLt.AssertEmptyTable '#TablesWithoutAPrimaryKey';

drop table #TablesWithoutAPrimaryKey;

go