
if object_id('PolicyTest.TestIdentityColumnWithPotentialAlternativePK') is not null
drop procedure PolicyTest.TestIdentityColumnWithPotentialAlternativePK;
go

create procedure PolicyTest.TestIdentityColumnWithPotentialAlternativePK
	@GenerateUnitTestExceptions bit = 0 -- Don't use this as a substitute for fixing your schemas. This is for legacy only.
as

select 
	s.name as SchemaName,
	o.name as TableName,
	c.name as ColumnName,
	i.name as PotentialAlternativePK
into #Actual
from PolicyTest.ProductObject o
join sys.schemas s on s.schema_id = o.schema_id
join sys.columns c on c.object_id = o.object_id
join sys.indexes i on i.object_id = o.object_id
join sys.index_columns ic on ic.object_id = o.object_id and ic.index_id = i.index_id and ic.index_column_id = 1
join sys.columns icc on icc.object_id = o.object_id and icc.column_id = ic.column_id
left join sys.extended_properties ep 
	on  ep.class = 1 
	and ep.major_id = o.object_id
	and ep.name = 'UnitTestException_IdentityColumnWithPotentialAlternativePK'
where o.type = 'U'
and o.object_id <> object_id('dbo.DBVersion')
and ep.major_id is null
and c.is_identity = 1
and i.is_unique = 1
and i.has_filter = 0
and type_name(icc.system_type_id) not in ('uniqueidentifier', 'varchar')
and not exists (select * from sys.foreign_key_columns fkc where fkc.referenced_object_id = o.object_id and fkc.referenced_column_id = c.column_id)
and not exists (select * from sys.index_columns ic where ic.object_id = o.object_id and ic.index_id = i.index_id and ic.column_id = c.column_id);

if @GenerateUnitTestExceptions = 1
begin
	select distinct
		'exec sys.sp_addextendedproperty
	@name = N''UnitTestException_IdentityColumnWithPotentialAlternativePK'',
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
	exec tSQLt.AssertEmptyTable '#Actual', 'This table has an identity column as its primary key that may be inappropriate.
	
It is not being referenced by any other tables, and there is another unique constraint on the table which may be a more natural choice for a primary key.
Consider removing the identity column altogether and making a different PK from the natural PK.';
end;