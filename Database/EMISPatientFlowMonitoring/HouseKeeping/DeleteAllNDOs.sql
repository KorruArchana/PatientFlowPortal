
create table #ObjectToDrop (
	  ObjectId int not null primary key
	, ObjectType varchar(3) not null
	, SchemaName nvarchar(128) not null
	, ObjectName nvarchar(128) not null
	, ObjectTypeDropOrder tinyint not null
	, FullObjectName as (quotename(SchemaName) + '.' + quotename(ObjectName)) persisted
);
create table #SchemaBoundDependentObjectDrop (
	  DependentObjectId int not null primary key
	, MaxLinkLevel int not null
)
declare @ObjectTypeTSQL table (
	  ObjectType varchar(3) not null primary key
	, FullFunctionalName varchar(20) not null
);
declare @NewLine char(2) = char(13) + char(10);
declare @ObjectDropSQL nvarchar(max) = N'';

insert into @ObjectTypeTSQL (
	  ObjectType
	, FullFunctionalName
)
select 
	  'P' as ObjectType
	, 'procedure' as FullFunctionalName
union all  
select 
	  'PC' as ObjectType
	, 'procedure' as FullFunctionalName
union all  
select 
	  'TR' as ObjectType
	, 'trigger' as FullFunctionalName
union all  
select 
	  'V' as ObjectType
	, 'view' as FullFunctionalName
union all
select 
	  'FN' as ObjectType
	, 'function' as FullFunctionalName
union all    
select 
	  'TF' as ObjectType
	, 'function' as FullFunctionalName
union all    
select 
	  'FT' as ObjectType
	, 'function' as FullFunctionalName
union all    
select 
	  'IF' as ObjectType
	, 'function' as FullFunctionalName
union all    
select 
	  'FS' as ObjectType
	, 'function' as FullFunctionalName;


insert into #ObjectToDrop (
	  ObjectId
	, ObjectType
	, SchemaName
	, ObjectName
	, ObjectTypeDropOrder
)
-- Procedures
select 
	  o.object_id as ObjectId
	, o.type as ObjectType
	, s.name as SchemaName
	, o.name as ObjectName
	, 1 as ObjectTypeDropOrder
from sys.objects o
join sys.schemas s on s.schema_id = o.schema_id
where o.type in ('P', 'PC')
and s.name <>  'tSQLt'
and o.is_ms_shipped = 0
union all
-- Triggers
select 
	  o.object_id as ObjectId
	, o.type as ObjectType
	, s.name as SchemaName
	, o.name as ObjectName
	, 2 as ObjectTypeDropOrder	
from sys.objects o
join sys.schemas s on s.schema_id = o.schema_id
where o.type = 'TR'
and s.name <>  'tSQLt'
and o.is_ms_shipped = 0
and o.name not like '%__LibraryDatabaseBackwardsCompatability' -- Temp: WI61485
and o.name not like '%__PatientChangeLog'
and o.name not like '%__PatientAdminConfig'
and o.name not like '%__ChangeLog'
union all
-- Views
select 
	  o.object_id as ObjectId
	, o.type as ObjectType
	, s.name as SchemaName
	, o.name as ObjectName
	, 3 as ObjectTypeDropOrder	
from sys.objects o
join sys.schemas s on s.schema_id = o.schema_id
where o.type = 'V'
and s.name <>  'tSQLt'
and o.is_ms_shipped = 0
and not exists	(
	select * from sys.indexes i
	where o.object_id = i.object_id
)
union all
-- Functions
select 
	  o.object_id as ObjectId
	, o.type as ObjectType
	, s.name as SchemaName
	, o.name as ObjectName
	, 4 as ObjectTypeDropOrder	
from sys.objects o
join sys.schemas s on s.schema_id = o.schema_id
where o.type in ('FN', 'TF', 'FT', 'IF', 'FS')
and s.name <>  'tSQLt'
and o.is_ms_shipped = 0;

--- Restrict objects from being dropped
delete otd
from #ObjectToDrop otd
inner join sys.extended_properties ep on otd.ObjectId = ep.major_id
where ep.name = 'StoredProcedure_DeleteAllNDOs_DropException';

