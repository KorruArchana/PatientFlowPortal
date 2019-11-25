if object_id('tSQLtValidationTest.TestAllTestProcedureWillExecute') is not null
drop procedure tSQLtValidationTest.TestAllTestProcedureWillExecute
go
 
create procedure tSQLtValidationTest.TestAllTestProcedureWillExecute
/**************************************************************************************************************   
Description:  A test to check that procedures (that exhibit the properties of a tSQLt unit test) are in the 
correct format to be actually executed by tSQLt.Run, tSQLt.RunTestClass and tSQLt.RunAll.
**************************************************************************************************************/
as
set transaction isolation level read committed;
set nocount on;

with TestClassProcedures (
	ObjectId,
	SchemaName, 
	ProcedureName
) as (
	select
		pr.object_id as ObjectId,
		tc.Name as SchemaName,
		pr.name as ProcedureName
	from sys.procedures pr
		inner join tSQLt.TestClasses tc
			on tc.SchemaId = pr.schema_id
), TestClassProceduresTestAndSetup (
	ObjectId,
	SchemaName, 
	ProcedureName
) as (
	select 
		tcProc.ObjectId,
		tcProc.SchemaName, 
		tcProc.ProcedureName
	from TestClassProcedures tcProc
	where (
		tcProc.ProcedureName like 'Test%'
		or tcProc.ProcedureName = 'Setup'	
	)
), ObjectDependency as (
	select 
		object_id as ReferencingObjectId,
		referenced_major_id as ReferencedObjectId
	from sys.sql_dependencies
	union 
	-- Objects referenced only using "insert-exec" appear in sys.sql_expression_dependencies
	select 
		referencing_id as ReferencingObjectId,
		referenced_id as ReferencedObjectId
	from sys.sql_expression_dependencies
), ProcedureObjectDependency as (
	select 
		ReferencingObjectId,
		ReferencedObjectId	
	from ObjectDependency od
	where exists (
		select *
		from sys.procedures pReferencing
		where pReferencing.object_id = od.ReferencingObjectId
	)
	and exists (
		select *
		from sys.procedures pReferenced
		where pReferenced.object_id = od.ReferencedObjectId
	)
)
select 
	tcProc.SchemaName, 
	tcProc.ProcedureName
into #TestProcedureThatWillNotExecute
from TestClassProcedures tcProc
where not exists (
	select *
	from TestClassProceduresTestAndSetup tcptas
	where tcptas.ObjectId = tcProc.ObjectId
)
and not exists (
	select *
	from TestClassProceduresTestAndSetup tcptasNotReferenced
		inner join ProcedureObjectDependency pod
			on  tcptasNotReferenced.ObjectId = pod.ReferencingObjectId
	where pod.ReferencedObjectId = tcProc.ObjectId
)
and exists (
	select *
	from ProcedureObjectDependency pod
		inner join sys.all_objects ao
			on  pod.ReferencedObjectId = ao.object_id
	where ao.schema_id = schema_id('tSQLt')
	and tcProc.ObjectId = pod.ReferencingObjectId
)

					
exec tSQLt.AssertEmptyTable '#TestProcedureThatWillNotExecute';

drop table #TestProcedureThatWillNotExecute;

go