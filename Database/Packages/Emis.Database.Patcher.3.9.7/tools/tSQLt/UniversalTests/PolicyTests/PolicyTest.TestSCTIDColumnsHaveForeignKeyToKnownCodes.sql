
if object_id('PolicyTest.TestSCTIDColumnsHaveForeignKeyToKnownCodes') is not null
drop procedure PolicyTest.TestSCTIDColumnsHaveForeignKeyToKnownCodes;
go

create procedure PolicyTest.TestSCTIDColumnsHaveForeignKeyToKnownCodes
as
/**************************************************************************************************************   
Description:  A test to ensure that all columns with the type SCTID have a foreign key to CareRecord.KnownCodes.

If this policy test fails on a database other than the CR database due to the addition of a SCTID type on those
databases, then consideration must be taken towards the creation of a KnownCodes table on that database.  In
that case please consult Database Development for advice on how to set that up if you are not sure.

Foreign keys do not have to point directly to CareRecord.KnownCodes, but may point at a unique column in a table
that itself references CareRecord.KnownCodes (e.g. if you have a table that contains a subset of codes)

**************************************************************************************************************/

create table #ConstrainedKnownCodesPrimaryKeys
(
	ObjectId int not null,
	ColumnId int not null,
	primary key (ObjectId, ColumnId)
);

insert into #ConstrainedKnownCodesPrimaryKeys 
(
	ObjectId,
	ColumnId
)
select
	c.object_id,
	c.column_id
from sys.columns c
where c.object_id = object_id('CareRecord.KnownCodes')
and c.name = 'CodeId';

/* Add in any tables/columns that reference CareRecord.KnownCodes, and themselves have a PK/unique constraint
   defined on them i.e. are a valid target for a FK.

   Using 'while' loop rather than recursive CTE for performance. Only 1-2 iterations should be needed unless
   someone goes crazy.
*/
while @@rowcount > 0
begin
	insert into #ConstrainedKnownCodesPrimaryKeys
	(
		ObjectId,
		ColumnId
	)
	select
		fkc.parent_object_id,
		fkc.parent_column_id
	from sys.foreign_key_columns fkc
	join #ConstrainedKnownCodesPrimaryKeys ckcpk
		on  ckcpk.ObjectId = fkc.referenced_object_id
		and ckcpk.ColumnId = fkc.referenced_column_id
	join sys.index_columns ic
		on  ic.object_id = fkc.parent_object_id
		and ic.column_id = fkc.parent_column_id
		and ic.index_column_id = 1
	join sys.indexes i 
		on  i.object_id = ic.object_id and i.index_id = ic.index_id
		and i.is_unique = 1
	where not exists (
		select * from sys.index_columns ic1 
		where ic1.object_id = fkc.parent_object_id
			and ic1.index_id = i.index_id
			and ic1.column_id = fkc.parent_column_id 
			and ic1.index_column_id > 1
	)
	and not exists (
		select * from #ConstrainedKnownCodesPrimaryKeys ckcpk
		where ckcpk.ObjectId = fkc.parent_object_id
		and ckcpk.ColumnId = fkc.parent_column_id
	);
end;

select 
	ss.name as SchemaName, 
	so.name as ObjectName, 
	sc.name as ColumnName,
	case 
		when fkc.constraint_object_id is null and ep.major_id is null then 'Missing foreign key to CareRecord.KnownCodes or a table referencing it'
		when fkc.constraint_object_id is not null and ep.major_id is not null then 'Column in exceptions list yet has foreign key to CareRecord.KnownCodes or a table referencing it'
	end as FailureReason
into #Actual
from sys.columns sc
join sys.types st on st.user_type_id = sc.user_type_id
join sys.objects so on so.object_id = sc.object_id
join sys.schemas ss on ss.schema_id = so.schema_id
left join (
	sys.foreign_key_columns fkc
	join #ConstrainedKnownCodesPrimaryKeys ckcpk 
		on  ckcpk.ObjectId = fkc.referenced_object_id 
		and ckcpk.ColumnId = fkc.referenced_column_id
) on fkc.parent_object_id = sc.object_id and fkc.parent_column_id = sc.column_id
left join sys.extended_properties ep 
	on  ep.major_id = sc.object_id 
	and ep.minor_id = sc.column_id 
	and ep.name = 'UnitTestException_SCTIDColumnHasForeignKeyToKnownCodes'
where so.type = 'U'
and st.name = 'SCTID'
and (
	(fkc.constraint_object_id is null and ep.major_id is null) -- missing FK, no extended property
	or (fkc.constraint_object_id is not null and ep.major_id is not null) -- FK present, extended property present
)
and object_id('CareRecord.KnownCodes') is not null;

exec tSQLt.AssertEmptyTable '#Actual';

drop table #ConstrainedKnownCodesPrimaryKeys;