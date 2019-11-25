
if object_id('PolicyTest.ProductObject') is not null
drop view PolicyTest.ProductObject;
go

create view PolicyTest.ProductObject
/**************************************************************************************************************   
Description:  Bespoke view of sys.objects. Returns a subset of objects that are to be covered by the policy testing.
**************************************************************************************************************/
as
with ApplicableObject as (
	select 
		s.name as SchemaName,
		o.name,
		o.[object_id],	
		o.principal_id,	
		o.[schema_id],	
		o.parent_object_id,	
		o.[type],
		o.type_desc,	
		o.create_date,	
		o.modify_date,	
		o.is_ms_shipped,
		o.is_published,	
		o.is_schema_published
	from sys.objects o
	join sys.schemas s
		on s.schema_id = o.schema_id
	left outer join tSQLt.TestClasses tsqlttcNotTestClass
		on s.schema_id = tsqlttcNotTestClass.SchemaId
	where o.name collate SQL_Latin1_General_CP1_CS_AS not like 'sys%'  --Database diagrams system tables. Created by SQL server when a database diagram is created.
	and not (s.name = 'PCSMiquest' and o.name like 'Compiler_AutoGen%')  --Exclude PCSMiquest compiler auto generated objects.
	and o.is_ms_shipped = 0 --Exclude microsoft tables.
	and s.name <> 'tSQLt' --Exclude the tSQLt schema from the policy test.
	and s.name <> 'LegacyAudit' -- Exclude legacy audit tables
	and not (s.name = 'dbo' and o.name like 'tSQLt_tempobject_%')  --Exclude tSQLt temporary objects created in dbo schema.
	and tsqlttcNotTestClass.SchemaId is null --Not an object that is a member of a tSQL test class.
	union all
	select 
		s.name as SchemaName,
		tt.name,
		o.[object_id],	
		o.principal_id,	
		tt.[schema_id],	
		o.parent_object_id,	
		o.[type],
		o.type_desc,	
		o.create_date,	
		o.modify_date,	
		0 as is_ms_shipped,
		o.is_published,	
		o.is_schema_published
	from sys.table_types tt
		inner join sys.all_objects o
			on  tt.type_table_object_id = o.object_id
		inner join sys.schemas s
			on tt.[schema_id] = s.schema_id
		left outer join tSQLt.TestClasses tsqlttcNotTestClass
			on s.schema_id = tsqlttcNotTestClass.SchemaId
	where s.name <> 'tSQLt' --Exclude the tSQLt schema from the policy test.
	and not (s.name = 'dbo' and o.name like 'tSQLt_tempobject_%')  --Exclude tSQLt temporary objects created in dbo schema.
	and tsqlttcNotTestClass.SchemaId is null --Not an object that is a member of a tSQL test class.
)
select 
	ao.SchemaName,
	ao.name,
	ao.[object_id],	
	ao.principal_id,	
	ao.[schema_id],	
	ao.parent_object_id,	
	ao.[type],
	ao.type_desc,	
	ao.create_date,	
	ao.modify_date,	
	ao.is_ms_shipped,
	ao.is_published,	
	ao.is_schema_published
from ApplicableObject ao
	left outer join sys.extended_properties epSuppressDatabase
		on  epSuppressDatabase.class = 0
		and epSuppressDatabase.class_desc = 'DATABASE'
		and epSuppressDatabase.major_id = 0
		and epSuppressDatabase.minor_id = 0
		and epSuppressDatabase.name = 'SuppressExecutionOfPolicyTestUnitTestsOnDatabase'
	left outer join sys.extended_properties epSuppressSchema
		on  epSuppressSchema.class = 3
		and epSuppressSchema.class_desc = 'SCHEMA'
		and epSuppressSchema.major_id = ao.[schema_id]
		and epSuppressSchema.minor_id = 0
		and epSuppressSchema.name = 'SuppressExecutionOfPolicyTestUnitTestsOnSchema'
where epSuppressDatabase.major_id is null
and epSuppressSchema.major_id is null;