
if object_id('PolicyTest.TestForUnusedParameters') is not null
drop procedure PolicyTest.TestForUnusedParameters;
go

create procedure PolicyTest.TestForUnusedParameters  
as  
/*******************************************************************************

PolicyTest.TestForUnusedParameters

Ensures that all stored procedure parameters going forward are used

*******************************************************************************/

set transaction isolation level read committed;
set nocount on;

select
	ss.name as SchemaName,
	o.name as ObjectName,
	ap.name as ParameterName
into #TestForUnusedParametersActual
from PolicyTest.ProductObject o
join sys.schemas ss on ss.schema_id = o.schema_id
join sys.all_parameters ap on ap.object_id = o.object_id
join sys.sql_modules m on m.object_id = o.object_id
left outer join sys.extended_properties epException
	on  o.object_id = epException.major_id
	and ap.parameter_id = epException.minor_id
	and epException.name = 'UnitTestException_TestForUnusedParameters'
	and epException.class_desc = 'PARAMETER'
where m.definition not like '%' + ap.name + '%' + ap.name + '%'
and epException.major_id is null
order by 
	ss.name, 
	o.name, 
	ap.parameter_id

exec tSQLt.AssertEmptyTable @TableName = '#TestForUnusedParametersActual';