
if object_id('PolicyTest.TestVarcharAndDecimalHaveLengthSpecified') is not null
drop procedure PolicyTest.TestVarcharAndDecimalHaveLengthSpecified;
go

create procedure PolicyTest.TestVarcharAndDecimalHaveLengthSpecified
as

select 
	ss.name as SchemaName,
	po.name as ObjectName,
	sc.name as ColumnName,
	case po.type
		when 'U' then 'Table'
		when 'TF' then 'Table-Valued Function'
		when 'TT' then 'User-defined table type'
	end as ObjectType
into #Actual
from PolicyTest.ProductObject po
join sys.columns sc on sc.object_id = po.object_id
join sys.schemas ss on ss.schema_id = po.schema_id
join sys.types t on t.system_type_id = sc.system_type_id
left join sys.extended_properties ep on ep.major_id = po.object_id and ep.minor_id = sc.column_id and ep.name = 'UnitTestException_TestVarcharAndDecimalHaveLengthSpecified'
where ( 
	(t.name in ('decimal', 'numeric') and sc.precision = 18 and sc.scale = 0)
	or (t.name in ('binary', 'varbinary') and sc.max_length = 30)
	or (t.name in ('varchar', 'nvarchar') and sc.max_length = 1) 
)
and po.type in ('U', 'TF', 'TT')
and ep.major_id is null;

exec tSQLt.AssertEmptyTable '#Actual';

drop table #Actual;
go