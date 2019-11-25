
if object_id('PolicyTest.TestDeprecatedBlobTypesNotUsed') is not null
drop procedure PolicyTest.TestDeprecatedBlobTypesNotUsed;
go

create procedure PolicyTest.TestDeprecatedBlobTypesNotUsed  
as  
/*******************************************************************************

PolicyTest.TestDeprecatedBlobTypesNotUsed

Ensures that no tables contain image, text or ntext columns

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TestDeprecatedBlobTypesNotUsed';

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


with ObjectToBeChecked as (
	select 
		object_id as ObjectId,
		po.schema_id as schema_id,
		schema_name(po.schema_id) as SchemaName,
		po.name as ObjectName,		
		convert(bit,1) as IsUserTable
	from PolicyTest.ProductObject po
	where po.type = 'U'
	union all
	select 
		tt.type_table_object_id as ObjectId,
		tt.schema_id as schema_id,
		schema_name(tt.schema_id) as SchemaName,
		tt.name as ObjectName,
		convert(bit,0) as IsUserTable
	from sys.table_types tt
)
select
	otbc.SchemaName as SchemaName,
	otbc.ObjectName as TableName,
	otbc.IsUserTable,
	sc.name as ColumnName,
	st.name as TypeName
into #DeprecatedBlobTypesInUse
from ObjectToBeChecked otbc
join sys.columns sc on sc.object_id = otbc.ObjectId
join sys.types st on st.system_type_id = sc.system_type_id
where st.name in ('text', 'image', 'ntext')
and not exists (
	-- Allow schema override for items like BI Staging tables where exact data type of source is matched.
	select *
	from sys.extended_properties epSchema
	where epSchema.name like @PolicyTestExceptionName
	and epSchema.class_desc = 'SCHEMA'
	and epSchema.major_id = otbc.schema_id
);

exec tSQLt.AssertEmptyTable @TableName = '#DeprecatedBlobTypesInUse';