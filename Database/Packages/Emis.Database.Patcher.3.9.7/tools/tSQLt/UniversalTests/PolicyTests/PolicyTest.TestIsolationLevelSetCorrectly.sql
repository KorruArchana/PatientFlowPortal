
if object_id('PolicyTest.TestIsolationLevelSetCorrectly') is not null
drop procedure PolicyTest.TestIsolationLevelSetCorrectly;
go

create procedure PolicyTest.TestIsolationLevelSetCorrectly
as
/*******************************************************************************

PolicyTest.TestIsolationLevelSetCorrectly

Ensures that all stored procedures set isolation level explicitly

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

declare @PolicyTestExceptionName varchar(100) = 'UnitTestException_TestIsolationLevelSetCorrectly%';

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

select
	s.[name] as SchemaName,
	ao.[name] as ObjectName,
	'Not setting isolation level explicitly in stored procedure.' as PolicyBroken
into #IsolationLevelSetNotCorrectly
from sys.schemas s
left join tSQLt.TestClasses tc on tc.SchemaId = s.schema_id
join sys.all_objects ao on s.schema_id = ao.schema_id
join sys.all_sql_modules asm on ao.object_id = asm.object_id
where s.principal_id = 1 -- user schema
and ao.object_id > 0 -- non-system object					
-- Filtering...		
and ao.[type]	= 'P'
and s.[name] <> 'tSQLt' -- Not a tSQLt object
and tc.Name is null -- Not a test class
and replace(replace(asm.[definition],char(9),''),' ','') not like '%settransactionisolationlevel%'
and ao.name not in ('sp_alterdiagram','sp_creatediagram','sp_dropdiagram','sp_helpdiagramdefinition','sp_helpdiagrams','sp_renamediagram','sp_upgraddiagrams') --system diagraming procedures.
order by 1, 2

exec tSQLt.AssertEmptyTable @TableName = '#IsolationLevelSetNotCorrectly';