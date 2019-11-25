if object_id('PolicyTest.TestIndexesAreNotCoveredByOtherIndexes') is not null
drop procedure PolicyTest.TestIndexesAreNotCoveredByOtherIndexes
go
 
create procedure PolicyTest.TestIndexesAreNotCoveredByOtherIndexes
/**************************************************************************************************************   
Description:  A test to check that indexes are necessary and are not actually covered by other indexes.
**************************************************************************************************************/
as
set transaction isolation level read committed
set nocount on

--Exceptions list
select 
	schema_name(o.schema_id) as SchemaName,
	o.name as ObjectName,
	i.name as IndexName,
	i.object_id as ObjectId,
	i.index_id as IndexId
into #ExceptionList
from sys.extended_properties ep
	inner join sys.indexes i
		on  ep.major_id = i.object_id
		and ep.minor_id = i.index_id
	inner join sys.objects o
		on i.object_id = o.object_id
where ep.class = 7
and ep.name like 'UnitTestException_TestIndexesAreNotCoveredByOtherIndexes';

with IndexColumn as (
	select 
		i.object_id,
		i.name,
		i.index_id,
		ic.key_ordinal,
		ic.column_id,
		ic.is_included_column,
		i.is_unique
	from PolicyTest.ProductObject po
		inner join sys.indexes i
			on  po.object_id = i.object_id
		inner join sys.index_columns ic
			on  i.object_id = ic.object_id
			and i.index_id = ic.index_id
	where i.has_filter = 0
), IndexColumnCount as (
	select 
		ic.object_id,
		ic.name,
		ic.index_id,
		count(*) as ColumnCount
	from IndexColumn ic
	group by 
		ic.object_id,
		ic.name,
		ic.index_id
)
select 
	object_schema_name(ic1.object_id) as ObjectSchemaName,
	object_name(ic1.object_id) as ObjectName,
	ic1.name as CoveringIndexName,
	ic2.name as PotentiallyUnnecessaryIndexName,
	case when exists (
			select *
			from sys.index_columns ic
			where ic1.object_id = ic.object_id
			and ic2.index_id = ic.index_id
			and ic.is_included_column = 1
		) then 'Yes'
		else 'No'
	end as PotentiallyUnnecessaryIndexContainsIncludedColumns
into #IndexesCoveredByOtherIndexes
from IndexColumn ic1
	inner join IndexColumn ic2
		on  ic1.object_id = ic2.object_id
		and ic1.column_id = ic2.column_id
	inner join IndexColumnCount icc2
		on  ic2.object_id = icc2.object_id
		and ic2.index_id = icc2.index_id
where (
	-- either the ordinal positions match or the index already covered is an included column.
	ic1.key_ordinal = ic2.key_ordinal
	or ic2.is_included_column = 1
)
and (
	-- if the index already covered is defining uniqueness then that is an additional constraint being upheld.
	ic2.is_unique = 0
	or (ic1.is_unique = 1 and ic2.is_unique = 1)
)
and ic1.index_id != ic2.index_id
and not exists ( -- indexing duplication is not required for referential integrity.
	select *
	from sys.foreign_keys fk
	where fk.referenced_object_id = ic1.object_id
	and (
		fk.key_index_id = ic1.index_id
		or fk.key_index_id = ic2.index_id
	)
)
and not exists (
	select *
	from #ExceptionList el
	where ic1.object_id = el.ObjectId
	and ic2.index_id = el.IndexId
)
group by 
	ic1.object_id,
	ic1.index_id,
	ic1.name,
	ic2.index_id,
	ic2.name,
	icc2.ColumnCount
having count(*) = icc2.ColumnCount
order by 
	ObjectSchemaName,
	ObjectName,
	PotentiallyUnnecessaryIndexName,
	CoveringIndexName;

exec tSQLt.AssertEmptyTable '#IndexesCoveredByOtherIndexes';