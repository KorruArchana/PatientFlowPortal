if object_id(N'PolicyTest.TestProceduresReferenceExistingObjects') is not null
begin

  drop procedure PolicyTest.TestProceduresReferenceExistingObjects;

end

go

create procedure PolicyTest.TestProceduresReferenceExistingObjects
as
/*******************************************************************************

PolicyTest.TestProceduresReferenceExistingObjects

Calls into the system tables and finds all dead references, then references are
then tested to make sure they aren't alias then all valid are shown as a table

*******************************************************************************/
set transaction isolation level read committed;
set nocount on;

create table #Actual 
(
	id int identity,
	SchemaName sysname,
	ObjectName sysname,
	ReferencedSchemaName nvarchar(128),
	ReferencedObjectName nvarchar(128)
);

declare @exceptions table
(
	[schema_name] nvarchar(128),
	[entity_name] nvarchar(128) not null
);

declare @nonWarningIds table
(
  id int
);

/*******************************************************************************

Exceptions

Adding warnings to the exception list will prevent that warning from showing 
in the DB patcher. Do not do this unless absolutely necessary.

*******************************************************************************/
insert into @exceptions
(
	[entity_name]
)
values
('srSaveFolder'),		-- CLR stored proc
('srSaveReport'),		-- CLR stored proc
('sysarticles'),		-- System table
('syspublications'),	-- System table
('sysarticles'),		-- System table
('RethrowError');		-- Exception handling

declare @CrossDatabaseSuppressionName varchar(100) = 'TestProceduresReferenceExistingObjects_SuppressCrossDatabaseCallsFor_';
declare @DatabaseForCrossDatabaseCallToIgnore table (
	DatabaseName nvarchar(128) not null primary key
);

with LegacyListForDatabaseForCrossDatabaseCallToIgnore as (
	select 'MKBRuntime' as DatabaseName union all
	select 'msdb' as DatabaseName union all
	select 'CustomerDB' as DatabaseName
)
insert into @DatabaseForCrossDatabaseCallToIgnore (
	DatabaseName
)
select 
	DatabaseName
from LegacyListForDatabaseForCrossDatabaseCallToIgnore
union 
select 
	convert(nvarchar(128),ep.value)
from sys.extended_properties ep
where ep.name like @CrossDatabaseSuppressionName + '%'
and ep.class = 0
and ep.class_desc = 'DATABASE'
and ep.name = @CrossDatabaseSuppressionName + convert(nvarchar(128),ep.value)

declare 
	@id int,
	@schema_name sysname,
	@entity_name sysname,
	@referenced_schema_name nvarchar(128),
	@referenced_entity_name nvarchar(128),
	@sql nvarchar(max),
	@exceptionCount int,
	@validWarning int,
	@rowCount int,
	@counter int;

insert into #Actual
(
  SchemaName,
  ObjectName,
  ReferencedSchemaName,
  ReferencedObjectName
)
select 
  s.name as [schema_name], 
  o.name as [entity_name], 
  ed.referenced_schema_name,
  ed.referenced_entity_name
from PolicyTest.ProductObject o 
join sys.schemas s on s.schema_id = o.schema_id
left join tSQLt.TestClasses tc on tc.SchemaId = s.schema_id
join sys.sql_expression_dependencies ed on ed.referencing_id = o.object_id
where o.name not in ('sp_alterdiagram','sp_creatediagram','sp_dropdiagram','sp_helpdiagramdefinition','sp_helpdiagrams','sp_renamediagram','sp_upgraddiagrams') --system diagraming procedures.
and ed.referenced_id is null																						--no known reference
and referenced_entity_name not in ('value', 'query', 'exist')						--not using xml query
and referenced_entity_name not in ('inserted', 'deleted')							--not inside a cursor/output clause
and isnull(ed.referenced_schema_name,'') not in ('Geodata')							--not a spacial query
and ed.referenced_entity_name not in (select [entity_name] from @exceptions)
and tc.Name is null -- not a tSQLt test class
and not exists (
	select *
	from @DatabaseForCrossDatabaseCallToIgnore dfcdcti
	where dfcdcti.DatabaseName = ed.referenced_database_name
)
order by s.name asc;


set @rowCount = @@rowcount;
set @counter = 1;

while (@counter <= @rowCount) 
begin

	select 
		@id = id,
		@schema_name = SchemaName,
		@entity_name = ObjectName,
		@referenced_schema_name = ReferencedSchemaName,
		@referenced_entity_name = ReferencedObjectName 
	from #Actual
	where id = @counter;
	
	-- Anything that has a schema is definitely a warning.
	if (@referenced_schema_name is null)
	begin
		  
		exec PolicyTest.ReferenceValidator 
			@schema_name, 
			@entity_name, 
			@referenced_entity_name, 
			@validWarning output
	
		if @validWarning = 0
			insert into @nonWarningIds 
			select @id;		
			
	end;
	
	set @counter = @counter + 1;
		
end;

delete from #Actual
where id in (select id from @nonWarningIds);

exec tSQLt.AssertEmptyTable @TableName = '#Actual', @Message = 'One or more stored procedures reference nonexistent objects!';

drop table #Actual;
