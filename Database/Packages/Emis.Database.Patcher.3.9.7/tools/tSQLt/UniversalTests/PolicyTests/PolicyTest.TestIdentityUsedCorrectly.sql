
if object_id('PolicyTest.TestIdentityUsedCorrectly') is not null
drop procedure PolicyTest.TestIdentityUsedCorrectly;
go

create procedure PolicyTest.TestIdentityUsedCorrectly  
as  
/*******************************************************************************

PolicyTest.TestIdentityUsedCorrectly

Ensures that @@identity and ident_current are not used

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

if object_id('PolicyTest.expected') is not null drop table PolicyTest.expected;  
if object_id('PolicyTest.actual') is not null drop table PolicyTest.actual;  
  
create table PolicyTest.expected  
(  
	SchemaName sysname,  
	ObjectName sysname,
	PolicyBroken varchar(max)
);

declare @exceptions table
(
	ObjectName sysname
);

-- Only insert exceptions where you REALLY know you should be using @@identity/ident_current!
insert into @exceptions(
	ObjectName)
select 
	schema_name(o.schema_id)+'.'+o.name
from sys.extended_properties ep
join sys.objects o
	on o.object_id = ep.major_id
where ep.name = 'UnitTestException_TestIdentityUsedCorrectly'


select distinct
	ss.name as SchemaName,
	so.name as ObjectName,
	'Inappropriate use of @@identity or ident_current' as PolicyBroken
into PolicyTest.actual
from PolicyTest.ProductObject so
left join @exceptions exc on object_id(exc.ObjectName) = so.object_id
join sys.schemas ss on ss.schema_id = so.schema_id
join sys.syscomments ssc on ssc.id = so.object_id
where (ssc.text like '%@@identity%' or ssc.text like '%ident_current%')
and so.name <> 'sp_creatediagram'
and exc.ObjectName is null
  
exec tSQLt.AssertEqualsTable 'PolicyTest.expected', 'PolicyTest.actual', 'One or more procedures uses @@identity or ident_current!';
go