-- Sort out dependencies
with SchemaBoundDependency as (
	select 
		  d.referenced_major_id as AncestorLinkedObjectId
		, d.referenced_major_id as LinkedObjectId
		, av.object_id as DependentObjectId
		, 0 as LinkLevel
	from #ObjectToDrop otd
		inner join sys.sql_dependencies d
			on  otd.ObjectId = d.referenced_major_id
		inner join sys.all_objects av
			on  d.object_id = av.object_id
		inner join sys.all_sql_modules asm
			on  d.object_id = asm.object_id
		inner join #ObjectToDrop otdCanDrop
			on  av.object_id = otdCanDrop.ObjectId
	where d.class = 1 /* OBJECT_OR_COLUMN_REFERENCE_SCHEMA_BOUND */
	and asm.is_schema_bound = 1  
	and av.is_ms_shipped = 0
	union all
	select 
		  sbd.AncestorLinkedObjectId as AncestorLinkedObjectId
		, d.referenced_major_id as LinkedObjectId
		, av.object_id as DependentObjectId
		, sbd.LinkLevel + 1 as LinkLevel
	from SchemaBoundDependency sbd
		inner join sys.sql_dependencies d
			on  sbd.DependentObjectId = d.referenced_major_id
		inner join sys.all_objects av
			on  d.object_id = av.object_id		
		inner join sys.all_sql_modules asm
			on  d.object_id = asm.object_id
		inner join #ObjectToDrop otdCanDrop
			on  av.object_id = otdCanDrop.ObjectId			
	where d.class = 1 /* OBJECT_OR_COLUMN_REFERENCE_SCHEMA_BOUND */
	and asm.is_schema_bound = 1  
	and av.is_ms_shipped = 0
), MaximumLinkLevelForDependentObject as (
	select
		  DependentObjectId
		, max(LinkLevel) as MaxLinkLevel
	from SchemaBoundDependency sdb
	group by DependentObjectId
)
insert into #SchemaBoundDependentObjectDrop (
	  DependentObjectId
	, MaxLinkLevel
)
select 
	  mllfdo.DependentObjectId
	, mllfdo.MaxLinkLevel
from MaximumLinkLevelForDependentObject mllfdo;

select @ObjectDropSQL = (
	  @ObjectDropSQL 
	+ 'drop ' 
	+ isnull(ottsql.FullFunctionalName, 'ErrorMissingObjectTypeMapping')
	+ ' '
	+ otd.FullObjectName
	+ ';'
	+ @NewLine
)
from #SchemaBoundDependentObjectDrop sbdop
	inner join #ObjectToDrop otd
		on  sbdop.DependentObjectId = otd.ObjectId
	left outer join @ObjectTypeTSQL ottsql
		on  otd.ObjectType = ottsql.ObjectType
order by sbdop.MaxLinkLevel desc;

with AllOtherObjectToDrop as (
	select 
		  otd.ObjectId
		, otd.ObjectType
		, otd.FullObjectName
		, otd.ObjectTypeDropOrder
		, row_number() over (partition by otd.ObjectTypeDropOrder order by otd.SchemaName asc, otd.ObjectName asc, otd.ObjectId asc) as ObjectDropOrder
		, row_number() over (partition by otd.ObjectTypeDropOrder order by otd.SchemaName desc, otd.ObjectName desc, otd.ObjectId desc) as ReverseObjectDropOrder
	from #ObjectToDrop otd
		left outer join #SchemaBoundDependentObjectDrop sbdodNull
			on  otd.ObjectId = sbdodNull.DependentObjectId
	where sbdodNull.DependentObjectId is null
), ConcatonatedDropObjectSyntax as (
	select (
		select (
				case when ObjectDropOrder = 1 then 'drop ' + isnull(ottsql.FullFunctionalName, 'ErrorMissingObjectTypeMapping') + ' ' else char(9) end
			+ aootp.FullObjectName
			+ case when ReverseObjectDropOrder = 1 then ';' else ',' end
			+ @NewLine
		)
		from AllOtherObjectToDrop aootp
			left outer join @ObjectTypeTSQL ottsql
				on  aootp.ObjectType = ottsql.ObjectType
		order by 
			  ObjectTypeDropOrder
			, ObjectDropOrder
		for xml path(''), root('sql'), type
	) as DropObjectSyntax
)
select @ObjectDropSQL += ConcatonatedDropObjectSyntax.DropObjectSyntax.value('sql[1]/.','nvarchar(max)')
from ConcatonatedDropObjectSyntax;

exec (@ObjectDropSQL);

drop table #ObjectToDrop;
drop table #SchemaBoundDependentObjectDrop;