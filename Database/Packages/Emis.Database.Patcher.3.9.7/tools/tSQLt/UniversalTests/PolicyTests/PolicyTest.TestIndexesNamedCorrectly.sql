if object_id('PolicyTest.[TestIndexesNamedCorrectly]') is not null
drop procedure PolicyTest.[TestIndexesNamedCorrectly]
go
 
create procedure PolicyTest.[TestIndexesNamedCorrectly]
/**************************************************************************************************************   
Description:  A test to check that all indexes follow the current in house index naming convention.
**************************************************************************************************************/
as
set transaction isolation level read committed
set nocount on

--create exception list
if object_id('tempdb..#ExecptionList') is not null
	drop table #ExecptionList;

create table #ExecptionList(
	SchemaName nvarchar(128) not null,
	TableName nvarchar(128) not null, 
	IndexName nvarchar(128) not null
);

--Populate this table with indexes that you would not liked picked up by this policy test.
insert into #ExecptionList(
	SchemaName, 
	TableName, 
	IndexName)
select 
	schema_name(o.schema_id),
	o.name,
	i.name
from sys.extended_properties ep
	inner join sys.indexes i
		on  ep.major_id = i.object_id
		and ep.minor_id = i.index_id
	inner join sys.objects o
		on i.object_id = o.object_id
where ep.class = 7
and ep.name like 'UnitTestException_TestIndexesNamedCorrectly';

with IndexName (
	SchemaName,
	TableName, 
	CurrentName, 
	CorrectNameExcFreeText
) as (
	select
		schema_name(o.Schema_id),
		object_name(i.object_id), --Table name
		i.name as CurrentName, --Current index name
		case 
			when i.is_primary_key = 1 then 'PK_'
			when i.is_unique = 1 and i.has_filter = 0 then 'UQ_'
			when i.is_unique = 1 and i.has_filter = 1 then 'UQF_'
			when i.is_unique =  0 and i.has_filter = 1 then 'IDXF_'
			else 'IDX_'
		end
		+ (
			case
				when schema_name(o.schema_id) = 'dbo' then '' --default dbo schema not included in naming convention
				else schema_name(o.schema_id) + '_'
			end
		)
		+ object_name(i.object_id) --Table name
		+ cast((
				select 
					'_' + name --Concatinate column names that make up the index.
				from sys.columns c
				join sys.index_columns ic
					on ic.column_id = c.column_id
					and ic.key_ordinal <> 0 --Columns are part of the index
					and ic.object_id  = c.object_id
					and ic.index_id = i.index_id
				where c.object_id = i.object_id
				order by ic.key_ordinal asc
				for xml path ('') --Code cheat. XML used here to transform multiple rows into a single text string.
			)
		 as sysname) as CorrectNameExcFreeText
	from sys.indexes i
	join PolicyTest.ProductObject o
		on o.object_id = i.object_id
		and o.type in ('U', 'V') --Tables and Views only.
	where i.index_id <> 0 --Not heap indexes
	and i.is_primary_key = 0 --Exclude PK constraints
	and i.is_unique_constraint = 0 --Exclude UQ constraints
), CorrectIndexNameCaseSensitive as (
	select	
		cast(idxn.SchemaName as nvarchar(256)) as SchemaName,
		cast(idxn.TableName as nvarchar(256)) as TableName,
		cast(idxn.CurrentName as nvarchar(128)) collate SQL_Latin1_General_CP1_CS_AS as CurrentName,
		cast(idxn.CorrectNameExcFreeText + (
				case 
					when patindex('%[_][_]%', idxn.CurrentName) = 0 then ''
					else substring(idxn.CurrentName, patindex('%[_][_]%', idxn.CurrentName), len(idxn.CurrentName) + 1)
				end 
			) as nvarchar(128)) collate SQL_Latin1_General_CP1_CS_AS as CorrectName
	from IndexName idxn
	where not exists (
		select *
		from #ExecptionList el
		where el.SchemaName = idxn.SchemaName
		and el.TableName = idxn.TableName
		and el.IndexName = idxn.CurrentName
	)
)
select 
	SchemaName,
	TableName,
	CurrentName,
	CorrectName
into #IncorrectlyNamedIndex
from CorrectIndexNameCaseSensitive
where CurrentName <> CorrectName;
					
exec tSQLt.AssertEmptyTable @TableName = '#IncorrectlyNamedIndex';