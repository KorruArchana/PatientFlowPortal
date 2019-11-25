
if object_id('PolicyTest.ObjectExtendedProperty') is not null
drop view PolicyTest.ObjectExtendedProperty;
go

create view PolicyTest.ObjectExtendedProperty
as 
select 
	schema_name(o.schema_id) as SchemaName,
	object_name(o.object_id) as ObjectName,
	o.object_id,
	o.schema_id,
	ep.class,
	ep.class_desc,
	ep.major_id as OriginalMajorId,
	ep.major_id,
	ep.minor_id,
	ep.name,
	ep.value	
from sys.extended_properties ep
	inner join sys.all_objects o
		on  ep.major_id = o.object_id
	left outer join sys.table_types tt
		on  ep.major_id = tt.user_type_id
where ep.class_desc not in ('TYPE','TYPE_COLUMN')
and tt.user_type_id is null
union all
select 
	schema_name(tt.schema_id) as SchemaName,
	tt.name as ObjectName,
	tt.type_table_object_id as object_id,
	tt.schema_id,
	ep.class,
	ep.class_desc,
	ep.major_id as OriginalMajorId,
	tt.type_table_object_id as major_id,
	ep.minor_id,
	ep.name,
	ep.value	
from sys.extended_properties ep
	inner join sys.table_types tt
		on  ep.major_id = tt.user_type_id
where ep.class_desc in ('TYPE','TYPE_COLUMN')