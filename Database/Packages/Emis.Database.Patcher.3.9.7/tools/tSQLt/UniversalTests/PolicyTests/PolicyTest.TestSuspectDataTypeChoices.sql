
if object_id('PolicyTest.TestSuspectDataTypeChoices') is not null
drop procedure PolicyTest.TestSuspectDataTypeChoices;
go

create procedure PolicyTest.TestSuspectDataTypeChoices
as

set transaction isolation level read committed;
set nocount on;

select 
	s.name as SchemaName, 
	o.name as ObjectName, 
	c.name as ColumnName,
	t.name as CurrentTypeName,
	case
		when c.name like '%guid' then 'uniqueidentifier'
		when c.name like '%datetime' then 'datetime/datetime2/datetimeoffset'
		when c.name like '%date' then 'date/datetime/datetime2/datetimeoffset'
	end as ExpectedDataType
into #Actual
from sys.columns c
join PolicyTest.ProductObject o on o.object_id = c.object_id
join sys.schemas s on s.schema_id = o.schema_id
join sys.types t on t.system_type_id = c.system_type_id
where o.type = 'U'
and (
	(c.name like '%guid' and t.name <> 'uniqueidentifier')
	or (c.name like '%datetime' and t.name = 'date')
	or (c.name like '%date' and t.name not in ('date', 'datetime2', 'datetimeoffset', 'smalldatetime', 'datetime'))
);

exec tSQLt.AssertEmptyTable '#Actual', 'The following columns are named to imply a well-formed data type but are using a 
real-world type that does not match the name.

';

