
if object_id('PolicyTest.TestForeignKeysExistWhereIndicated') is not null
drop procedure PolicyTest.TestForeignKeysExistWhereIndicated;
go

create procedure PolicyTest.TestForeignKeysExistWhereIndicated
as


declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TestForeignKeysExistWhereIndicated';

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
	schema_name(o.schema_id) + '.' + o.name as SourceTable,
	c.name as SourceColumn,
	schema_name(o1.schema_id) + '.' + o1.name as PotentialTargetTablePleaseCheckWhichOne,
	c1.name as PotentialTargetColumnPleaseCheckWhichOne
into #Actual
from PolicyTest.ProductObject o
join sys.columns c on c.object_id = o.object_id
left join sys.foreign_key_columns fkc on fkc.parent_object_id = c.object_id and fkc.parent_column_id = c.column_id
join sys.columns c1 on c1.name = c.name and c1.object_id <> c.object_id
join sys.objects o1 on o1.object_id = c1.object_id
join (
	sys.indexes i
	join sys.index_columns ic on ic.object_id = i.object_id and ic.index_id = i.index_id and not exists (select * from sys.index_columns ic1 where ic1.object_id = ic.object_id and ic1.index_id = ic.index_id and ic1.index_column_id > 1)
) on i.object_id = c1.object_id and ic.column_id = c1.column_id
left join (
	sys.indexes uqi
	join sys.index_columns uqic on uqic.object_id = uqi.object_id and uqic.index_id = uqi.index_id and not exists (select * from sys.index_columns uqic1 where uqic1.object_id = uqic.object_id and uqic1.index_id = uqic.index_id and uqic1.index_column_id > 1)
) on uqi.object_id = c.object_id and uqic.column_id = c.column_id and uqi.is_unique = 1
left join sys.extended_properties ep on ep.major_id = o.object_id and ep.minor_id = c.column_id and ep.name in (@PolicyTestExceptionName, 'UnitTestException_SCTIDColumnHasForeignKeyToKnownCodes')
where o.type = 'U'
and exists (select * from sys.foreign_key_columns fkc1 where fkc1.referenced_object_id = c1.object_id and fkc1.referenced_column_id = c1.column_id)
and fkc.referenced_object_id is null
and uqi.object_id is null
and c.name not like 'GUID'
and ep.major_id is null
and not exists (
	-- Do not include computed columns 
	-- in the analyis as you cannot foreign key 
	-- onto them.
	select *
	from sys.computed_columns cc
	where c.object_id = cc.object_id
	and c.column_id = cc.column_id
	and cc.is_computed = 1
	and cc.is_persisted = 0
)
order by SourceTable, SourceColumn, PotentialTargetTablePleaseCheckWhichOne;

exec tSQLt.AssertEmptyTable '#Actual';