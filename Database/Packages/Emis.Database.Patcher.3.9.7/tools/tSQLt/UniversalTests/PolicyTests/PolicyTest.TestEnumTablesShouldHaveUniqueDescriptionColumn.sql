
if object_id('PolicyTest.TestEnumTableShouldHaveUniqueDescriptionColumn') is not null
drop procedure PolicyTest.TestEnumTableShouldHaveUniqueDescriptionColumn;
go

create proc PolicyTest.TestEnumTableShouldHaveUniqueDescriptionColumn
as

with SingleColumnUniqueIndexes as 
(
	select
		i.name,
		i.object_id,
		ic.column_id
	from sys.indexes i
	join sys.index_columns ic on ic.index_id = i.index_id and ic.object_id = i.object_id
	left join sys.index_columns icN on icN.index_id = i.index_id and icN.object_id = i.object_id and icN.index_column_id > 1
	where icN.index_column_id is null
	and i.is_unique = 1
)
select
	s.name + '.' + o.name as ObjectName,
	c1.name as KeyColumnName,
	c2.name as DescriptionColumnName
into #TablesThatLookLikeEnumTablesWithNoUniqueConstraintOnDescriptionColumn
from PolicyTest.ProductObject o
join sys.schemas s on s.schema_id = o.schema_id
join sys.columns c1 on c1.object_id = o.object_id and c1.column_id = 1 and c1.name like '%Id'
join sys.types t1 on t1.system_type_id = c1.system_type_id
join sys.columns c2 on c2.object_id = o.object_id and c2.column_id = 2
join sys.types t2 on t2.system_type_id = c2.system_type_id
left join sys.columns cN on cN.object_id = o.object_id and cN.column_id > 2
join SingleColumnUniqueIndexes scui1 on scui1.object_id = o.object_id and scui1.column_id = c1.column_id
left join SingleColumnUniqueIndexes scui2 on scui2.object_id = o.object_id and scui2.column_id = c2.column_id
left join sys.foreign_key_columns fkc on fkc.parent_object_id = o.object_id and fkc.parent_column_id = c1.column_id
left join sys.extended_properties ep on ep.major_id = o.object_id and ep.minor_id = c2.column_id and ep.name = 'UnitTestException_EnumTableShouldHaveUniqueDescriptionColumn'
where o.type = 'U'
and s.name <> 'PCSMiquest'
and cN.column_id is null -- Two columns only!
and fkc.parent_object_id is null -- "Id column" is not foreign-keyed onto another table.
and t1.name like '%int'
and c1.is_identity = 0
and (t2.name like '%char' and isnull(nullif(c2.max_length,-1),401) <= 400)
and scui2.name is null
and ep.major_id is null;

exec tSQLt.AssertEmptyTable '#TablesThatLookLikeEnumTablesWithNoUniqueConstraintOnDescriptionColumn';

drop table #TablesThatLookLikeEnumTablesWithNoUniqueConstraintOnDescriptionColumn;