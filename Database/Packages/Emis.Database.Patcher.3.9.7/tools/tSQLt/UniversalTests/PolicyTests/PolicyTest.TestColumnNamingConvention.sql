

if object_id('PolicyTest.TestColumnNamingConvention') is not null
drop procedure PolicyTest.TestColumnNamingConvention;
go

create procedure PolicyTest.TestColumnNamingConvention
	@GenerateUnitTestExceptions bit = 0 -- Don't use this as a substitute for fixing your schemas. This is for legacy only.
as

select 
	s.name as SchemaName,
	o.name as TableName,
	c.name as ColumnName
into #Actual
from PolicyTest.ProductObject o
join sys.schemas s on s.schema_id = o.schema_id
join sys.columns c on c.object_id = o.object_id
left join sys.extended_properties ep 
	on  ep.class = 1 
	and ep.major_id = o.object_id
	and ep.minor_id = c.column_id
	and ep.name = 'UnitTestException_ColumnNamingConvention'
where o.type = 'U'
and ep.major_id is null
and (
	ascii(substring(c.name, 1, 1)) between ascii('a') and ascii('z')
	or c.name like '%[ _]%'
);

if @GenerateUnitTestExceptions = 1
begin
	select
		'exec sys.sp_addextendedproperty
		@name = N''UnitTestException_ColumnNamingConvention'',
		@value = N''Created prior to unit test being added'',
		@level0type = ''SCHEMA'',
		@level0name = ''' + SchemaName + ''',
		@level1type = ''TABLE'',
		@level1name = ''' + TableName + ''',
		@level2type = ''COLUMN'',
		@level2name = ''' + ColumnName + '''

'
	from #Actual
	order by SchemaName, TableName;
end
else
begin
	exec tSQLt.AssertEmptyTable '#Actual', 'This column''s name does not conform to our naming conventions (PascalCase, with no spaces or underscores)';
end;
go
