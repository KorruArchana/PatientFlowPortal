

if object_id('PolicyTest.TestHungarianNaming') is not null
drop procedure PolicyTest.TestHungarianNaming;
go

create procedure PolicyTest.TestHungarianNaming
	@GenerateUnitTestExceptions bit = 0 -- Don't use this as a substitute for fixing your schemas. This is for legacy only.
as

select 
	o.type_desc as ObjectType,
	s.name as SchemaName,
	o.name as TableName
into #Actual
from PolicyTest.ProductObject o
join sys.schemas s on s.schema_id = o.schema_id
left join sys.extended_properties ep 
	on  ep.class = 1 
	and ep.major_id = o.object_id
	and ep.name = 'UnitTestException_HungarianNaming'
where ep.major_id is null
and (
	o.name like 'sp[_]%'
	or o.name like 'usp[_]%'
	or o.name like 'fn[_]%'
	or o.name like 'vw[_]%'
	or o.name like 'tbl[_]%'
)

if @GenerateUnitTestExceptions = 1
begin
	select
		'exec sys.sp_addextendedproperty
		@name = N''UnitTestException_HungarianNaming'',
		@value = N''Created prior to unit test being added'',
		@level0type = ''SCHEMA'',
		@level0name = ''' + SchemaName + ''',
		@level1type = ''TABLE'',
		@level1name = ''' + TableName + ''''
	from #Actual
	order by SchemaName, TableName;
end
else
begin
	exec tSQLt.AssertEmptyTable '#Actual', 'Don''t prefix object names with a type identifier.';
end;