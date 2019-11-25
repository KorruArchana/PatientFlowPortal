
if object_id('PolicyTest.TestNoOrphanTablesExist') is not null
drop proc PolicyTest.TestNoOrphanTablesExist;
go

create proc PolicyTest.TestNoOrphanTablesExist
	@GenerateUnitTestExceptions bit = 0 -- Don't use this as a substitute for fixing your schemas. This is for legacy only.
as

select 
	o.SchemaName,
	o.name as TableName
into #Actual
from PolicyTest.ProductObject o
left join sys.extended_properties ep 
	on  ep.class = 1 
	and ep.major_id = o.object_id
	and ep.name like 'UnitTestException_OrphanTables%'
join sys.indexes i on i.object_id = o.object_id and i.is_primary_key = 1
where o.type = 'U'
and ep.major_id is null
and not exists (
	select *
	from sys.columns c
	join sys.index_columns ic on ic.object_id = c.object_id and ic.index_id = i.index_id and ic.column_id = c.column_id
	where c.object_id = o.object_id
	and c.is_identity = 1)
and not exists (
	select * from sys.foreign_key_columns fkc 
	join sys.index_columns ic on ic.object_id = i.object_id and ic.index_id = i.index_id
	join sys.columns c on c.object_id = o.object_id and c.column_id = ic.column_id
	where fkc.parent_object_id = o.object_id and fkc.parent_column_id = c.column_id)
and not exists (
	select * from sys.foreign_key_columns fkc 
	join sys.index_columns ic on ic.object_id = i.object_id and ic.index_id = i.index_id
	join sys.columns c on c.object_id = o.object_id and c.column_id = ic.column_id
	where fkc.referenced_object_id = o.object_id and fkc.referenced_column_id = c.column_id);

if @GenerateUnitTestExceptions = 1
begin
	select distinct
		'exec sys.sp_addextendedproperty
	@name = N''UnitTestException_OrphanTables'',
	@value = N''Created prior to unit test being added'',
	@level0type = ''SCHEMA'',
	@level0name = ''' + SchemaName + ''',
	@level1type = ''TABLE'',
	@level1name = ''' + TableName + '''

'
	from #Actual;
end
else
begin
	exec tSQLt.AssertEmptyTable '#Actual', 'These tables are neither referencing or referenced by another table. 
	
This may imply that there is a missing foreign key relationship.';
end;