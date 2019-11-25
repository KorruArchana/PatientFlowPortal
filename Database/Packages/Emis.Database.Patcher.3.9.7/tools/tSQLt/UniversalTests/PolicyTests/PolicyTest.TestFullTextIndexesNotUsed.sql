
if object_id('PolicyTest.TestFullTextIndexesNotUsed') is not null
drop procedure PolicyTest.TestFullTextIndexesNotUsed;
go

create procedure PolicyTest.TestFullTextIndexesNotUsed
as

set transaction isolation level read committed;
set nocount on;

select
	schema_name(o.schema_id) + '.' + o.name as TableWithFullTextIndex
into #Actual
from sys.objects o
join sys.fulltext_indexes fti on fti.object_id = o.object_id
left join sys.extended_properties ep 
	on  ep.class = 1 
	and ep.major_id = o.object_id 
	and ep.name = 'UnitTestException_FullTextIndexesNotUsed'
where ep.major_id is null;

exec tSQLt.AssertEmptyTable '#Actual', 'Use Admin.TextIndex for search tasks over full text indexes.

';

go

