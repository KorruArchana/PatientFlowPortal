
if object_id('PolicyTest.TestCheckConstraintsThatShouldBeForeignKeys') is not null
drop procedure PolicyTest.TestCheckConstraintsThatShouldBeForeignKeys;
go

create procedure PolicyTest.TestCheckConstraintsThatShouldBeForeignKeys
	@GenerateUnitTestExceptions bit = 0 -- Don't use this as a substitute for fixing your schemas. This is for legacy only.
as

select 
	schema_name(po.schema_id) as SchemaName,
	po.name as ObjectName,
	c.name as ColumnName,
	cc.name as ConstraintName
into #Actual
from sys.check_constraints cc
join PolicyTest.ProductObject po 
	on  po.object_id = cc.parent_object_id
join sys.columns c 
	on  c.object_id = cc.parent_object_id 
	and c.column_id = cc.parent_column_id
left join sys.foreign_key_columns fkc
	on  fkc.parent_object_id = c.object_id 
	and fkc.parent_column_id = c.column_id
left join sys.extended_properties ep 
	on  ep.class = 1 
	and ep.major_id = c.object_id
	and ep.minor_id = c.column_id
	and ep.name like 'UnitTestException_CheckConstraintsThatShouldBeForeignKeys%'
where cc.parent_column_id <> 0
and not exists (select * from PolicyTest.Split(substring(cc.definition, 2, len(cc.definition) - 2), ' OR ') where value not like '[[]' + c.name + ']=%')
and fkc.parent_object_id is null
and ep.major_id is null
and schema_name(po.schema_id) <> 'Patching';

if @GenerateUnitTestExceptions = 1
begin
	select distinct
		'exec sys.sp_addextendedproperty
	@name = N''UnitTestException_CheckConstraintsThatShouldBeForeignKeys'',
	@value = N''Created prior to unit test being added'',
	@level0type = ''SCHEMA'',
	@level0name = ''' + SchemaName + ''',
	@level1type = ''TABLE'',
	@level1name = ''' + ObjectName + ''',
	@level1type = ''COLUMN'',
	@level1name = ''' + ColumnName + ''',

'
	from #Actual;
end
else
begin

	exec tSQLt.AssertEmptyTable '#Actual', 'There exist check constraints that appear to be limiting the acceptable values rather than using a
foreign key constraint to do the same task.

';

end