
if object_id('PolicyTest.TestColumnNaming') is not null
drop proc PolicyTest.TestColumnNaming;
go

create proc PolicyTest.TestColumnNaming
as

declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TestColumnNaming';

select 
	po.SchemaName as SchemaName,
	po.name as ObjectName,
	c.name as ColumnName
into #Results
from PolicyTest.ProductObject po
join sys.columns c on c.object_id = po.object_id
left join sys.extended_properties ep on ep.name = @PolicyTestExceptionName and ep.major_id = c.object_id and ep.minor_id = c.column_id
where po.type = 'U'
and po.SchemaName <> 'Patching'
and c.name in ('ID', 'GUID')
and ep.name is null;

exec tSQLt.AssertEmptyTable '#Results', 'Do not name columns ''ID'' or ''GUID''.  ';