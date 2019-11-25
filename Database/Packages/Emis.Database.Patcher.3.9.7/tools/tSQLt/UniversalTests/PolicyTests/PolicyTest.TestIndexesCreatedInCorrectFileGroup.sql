if object_id('PolicyTest.[TestIndexesCreatedInCorrectFileGroup]') is not null
drop procedure PolicyTest.[TestIndexesCreatedInCorrectFileGroup]
go
 
create procedure PolicyTest.[TestIndexesCreatedInCorrectFileGroup]
/**************************************************************************************************************   
Description:  A test to check that indexes are created in the correct file group following established standards.
**************************************************************************************************************/
as
set transaction isolation level read committed
set nocount on

declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TestIndexesCreatedInCorrectFileGroup%';

if exists (
	select *
	from sys.extended_properties
	where name like @PolicyTestExceptionName
	and class = 0
	and class_desc = 'DATABASE'
	and major_id = 0
	and minor_id = 0
)
begin 
	return;
end;

declare @DesignatedDataSpaceIndexType table (
	DataSpaceId int not null,
	IndexTypeId tinyint not null primary key
);
declare @IncludedIndexType table (
	IndexTypeId tinyint not null primary key,
	IsNonClustered bit not null
);
declare @IndexLocationException table (
	SchemaId int not null,
	ObjectId int not null,
	IndexName nvarchar(128) not null primary key,
	DataSpaceId int not null,
	IndexTypeId tinyint not null
);

insert into @IncludedIndexType (
	IndexTypeId,
	IsNonClustered
)
select 
	IndexTypeId,
	IsNonClustered
from (
	values
	(0,0), -- HEAP (effectively Clustered)
	(1,0), -- CLUSTERED
	(2,1)  -- NONCLUSTERED
) IncludedIndexType (
	IndexTypeId,
	IsNonClustered
);

if exists (select * from sys.data_spaces where name like '%_Data')
begin 

	insert into @DesignatedDataSpaceIndexType (
		DataSpaceId,
		IndexTypeId
	)
	select 
		ds.data_space_id,
		IndexType.IndexTypeId
	from sys.data_spaces ds
		cross join @IncludedIndexType IndexType
	where ds.name like '%_Data'
	and IndexType.IsNonClustered = 0;

end;

if exists (select * from sys.data_spaces where name like '%_Indexes')
begin 

	insert into @DesignatedDataSpaceIndexType (
		DataSpaceId,
		IndexTypeId
	)
	select 
		ds.data_space_id,
		IndexType.IndexTypeId
	from sys.data_spaces ds
		cross join @IncludedIndexType IndexType
	where ds.name like '%_Indexes'
	and IndexType.IsNonClustered = 1;

end;

-- Anything not defined at this point will be on the default.
-- For EMIS Web and similar this will impact no rows.
insert into @DesignatedDataSpaceIndexType (
	DataSpaceId,
	IndexTypeId
)
select 
	ds.data_space_id,
	IndexType.IndexTypeId
from sys.data_spaces ds
	cross join @IncludedIndexType IndexType
	left outer join @DesignatedDataSpaceIndexType ddsitNotDefined
		on  IndexType.IndexTypeId = ddsitNotDefined.IndexTypeId
where ds.is_default = 1
and ddsitNotDefined.IndexTypeId is null;

insert into @IndexLocationException (
	SchemaId,
	ObjectId,
	IndexName,
	DataSpaceId,
	IndexTypeId
)
select 
	ep.schema_id as SchemaId,
	ep.object_id as ObjectId,
	isnull(i.name, ep.SchemaName + '.' + ep.ObjectName) as IndexName,
	i.data_space_id as DataSpaceId,
	i.[type] as IndexTypeId
from PolicyTest.ObjectExtendedProperty ep
	inner join sys.indexes i
		on  ep.major_id = i.object_id
		and ep.minor_id = i.index_id
	inner join sys.objects o
		on i.object_id = o.object_id
	inner join sys.data_spaces ds
		on i.data_space_id = ds.data_space_id
where ep.class in(1,7)
and ep.name like @PolicyTestExceptionName;

select 
	schema_name(po.schema_id) as SchemaName,
	po.name as ObjectName,
	i.name as IndexName,
	i.type_desc as IndexTypeDescription,
	ds.name as CurrentDataSpaceName,
	dsCorrect.name as CorrectDataSpaceName
into #IndexesInWrongFileGroup
from sys.indexes i
	inner join @IncludedIndexType iit
		on  i.[type] = iit.IndexTypeId
	inner join PolicyTest.ProductObject po
		on  i.object_id = po.object_id
	inner join sys.data_spaces ds
		on  i.data_space_id = ds.data_space_id
	inner join @DesignatedDataSpaceIndexType dssitCorrect
		on  i.[type] = dssitCorrect.IndexTypeId
	inner join sys.data_spaces dsCorrect
		on  dssitCorrect.DataSpaceId = dsCorrect.data_space_id
	left outer join @DesignatedDataSpaceIndexType ddsitIncorrect
		on  i.[type] = ddsitIncorrect.IndexTypeId
		and i.data_space_id = ddsitIncorrect.DataSpaceId
	left outer join @IndexLocationException ileNotAnException
		on  isnull(i.name, schema_name(po.schema_id)+'.'+po.name) = ileNotAnException.IndexName
		and i.[type] = ileNotAnException.IndexTypeId
		and i.data_space_id = ileNotAnException.DataSpaceId
		and po.schema_id = ileNotAnException.SchemaId
		and po.object_id = ileNotAnException.ObjectId
where ddsitIncorrect.DataSpaceId is null
and ds.type = 'FG' -- Only interested in ROWS_FILEGROUP not PARTITION_SCHEME
and po.[type] in ('U','V') -- (User Table and View [for indexed views]).
and ileNotAnException.DataSpaceId is null; -- Not an exception.

exec tSQLt.AssertEmptyTable '#IndexesInWrongFileGroup';

drop table #IndexesInWrongFileGroup;

