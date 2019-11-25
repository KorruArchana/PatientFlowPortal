if object_id('PolicyTest.TestIndexesThatShouldBeUniqueAreUnique') is not null
drop procedure PolicyTest.TestIndexesThatShouldBeUniqueAreUnique
go
 
create procedure PolicyTest.TestIndexesThatShouldBeUniqueAreUnique
/**************************************************************************************************************   
Description:  A test to check that all indexes that could be unique (via other contraints on the table) are
correctly defined as unique indexes.
**************************************************************************************************************/
as
set transaction isolation level read committed
set nocount on

--Exceptions list
select 
	schema_name(o.schema_id) as SchemaName,
	o.name as ObjectName,
	i.name as IndexName
into #ExceptionList
from sys.extended_properties ep
	inner join sys.indexes i
		on  ep.major_id = i.object_id
		and ep.minor_id = i.index_id
	inner join sys.objects o
		on i.object_id = o.object_id
where ep.class = 7
and ep.name like 'UnitTestException_TestIndexesThatShouldBeUniqueAreUnique';


select 
	  schema_name(po.schema_id) as SchemaName
	, object_name(po.object_id) as ObjectName
	, iNotUniqueIndex.name as ShouldBeUniqueIndexName
	, iOtherUniqueIndex.name as CoveringUniqueIndexName
into #IndexesThatShouldBeDefinedAsUnique
from PolicyTest.ProductObject po
	inner join sys.indexes iNotUniqueIndex
		on  po.object_id = iNotUniqueIndex.object_id
	inner join sys.indexes iOtherUniqueIndex 
		on  iNotUniqueIndex.object_id = iOtherUniqueIndex.object_id
	inner join sys.index_columns icNotUniqueIndexColumn
		on  iNotUniqueIndex.object_id = icNotUniqueIndexColumn.object_id
		and iNotUniqueIndex.index_id = icNotUniqueIndexColumn.index_id
		and icNotUniqueIndexColumn.is_included_column = 0 -- ignore included columns.
	inner join sys.index_columns icOtherUniqueIndexColumn
		on  iOtherUniqueIndex.object_id = icOtherUniqueIndexColumn.object_id
		and iOtherUniqueIndex.index_id = icOtherUniqueIndexColumn.index_id
		and icOtherUniqueIndexColumn.is_included_column = 0 -- ignore included columns.
		-- matching columns from table
		and icNotUniqueIndexColumn.column_id = icOtherUniqueIndexColumn.column_id
	cross apply (
		select count(*) as OtherUniqueIndexTotalIncludedColumn
		from sys.index_columns icOtherUniqueIndexTotalIncludedColumn
		where iOtherUniqueIndex.object_id = icOtherUniqueIndexTotalIncludedColumn.object_id
		and iOtherUniqueIndex.index_id = icOtherUniqueIndexTotalIncludedColumn.index_id
		and icOtherUniqueIndexTotalIncludedColumn.is_included_column = 0 -- ignore included columns.
	) OtherUniqueIndexTotalIncludedColumn
where iNotUniqueIndex.is_unique = 0 -- non-unique index
and iOtherUniqueIndex.is_unique = 1 -- other unique index on table
and iOtherUniqueIndex.has_filter = 0 -- other unique index on table must not be filtered.
group by 
	  po.schema_id
	, po.object_id
	, iNotUniqueIndex.index_id
	, iNotUniqueIndex.name
	, iOtherUniqueIndex.index_id
	, iOtherUniqueIndex.name
	, OtherUniqueIndexTotalIncludedColumn.OtherUniqueIndexTotalIncludedColumn
having count(*) = OtherUniqueIndexTotalIncludedColumn.OtherUniqueIndexTotalIncludedColumn
order by 
	  schema_name(po.schema_id)
	, object_name(po.object_id)
	, iNotUniqueIndex.name;

--Remove exceptions from found list
delete itsbdau
from #IndexesThatShouldBeDefinedAsUnique itsbdau
left outer join #ExceptionList el
	on el.SchemaName = itsbdau.SchemaName
	and el.ObjectName = itsbdau.ObjectName
	and el.IndexName = itsbdau.ShouldBeUniqueIndexName
where el.SchemaName is not null
and el.ObjectName is not null
and el.IndexName is not null
					
exec tSQLt.AssertEmptyTable '#IndexesThatShouldBeDefinedAsUnique';

go